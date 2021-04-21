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
using System.Data.SQLite;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageClientes : Page
    {
        Bm_Clientes_Database BM_Database_Clientes = new Bm_Clientes_Database();
        SQLiteConnection BM_Connection;

        public PageClientes()
        {
            // this.BM_Connection = BM_Connection;
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationCacheMode = NavigationCacheMode.Disabled;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            {
                //greeting.Text = $"Hi, {e.Parameter.ToString()}";
            }
            else
            {
                BM_Connection = (SQLiteConnection)e.Parameter;
                if (BM_Database_Clientes.BM_CreateDatabase(BM_Connection))
                {
                    if (BM_Database_Clientes.Bm_Clientes_Buscar())
                    {
                        LlenarPantallaConDb();
                        // BM_Existe_Clientes = true;
                    }
                    else
                    {
                        // textBoxNombreEmpresa.Text = "Sin Empresa";
                    }
                }
            }
            base.OnNavigatedTo(e);
        }

        

        private void BtnSeleccionarAjustes(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageAjustes), BM_Connection, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarServicios(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageServicios), BM_Connection, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarClientes(object sender, RoutedEventArgs e)
        {
            // this.Frame.Navigate(typeof(PageClientes), BM_Connection, new SuppressNavigationTransitionInfo());
        }


        private void BtnSeleccionarRecursos(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageRecursos), BM_Connection, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarEmpresa(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageEmpresa), BM_Connection, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarPersonal(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PagePersonal), BM_Connection, new SuppressNavigationTransitionInfo());
        }

        private async void BtnClientesCargarFoto(object sender, RoutedEventArgs e)
        {
            // Set up the file picker.
            Windows.Storage.Pickers.FileOpenPicker openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            openPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;

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
                    imageLogoCliente.Source = bitmapImage;
                }
            }
        }

        private void LlenarPantallaConDb()
        {
            try
            {
                textBoxRut.Text = BM_Database_Clientes.BK_RUTID;
                textBoxDigitoVerificador.Text = BM_Database_Clientes.BK_DIGVER;
                textBoxNombreCliente.Text = BM_Database_Clientes.BK_NOMBRE;
                textBoxActividad1.Text = BM_Database_Clientes.BK_ACTIVIDAD1;
                textBoxActividad2.Text = BM_Database_Clientes.BK_ACTIVIDAD2;
                textBoxRepresentantes1.Text = BM_Database_Clientes.BK_REPRESENTANTE1;
                textBoxRepresentantes2.Text = BM_Database_Clientes.BK_REPRESENTANTE2;
                textBoxRepresentantes3.Text = BM_Database_Clientes.BK_REPRESENTANTE3;
                textBoxTelefono1.Text = BM_Database_Clientes.BK_TELEFONO1;
                textBoxTelefono2.Text = BM_Database_Clientes.BK_TELEFONO2;
                textBoxDomicilio1.Text = BM_Database_Clientes.BK_DOMICILIO1;
                textBoxDomicilio2.Text = BM_Database_Clientes.BK_DOMICILIO2;
                textBoxNumero.Text = BM_Database_Clientes.BK_NUMERO;
                textBoxPiso.Text = BM_Database_Clientes.BK_PISO;
                textBoxOficina.Text = BM_Database_Clientes.BK_OFICINA;
                textBoxCodigoPostal.Text = BM_Database_Clientes.BK_CODIGOPOSTAL;
                textBoxObservaciones.Text = BM_Database_Clientes.BK_OBSERVACIONES;

                if (BM_Database_Clientes.Bm_E_Pais_EjecutarSelect())
                {
                    while (BM_Database_Clientes.Bm_E_Pais_Buscar())
                    {
                        comboBoxPais.Items.Add(BM_Database_Clientes.BK_E_PAIS);
                    }
                }

                comboBoxPais.Items.Add(BM_Database_Clientes.BK_PAIS);
                comboBoxPais.SelectedValue = BM_Database_Clientes.BK_PAIS;

                comboBoxEstado.Items.Add(BM_Database_Clientes.BK_REGION);
                comboBoxEstado.SelectedValue = BM_Database_Clientes.BK_REGION;

                comboBoxComuna.Items.Add(BM_Database_Clientes.BK_COMUNA);
                comboBoxComuna.SelectedValue = BM_Database_Clientes.BK_COMUNA;

                comboBoxCiudad.Items.Add(BM_Database_Clientes.BK_CIUDAD);
                comboBoxCiudad.SelectedValue = BM_Database_Clientes.BK_CIUDAD;

                textBoxObservaciones.Text = BM_Database_Clientes.BK_OBSERVACIONES;

                imageLogoCliente.Source = Base64StringToBitmap(BM_Database_Clientes.BK_FOTO);
            }
            catch (System.ArgumentNullException)
            {
                ErrorDeRecuperacionDialog();
            }
        }


        private async System.Threading.Tasks.Task LlenarDbConPantallaAsync()
        {
            BM_Database_Clientes.BK_FOTO = await ConvertirImageABase64Async();
            BM_Database_Clientes.BK_RUTID = textBoxRut.Text;
            BM_Database_Clientes.BK_DIGVER =  textBoxDigitoVerificador.Text;
            BM_Database_Clientes.BK_NOMBRE = textBoxNombreCliente.Text;
            BM_Database_Clientes.BK_ACTIVIDAD1 = textBoxActividad1.Text;
            BM_Database_Clientes.BK_ACTIVIDAD2 = textBoxActividad2.Text;
            BM_Database_Clientes.BK_REPRESENTANTE1 = textBoxRepresentantes1.Text;
            BM_Database_Clientes.BK_REPRESENTANTE2 = textBoxRepresentantes2.Text;
            BM_Database_Clientes.BK_REPRESENTANTE3 = textBoxRepresentantes3.Text;
            BM_Database_Clientes.BK_TELEFONO1 = textBoxTelefono1.Text;
            BM_Database_Clientes.BK_TELEFONO2 = textBoxTelefono2.Text;
            BM_Database_Clientes.BK_DOMICILIO1 = textBoxDomicilio1.Text;
            BM_Database_Clientes.BK_DOMICILIO2 = textBoxDomicilio2.Text;
            BM_Database_Clientes.BK_NUMERO = textBoxNumero.Text;
            BM_Database_Clientes.BK_PISO = textBoxPiso.Text;
            BM_Database_Clientes.BK_OFICINA = textBoxOficina.Text;
            BM_Database_Clientes.BK_CODIGOPOSTAL = textBoxCodigoPostal.Text;
            BM_Database_Clientes.BK_PAIS = comboBoxPais.Text;
            BM_Database_Clientes.BK_REGION = comboBoxEstado.Text;
            BM_Database_Clientes.BK_COMUNA = comboBoxComuna.Text;
            BM_Database_Clientes.BK_CIUDAD = comboBoxCiudad.Text;
            BM_Database_Clientes.BK_OBSERVACIONES = textBoxObservaciones.Text;
        }

        private async void BtnModificarClientes(object sender, RoutedEventArgs e)
        {
            await LlenarDbConPantallaAsync();
            BM_Database_Clientes.Bm_Clientes_Modificar();
        }

        private async void BtnAgregarClientes(object sender, RoutedEventArgs e)
        {
            await LlenarDbConPantallaAsync();
            BM_Database_Clientes.Bm_Clientes_Agregar();
        }

        private async System.Threading.Tasks.Task<string> ConvertirImageABase64Async()
        {
            var bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(imageLogoCliente);

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
                Title = "Acceso a Base de Datos",
                Content = "El registro a recuperar tiene valores nulos.",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noErrorRecuperacionDialog.ShowAsync();
        }
    }
}
