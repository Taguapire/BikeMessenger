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
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageEmpresa : Page
    {
        private JsonBikeMessengerEmpresa EnviarJsonEmpresa = new JsonBikeMessengerEmpresa();
        private JsonBikeMessengerEmpresa RecibirJsonEmpresa = new JsonBikeMessengerEmpresa();
        private Bm_Empresa_Database BM_Database_Empresa = new Bm_Empresa_Database();
        private TransferVar LvrTransferVar;
        private PentalphaCripto LvrCrypto = new PentalphaCripto();

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
                using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
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
                textBoxUsuario.Text = BM_Database_Empresa.BK_USUARIO;
                passwordClave.Password = BM_Database_Empresa.BK_CLAVE;
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

                textBoxTelefono1.Text = BM_Database_Empresa.BK_TELEFONO1;
                textBoxTelefono1.Text = BM_Database_Empresa.BK_TELEFONO2;
                textBoxTelefono1.Text = BM_Database_Empresa.BK_TELEFONO3;

                textBoxObservaciones.Text = BM_Database_Empresa.BK_OBSERVACIONES;

                imageLogoEmpresa.Source = Base64StringToBitmap(BM_Database_Empresa.BK_LOGO);
            }
            catch (System.ArgumentNullException)
            {
                await AvisoOperacionEmpresaDialogAsync("Acceso a Base de Datos", "Error de carga de datos desde la Base de Datos.");
            }
        }


        private void RellenarCombos()
        {
            // Limpiar Combo Box
            comboBoxPais.Items.Clear();
            comboBoxEstado.Items.Clear();
            comboBoxComuna.Items.Clear();
            comboBoxCiudad.Items.Clear();

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
            BM_Database_Empresa.BK_USUARIO = textBoxUsuario.Text;
            BM_Database_Empresa.BK_CLAVE = passwordClave.Password;
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

            if (comboBoxPais.Text != "")
                BM_Database_Empresa.BK_PAIS = comboBoxPais.Text;
            if (comboBoxEstado.Text != "")
                BM_Database_Empresa.BK_ESTADOREGION = comboBoxEstado.Text;
            if (comboBoxComuna.Text != "")
                BM_Database_Empresa.BK_COMUNA = comboBoxComuna.Text;
            if (comboBoxCiudad.Text != "")
                BM_Database_Empresa.BK_CIUDAD = comboBoxCiudad.Text;

            BM_Database_Empresa.BK_TELEFONO1 = textBoxTelefono1.Text;
            BM_Database_Empresa.BK_TELEFONO2 = textBoxTelefono2.Text;
            BM_Database_Empresa.BK_TELEFONO3 = textBoxTelefono3.Text;

            BM_Database_Empresa.BK_OBSERVACIONES = textBoxObservaciones.Text;
            BM_Database_Empresa.BK_LOGO = await ConvertirImageABase64Async();
        }

        private async void BtnAgregarEmpresa(object sender, RoutedEventArgs e)
        {
            // Muestra de espera
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // 1 sec delay

            // **********************************************************
            // Verificación de Claves
            // **********************************************************
            if (textBoxUsuario.Text == "" || passwordClave.Password == "")
            {
                LvrProgresRing.IsActive = false;
                await Task.Delay(500); // 1 sec delay
                _ = AvisoOperacionEmpresaDialogAsync("Identificación de Usuario", "Debe completar su usuario y clave para el envio de la Orden de Servicio.");
                return;
            }

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

                    // Agregar en Servidor Remoto
                    ProRegistroEmpresa("AGREGAR");
                    // Verificar Operación

                    // Aviso de OK
                    _ = AvisoOperacionEmpresaDialogAsync("Agregando Empresa", "Operación completada con exito.");
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
            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }

        private async void BtnModificarEmpresa(object sender, RoutedEventArgs e)
        {

            // Muestra de espera
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // 1 sec delay

            // **********************************************************
            // Verificación de Claves
            // **********************************************************
            if (textBoxUsuario.Text == "" || passwordClave.Password == "")
            {
                LvrProgresRing.IsActive = false;
                await Task.Delay(500); // 1 sec delay
                _ = AvisoOperacionEmpresaDialogAsync("Identificación de Usuario", "Debe completar su usuario y clave para el envio de la Orden de Servicio.");
                return;
            }

            // **********************************************************
            // Verificación de Valores de Pantalla
            // **********************************************************
            try
            {
                await LlenarDbConPantallaAsync();
                if (BM_Database_Empresa.Bm_Empresa_Modificar())
                {
                    ProRegistroEmpresa("MODIFICAR");
                    _ = AvisoOperacionEmpresaDialogAsync("Modificando Empresa", "Operación completada con exito.");
                }
                else
                {
                    _ = AvisoOperacionEmpresaDialogAsync("Modificando Empresa", "Se a producido un error al intentar modificar la empresa.");
                }
            }
            catch (System.ArgumentException)
            {
                _ = AvisoOperacionEmpresaDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de la empresa.");
            }
            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }

        private async void BtnBorrarEmpresa(object sender, RoutedEventArgs e)
        {

            // Muestra de espera
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // 1 sec delay

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

                    textBoxRut.IsReadOnly = false;
                    textBoxDigitoVerificador.IsReadOnly = false;

                    BM_Database_Empresa.BK_LOGO = "";
                    BM_Database_Empresa.BK_RUTID = "";
                    BM_Database_Empresa.BK_DIGVER = "";
                    BM_Database_Empresa.BK_NOMBRE = "";
                    BM_Database_Empresa.BK_USUARIO = "";
                    BM_Database_Empresa.BK_CLAVE = "";
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
                    BM_Database_Empresa.BK_TELEFONO1 = "";
                    BM_Database_Empresa.BK_TELEFONO2 = "";
                    BM_Database_Empresa.BK_TELEFONO3 = "";
                    BM_Database_Empresa.BK_OBSERVACIONES = "";
                    ProRegistroEmpresa("BORRAR");
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
                await AvisoOperacionEmpresaDialogAsync("Acceso a Base de Datos :", "Debe llenar los datos de la empresa.");
            }
            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
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

        private void LvrCalculoHashEmpresa(object sender, TextChangedEventArgs e)
        {
            string TempTexto = LvrCrypto.LvrRegionGeografica() + textBoxRut.Text + "-" + textBoxDigitoVerificador.Text + LvrCrypto.LvrGenRandomData(5);
            BM_Database_Empresa.BK_PENTALPHA = LvrCrypto.LvrByteArrayToString(LvrCrypto.LvrCalculoSHA256(TempTexto));
        }

        private void LvrCalculoHashEmpresaDig(object sender, TextChangedEventArgs e)
        {
            string TempTexto = LvrCrypto.LvrRegionGeografica() + textBoxRut.Text + "-" + textBoxDigitoVerificador.Text + LvrCrypto.LvrGenRandomData(5);
            BM_Database_Empresa.BK_PENTALPHA = LvrCrypto.LvrByteArrayToString(LvrCrypto.LvrCalculoSHA256(TempTexto));
        }

        //**************************************************
        // Ejecuta operacion de registro de empresa
        //**************************************************
        private void ProRegistroEmpresa(string pTipoOperacion)
        {
            string LvrPRecibirServer;
            string LvrPData;
            string LvrStringHttp1 = "https://finanven.ddns.net/Api/BikeMessengerEmpresa";

            LvrInternet LvrBKInternet = new LvrInternet();
            string LvrParametros;

            List<JsonBikeMessengerEmpresa> EnviarJsonEmpresaArray = new List<JsonBikeMessengerEmpresa>();
            List<JsonBikeMessengerEmpresa> RecibirJsonEmpresaArray = new List<JsonBikeMessengerEmpresa>();

            // Llenar estructura Json
            CopiarMemoriaEnJson(pTipoOperacion);

            // Proceso Serializar

            EnviarJsonEmpresaArray.Add(EnviarJsonEmpresa);
            LvrPData = JsonConvert.SerializeObject(EnviarJsonEmpresaArray);

            // Preparar Parametros
            LvrParametros = LvrPData;

            LvrBKInternet.LvrInetPOST(LvrStringHttp1, LvrParametros);
            LvrPRecibirServer = LvrBKInternet.LvrResultadoWeb;

            if (LvrPRecibirServer != "ERROR" && LvrPRecibirServer != "" && LvrPRecibirServer != null)
            {
                // Procesar primer servidor
                RecibirJsonEmpresaArray = JsonConvert.DeserializeObject<List<JsonBikeMessengerEmpresa>>(LvrPRecibirServer); // resp será el string JSON a deserializa
                RecibirJsonEmpresa = RecibirJsonEmpresaArray[0];

                if (RecibirJsonEmpresa.RESOPERACION == "OK") {
                    //CopiarJsonEnMemoria(pTipoOperacion);
                    _ = AvisoOperacionEmpresaDialogAsync("Estado Envio", RecibirJsonEmpresa.RESMENSAJE);
                }
                else
                {
                    _ = AvisoOperacionEmpresaDialogAsync("Estado Envio", RecibirJsonEmpresa.RESMENSAJE);
                }

                return;
            }
            _ = AvisoOperacionEmpresaDialogAsync("Registro de Empresa", "Problemas durante el registro remoto de la empresa. Debe repetir la operación");
        }

        void CopiarMemoriaEnJson(string pOPERACION)
        {
            // Limpiar Variables
            EnviarJsonEmpresa.OPERACION = "";
            EnviarJsonEmpresa.PENTALPHA = "";
            EnviarJsonEmpresa.RUTID = "";
            EnviarJsonEmpresa.DIGVER = "";
            EnviarJsonEmpresa.NOMBRE = "";
            EnviarJsonEmpresa.USUARIO = "";
            EnviarJsonEmpresa.CLAVE = "";
            EnviarJsonEmpresa.ACTIVIDAD1 = "";
            EnviarJsonEmpresa.ACTIVIDAD2 = "";
            EnviarJsonEmpresa.REPRESENTANTE1 = "";
            EnviarJsonEmpresa.REPRESENTANTE2 = "";
            EnviarJsonEmpresa.REPRESENTANTE3 = "";
            EnviarJsonEmpresa.DOMICILIO1 = "";
            EnviarJsonEmpresa.DOMICILIO2 = "";
            EnviarJsonEmpresa.NUMERO = "";
            EnviarJsonEmpresa.PISO = "";
            EnviarJsonEmpresa.OFICINA = "";
            EnviarJsonEmpresa.CIUDAD = "";
            EnviarJsonEmpresa.COMUNA = "";
            EnviarJsonEmpresa.ESTADOREGION = "";
            EnviarJsonEmpresa.CODIGOPOSTAL = "";
            EnviarJsonEmpresa.PAIS = "";
            EnviarJsonEmpresa.OBSERVACIONES = "";
            EnviarJsonEmpresa.LOGO = "";
            EnviarJsonEmpresa.RESOPERACION = "";
            EnviarJsonEmpresa.RESMENSAJE = "";

            // Llenar Variables
            EnviarJsonEmpresa.OPERACION = pOPERACION;
            EnviarJsonEmpresa.PENTALPHA = BM_Database_Empresa.BK_PENTALPHA;
            EnviarJsonEmpresa.RUTID = BM_Database_Empresa.BK_RUTID;
            EnviarJsonEmpresa.DIGVER = BM_Database_Empresa.BK_DIGVER; 
            EnviarJsonEmpresa.NOMBRE = BM_Database_Empresa.BK_NOMBRE; 
            EnviarJsonEmpresa.USUARIO = BM_Database_Empresa.BK_USUARIO; 
            EnviarJsonEmpresa.CLAVE = BM_Database_Empresa.BK_CLAVE; 
            EnviarJsonEmpresa.ACTIVIDAD1 = BM_Database_Empresa.BK_ACTIVIDAD1; 
            EnviarJsonEmpresa.ACTIVIDAD2 = BM_Database_Empresa.BK_ACTIVIDAD2; 
            EnviarJsonEmpresa.REPRESENTANTE1 = BM_Database_Empresa.BK_REPRESENTANTE1; 
            EnviarJsonEmpresa.REPRESENTANTE2 = BM_Database_Empresa.BK_REPRESENTANTE2; 
            EnviarJsonEmpresa.REPRESENTANTE3 = BM_Database_Empresa.BK_REPRESENTANTE3; 
            EnviarJsonEmpresa.DOMICILIO1 = BM_Database_Empresa.BK_DOMICILIO1; 
            EnviarJsonEmpresa.DOMICILIO2 = BM_Database_Empresa.BK_DOMICILIO2; 
            EnviarJsonEmpresa.NUMERO = BM_Database_Empresa.BK_NUMERO; 
            EnviarJsonEmpresa.PISO = BM_Database_Empresa.BK_PISO; 
            EnviarJsonEmpresa.OFICINA = BM_Database_Empresa.BK_OFICINA; 
            EnviarJsonEmpresa.CIUDAD = BM_Database_Empresa.BK_CIUDAD; 
            EnviarJsonEmpresa.COMUNA = BM_Database_Empresa.BK_COMUNA; 
            EnviarJsonEmpresa.ESTADOREGION = BM_Database_Empresa.BK_ESTADOREGION;
            EnviarJsonEmpresa.CODIGOPOSTAL = BM_Database_Empresa.BK_CODIGOPOSTAL;
            EnviarJsonEmpresa.PAIS = BM_Database_Empresa.BK_PAIS;
            EnviarJsonEmpresa.OBSERVACIONES = BM_Database_Empresa.BK_OBSERVACIONES;
            EnviarJsonEmpresa.LOGO = BM_Database_Empresa.BK_LOGO;
            EnviarJsonEmpresa.RESOPERACION = "";
            EnviarJsonEmpresa.RESMENSAJE = "";
        }

        void CopiarJsonEnMemoria(string pOPERACION)
        {
            // Proceso
            // RecibirJsonEmpresa.Json_OPERACION = pOPERACION;
            BM_Database_Empresa.BK_PENTALPHA = RecibirJsonEmpresa.PENTALPHA;
            BM_Database_Empresa.BK_RUTID = RecibirJsonEmpresa.RUTID;
            BM_Database_Empresa.BK_DIGVER = RecibirJsonEmpresa.DIGVER;
            BM_Database_Empresa.BK_NOMBRE = RecibirJsonEmpresa.NOMBRE;
            BM_Database_Empresa.BK_USUARIO = RecibirJsonEmpresa.USUARIO;
            BM_Database_Empresa.BK_CLAVE = RecibirJsonEmpresa.CLAVE;
            BM_Database_Empresa.BK_ACTIVIDAD1 = RecibirJsonEmpresa.ACTIVIDAD1;
            BM_Database_Empresa.BK_ACTIVIDAD2 = RecibirJsonEmpresa.ACTIVIDAD2;
            BM_Database_Empresa.BK_REPRESENTANTE1 = RecibirJsonEmpresa.REPRESENTANTE1;
            BM_Database_Empresa.BK_REPRESENTANTE2 = RecibirJsonEmpresa.REPRESENTANTE2;
            BM_Database_Empresa.BK_REPRESENTANTE3 = RecibirJsonEmpresa.REPRESENTANTE3;
            BM_Database_Empresa.BK_DOMICILIO1 = RecibirJsonEmpresa.DOMICILIO1;
            BM_Database_Empresa.BK_DOMICILIO2 = RecibirJsonEmpresa.DOMICILIO2;
            BM_Database_Empresa.BK_NUMERO = RecibirJsonEmpresa.NUMERO;
            BM_Database_Empresa.BK_PISO = RecibirJsonEmpresa.PISO;
            BM_Database_Empresa.BK_OFICINA = RecibirJsonEmpresa.OFICINA;
            BM_Database_Empresa.BK_CIUDAD = RecibirJsonEmpresa.CIUDAD;
            BM_Database_Empresa.BK_COMUNA = RecibirJsonEmpresa.COMUNA;
            BM_Database_Empresa.BK_ESTADOREGION = RecibirJsonEmpresa.ESTADOREGION;
            BM_Database_Empresa.BK_CODIGOPOSTAL = RecibirJsonEmpresa.CODIGOPOSTAL;
            BM_Database_Empresa.BK_PAIS = RecibirJsonEmpresa.PAIS;
            BM_Database_Empresa.BK_OBSERVACIONES = RecibirJsonEmpresa.OBSERVACIONES;
            BM_Database_Empresa.BK_LOGO = RecibirJsonEmpresa.LOGO;
            // BM_Database_Empresa = RecibirJsonEmpresa.Json_Resultado;
            // BM_Database_Empresa = RecibirJsonEmpresa.Json_ResultadoMsg;
        }
    }
}
