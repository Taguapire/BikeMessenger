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

namespace BikeMessenger
{
    internal class ComunicacionXMPP
    {
        public ComunicacionXMPP()
        {

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
            await xmppClient.SendChatMessageAsync("luis@finanven.ddns.net", "Tienes un servicio Luis");

            // Close connection again
            _ = await xmppClient.DisconnectAsync();
        }
    }
}
