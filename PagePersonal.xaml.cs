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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PagePersonal : Page
    {
        private List<JsonBikeMessengerPersonal> PersonalIOArray = new List<JsonBikeMessengerPersonal>();
        private JsonBikeMessengerPersonal PersonalIO = new JsonBikeMessengerPersonal();
        private JsonBikeMessengerPersonal EnviarJsonPersonal = new JsonBikeMessengerPersonal();
        private JsonBikeMessengerPersonal RecibirJsonPersonal = new JsonBikeMessengerPersonal();
        private Bm_Personal_Database BM_Database_Personal = new Bm_Personal_Database();
        private TransferVar LvrTransferVar = new TransferVar();
        private bool BorrarSiNo;

        public PagePersonal()
        {
            InitializeComponent();
            comboBoxAutorizacion.Items.Add("ADMINISTRADOR");
            comboBoxAutorizacion.Items.Add("OPERADOR");
            comboBoxAutorizacion.Items.Add("COLABORADOR");
            comboBoxAutorizacion.Items.Add("INTERMEDIARIO");
            comboBoxAutorizacion.Items.Add("CLIENTE");
            InicioPantalla();
        }

        void InicioPantalla()
        {

            RellenarCombos();

            if (LvrTransferVar.CLI_RUTID == "")
            {
                PersonalIOArray = BM_Database_Personal.BuscarPersonal();
                if (PersonalIOArray != null && PersonalIOArray.Count > 0)
                {
                    PersonalIO = PersonalIOArray[0];
                    LlenarPantallaConDb();
                }
            }
            else
            {
                PersonalIOArray = BM_Database_Personal.BuscarPersonal(LvrTransferVar.PER_PENTALPHA, LvrTransferVar.CLI_RUTID, LvrTransferVar.CLI_DIGVER);
                if (PersonalIOArray != null && PersonalIOArray.Count > 0)
                {
                    PersonalIO = PersonalIOArray[0];
                    LlenarPantallaConDb();
                }
            }
            LlenarListaPersonal();

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
            openPicker.FileTypeFilter.Add(".png");
            
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
                LvrTransferVar.CLI_RUTID = PersonalIO.RUTID;
                LvrTransferVar.CLI_DIGVER = PersonalIO.DIGVER;
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
                catch (NullReferenceException)
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
                catch (NullReferenceException)
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
                catch (NullReferenceException)
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
                catch (NullReferenceException)
                {

                }
            }
        }

        private void LimpiarPantalla()
        {
            // LvrTransferVar.PER_PENTALPHA = "";
            LvrTransferVar.CLI_RUTID = "";
            LvrTransferVar.CLI_DIGVER = "";
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
            PersonalIO.PENTALPHA = LvrTransferVar.PER_PENTALPHA;
            PersonalIO.RUTID = textBoxRut.Text;
            PersonalIO.DIGVER = textBoxDigitoVerificador.Text;
            // ***********************************************************
            // Agregando Campos de Parametros
            LvrTransferVar.CLI_RUTID = PersonalIO.RUTID;
            LvrTransferVar.CLI_DIGVER = PersonalIO.DIGVER;
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

            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            await LlenarDbConPantallaAsync();


            if (BM_Database_Personal.AgregarPersonal(PersonalIO))
            {
                await AvisoOperacionPersonalDialogAsync("Agregar Personal", "Personal agregado exitosamente.");
                LlenarListaPersonal();
                LvrTransferVar.CLI_RUTID = PersonalIO.RUTID;
                LvrTransferVar.CLI_DIGVER = PersonalIO.DIGVER;
                LvrTransferVar.EscribirValoresDeAjustes();
                LvrTransferVar.LeerValoresDeAjustes();
            }
            else
            {
                await AvisoOperacionPersonalDialogAsync("Agregar Personal", "Error en ingreso de personal. Reintente o escriba a soporte contacto@pentalpha.net");
            }

            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }

        private async void BtnModificarPersonal(object sender, RoutedEventArgs e)
        {
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            await LlenarDbConPantallaAsync();

            if (BM_Database_Personal.ModificarPersonal(PersonalIO))
            {
                await AvisoOperacionPersonalDialogAsync("Modificar Personal", "Personal modificada exitosamente.");
                LlenarListaPersonal();
                LvrTransferVar.CLI_RUTID = PersonalIO.RUTID;
                LvrTransferVar.CLI_DIGVER = PersonalIO.DIGVER;
                LvrTransferVar.EscribirValoresDeAjustes();
                LvrTransferVar.LeerValoresDeAjustes();
            }
            else
            {
                await AvisoOperacionPersonalDialogAsync("Modificar Personal", "Error en modificación de personal. Reintente o escriba a soporte contacto@pentalpha.net");
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

            if (BM_Database_Personal.BorrarPersonal(LvrTransferVar.PER_PENTALPHA, PersonalIO.RUTID, PersonalIO.DIGVER))
            {
                if ((PersonalIOArray = BM_Database_Personal.BuscarPersonal()) != null)
                {
                    PersonalIO = PersonalIOArray[0];
                    LvrTransferVar.CLI_RUTID = PersonalIO.RUTID;
                    LvrTransferVar.CLI_DIGVER = PersonalIO.DIGVER;
                    await AvisoOperacionPersonalDialogAsync("Borrar Personal", "Personal borrado exitosamente.");
                }
                else
                {
                    LvrTransferVar.CLI_RUTID = "";
                    LvrTransferVar.CLI_DIGVER = "";
                    await AvisoOperacionPersonalDialogAsync("Borrar Personal", "Error en borrado de personal. Reintente o escriba a soporte contacto@pentalpha.net");
                }

                LvrTransferVar.EscribirValoresDeAjustes();
                LvrTransferVar.LeerValoresDeAjustes();

                LlenarPantallaConDb();
                LlenarListaPersonal();

                await AvisoOperacionPersonalDialogAsync("Borrar Personal", "Personal borrado exitosamente.");
            }
            else
            {
                await AvisoOperacionPersonalDialogAsync("Borrar Personal", "Error en borrado de personal. Reintente o escriba a soporte contacto@pentalpha.net");
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
            List<ClasePersonalGrid> GridPersonalDb = new List<ClasePersonalGrid>();
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

                PersonalIOArray = BM_Database_Personal.BuscarPersonal(LvrTransferVar.PER_PENTALPHA, CadenaDividida[0], CadenaDividida[1]);

                if (PersonalIOArray != null && PersonalIOArray.Count > 0)
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

        public GridPersonalIndividual()
        {
            RUT = "";
            APELLIDO = "";
            NOMBRE = "";
        }
    }
}
