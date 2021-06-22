using System;
using System.Threading.Tasks;
using Windows.Storage;
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
    public sealed partial class PageAjustes : Page
    {
        private TransferVar LvrTransferVar;
        private bool CopiarBaseDeDatosSiNo;

        public PageAjustes()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;
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
                checkBoxSincronizacion.IsChecked = LvrTransferVar.SincronizarWebRemoto() ? true : false;
                textBoxDirectorioActual.Text = LvrTransferVar.Directorio;
                textBoxDirectorioDeRespaldos.Text = LvrTransferVar.DirectorioRespaldos;
            }
        }

        private void BtnSeleccionarAjustes(object sender, RoutedEventArgs e)
        {
            // _ = Frame.Navigate(typeof(PageAjustes), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarServicios(object sender, RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(PageServicios), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarClientes(object sender, RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(PageClientes), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarRecursos(object sender, RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(PageRecursos), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarEmpresa(object sender, RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(PageEmpresa), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarPersonal(object sender, RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(PagePersonal), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSalirAjustes(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private async void BtnEventoBuscarNuevoDirectorio(object sender, RoutedEventArgs e)
        {
            try
            {

                Control b = (Control)sender;
                b.IsEnabled = false;

                Windows.Storage.Pickers.FolderPicker folderPicker = new Windows.Storage.Pickers.FolderPicker
                {
                    SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder
                };
                folderPicker.FileTypeFilter.Add("*");

                StorageFolder folder = await folderPicker.PickSingleFolderAsync();

                if (folder == null)
                {
                    btnBuscarNuevoDirectorio.IsEnabled = true;
                    return;
                }

                textBoxDirectorioDeRespaldos.Text = folder.Path;
            }
            catch (NullReferenceException)
            {
                ;
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                ;
            }
            btnBuscarNuevoDirectorio.IsEnabled = true;
            return;
        }

        private async void BtnEventoCopiarBaseDeDatos(object sender, RoutedEventArgs e)
        {

            CopiarBaseDeDatosSiNo = false;

            await AvisoCopiarBaseDeDatosDialogAsync();

            if (!CopiarBaseDeDatosSiNo)
            {
                return;
            }

            try
            {
                await CopiarBaseDeDatosAsync(textBoxDirectorioActual.Text, textBoxDirectorioDeRespaldos.Text);
            }
            catch (ArgumentException)
            {
                await AvisoCopiaBaseDialogAsync("Respaldar Base de Datos", "No a sido posible copiar la base de datos a directorio destino.");
            }
            catch (InvalidCastException)
            {
                await AvisoCopiaBaseDialogAsync("Respaldar Base de Datos", "No a sido posible copiar la base de datos a directorio destino.");
            }
            catch (UnauthorizedAccessException)
            {
                await AvisoCopiaBaseDialogAsync("Respaldar Base de Datos", "Acceso no autorizado a este directorio.");
            }
            catch (Exception)
            {
                await AvisoCopiaBaseDialogAsync("Respaldar Base de Datos", "No puede copiarse la Base de Datos en Destino.");
            }
            CopiarBaseDeDatosSiNo = false;
        }

        private async Task AvisoCopiarBaseDeDatosDialogAsync()
        {
            ContentDialog AvisoCopiarBaseDialog = new ContentDialog
            {
                Title = "Copiar Base de Datos",
                Content = "Confirme el Respaldo de Base de Datos. La Base de Datos se cerrara para ejecutar esta operación, por lo que al final debera salir de la aplicación y la ejecutara nuevamente.",
                PrimaryButtonText = "Confirmar",
                CloseButtonText = "Cancelar"
            };

            ContentDialogResult result = await AvisoCopiarBaseDialog.ShowAsync();

            CopiarBaseDeDatosSiNo = result == ContentDialogResult.Primary;
        }

        private async Task AvisoCopiaBaseDialogAsync(string pTitle, string pContent)
        {
            ContentDialog AvisoCopiaDbDialog = new ContentDialog
            {
                Title = pTitle,
                Content = pContent,
                CloseButtonText = "Continuar"
            };
            _ = await AvisoCopiaDbDialog.ShowAsync();
        }

        private async Task CopiarBaseDeDatosAsync(string DirectorioOrigen, string DirectorioDestino)
        {
            // Muestra de espera
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // 1 sec delay

            StorageFolder FolderOrigen = await StorageFolder.GetFolderFromPathAsync(DirectorioOrigen);
            StorageFile BaseDatosOrigen = await FolderOrigen.GetFileAsync("BikeMessenger.db3");

            StorageFolder FolderDestino = await StorageFolder.GetFolderFromPathAsync(DirectorioDestino);
            //StorageFile BaseDatosDestino = await FolderDestino.GetFileAsync("BikeMessenger.db3");

            _ = await BaseDatosOrigen.CopyAsync(FolderDestino, "BikeMessenger.db3", NameCollisionOption.GenerateUniqueName);

            LvrTransferVar.CrearDirectorioRespaldo(DirectorioDestino);
            LvrTransferVar.LeerDirectorio();

            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay

            await AvisoCopiaBaseDialogAsync("Cambiar Directorio", "La Copia de la Base se Datos se ha completado. A continuación saldra de la aplicación al presionar continuar.");

            Application.Current.Exit();
        }

        private void CambiarModoSincronizacion(object sender, RoutedEventArgs e)
        {
            if ((bool)checkBoxSincronizacion.IsChecked)
            {
                LvrTransferVar.CrearSincronizarRemoto("S");
            }
            else
            {
                LvrTransferVar.CrearSincronizarRemoto("N");
            }
        }
    }
}
