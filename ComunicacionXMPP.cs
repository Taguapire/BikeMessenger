using System;
using System.Reactive.Linq;
using Matrix;
using Matrix.Extensions.Client.Roster;
using Matrix.Extensions.Client.Presence;
using Matrix.Xmpp;
using Matrix.Xmpp.Base;
using Matrix.Srv;
using DotNetty.Transport.Channels;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Matrix.Extensions.Client.Message;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BikeMessenger
{
    internal class ComunicacionXMPP
    {
        AvisoDeEnvio AvisoEnvioXMPP = new AvisoDeEnvio();
        private string CadenaJsonXMPP;

        public ComunicacionXMPP(StructBikeMessengerServicio ServicioXMPP)
        {
            AvisoEnvioXMPP.ENVIO = ServicioXMPP.NROENVIO;
            AvisoEnvioXMPP.FECHA = ServicioXMPP.FECHAENTREGA;
            AvisoEnvioXMPP.HORA = ServicioXMPP.HORAENTREGA;
            AvisoEnvioXMPP.CLIENTE = ServicioXMPP.CLIENTERUT + ServicioXMPP.CLIENTEDIGVER;
            AvisoEnvioXMPP.DESCRIPCION = ServicioXMPP.DESCRIPCION;
            AvisoEnvioXMPP.LATITUDORIGEN = ServicioXMPP.OLATITUD;
            AvisoEnvioXMPP.LONGITUDORIGEN = ServicioXMPP.OLONGITUD;
            AvisoEnvioXMPP.LATITUDDESTINO = ServicioXMPP.DLATITUD;
            AvisoEnvioXMPP.LONGITUDDESTINO = ServicioXMPP.DLONGITUD;
            AvisoEnvioXMPP.DISTANCIA = ServicioXMPP.DISTANCIA;
        }

        public void ProcesarJson()
        {
            CadenaJsonXMPP = JsonConvert.SerializeObject(AvisoEnvioXMPP);
        }

        public async Task ProcesoEnvioMensaje()
        {
            // setup XmppClient with some properties
            XmppClient xmppClient = new XmppClient
            {
                Username = "pruebaswindows",
                Password = "Pruebas1970",
                XmppDomain = "finanven.ddns.net",
                // setting the resolver to use the Srv resolver is optional, but recommended
                HostnameResolver = new SrvNameResolver()
            };

            // connect so the server
            _ = await xmppClient.ConnectAsync();

            // request roster (contact list). This is optional, 
            // but most chat clients do this on startup
            // Matrix.Xmpp.Client.Iq roster = xmppClient.RequestRosterAsync().GetAwaiter().GetResult();

            // send our own presence to the server. This is required for most scenarios
            // but there are also use cases where you may not want to publish and send you own presence
            // await xmppClient.SendPresenceAsync(Show.Chat, "Listo para chatear");

            // send a chat message to user2
            await xmppClient.SendChatMessageAsync("luis@finanven.ddns.net", CadenaJsonXMPP);

            // Close connection again
            _ = await xmppClient.DisconnectAsync();
        }
    }

    internal class AvisoDeEnvio
    {
        public string ENVIO { get; set; }
        public string FECHA { get; set; }
        public string HORA { get; set; }
        public string CLIENTE { get; set; }
        public string DESCRIPCION { get; set; }
        public double LATITUDORIGEN { get; set; }
        public double LONGITUDORIGEN { get; set; }
        public double LATITUDDESTINO { get; set; }
        public double LONGITUDDESTINO { get; set; }
        public double DISTANCIA { get; set; }

        public AvisoDeEnvio ()
        {
            ENVIO = "";
            FECHA = "";
            HORA = "";
            CLIENTE = "";
            DESCRIPCION = "";
            LATITUDORIGEN = 0;
            LONGITUDORIGEN = 0;
            LATITUDDESTINO = 0;
            LONGITUDDESTINO = 0;
            DISTANCIA = 0;
        }
    }
}
