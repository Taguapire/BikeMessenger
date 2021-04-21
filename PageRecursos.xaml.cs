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
    public sealed partial class PageRecursos : Page
    {
        readonly Bm_Recursos_Database BM_Database_Recursos = new Bm_Recursos_Database();
        SQLiteConnection BM_Connection;

        public PageRecursos()
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
                if (BM_Database_Recursos.BM_CreateDatabase(BM_Connection))
                {
                    if (BM_Database_Recursos.Bm_Personal_Buscar())
                    {
                        LlenarPantallaConDb();
                        // BM_Existe_Empresa = true;
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
            this.Frame.Navigate(typeof(PageClientes), BM_Connection, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarRecursos(object sender, RoutedEventArgs e)
        {
            // this.Frame.Navigate(typeof(PageRecursos), BM_Connection, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarEmpresa(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageEmpresa), BM_Connection, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarPersonal(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PagePersonal), BM_Connection, new SuppressNavigationTransitionInfo());
        }

        private async void BtnRecursosCargarFoto(object sender, RoutedEventArgs e)
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
                    imageFotoRecurso.Source = bitmapImage;
                }
            }
        }

        private void LlenarPantallaConDb()
        {
            try
            {
                textBoxRut.Text = BM_Database_Recursos.BK_RUTID;
                textBoxDigitoVerificador.Text = BM_Database_Recursos.BK_DIGVER;
                textBoxPropietario.Text = BM_Database_Recursos.BK_PROPIETARIO;
                textBoxDescripcion.Text = BM_Database_Recursos.BK_DESCRIPCION;
                textBoxPatenteCodigo.Text = BM_Database_Recursos.BK_PATENTE;
                textBoxMarca.Text = BM_Database_Recursos.BK_MARCA;
                textBoxModelo.Text = BM_Database_Recursos.BK_MODELO;
                textBoxVariante.Text = BM_Database_Recursos.BK_VARIANTE;
                textBoxAno.Text = BM_Database_Recursos.BK_ANO;
                textBoxColor.Text = BM_Database_Recursos.BK_COLOR;

                if (BM_Database_Recursos.Bm_E_Pais_EjecutarSelect())
                {
                    while (BM_Database_Recursos.Bm_E_Pais_Buscar())
                    {
                        comboBoxPais.Items.Add(BM_Database_Recursos.BK_E_PAIS);
                    }
                }
                comboBoxPais.Items.Add(BM_Database_Recursos.BK_PAIS);
                comboBoxPais.SelectedValue = BM_Database_Recursos.BK_PAIS;

                comboBoxEstado.Items.Add(BM_Database_Recursos.BK_REGION);
                comboBoxEstado.SelectedValue = BM_Database_Recursos.BK_REGION;

                comboBoxComuna.Items.Add(BM_Database_Recursos.BK_COMUNA);
                comboBoxComuna.SelectedValue = BM_Database_Recursos.BK_COMUNA;

                comboBoxCiudad.Items.Add(BM_Database_Recursos.BK_CIUDAD);
                comboBoxCiudad.SelectedValue = BM_Database_Recursos.BK_CIUDAD;

                textBoxObservaciones.Text = BM_Database_Recursos.BK_OBSERVACIONES;

                imageFotoRecurso.Source = Base64StringToBitmap(BM_Database_Recursos.BK_FOTO);
            }
            catch (System.ArgumentNullException)
            {
                ;
            }
        }

        private async System.Threading.Tasks.Task LlenarDbConPantallaAsync()
        {
            BM_Database_Recursos.BK_FOTO = await ConvertirImageABase64Async();
            BM_Database_Recursos.BK_RUTID = textBoxRut.Text;
            BM_Database_Recursos.BK_DIGVER = textBoxDigitoVerificador.Text;
            BM_Database_Recursos.BK_PROPIETARIO = textBoxPropietario.Text;
            BM_Database_Recursos.BK_DESCRIPCION = textBoxDescripcion.Text;
            BM_Database_Recursos.BK_PATENTE = textBoxPatenteCodigo.Text;
            BM_Database_Recursos.BK_MARCA = textBoxMarca.Text;
            BM_Database_Recursos.BK_MODELO = textBoxModelo.Text;
            BM_Database_Recursos.BK_VARIANTE = textBoxVariante.Text;
            BM_Database_Recursos.BK_ANO = textBoxAno.Text;
            BM_Database_Recursos.BK_COLOR = textBoxColor.Text;
            BM_Database_Recursos.BK_CIUDAD = comboBoxCiudad.Text;
            BM_Database_Recursos.BK_COMUNA = comboBoxComuna.Text;
            BM_Database_Recursos.BK_REGION = comboBoxEstado.Text;
            BM_Database_Recursos.BK_PAIS = comboBoxPais.Text;
            BM_Database_Recursos.BK_OBSERVACIONES = textBoxObservaciones.Text;
        }

        private async void BtnModificarRecursos(object sender, RoutedEventArgs e)
        {
            await LlenarDbConPantallaAsync();
            BM_Database_Recursos.Bm_Recursos_Modificar();
        }

        private async void BtnAgregarRecursos(object sender, RoutedEventArgs e)
        {
            await LlenarDbConPantallaAsync();
            BM_Database_Recursos.Bm_Recursos_Agregar();
        }

        private async System.Threading.Tasks.Task<string> ConvertirImageABase64Async()
        {
            var bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(imageFotoRecurso);

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
    }
}
