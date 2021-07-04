using Microsoft.Toolkit.Uwp.UI.Controls;
using Newtonsoft.Json;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageClientes : Page
    {
        private List<JsonBikeMessengerCliente> ClienteIOArray = new List<JsonBikeMessengerCliente>();
        private JsonBikeMessengerCliente ClienteIO = new JsonBikeMessengerCliente();
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
                RellenarCombos();

                if (LvrTransferVar.CLI_RUTID == "")
                {
                    ClienteIOArray = BM_Database_Cliente.BuscarCliente();
                    if (ClienteIOArray != null && ClienteIOArray.Count > 0)
                    {
                        ClienteIO = ClienteIOArray[0];
                        LlenarPantallaConDb();
                    }
                }
                else
                {
                    ClienteIOArray = BM_Database_Cliente.BuscarCliente(LvrTransferVar.CLI_PENTALPHA, LvrTransferVar.CLI_RUTID, LvrTransferVar.CLI_DIGVER);
                    if (ClienteIOArray != null && ClienteIOArray.Count > 0)
                    {
                        ClienteIO = ClienteIOArray[0];
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
                // LvrTransferVar.REC_PENTALPHA = BM_Database_Recurso.BK_PENTALPHA;
                textBoxRut.Text = ClienteIO.RUTID;
                textBoxDigitoVerificador.Text = ClienteIO.DIGVER;
                textBoxNombreCliente.Text = ClienteIO.NOMBRE;
                textBoxUsuario.Text = ClienteIO.USUARIO;
                passwordClave.Password = ClienteIO.CLAVE;
                textBoxActividad1.Text = ClienteIO.ACTIVIDAD1;
                textBoxActividad2.Text = ClienteIO.ACTIVIDAD2;
                textBoxRepresentantes1.Text = ClienteIO.REPRESENTANTE1;
                textBoxRepresentantes2.Text = ClienteIO.REPRESENTANTE2;
                textBoxEMail.Text = ClienteIO.EMAIL;
                textBoxTelefono1.Text = ClienteIO.TELEFONO1;
                textBoxTelefono2.Text = ClienteIO.TELEFONO2;
                textBoxDomicilio1.Text = ClienteIO.DOMICILIO1;
                textBoxDomicilio2.Text = ClienteIO.DOMICILIO2;
                textBoxNumero.Text = ClienteIO.NUMERO;
                textBoxPiso.Text = ClienteIO.PISO;
                textBoxOficina.Text = ClienteIO.OFICINA;
                textBoxCodigoPostal.Text = ClienteIO.CODIGOPOSTAL;
                textBoxObservaciones.Text = ClienteIO.OBSERVACIONES;

                comboBoxPais.SelectedValue = ClienteIO.PAIS;
                comboBoxEstado.SelectedValue = ClienteIO.ESTADOREGION;
                comboBoxComuna.SelectedValue = ClienteIO.COMUNA;
                comboBoxCiudad.SelectedValue = ClienteIO.CIUDAD;

                textBoxObservaciones.Text = ClienteIO.OBSERVACIONES;

                imageLogoCliente.Source = Base64StringToBitmap(ClienteIO.LOGO);
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
            List<string> ListaPais = BM_Database_Cliente.GetPais();
            for (int i = 0; i < ListaPais.Count; i++)
            {
                comboBoxPais.Items.Add(ListaPais[i]);
            }

            // Llenar Combo Region
            List<string> ListaEstado = BM_Database_Cliente.GetRegion();
            for (int i = 0; i < ListaEstado.Count; i++)
            {
                comboBoxEstado.Items.Add(ListaEstado[i]);
            }

            // Llenar Combo Comuna
            List<string> ListaComuna = BM_Database_Cliente.GetComuna();
            for (int i = 0; i < ListaComuna.Count; i++)
            {
                comboBoxComuna.Items.Add(ListaComuna[i]);
            }

            // Llenar Combo Ciudad
            List<string> ListaCiudad = BM_Database_Cliente.GetCiudad();
            for (int i = 0; i < ListaCiudad.Count; i++)
            {
                comboBoxCiudad.Items.Add(ListaCiudad[i]);
            }
        }

        private void LimpiarPantalla()
        {
            LvrTransferVar.CLI_RUTID = "";
            LvrTransferVar.CLI_DIGVER = "";
            textBoxRut.Text = "";
            textBoxDigitoVerificador.Text = "";
            textBoxNombreCliente.Text = "";
            textBoxUsuario.Text = "";
            passwordClave.Password = "";
            textBoxActividad1.Text = "";
            textBoxActividad2.Text = "";
            textBoxRepresentantes1.Text = "";
            textBoxRepresentantes2.Text = "";
            textBoxEMail.Text = "";
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
            ClienteIO.PENTALPHA = LvrTransferVar.CLI_PENTALPHA;
            ClienteIO.RUTID = textBoxRut.Text;
            ClienteIO.DIGVER = textBoxDigitoVerificador.Text;
            ClienteIO.NOMBRE = textBoxNombreCliente.Text;
            ClienteIO.USUARIO = textBoxUsuario.Text;
            ClienteIO.CLAVE = passwordClave.Password;
            ClienteIO.ACTIVIDAD1 = textBoxActividad1.Text;
            ClienteIO.ACTIVIDAD2 = textBoxActividad2.Text;
            ClienteIO.REPRESENTANTE1 = textBoxRepresentantes1.Text;
            ClienteIO.REPRESENTANTE2 = textBoxRepresentantes2.Text;
            ClienteIO.EMAIL = textBoxEMail.Text;
            ClienteIO.TELEFONO1 = textBoxTelefono1.Text;
            ClienteIO.TELEFONO2 = textBoxTelefono2.Text;
            ClienteIO.DOMICILIO1 = textBoxDomicilio1.Text;
            ClienteIO.DOMICILIO2 = textBoxDomicilio2.Text;
            ClienteIO.NUMERO = textBoxNumero.Text;
            ClienteIO.PISO = textBoxPiso.Text;
            ClienteIO.OFICINA = textBoxOficina.Text;
            ClienteIO.CODIGOPOSTAL = textBoxCodigoPostal.Text;
            if (comboBoxPais.Text != "")
            {
                ClienteIO.PAIS = comboBoxPais.Text;
            }

            if (comboBoxEstado.Text != "")
            {
                ClienteIO.ESTADOREGION = comboBoxEstado.Text;
            }

            if (comboBoxComuna.Text != "")
            {
                ClienteIO.COMUNA = comboBoxComuna.Text;
            }

            if (comboBoxCiudad.Text != "")
            {
                ClienteIO.CIUDAD = comboBoxCiudad.Text;
            }

            ClienteIO.OBSERVACIONES = textBoxObservaciones.Text;
            ClienteIO.LOGO = await ConvertirImageABase64Async();
        }

        private async void BtnAgregarClientes(object sender, RoutedEventArgs e)
        {
            // Muestra de espera
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            await LlenarDbConPantallaAsync();

            bool TransaccionOK = false;

            if (BM_Database_Cliente.AgregarCliente(ClienteIO))
            {
                TransaccionOK = true;

                if (LvrTransferVar.SincronizarWebRemoto())
                {
                    TransaccionOK = ProRegistroCliente("AGREGAR");
                }

                if (LvrTransferVar.SincronizarWebPropio())
                {
                    TransaccionOK = ProRegistroCliente("AGREGAR");
                }

                if (TransaccionOK)
                {
                    await AvisoOperacionClientesDialogAsync("Agregar Cliente", "Cliente agregado exitosamente.");
                    LlenarListaClientes();
                    LvrTransferVar.CLI_RUTID = ClienteIO.RUTID;
                    LvrTransferVar.CLI_DIGVER = ClienteIO.DIGVER;
                }

                else
                {
                    await AvisoOperacionClientesDialogAsync("Agregar Cliente", "Error en ingreso de Cliente. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            else
            {
                await AvisoOperacionClientesDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de Cliente.");
            }

            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }


        private async void BtnModificarClientes(object sender, RoutedEventArgs e)
        {
            // Muestra de espera
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            await LlenarDbConPantallaAsync();

            bool TransaccionOK = false;

            if (BM_Database_Cliente.ModificarCliente(ClienteIO))
            {
                TransaccionOK = true;

                if (LvrTransferVar.SincronizarWebRemoto())
                {
                    TransaccionOK = ProRegistroCliente("MODIFICAR");
                }

                if (LvrTransferVar.SincronizarWebPropio())
                {
                    TransaccionOK = ProRegistroCliente("MODIFICAR");
                }

                if (TransaccionOK)
                {
                    await AvisoOperacionClientesDialogAsync("Modificar Personal", "Personal modificada exitosamente.");
                    LlenarListaClientes();
                    LvrTransferVar.CLI_RUTID = ClienteIO.RUTID;
                    LvrTransferVar.CLI_DIGVER = ClienteIO.DIGVER;
                }
                else
                {
                    await AvisoOperacionClientesDialogAsync("Modificar Cliente", "Error en modificación de Cliente. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            else
            {
                await AvisoOperacionClientesDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de la personal.");
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

            await LlenarDbConPantallaAsync();

            bool TransaccionOK = false;

            if (BM_Database_Cliente.BorrarCliente(LvrTransferVar.CLI_PENTALPHA, ClienteIO.RUTID, ClienteIO.DIGVER))
            {
                TransaccionOK = true;

                if (LvrTransferVar.SincronizarWebRemoto())
                {
                    TransaccionOK = ProRegistroCliente("BORRAR");
                }

                if (LvrTransferVar.SincronizarWebPropio())
                {
                    TransaccionOK = ProRegistroCliente("BORRAR");
                }


                if (TransaccionOK)
                {
                    if ((ClienteIOArray = BM_Database_Cliente.BuscarCliente()) != null)
                    {
                        ClienteIO = ClienteIOArray[0];
                        LvrTransferVar.CLI_RUTID = ClienteIO.RUTID;
                        LvrTransferVar.CLI_DIGVER = ClienteIO.DIGVER;
                    }
                    else
                    {
                        LvrTransferVar.CLI_RUTID = "";
                        LvrTransferVar.CLI_DIGVER = "";
                    }
                    LlenarPantallaConDb();
                    LlenarListaClientes();
                    await AvisoOperacionClientesDialogAsync("Borrar Cliente", "Cliente borrado exitosamente.");
                }
                else
                {
                    await AvisoOperacionClientesDialogAsync("Borrar Cliente", "Error en borrado de Cliente. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            else
            {
                await AvisoOperacionClientesDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de Cliente.");
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
            List<ClaseClientesGrid> GridClienteDb = new List<ClaseClientesGrid>();
            List<GridClientesIndividual> GridClienteLista = new List<GridClientesIndividual>();

            if ((GridClienteDb = BM_Database_Cliente.BuscarGridClientes()) != null)
            {
                for (int i = 0; i < GridClienteDb.Count; i++)
                {
                    GridClienteLista.Add(
                        new GridClientesIndividual
                        {
                            RUT = GridClienteDb[i].RUTID + "-" + GridClienteDb[i].DIGVER,
                            CLIENTE = GridClienteDb[i].NOMBRE
                        });
                }
            }
            dataGridListadoClientes.IsReadOnly = true;
            dataGridListadoClientes.ItemsSource = GridClienteLista;
        }

        private void DataGridSeleccion_Clientes(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid CeldaSeleccionada = sender as DataGrid;
                GridClientesIndividual Fila = (GridClientesIndividual)CeldaSeleccionada.SelectedItems[0];
                string[] CadenaDividida = Fila.RUT.Split("-", 2, StringSplitOptions.None);

                if ((ClienteIOArray = BM_Database_Cliente.BuscarCliente(LvrTransferVar.CLI_PENTALPHA, CadenaDividida[0], CadenaDividida[1])) != null)
                {
                    ClienteIO = ClienteIOArray[0];
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
            // Llenar Variables
            EnviarJsonCliente.OPERACION = pOPERACION;
            EnviarJsonCliente.PENTALPHA = ClienteIO.PENTALPHA;
            EnviarJsonCliente.RUTID = ClienteIO.RUTID;
            EnviarJsonCliente.DIGVER = ClienteIO.DIGVER;
            EnviarJsonCliente.NOMBRE = ClienteIO.NOMBRE;
            EnviarJsonCliente.USUARIO = ClienteIO.USUARIO;
            EnviarJsonCliente.CLAVE = ClienteIO.CLAVE;
            EnviarJsonCliente.ACTIVIDAD1 = ClienteIO.ACTIVIDAD1;
            EnviarJsonCliente.ACTIVIDAD2 = ClienteIO.ACTIVIDAD2;
            EnviarJsonCliente.REPRESENTANTE1 = ClienteIO.REPRESENTANTE1;
            EnviarJsonCliente.REPRESENTANTE2 = ClienteIO.REPRESENTANTE2;
            EnviarJsonCliente.EMAIL = ClienteIO.EMAIL;
            EnviarJsonCliente.DOMICILIO1 = ClienteIO.DOMICILIO1;
            EnviarJsonCliente.DOMICILIO2 = ClienteIO.DOMICILIO2;
            EnviarJsonCliente.NUMERO = ClienteIO.NUMERO;
            EnviarJsonCliente.PISO = ClienteIO.PISO;
            EnviarJsonCliente.OFICINA = ClienteIO.OFICINA;
            EnviarJsonCliente.CIUDAD = ClienteIO.CIUDAD;
            EnviarJsonCliente.COMUNA = ClienteIO.COMUNA;
            EnviarJsonCliente.ESTADOREGION = ClienteIO.ESTADOREGION;
            EnviarJsonCliente.CODIGOPOSTAL = ClienteIO.CODIGOPOSTAL;
            EnviarJsonCliente.PAIS = ClienteIO.PAIS;
            EnviarJsonCliente.OBSERVACIONES = ClienteIO.OBSERVACIONES;
            EnviarJsonCliente.LOGO = ClienteIO.LOGO;
            EnviarJsonCliente.RESOPERACION = "";
            EnviarJsonCliente.RESMENSAJE = "";
        }

        private void CopiarJsonEnMemoria(string pOPERACION)
        {
            // Proceso
            //EnviarJsonCliente.OPERACION = pOPERACION;
            ClienteIO.PENTALPHA = EnviarJsonCliente.PENTALPHA;
            ClienteIO.RUTID = EnviarJsonCliente.RUTID;
            ClienteIO.DIGVER = EnviarJsonCliente.DIGVER;
            ClienteIO.NOMBRE = EnviarJsonCliente.NOMBRE;
            ClienteIO.USUARIO = EnviarJsonCliente.USUARIO;
            ClienteIO.CLAVE = EnviarJsonCliente.CLAVE;
            ClienteIO.ACTIVIDAD1 = EnviarJsonCliente.ACTIVIDAD1;
            ClienteIO.ACTIVIDAD2 = EnviarJsonCliente.ACTIVIDAD2;
            ClienteIO.REPRESENTANTE1 = EnviarJsonCliente.REPRESENTANTE1;
            ClienteIO.REPRESENTANTE2 = EnviarJsonCliente.REPRESENTANTE2;
            ClienteIO.EMAIL = EnviarJsonCliente.EMAIL;
            ClienteIO.DOMICILIO1 = EnviarJsonCliente.DOMICILIO1;
            ClienteIO.DOMICILIO2 = EnviarJsonCliente.DOMICILIO2;
            ClienteIO.NUMERO = EnviarJsonCliente.NUMERO;
            ClienteIO.PISO = EnviarJsonCliente.PISO;
            ClienteIO.OFICINA = EnviarJsonCliente.OFICINA;
            ClienteIO.CIUDAD = EnviarJsonCliente.CIUDAD;
            ClienteIO.COMUNA = EnviarJsonCliente.COMUNA;
            ClienteIO.ESTADOREGION = EnviarJsonCliente.ESTADOREGION;
            ClienteIO.CODIGOPOSTAL = EnviarJsonCliente.CODIGOPOSTAL;
            ClienteIO.PAIS = EnviarJsonCliente.PAIS;
            ClienteIO.OBSERVACIONES = EnviarJsonCliente.OBSERVACIONES;
            ClienteIO.LOGO = EnviarJsonCliente.LOGO;
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
