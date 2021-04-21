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
    public sealed partial class PageEmpresa : Page
    {
        private Bm_Empresa_Database BM_Database_Empresa = new Bm_Empresa_Database();
        SQLiteConnection BM_Connection;

        public PageEmpresa()
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
                BM_Connection = (SQLiteConnection) e.Parameter;
                if (BM_Database_Empresa.BM_CreateDatabase(BM_Connection))
                {
                    if (BM_Database_Empresa.Bm_Empresa_Buscar())
                    {
                        LlenarPantallaConDb();
                    }
                    else
                    {
                        textBoxNombreEmpresa.Text = "Sin Empresa";
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
            this.Frame.Navigate(typeof(PageRecursos), BM_Connection, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarEmpresa(object sender, RoutedEventArgs e)
        {
            // this.Frame.Navigate(typeof(PageEmpresa), BM_Connection, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarPersonal(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PagePersonal), BM_Connection, new SuppressNavigationTransitionInfo());
        }

        //private void flipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //
        //}

        private async void BtnEmpresasCargarFoto(object sender, RoutedEventArgs e)
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
                    imageLogoEmpresa.Source = bitmapImage;
                }
            }
        }
        private void LlenarPantallaConDb()
        {
            try
            {
                // List<DataGridActividadFilas> sourceActividad = new List<DataGridActividadFilas>();
                // List<DataGridRepresentanteFilas> sourceRepresentante = new List<DataGridRepresentanteFilas>();

                textBoxRut.Text = BM_Database_Empresa.BK_RUTID;
                textBoxDigitoVerificador.Text = BM_Database_Empresa.BK_DIGVER;
                textBoxNombreEmpresa.Text = BM_Database_Empresa.BK_NOMBRE;
                textBoxActividad1.Text = BM_Database_Empresa.BK_ACTIVIDAD1;
                textBoxActividad2.Text = BM_Database_Empresa.BK_ACTIVIDAD2;
                textBoxRepresentantes1.Text = BM_Database_Empresa.BK_REPRESENTANTE1;
                textBoxRepresentantes2.Text = BM_Database_Empresa.BK_REPRESENTANTE2;
                textBoxRepresentantes3.Text = BM_Database_Empresa.BK_REPRESENTANTE3;

                // dataGridActividad
                //sourceActividad.Add(new DataGridActividadFilas() { Actividad = "Actividad 1" });
                //sourceActividad.Add(new DataGridActividadFilas() { Actividad = "Actividad 2" });
                //sourceActividad.Add(new DataGridActividadFilas() { Actividad = "Actividad 3" });
                //sourceActividad.Add(new DataGridActividadFilas() { Actividad = "Actividad 4" });
                //sourceActividad.Add(new DataGridActividadFilas() { Actividad = "Actividad 5" });
                //dataGridActividad.ItemsSource = sourceActividad;

                // dataGridRepresentantes
                //sourceRepresentante.Add(new DataGridRepresentanteFilas() { 
                //    Nombre = "Nombre 1",
                //    Apellido = "Apellido 1",
                //    Cargo = "Cargo 1",
                //    Telefono = "Telefono 1",
                //    Email = "Email 1"
                //});
                //sourceRepresentante.Add(new DataGridRepresentanteFilas()
                //{
                //    Nombre = "Nombre 2",
                //    Apellido = "Apellido 2",
                //    Cargo = "Cargo 2",
                //    Telefono = "Telefono 2",
                //    Email = "Email 2"
                //});
                //sourceRepresentante.Add(new DataGridRepresentanteFilas()
                //{
                //    Nombre = "Nombre 3",
                //    Apellido = "Apellido 3",
                //    Cargo = "Cargo 3",
                //    Telefono = "Telefono 3",
                //    Email = "Email 3"
                //});
                //sourceRepresentante.Add(new DataGridRepresentanteFilas()
                //{
                //    Nombre = "Nombre 4",
                //    Apellido = "Apellido 4",
                //    Cargo = "Cargo 4",
                //    Telefono = "Telefono 4",
                //    Email = "Email 4"
                //});
                //sourceRepresentante.Add(new DataGridRepresentanteFilas()
                //{
                //    Nombre = "Nombre 5",
                //    Apellido = "Apellido 5",
                //    Cargo = "Cargo 5",
                //    Telefono = "Telefono 5",
                //    Email = "Email 5"
                //});

                // dataGridRepresentantes.ItemsSource = sourceRepresentante;

                textBoxCalleAvenida1.Text = BM_Database_Empresa.BK_DOMICILIO1;
                textBoxCalleAvenida2.Text = BM_Database_Empresa.BK_DOMICILIO2;
                textBoxNumero.Text = BM_Database_Empresa.BK_NUMERO;
                textBoxPiso.Text = BM_Database_Empresa.BK_PISO;
                textBoxOficina.Text = BM_Database_Empresa.BK_OFICINA;
                textBoxCodigoPostal.Text = BM_Database_Empresa.BK_CODIGOPOSTAL;

                if (BM_Database_Empresa.Bm_E_Pais_EjecutarSelect())
                {
                    while (BM_Database_Empresa.Bm_E_Pais_Buscar())
                    {
                        comboBoxPais.Items.Add(BM_Database_Empresa.BK_E_PAIS);
                    }
                }
                comboBoxPais.SelectedValue = BM_Database_Empresa.BK_PAIS;

                comboBoxEstado.Items.Add(BM_Database_Empresa.BK_ESTADOREGION);
                comboBoxEstado.SelectedValue = BM_Database_Empresa.BK_ESTADOREGION;

                comboBoxComuna.Items.Add(BM_Database_Empresa.BK_COMUNA);
                comboBoxComuna.SelectedValue = BM_Database_Empresa.BK_COMUNA;

                comboBoxCiudad.Items.Add(BM_Database_Empresa.BK_CIUDAD);
                comboBoxCiudad.SelectedValue = BM_Database_Empresa.BK_CIUDAD;

                textBoxObservaciones.Text = BM_Database_Empresa.BK_OBSERVACIONES;

                imageLogoEmpresa.Source = Base64StringToBitmap(BM_Database_Empresa.BK_LOGO);
            }
            catch (System.ArgumentNullException)
            {
                ErrorDeRecuperacionDialog();
            }
        }

            private async System.Threading.Tasks.Task LlenarDbConPantallaAsync()
        {
            BM_Database_Empresa.BK_LOGO = await ConvertirImageABase64Async();
            BM_Database_Empresa.BK_RUTID = textBoxRut.Text;
            BM_Database_Empresa.BK_DIGVER = textBoxDigitoVerificador.Text;
            BM_Database_Empresa.BK_NOMBRE = textBoxNombreEmpresa.Text;
            BM_Database_Empresa.BK_ACTIVIDAD1 = textBoxActividad1.Text;
            BM_Database_Empresa.BK_ACTIVIDAD2 = textBoxActividad2.Text;
            BM_Database_Empresa.BK_REPRESENTANTE1 = textBoxRepresentantes1.Text;
            BM_Database_Empresa.BK_REPRESENTANTE2 = textBoxRepresentantes2.Text;
            BM_Database_Empresa.BK_REPRESENTANTE3 = textBoxRepresentantes3.Text;
            BM_Database_Empresa.BK_DOMICILIO1 = textBoxCalleAvenida1.Text;
            BM_Database_Empresa.BK_DOMICILIO2 = textBoxCalleAvenida2.Text;
            BM_Database_Empresa.BK_NUMERO = textBoxNumero.Text;
            BM_Database_Empresa.BK_PISO = textBoxPiso.Text;
            BM_Database_Empresa.BK_OFICINA = textBoxOficina.Text;
            BM_Database_Empresa.BK_CODIGOPOSTAL = textBoxCodigoPostal.Text;
            BM_Database_Empresa.BK_PAIS = comboBoxPais.Text;
            BM_Database_Empresa.BK_ESTADOREGION = comboBoxEstado.Text;
            BM_Database_Empresa.BK_COMUNA = comboBoxComuna.Text;
            BM_Database_Empresa.BK_CIUDAD = comboBoxCiudad.Text;
            BM_Database_Empresa.BK_OBSERVACIONES = textBoxObservaciones.Text;
        }

        public class DataGridActividadFilas
        {
            public string Actividad { get; set; }
        }

        public class DataGridRepresentanteFilas
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Cargo { get; set; }
            public string Telefono { get; set; }
            public string Email { get; set; }
        }

        private async void BtnModificarEmpresa(object sender, RoutedEventArgs e)
        {
            await LlenarDbConPantallaAsync();
            BM_Database_Empresa.Bm_Empresa_Modificar();
        }

        private async System.Threading.Tasks.Task<string> ConvertirImageABase64Async()
        {
            var bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(imageLogoEmpresa);

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
