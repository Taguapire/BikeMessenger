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
    public sealed partial class PageRecursos : Page
    {
        private List<JsonBikeMessengerRecurso> RecursoIOArray = null;
        private JsonBikeMessengerRecurso RecursoIO = null;
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
                RellenarCombos();

                if (LvrTransferVar.R_PAT_SER == "")
                {
                    if ((RecursoIOArray = BM_Database_Recurso.BuscarRecurso()) != null)
                    {
                        RecursoIO = RecursoIOArray[0];
                        LlenarPantallaConDb();
                    }
                }
                else
                {
                    if ((RecursoIOArray = BM_Database_Recurso.BuscarRecurso(LvrTransferVar.R_PENTALPHA, LvrTransferVar.R_PAT_SER)) != null)
                    {
                        RecursoIO = RecursoIOArray[0];
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
            List<string> ListaPais = BM_Database_Recurso.GetPais();
            for (int i = 0; i < ListaPais.Count; i++)
            {
                comboBoxPais.Items.Add(ListaPais[i]);
            }

            // Llenar Combo Region
            List<string> ListaEstado = BM_Database_Recurso.GetRegion();
            for (int i = 0; i < ListaEstado.Count; i++)
            {
                comboBoxEstado.Items.Add(ListaEstado[i]);
            }

            // Llenar Combo Comuna
            List<string> ListaComuna = BM_Database_Recurso.GetComuna();
            for (int i = 0; i < ListaComuna.Count; i++)
            {
                comboBoxComuna.Items.Add(ListaComuna[i]);
            }

            // Llenar Combo Ciudad
            List<string> ListaCiudad = BM_Database_Recurso.GetCiudad();
            for (int i = 0; i < ListaCiudad.Count; i++)
            {
                comboBoxCiudad.Items.Add(ListaCiudad[i]);
            }
        }

        private async void LlenarPantallaConDb()
        {
            try
            {
                // LvrTransferVar.R_PENTALPHA = RecursoIO.PENTALPHA;
                LvrTransferVar.R_RUTID = RecursoIO.RUTID;
                LvrTransferVar.R_DIGVER = RecursoIO.DIGVER;
                LvrTransferVar.R_PAT_SER = RecursoIO.PATENTE;
                textBoxRut.Text = RecursoIO.RUTID;
                textBoxDigitoVerificador.Text = RecursoIO.DIGVER;
                textBoxPropietario.Text = RecursoIO.PROPIETARIO;
                comboBoxTipo.SelectedValue = RecursoIO.TIPO;
                textBoxPatenteCodigo.Text = RecursoIO.PATENTE;
                textBoxMarca.Text = RecursoIO.MARCA;
                textBoxModelo.Text = RecursoIO.MODELO;
                textBoxVariante.Text = RecursoIO.VARIANTE;
                textBoxAno.Text = RecursoIO.ANO;
                textBoxColor.Text = RecursoIO.COLOR;
                comboBoxPais.SelectedValue = RecursoIO.PAIS;
                comboBoxEstado.SelectedValue = RecursoIO.REGION;
                comboBoxComuna.SelectedValue = RecursoIO.COMUNA;
                comboBoxCiudad.SelectedValue = RecursoIO.CIUDAD;
                textBoxObservaciones.Text = RecursoIO.OBSERVACIONES;
                imageFotoRecurso.Source = Base64StringToBitmap(RecursoIO.FOTO);
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
            RecursoIO.PENTALPHA = LvrTransferVar.R_PENTALPHA;
            RecursoIO.PATENTE = textBoxPatenteCodigo.Text;
            RecursoIO.RUTID = textBoxRut.Text;
            RecursoIO.DIGVER = textBoxDigitoVerificador.Text;
            // ***********************************************************
            // Agregando Campos de Parametros
            LvrTransferVar.R_PAT_SER = RecursoIO.PATENTE;
            LvrTransferVar.R_RUTID = RecursoIO.RUTID;
            LvrTransferVar.R_DIGVER = RecursoIO.DIGVER;
            // ***********************************************************
            RecursoIO.PROPIETARIO = textBoxPropietario.Text;
            RecursoIO.TIPO = comboBoxTipo.Text;
            RecursoIO.MARCA = textBoxMarca.Text;
            RecursoIO.MODELO = textBoxModelo.Text;
            RecursoIO.VARIANTE = textBoxVariante.Text;
            RecursoIO.ANO = textBoxAno.Text;
            RecursoIO.COLOR = textBoxColor.Text;
            if (comboBoxCiudad.Text != "")
                RecursoIO.CIUDAD = comboBoxCiudad.Text;
            if (comboBoxComuna.Text != "")
                RecursoIO.COMUNA = comboBoxComuna.Text;
            if (comboBoxEstado.Text != "")
                RecursoIO.REGION = comboBoxEstado.Text;
            if (comboBoxPais.Text != "")
                RecursoIO.PAIS = comboBoxPais.Text;
            RecursoIO.OBSERVACIONES = textBoxObservaciones.Text;
            RecursoIO.FOTO = await ConvertirImageABase64Async();
        }


        private async void BtnAgregarRecursos(object sender, RoutedEventArgs e)
        {
            // Muestra de espera
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            await LlenarDbConPantallaAsync();

            bool TransaccionOK = false;

            if (BM_Database_Recurso.AgregarRecurso(RecursoIO))
            {
                TransaccionOK = true;

                if (LvrTransferVar.SincronizarWebRemoto())
                {
                    TransaccionOK = ProRegistroRecurso("AGREGAR");
                }

                if (LvrTransferVar.SincronizarWebPropio())
                {
                    TransaccionOK = ProRegistroRecurso("AGREGAR");
                }

                if (TransaccionOK)
                {
                    await AvisoOperacionRecursosDialogAsync("Agregar Recurso", "Recurso agregado exitosamente.");
                    LlenarListaRecursos();
                    LlenarListaPropietarios();
                    LvrTransferVar.R_RUTID = RecursoIO.RUTID;
                    LvrTransferVar.R_DIGVER = RecursoIO.DIGVER;
                    LvrTransferVar.R_PAT_SER = RecursoIO.PATENTE;
                }

                else
                {
                    await AvisoOperacionRecursosDialogAsync("Agregar Recurso", "Error en ingreso de Recurso. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            else
            {
                await AvisoOperacionRecursosDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de Cliente.");
            }

            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }

        private async void BtnModificarRecursos(object sender, RoutedEventArgs e)
        {
            // Muestra de espera
            bool TransaccionOK = false;
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            await LlenarDbConPantallaAsync();

            if (BM_Database_Recurso.ModificarRecurso(RecursoIO))
            {
                TransaccionOK = true;

                if (LvrTransferVar.SincronizarWebRemoto())
                {
                    TransaccionOK = ProRegistroRecurso("MODIFICAR");
                }
                if (LvrTransferVar.SincronizarWebPropio())
                {
                    TransaccionOK = ProRegistroRecurso("MODIFICAR");
                }


                if (TransaccionOK)
                {
                    LlenarListaRecursos();
                    LlenarListaPropietarios();
                    LvrTransferVar.R_PAT_SER = RecursoIO.PATENTE;
                    LvrTransferVar.R_RUTID = RecursoIO.RUTID;
                    LvrTransferVar.R_DIGVER = RecursoIO.DIGVER;
                }
                else
                {
                    await AvisoOperacionRecursosDialogAsync("Modificar Recurso", "Error en modificación de recurso. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            else
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
            bool TransaccionOK = false;
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            if (BM_Database_Recurso.BorrarRecurso(LvrTransferVar.R_PENTALPHA, RecursoIO.PATENTE))
            {
                TransaccionOK = true;

                if (LvrTransferVar.SincronizarWebRemoto())
                {
                    TransaccionOK = ProRegistroRecurso("BORRAR");
                }

                if (LvrTransferVar.SincronizarWebPropio())
                {
                    TransaccionOK = ProRegistroRecurso("BORRAR");
                }


                if (TransaccionOK)
                {
                    if ((RecursoIOArray = BM_Database_Recurso.BuscarRecurso()) != null)
                    {
                        LvrTransferVar.R_PAT_SER = RecursoIO.PATENTE;
                        LvrTransferVar.R_RUTID = RecursoIO.RUTID;
                        LvrTransferVar.R_DIGVER = RecursoIO.DIGVER;
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
                    await AvisoOperacionRecursosDialogAsync("Borrar Recurso", "Error en borrado de recurso. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            else
            {
                await AvisoOperacionRecursosDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de recurso.");
            }
            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }

        private void BtnListarRecursos(object sender, RoutedEventArgs e)
        {
            LvrTransferVar.PantallaAnterior = "RECURSO";
            _ = Frame.Navigate(typeof(ListadosGenerales), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSalirRecursos(object sender, RoutedEventArgs e)
        {
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
        //**************************************** Propietarios ************************************
        private void LlenarListaPropietarios()
        {
            List<ClasePersonalGrid> GridPersonalDb = new List<ClasePersonalGrid>();
            List<GridPropietarioIndividual> GridPropietariosLista = new List<GridPropietarioIndividual>();

            if ((GridPersonalDb = BM_Database_Recurso.BuscarGridPersonal()) != null)
            {
                for (int i = 0; i < GridPersonalDb.Count; i++)
                {
                    GridPropietariosLista.Add(
                        new GridPropietarioIndividual
                        {
                            RUT = GridPersonalDb[i].RUTID + "-" + GridPersonalDb[i].DIGVER,
                            APELLIDO = GridPersonalDb[i].APELLIDOS,
                            NOMBRE = GridPersonalDb[i].NOMBRES
                        });
                }
            }
            dataGridListadoPropietarios.IsReadOnly = true;
            dataGridListadoPropietarios.ItemsSource = GridPropietariosLista;
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

        //**************************************** Recursos ************************************
        private void LlenarListaRecursos()
        {

            List<JsonBikeMessengerRecurso> GridRecursoDb = new List<JsonBikeMessengerRecurso>();
            List<GridRecursoIndividual> GridRecursoLista = new List<GridRecursoIndividual>();
            if ((GridRecursoDb = BM_Database_Recurso.BuscarRecurso()) != null)
            {
                for (int i = 0; i < GridRecursoDb.Count; i++)
                {
                    GridRecursoLista.Add(
                        new GridRecursoIndividual
                        {
                            PATENTE = GridRecursoDb[i].PATENTE,
                            TIPO = GridRecursoDb[i].TIPO,
                            MARCA = GridRecursoDb[i].MARCA,
                            MODELO = GridRecursoDb[i].MODELO,
                            CIUDAD = GridRecursoDb[i].CIUDAD
                        });
                }
            }
            dataGridListadoRecursos.IsReadOnly = true;
            dataGridListadoRecursos.ItemsSource = GridRecursoLista;
        }

        private void DataGridRecursos_Seleccion(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid CeldaSeleccionada = sender as DataGrid;
                GridRecursoIndividual Fila = (GridRecursoIndividual)CeldaSeleccionada.SelectedItems[0];
                if ((RecursoIOArray = BM_Database_Recurso.BuscarRecurso(LvrTransferVar.R_PENTALPHA, Fila.PATENTE)) != null)
                {
                    LimpiarPantalla();
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
            string LvrStringHttp = "https://finanven.ddns.net";
            string LvrStringPort = "443";
            string LvrStringController = "/Api/BikeMessengerRecurso";


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

            LvrBKInternet.LvrInetPOST(LvrStringHttp, LvrStringPort, LvrStringController, LvrParametros);
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
            EnviarJsonRecurso.PENTALPHA = RecursoIO.PENTALPHA;
            EnviarJsonRecurso.PATENTE = RecursoIO.PATENTE;
            EnviarJsonRecurso.RUTID = RecursoIO.RUTID;
            EnviarJsonRecurso.DIGVER = RecursoIO.DIGVER;
            EnviarJsonRecurso.PROPIETARIO = RecursoIO.PROPIETARIO;
            EnviarJsonRecurso.TIPO = RecursoIO.TIPO;
            EnviarJsonRecurso.MARCA = RecursoIO.MARCA;
            EnviarJsonRecurso.MODELO = RecursoIO.MODELO;
            EnviarJsonRecurso.VARIANTE = RecursoIO.VARIANTE;
            EnviarJsonRecurso.ANO = RecursoIO.ANO;
            EnviarJsonRecurso.COLOR = RecursoIO.COLOR;
            EnviarJsonRecurso.CIUDAD = RecursoIO.CIUDAD;
            EnviarJsonRecurso.COMUNA = RecursoIO.COMUNA;
            EnviarJsonRecurso.REGION = RecursoIO.REGION;
            EnviarJsonRecurso.PAIS = RecursoIO.PAIS;
            EnviarJsonRecurso.OBSERVACIONES = RecursoIO.OBSERVACIONES;
            EnviarJsonRecurso.FOTO = RecursoIO.FOTO;
            EnviarJsonRecurso.RESOPERACION = "";
            EnviarJsonRecurso.RESMENSAJE = "";
        }

        private void CopiarJsonEnMemoria(string pOPERACION)
        {
            // Proceso
            // EnviarJsonPersonal.OPERACION = pOPERACION;
            // EnviarJsonRecurso.OPERACION = pOPERACION;
            RecursoIO.PENTALPHA = EnviarJsonRecurso.PENTALPHA;
            RecursoIO.PATENTE = EnviarJsonRecurso.PATENTE;
            RecursoIO.RUTID = EnviarJsonRecurso.RUTID;
            RecursoIO.DIGVER = EnviarJsonRecurso.DIGVER;
            RecursoIO.PROPIETARIO = EnviarJsonRecurso.PROPIETARIO;
            RecursoIO.TIPO = EnviarJsonRecurso.TIPO;
            RecursoIO.MARCA = EnviarJsonRecurso.MARCA;
            RecursoIO.MODELO = EnviarJsonRecurso.MODELO;
            RecursoIO.VARIANTE = EnviarJsonRecurso.VARIANTE;
            RecursoIO.ANO = EnviarJsonRecurso.ANO;
            RecursoIO.COLOR = EnviarJsonRecurso.COLOR;
            RecursoIO.CIUDAD = EnviarJsonRecurso.CIUDAD;
            RecursoIO.COMUNA = EnviarJsonRecurso.COMUNA;
            RecursoIO.REGION = EnviarJsonRecurso.REGION;
            RecursoIO.PAIS = EnviarJsonRecurso.PAIS;
            RecursoIO.OBSERVACIONES = EnviarJsonRecurso.OBSERVACIONES;
            RecursoIO.FOTO = EnviarJsonRecurso.FOTO;
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
