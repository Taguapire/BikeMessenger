using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
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
        private Bm_Empresa_Database BM_Database_Empresa = new Bm_Empresa_Database();
        TransferVar LvrTransferVar;

        public PageEmpresa()
        {
            // this.BM_Connection = BM_Connection;
            this.InitializeComponent();
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

        protected override async void OnNavigatedTo(NavigationEventArgs navigationEvent)
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
                LvrTransferVar = (TransferVar)navigationEvent.Parameter;
                if (BM_Database_Empresa.BM_CreateDatabase(LvrTransferVar.TV_Connection))
                {
                    RellenarCombos();
                    if (BM_Database_Empresa.Bm_Empresa_Buscar())
                    {
                        LlenarPantallaConDb();

                        appBarButtonEmpresa.IsEnabled = true;
                        appBarButtonPersonal.IsEnabled = true;
                        appBarButtonRecursos.IsEnabled = true;
                        appBarButtonClientes.IsEnabled = true;
                        appBarButtonServicios.IsEnabled = true;
                        appBarButtonAjustes.IsEnabled = true;

                        appBarAgregar.IsEnabled = false;
                        appBarModificar.IsEnabled = true;
                        appBarBorrar.IsEnabled = true;
                        appBarAceptar.IsEnabled = false;
                        appBarAceptar.IsEnabled = false;

                        textBoxRut.IsReadOnly = true;
                        textBoxDigitoVerificador.IsReadOnly = true;

                    }
                    else
                    {
                        appBarButtonEmpresa.IsEnabled = true;
                        appBarButtonPersonal.IsEnabled = false;
                        appBarButtonRecursos.IsEnabled = false;
                        appBarButtonClientes.IsEnabled = false;
                        appBarButtonServicios.IsEnabled = false;
                        appBarButtonAjustes.IsEnabled = true;

                        appBarAgregar.IsEnabled = true;
                        appBarModificar.IsEnabled = false;
                        appBarBorrar.IsEnabled = false;
                        appBarAceptar.IsEnabled = false;
                        appBarAceptar.IsEnabled = false;

                        textBoxRut.IsReadOnly = false;
                        textBoxDigitoVerificador.IsReadOnly = false;
                        await AvisoOperacionEmpresaDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de la empresa.");
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
            // this.Frame.Navigate(typeof(PageEmpresa), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarPersonal(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PagePersonal), LvrTransferVar, new SuppressNavigationTransitionInfo());
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
        private async void LlenarPantallaConDb()
        {
            try
            {
                textBoxRut.Text = BM_Database_Empresa.BK_RUTID;
                textBoxDigitoVerificador.Text = BM_Database_Empresa.BK_DIGVER;
                textBoxNombreEmpresa.Text = BM_Database_Empresa.BK_NOMBRE;
                textBoxActividad1.Text = BM_Database_Empresa.BK_ACTIVIDAD1;
                textBoxActividad2.Text = BM_Database_Empresa.BK_ACTIVIDAD2;
                textBoxRepresentantes1.Text = BM_Database_Empresa.BK_REPRESENTANTE1;
                textBoxRepresentantes2.Text = BM_Database_Empresa.BK_REPRESENTANTE2;
                textBoxRepresentantes3.Text = BM_Database_Empresa.BK_REPRESENTANTE3;

                textBoxCalleAvenida1.Text = BM_Database_Empresa.BK_DOMICILIO1;
                textBoxCalleAvenida2.Text = BM_Database_Empresa.BK_DOMICILIO2;
                textBoxNumero.Text = BM_Database_Empresa.BK_NUMERO;
                textBoxPiso.Text = BM_Database_Empresa.BK_PISO;
                textBoxOficina.Text = BM_Database_Empresa.BK_OFICINA;
                textBoxCodigoPostal.Text = BM_Database_Empresa.BK_CODIGOPOSTAL;

                comboBoxPais.SelectedValue = BM_Database_Empresa.BK_PAIS;
                comboBoxEstado.SelectedValue = BM_Database_Empresa.BK_ESTADOREGION;
                comboBoxComuna.SelectedValue = BM_Database_Empresa.BK_COMUNA;
                comboBoxCiudad.SelectedValue = BM_Database_Empresa.BK_CIUDAD;

                textBoxObservaciones.Text = BM_Database_Empresa.BK_OBSERVACIONES;

                imageLogoEmpresa.Source = Base64StringToBitmap(BM_Database_Empresa.BK_LOGO);
            }
            catch (System.ArgumentNullException e)
            {
                await AvisoOperacionEmpresaDialogAsync("Acceso a Base de Datos", e.Message);
            }
        }


        private void RellenarCombos()
        {
            // Llenar Combo Pais
            if (BM_Database_Empresa.Bm_E_Pais_EjecutarSelect())
            {
                while (BM_Database_Empresa.Bm_E_Pais_Buscar())
                {
                    comboBoxPais.Items.Add(BM_Database_Empresa.BK_E_PAIS);
                }
            }

            // Llenar Combo Region
            if (BM_Database_Empresa.Bm_E_Region_EjecutarSelect())
            {
                while (BM_Database_Empresa.Bm_E_Region_Buscar())
                {
                    comboBoxEstado.Items.Add(BM_Database_Empresa.BK_E_REGION);
                }
            }

            // Llenar Combo Comuna
            if (BM_Database_Empresa.Bm_E_Comuna_EjecutarSelect())
            {
                while (BM_Database_Empresa.Bm_E_Comuna_Buscar())
                {
                    comboBoxComuna.Items.Add(BM_Database_Empresa.BK_E_COMUNA);
                }
            }

            // Llenar Combo Ciudad
            if (BM_Database_Empresa.Bm_E_Ciudad_EjecutarSelect())
            {
                while (BM_Database_Empresa.Bm_E_Ciudad_Buscar())
                {
                    comboBoxCiudad.Items.Add(BM_Database_Empresa.BK_E_CIUDAD);
                }
            }
        }

        private async System.Threading.Tasks.Task LlenarDbConPantallaAsync()
        {
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
            BM_Database_Empresa.BK_LOGO = await ConvertirImageABase64Async();
        }

        private async void BtnAgregarEmpresa(object sender, RoutedEventArgs e)
        {
            try
            {
                await LlenarDbConPantallaAsync();
                if (BM_Database_Empresa.Bm_Empresa_Agregar())
                {
                    appBarButtonEmpresa.IsEnabled = true;
                    appBarButtonPersonal.IsEnabled = true;
                    appBarButtonRecursos.IsEnabled = true;
                    appBarButtonClientes.IsEnabled = true;
                    appBarButtonServicios.IsEnabled = true;
                    appBarButtonAjustes.IsEnabled = true;

                    appBarAgregar.IsEnabled = false;
                    appBarModificar.IsEnabled = true;
                    appBarBorrar.IsEnabled = true;
                    appBarAceptar.IsEnabled = false;
                    appBarAceptar.IsEnabled = false;

                    textBoxRut.IsReadOnly = true;
                    textBoxDigitoVerificador.IsReadOnly = true;

                    await AvisoOperacionEmpresaDialogAsync("Agregando Empresa", "Operación completada con exito.");
                }
                else
                {
                    await AvisoOperacionEmpresaDialogAsync("Agregando Empresa", "Se a producido un error al intentar agregar la empresa.");
                }
            }
            catch (System.ArgumentException)
            {
                await AvisoOperacionEmpresaDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de la empresa.");
            }
        }

        private async void BtnModificarEmpresa(object sender, RoutedEventArgs e)
        {
            try
            {
                await LlenarDbConPantallaAsync();
                if (BM_Database_Empresa.Bm_Empresa_Modificar())
                    await AvisoOperacionEmpresaDialogAsync("Modificando Empresa", "Operación completada con exito.");
                else
                    await AvisoOperacionEmpresaDialogAsync("Modificando Empresa", "Se a producido un error al intentar agregar la empresa.");
            }
            catch (System.ArgumentException)
            {
                await AvisoOperacionEmpresaDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de la empresa.");
            }
        }

        private async void BtnBorrarEmpresa(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BM_Database_Empresa.Bm_Empresa_Borrar())
                {
                    appBarButtonEmpresa.IsEnabled = true;
                    appBarButtonPersonal.IsEnabled = false;
                    appBarButtonRecursos.IsEnabled = false;
                    appBarButtonClientes.IsEnabled = false;
                    appBarButtonServicios.IsEnabled = false;
                    appBarButtonAjustes.IsEnabled = false;

                    appBarAgregar.IsEnabled = true;
                    appBarModificar.IsEnabled = false;
                    appBarBorrar.IsEnabled = false;
                    appBarAceptar.IsEnabled = false;
                    appBarAceptar.IsEnabled = false;

                    textBoxRut.IsReadOnly = false;
                    textBoxDigitoVerificador.IsReadOnly = false;

                    BM_Database_Empresa.BK_LOGO = "";
                    BM_Database_Empresa.BK_RUTID = "";
                    BM_Database_Empresa.BK_DIGVER = "";
                    BM_Database_Empresa.BK_NOMBRE = "";
                    BM_Database_Empresa.BK_ACTIVIDAD1 = "";
                    BM_Database_Empresa.BK_ACTIVIDAD2 = "";
                    BM_Database_Empresa.BK_REPRESENTANTE1 = "";
                    BM_Database_Empresa.BK_REPRESENTANTE2 = "";
                    BM_Database_Empresa.BK_REPRESENTANTE3 = "";
                    BM_Database_Empresa.BK_DOMICILIO1 = "";
                    BM_Database_Empresa.BK_DOMICILIO2 = "";
                    BM_Database_Empresa.BK_NUMERO = "";
                    BM_Database_Empresa.BK_PISO = "";
                    BM_Database_Empresa.BK_OFICINA = "";
                    BM_Database_Empresa.BK_CODIGOPOSTAL = "";
                    BM_Database_Empresa.BK_PAIS = "";
                    BM_Database_Empresa.BK_ESTADOREGION = "";
                    BM_Database_Empresa.BK_COMUNA = "";
                    BM_Database_Empresa.BK_CIUDAD = "";
                    BM_Database_Empresa.BK_OBSERVACIONES = "";
                    await AvisoOperacionEmpresaDialogAsync("Borrando Empresa", "Operación completada con exito.");
                    LlenarPantallaConDb();
                }
                else
                {
                    await AvisoOperacionEmpresaDialogAsync("Borrando Empresa", "Se a producido un error al intentar borrar la empresa.");
                }
            }
            catch (System.ArgumentException)
            {
                await AvisoOperacionEmpresaDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de la empresa.");
            }
        }

        private void BtnSalirEmpresa(object sender, RoutedEventArgs e)
        {
            LvrTransferVar.TV_Connection.Close();
            Application.Current.Exit();
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

        private async System.Threading.Tasks.Task AvisoOperacionEmpresaDialogAsync(string xTitulo, string xDescripcion)
        {
            ContentDialog AvisoOperacionEmpresaDialog = new ContentDialog
            {
                Title = xTitulo,
                Content = xDescripcion,
                CloseButtonText = "Continuar"
            };

            ContentDialogResult result = await AvisoOperacionEmpresaDialog.ShowAsync();
        }
    }
}
