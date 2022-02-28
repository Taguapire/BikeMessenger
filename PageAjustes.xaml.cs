using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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

            // En Estudio
            checkBoxActivarSQLite.IsChecked = true;
            LvrTransferVar.ESTADOPARAMETROS = "S";
            LvrTransferVar.BASEDEDATOSLOCAL = "S";
            BtnConfiguraciónUsuario.IsEnabled = (LvrTransferVar.MOBILES_XMPP == "S");
            checkBoxActivarXMPP.IsChecked = (LvrTransferVar.MOBILES_XMPP == "S");

            textBoxDirectorioActual.Text = LvrTransferVar.DIRECTORIO_BASE_LOCAL;
            try
            {
                textBoxDirectorioDeRespaldos.Text = LvrTransferVar.DIRECTORIO_RESPALDOS;
            }
            catch (System.ArgumentNullException)
            {
                textBoxDirectorioDeRespaldos.Text = "";
            }
            _ = ActualizarGrillaDerespaldos();
        }

        private void BtnSalirAjustes(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void BtnGrabarCambios(object sender, RoutedEventArgs e)
        {
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
                await ActualizarGrillaDerespaldos();
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
            catch (FileNotFoundException)
            {
                await AvisoCopiaBaseDialogAsync("Respaldar Base de Datos", "No se puede localizar la Base de Datos.");
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
            StorageFile BaseDatosOrigen = await FolderOrigen.GetFileAsync("BikeMessenger.db");

            StorageFolder FolderDestino = await StorageFolder.GetFolderFromPathAsync(DirectorioDestino);
            //StorageFile BaseDatosDestino = await FolderDestino.GetFileAsync("BikeMessenger.db");

            _ = await BaseDatosOrigen.CopyAsync(FolderDestino, "BikeMessenger.db", NameCollisionOption.GenerateUniqueName);

            //LvrTransferVar.CrearDirectorioRespaldo(DirectorioDestino);
            //LvrTransferVar.LeerDirectorio();

            // LvrProgresRing.IsActive = false;
            // await Task.Delay(500); // 1 sec delay

            await AvisoCopiaBaseDialogAsync("Cambiar Directorio", "La Copia de la Base se Datos se ha completado. A continuación saldra de la aplicación al presionar continuar.");

            Application.Current.Exit();
        }

        private async Task ActualizarGrillaDerespaldos()
        {
            StorageFolder folder;
            List<string> fileTypeFilter = new List<string>();
            QueryOptions queryOptions;
            StorageFileQueryResult queryResult;
            IReadOnlyList<StorageFile> ListadoDeBasesDedatos;
            List<GridListadoArchivosDB> GrillaDeArchivosDB = new List<GridListadoArchivosDB>();

            try
            {
                folder = await StorageFolder.GetFolderFromPathAsync(LvrTransferVar.DIRECTORIO_RESPALDOS);
                fileTypeFilter.Add(".db");
                fileTypeFilter.Add(".DB");
                fileTypeFilter.Add(".db3");
                fileTypeFilter.Add(".DB3");
                queryOptions = new QueryOptions(CommonFileQuery.OrderByName, fileTypeFilter);
                queryResult = folder.CreateFileQueryWithOptions(queryOptions);
                ListadoDeBasesDedatos = await queryResult.GetFilesAsync();

                // Access properties for each file
                foreach (StorageFile Archivo in ListadoDeBasesDedatos)
                {
                    GrillaDeArchivosDB.Add(new GridListadoArchivosDB
                    {
                        CREACION = Archivo.DateCreated.DateTime.ToString(),
                        NOMBRE = Archivo.Name,
                        RESPALDO = Archivo.Path
                    });
                }
                dataGridRespaldos.ItemsSource = GrillaDeArchivosDB;
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
            }
        }

        internal class GridListadoArchivosDB
        {
            public string CREACION { get; set; }
            public string NOMBRE { get; set; }
            public string RESPALDO { get; set; }

            public GridListadoArchivosDB()
            {
                CREACION = "";
                NOMBRE = "";
                RESPALDO = "";
            }
        }

        private async void BtnConfiguraciónUsuarioClick(object sender, RoutedEventArgs e)
        {
            PentalphaJson localPentalphaJson = new PentalphaJson();

            localPentalphaJson.PENTALPHA = LvrTransferVar.PENTALPHA_ID;
            localPentalphaJson.EMPRESA = LvrTransferVar.EMP_NOMBRE;
            localPentalphaJson.RUTID = LvrTransferVar.EMP_RUTID;
            localPentalphaJson.DIGVER = LvrTransferVar.EMCLI_DIGVER;
            localPentalphaJson.USUARIO = LvrTransferVar.USUARIO;
            localPentalphaJson.CLAVE = LvrTransferVar.CLAVE;
            localPentalphaJson.REMOTO = LvrTransferVar.REMOTO;
            localPentalphaJson.PROPIO = LvrTransferVar.PROPIO;
            localPentalphaJson.LICENCIA = LvrTransferVar.LICENCIA;

            RegistroXMPP dialogXMPP = new RegistroXMPP();
            dialogXMPP.localPentalphaJson = localPentalphaJson;

            dialogXMPP.MemoriaPantalla();

            var Resultado = await dialogXMPP.ShowAsync();

            if (Resultado == ContentDialogResult.Primary)
            {
                LvrTransferVar.PENTALPHA_ID = localPentalphaJson.PENTALPHA;
                LvrTransferVar.EMP_NOMBRE = localPentalphaJson.EMPRESA;
                LvrTransferVar.EMP_RUTID = localPentalphaJson.RUTID;
                LvrTransferVar.EMCLI_DIGVER = localPentalphaJson.DIGVER;
                LvrTransferVar.USUARIO = localPentalphaJson.USUARIO;
                LvrTransferVar.CLAVE = localPentalphaJson.CLAVE;
                LvrTransferVar.REMOTO = localPentalphaJson.REMOTO;
                LvrTransferVar.PROPIO = localPentalphaJson.PROPIO;
                LvrTransferVar.LICENCIA = localPentalphaJson.LICENCIA;
            }
        }    

        private void CheckBoxActivarSQLiteClick(object sender, RoutedEventArgs e)
        {
            checkBoxActivarSQLite.IsChecked = true;
        }

        private void CheckBoxActivarXMPPClick(object sender, RoutedEventArgs e)
        {
            CheckBox VerificarActivación = (CheckBox)sender;
            
            if ((bool)VerificarActivación.IsChecked)
            {
                LvrTransferVar.MOBILES_XMPP = "S";
                BtnConfiguraciónUsuario.IsEnabled = true;
            }
            else
            {
                LvrTransferVar.MOBILES_XMPP = "N";
                BtnConfiguraciónUsuario.IsEnabled = false;
            }
            LvrTransferVar.EscribirValoresDeAjustes();
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            // Save theme choice to LocalSettings. 
            // ApplicationTheme enum values: 0 = Light, 1 = Dark
            ApplicationData.Current.LocalSettings.Values["themeSetting"] =
                                                             ((ToggleSwitch)sender).IsOn ? 0 : 1;
        }

        private void ToggleSwitch_Loaded(object sender, RoutedEventArgs e)
        {
            ((ToggleSwitch)sender).IsOn = App.Current.RequestedTheme == ApplicationTheme.Light;
        }
    }
}

