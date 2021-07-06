using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

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
        private string LvrSQLPuerto = "";

        public PageAjustes()
        {
            InitializeComponent();
            // OJO            NavigationCacheMode = NavigationCacheMode.Disabled;
            if (LvrTransferVar.SincronizarBaseSQLServer())
            {
                checkBoxActivarSQLServer.IsChecked = LvrTransferVar.SincronizarBaseSQLServer();
                LvrTransferVar.ESTADOPARAMETROS = "S";
                LvrTransferVar.BDSQLSERVER = "S";
                textBoxSQLServidorInstancia.Text = LvrTransferVar.BDSQLSERVERINSTANCIA;
                textBoxSQLPuerto.Text = LvrTransferVar.BDSQLSERVERPUERTO;
                textBoxSQLUsuario.Text = LvrTransferVar.BDSQLSERVERUSUARIO;
                passwordBoxSQLClave.Password = LvrTransferVar.BDSQLSERVERCLAVE;
                textBoxSQLBaseDeDatos.Text = LvrTransferVar.BDSQLSERVERCATALOGO;
            }

            if (LvrTransferVar.SincronizarBaseLocal()) {
                checkBoxActivarSQLite.IsChecked = LvrTransferVar.SincronizarBaseLocal();
                LvrTransferVar.ESTADOPARAMETROS = "S";
                LvrTransferVar.BASEDEDATOSLOCAL = "S";
                textBoxDirectorioActual.Text = LvrTransferVar.DIRECTORIO_BASE_LOCAL;
                textBoxDirectorioDeRespaldos.Text = LvrTransferVar.DIRECTORIO_RESPALDOS;
                // LvrTransferVar.DIRECTORIO_USB_MEMORIA = textBoxDirectorioDeRespaldos.Text;
            }

            if (LvrTransferVar.SincronizarWebPentalpha())
            {
                checkBoxSincronizacionPentalpha.IsChecked = LvrTransferVar.SincronizarWebPentalpha();
                LvrTransferVar.ESTADOPARAMETROS = "S";
                LvrTransferVar.SINCRONIZACIONWEBPENTALPHA = "S";
            }
            
            if (LvrTransferVar.SincronizarWebPropio())
            {
                checkBoxSincronizacionPropio.IsChecked = LvrTransferVar.SincronizarWebPropio();
                LvrTransferVar.ESTADOPARAMETROS = "S";
                LvrTransferVar.SINCRONIZACIONWEBPROPIO = "S";
                textBoxServidor.Text = LvrTransferVar.BDREMOTACLIENTE;
                textBoxServidorPuerto.Text = LvrTransferVar.BDREMOTACLIENTEPUERTO;
            }
        }

        /* Ojo
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
                checkBoxSincronizacion.IsChecked = LvrTransferVar.SincronizarWebPentalpha() ? true : false;
                textBoxDirectorioActual.Text = LvrTransferVar.DIRECTORIO_BASE_LOCAL;
                textBoxDirectorioDeRespaldos.Text = LvrTransferVar.DIRECTORIO_USB_MEMORIA;
            }
        }
        OJO */

        private void BtnSalirAjustes(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void BtnGrabarCambios(object sender, RoutedEventArgs e)
        {
            LvrTransferVar.ESTADOPARAMETROS = "NADA";

            if ((bool)checkBoxActivarSQLServer.IsChecked)
            {
                LvrTransferVar.ESTADOPARAMETROS = "S";
                LvrTransferVar.BDSQLSERVER = "S";
                LvrTransferVar.BDSQLSERVERINSTANCIA = textBoxSQLServidorInstancia.Text;
                LvrTransferVar.BDSQLSERVERPUERTO = LvrSQLPuerto;
                LvrTransferVar.BDSQLSERVERUSUARIO = textBoxSQLUsuario.Text;
                LvrTransferVar.BDSQLSERVERCLAVE = passwordBoxSQLClave.Password;
                LvrTransferVar.BDSQLSERVERCATALOGO = textBoxSQLBaseDeDatos.Text;
            }
            else {
                LvrTransferVar.BDSQLSERVER = "N";
            }

            if ((bool)checkBoxActivarSQLite.IsChecked)
            {
                LvrTransferVar.ESTADOPARAMETROS = "S";
                LvrTransferVar.BASEDEDATOSLOCAL = "S";
                LvrTransferVar.DIRECTORIO_BASE_LOCAL = textBoxDirectorioActual.Text;
                LvrTransferVar.DIRECTORIO_RESPALDOS = textBoxDirectorioDeRespaldos.Text;
                LvrTransferVar.DIRECTORIO_USB_MEMORIA = textBoxDirectorioDeRespaldos.Text;
            }
            else
            {
                LvrTransferVar.BASEDEDATOSLOCAL = "N";
            }

            if ((bool)checkBoxSincronizacionPentalpha.IsChecked)
            {
                LvrTransferVar.ESTADOPARAMETROS = "S";
                LvrTransferVar.SINCRONIZACIONWEBPENTALPHA = "S";
                LvrTransferVar.BDREMOTAPENTALPHA = "https://finanven.ddns.net";
            }
            else
            {
                LvrTransferVar.SINCRONIZACIONWEBPENTALPHA = "N";
            }

            if ((bool)checkBoxSincronizacionPropio.IsChecked)
            {
                LvrTransferVar.ESTADOPARAMETROS = "S";
                LvrTransferVar.SINCRONIZACIONWEBPROPIO = "S";
                LvrTransferVar.BDREMOTACLIENTE = textBoxServidor.Text;
                LvrTransferVar.BDREMOTACLIENTEPUERTO = textBoxServidorPuerto.Text;
            }
            else
            {
                LvrTransferVar.SINCRONIZACIONWEBPROPIO = "N";
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

        private void CambiarModoSincronizacionPentalpha(object sender, RoutedEventArgs e)
        {
            if ((bool)checkBoxSincronizacionPentalpha.IsChecked)
            {
                LvrTransferVar.SINCRONIZACIONWEBPENTALPHA = "S";
            }
            else
            {
                LvrTransferVar.SINCRONIZACIONWEBPENTALPHA = "N";
            }
            LvrTransferVar.EscribirValoresDeAjustes();
        }

        private void CambiarModoSincronizacionPropio(object sender, RoutedEventArgs e)
        {
            if ((bool)checkBoxSincronizacionPropio.IsChecked)
            {
                LvrTransferVar.SINCRONIZACIONWEBPROPIO = "S";
            }
            else
            {
                LvrTransferVar.SINCRONIZACIONWEBPROPIO = "N";
            }
            LvrTransferVar.EscribirValoresDeAjustes();
        }

        private void CambiarModoSQLServer(object sender, RoutedEventArgs e)
        {
            if ((bool)checkBoxActivarSQLServer.IsChecked)
            {
                LvrTransferVar.BDSQLSERVER = "S";
            }
            else
            {
                LvrTransferVar.BDSQLSERVER = "N";
            }
            LvrTransferVar.EscribirValoresDeAjustes();
        }

        private void ButtonTestSQLServer_Conexion(object sender, RoutedEventArgs e)
        {
            bool LvrSQLOK;
            SqlConnection BM_Conexion;
            LvrSQLPuerto = textBoxSQLPuerto.Text.Trim();
            LvrSQLPuerto = LvrSQLPuerto != "" && LvrSQLPuerto != null ? "," + LvrSQLPuerto : "";

            BM_Conexion = new SqlConnection("Data Source=" + textBoxSQLServidorInstancia.Text + LvrSQLPuerto + ";Initial Catalog=" + textBoxSQLBaseDeDatos.Text + "; MultipleActiveResultSets=true;User ID=" + textBoxSQLUsuario.Text + "; Password=" + passwordBoxSQLClave.Password + ";");
            
            try
            {
                BM_Conexion.Open();
                LvrSQLOK = true;
            }
            catch (Exception)
            {
                LvrSQLOK = false;
            }

            if (LvrSQLOK)
            {
                BM_Conexion.Close();
                _ = AvisoCopiaBaseDialogAsync("Conexion a SQLServer", "La conexión a la Base de Datos SQLServer esta correcta, ahora debe proceser a copiar la estructura BikeMessenger a su base de datos, para ese soporte escriba a contacto@pentalpha.net.");
            }
            else
            {
                _ = AvisoCopiaBaseDialogAsync("Conexion a SQLServer", "La conexión a la Base de Datos SQLServer esta incorrecta, debe proceser a verificar datos de conexión o solicite soporte a contacto@pentalpha.net.");
            }
        }
    }
}
