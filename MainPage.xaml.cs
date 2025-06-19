using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SQLite;
using System.Diagnostics;
using System.Threading.Tasks;

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
        private String NombreDeUsuario = "Luis Vasquez";
        private bool UsuarioValido = true;
        private bool UsuarioEmailVerificado = true;

        public MainPage()
        {
            InitializeComponent();
            InicializarBasesDeDatos();
            CambioDeEstadoMenu(UsuarioValido,UsuarioEmailVerificado);
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
            BM_NavPag.Header = "Estado de Servicios\n";
            _ = CuadroDeContenido.Navigate(typeof(PageInicio));
        }

        private void BM_NavPag_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            LvrTransferVar.LeerValoresDeAjustes();
        }

        private async void BM_NavPag_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
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
                                BM_NavPag.Header = "Estado de Servicios\n";
                                _ = CuadroDeContenido.Navigate(typeof(PageInicio));
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
                                _ = CuadroDeContenido.Navigate(typeof(PageEmpresa));
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
                                _ = CuadroDeContenido.Navigate(typeof(PagePersonal));
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
                                _ = CuadroDeContenido.Navigate(typeof(PageRecursos));
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
                                _ = CuadroDeContenido.Navigate(typeof(PageClientes));
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
                                BM_NavPag.Header = "Cotizaciones Solicitadas\n";
                                BM_NavPag.IsBackEnabled = true;
                                _ = CuadroDeContenido.Navigate(typeof(PageCotizacion));
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
                                _ = CuadroDeContenido.Navigate(typeof(PageServicios));
                            }
                            break;
                        case "Ajustes":
                            if (BM_Ultimo_Item != BM_ItemContent)
                            {
                                BM_Ultimo_Item = BM_ItemContent;
                                BM_NavPag.Header = "Configuración y Ajustes";
                                BM_NavPag.IsBackEnabled = true;
                                _ = CuadroDeContenido.Navigate(typeof(PageAjustes));
                            }
                            break;
                        case "Login":
                            await ValidarLogin();
                            break;
                        case "Logout":
                            await ValidarLogout();
                            break;
                        case "Salir":
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

        private async Task ValidarLogin()
        {

            CambioDeEstadoMenu(UsuarioValido, UsuarioEmailVerificado);
        }

        private Task ValidarLogout()
        {
            UsuarioValido = true;
            UsuarioEmailVerificado = true;
            CambioDeEstadoMenu(UsuarioValido, UsuarioEmailVerificado);
            return Task.CompletedTask;
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

        private void CambioDeEstadoMenu(bool pDisponible, bool pEmailVerificado)
        {
            MenuNav_Inicio.IsEnabled = pDisponible;
            MenuNav_Cotizacion.IsEnabled = pDisponible;
            if (pDisponible && pEmailVerificado)
            {
                MenuNav_Empresa.IsEnabled = true;
                MenuNav_Personal.IsEnabled = true;
                MenuNav_Recursos.IsEnabled = true;
                MenuNav_Clientes.IsEnabled = true;
                MenuNav_Servicios.IsEnabled = true;
                MenuNav_Configuracion.IsEnabled = true;
            }
            else
            {
                MenuNav_Empresa.IsEnabled = false;
                MenuNav_Personal.IsEnabled = false;
                MenuNav_Recursos.IsEnabled = false;
                MenuNav_Clientes.IsEnabled = false;
                MenuNav_Servicios.IsEnabled = false;
                MenuNav_Configuracion.IsEnabled = false;
            }
        }

        private void BM_NavPag_Unloaded(object sender, RoutedEventArgs e)
        {
            ;
        }

        private void PaginaPrincipalBikeMessenger_Loaded(object sender, RoutedEventArgs e)
        {
            ;
        }
    }
}