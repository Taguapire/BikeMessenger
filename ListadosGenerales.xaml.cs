using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage.Provider;
using Windows.Storage;
using Windows.Storage.Pickers;
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
        private WebView VisorWeb;

        public ListadosGenerales()
        {
            InitializeComponent();
            PanelDePresentacion.Height = PaginaDeImpresion.ActualHeight;
            PanelDePresentacion.Width =PaginaDeImpresion.ActualWidth;
            VisorWeb = new WebView(WebViewExecutionMode.SameThread)
            {
                CanBeScrollAnchor = true
            };
            PanelDePresentacion.Children.Add(VisorWeb);
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
                    try
                    {
                        VisorWeb.NavigateToString(HtmlImprimir);
                    }
                    catch(ArgumentNullException)
                    {
                        _ = AvisoOperacionListadoDialogAsync("Listado de Personal", "El reporte de personal no contiene datos para listar.");
                    }
                    break;
                case "RECURSO":
                    Bm_Recurso_Database BM_Database_Recurso = new Bm_Recurso_Database();
                    HtmlImprimir = BM_Database_Recurso.Bm_Recurso_Listado(LvrTransferVar.EMP_PENTALPHA);
                    try
                    {
                        VisorWeb.NavigateToString(HtmlImprimir);
                    }
                    catch (ArgumentNullException)
                    {
                        _ = AvisoOperacionListadoDialogAsync("Listado de Recursos", "El reporte de recursos no contiene datos para listar.");
                    }
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
                        _ = AvisoOperacionListadoDialogAsync("Listado de Clientes", "El reporte de clientes no contiene datos para listar.");
                    }
                    break;
                case "SERVICIO":
                    Bm_Servicio_Database BM_Database_Servicio = new Bm_Servicio_Database();
                    HtmlImprimir = BM_Database_Servicio.Bm_Servicio_Listado();
                    try
                    {
                        VisorWeb.NavigateToString(HtmlImprimir);
                    }
                    catch (ArgumentNullException)
                    {
                        _ = AvisoOperacionListadoDialogAsync("Listado de Servicios", "El reporte de servicios no contiene datos para listar.");
                    }
                    break;
                default:
                    break;
            }
        }

        private async void LvrAlmacenarReporte(object sender, RoutedEventArgs e)
        {
            FileSavePicker savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".html" });
            savePicker.SuggestedFileName = LvrTransferVar.PantallaAnterior;
            StorageFile file = await savePicker.PickSaveFileAsync();
            
            if (file != null)
            {
                // Prevent updates to the remote version of the file until
                // we finish making changes and call CompleteUpdatesAsync.
                CachedFileManager.DeferUpdates(file);
                // write to file
                await FileIO.WriteTextAsync(file, HtmlImprimir);
                // Let Windows know that we're finished changing the file so
                // the other app can update the remote version of the file.
                // Completing updates may require Windows to ask for user input.
                FileUpdateStatus status =
                    await CachedFileManager.CompleteUpdatesAsync(file);
                if (status == FileUpdateStatus.Complete)
                {
                    _ = AvisoOperacionListadoDialogAsync("Almacenando Reporte", "Archivo " + file.Name + " fué almacenado.");
                }
                else
                {
                    _ = AvisoOperacionListadoDialogAsync("Almacenando Reporte", "Archivo " + file.Name + " no fué almacenado.");
      
                }
            }
            else
            {
                _ = AvisoOperacionListadoDialogAsync("Almacenando Reporte", "La operación fue cancelada.");
            }
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
