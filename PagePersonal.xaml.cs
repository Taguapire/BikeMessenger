using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Animation;
using System.Collections.Generic;
using Microsoft.Toolkit.Uwp.UI.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PagePersonal : Page
    {
        Bm_Personal_Database BM_Database_Personal = new Bm_Personal_Database();
        TransferVar LvrTransferVar;
        bool BorrarSiNo;

        public PagePersonal()
        {
            this.InitializeComponent();
            comboBoxAutorizacion.Items.Add("ADMINISTRADOR");
            comboBoxAutorizacion.Items.Add("OPERADOR");
            comboBoxAutorizacion.Items.Add("COLABORADOR");
            comboBoxAutorizacion.Items.Add("INTERMEDIARIO");
            comboBoxAutorizacion.Items.Add("CLIENTE");
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs navigationEvent)
        {
            // call the original OnNavigatingFrom
            base.OnNavigatingFrom(navigationEvent);

            // when the dialog is removed from navigation stack 
            if (navigationEvent.NavigationMode == NavigationMode.Back)
            {
                // set the cache mode
                this.NavigationCacheMode = NavigationCacheMode.Disabled;

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

            if (navigationEvent.Parameter is string && !string.IsNullOrWhiteSpace((string)navigationEvent.Parameter))
            {
                //greeting.Text = $"Hi, {e.Parameter.ToString()}";
            }
            else
            {
                LvrTransferVar = (TransferVar) navigationEvent.Parameter;
                BM_Database_Personal.BM_CreateDatabase(LvrTransferVar.TV_Connection);

                if (LvrTransferVar.P_RUTID == "") {
                    if (BM_Database_Personal.Bm_Personal_Buscar())
                    {
                        LlenarPantallaConDb();
                        LlenarListaPersonal();
                     }
                }
                else
                {
                    if (BM_Database_Personal.Bm_Personal_Buscar(LvrTransferVar.P_RUTID,LvrTransferVar.P_DIGVER))
                    {
                        LlenarPantallaConDb();
                        LlenarListaPersonal();
                    }
                }
            }
        }

        private void BtnSeleccionarAjustes(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageAjustes), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarServicios(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageServicios), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarClientes(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageClientes), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarRecursos(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageRecursos), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarEmpresa(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageEmpresa), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarPersonal(object sender, RoutedEventArgs e)
        {
            // this.Frame.Navigate(typeof(PagePersonal), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private async void BtnPersonalCargarFoto(object sender, RoutedEventArgs e)
        {
            // Set up the file picker.
            Windows.Storage.Pickers.FileOpenPicker openPicker = new Windows.Storage.Pickers.FileOpenPicker
            {
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary,
                ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail
            };

            // Filter to include a sample subset of file types.
            openPicker.FileTypeFilter.Clear();
            openPicker.FileTypeFilter.Add(".bmp");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".jpg");

            // Open the file picker.
            Windows.Storage.StorageFile file = await openPicker.PickSingleFileAsync();

            // 'file' is null if user cancels the file picker.
            if (file != null)
            {
                // Open a stream for the selected file.
                // The 'using' block ensures the stream is disposed
                // after the image is loaded.
                using (Windows.Storage.Streams.IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap.
                    Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage();

                    bitmapImage.SetSource(fileStream);
                    imageFotoPersonal.Source = bitmapImage;
                }
            }
        }

        private void LlenarPantallaConDb()
        {
            try
            {
                LvrTransferVar.P_RUTID = BM_Database_Personal.BK_RUTID;
                LvrTransferVar.P_DIGVER = BM_Database_Personal.BK_DIGVER;
                textBoxRut.Text = BM_Database_Personal.BK_RUTID;
                textBoxDigitoVerificador.Text = BM_Database_Personal.BK_DIGVER;
                textBoxNombres.Text = BM_Database_Personal.BK_NOMBRES;
                textBoxApellidos.Text = BM_Database_Personal.BK_APELLIDOS;
                textBoxTelefono1.Text = BM_Database_Personal.BK_TELEFONO1;
                textBoxTelefono2.Text = BM_Database_Personal.BK_TELEFONO2;
                textBoxCorreoElectronico.Text = BM_Database_Personal.BK_EMAIL;
                comboBoxAutorizacion.SelectedValue = BM_Database_Personal.BK_AUTORIZACION;
                textBoxCargo.Text = BM_Database_Personal.BK_CARGO;
                textBoxDomicilio.Text = BM_Database_Personal.BK_DOMICILIO;
                textBoxNumero.Text = BM_Database_Personal.BK_NUMERO;
                textBoxPiso.Text = BM_Database_Personal.BK_PISO;
                textBoxDepartamento.Text = BM_Database_Personal.BK_DPTO;
                textBoxCodigoPostal.Text = BM_Database_Personal.BK_CODIGOPOSTAL;

                if (BM_Database_Personal.Bm_E_Pais_EjecutarSelect())
                {
                    while (BM_Database_Personal.Bm_E_Pais_Buscar())
                    {
                        comboBoxPais.Items.Add(BM_Database_Personal.BK_E_PAIS);
                    }
                }
                comboBoxRegion.Items.Add(BM_Database_Personal.BK_PAIS);
                comboBoxPais.SelectedValue = BM_Database_Personal.BK_PAIS;

                comboBoxRegion.Items.Add(BM_Database_Personal.BK_REGION);
                comboBoxRegion.SelectedValue = BM_Database_Personal.BK_REGION;

                comboBoxComuna.Items.Add(BM_Database_Personal.BK_COMUNA);
                comboBoxComuna.SelectedValue = BM_Database_Personal.BK_COMUNA;

                comboBoxCiudad.Items.Add(BM_Database_Personal.BK_CIUDAD);
                comboBoxCiudad.SelectedValue = BM_Database_Personal.BK_CIUDAD;

                textBoxObservaciones.Text = BM_Database_Personal.BK_OBSERVACIONES;

                imageFotoPersonal.Source = Base64StringToBitmap(BM_Database_Personal.BK_FOTO);
            }
            catch (System.ArgumentNullException)
            {
                ErrorDeRecuperacionDialog();
            }
        }

        private void LimpiarPantalla()
        {
            LvrTransferVar.P_RUTID = "";
            LvrTransferVar.P_DIGVER = "";
            textBoxRut.Text = "";
            textBoxDigitoVerificador.Text = "";
            textBoxNombres.Text = "";
            textBoxApellidos.Text = "";
            textBoxTelefono1.Text = "";
            textBoxTelefono2.Text = "";
            textBoxCorreoElectronico.Text = "";
            comboBoxAutorizacion.Text = "";
            textBoxCargo.Text = "";
            textBoxDomicilio.Text = "";
            textBoxNumero.Text = "";
            textBoxPiso.Text = "";
            textBoxDepartamento.Text = "";
            textBoxCodigoPostal.Text = "";
            comboBoxPais.Text = "";
            comboBoxRegion.Text = "";
            comboBoxComuna.Text = "";
            comboBoxCiudad.Text = "";
            textBoxObservaciones.Text = "";
            imageFotoPersonal.Source = Base64StringToBitmap("");
        }

        private async System.Threading.Tasks.Task LlenarDbConPantallaAsync()
        {
            BM_Database_Personal.BK_FOTO = await ConvertirImageABase64Async();
            BM_Database_Personal.BK_RUTID = textBoxRut.Text;
            BM_Database_Personal.BK_DIGVER = textBoxDigitoVerificador.Text;
            BM_Database_Personal.BK_APELLIDOS = textBoxApellidos.Text;
            BM_Database_Personal.BK_NOMBRES = textBoxNombres.Text;
            BM_Database_Personal.BK_TELEFONO1 = textBoxTelefono1.Text;
            BM_Database_Personal.BK_TELEFONO2 = textBoxTelefono2.Text;
            BM_Database_Personal.BK_EMAIL = textBoxCorreoElectronico.Text;
            BM_Database_Personal.BK_AUTORIZACION = comboBoxAutorizacion.Text;
            BM_Database_Personal.BK_CARGO = textBoxCargo.Text;
            BM_Database_Personal.BK_DOMICILIO = textBoxDomicilio.Text;
            BM_Database_Personal.BK_NUMERO = textBoxNumero.Text;
            BM_Database_Personal.BK_PISO = textBoxPiso.Text;
            BM_Database_Personal.BK_DPTO = textBoxDepartamento.Text;
            BM_Database_Personal.BK_CODIGOPOSTAL = textBoxCodigoPostal.Text;
            BM_Database_Personal.BK_CIUDAD = comboBoxCiudad.Text;
            BM_Database_Personal.BK_COMUNA = comboBoxComuna.Text;
            BM_Database_Personal.BK_REGION = comboBoxRegion.Text;
            BM_Database_Personal.BK_PAIS = comboBoxPais.Text;
            BM_Database_Personal.BK_OBSERVACIONES = comboBoxComuna.Text;
        }

        private async void BtnAgregarPersonal(object sender, RoutedEventArgs e)
        {
            await LlenarDbConPantallaAsync();
            if (BM_Database_Personal.Bm_Personal_Agregar())
            {
                LlenarListaPersonal();
                LvrTransferVar.P_RUTID = BM_Database_Personal.BK_RUTID;
                LvrTransferVar.P_DIGVER = BM_Database_Personal.BK_DIGVER;
                AvisoOperacionPersonalDialog("Agregar Personal", "Operación completada con exito.");
            }
            else
            {
                AvisoOperacionPersonalDialog("Agregando Personal", "Se a producido un error al intentar agregar personal.");
            }
        }

        private async void BtnModificarPersonal(object sender, RoutedEventArgs e)
        {
            await LlenarDbConPantallaAsync();
            if (BM_Database_Personal.Bm_Personal_Modificar(BM_Database_Personal.BK_RUTID, BM_Database_Personal.BK_DIGVER))
            {
                LlenarListaPersonal();
                LvrTransferVar.P_RUTID = BM_Database_Personal.BK_RUTID;
                LvrTransferVar.P_DIGVER = BM_Database_Personal.BK_DIGVER;
                AvisoOperacionPersonalDialog("Modificar Personal", "Operación completada con exito.");
            }
            else
            {
                AvisoOperacionPersonalDialog("Modificando Personal", "Se a producido un error al intentar modificar personal.");
            }
        }

        private void BtnBorrarPersonal(object sender, RoutedEventArgs e)
        {
            BorrarSiNo = false;

            AvisoBorrarPersonalDialog();

            if (BorrarSiNo)
                return;

            try
            {
                if (BM_Database_Personal.Bm_Personal_Borrar(BM_Database_Personal.BK_RUTID, BM_Database_Personal.BK_DIGVER))
                {

                    textBoxRut.IsReadOnly = false;
                    textBoxDigitoVerificador.IsReadOnly = false;

                    if (BM_Database_Personal.Bm_Personal_Buscar())
                    {
                        LlenarPantallaConDb();
                        LlenarListaPersonal();
                        LvrTransferVar.P_RUTID = BM_Database_Personal.BK_RUTID;
                        LvrTransferVar.P_DIGVER = BM_Database_Personal.BK_DIGVER;
                    }
                    else
                    {
                        LvrTransferVar.P_RUTID = "";
                        LvrTransferVar.P_DIGVER = "";
                    }

                    AvisoOperacionPersonalDialog("Borrando Personal", "Operación completada con exito.");

                    LlenarPantallaConDb();
                }
                else
                {
                    AvisoOperacionPersonalDialog("Borrando Personal", "Se a producido un error al intentar borrar personal.");
                }
            }
            catch (System.ArgumentException)
            {
                AvisoOperacionPersonalDialog("Acceso a Base de Datos", "Debe llenar los datos del personal.");
            }
            BorrarSiNo = false;
        }

        private void BtnSalirPersonal(object sender, RoutedEventArgs e)
        {
            LvrTransferVar.TV_Connection.Close();
            Application.Current.Exit();
        }

        private async System.Threading.Tasks.Task<string> ConvertirImageABase64Async()
        {
            var bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(imageFotoPersonal);

            var image = (await bitmap.GetPixelsAsync()).ToArray();
            var width = (uint)bitmap.PixelWidth;
            var height = (uint)bitmap.PixelHeight;

            double dpiX = 96;
            double dpiY = 96;

            var encoded = new Windows.Storage.Streams.InMemoryRandomAccessStream();
            var encoder = await Windows.Graphics.Imaging.BitmapEncoder.CreateAsync(Windows.Graphics.Imaging.BitmapEncoder.PngEncoderId, encoded);

            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, width, height, dpiX, dpiY, image);
            await encoder.FlushAsync();
            encoded.Seek(0);

            var bytes = new byte[encoded.Size];
            await encoded.AsStream().ReadAsync(bytes, 0, bytes.Length);

            var base64String = Convert.ToBase64String(bytes);

            return base64String;
        }

        private BitmapImage Base64StringToBitmap(string source)
        {
            var ims = new InMemoryRandomAccessStream();
            var bytes = Convert.FromBase64String(source);
            var dataWriter = new DataWriter(ims);
            dataWriter.WriteBytes(bytes);
            _ = dataWriter.StoreAsync();
            ims.Seek(0);
            var img = new BitmapImage();
            img.SetSource(ims);
            return img;
        }

        private async void ErrorDeRecuperacionDialog()
        {
            ContentDialog noErrorRecuperacionDialog = new ContentDialog
            {
                Title = "Error",
                Content = "El registro a recuperar tiene valores nulos.",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noErrorRecuperacionDialog.ShowAsync();
        }

        private async void AvisoOperacionPersonalDialog(string xTitulo, string xDescripcion)
        {
            try
            {
                ContentDialog AvisoOperacionEmpresaDialog = new ContentDialog
                {
                    Title = xTitulo,
                    Content = xDescripcion,
                    CloseButtonText = "Continuar"
                };
                ContentDialogResult result = await AvisoOperacionEmpresaDialog.ShowAsync();
            } catch (System.Exception)
            {
                ;
            }
        }

        private async void AvisoBorrarPersonalDialog()
        {
            ContentDialog AvisoConfirmacionPersonalDialog = new ContentDialog
            {
                Title = "Borrar Persona",
                Content = "Confirme borrado del registro actual!",
                PrimaryButtonText = "Borrar",
                CloseButtonText = "Cancelar"
            };

            ContentDialogResult result = await AvisoConfirmacionPersonalDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
                BorrarSiNo = true;
            else
                BorrarSiNo = false;
        }

        private void LlenarListaPersonal()
        {
            List<GridPersonalIndividual> GridPersonalLista = new List<GridPersonalIndividual>();
            if (BM_Database_Personal.Bm_Personal_BuscarGrid())
            {
                while (BM_Database_Personal.Bm_Personal_BuscarGridProxima())
                {
                    GridPersonalLista.Add(
                        new GridPersonalIndividual { 
                            RUTID = BM_Database_Personal.BK_GRID_RUT, 
                            APELLIDO = BM_Database_Personal.BK_GRID_APELLIDOS,
                            NOMBRE = BM_Database_Personal.BK_GRID_NOMBRES
                        });
                }
            }
            dataGridPersonal.IsReadOnly = true;
            dataGridPersonal.ItemsSource = GridPersonalLista;
        }

        private void dataGridPersonal_Seleccion(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid CeldaSeleccionada = sender as DataGrid;
                GridPersonalIndividual Fila = (GridPersonalIndividual) CeldaSeleccionada.SelectedItems[0];
                string[] CadenaDividida = Fila.RUTID.Split("-", 2, StringSplitOptions.None);

                if (BM_Database_Personal.Bm_Personal_Buscar(CadenaDividida[0], CadenaDividida[1]))
                {
                    LimpiarPantalla();
                    LlenarPantallaConDb();
                    // LlenarListaPersonal();
                }
                else
                {
                    // textBoxNombreEmpresa.Text = "Sin Empresa";
                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                ; // No hacer nada, es un control vacio de error
            }
        }
    }

    public class GridPersonalIndividual
    {
        public string RUTID { get; set; }
        public string APELLIDO { get; set; }
        public string NOMBRE { get; set; }
    }
}
