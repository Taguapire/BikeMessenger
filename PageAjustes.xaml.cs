using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAjustes : Page
    {
        private TransferVar LvrTransferVar = new TransferVar();
        private bool CopiarBaseDeDatosSiNo;

        public PageAjustes()
        {
            InitializeComponent();

            if (LvrTransferVar.SincronizarBaseLocal())
            {
                checkBoxActivarSQLite.IsChecked = LvrTransferVar.SincronizarBaseLocal();
                LvrTransferVar.ESTADOPARAMETROS = "S";
                LvrTransferVar.BASEDEDATOSLOCAL = "S";
                textBoxDirectorioActual.Text = LvrTransferVar.DIRECTORIO_BASE_LOCAL;
                textBoxDirectorioDeRespaldos.Text = LvrTransferVar.DIRECTORIO_RESPALDOS;
            }
        }

        private void BtnSalirAjustes(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void BtnGrabarCambios(object sender, RoutedEventArgs e)
        {
            LvrTransferVar.ESTADOPARAMETROS = "NADA";

            if ((bool)checkBoxActivarSQLite.IsChecked)
            {
                LvrTransferVar.ESTADOPARAMETROS = "S";
                LvrTransferVar.BASEDEDATOSLOCAL = "S";
                LvrTransferVar.DIRECTORIO_BASE_LOCAL = textBoxDirectorioActual.Text;
                LvrTransferVar.DIRECTORIO_RESPALDOS = textBoxDirectorioDeRespaldos.Text;
            }

            LvrTransferVar.EscribirValoresDeAjustes();
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
            //            LvrProgresRing.IsActive = true;
            //            await Task.Delay(500); // 1 sec delay

            StorageFolder FolderOrigen = await StorageFolder.GetFolderFromPathAsync(DirectorioOrigen);
            StorageFile BaseDatosOrigen = await FolderOrigen.GetFileAsync("BikeMessenger.db3");

            StorageFolder FolderDestino = await StorageFolder.GetFolderFromPathAsync(DirectorioDestino);
            //StorageFile BaseDatosDestino = await FolderDestino.GetFileAsync("BikeMessenger.db3");

            _ = await BaseDatosOrigen.CopyAsync(FolderDestino, "BikeMessenger.db3", NameCollisionOption.GenerateUniqueName);

            //LvrTransferVar.CrearDirectorioRespaldo(DirectorioDestino);
            //LvrTransferVar.LeerDirectorio();

            //            LvrProgresRing.IsActive = false;
            //            await Task.Delay(500); // 1 sec delay

            await AvisoCopiaBaseDialogAsync("Cambiar Directorio", "La Copia de la Base se Datos se ha completado. A continuación saldra de la aplicación al presionar continuar.");

            Application.Current.Exit();
        }
    }
}

