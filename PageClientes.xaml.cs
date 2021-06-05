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
    public sealed partial class PageClientes : Page
    {
        private readonly JsonBikeMessengerCliente EnviarJsonCliente = new JsonBikeMessengerCliente();
        private JsonBikeMessengerCliente RecibirJsonCliente = new JsonBikeMessengerCliente();
        private readonly Bm_Cliente_Database BM_Database_Cliente = new Bm_Cliente_Database();
        private TransferVar LvrTransferVar;
        private bool BorrarSiNo;
        public PageClientes()
        {
            // this.BM_Connection = BM_Connection;
            InitializeComponent();
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
                _ = BM_Database_Cliente.BM_CreateDatabase(LvrTransferVar.TV_Connection);
                RellenarCombos();

                if (LvrTransferVar.C_RUTID == "")
                {
                    if (BM_Database_Cliente.Bm_Clientes_Buscar())
                    {
                        LlenarPantallaConDb();
                    }
                }
                else
                {
                    if (BM_Database_Cliente.Bm_Clientes_Buscar(LvrTransferVar.C_PENTALPHA, LvrTransferVar.C_RUTID, LvrTransferVar.C_DIGVER))
                    {
                        LlenarPantallaConDb();
                    }
                }
                LlenarListaClientes();
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
            // _ = Frame.Navigate(typeof(PageClientes), LvrTransferVar, new SuppressNavigationTransitionInfo());
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
            _ = Frame.Navigate(typeof(PagePersonal), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private async void BtnClientesCargarFoto(object sender, RoutedEventArgs e)
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
                    BitmapImage bitmapImage = new BitmapImage();

                    bitmapImage.SetSource(fileStream);
                    imageLogoCliente.Source = bitmapImage;
                }
            }
        }

        private async void LlenarPantallaConDb()
        {
            try
            {
                // LvrTransferVar.R_PENTALPHA = BM_Database_Recurso.BK_PENTALPHA;
                textBoxRut.Text = BM_Database_Cliente.BK_RUTID;
                textBoxDigitoVerificador.Text = BM_Database_Cliente.BK_DIGVER;
                textBoxNombreCliente.Text = BM_Database_Cliente.BK_NOMBRE;
                textBoxUsuario.Text = BM_Database_Cliente.BK_USUARIO;
                passwordClave.Password = BM_Database_Cliente.BK_CLAVE;
                textBoxActividad1.Text = BM_Database_Cliente.BK_ACTIVIDAD1;
                textBoxActividad2.Text = BM_Database_Cliente.BK_ACTIVIDAD2;
                textBoxRepresentantes1.Text = BM_Database_Cliente.BK_REPRESENTANTE1;
                textBoxRepresentantes2.Text = BM_Database_Cliente.BK_REPRESENTANTE2;
                textBoxRepresentantes3.Text = BM_Database_Cliente.BK_REPRESENTANTE3;
                textBoxTelefono1.Text = BM_Database_Cliente.BK_TELEFONO1;
                textBoxTelefono2.Text = BM_Database_Cliente.BK_TELEFONO2;
                textBoxDomicilio1.Text = BM_Database_Cliente.BK_DOMICILIO1;
                textBoxDomicilio2.Text = BM_Database_Cliente.BK_DOMICILIO2;
                textBoxNumero.Text = BM_Database_Cliente.BK_NUMERO;
                textBoxPiso.Text = BM_Database_Cliente.BK_PISO;
                textBoxOficina.Text = BM_Database_Cliente.BK_OFICINA;
                textBoxCodigoPostal.Text = BM_Database_Cliente.BK_CODIGOPOSTAL;
                textBoxObservaciones.Text = BM_Database_Cliente.BK_OBSERVACIONES;

                comboBoxPais.SelectedValue = BM_Database_Cliente.BK_PAIS;
                comboBoxEstado.SelectedValue = BM_Database_Cliente.BK_REGION;
                comboBoxComuna.SelectedValue = BM_Database_Cliente.BK_COMUNA;
                comboBoxCiudad.SelectedValue = BM_Database_Cliente.BK_CIUDAD;

                textBoxObservaciones.Text = BM_Database_Cliente.BK_OBSERVACIONES;

                imageLogoCliente.Source = Base64StringToBitmap(BM_Database_Cliente.BK_FOTO);
            }
            catch (ArgumentNullException)
            {
                await AvisoOperacionClientesDialogAsync("Acceso a Base de Datos Cliente", "Error de carga de datos desde la Base de Datos.");
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
            if (BM_Database_Cliente.Bm_E_Pais_EjecutarSelect())
            {
                while (BM_Database_Cliente.Bm_E_Pais_Buscar())
                {
                    comboBoxPais.Items.Add(BM_Database_Cliente.BK_E_PAIS);
                }
            }

            // Llenar Combo Region
            if (BM_Database_Cliente.Bm_E_Region_EjecutarSelect())
            {
                while (BM_Database_Cliente.Bm_E_Region_Buscar())
                {
                    comboBoxEstado.Items.Add(BM_Database_Cliente.BK_E_REGION);
                }
            }

            // Llenar Combo Comuna
            if (BM_Database_Cliente.Bm_E_Comuna_EjecutarSelect())
            {
                while (BM_Database_Cliente.Bm_E_Comuna_Buscar())
                {
                    comboBoxComuna.Items.Add(BM_Database_Cliente.BK_E_COMUNA);
                }
            }

            // Llenar Combo Ciudad
            if (BM_Database_Cliente.Bm_E_Ciudad_EjecutarSelect())
            {
                while (BM_Database_Cliente.Bm_E_Ciudad_Buscar())
                {
                    comboBoxCiudad.Items.Add(BM_Database_Cliente.BK_E_CIUDAD);
                }
            }
        }

        private void LimpiarPantalla()
        {
            LvrTransferVar.C_RUTID = "";
            LvrTransferVar.C_DIGVER = "";
            textBoxRut.Text = "";
            textBoxDigitoVerificador.Text = "";
            textBoxNombreCliente.Text = "";
            textBoxUsuario.Text = "";
            passwordClave.Password = "";
            textBoxActividad1.Text = "";
            textBoxActividad2.Text = "";
            textBoxRepresentantes1.Text = "";
            textBoxRepresentantes2.Text = "";
            textBoxRepresentantes3.Text = "";
            textBoxTelefono1.Text = "";
            textBoxTelefono2.Text = "";
            textBoxDomicilio1.Text = "";
            textBoxDomicilio2.Text = "";
            textBoxNumero.Text = "";
            textBoxPiso.Text = "";
            textBoxOficina.Text = "";
            textBoxCodigoPostal.Text = "";
            comboBoxPais.Text = "";
            comboBoxEstado.Text = "";
            comboBoxComuna.Text = "";
            comboBoxCiudad.Text = "";
            textBoxObservaciones.Text = "";
            imageLogoCliente.Source = Base64StringToBitmap("");
        }

        private async Task LlenarDbConPantallaAsync()
        {
            BM_Database_Cliente.BK_PENTALPHA = LvrTransferVar.C_PENTALPHA;
            BM_Database_Cliente.BK_RUTID = textBoxRut.Text;
            BM_Database_Cliente.BK_DIGVER = textBoxDigitoVerificador.Text;
            BM_Database_Cliente.BK_NOMBRE = textBoxNombreCliente.Text;
            BM_Database_Cliente.BK_USUARIO = textBoxUsuario.Text;
            BM_Database_Cliente.BK_CLAVE = passwordClave.Password;
            BM_Database_Cliente.BK_ACTIVIDAD1 = textBoxActividad1.Text;
            BM_Database_Cliente.BK_ACTIVIDAD2 = textBoxActividad2.Text;
            BM_Database_Cliente.BK_REPRESENTANTE1 = textBoxRepresentantes1.Text;
            BM_Database_Cliente.BK_REPRESENTANTE2 = textBoxRepresentantes2.Text;
            BM_Database_Cliente.BK_REPRESENTANTE3 = textBoxRepresentantes3.Text;
            BM_Database_Cliente.BK_TELEFONO1 = textBoxTelefono1.Text;
            BM_Database_Cliente.BK_TELEFONO2 = textBoxTelefono2.Text;
            BM_Database_Cliente.BK_DOMICILIO1 = textBoxDomicilio1.Text;
            BM_Database_Cliente.BK_DOMICILIO2 = textBoxDomicilio2.Text;
            BM_Database_Cliente.BK_NUMERO = textBoxNumero.Text;
            BM_Database_Cliente.BK_PISO = textBoxPiso.Text;
            BM_Database_Cliente.BK_OFICINA = textBoxOficina.Text;
            BM_Database_Cliente.BK_CODIGOPOSTAL = textBoxCodigoPostal.Text;
            if (comboBoxPais.Text != "")
            {
                BM_Database_Cliente.BK_PAIS = comboBoxPais.Text;
            }

            if (comboBoxEstado.Text != "")
            {
                BM_Database_Cliente.BK_REGION = comboBoxEstado.Text;
            }

            if (comboBoxComuna.Text != "")
            {
                BM_Database_Cliente.BK_COMUNA = comboBoxComuna.Text;
            }

            if (comboBoxCiudad.Text != "")
            {
                BM_Database_Cliente.BK_CIUDAD = comboBoxCiudad.Text;
            }

            BM_Database_Cliente.BK_OBSERVACIONES = textBoxObservaciones.Text;
            BM_Database_Cliente.BK_FOTO = await ConvertirImageABase64Async();
        }

        private async void BtnAgregarClientes(object sender, RoutedEventArgs e)
        {
            // Muestra de espera
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            try
            {
                await LlenarDbConPantallaAsync();

                bool TransaccionOK = false;

                if (BM_Database_Cliente.Bm_Iniciar_Transaccion())
                {
                    if (BM_Database_Cliente.Bm_Clientes_Agregar())
                    {
                        TransaccionOK = true;
                    }
                    if (TransaccionOK)
                    {
                        if (LvrTransferVar.SincronizarWebRemoto())
                        {
                            TransaccionOK = ProRegistroCliente("AGREGAR");
                        }
                        if (LvrTransferVar.SincronizarWebPropio())
                        {
                            TransaccionOK = ProRegistroCliente("AGREGAR");
                        }
                    }
                }
                if (TransaccionOK)
                {
                    _ = BM_Database_Cliente.Bm_Commit_Transaccion();
                    await AvisoOperacionClientesDialogAsync("Agregar Cliente", "Cliente agregado exitosamente.");
                    LlenarListaClientes();
                    LvrTransferVar.C_RUTID = BM_Database_Cliente.BK_RUTID;
                    LvrTransferVar.C_DIGVER = BM_Database_Cliente.BK_DIGVER;
                }
                else
                {
                    _ = BM_Database_Cliente.Bm_Rollback_Transaccion();
                    await AvisoOperacionClientesDialogAsync("Agregar Cliente", "Error en ingreso de cliente. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            catch (ArgumentException)
            {
                await AvisoOperacionClientesDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de cliente.");
            }

            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }


        private async void BtnModificarClientes(object sender, RoutedEventArgs e)
        {
            // Muestra de espera
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            try
            {
                await LlenarDbConPantallaAsync();

                bool TransaccionOK = false;

                if (BM_Database_Cliente.Bm_Iniciar_Transaccion())
                {
                    if (BM_Database_Cliente.Bm_Clientes_Modificar(LvrTransferVar.C_PENTALPHA, BM_Database_Cliente.BK_RUTID, BM_Database_Cliente.BK_DIGVER))
                    {
                        TransaccionOK = true;
                    }

                    if (TransaccionOK)
                    {
                        if (LvrTransferVar.SincronizarWebRemoto())
                        {
                            TransaccionOK = ProRegistroCliente("MODIFICAR");
                        }
                        if (LvrTransferVar.SincronizarWebPropio())
                        {
                            TransaccionOK = ProRegistroCliente("MODIFICAR");
                        }
                    }
                }

                if (TransaccionOK)
                {
                    _ = BM_Database_Cliente.Bm_Commit_Transaccion();
                    LlenarListaClientes();
                    LvrTransferVar.C_RUTID = BM_Database_Cliente.BK_RUTID;
                    LvrTransferVar.C_DIGVER = BM_Database_Cliente.BK_DIGVER;
                }
                else
                {
                    _ = BM_Database_Cliente.Bm_Rollback_Transaccion();
                    await AvisoOperacionClientesDialogAsync("Modificar Cliente", "Error en modificación de cliente. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            catch (ArgumentException)
            {
                await AvisoOperacionClientesDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de cliente.");
            }

            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }

        private async void BtnBorrarClientes(object sender, RoutedEventArgs e)
        {
            BorrarSiNo = false;

            await AvisoBorrarClientesDialogAsync();

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

                if (BM_Database_Cliente.Bm_Iniciar_Transaccion())
                {
                    if (BM_Database_Cliente.Bm_Clientes_Borrar(LvrTransferVar.C_PENTALPHA, BM_Database_Cliente.BK_RUTID, BM_Database_Cliente.BK_DIGVER))
                    {
                        TransaccionOK = true;
                    }

                    if (TransaccionOK)
                    {
                        if (LvrTransferVar.SincronizarWebRemoto())
                        {
                            TransaccionOK = ProRegistroCliente("BORRAR");
                        }
                        if (LvrTransferVar.SincronizarWebPropio())
                        {
                            TransaccionOK = ProRegistroCliente("BORRAR");
                        }
                    }
                }

                if (TransaccionOK)
                {
                    _ = BM_Database_Cliente.Bm_Commit_Transaccion();
                    if (BM_Database_Cliente.Bm_Clientes_Buscar())
                    {
                        LvrTransferVar.C_RUTID = BM_Database_Cliente.BK_RUTID;
                        LvrTransferVar.C_DIGVER = BM_Database_Cliente.BK_DIGVER;
                    }
                    else
                    {
                        LvrTransferVar.P_RUTID = "";
                        LvrTransferVar.P_DIGVER = "";
                    }
                    LlenarPantallaConDb();
                    LlenarListaClientes();
                    await AvisoOperacionClientesDialogAsync("Borrar Cliente", "Cliente borrado exitosamente.");
                }
                else
                {
                    _ = BM_Database_Cliente.Bm_Rollback_Transaccion();
                    await AvisoOperacionClientesDialogAsync("Borrar Cliente", "Error en borrado de cliente. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            catch (ArgumentException)
            {
                await AvisoOperacionClientesDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de cliente.");
            }
            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }

        private void BtnListarClientes(object sender, RoutedEventArgs e)
        {
            LvrTransferVar.PantallaAnterior = "CLIENTE";
            _ = Frame.Navigate(typeof(ListadosGenerales), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSalirClientes(object sender, RoutedEventArgs e)
        {
            LvrTransferVar.TV_Connection.Close();
            Application.Current.Exit();
        }

        private async Task<string> ConvertirImageABase64Async()
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(imageLogoCliente);

            byte[] image = (await bitmap.GetPixelsAsync()).ToArray();
            uint width = (uint)bitmap.PixelWidth;
            uint height = (uint)bitmap.PixelHeight;

            double dpiX = 96;
            double dpiY = 96;

            InMemoryRandomAccessStream encoded = new InMemoryRandomAccessStream();
            BitmapEncoder encoder = await Windows.Graphics.Imaging.BitmapEncoder.CreateAsync(Windows.Graphics.Imaging.BitmapEncoder.PngEncoderId, encoded);

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


        private async Task AvisoOperacionClientesDialogAsync(string xTitulo, string xDescripcion)
        {
            ContentDialog AvisoOperacionClientesDialog = new ContentDialog
            {
                Title = xTitulo,
                Content = xDescripcion,
                CloseButtonText = "Continuar"
            };
            _ = await AvisoOperacionClientesDialog.ShowAsync();
        }

        private async Task AvisoBorrarClientesDialogAsync()
        {
            ContentDialog AvisoConfirmacionClientesDialog = new ContentDialog
            {
                Title = "Borrar Persona",
                Content = "Confirme borrado del registro actual!",
                PrimaryButtonText = "Borrar",
                CloseButtonText = "Cancelar"
            };

            ContentDialogResult result = await AvisoConfirmacionClientesDialog.ShowAsync();

            BorrarSiNo = result == ContentDialogResult.Primary;
        }

        private void LlenarListaClientes()
        {
            List<GridClientesIndividual> GridClientesLista = new List<GridClientesIndividual>();

            if (BM_Database_Cliente.Bm_Clientes_BuscarGrid())
            {
                while (BM_Database_Cliente.Bm_Clientes_BuscarGridProxima())
                {
                    GridClientesLista.Add(
                        new GridClientesIndividual {
                            RUT = BM_Database_Cliente.BK_GRID_RUT, 
                            CLIENTE = BM_Database_Cliente.BK_GRID_NOMBRES
                        }
                        );
                }
            }

            dataGridListadoClientes.IsReadOnly = true;
            dataGridListadoClientes.ItemsSource = GridClientesLista;
        }

        private void DataGridSeleccion_Clientes(object sender, SelectionChangedEventArgs e) 
        {
            try
            {
                DataGrid CeldaSeleccionada = sender as DataGrid;
                GridClientesIndividual Fila = (GridClientesIndividual)CeldaSeleccionada.SelectedItems[0];
                string[] CadenaDividida = Fila.RUT.Split("-", 2, StringSplitOptions.None);

                if (BM_Database_Cliente.Bm_Clientes_Buscar(LvrTransferVar.C_PENTALPHA, CadenaDividida[0], CadenaDividida[1]))
                {
                    LimpiarPantalla();
                    LlenarPantallaConDb();
                    // LlenarListaClientes();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                // No hacer nada, es un control vacio de error
            }
        }

        //**************************************************
        // Ejecuta operacion de registro de Clientes
        //**************************************************
        private bool ProRegistroCliente(string pTipoOperacion)
        {
            string LvrPRecibirServer;
            string LvrPData;
            string LvrStringHttp = "https://finanven.ddns.net";
            string LvrStringPort = "443";
            string LvrStringController = "/Api/BikeMessengerCliente";


            LvrInternet LvrBKInternet = new LvrInternet();
            string LvrParametros;

            List<JsonBikeMessengerCliente> EnviarJsonClienteArray = new List<JsonBikeMessengerCliente>();
            List<JsonBikeMessengerCliente> RecibirJsonClienteArray = new List<JsonBikeMessengerCliente>();

            // Llenar estructura Json
            CopiarMemoriaEnJson(pTipoOperacion);

            // Proceso Serializar

            EnviarJsonClienteArray.Add(EnviarJsonCliente);
            LvrPData = JsonConvert.SerializeObject(EnviarJsonClienteArray);

            // Preparar Parametros
            LvrParametros = LvrPData;

            LvrBKInternet.LvrInetPOST(LvrStringHttp, LvrStringPort, LvrStringController, LvrParametros);
            LvrPRecibirServer = LvrBKInternet.LvrResultadoWeb;

            if (LvrPRecibirServer != "ERROR" && LvrPRecibirServer != "" && LvrPRecibirServer != null)
            {
                // Procesar primer servidor
                RecibirJsonClienteArray = JsonConvert.DeserializeObject<List<JsonBikeMessengerCliente>>(LvrPRecibirServer); // resp será el string JSON a deserializa
                RecibirJsonCliente = RecibirJsonClienteArray[0];

                return RecibirJsonCliente.RESOPERACION == "OK";
            }
            return false;
        }


        private void CopiarMemoriaEnJson(string pOPERACION)
        {
            // Limpiar Variables

            EnviarJsonCliente.OPERACION = "";
            EnviarJsonCliente.PENTALPHA = "";
            EnviarJsonCliente.RUTID = "";
            EnviarJsonCliente.DIGVER = "";
            EnviarJsonCliente.NOMBRE = "";
            EnviarJsonCliente.USUARIO = "";
            EnviarJsonCliente.CLAVE = "";
            EnviarJsonCliente.ACTIVIDAD1 = "";
            EnviarJsonCliente.ACTIVIDAD2 = "";
            EnviarJsonCliente.REPRESENTANTE1 = "";
            EnviarJsonCliente.REPRESENTANTE2 = "";
            EnviarJsonCliente.REPRESENTANTE3 = "";
            EnviarJsonCliente.DOMICILIO1 = "";
            EnviarJsonCliente.DOMICILIO2 = "";
            EnviarJsonCliente.NUMERO = "";
            EnviarJsonCliente.PISO = "";
            EnviarJsonCliente.OFICINA = "";
            EnviarJsonCliente.CIUDAD = "";
            EnviarJsonCliente.COMUNA = "";
            EnviarJsonCliente.ESTADOREGION = "";
            EnviarJsonCliente.CODIGOPOSTAL = "";
            EnviarJsonCliente.PAIS = "";
            EnviarJsonCliente.OBSERVACIONES = "";
            EnviarJsonCliente.LOGO = "";
            EnviarJsonCliente.RESOPERACION = "";
            EnviarJsonCliente.RESMENSAJE = "";

            // Llenar Variables
            EnviarJsonCliente.OPERACION =  pOPERACION;
            EnviarJsonCliente.PENTALPHA = BM_Database_Cliente.BK_PENTALPHA;
            EnviarJsonCliente.RUTID = BM_Database_Cliente.BK_RUTID;
            EnviarJsonCliente.DIGVER = BM_Database_Cliente.BK_DIGVER;
            EnviarJsonCliente.NOMBRE = BM_Database_Cliente.BK_NOMBRE;
            EnviarJsonCliente.USUARIO = BM_Database_Cliente.BK_USUARIO;
            EnviarJsonCliente.CLAVE = BM_Database_Cliente.BK_CLAVE;
            EnviarJsonCliente.ACTIVIDAD1 = BM_Database_Cliente.BK_ACTIVIDAD1;
            EnviarJsonCliente.ACTIVIDAD2 = BM_Database_Cliente.BK_ACTIVIDAD2;
            EnviarJsonCliente.REPRESENTANTE1 = BM_Database_Cliente.BK_REPRESENTANTE1;
            EnviarJsonCliente.REPRESENTANTE2 = BM_Database_Cliente.BK_REPRESENTANTE2;
            EnviarJsonCliente.REPRESENTANTE3 = BM_Database_Cliente.BK_REPRESENTANTE3;
            EnviarJsonCliente.DOMICILIO1 = BM_Database_Cliente.BK_DOMICILIO1;
            EnviarJsonCliente.DOMICILIO2 = BM_Database_Cliente.BK_DOMICILIO2;
            EnviarJsonCliente.NUMERO = BM_Database_Cliente.BK_NUMERO;
            EnviarJsonCliente.PISO = BM_Database_Cliente.BK_PISO;
            EnviarJsonCliente.OFICINA = BM_Database_Cliente.BK_OFICINA;
            EnviarJsonCliente.CIUDAD = BM_Database_Cliente.BK_CIUDAD;
            EnviarJsonCliente.COMUNA = BM_Database_Cliente.BK_COMUNA;
            EnviarJsonCliente.ESTADOREGION = BM_Database_Cliente.BK_REGION;
            EnviarJsonCliente.CODIGOPOSTAL = BM_Database_Cliente.BK_CODIGOPOSTAL;
            EnviarJsonCliente.PAIS = BM_Database_Cliente.BK_PAIS;
            EnviarJsonCliente.OBSERVACIONES = BM_Database_Cliente.BK_OBSERVACIONES;
            EnviarJsonCliente.LOGO = BM_Database_Cliente.BK_FOTO;
            EnviarJsonCliente.RESOPERACION = "";
            EnviarJsonCliente.RESMENSAJE = "";
        }

        private void CopiarJsonEnMemoria(string pOPERACION)
        {
            // Proceso
            //EnviarJsonCliente.OPERACION = pOPERACION;
            BM_Database_Cliente.BK_PENTALPHA = EnviarJsonCliente.PENTALPHA;
            BM_Database_Cliente.BK_RUTID = EnviarJsonCliente.RUTID;
            BM_Database_Cliente.BK_DIGVER = EnviarJsonCliente.DIGVER;
            BM_Database_Cliente.BK_NOMBRE = EnviarJsonCliente.NOMBRE;
            BM_Database_Cliente.BK_USUARIO = EnviarJsonCliente.USUARIO;
            BM_Database_Cliente.BK_CLAVE = EnviarJsonCliente.CLAVE;
            BM_Database_Cliente.BK_ACTIVIDAD1 = EnviarJsonCliente.ACTIVIDAD1;
            BM_Database_Cliente.BK_ACTIVIDAD2 = EnviarJsonCliente.ACTIVIDAD2;
            BM_Database_Cliente.BK_REPRESENTANTE1 = EnviarJsonCliente.REPRESENTANTE1;
            BM_Database_Cliente.BK_REPRESENTANTE2 = EnviarJsonCliente.REPRESENTANTE2;
            BM_Database_Cliente.BK_REPRESENTANTE3 = EnviarJsonCliente.REPRESENTANTE3;
            BM_Database_Cliente.BK_DOMICILIO1 = EnviarJsonCliente.DOMICILIO1;
            BM_Database_Cliente.BK_DOMICILIO2 = EnviarJsonCliente.DOMICILIO2;
            BM_Database_Cliente.BK_NUMERO = EnviarJsonCliente.NUMERO;
            BM_Database_Cliente.BK_PISO = EnviarJsonCliente.PISO;
            BM_Database_Cliente.BK_OFICINA = EnviarJsonCliente.OFICINA;
            BM_Database_Cliente.BK_CIUDAD = EnviarJsonCliente.CIUDAD;
            BM_Database_Cliente.BK_COMUNA = EnviarJsonCliente.COMUNA;
            BM_Database_Cliente.BK_REGION = EnviarJsonCliente.ESTADOREGION;
            BM_Database_Cliente.BK_CODIGOPOSTAL = EnviarJsonCliente.CODIGOPOSTAL;
            BM_Database_Cliente.BK_PAIS = EnviarJsonCliente.PAIS;
            BM_Database_Cliente.BK_OBSERVACIONES = EnviarJsonCliente.OBSERVACIONES;
            BM_Database_Cliente.BK_FOTO = EnviarJsonCliente.LOGO;
            // EnviarJsonCliente.RESOPERACION = "";
            // EnviarJsonCliente.RESMENSAJE = "";
        }
    }

    internal class GridClientesIndividual
    {
        public string RUT { get; set; }
        public string CLIENTE { get; set; }
    }
}
