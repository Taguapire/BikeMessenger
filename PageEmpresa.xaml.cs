using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageEmpresa : Page
    {
        private bool BM_Existe_Empresa = false;
        private BK_Database BM_Database_Empresa = new BK_Database();

        public PageEmpresa()
        {
            this.InitializeComponent();
            if (BM_Database_Empresa.BM_CreateDatabase())
            {
                if (BM_Database_Empresa.Buscar_Empresa())
                {
                    LlenarPantallaConDb();
                    BM_Existe_Empresa = true;
                }
                else
                {
                    textBoxNombreEmpresa.Text = "Sin Empresa";
                }
            }

            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        private void BtnSeleccionarEmpresa(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageEmpresa));
        }

        private void BtnSeleccionarPersonal(object sender, RoutedEventArgs e)
        {
            if (BM_Existe_Empresa) // Verifica que exista una empresa
                this.Frame.Navigate(typeof(PagePersonal));
        }

        //private void flipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //
        //}

        private async void BtnEmpresasCargarFoto(object sender, RoutedEventArgs e)
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
                    imageLogoEmpresa.Source = bitmapImage;
                }
            }
        }
        private void LlenarPantallaConDb()
        {
            // List<DataGridActividadFilas> sourceActividad = new List<DataGridActividadFilas>();
            // List<DataGridRepresentanteFilas> sourceRepresentante = new List<DataGridRepresentanteFilas>();

            textBoxRut.Text = BM_Database_Empresa.Db_Empresa.BK_RUTID;
            textBoxDigitoVerificador.Text = BM_Database_Empresa.Db_Empresa.BK_DIGVER;
            textBoxNombreEmpresa.Text = BM_Database_Empresa.Db_Empresa.BK_NOMBRE;
            textBoxActividad1.Text = BM_Database_Empresa.Db_Empresa.BK_ACTIVIDAD1;
            textBoxActividad2.Text = BM_Database_Empresa.Db_Empresa.BK_ACTIVIDAD2;
            textBoxRepresentantes1.Text = BM_Database_Empresa.Db_Empresa.BK_REPRESENTANTE1;
            textBoxRepresentantes2.Text = BM_Database_Empresa.Db_Empresa.BK_REPRESENTANTE2;
            textBoxRepresentantes3.Text = BM_Database_Empresa.Db_Empresa.BK_REPRESENTANTE3;

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

            textBoxCalleAvenida1.Text = BM_Database_Empresa.Db_Empresa.BK_DOMICILIO1;
            textBoxCalleAvenida2.Text = BM_Database_Empresa.Db_Empresa.BK_DOMICILIO2;
            textBoxNumero.Text = BM_Database_Empresa.Db_Empresa.BK_NUMERO;
            textBoxPiso.Text = BM_Database_Empresa.Db_Empresa.BK_PISO;
            textBoxOficina.Text = BM_Database_Empresa.Db_Empresa.BK_OFICINA;
            textBoxCodigoPostal.Text = BM_Database_Empresa.Db_Empresa.BK_CODIGOPOSTAL;

            if (BM_Database_Empresa.Db_Empresa.Bm_E_Pais_EjecutarSelect())
            {
                while (BM_Database_Empresa.Db_Empresa.Bm_E_Pais_Buscar())
                {
                    comboBoxPais.Items.Add(BM_Database_Empresa.Db_Empresa.BK_E_PAIS);
                }
            }
            comboBoxPais.SelectedValue = BM_Database_Empresa.Db_Empresa.BK_PAIS;

            comboBoxEstado.Items.Add(BM_Database_Empresa.Db_Empresa.BK_ESTADOREGION);
            comboBoxEstado.SelectedValue = BM_Database_Empresa.Db_Empresa.BK_ESTADOREGION;

            comboBoxComuna.Items.Add(BM_Database_Empresa.Db_Empresa.BK_COMUNA);
            comboBoxComuna.SelectedValue = BM_Database_Empresa.Db_Empresa.BK_COMUNA;

            comboBoxCiudad.Items.Add(BM_Database_Empresa.Db_Empresa.BK_CIUDAD);
            comboBoxCiudad.SelectedValue = BM_Database_Empresa.Db_Empresa.BK_CIUDAD;
            
            textBoxObservaciones.Text = BM_Database_Empresa.Db_Empresa.BK_OBSERVACIONES;

            imageLogoEmpresa.Source = Base64StringToBitmap(BM_Database_Empresa.Db_Empresa.BK_LOGO);
        }

        private async System.Threading.Tasks.Task LlenarDbConPantallaAsync()
        {
            BM_Database_Empresa.Db_Empresa.BK_LOGO = await ConvertirImageABase64Async();
            BM_Database_Empresa.Db_Empresa.BK_RUTID = textBoxRut.Text;
            BM_Database_Empresa.Db_Empresa.BK_DIGVER = textBoxDigitoVerificador.Text;
            BM_Database_Empresa.Db_Empresa.BK_NOMBRE = textBoxNombreEmpresa.Text;
            BM_Database_Empresa.Db_Empresa.BK_ACTIVIDAD1 = textBoxActividad1.Text;
            BM_Database_Empresa.Db_Empresa.BK_ACTIVIDAD2 = textBoxActividad2.Text;
            BM_Database_Empresa.Db_Empresa.BK_REPRESENTANTE1 = textBoxRepresentantes1.Text;
            BM_Database_Empresa.Db_Empresa.BK_REPRESENTANTE2 = textBoxRepresentantes2.Text;
            BM_Database_Empresa.Db_Empresa.BK_REPRESENTANTE3 = textBoxRepresentantes3.Text;
            BM_Database_Empresa.Db_Empresa.BK_DOMICILIO1 = textBoxCalleAvenida1.Text;
            BM_Database_Empresa.Db_Empresa.BK_DOMICILIO2 = textBoxCalleAvenida2.Text;
            BM_Database_Empresa.Db_Empresa.BK_NUMERO = textBoxNumero.Text;
            BM_Database_Empresa.Db_Empresa.BK_PISO = textBoxPiso.Text;
            BM_Database_Empresa.Db_Empresa.BK_OFICINA = textBoxOficina.Text;
            BM_Database_Empresa.Db_Empresa.BK_CODIGOPOSTAL = textBoxCodigoPostal.Text;
            BM_Database_Empresa.Db_Empresa.BK_PAIS = comboBoxPais.Text;
            BM_Database_Empresa.Db_Empresa.BK_ESTADOREGION = comboBoxEstado.Text;
            BM_Database_Empresa.Db_Empresa.BK_COMUNA = comboBoxComuna.Text;
            BM_Database_Empresa.Db_Empresa.BK_CIUDAD = comboBoxCiudad.Text;
            BM_Database_Empresa.Db_Empresa.BK_OBSERVACIONES = textBoxObservaciones.Text;
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
            BM_Database_Empresa.Modificar_Empresa();
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
    }
}
