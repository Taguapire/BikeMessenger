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
using Windows.UI.Xaml.Controls;

namespace BikeMessenger
{
    internal class ComunicacionXMPP
    {
        private AvisoDeEnvio AvisoEnvioXMPP = new AvisoDeEnvio();
        private string CadenaJsonXMPP;
        public XmppClient xmppClient;
        private Bm_Servicio_Database BM_Database_Servicio = new Bm_Servicio_Database();

        public ComunicacionXMPP()
        {

        }

        public void ProcesarJson(AvisoDeEnvio pServicioXMPP)
        {
            AvisoEnvioXMPP.ENVIO = pServicioXMPP.ENVIO;
            AvisoEnvioXMPP.FECHA = pServicioXMPP.FECHA;
            AvisoEnvioXMPP.HORA = pServicioXMPP.HORA;
            AvisoEnvioXMPP.CLIENTE = pServicioXMPP.CLIENTE;
            AvisoEnvioXMPP.DESCRIPCION = pServicioXMPP.DESCRIPCION;
            AvisoEnvioXMPP.LATITUDORIGEN = pServicioXMPP.LATITUDORIGEN;
            AvisoEnvioXMPP.LONGITUDORIGEN = pServicioXMPP.LONGITUDORIGEN;
            AvisoEnvioXMPP.LATITUDDESTINO = pServicioXMPP.LATITUDDESTINO;
            AvisoEnvioXMPP.LONGITUDDESTINO = pServicioXMPP.LONGITUDDESTINO;
            AvisoEnvioXMPP.DISTANCIA = pServicioXMPP.DISTANCIA;
            CadenaJsonXMPP = JsonConvert.SerializeObject(AvisoEnvioXMPP);
        }

        public async void ProcesarConexionAlserver(string pUsername, string pPassword, string pXmppDomain)
        {
            xmppClient = new XmppClient
            {
                Username = pUsername,
                Password = pPassword,
                XmppDomain = pXmppDomain,
                // setting the resolver to use the Srv resolver is optional, but recommended
                HostnameResolver = new SrvNameResolver()
            };

            // connect so the server
            await xmppClient.ConnectAsync();
            await xmppClient.SendPresenceAsync(Show.None, "Online");
        }

        public async void ProcesarDesconexionDelServer()
        {
            // Desconectar del Servidor
            await xmppClient.DisconnectAsync();
        }

        public async void ProcesoEnvioMensaje(string pMensajeroDestinatario)
        {
            // Envio del Mensaje
            await xmppClient.SendChatMessageAsync(pMensajeroDestinatario, CadenaJsonXMPP);
        }

        public void ProcesoRecibirMensajes()
        {
            xmppClient.XmppXElementStreamObserver
                .Where(el => el is Message)
                .Subscribe(el =>
                {
                    var msgbox = new ContentDialog
                    {
                        Title = "Nuevo Mensaje",
                        Content = el.ToString(),
                        CloseButtonText = "Continuar"
                    };
                    var ignored = msgbox.ShowAsync();

                });
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

        public AvisoDeEnvio()
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
