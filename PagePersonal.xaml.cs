using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PagePersonal : Page
    {
        private readonly JsonBikeMessengerPersonal EnviarJsonPersonal = new JsonBikeMessengerPersonal();
        private JsonBikeMessengerPersonal RecibirJsonPersonal = new JsonBikeMessengerPersonal();
        private readonly Bm_Personal_Database BM_Database_Personal = new Bm_Personal_Database();
        private TransferVar LvrTransferVar;
        private bool BorrarSiNo;

        public PagePersonal()
        {
            InitializeComponent();
            comboBoxAutorizacion.Items.Add("ADMINISTRADOR");
            comboBoxAutorizacion.Items.Add("OPERADOR");
            comboBoxAutorizacion.Items.Add("COLABORADOR");
            comboBoxAutorizacion.Items.Add("INTERMEDIARIO");
            comboBoxAutorizacion.Items.Add("CLIENTE");
            NavigationCacheMode = NavigationCacheMode.Disabled;
        }

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
                BM_Database_Personal.BM_CreateDatabase(LvrTransferVar.TV_Connection);
                RellenarCombos();

                if (LvrTransferVar.P_RUTID == "")
                {
                    if (BM_Database_Personal.Bm_Personal_Buscar())
                    {
                        LlenarPantallaConDb();
                    }
                }
                else
                {
                    if (BM_Database_Personal.Bm_Personal_Buscar(LvrTransferVar.P_PENTALPHA, LvrTransferVar.P_RUTID, LvrTransferVar.P_DIGVER))
                    {
                        LlenarPantallaConDb();
                    }
                }
                LlenarListaPersonal();
            }
        }

        private void BtnSeleccionarAjustes(object sender, RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(PageAjustes), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarServicios(object sender, RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(PageServicios), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarClientes(object sender, RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(PageClientes), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarRecursos(object sender, RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(PageRecursos), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarEmpresa(object sender, RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(PageEmpresa), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarPersonal(object sender, RoutedEventArgs e)
        {
            // _ = Frame.Navigate(typeof(PagePersonal), LvrTransferVar, new SuppressNavigationTransitionInfo());
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
                using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap.
                    Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage();

                    bitmapImage.SetSource(fileStream);
                    imageFotoPersonal.Source = bitmapImage;
                }
            }
        }

        private async void LlenarPantallaConDb()
        {
            try
            {
                // LvrTransferVar.P_PENTALPHA = BM_Database_Personal.BK_PENTALPHA;
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
                comboBoxPais.SelectedValue = BM_Database_Personal.BK_PAIS;
                comboBoxRegion.SelectedValue = BM_Database_Personal.BK_REGION;
                comboBoxComuna.SelectedValue = BM_Database_Personal.BK_COMUNA;
                comboBoxCiudad.SelectedValue = BM_Database_Personal.BK_CIUDAD;
                textBoxObservaciones.Text = BM_Database_Personal.BK_OBSERVACIONES;

                imageFotoPersonal.Source = Base64StringToBitmap(BM_Database_Personal.BK_FOTO);
            }
            catch (ArgumentNullException)
            {
                await AvisoOperacionPersonalDialogAsync("Acceso a Base de Datos Personal", "Error de carga de datos desde la Base de Datos.");
            }
        }

        private void RellenarCombos()
        {
            // Limpiar comboBox
            comboBoxPais.Items.Clear();
            comboBoxRegion.Items.Clear();
            comboBoxComuna.Items.Clear();
            comboBoxCiudad.Items.Clear();

            // Llenar Combo Pais
            if (BM_Database_Personal.Bm_E_Pais_EjecutarSelect())
            {
                while (BM_Database_Personal.Bm_E_Pais_Buscar())
                {
                    comboBoxPais.Items.Add(BM_Database_Personal.BK_E_PAIS);
                }
            }

            // Llenar Combo Region
            if (BM_Database_Personal.Bm_E_Region_EjecutarSelect())
            {
                while (BM_Database_Personal.Bm_E_Region_Buscar())
                {
                    comboBoxRegion.Items.Add(BM_Database_Personal.BK_E_REGION);
                }
            }

            // Llenar Combo Comuna
            if (BM_Database_Personal.Bm_E_Comuna_EjecutarSelect())
            {
                while (BM_Database_Personal.Bm_E_Comuna_Buscar())
                {
                    comboBoxComuna.Items.Add(BM_Database_Personal.BK_E_COMUNA);
                }
            }

            // Llenar Combo Ciudad
            if (BM_Database_Personal.Bm_E_Ciudad_EjecutarSelect())
            {
                while (BM_Database_Personal.Bm_E_Ciudad_Buscar())
                {
                    comboBoxCiudad.Items.Add(BM_Database_Personal.BK_E_CIUDAD);
                }
            }
        }

        private void LimpiarPantalla()
        {
            // LvrTransferVar.P_PENTALPHA = "";
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

        private async Task LlenarDbConPantallaAsync()
        {
            BM_Database_Personal.BK_PENTALPHA = LvrTransferVar.P_PENTALPHA;
            BM_Database_Personal.BK_RUTID = textBoxRut.Text;
            BM_Database_Personal.BK_DIGVER = textBoxDigitoVerificador.Text;
            // ***********************************************************
            // Agregando Campos de Parametros
            LvrTransferVar.P_RUTID = BM_Database_Personal.BK_RUTID;
            LvrTransferVar.P_DIGVER = BM_Database_Personal.BK_DIGVER;
            // ***********************************************************
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
            if (comboBoxCiudad.Text != "")
            {
                BM_Database_Personal.BK_CIUDAD = comboBoxCiudad.Text;
            }

            if (comboBoxComuna.Text != "")
            {
                BM_Database_Personal.BK_COMUNA = comboBoxComuna.Text;
            }

            if (comboBoxRegion.Text != "")
            {
                BM_Database_Personal.BK_REGION = comboBoxRegion.Text;
            }

            if (comboBoxPais.Text != "")
            {
                BM_Database_Personal.BK_PAIS = comboBoxPais.Text;
            }

            BM_Database_Personal.BK_OBSERVACIONES = textBoxObservaciones.Text;
            BM_Database_Personal.BK_FOTO = await ConvertirImageABase64Async();
        }

        private async void BtnAgregarPersonal(object sender, RoutedEventArgs e)
        {
            // Muestra de espera
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            try
            {
                await LlenarDbConPantallaAsync();

                bool TransaccionOK = false;

                if (BM_Database_Personal.Bm_Iniciar_Transaccion())
                {
                    if (BM_Database_Personal.Bm_Personal_Agregar())
                    {
                        TransaccionOK = true;

                    }
                    if (TransaccionOK)
                    {
                        if (LvrTransferVar.SincronizarWebRemoto())
                        {
                            TransaccionOK = ProRegistroPersonal("AGREGAR");
                        }
                        if (LvrTransferVar.SincronizarWebPropio())
                        {
                            TransaccionOK = ProRegistroPersonal("AGREGAR");
                        }
                    }
                }
                if (TransaccionOK)
                {
                    _ = BM_Database_Personal.Bm_Commit_Transaccion();
                    await AvisoOperacionPersonalDialogAsync("Agregar Personal", "Personal agregado exitosamente.");
                    LlenarListaPersonal();
                    LvrTransferVar.P_RUTID = BM_Database_Personal.BK_RUTID;
                    LvrTransferVar.P_DIGVER = BM_Database_Personal.BK_DIGVER;
                }
                else
                {
                    _ = BM_Database_Personal.Bm_Rollback_Transaccion();
                    await AvisoOperacionPersonalDialogAsync("Agregar Personal", "Error en ingreso de personal. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            catch (ArgumentException)
            {
                await AvisoOperacionPersonalDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de personal.");
            }

            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }

        private async void BtnModificarPersonal(object sender, RoutedEventArgs e)
        {
            // Muestra de espera
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            try
            {
                await LlenarDbConPantallaAsync();

                bool TransaccionOK = false;

                if (BM_Database_Personal.Bm_Iniciar_Transaccion())
                {
                    if (BM_Database_Personal.Bm_Personal_Modificar(LvrTransferVar.P_PENTALPHA, BM_Database_Personal.BK_RUTID, BM_Database_Personal.BK_DIGVER))
                    {
                        TransaccionOK = true;
                    }

                    if (TransaccionOK)
                    {
                        if (LvrTransferVar.SincronizarWebRemoto())
                        {
                            TransaccionOK = ProRegistroPersonal("MODIFICAR");
                        }
                        if (LvrTransferVar.SincronizarWebPropio())
                        {
                            TransaccionOK = ProRegistroPersonal("MODIFICAR");
                        }
                    }
                }

                if (TransaccionOK)
                {
                    _ = BM_Database_Personal.Bm_Commit_Transaccion();
                    await AvisoOperacionPersonalDialogAsync("Modificar Personal", "Personal modificada exitosamente.");
                    LlenarListaPersonal();
                    LvrTransferVar.P_RUTID = BM_Database_Personal.BK_RUTID;
                    LvrTransferVar.P_DIGVER = BM_Database_Personal.BK_DIGVER;
                }
                else
                {
                    _ = BM_Database_Personal.Bm_Rollback_Transaccion();
                    await AvisoOperacionPersonalDialogAsync("Modificar Personal", "Error en modificación de personal. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            catch (ArgumentException)
            {
                await AvisoOperacionPersonalDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de la personal.");
            }

            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }

        private async void BtnBorrarPersonal(object sender, RoutedEventArgs e)
        {

            BorrarSiNo = false;

            await AvisoBorrarPersonalDialogAsync();

            if (!BorrarSiNo)
            {
                return;
            }

            // Muestra de espera
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            try
            {
                await LlenarDbConPantallaAsync();

                bool TransaccionOK = false;

                if (BM_Database_Personal.Bm_Iniciar_Transaccion())
                {
                    if (BM_Database_Personal.Bm_Personal_Borrar(LvrTransferVar.P_PENTALPHA, BM_Database_Personal.BK_RUTID, BM_Database_Personal.BK_DIGVER))
                    {
                        TransaccionOK = true;
                    }

                    if (TransaccionOK)
                    {
                        if (LvrTransferVar.SincronizarWebRemoto())
                        {
                            TransaccionOK = ProRegistroPersonal("BORRAR");
                        }
                        if (LvrTransferVar.SincronizarWebPropio())
                        {
                            TransaccionOK = ProRegistroPersonal("BORRAR");
                        }
                    }
                }

                if (TransaccionOK)
                {
                    _ = BM_Database_Personal.Bm_Commit_Transaccion();
                    if (BM_Database_Personal.Bm_Personal_Buscar())
                    {
                        LvrTransferVar.P_RUTID = BM_Database_Personal.BK_RUTID;
                        LvrTransferVar.P_DIGVER = BM_Database_Personal.BK_DIGVER;
                    }
                    else
                    {
                        LvrTransferVar.P_RUTID = "";
                        LvrTransferVar.P_DIGVER = "";
                    }
                    LlenarPantallaConDb();
                    LlenarListaPersonal();
                    await AvisoOperacionPersonalDialogAsync("Borrar Personal", "Personal borrado exitosamente.");
                }
                else
                {
                    _ = BM_Database_Personal.Bm_Rollback_Transaccion();
                    await AvisoOperacionPersonalDialogAsync("Borrar Personal", "Error en borrado de personal. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            catch (ArgumentException)
            {
                await AvisoOperacionPersonalDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de personal.");
            }
            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }

        private void BtnListarPersonal(object sender, RoutedEventArgs e)
        {
            LvrTransferVar.PantallaAnterior = "PERSONAL";
            _ = Frame.Navigate(typeof(ListadosGenerales), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSalirPersonal(object sender, RoutedEventArgs e)
        {
            LvrTransferVar.TV_Connection.Close();
            Application.Current.Exit();
        }

        private async Task<string> ConvertirImageABase64Async()
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(imageFotoPersonal);

            byte[] image = (await bitmap.GetPixelsAsync()).ToArray();
            uint width = (uint)bitmap.PixelWidth;
            uint height = (uint)bitmap.PixelHeight;

            double dpiX = 96;
            double dpiY = 96;

            InMemoryRandomAccessStream encoded = new InMemoryRandomAccessStream();
            BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, encoded);

            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, width, height, dpiX, dpiY, image);
            await encoder.FlushAsync();
            encoded.Seek(0);

            byte[] bytes = new byte[encoded.Size];
            _ = await encoded.AsStream().ReadAsync(bytes, 0, bytes.Length);

            string base64String = Convert.ToBase64String(bytes);

            return base64String;
        }

        private BitmapImage Base64StringToBitmap(string source)
        {
            InMemoryRandomAccessStream ims = new InMemoryRandomAccessStream();
            byte[] bytes = Convert.FromBase64String(source);
            DataWriter dataWriter = new DataWriter(ims);
            dataWriter.WriteBytes(bytes);
            _ = dataWriter.StoreAsync();
            ims.Seek(0);
            BitmapImage img = new BitmapImage();
            img.SetSource(ims);
            return img;
        }

        private async Task AvisoOperacionPersonalDialogAsync(string xTitulo, string xDescripcion)
        {
            ContentDialog AvisoOperacionPersonalDialog = new ContentDialog
            {
                Title = xTitulo,
                Content = xDescripcion,
                CloseButtonText = "Continuar"
            };
            _ = await AvisoOperacionPersonalDialog.ShowAsync();
        }

        private async Task AvisoBorrarPersonalDialogAsync()
        {
            ContentDialog AvisoConfirmacionPersonalDialog = new ContentDialog
            {
                Title = "Borrar Personal",
                Content = "Confirme borrado del registro actual!",
                PrimaryButtonText = "Borrar",
                CloseButtonText = "Cancelar"
            };

            ContentDialogResult result = await AvisoConfirmacionPersonalDialog.ShowAsync();

            BorrarSiNo = result == ContentDialogResult.Primary;
        }

        private void LlenarListaPersonal()
        {
            List<GridPersonalIndividual> GridPersonalLista = new List<GridPersonalIndividual>();
            if (BM_Database_Personal.Bm_Personal_BuscarGrid())
            {
                while (BM_Database_Personal.Bm_Personal_BuscarGridProxima())
                {
                    GridPersonalLista.Add(
                        new GridPersonalIndividual
                        {
                            RUT = BM_Database_Personal.BK_GRID_RUT,
                            APELLIDO = BM_Database_Personal.BK_GRID_APELLIDOS,
                            NOMBRE = BM_Database_Personal.BK_GRID_NOMBRES
                        });
                }
            }
            dataGridPersonal.IsReadOnly = true;
            dataGridPersonal.ItemsSource = GridPersonalLista;
        }

        private void DataGridPersonal_Seleccion(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid CeldaSeleccionada = sender as DataGrid;
                GridPersonalIndividual Fila = (GridPersonalIndividual)CeldaSeleccionada.SelectedItems[0];
                string[] CadenaDividida = Fila.RUT.Split("-", 2, StringSplitOptions.None);

                if (BM_Database_Personal.Bm_Personal_Buscar(LvrTransferVar.P_PENTALPHA, CadenaDividida[0], CadenaDividida[1]))
                {
                    LimpiarPantalla();
                    LlenarPantallaConDb();
                    // LlenarListaPersonal();
                }
                else
                {
                    // textBoxNombrePersonal.Text = "Sin Personal";
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                ; // No hacer nada, es un control vacio de error
            }
        }

        //**************************************************
        // Ejecuta operacion de registro de personal
        //**************************************************
        private bool ProRegistroPersonal(string pTipoOperacion)
        {
            string LvrPRecibirServer;
            string LvrPData;
            string LvrStringHttp = "https://finanven.ddns.net";
            string LvrStringPort = "443";
            string LvrStringController = "/Api/BikeMessengerPersonal";

            LvrInternet LvrBKInternet = new LvrInternet();
            string LvrParametros;

            List<JsonBikeMessengerPersonal> EnviarJsonPersonalArray = new List<JsonBikeMessengerPersonal>();
            List<JsonBikeMessengerPersonal> RecibirJsonPersonalArray = new List<JsonBikeMessengerPersonal>();

            // Llenar estructura Json
            CopiarMemoriaEnJson(pTipoOperacion);

            // Proceso Serializar

            EnviarJsonPersonalArray.Add(EnviarJsonPersonal);
            LvrPData = JsonConvert.SerializeObject(EnviarJsonPersonalArray);

            // Preparar Parametros
            LvrParametros = LvrPData;

            LvrBKInternet.LvrInetPOST(LvrStringHttp, LvrStringPort, LvrStringController, LvrParametros);
            LvrPRecibirServer = LvrBKInternet.LvrResultadoWeb;

            if (LvrPRecibirServer != "ERROR" && LvrPRecibirServer != "" && LvrPRecibirServer != null)
            {
                // Procesar primer servidor
                RecibirJsonPersonalArray = JsonConvert.DeserializeObject<List<JsonBikeMessengerPersonal>>(LvrPRecibirServer); // resp será el string JSON a deserializa
                RecibirJsonPersonal = RecibirJsonPersonalArray[0];

                return RecibirJsonPersonal.RESOPERACION == "OK";
            }
            return false;
        }

        private void CopiarMemoriaEnJson(string pOPERACION)
        {
            // Limpiar Variables
            EnviarJsonPersonal.OPERACION = "";
            EnviarJsonPersonal.PENTALPHA = "";
            EnviarJsonPersonal.RUTID = "";
            EnviarJsonPersonal.DIGVER = "";
            EnviarJsonPersonal.APELLIDOS = "";
            EnviarJsonPersonal.NOMBRES = "";
            EnviarJsonPersonal.TELEFONO1 = "";
            EnviarJsonPersonal.TELEFONO2 = "";
            EnviarJsonPersonal.EMAIL = "";
            EnviarJsonPersonal.AUTORIZACION = "";
            EnviarJsonPersonal.CARGO = "";
            EnviarJsonPersonal.DOMICILIO = "";
            EnviarJsonPersonal.NUMERO = "";
            EnviarJsonPersonal.PISO = "";
            EnviarJsonPersonal.DPTO = "";
            EnviarJsonPersonal.CODIGOPOSTAL = "";
            EnviarJsonPersonal.CIUDAD = "";
            EnviarJsonPersonal.COMUNA = "";
            EnviarJsonPersonal.REGION = "";
            EnviarJsonPersonal.PAIS = "";
            EnviarJsonPersonal.OBSERVACIONES = "";
            EnviarJsonPersonal.FOTO = "";
            EnviarJsonPersonal.RESOPERACION = "";
            EnviarJsonPersonal.RESMENSAJE = "";

            // Llenar Variables
            EnviarJsonPersonal.OPERACION = pOPERACION;
            EnviarJsonPersonal.PENTALPHA = BM_Database_Personal.BK_PENTALPHA;
            EnviarJsonPersonal.RUTID = BM_Database_Personal.BK_RUTID;
            EnviarJsonPersonal.DIGVER = BM_Database_Personal.BK_DIGVER;
            EnviarJsonPersonal.APELLIDOS = BM_Database_Personal.BK_APELLIDOS;
            EnviarJsonPersonal.NOMBRES = BM_Database_Personal.BK_NOMBRES;
            EnviarJsonPersonal.TELEFONO1 = BM_Database_Personal.BK_TELEFONO1;
            EnviarJsonPersonal.TELEFONO2 = BM_Database_Personal.BK_TELEFONO2;
            EnviarJsonPersonal.EMAIL = BM_Database_Personal.BK_EMAIL;
            EnviarJsonPersonal.AUTORIZACION = BM_Database_Personal.BK_AUTORIZACION;
            EnviarJsonPersonal.CARGO = BM_Database_Personal.BK_CARGO;
            EnviarJsonPersonal.DOMICILIO = BM_Database_Personal.BK_DOMICILIO;
            EnviarJsonPersonal.NUMERO = BM_Database_Personal.BK_NUMERO;
            EnviarJsonPersonal.PISO = BM_Database_Personal.BK_PISO;
            EnviarJsonPersonal.DPTO = BM_Database_Personal.BK_DPTO;
            EnviarJsonPersonal.CODIGOPOSTAL = BM_Database_Personal.BK_CODIGOPOSTAL;
            EnviarJsonPersonal.CIUDAD = BM_Database_Personal.BK_CIUDAD;
            EnviarJsonPersonal.COMUNA = BM_Database_Personal.BK_COMUNA;
            EnviarJsonPersonal.REGION = BM_Database_Personal.BK_REGION;
            EnviarJsonPersonal.PAIS = BM_Database_Personal.BK_PAIS;
            EnviarJsonPersonal.OBSERVACIONES = BM_Database_Personal.BK_OBSERVACIONES;
            EnviarJsonPersonal.FOTO = BM_Database_Personal.BK_FOTO;
            EnviarJsonPersonal.RESOPERACION = "";
            EnviarJsonPersonal.RESMENSAJE = "";
        }

        private void CopiarJsonEnMemoria(string pOPERACION)
        {
            // Proceso
            // EnviarJsonPersonal.OPERACION = pOPERACION;
            BM_Database_Personal.BK_PENTALPHA = EnviarJsonPersonal.PENTALPHA;
            BM_Database_Personal.BK_RUTID = EnviarJsonPersonal.RUTID;
            BM_Database_Personal.BK_DIGVER = EnviarJsonPersonal.DIGVER;
            BM_Database_Personal.BK_APELLIDOS = EnviarJsonPersonal.APELLIDOS;
            BM_Database_Personal.BK_NOMBRES = EnviarJsonPersonal.NOMBRES;
            BM_Database_Personal.BK_TELEFONO1 = EnviarJsonPersonal.TELEFONO1;
            BM_Database_Personal.BK_TELEFONO2 = EnviarJsonPersonal.TELEFONO2;
            BM_Database_Personal.BK_EMAIL = EnviarJsonPersonal.EMAIL;
            BM_Database_Personal.BK_AUTORIZACION = EnviarJsonPersonal.AUTORIZACION;
            BM_Database_Personal.BK_CARGO = EnviarJsonPersonal.CARGO;
            BM_Database_Personal.BK_DOMICILIO = EnviarJsonPersonal.DOMICILIO;
            BM_Database_Personal.BK_NUMERO = EnviarJsonPersonal.NUMERO;
            BM_Database_Personal.BK_PISO = EnviarJsonPersonal.PISO;
            BM_Database_Personal.BK_DPTO = EnviarJsonPersonal.DPTO;
            BM_Database_Personal.BK_CODIGOPOSTAL = EnviarJsonPersonal.CODIGOPOSTAL;
            BM_Database_Personal.BK_CIUDAD = EnviarJsonPersonal.CIUDAD;
            BM_Database_Personal.BK_COMUNA = EnviarJsonPersonal.COMUNA;
            BM_Database_Personal.BK_REGION = EnviarJsonPersonal.REGION;
            BM_Database_Personal.BK_PAIS = EnviarJsonPersonal.PAIS;
            BM_Database_Personal.BK_OBSERVACIONES = EnviarJsonPersonal.OBSERVACIONES;
            BM_Database_Personal.BK_FOTO = EnviarJsonPersonal.FOTO;
            // BM_Database_Personal.BK_RESOPERACION = EnviarJsonPersonal.RESOPERACION;
            // BM_Database_Personal.BK_RESMENSAJE = EnviarJsonPersonal.RESMENSAJE;
        }
    }

    internal class GridPersonalIndividual
    {
        public string RUT { get; set; }
        public string APELLIDO { get; set; }
        public string NOMBRE { get; set; }
    }
}
