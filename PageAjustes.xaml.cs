using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Windows.Services.Maps;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAjustes : Page
    {
        private TransferVar LvrTransferVar;
        private bool CambiarDirectorioSiNo;

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
                checkBoxSincronizacion.IsChecked = LvrTransferVar.SincronizarWeb() ? true : false;
                textBoxDirectorioActual.Text = LvrTransferVar.Directorio;
                textBoxDirectorioNuevo.Text = LvrTransferVar.Directorio;
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
            LvrTransferVar.TV_Connection.Close();
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
                //folderPicker.FileTypeFilter.Add("*.db3");

                StorageFolder folder = await folderPicker.PickSingleFolderAsync();

                if (folder == null)
                {
                    btnBuscarNuevoDirectorio.IsEnabled = true;
                    return;
                }

                textBoxDirectorioNuevo.Text = folder.Path;
            }
            catch (NullReferenceException)
            {
                ;// textBoxDirectorioNuevo.Text
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                ;
            }

            //StorageFile file = await folder.CreateFileAsync("sample.csv", CreationCollisionOption.ReplaceExisting);
            //var stream = await file.OpenStreamForWriteAsync();
            //var buf = Encoding.UTF8.GetBytes("Hello, World");
            //await stream.WriteAsync(buf, 0, buf.Length);
            //stream.Dispose();
            btnBuscarNuevoDirectorio.IsEnabled = true;
            return;
        }

        private async void BtnEventoCambiarANuevoDirectorio(object sender, RoutedEventArgs e)
        {

            CambiarDirectorioSiNo = false;

            await AvisoCambiarDirectorioDialogAsync();

            if (!CambiarDirectorioSiNo)
            {
                return;
            }

            try
            {
                await CopiarBaseDeDatosAsync(textBoxDirectorioActual.Text, textBoxDirectorioNuevo.Text);
            }
            catch (ArgumentException)
            {
                await AvisoDirectorioDialogAsync("Cambiar Directorio", "No a sido posible cambiar la base de datos a nuevo directorio.");
            }
            catch (InvalidCastException)
            {
                await AvisoDirectorioDialogAsync("Cambiar Directorio", "No a sido posible cambiar la base de datos a nuevo directorio.");
            }
            catch (UnauthorizedAccessException)
            {
                await AvisoDirectorioDialogAsync("Cambiar Directorio", "Acceso no autorizado a este directorio.");
            }
            catch (Exception)
            {
                await AvisoDirectorioDialogAsync("Cambiar Directorio", "No puede crearse la Base en Destino.");
            }
            CambiarDirectorioSiNo = false;
        }

        private async Task AvisoCambiarDirectorioDialogAsync()
        {
            ContentDialog AvisoCambiarDirectorioDialog = new ContentDialog
            {
                Title = "Cambiar Directorio",
                Content = "Confirme el cambio de la Base de Datos al nuevo directorio!",
                PrimaryButtonText = "Confirmar",
                CloseButtonText = "Cancelar"
            };

            ContentDialogResult result = await AvisoCambiarDirectorioDialog.ShowAsync();

            CambiarDirectorioSiNo = result == ContentDialogResult.Primary;
        }

        private async Task AvisoDirectorioDialogAsync(string pTitle, string pContent)
        {
            ContentDialog AvisoCambiarDirectorioDialog = new ContentDialog
            {
                Title = pTitle,
                Content = pContent,
                CloseButtonText = "Continuar"
            };
            _ = await AvisoCambiarDirectorioDialog.ShowAsync();
        }

        private async Task CopiarBaseDeDatosAsync(string DirectorioOrigen, string DirectorioDestino)
        {
            StorageFolder FolderOrigen = await StorageFolder.GetFolderFromPathAsync(DirectorioOrigen);
            StorageFile BaseDatosOrigen = await FolderOrigen.GetFileAsync("BikeMessenger.db");

            StorageFolder FolderDestino = await StorageFolder.GetFolderFromPathAsync(DirectorioDestino);
            //StorageFile BaseDatosDestino = await FolderDestino.GetFileAsync("BikeMessenger.db");

            _ = await BaseDatosOrigen.CopyAsync(FolderDestino, "BikeMessenger.db", NameCollisionOption.GenerateUniqueName);
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
