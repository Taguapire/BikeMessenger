using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SQLite;
using Matrix;
using Matrix.Srv;
using Matrix.Extensions.Client.Presence;
using Matrix.Xmpp;
using System.Reactive.Linq;
using Windows.UI.Popups;
using Matrix.Xml;
using System.Threading.Tasks;
using System.Linq;
using Matrix.Xmpp.Base;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string BM_Ultimo_Item = "";
        private TransferVar LvrTransferVar = new TransferVar();
        public XmppClient xmppClient;
        private string MensajeNuevoRecibido;

        public MainPage()
        {
            InitializeComponent();
            InicializarBasesDeDatos();
            if (LvrTransferVar.MOBILES_XMPP == "S")
            {
                IniciarXMPP(LvrTransferVar.USUARIO, LvrTransferVar.CLAVE, LvrTransferVar.DOMINIO_XMPP);
            }
        }

        //************************************************************************************
        // Conexión Principal al Servidor XMPP
        //************************************************************************************
        private async void IniciarXMPP(string pUsername, string pPassword, string pXmppDomain)
        {
            TaskScheduler UISyncContext = TaskScheduler.FromCurrentSynchronizationContext();

            try
            {
                xmppClient = new XmppClient
                {
                    Username = pUsername,
                    Password = pPassword,
                    XmppDomain = pXmppDomain,
                    Tls = false,
                    HostnameResolver = new SrvNameResolver()
                };

                // connect so the server
                await xmppClient.ConnectAsync();

                await xmppClient.SendPresenceAsync(Show.None, "Online");

                _ = xmppClient.XmppXElementStreamObserver
                    .Where(el => el is Message)
                    .Subscribe(async el =>
                    {
                        MensajeNuevoRecibido = el.Cast<Message>().Body;
                        MessageDialog dialog = new MessageDialog(MensajeNuevoRecibido);
                        string Usuario = "";
                        Usuario = el.Cast<Message>().From.User;
                        if (Usuario != "" && Usuario != null)
                        {
                            Usuario = el.Cast<Message>().From.User;
                            ProcesarMensajeRecibido(Usuario, MensajeNuevoRecibido);
                        }
                        else
                        {
                            Usuario = "ADMINISTRADOR";
                        }
                        dialog.Title = "Mensaje De: " + Usuario;
                        dialog.Options = MessageDialogOptions.None;
                        await Task.Factory.StartNew(() => { _ = dialog.ShowAsync(); }, new System.Threading.CancellationToken(), TaskCreationOptions.PreferFairness, UISyncContext);
                    });
            }
            catch (Matrix.AuthenticationException)
            {
                LvrTransferVar.MOBILES_XMPP = "N";
                LvrTransferVar.EscribirValoresDeAjustes();
            }
            catch (DotNetty.Transport.Channels.ConnectException)
            {
                LvrTransferVar.MOBILES_XMPP = "N";
                LvrTransferVar.EscribirValoresDeAjustes();
            }
            catch (System.Net.Sockets.SocketException)
            {
                LvrTransferVar.MOBILES_XMPP = "N";
                LvrTransferVar.EscribirValoresDeAjustes();
            }
            catch (System.IO.IOException)
            {
                LvrTransferVar.MOBILES_XMPP = "N";
                LvrTransferVar.EscribirValoresDeAjustes();
            }
            catch (System.NullReferenceException)
            {
                LvrTransferVar.MOBILES_XMPP = "N";
                LvrTransferVar.EscribirValoresDeAjustes();
            }
        }

        public bool ProcesarMensajeRecibido(string pUsuario, string pMensajeJSON)
        {
            // Procesar aqui el mensaje recivido
            // Tipos de Mensajes:
            //  01.- Aviso General del Administrador
            //  02.- Aviso de Aceptación de Servicio
            //  03.- Aviso de Rechazo de Servicio
            //  04.- Aviso de Entrega de Servicio
            //  05.- Aviso de No entrega de Servicio
            //  06.- Aviso de Cancelación de Servicio
            //  07.- Aviso de Solicitud de Cotización
            if (pUsuario == "officeboycotizar")
            {
                // Procesar Deserializacion
                Bm_Cotizacion_Database BM_Database_Cotizacion = new Bm_Cotizacion_Database();
                StructBikeMessengerCotizacion CotizacionIO = JsonConvert.DeserializeObject<StructBikeMessengerCotizacion>(pMensajeJSON);
                CotizacionIO.PENTALPHA = LvrTransferVar.PENTALPHA_ID;
                CotizacionIO.RESMENSAJE = "OK";
                CotizacionIO.RESOPERACION = "OK";
                if (BM_Database_Cotizacion.AgregarCotizacion(CotizacionIO))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //  08.- Aviso de Aceptación de Cotización
            //  09.- Aviso de Solicitud de Soporte
            //  10.- Aviso de Reclamo de Cliente
            //  11.- Aviso de Puntaje de Evaluación de Servicio
            //  12.- Aviso de Pago de Servicio por Tarjeta
            //  13.- Aviso de Pago de Servicio por Transferencia
            //  14.- Aviso de Pago de Servicio en Efectivo
            //  15.- Aviso de Traspaso de Servicio
            return true;
        }

        private async void TerminarXMPP()
        {
            try
            {
                if (LvrTransferVar.MOBILES_XMPP == "S")
                {
                    await xmppClient.DisconnectAsync();
                }
            }
            catch (DotNetty.Transport.Channels.ClosedChannelException)
            {
                ;
            }
            catch (DotNetty.Transport.Channels.ConnectException)
            {
                ;
            }
            catch (System.Net.Sockets.SocketException)
            {
                ;
            }
        }

        private void BM_NavPag_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LvrTransferVar.LeerValoresDeAjustes();
            // set the initial SelectedItem
            foreach (NavigationViewItemBase item in BM_NavPag.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "Inicio_Page")
                {
                    BM_NavPag.SelectedItem = item;
                    break;
                }
            }
            _ = CuadroDeContenido.Navigate(typeof(PageInicio));
        }

        private void BM_NavPag_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            LvrTransferVar.LeerValoresDeAjustes();
        }

        private void BM_NavPag_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            LvrTransferVar.LeerValoresDeAjustes();

            try
            {
                string BM_ItemContent = (string)args.InvokedItem;

                if (BM_ItemContent != "" || BM_ItemContent != null)
                {
                    switch (BM_ItemContent)
                    {
                        case "Inicio":
                            if (BM_Ultimo_Item != BM_ItemContent)
                            {
                                BM_Ultimo_Item = BM_ItemContent;
                                BM_NavPag.Header = "Servicios";
                                _ = CuadroDeContenido.Navigate(typeof(PageInicio), xmppClient);
                            }
                            break;

                        case "Empresa":
                            if (LvrTransferVar.ESTADOPARAMETROS == "NADA")
                            {
                                break;
                            }

                            if (BM_Ultimo_Item != BM_ItemContent)
                            {
                                BM_Ultimo_Item = BM_ItemContent;
                                BM_NavPag.Header = "Edición de Empresa";
                                BM_NavPag.IsBackEnabled = true;
                                _ = CuadroDeContenido.Navigate(typeof(PageEmpresa), xmppClient);
                            }
                            break;

                        case "Personal":
                            if (LvrTransferVar.ESTADOPARAMETROS == "NADA")
                            {
                                break;
                            }

                            if (BM_Ultimo_Item != BM_ItemContent)
                            {
                                BM_Ultimo_Item = BM_ItemContent;
                                BM_NavPag.Header = "Administración de Personal";
                                BM_NavPag.IsBackEnabled = true;
                                _ = CuadroDeContenido.Navigate(typeof(PagePersonal), xmppClient);
                            }
                            break;

                        case "Recursos":
                            if (LvrTransferVar.ESTADOPARAMETROS == "NADA")
                            {
                                break;
                            }

                            if (BM_Ultimo_Item != BM_ItemContent)
                            {
                                BM_Ultimo_Item = BM_ItemContent;
                                BM_NavPag.Header = "Administración de Recursos";
                                BM_NavPag.IsBackEnabled = true;
                                _ = CuadroDeContenido.Navigate(typeof(PageRecursos), xmppClient);
                            }
                            break;
                        case "Clientes":
                            if (LvrTransferVar.ESTADOPARAMETROS == "NADA")
                            {
                                break;
                            }

                            if (BM_Ultimo_Item != BM_ItemContent)
                            {
                                BM_Ultimo_Item = BM_ItemContent;
                                BM_NavPag.Header = "Administración de Clientes";
                                BM_NavPag.IsBackEnabled = true;
                                _ = CuadroDeContenido.Navigate(typeof(PageClientes), xmppClient);
                            }
                            break;
                        case "Cotización":
                            if (LvrTransferVar.ESTADOPARAMETROS == "NADA")
                            {
                                break;
                            }

                            if (BM_Ultimo_Item != BM_ItemContent)
                            {
                                BM_Ultimo_Item = BM_ItemContent;
                                BM_NavPag.Header = "Cotizaciones Solicitadas";
                                BM_NavPag.IsBackEnabled = true;
                                _ = CuadroDeContenido.Navigate(typeof(PageCotizacion), xmppClient);
                            }
                            break;
                        case "Servicios":
                            if (LvrTransferVar.ESTADOPARAMETROS == "NADA")
                            {
                                break;
                            }

                            if (BM_Ultimo_Item != BM_ItemContent)
                            {
                                BM_Ultimo_Item = BM_ItemContent;
                                BM_NavPag.Header = "Administración de Servicios";
                                BM_NavPag.IsBackEnabled = true;
                                _ = CuadroDeContenido.Navigate(typeof(PageServicios), xmppClient);
                            }
                            break;
                        case "Ajustes":
                            if (BM_Ultimo_Item != BM_ItemContent)
                            {
                                BM_Ultimo_Item = BM_ItemContent;
                                BM_NavPag.Header = "Configuración y Ajustes";
                                BM_NavPag.IsBackEnabled = true;
                                _ = CuadroDeContenido.Navigate(typeof(PageAjustes), xmppClient);
                            }
                            break;
                        case "Salir":
                            TerminarXMPP();
                            Application.Current.Exit();
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.InnerException.Message);
                Console.WriteLine(ee.Message);
            }
        }

        private void BM_NavPag_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (CuadroDeContenido.CanGoBack)
            {
                CuadroDeContenido.GoBack();
            }
            else
            {
                BM_NavPag.IsBackEnabled = false;
            }
        }

        private void InicializarBasesDeDatos()
        {
            string DbVistaRecursosPropietario = "";
            DbVistaRecursosPropietario += "CREATE VIEW if not exists Vista_Recursos_Propietario ";
            DbVistaRecursosPropietario += "as ";
            DbVistaRecursosPropietario += "select  a.tipo, ";
            DbVistaRecursosPropietario += "a.patente, ";
            DbVistaRecursosPropietario += "a.marca, ";
            DbVistaRecursosPropietario += "a.modelo, ";
            DbVistaRecursosPropietario += "a.variante, ";
            DbVistaRecursosPropietario += "b.apellidos, ";
            DbVistaRecursosPropietario += "b.nombres, ";
            DbVistaRecursosPropietario += "a.ano, ";
            DbVistaRecursosPropietario += "a.ciudad, ";
            DbVistaRecursosPropietario += "a.comuna, ";
            DbVistaRecursosPropietario += "a.region, ";
            DbVistaRecursosPropietario += "a.pais ";
            DbVistaRecursosPropietario += "from TbBikeMessengerRecurso as a, ";
            DbVistaRecursosPropietario += "TbBikeMessengerPersonal as b ";
            DbVistaRecursosPropietario += "where a.pentalpha = b.pentalpha AND ";
            DbVistaRecursosPropietario += "a.rutid = b.rutid AND ";
            DbVistaRecursosPropietario += "a.digver = b.digver ";

            string DbVistaServicioCliMen = "";
            DbVistaServicioCliMen += "CREATE VIEW if not exists Vista_Servicio_CliMen ";
            DbVistaServicioCliMen += "as ";
            DbVistaServicioCliMen += "select a.nroenvio, ";
            DbVistaServicioCliMen += "a.guiadespacho, ";
            DbVistaServicioCliMen += "a.fechaentrega, ";
            DbVistaServicioCliMen += "a.horaentrega, ";
            DbVistaServicioCliMen += "b.nombre, ";
            DbVistaServicioCliMen += "c.apellidos, ";
            DbVistaServicioCliMen += "c.nombres, ";
            DbVistaServicioCliMen += "a.recepcion, ";
            DbVistaServicioCliMen += "a.distancia ";
            DbVistaServicioCliMen += "from TbBikeMessengerServicio a ";
            DbVistaServicioCliMen += "left join TbBikeMessengerCliente b ";
            DbVistaServicioCliMen += "on a.pentalpha = b.pentalpha AND a.clienterut = b.rutid AND a.clientedigver = b.digver ";
            DbVistaServicioCliMen += "left join TbBikeMessengerPersonal c ";
            DbVistaServicioCliMen += "on a.pentalpha = c.pentalpha AND a.mensajerorut = c.rutid AND a.mensajerodigver = c.digver ";

            string DbVistaCotizacionCliMen = "";
            DbVistaCotizacionCliMen += "CREATE VIEW if not exists Vista_Cotizacion_CliMen ";
            DbVistaCotizacionCliMen += "as ";
            DbVistaCotizacionCliMen += "select ";
            DbVistaCotizacionCliMen += "cotizacion, ";
            DbVistaCotizacionCliMen += "nombre, ";
            DbVistaCotizacionCliMen += "tipocarga, ";
            DbVistaCotizacionCliMen += "(ocalle || ' ' || onumero || ' ' || ocomuna) as origen, ";
            DbVistaCotizacionCliMen += "(dcalle || ' ' || dnumero || ' ' || dcomuna) as destino, ";
            DbVistaCotizacionCliMen += "fechaentrega, ";
            DbVistaCotizacionCliMen += "horaentrega, ";
            DbVistaCotizacionCliMen += "distancia "; 
            DbVistaCotizacionCliMen += "from TbBikeMessengerCotizacion ";

            try
            {
                SQLiteConnection BM_ConexionLite = new SQLiteConnection(LvrTransferVar.DIRECTORIO_BASE_LOCAL + "\\BikeMessenger.db");

                _ = BM_ConexionLite.CreateTable<TbBikeMessengerEmpresa>();
                _ = BM_ConexionLite.CreateTable<TbBikeMessengerPersonal>();
                _ = BM_ConexionLite.CreateTable<TbBikeMessengerRecurso>();
                _ = BM_ConexionLite.CreateTable<TbBikeMessengerCliente>();
                _ = BM_ConexionLite.CreateTable<TbBikeMessengerCotizacion>();
                _ = BM_ConexionLite.CreateTable<TbBikeMessengerServicio>();
                _ = BM_ConexionLite.CreateTable<TbBikeMessengerComuna>();
                _ = BM_ConexionLite.CreateTable<TbBikeMessengerCiudad>();
                _ = BM_ConexionLite.CreateTable<TbBikeMessengerRegion>();
                _ = BM_ConexionLite.CreateTable<TbBikeMessengerPais>();
                _ = BM_ConexionLite.Execute(DbVistaRecursosPropietario);
                _ = BM_ConexionLite.Execute(DbVistaServicioCliMen);
                _ = BM_ConexionLite.Execute(DbVistaCotizacionCliMen);

                BM_ConexionLite.Close();
                BM_ConexionLite.Dispose();
                BM_ConexionLite = null;
            }
            catch (SQLiteException Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
            }
        }

        private void BM_NavPag_Unloaded(object sender, RoutedEventArgs e)
        {
            ; 
        }
    }
}