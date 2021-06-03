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
    public sealed partial class PageRecursos : Page
    {
        private readonly JsonBikeMessengerRecurso EnviarJsonRecurso = new JsonBikeMessengerRecurso();
        private JsonBikeMessengerRecurso RecibirJsonRecurso = new JsonBikeMessengerRecurso();
        private readonly Bm_Recurso_Database BM_Database_Recurso = new Bm_Recurso_Database();
        private TransferVar LvrTransferVar;
        private bool BorrarSiNo;

        public PageRecursos()
        {
            InitializeComponent();
            comboBoxTipo.Items.Add("BICICLETA");
            comboBoxTipo.Items.Add("BICIMOTO");
            comboBoxTipo.Items.Add("MOTOCICLETA");
            comboBoxTipo.Items.Add("AUTOMOVIL");
            comboBoxTipo.Items.Add("FURGON");
            comboBoxTipo.Items.Add("CAMBION");
            comboBoxTipo.Items.Add("VEHICULO MARITIMO");
            comboBoxTipo.Items.Add("VEHICULO AEREO");
            comboBoxTipo.Items.Add("OTROS");
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
                _ = BM_Database_Recurso.BM_CreateDatabase(LvrTransferVar.TV_Connection);
                RellenarCombos();

                if (LvrTransferVar.R_PAT_SER == "")
                {
                    if (BM_Database_Recurso.Bm_Recursos_Buscar())
                    {
                        LlenarPantallaConDb();
                    }
                }
                else
                {
                    if (BM_Database_Recurso.Bm_Recursos_Buscar(LvrTransferVar.R_PENTALPHA, LvrTransferVar.R_PAT_SER))
                    {
                        LlenarPantallaConDb();
                    }
                }
                LlenarListaRecursos();
                LlenarListaPropietarios();
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
            // _ = Frame.Navigate(typeof(PageRecursos), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarEmpresa(object sender, RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(PageEmpresa), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarPersonal(object sender, RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(PagePersonal), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private async void BtnRecursosCargarFoto(object sender, RoutedEventArgs e)
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
                    imageFotoRecurso.Source = bitmapImage;
                }
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
            if (BM_Database_Recurso.Bm_E_Pais_EjecutarSelect())
            {
                while (BM_Database_Recurso.Bm_E_Pais_Buscar())
                {
                    comboBoxPais.Items.Add(BM_Database_Recurso.BK_E_PAIS);
                }
            }

            // Llenar Combo Region
            if (BM_Database_Recurso.Bm_E_Region_EjecutarSelect())
            {
                while (BM_Database_Recurso.Bm_E_Region_Buscar())
                {
                    comboBoxEstado.Items.Add(BM_Database_Recurso.BK_E_REGION);
                }
            }

            // Llenar Combo Comuna
            if (BM_Database_Recurso.Bm_E_Comuna_EjecutarSelect())
            {
                while (BM_Database_Recurso.Bm_E_Comuna_Buscar())
                {
                    comboBoxComuna.Items.Add(BM_Database_Recurso.BK_E_COMUNA);
                }
            }

            // Llenar Combo Ciudad
            if (BM_Database_Recurso.Bm_E_Ciudad_EjecutarSelect())
            {
                while (BM_Database_Recurso.Bm_E_Ciudad_Buscar())
                {
                    comboBoxCiudad.Items.Add(BM_Database_Recurso.BK_E_CIUDAD);
                }
            }
        }

        private async void LlenarPantallaConDb()
        {
            try
            {
                // LvrTransferVar.R_PENTALPHA = BM_Database_Recurso.BK_PENTALPHA;
                LvrTransferVar.R_RUTID = BM_Database_Recurso.BK_RUTID;
                LvrTransferVar.R_DIGVER = BM_Database_Recurso.BK_DIGVER;
                LvrTransferVar.R_PAT_SER = BM_Database_Recurso.BK_PATENTE;
                textBoxRut.Text = BM_Database_Recurso.BK_RUTID;
                textBoxDigitoVerificador.Text = BM_Database_Recurso.BK_DIGVER;
                textBoxPropietario.Text = BM_Database_Recurso.BK_PROPIETARIO;
                comboBoxTipo.SelectedValue = BM_Database_Recurso.BK_TIPO;
                textBoxPatenteCodigo.Text = BM_Database_Recurso.BK_PATENTE;
                textBoxMarca.Text = BM_Database_Recurso.BK_MARCA;
                textBoxModelo.Text = BM_Database_Recurso.BK_MODELO;
                textBoxVariante.Text = BM_Database_Recurso.BK_VARIANTE;
                textBoxAno.Text = BM_Database_Recurso.BK_ANO;
                textBoxColor.Text = BM_Database_Recurso.BK_COLOR;
                comboBoxPais.SelectedValue = BM_Database_Recurso.BK_PAIS;
                comboBoxEstado.SelectedValue = BM_Database_Recurso.BK_REGION;
                comboBoxComuna.SelectedValue = BM_Database_Recurso.BK_COMUNA;
                comboBoxCiudad.SelectedValue = BM_Database_Recurso.BK_CIUDAD;
                textBoxObservaciones.Text = BM_Database_Recurso.BK_OBSERVACIONES;
                imageFotoRecurso.Source = Base64StringToBitmap(BM_Database_Recurso.BK_FOTO);
            }
            catch (ArgumentNullException)
            {
                await AvisoOperacionRecursosDialogAsync("Acceso a Base de Datos Recurso", "Error de carga de datos desde la Base de Datos.");
            }
        }

        private void LimpiarPantalla()
        {
            LvrTransferVar.R_RUTID = "";
            LvrTransferVar.R_DIGVER = "";
            LvrTransferVar.R_PAT_SER = "";
            textBoxPatenteCodigo.Text = "";
            textBoxRut.Text = "";
            textBoxDigitoVerificador.Text = "";
            textBoxPropietario.Text = "";
            comboBoxTipo.Text = "";
            textBoxMarca.Text = "";
            textBoxModelo.Text = "";
            textBoxVariante.Text = "";
            textBoxAno.Text = "";
            textBoxColor.Text = "";
            comboBoxCiudad.Text = "";
            comboBoxComuna.Text = "";
            comboBoxEstado.Text = "";
            comboBoxPais.Text = "";
            textBoxObservaciones.Text = "";
            imageFotoRecurso.Source = Base64StringToBitmap("");
        }

        private async Task LlenarDbConPantallaAsync()
        {
            BM_Database_Recurso.BK_PENTALPHA = LvrTransferVar.R_PENTALPHA;
            BM_Database_Recurso.BK_PATENTE = textBoxPatenteCodigo.Text;
            BM_Database_Recurso.BK_RUTID = textBoxRut.Text;
            BM_Database_Recurso.BK_DIGVER = textBoxDigitoVerificador.Text;
            // ***********************************************************
            // Agregando Campos de Parametros
            LvrTransferVar.R_PAT_SER = BM_Database_Recurso.BK_PATENTE;
            LvrTransferVar.R_RUTID = BM_Database_Recurso.BK_RUTID;
            LvrTransferVar.R_DIGVER = BM_Database_Recurso.BK_DIGVER;
            // ***********************************************************
            BM_Database_Recurso.BK_PROPIETARIO = textBoxPropietario.Text;
            BM_Database_Recurso.BK_TIPO = comboBoxTipo.Text;
            BM_Database_Recurso.BK_MARCA = textBoxMarca.Text;
            BM_Database_Recurso.BK_MODELO = textBoxModelo.Text;
            BM_Database_Recurso.BK_VARIANTE = textBoxVariante.Text;
            BM_Database_Recurso.BK_ANO = textBoxAno.Text;
            BM_Database_Recurso.BK_COLOR = textBoxColor.Text;
            if (comboBoxCiudad.Text != "")
                BM_Database_Recurso.BK_CIUDAD = comboBoxCiudad.Text;
            if (comboBoxComuna.Text != "")
                BM_Database_Recurso.BK_COMUNA = comboBoxComuna.Text;
            if (comboBoxEstado.Text != "")
                BM_Database_Recurso.BK_REGION = comboBoxEstado.Text;
            if (comboBoxPais.Text != "")
                BM_Database_Recurso.BK_PAIS = comboBoxPais.Text;
            BM_Database_Recurso.BK_OBSERVACIONES = textBoxObservaciones.Text;
            BM_Database_Recurso.BK_FOTO = await ConvertirImageABase64Async();
        }

        private async void BtnAgregarRecursos(object sender, RoutedEventArgs e)
        {            
            // Muestra de espera
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            try
            {
                await LlenarDbConPantallaAsync();

                bool TransaccionOK = false;

                if (BM_Database_Recurso.Bm_Iniciar_Transaccion())
                {
                    if (BM_Database_Recurso.Bm_Recursos_Agregar())
                    {
                        TransaccionOK = true;

                    }
                    if (TransaccionOK)
                    {
                        if (LvrTransferVar.SincronizarWeb())
                        {
                            TransaccionOK = ProRegistroRecurso("AGREGAR");
                        }
                    }
                }
                if (TransaccionOK)
                {
                    _ = BM_Database_Recurso.Bm_Commit_Transaccion();
                    await AvisoOperacionRecursosDialogAsync("Agregar Recurso", "Recurso agregado exitosamente.");
                    LlenarListaRecursos();
                    LlenarListaPropietarios();
                    LvrTransferVar.R_RUTID = BM_Database_Recurso.BK_RUTID;
                    LvrTransferVar.R_DIGVER = BM_Database_Recurso.BK_DIGVER;
                    LvrTransferVar.R_PAT_SER = BM_Database_Recurso.BK_PATENTE;
                }
                else
                {
                    _ = BM_Database_Recurso.Bm_Rollback_Transaccion();
                    await AvisoOperacionRecursosDialogAsync("Agregar Recurso", "Error en ingreso de recurso. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            catch (ArgumentException)
            {
                await AvisoOperacionRecursosDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de recurso.");
            }

            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }

        private async void BtnModificarRecursos(object sender, RoutedEventArgs e)
        {
            // Muestra de espera
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            try
            {
                await LlenarDbConPantallaAsync();

                bool TransaccionOK = false;

                if (BM_Database_Recurso.Bm_Iniciar_Transaccion())
                {
                    if (BM_Database_Recurso.Bm_Recursos_Modificar(LvrTransferVar.R_PENTALPHA, BM_Database_Recurso.BK_PATENTE))
                    {
                        TransaccionOK = true;
                    }

                    if (TransaccionOK)
                    {
                        if (LvrTransferVar.SincronizarWeb())
                        {
                            TransaccionOK = ProRegistroRecurso("MODIFICAR");
                        }
                    }
                }

                if (TransaccionOK)
                {
                    _ = BM_Database_Recurso.Bm_Commit_Transaccion();
                    LlenarListaRecursos();
                    LlenarListaPropietarios();
                    LvrTransferVar.R_PAT_SER = BM_Database_Recurso.BK_PATENTE;
                    LvrTransferVar.R_RUTID = BM_Database_Recurso.BK_RUTID;
                    LvrTransferVar.R_DIGVER = BM_Database_Recurso.BK_DIGVER;
                }
                else
                {
                    _ = BM_Database_Recurso.Bm_Rollback_Transaccion();
                    await AvisoOperacionRecursosDialogAsync("Modificar Recurso", "Error en modificación de recurso. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            catch (ArgumentException)
            {
                await AvisoOperacionRecursosDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de recursos.");
            }

            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }


        private async void BtnBorrarRecursos(object sender, RoutedEventArgs e)
        {

            BorrarSiNo = false;

            await AvisoBorrarRecursoDialogAsync();

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

                if (BM_Database_Recurso.Bm_Iniciar_Transaccion())
                {
                    if (BM_Database_Recurso.Bm_Recursos_Borrar(LvrTransferVar.R_PENTALPHA, BM_Database_Recurso.BK_PATENTE))
                    {
                        TransaccionOK = true;
                    }

                    if (TransaccionOK)
                    {
                        if (LvrTransferVar.SincronizarWeb())
                        {
                            TransaccionOK = ProRegistroRecurso("BORRAR");
                        }
                    }
                }

                if (TransaccionOK)
                {
                    _ = BM_Database_Recurso.Bm_Commit_Transaccion();
                    if (BM_Database_Recurso.Bm_Recursos_Buscar())
                    {
                        LvrTransferVar.R_PAT_SER = BM_Database_Recurso.BK_PATENTE;
                        LvrTransferVar.R_RUTID = BM_Database_Recurso.BK_RUTID;
                        LvrTransferVar.R_DIGVER = BM_Database_Recurso.BK_DIGVER;
                    }
                    else
                    {
                        LvrTransferVar.R_RUTID = "";
                        LvrTransferVar.R_DIGVER = "";
                        LvrTransferVar.R_PAT_SER = "";
                    }
                    LlenarPantallaConDb();
                    LlenarListaRecursos();
                    LlenarListaPropietarios();
                    await AvisoOperacionRecursosDialogAsync("Borrar Recurso", "Recurso borrado exitosamente.");
                }
                else
                {
                    _ = BM_Database_Recurso.Bm_Rollback_Transaccion();
                    await AvisoOperacionRecursosDialogAsync("Borrar Recurso", "Error en borrado de recurso. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            catch (ArgumentException)
            {
                await AvisoOperacionRecursosDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de recurso.");
            }
            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }

        private void BtnSalirRecursos(object sender, RoutedEventArgs e)
        {
            LvrTransferVar.TV_Connection.Close();
            Application.Current.Exit();
        }

        private async Task<string> ConvertirImageABase64Async()
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(imageFotoRecurso);

            byte[] image = (await bitmap.GetPixelsAsync()).ToArray();
            uint width = (uint)bitmap.PixelWidth;
            uint height = (uint)bitmap.PixelHeight;

            double dpiX = 96;
            double dpiY = 96;

            InMemoryRandomAccessStream encoded = new InMemoryRandomAccessStream();
            BitmapEncoder encoder = await BitmapEncoder.CreateAsync(Windows.Graphics.Imaging.BitmapEncoder.PngEncoderId, encoded);

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

        private async Task AvisoBorrarRecursoDialogAsync()
        {
            ContentDialog AvisoConfirmacionRecursoDialog = new ContentDialog
            {
                Title = "Borrar Recurso",
                Content = "Confirme borrado del registro actual!",
                PrimaryButtonText = "Borrar",
                CloseButtonText = "Cancelar"
            };

            ContentDialogResult result = await AvisoConfirmacionRecursoDialog.ShowAsync();

            BorrarSiNo = result == ContentDialogResult.Primary;
        }

        private void DataGridSeleccion_Propietario(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid CeldaSeleccionada = sender as DataGrid;
                GridPropietarioIndividual Fila = (GridPropietarioIndividual)CeldaSeleccionada.SelectedItems[0];
                string[] CadenaDividida = Fila.RUT.Split("-", 2, StringSplitOptions.None);

                textBoxRut.Text = CadenaDividida[0];
                textBoxDigitoVerificador.Text = CadenaDividida[1];
                textBoxPropietario.Text = Fila.APELLIDO + ", " + Fila.NOMBRE;
            }
            catch (ArgumentOutOfRangeException)
            {
                ;
            }
        }

        private void LlenarListaPropietarios()
        {
            List<GridPropietarioIndividual> GridPropietariosLista = new List<GridPropietarioIndividual>();
            if (BM_Database_Recurso.Bm_Personal_BuscarGrid())
            {
                while (BM_Database_Recurso.Bm_Personal_BuscarGridProxima())
                {
                    GridPropietariosLista.Add(
                        new GridPropietarioIndividual
                        {
                            RUT = BM_Database_Recurso.BK_GRID_RUT,
                            APELLIDO = BM_Database_Recurso.BK_GRID_APELLIDOS,
                            NOMBRE = BM_Database_Recurso.BK_GRID_NOMBRES
                        });
                }
            }
            dataGridListadoPropietarios.IsReadOnly = true;
            dataGridListadoPropietarios.ItemsSource = GridPropietariosLista;
        }

        private void DataGridRecursos_Seleccion(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid CeldaSeleccionada = sender as DataGrid;
                GridRecursoIndividual Fila = (GridRecursoIndividual)CeldaSeleccionada.SelectedItems[0];
                if (BM_Database_Recurso.Bm_Recursos_Buscar(LvrTransferVar.R_PENTALPHA, Fila.PATENTE))
                {
                    // LimpiarPantalla();
                    LlenarPantallaConDb();
                    // LlenarListaPersonal();
                }
                else
                {
                    // textBoxNombreEmpresa.Text = "Sin Empresa";
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                ;
            }
        }

        private void LlenarListaRecursos()
        {
            List<GridRecursoIndividual> GridRecursoLista = new List<GridRecursoIndividual>();
            if (BM_Database_Recurso.Bm_Recursos_BuscarGrid())
            {
                while (BM_Database_Recurso.Bm_Recursos_BuscarGridProxima())
                {
                    GridRecursoLista.Add(
                        new GridRecursoIndividual
                        {
                            PATENTE = BM_Database_Recurso.BK_RGRID_PATENTE,
                            TIPO = BM_Database_Recurso.BK_RGRID_TIPO,
                            MARCA = BM_Database_Recurso.BK_RGRID_MARCA,
                            MODELO = BM_Database_Recurso.BK_RGRID_MODELO,
                            CIUDAD = BM_Database_Recurso.BK_RGRID_CIUDAD
                        });
                }
            }
            dataGridListadoRecursos.IsReadOnly = true;
            dataGridListadoRecursos.ItemsSource = GridRecursoLista;
        }

        private async Task AvisoOperacionRecursosDialogAsync(string xTitulo, string xDescripcion)
        {
            ContentDialog AvisoOperacionPersonalDialog = new ContentDialog
            {
                Title = xTitulo,
                Content = xDescripcion,
                CloseButtonText = "Continuar"
            };
            _ = await AvisoOperacionPersonalDialog.ShowAsync();
        }

        //**************************************************
        // Ejecuta operacion de registro de Recurso
        //**************************************************
        private bool ProRegistroRecurso(string pTipoOperacion)
        {
            string LvrPRecibirServer;
            string LvrPData;
            string LvrStringHttp1 = "https://finanven.ddns.net/Api/BikeMessengerRecurso";

            LvrInternet LvrBKInternet = new LvrInternet();
            string LvrParametros;

            List<JsonBikeMessengerRecurso> EnviarJsonRecursoArray = new List<JsonBikeMessengerRecurso>();
            List<JsonBikeMessengerRecurso> RecibirJsonRecursoArray = new List<JsonBikeMessengerRecurso>();

            // Llenar estructura Json
            CopiarMemoriaEnJson(pTipoOperacion);

            // Proceso Serializar

            EnviarJsonRecursoArray.Add(EnviarJsonRecurso);
            LvrPData = JsonConvert.SerializeObject(EnviarJsonRecursoArray);

            // Preparar Parametros
            LvrParametros = LvrPData;

            LvrBKInternet.LvrInetPOST(LvrStringHttp1, LvrParametros);
            LvrPRecibirServer = LvrBKInternet.LvrResultadoWeb;

            if (LvrPRecibirServer != "ERROR" && LvrPRecibirServer != "" && LvrPRecibirServer != null)
            {
                // Procesar primer servidor
                RecibirJsonRecursoArray = JsonConvert.DeserializeObject<List<JsonBikeMessengerRecurso>>(LvrPRecibirServer); // resp será el string JSON a deserializa
                RecibirJsonRecurso = RecibirJsonRecursoArray[0];

                return RecibirJsonRecurso.RESOPERACION == "OK";
            }
            return false;
        }

        private void CopiarMemoriaEnJson(string pOPERACION)
        {
            // Limpiar Variables

            EnviarJsonRecurso.OPERACION = "";
            EnviarJsonRecurso.PENTALPHA = "";
            EnviarJsonRecurso.PATENTE = "";
            EnviarJsonRecurso.RUTID = "";
            EnviarJsonRecurso.DIGVER = "";
            EnviarJsonRecurso.PROPIETARIO = "";
            EnviarJsonRecurso.TIPO = "";
            EnviarJsonRecurso.MARCA = "";
            EnviarJsonRecurso.MODELO = "";
            EnviarJsonRecurso.VARIANTE = "";
            EnviarJsonRecurso.ANO = "";
            EnviarJsonRecurso.COLOR = "";
            EnviarJsonRecurso.CIUDAD = "";
            EnviarJsonRecurso.COMUNA = "";
            EnviarJsonRecurso.REGION = "";
            EnviarJsonRecurso.PAIS = "";
            EnviarJsonRecurso.OBSERVACIONES = "";
            EnviarJsonRecurso.FOTO = "";
            EnviarJsonRecurso.RESOPERACION = "";
            EnviarJsonRecurso.RESMENSAJE = "";

            // Llenar Variables
            EnviarJsonRecurso.OPERACION = pOPERACION;
            EnviarJsonRecurso.PENTALPHA = BM_Database_Recurso.BK_PENTALPHA;
            EnviarJsonRecurso.PATENTE = BM_Database_Recurso.BK_PATENTE;
            EnviarJsonRecurso.RUTID = BM_Database_Recurso.BK_RUTID;
            EnviarJsonRecurso.DIGVER = BM_Database_Recurso.BK_DIGVER;
            EnviarJsonRecurso.PROPIETARIO = BM_Database_Recurso.BK_PROPIETARIO;
            EnviarJsonRecurso.TIPO = BM_Database_Recurso.BK_TIPO;
            EnviarJsonRecurso.MARCA = BM_Database_Recurso.BK_MARCA;
            EnviarJsonRecurso.MODELO = BM_Database_Recurso.BK_MODELO;
            EnviarJsonRecurso.VARIANTE = BM_Database_Recurso.BK_VARIANTE;
            EnviarJsonRecurso.ANO = BM_Database_Recurso.BK_ANO;
            EnviarJsonRecurso.COLOR = BM_Database_Recurso.BK_COLOR;
            EnviarJsonRecurso.CIUDAD = BM_Database_Recurso.BK_CIUDAD;
            EnviarJsonRecurso.COMUNA = BM_Database_Recurso.BK_COMUNA;
            EnviarJsonRecurso.REGION = BM_Database_Recurso.BK_REGION;
            EnviarJsonRecurso.PAIS = BM_Database_Recurso.BK_PAIS;
            EnviarJsonRecurso.OBSERVACIONES = BM_Database_Recurso.BK_OBSERVACIONES;
            EnviarJsonRecurso.FOTO = BM_Database_Recurso.BK_FOTO;
            EnviarJsonRecurso.RESOPERACION = "";
            EnviarJsonRecurso.RESMENSAJE = "";
        }

        private void CopiarJsonEnMemoria(string pOPERACION)
        {
            // Proceso
            // EnviarJsonPersonal.OPERACION = pOPERACION;
            // EnviarJsonRecurso.OPERACION = pOPERACION;
            BM_Database_Recurso.BK_PENTALPHA = EnviarJsonRecurso.PENTALPHA;
            BM_Database_Recurso.BK_PATENTE = EnviarJsonRecurso.PATENTE;
            BM_Database_Recurso.BK_RUTID = EnviarJsonRecurso.RUTID;
            BM_Database_Recurso.BK_DIGVER = EnviarJsonRecurso.DIGVER;
            BM_Database_Recurso.BK_PROPIETARIO = EnviarJsonRecurso.PROPIETARIO;
            BM_Database_Recurso.BK_TIPO = EnviarJsonRecurso.TIPO;
            BM_Database_Recurso.BK_MARCA = EnviarJsonRecurso.MARCA;
            BM_Database_Recurso.BK_MODELO = EnviarJsonRecurso.MODELO;
            BM_Database_Recurso.BK_VARIANTE = EnviarJsonRecurso.VARIANTE;
            BM_Database_Recurso.BK_ANO = EnviarJsonRecurso.ANO;
            BM_Database_Recurso.BK_COLOR = EnviarJsonRecurso.COLOR;
            BM_Database_Recurso.BK_CIUDAD = EnviarJsonRecurso.CIUDAD;
            BM_Database_Recurso.BK_COMUNA = EnviarJsonRecurso.COMUNA;
            BM_Database_Recurso.BK_REGION = EnviarJsonRecurso.REGION;
            BM_Database_Recurso.BK_PAIS = EnviarJsonRecurso.PAIS;
            BM_Database_Recurso.BK_OBSERVACIONES = EnviarJsonRecurso.OBSERVACIONES;
            BM_Database_Recurso.BK_FOTO = EnviarJsonRecurso.FOTO;
            // EnviarJsonRecurso.RESOPERACION = "";
            // EnviarJsonRecurso.RESMENSAJE = "";
        }
    }

    public class GridPropietarioIndividual
    {
        public string RUT { get; set; }
        public string APELLIDO { get; set; }
        public string NOMBRE { get; set; }
    }

    internal class GridRecursoIndividual
    {
        public string PATENTE { get; set; }
        public string TIPO { get; set; }
        public string MARCA { get; set; }
        public string MODELO { get; set; }
        public string CIUDAD { get; set; }
    }
}
