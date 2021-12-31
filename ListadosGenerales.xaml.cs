using System;
using System.Threading.Tasks;
using Windows.Graphics.Printing;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListadosGenerales : Page
    {
        private TransferVar LvrTransferVar;
        private string HtmlImprimir;

        public ListadosGenerales()
        {
            InitializeComponent();
            // PrintMan.PrintTaskRequested += PrintTaskRequested;
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
                    HtmlImprimir = BM_Database_Personal.Bm_Personal_Listado(LvrTransferVar.EMP_PENTALPHA);
                    VisorWeb.NavigateToString(HtmlImprimir);
                    break;
                case "RECURSO":
                    Bm_Recurso_Database BM_Database_Recurso = new Bm_Recurso_Database();
                    HtmlImprimir = BM_Database_Recurso.Bm_Recurso_Listado(LvrTransferVar.EMP_PENTALPHA);
                    VisorWeb.NavigateToString(HtmlImprimir);
                    break;
                case "CLIENTE":
                    Bm_Cliente_Database BM_Database_Cliente = new Bm_Cliente_Database();
                    HtmlImprimir = BM_Database_Cliente.Bm_Cliente_Listado(LvrTransferVar.EMP_PENTALPHA);
                    try
                    {
                        VisorWeb.NavigateToString(HtmlImprimir);
                    }
                    catch (ArgumentNullException)
                    {
                        ;
                    }
                    break;
                case "SERVICIO":
                    Bm_Servicio_Database BM_Database_Servicio = new Bm_Servicio_Database();
                    HtmlImprimir = BM_Database_Servicio.Bm_Servicio_Listado();
                    VisorWeb.NavigateToString(HtmlImprimir);
                    break;
                default:
                    break;
            }
        }

        private async void LvrImprimir(object sender, RoutedEventArgs e)
        {

            //PrintManager LvrPrintManager;
            //PrintDocument LvrPrintDocument = new PrintDocument();

            //LvrPrintManager = PrintManager.GetForCurrentView();
            //LvrPrintDocument.AddPage(VisorWeb);
            //LvrPrintDocument.AddPagesComplete();

            if (PrintManager.IsSupported())
            {
                try
                {
                    // Show print UI
                    _ = await PrintManager.ShowPrintUIAsync();

                }
                catch
                {
                    // Printing cannot proceed at this time
                    ContentDialog noPrintingDialog = new ContentDialog()
                    {
                        Title = "Error de Impresión",
                        Content = "\nDisculpe, el proceso de impresión no puede iniciarse en estos momentos.",
                        PrimaryButtonText = "OK"
                    };
                    _ = await noPrintingDialog.ShowAsync();
                }
            }
            else
            {
                // Printing is not supported on this device
                ContentDialog noPrintingDialog = new ContentDialog()
                {
                    Title = "Impresión no Soportada",
                    Content = "\nDisculpe, el proceso de impresión no es soportado en este dispositivo.",
                    PrimaryButtonText = "OK"
                };
                _ = await noPrintingDialog.ShowAsync();
            }
        }

        private void LvrAlmacenarEnPendrive(object sender, RoutedEventArgs e)
        {
        }

        private async Task AvisoOperacionListadoDialogAsync(string xTitulo, string xDescripcion)
        {
            ContentDialog AvisoOperacionEmpresaDialog = new ContentDialog
            {
                Title = xTitulo,
                Content = xDescripcion,
                CloseButtonText = "Continuar"
            };
            _ = await AvisoOperacionEmpresaDialog.ShowAsync();
        }
    }
}
