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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageClientes : Page
    {
        private List<StructBikeMessengerCliente> ClienteIOArray = new List<StructBikeMessengerCliente>();
        private StructBikeMessengerCliente ClienteIO = new StructBikeMessengerCliente();
        private Bm_Cliente_Database BM_Database_Cliente = new Bm_Cliente_Database();
        private TransferVar LvrTransferVar = new TransferVar();
        private bool BorrarSiNo;

        public PageClientes()
        {
            InitializeComponent();
            InicioPantalla();
        }

        void InicioPantalla()
        {

            RellenarCombos();

            if (LvrTransferVar.CLI_RUTID == "")
            {
                ClienteIOArray = BM_Database_Cliente.BuscarCliente(LvrTransferVar.CLI_PENTALPHA);
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

                // Llenado de Pais
                comboBoxPais.SelectedValue = ClienteIO.PAIS;

                // Llenado de Estado o Region
                comboBoxEstado.SelectedValue = ClienteIO.ESTADOREGION;

                // Llenado de Comuna o Municipio
                comboBoxComuna.SelectedValue = ClienteIO.COMUNA;

                // Llenado de Cuidad
                comboBoxCiudad.SelectedValue = ClienteIO.CIUDAD;

                // Llenado de Cuidad
                if (comboBoxCiudad.Items.Count == 0)
                    comboBoxCiudad.Items.Add(ClienteIO.CIUDAD);
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
            BM_CCRP LocalRellenarCombos = new BM_CCRP();

            // Limpiar Combo Box
            comboBoxPais.Items.Clear();
            comboBoxEstado.Items.Clear();
            comboBoxComuna.Items.Clear();
            comboBoxCiudad.Items.Clear();

            // Llenar Combo Pais
            comboBoxPais.ItemsSource = LocalRellenarCombos.BuscarPais();


            // Llenar Combo Region
            comboBoxEstado.ItemsSource = LocalRellenarCombos.BuscarRegion();

            // Llenar Combo Comuna
            comboBoxComuna.ItemsSource = LocalRellenarCombos.BuscarComuna();

            // Llenar Combo Ciudad
            comboBoxCiudad.ItemsSource = LocalRellenarCombos.BuscarCiudad();
        }

        private void ActualizarCombos()
        {
            BM_CCRP LocalRellenarCombos = new BM_CCRP();

            // Actualizar Combo Pais
            _ = LocalRellenarCombos.AgregarPais(ClienteIO.PAIS);

            // Actualizar Combo Region
            _ = LocalRellenarCombos.AgregarRegion(ClienteIO.ESTADOREGION);

            // Actualizar Combo Comuna
            _ = LocalRellenarCombos.AgregarComuna(ClienteIO.COMUNA);

            // Actualizar Combo Ciudad
            _ = LocalRellenarCombos.AgregarCiudad(ClienteIO.CIUDAD);
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
            ClienteIO.PKCLIENTE = ClienteIO.PENTALPHA + ClienteIO.RUTID + ClienteIO.DIGVER;
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

            try
            {
                ClienteIO.LOGO = await ConvertirImageABase64Async();
            }
            catch (ArgumentException)
            {
                ClienteIO.LOGO = "";
            }
        }

        private async void BtnAgregarClientes(object sender, RoutedEventArgs e)
        {
            await LlenarDbConPantallaAsync();
            if (BM_Database_Cliente.AgregarCliente(ClienteIO))
            {

                await AvisoOperacionClientesDialogAsync("Agregar Cliente", "Cliente agregado exitosamente.");
                LlenarListaClientes();
                LvrTransferVar.CLI_RUTID = ClienteIO.RUTID;
                LvrTransferVar.CLI_DIGVER = ClienteIO.DIGVER;
                LvrTransferVar.EscribirValoresDeAjustes();
                LvrTransferVar.LeerValoresDeAjustes();
                ActualizarCombos();
            }
            else
            {
                await AvisoOperacionClientesDialogAsync("Agregar Cliente", "Error en ingreso de Cliente. Reintente o escriba a soporte contacto@pentalpha.net");
            }
        }

        private async void BtnModificarClientes(object sender, RoutedEventArgs e)
        {
            await LlenarDbConPantallaAsync();

            if (BM_Database_Cliente.ModificarCliente(ClienteIO))
            {
                await AvisoOperacionClientesDialogAsync("Modificar Personal", "Personal modificada exitosamente.");
                LlenarListaClientes();
                LvrTransferVar.CLI_RUTID = ClienteIO.RUTID;
                LvrTransferVar.CLI_DIGVER = ClienteIO.DIGVER;
                LvrTransferVar.EscribirValoresDeAjustes();
                LvrTransferVar.LeerValoresDeAjustes();
                ActualizarCombos();
            }
            else
            {
                await AvisoOperacionClientesDialogAsync("Modificar Cliente", "Error en modificación de Cliente. Reintente o escriba a soporte contacto@pentalpha.net");
            }
        }

        private async void BtnBorrarClientes(object sender, RoutedEventArgs e)
        {

            BorrarSiNo = false;

            await AvisoBorrarClientesDialogAsync();

            if (!BorrarSiNo)
            {
                return;
            }

            await LlenarDbConPantallaAsync();

            if (BM_Database_Cliente.BorrarCliente(LvrTransferVar.CLI_PENTALPHA, ClienteIO.RUTID, ClienteIO.DIGVER))
            {
                await AvisoOperacionClientesDialogAsync("Borrar Cliente", "Cliente borrado exitosamente.");

                if ((ClienteIOArray = BM_Database_Cliente.BuscarCliente(LvrTransferVar.CLI_PENTALPHA)) != null)
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

                LvrTransferVar.EscribirValoresDeAjustes();
                LvrTransferVar.LeerValoresDeAjustes();
            }
            else
            {
                await AvisoOperacionClientesDialogAsync("Borrar Cliente", "Error en borrado de Cliente. Reintente o escriba a soporte contacto@pentalpha.net");
            }
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

            if ((GridClienteDb = BM_Database_Cliente.BuscarGridClientes(LvrTransferVar.CLI_PENTALPHA)) != null)
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

                ClienteIOArray = BM_Database_Cliente.BuscarCliente(LvrTransferVar.CLI_PENTALPHA, CadenaDividida[0], CadenaDividida[1]);
                if (ClienteIOArray != null && ClienteIOArray.Count > 0)
                {
                    ClienteIO = ClienteIOArray[0];
                    LimpiarPantalla();
                    LlenarPantallaConDb();
                    LvrTransferVar.CLI_RUTID = ClienteIO.RUTID;
                    LvrTransferVar.CLI_DIGVER = ClienteIO.DIGVER;
                    LvrTransferVar.EscribirValoresDeAjustes();
                    LvrTransferVar.LeerValoresDeAjustes();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                // No hacer nada, es un control vacio de error
            }
        }
    }

    internal class GridClientesIndividual
    {
        public string RUT { get; set; }
        public string CLIENTE { get; set; }
    }
}
