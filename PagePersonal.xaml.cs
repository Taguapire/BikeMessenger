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
    public sealed partial class PagePersonal : Page
    {
        private List<JsonBikeMessengerPersonal> PersonalIOArray = null;
        private JsonBikeMessengerPersonal PersonalIO = null;
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
                RellenarCombos();

                if (LvrTransferVar.P_RUTID == "")
                {
                    if ((PersonalIOArray = BM_Database_Personal.BuscarPersonal()) != null)
                    {
                        PersonalIO = PersonalIOArray[0];
                        LlenarPantallaConDb();
                    }
                }
                else
                {
                    if ((PersonalIOArray = BM_Database_Personal.BuscarPersonal(LvrTransferVar.P_PENTALPHA, LvrTransferVar.P_RUTID, LvrTransferVar.P_DIGVER)) != null)
                    {
                        PersonalIO = PersonalIOArray[0];
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
                using IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                // Set the image source to the selected bitmap.
                Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage();

                bitmapImage.SetSource(fileStream);
                imageFotoPersonal.Source = bitmapImage;
            }
        }

        private async void LlenarPantallaConDb()
        {
            try
            {
                LvrTransferVar.P_RUTID = PersonalIO.RUTID;
                LvrTransferVar.P_DIGVER = PersonalIO.DIGVER;
                textBoxRut.Text = PersonalIO.RUTID;
                textBoxDigitoVerificador.Text = PersonalIO.DIGVER;
                textBoxNombres.Text = PersonalIO.NOMBRES;
                textBoxApellidos.Text = PersonalIO.APELLIDOS;
                textBoxTelefono1.Text = PersonalIO.TELEFONO1;
                textBoxTelefono2.Text = PersonalIO.TELEFONO2;
                textBoxCorreoElectronico.Text = PersonalIO.EMAIL;
                comboBoxAutorizacion.SelectedValue = PersonalIO.AUTORIZACION;
                textBoxCargo.Text = PersonalIO.CARGO;
                textBoxDomicilio.Text = PersonalIO.DOMICILIO;
                textBoxNumero.Text = PersonalIO.NUMERO;
                textBoxPiso.Text = PersonalIO.PISO;
                textBoxDepartamento.Text = PersonalIO.DPTO;
                textBoxCodigoPostal.Text = PersonalIO.CODIGOPOSTAL;
                comboBoxPais.SelectedValue = PersonalIO.PAIS;
                comboBoxRegion.SelectedValue = PersonalIO.REGION;
                comboBoxComuna.SelectedValue = PersonalIO.COMUNA;
                comboBoxCiudad.SelectedValue = PersonalIO.CIUDAD;
                textBoxObservaciones.Text = PersonalIO.OBSERVACIONES;

                imageFotoPersonal.Source = Base64StringToBitmap(PersonalIO.FOTO);
            }
            catch (ArgumentNullException)
            {
                await AvisoOperacionPersonalDialogAsync("Acceso a Base de Datos Personal", "Error de carga de datos desde la Base de Datos.");
            }
        }

        private void RellenarCombos()
        {
            // Limpiar Combo Box
            comboBoxPais.Items.Clear();
            comboBoxRegion.Items.Clear();
            comboBoxComuna.Items.Clear();
            comboBoxCiudad.Items.Clear();

            // Llenar Combo Pais

            List<string> ListaPais = BM_Database_Personal.GetPais();

            if (ListaPais != null)
            {
                try
                {
                    for (int i = 0; i < ListaPais.Count; i++)
                    {
                        comboBoxPais.Items.Add(ListaPais[i]);
                    }
                }
                catch (System.NullReferenceException)
                {

                }
            }

            // Llenar Combo Region
            List<string> ListaEstado = BM_Database_Personal.GetRegion();

            if (ListaEstado != null)
            {
                try
                {
                    for (int i = 0; i < ListaEstado.Count; i++)
                    {
                        comboBoxRegion.Items.Add(ListaEstado[i]);
                    }
                }
                catch (System.NullReferenceException)
                {

                }
            }

            // Llenar Combo Comuna
            List<string> ListaComuna = BM_Database_Personal.GetComuna();

            if (ListaComuna != null)
            {
                try
                {
                    for (int i = 0; i < ListaComuna.Count; i++)
                    {
                        comboBoxComuna.Items.Add(ListaComuna[i]);
                    }
                }
                catch (System.NullReferenceException)
                {

                }
            }


            // Llenar Combo Ciudad
            List<string> ListaCiudad = BM_Database_Personal.GetCiudad();

            if (ListaCiudad != null)
            {
                try
                {
                    for (int i = 0; i < ListaCiudad.Count; i++)
                    {
                        comboBoxCiudad.Items.Add(ListaCiudad[i]);
                    }
                }
                catch (System.NullReferenceException)
                {

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
            PersonalIO.PENTALPHA = LvrTransferVar.P_PENTALPHA;
            PersonalIO.RUTID = textBoxRut.Text;
            PersonalIO.DIGVER = textBoxDigitoVerificador.Text;
            // ***********************************************************
            // Agregando Campos de Parametros
            LvrTransferVar.P_RUTID = PersonalIO.RUTID;
            LvrTransferVar.P_DIGVER = PersonalIO.DIGVER;
            // ***********************************************************
            PersonalIO.APELLIDOS = textBoxApellidos.Text;
            PersonalIO.NOMBRES = textBoxNombres.Text;
            PersonalIO.TELEFONO1 = textBoxTelefono1.Text;
            PersonalIO.TELEFONO2 = textBoxTelefono2.Text;
            PersonalIO.EMAIL = textBoxCorreoElectronico.Text;
            PersonalIO.AUTORIZACION = comboBoxAutorizacion.Text;
            PersonalIO.CARGO = textBoxCargo.Text;
            PersonalIO.DOMICILIO = textBoxDomicilio.Text;
            PersonalIO.NUMERO = textBoxNumero.Text;
            PersonalIO.PISO = textBoxPiso.Text;
            PersonalIO.DPTO = textBoxDepartamento.Text;
            PersonalIO.CODIGOPOSTAL = textBoxCodigoPostal.Text;
            if (comboBoxCiudad.Text != "")
            {
                PersonalIO.CIUDAD = comboBoxCiudad.Text;
            }

            if (comboBoxComuna.Text != "")
            {
                PersonalIO.COMUNA = comboBoxComuna.Text;
            }

            if (comboBoxRegion.Text != "")
            {
                PersonalIO.REGION = comboBoxRegion.Text;
            }

            if (comboBoxPais.Text != "")
            {
                PersonalIO.PAIS = comboBoxPais.Text;
            }

            PersonalIO.OBSERVACIONES = textBoxObservaciones.Text;
            PersonalIO.FOTO = await ConvertirImageABase64Async();
        }

        private async void BtnAgregarPersonal(object sender, RoutedEventArgs e)
        {
            // Muestra de espera
            bool TransaccionOK;

            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            await LlenarDbConPantallaAsync();


            if (BM_Database_Personal.AgregarPersonal(PersonalIO))
            {
                TransaccionOK = true;

                if (LvrTransferVar.SincronizarWebRemoto())
                {
                    TransaccionOK = ProRegistroPersonal("AGREGAR");
                }

                if (LvrTransferVar.SincronizarWebPropio())
                {
                    TransaccionOK = ProRegistroPersonal("AGREGAR");
                }

                if (TransaccionOK)
                {
                    await AvisoOperacionPersonalDialogAsync("Agregar Personal", "Personal agregado exitosamente.");
                    LlenarListaPersonal();
                    LvrTransferVar.P_RUTID = PersonalIO.RUTID;
                    LvrTransferVar.P_DIGVER = PersonalIO.DIGVER;
                }

                else
                {
                    await AvisoOperacionPersonalDialogAsync("Agregar Personal", "Error en ingreso de personal. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            else
            {
                await AvisoOperacionPersonalDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de personal.");
            }

            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }

        private async void BtnModificarPersonal(object sender, RoutedEventArgs e)
        {
            bool TransaccionOK;
            // Muestra de espera

            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            await LlenarDbConPantallaAsync();

            if (BM_Database_Personal.ModificarPersonal(PersonalIO))
            {
                TransaccionOK = true;

                if (LvrTransferVar.SincronizarWebRemoto())
                {
                    TransaccionOK = ProRegistroPersonal("MODIFICAR");
                }
                if (LvrTransferVar.SincronizarWebPropio())
                {
                    TransaccionOK = ProRegistroPersonal("MODIFICAR");
                }

                if (TransaccionOK)
                {
                    await AvisoOperacionPersonalDialogAsync("Modificar Personal", "Personal modificada exitosamente.");
                    LlenarListaPersonal();
                    LvrTransferVar.P_RUTID = PersonalIO.RUTID;
                    LvrTransferVar.P_DIGVER = PersonalIO.DIGVER;
                }
                else
                {
                    await AvisoOperacionPersonalDialogAsync("Modificar Personal", "Error en modificación de personal. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            else
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

            await LlenarDbConPantallaAsync();

            bool TransaccionOK = false;

            if (BM_Database_Personal.BorrarPersonal(LvrTransferVar.P_PENTALPHA, PersonalIO.RUTID, PersonalIO.DIGVER))
            {
                TransaccionOK = true;

                if (LvrTransferVar.SincronizarWebRemoto())
                {
                    TransaccionOK = ProRegistroPersonal("BORRAR");
                }

                if (LvrTransferVar.SincronizarWebPropio())
                {
                    TransaccionOK = ProRegistroPersonal("BORRAR");
                }


                if (TransaccionOK)
                {
                    if ((PersonalIOArray = BM_Database_Personal.BuscarPersonal()) != null)
                    {
                        PersonalIO = PersonalIOArray[0];
                        LvrTransferVar.P_RUTID = PersonalIO.RUTID;
                        LvrTransferVar.P_DIGVER = PersonalIO.DIGVER;
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
                    await AvisoOperacionPersonalDialogAsync("Borrar Personal", "Error en borrado de personal. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            else
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
            List<JsonBikeMessengerPersonal> GridPersonalDb = new List<JsonBikeMessengerPersonal>();
            List<GridPersonalIndividual> GridPersonalLista = new List<GridPersonalIndividual>();

            if ((GridPersonalDb = BM_Database_Personal.BuscarGridPersonal()) != null)
            {
                for (int i = 0; i < GridPersonalDb.Count; i++)
                {
                    GridPersonalLista.Add(
                        new GridPersonalIndividual
                        {
                            RUT = GridPersonalDb[i].RUTID + "-" + GridPersonalDb[i].DIGVER,
                            APELLIDO = GridPersonalDb[i].APELLIDOS,
                            NOMBRE = GridPersonalDb[i].NOMBRES
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

                if ((PersonalIOArray = BM_Database_Personal.BuscarPersonal(LvrTransferVar.P_PENTALPHA, CadenaDividida[0], CadenaDividida[1])) != null)
                {
                    PersonalIO = PersonalIOArray[0];
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
            EnviarJsonPersonal.PENTALPHA = PersonalIO.PENTALPHA;
            EnviarJsonPersonal.RUTID = PersonalIO.RUTID;
            EnviarJsonPersonal.DIGVER = PersonalIO.DIGVER;
            EnviarJsonPersonal.APELLIDOS = PersonalIO.APELLIDOS;
            EnviarJsonPersonal.NOMBRES = PersonalIO.NOMBRES;
            EnviarJsonPersonal.TELEFONO1 = PersonalIO.TELEFONO1;
            EnviarJsonPersonal.TELEFONO2 = PersonalIO.TELEFONO2;
            EnviarJsonPersonal.EMAIL = PersonalIO.EMAIL;
            EnviarJsonPersonal.AUTORIZACION = PersonalIO.AUTORIZACION;
            EnviarJsonPersonal.CARGO = PersonalIO.CARGO;
            EnviarJsonPersonal.DOMICILIO = PersonalIO.DOMICILIO;
            EnviarJsonPersonal.NUMERO = PersonalIO.NUMERO;
            EnviarJsonPersonal.PISO = PersonalIO.PISO;
            EnviarJsonPersonal.DPTO = PersonalIO.DPTO;
            EnviarJsonPersonal.CODIGOPOSTAL = PersonalIO.CODIGOPOSTAL;
            EnviarJsonPersonal.CIUDAD = PersonalIO.CIUDAD;
            EnviarJsonPersonal.COMUNA = PersonalIO.COMUNA;
            EnviarJsonPersonal.REGION = PersonalIO.REGION;
            EnviarJsonPersonal.PAIS = PersonalIO.PAIS;
            EnviarJsonPersonal.OBSERVACIONES = PersonalIO.OBSERVACIONES;
            EnviarJsonPersonal.FOTO = PersonalIO.FOTO;
            EnviarJsonPersonal.RESOPERACION = "";
            EnviarJsonPersonal.RESMENSAJE = "";
        }

        private void CopiarJsonEnMemoria(string pOPERACION)
        {
            // Proceso
            // EnviarJsonPersonal.OPERACION = pOPERACION;
            PersonalIO.PENTALPHA = EnviarJsonPersonal.PENTALPHA;
            PersonalIO.RUTID = EnviarJsonPersonal.RUTID;
            PersonalIO.DIGVER = EnviarJsonPersonal.DIGVER;
            PersonalIO.APELLIDOS = EnviarJsonPersonal.APELLIDOS;
            PersonalIO.NOMBRES = EnviarJsonPersonal.NOMBRES;
            PersonalIO.TELEFONO1 = EnviarJsonPersonal.TELEFONO1;
            PersonalIO.TELEFONO2 = EnviarJsonPersonal.TELEFONO2;
            PersonalIO.EMAIL = EnviarJsonPersonal.EMAIL;
            PersonalIO.AUTORIZACION = EnviarJsonPersonal.AUTORIZACION;
            PersonalIO.CARGO = EnviarJsonPersonal.CARGO;
            PersonalIO.DOMICILIO = EnviarJsonPersonal.DOMICILIO;
            PersonalIO.NUMERO = EnviarJsonPersonal.NUMERO;
            PersonalIO.PISO = EnviarJsonPersonal.PISO;
            PersonalIO.DPTO = EnviarJsonPersonal.DPTO;
            PersonalIO.CODIGOPOSTAL = EnviarJsonPersonal.CODIGOPOSTAL;
            PersonalIO.CIUDAD = EnviarJsonPersonal.CIUDAD;
            PersonalIO.COMUNA = EnviarJsonPersonal.COMUNA;
            PersonalIO.REGION = EnviarJsonPersonal.REGION;
            PersonalIO.PAIS = EnviarJsonPersonal.PAIS;
            PersonalIO.OBSERVACIONES = EnviarJsonPersonal.OBSERVACIONES;
            PersonalIO.FOTO = EnviarJsonPersonal.FOTO;
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
