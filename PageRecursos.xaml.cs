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
        private List<StructBikeMessengerRecurso> RecursoIOArray = new List<StructBikeMessengerRecurso>();
        private StructBikeMessengerRecurso RecursoIO = new StructBikeMessengerRecurso();
        private readonly StructBikeMessengerRecurso EnviarJsonRecurso = new StructBikeMessengerRecurso();
        private StructBikeMessengerRecurso RecibirJsonRecurso = new StructBikeMessengerRecurso();
        private readonly Bm_Recurso_Database BM_Database_Recurso = new Bm_Recurso_Database();
        private TransferVar LvrTransferVar = new TransferVar();
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
            InicioPantalla();
        }

        private void InicioPantalla()
        {
            RellenarCombos();

            if (LvrTransferVar.REC_PAT_SER == "")
            {
                RecursoIOArray = BM_Database_Recurso.BuscarRecurso(LvrTransferVar.PENTALPHA_ID);
                if (RecursoIOArray != null && RecursoIOArray.Count > 0)
                {
                    RecursoIO = RecursoIOArray[0];
                    LlenarPantallaConDb();
                }
            }
            else
            {
                RecursoIOArray = BM_Database_Recurso.BuscarRecurso(LvrTransferVar.REC_PENTALPHA, LvrTransferVar.REC_PAT_SER);
                if (RecursoIOArray != null && RecursoIOArray.Count > 0)
                {
                    RecursoIO = RecursoIOArray[0];
                    LlenarPantallaConDb();
                }
            }
            LlenarListaRecursos();
            LlenarListaPropietarios();
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
                using IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                // Set the image source to the selected bitmap.
                BitmapImage bitmapImage = new BitmapImage();

                bitmapImage.SetSource(fileStream);
                imageFotoRecurso.Source = bitmapImage;
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
            if (ListaPais != null)
            {
                try
                {
                    for (int i = 0; i < ListaPais.Count; i++)
                    {
                        comboBoxPais.Items.Add(ListaPais[i]);
                    }
                }
                catch (NullReferenceException)
                {

                }
            }

            // Llenar Combo Region
            List<string> ListaEstado = BM_Database_Recurso.GetRegion();

            if (ListaEstado != null)
            {
                try
                {
                    for (int i = 0; i < ListaEstado.Count; i++)
                    {
                        comboBoxEstado.Items.Add(ListaEstado[i]);
                    }
                }
                catch (NullReferenceException)
                {

                }
            }

            // Llenar Combo Comuna

            List<string> ListaComuna = BM_Database_Recurso.GetComuna();

            if (ListaComuna != null)
            {
                try
                {
                    for (int i = 0; i < ListaComuna.Count; i++)
                    {
                        comboBoxComuna.Items.Add(ListaComuna[i]);
                    }
                }
                catch (NullReferenceException)
                {

                }
            }

            // Llenar Combo Ciudad
            List<string> ListaCiudad = BM_Database_Recurso.GetCiudad();

            if (ListaCiudad != null)
            {
                try
                {
                    for (int i = 0; i < ListaCiudad.Count; i++)
                    {
                        comboBoxCiudad.Items.Add(ListaCiudad[i]);
                    }
                }
                catch (NullReferenceException)
                {

                }
            }
        }

        private async void LlenarPantallaConDb()
        {
            try
            {
                // LvrTransferVar.REC_PENTALPHA = RecursoIO.PENTALPHA;
                LvrTransferVar.REC_RUTID = RecursoIO.RUTID;
                LvrTransferVar.REC_DIGVER = RecursoIO.DIGVER;
                LvrTransferVar.REC_PAT_SER = RecursoIO.PATENTE;
                textBoxRut.Text = RecursoIO.RUTID;
                textBoxDigitoVerificador.Text = RecursoIO.DIGVER;
                textBoxPropietario.Text = BM_Database_Recurso.Bm_BuscarNombrePropietario(LvrTransferVar.REC_PENTALPHA, LvrTransferVar.REC_RUTID, LvrTransferVar.REC_DIGVER);
                comboBoxTipo.SelectedValue = RecursoIO.TIPO;
                textBoxPatenteCodigo.Text = RecursoIO.PATENTE;
                textBoxMarca.Text = RecursoIO.MARCA;
                textBoxModelo.Text = RecursoIO.MODELO;
                textBoxVariante.Text = RecursoIO.VARIANTE;
                textBoxAno.Text = RecursoIO.ANO;
                textBoxColor.Text = RecursoIO.COLOR;

                // Llenado de Pais
                if (comboBoxPais.Items.Count == 0)
                    comboBoxPais.Items.Add(RecursoIO.PAIS);
                comboBoxPais.SelectedValue = RecursoIO.PAIS;

                // Llenado de Estado o Region
                if (comboBoxEstado.Items.Count == 0)
                    comboBoxEstado.Items.Add(RecursoIO.REGION);
                comboBoxEstado.SelectedValue = RecursoIO.REGION;

                // Llenado de Comuna o Municipio
                if (comboBoxComuna.Items.Count == 0)
                    comboBoxComuna.Items.Add(RecursoIO.COMUNA);
                comboBoxComuna.SelectedValue = RecursoIO.COMUNA;

                // Llenado de Cuidad
                if (comboBoxCiudad.Items.Count == 0)
                    comboBoxCiudad.Items.Add(RecursoIO.CIUDAD);
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
            LvrTransferVar.REC_RUTID = "";
            LvrTransferVar.REC_DIGVER = "";
            LvrTransferVar.REC_PAT_SER = "";
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

            RecursoIO.PENTALPHA = LvrTransferVar.REC_PENTALPHA;
            RecursoIO.PATENTE = textBoxPatenteCodigo.Text;
            RecursoIO.PKRECURSO = RecursoIO.PENTALPHA + RecursoIO.PATENTE;
            RecursoIO.RUTID = textBoxRut.Text;
            RecursoIO.DIGVER = textBoxDigitoVerificador.Text;
            // ***********************************************************
            // Agregando Campos de Parametros
            LvrTransferVar.REC_PAT_SER = RecursoIO.PATENTE;
            LvrTransferVar.REC_RUTID = RecursoIO.RUTID;
            LvrTransferVar.REC_DIGVER = RecursoIO.DIGVER;
            // ***********************************************************
            RecursoIO.TIPO = comboBoxTipo.Text;
            RecursoIO.MARCA = textBoxMarca.Text;
            RecursoIO.MODELO = textBoxModelo.Text;
            RecursoIO.VARIANTE = textBoxVariante.Text;
            RecursoIO.ANO = textBoxAno.Text;
            RecursoIO.COLOR = textBoxColor.Text;
            if (comboBoxCiudad.Text != "")
            {
                RecursoIO.CIUDAD = comboBoxCiudad.Text;
            }

            if (comboBoxComuna.Text != "")
            {
                RecursoIO.COMUNA = comboBoxComuna.Text;
            }

            if (comboBoxEstado.Text != "")
            {
                RecursoIO.REGION = comboBoxEstado.Text;
            }

            if (comboBoxPais.Text != "")
            {
                RecursoIO.PAIS = comboBoxPais.Text;
            }

            RecursoIO.OBSERVACIONES = textBoxObservaciones.Text;
            try
            {
                RecursoIO.FOTO = await ConvertirImageABase64Async();
            }
            catch (ArgumentException)
            {
                RecursoIO.FOTO = "";
            }
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

                if (TransaccionOK)
                {
                    await AvisoOperacionRecursosDialogAsync("Agregar Recurso", "Recurso agregado exitosamente.");
                    LlenarListaRecursos();
                    LlenarListaPropietarios();
                    LvrTransferVar.REC_RUTID = RecursoIO.RUTID;
                    LvrTransferVar.REC_DIGVER = RecursoIO.DIGVER;
                    LvrTransferVar.REC_PAT_SER = RecursoIO.PATENTE;
                    LvrTransferVar.EscribirValoresDeAjustes();
                    LvrTransferVar.LeerValoresDeAjustes();
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
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            await LlenarDbConPantallaAsync();

            if (BM_Database_Recurso.ModificarRecurso(RecursoIO))
            {
                // Muestra de espera
                bool TransaccionOK = true;

                if (TransaccionOK)
                {
                    await AvisoOperacionRecursosDialogAsync("Modificar Recurso", "Recurso modificado exitosamente.");
                    LlenarListaRecursos();
                    LlenarListaPropietarios();
                    LvrTransferVar.REC_PAT_SER = RecursoIO.PATENTE;
                    LvrTransferVar.REC_RUTID = RecursoIO.RUTID;
                    LvrTransferVar.REC_DIGVER = RecursoIO.DIGVER;
                    LvrTransferVar.EscribirValoresDeAjustes();
                    LvrTransferVar.LeerValoresDeAjustes();
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

            if (BM_Database_Recurso.BorrarRecurso(LvrTransferVar.REC_PENTALPHA, RecursoIO.PATENTE))
            {
                TransaccionOK = true;



                if (TransaccionOK)
                {
                    RecursoIOArray = BM_Database_Recurso.BuscarRecurso(LvrTransferVar.REC_PENTALPHA);

                    if (RecursoIOArray != null && RecursoIOArray.Count > 0)
                    {
                        RecursoIO = RecursoIOArray[0];
                        LvrTransferVar.REC_PAT_SER = RecursoIO.PATENTE;
                        LvrTransferVar.REC_RUTID = RecursoIO.RUTID;
                        LvrTransferVar.REC_DIGVER = RecursoIO.DIGVER;
                    }
                    else
                    {
                        LvrTransferVar.REC_RUTID = "";
                        LvrTransferVar.REC_DIGVER = "";
                        LvrTransferVar.REC_PAT_SER = "";
                    }
                    LlenarPantallaConDb();
                    LlenarListaRecursos();
                    LlenarListaPropietarios();
                    await AvisoOperacionRecursosDialogAsync("Borrar Recurso", "Recurso borrado exitosamente.");
                    LvrTransferVar.EscribirValoresDeAjustes();
                    LvrTransferVar.LeerValoresDeAjustes();
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

            if ((GridPersonalDb = BM_Database_Recurso.BuscarGridPersonal(LvrTransferVar.EMP_PENTALPHA)) != null)
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

            List<ClaseRecursoGrid> GridRecursoDb = new List<ClaseRecursoGrid>();
            List<GridRecursoIndividual> GridRecursoLista = new List<GridRecursoIndividual>();
            if ((GridRecursoDb = BM_Database_Recurso.BuscarGridRecurso(LvrTransferVar.EMP_PENTALPHA)) != null)
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
                RecursoIOArray = BM_Database_Recurso.BuscarRecurso(LvrTransferVar.REC_PENTALPHA, Fila.PATENTE);
                if (RecursoIOArray != null && RecursoIOArray.Count > 0)
                {
                    LimpiarPantalla();
                    RecursoIO = RecursoIOArray[0];
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

        private void CopiarMemoriaEnJson(string pOPERACION)
        {
            // Limpiar Variables

            EnviarJsonRecurso.OPERACION = "";
            EnviarJsonRecurso.PENTALPHA = "";
            EnviarJsonRecurso.PATENTE = "";
            EnviarJsonRecurso.RUTID = "";
            EnviarJsonRecurso.DIGVER = "";
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
