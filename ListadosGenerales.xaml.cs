using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Graphics.Printing;
using Windows.UI.Xaml.Media.Animation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml.Shapes;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListadosGenerales : Page
    {
        private TransferVar LvrTransferVar;

        public ListadosGenerales()
        {
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs navigationEvent)
        {
            // call the original OnNavigatingFrom
            base.OnNavigatingFrom(navigationEvent);

            // when the dialog is removed from navigation stack 
            if (navigationEvent.NavigationMode == NavigationMode.Back)
            {
                // set the cache mode
                NavigationCacheMode = NavigationCacheMode.Disabled;

                ResetPageCache();
            }
        }

        private void ResetPageCache()
        {
            int cacheSize = ((Frame)Parent).CacheSize;

            ((Frame)Parent).CacheSize = 0;
            ((Frame)Parent).CacheSize = cacheSize;
        }

        protected override void OnNavigatedTo(NavigationEventArgs navigationEvent)
        {
            base.OnNavigatedTo(navigationEvent);
            // when the dialog displays then we create viewmodel and set the cache mode

            if (navigationEvent.NavigationMode == NavigationMode.New)
            {
                // set the cache mode
                NavigationCacheMode = NavigationCacheMode.Required;
            }

            if (navigationEvent.Parameter is string @string && !string.IsNullOrWhiteSpace(@string))
            {
                //greeting.Text = $"Hi, {e.Parameter.ToString()}";
            }
            else
            {
                LvrTransferVar = (TransferVar)navigationEvent.Parameter;
                LvrImprimirListados();
            }
        }

        private void BtnListadoGeneralVolver(object sender, RoutedEventArgs e)
        {
            switch (LvrTransferVar.PantallaAnterior)
            {
                case "EMPRESA":
                    _ = Frame.Navigate(typeof(PageEmpresa), LvrTransferVar, new SuppressNavigationTransitionInfo());
                    break;
                case "PERSONAL":
                    _ = Frame.Navigate(typeof(PagePersonal), LvrTransferVar, new SuppressNavigationTransitionInfo());
                    break;
                case "RECURSO":
                    _ = Frame.Navigate(typeof(PageRecursos), LvrTransferVar, new SuppressNavigationTransitionInfo());
                    break;
                case "CLIENTE":
                    _ = Frame.Navigate(typeof(PageClientes), LvrTransferVar, new SuppressNavigationTransitionInfo());
                    break;
                case "SERVICIO":
                    _ = Frame.Navigate(typeof(PageServicios), LvrTransferVar, new SuppressNavigationTransitionInfo());
                    break;
                default:
                    break;
            }
        }

        private void LvrImprimirListados()
        {
            switch (LvrTransferVar.PantallaAnterior)
            {
                case "EMPRESA":
                    break;
                case "PERSONAL":
                    Bm_Personal_Database BM_Database_Personal = new Bm_Personal_Database();
                    BM_Database_Personal.BM_CreateDatabase(LvrTransferVar.TV_Connection);
                    VisorWeb.NavigateToString(BM_Database_Personal.Bm_Personal_Listado());
                    break;
                case "RECURSO":
                    Bm_Recurso_Database BM_Database_Recurso = new Bm_Recurso_Database();
                    BM_Database_Recurso.BM_CreateDatabase(LvrTransferVar.TV_Connection);
                    VisorWeb.NavigateToString(BM_Database_Recurso.Bm_Recursos_Listado());
                    break;
                case "CLIENTE":
                    Bm_Cliente_Database BM_Database_Cliente = new Bm_Cliente_Database();
                    BM_Database_Cliente.BM_CreateDatabase(LvrTransferVar.TV_Connection);
                    VisorWeb.NavigateToString(BM_Database_Cliente.Bm_Clientes_Listado());
                    break;
                case "SERVICIO":
                    Bm_Servicio_Database BM_Database_Servicio = new Bm_Servicio_Database();
                    BM_Database_Servicio.BM_CreateDatabase(LvrTransferVar.TV_Connection);
                    VisorWeb.NavigateToString(BM_Database_Servicio.Bm_Servicios_Listado());
                    break;
                default:
                    break;
            }
        }

        private void LvrImprimir(object sender, RoutedEventArgs e)
        {
            WebViewBrush VisorBrush = new WebViewBrush();
            VisorBrush.SetSource(VisorWeb);
            VisorBrush.Redraw();
            Rectangle VisorRect = new Rectangle
            {
                // = new Rectangle();
                Fill = VisorBrush
            };
        }
    }
}
