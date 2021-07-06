
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using QRCoder;
using Newtonsoft.Json;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageEmpresa : Page
    {
        private List<JsonBikeMessengerEmpresa> EmpresaIOArray = new List<JsonBikeMessengerEmpresa>();
        private JsonBikeMessengerEmpresa EmpresaIO = new JsonBikeMessengerEmpresa();
        private JsonBikeMessengerEmpresa EnviarJsonEmpresa = new JsonBikeMessengerEmpresa();
        private JsonBikeMessengerEmpresa RecibirJsonEmpresa = new JsonBikeMessengerEmpresa();
        private Bm_Empresa_Database BM_Database_Empresa = new Bm_Empresa_Database();
        private TransferVar LvrTransferVar = new TransferVar();
        private PentalphaCripto LvrCrypto = new PentalphaCripto();
        private bool BorrarSiNo;

        public PageEmpresa()
        {
            // this.BM_Connection = BM_Connection;
            InitializeComponent();
            InicioPantalla();
        }

        private void InicioPantalla()
        {
            RellenarCombos();

            EmpresaIOArray = BM_Database_Empresa.BuscarEmpresa();

            if (EmpresaIOArray != null && EmpresaIOArray.Count > 0)
            {
                EmpresaIO = EmpresaIOArray[0];
                LlenarPantallaConDb();

                LvrTransferVar.PENTALPHA_ID = EmpresaIO.PENTALPHA;
                LvrTransferVar.ActualizarPentalphaId();
                LvrTransferVar.EscribirValoresDeAjustes();
                LvrTransferVar.LeerValoresDeAjustes();

                appBarAgregar.IsEnabled = false;
                appBarModificar.IsEnabled = true;
                appBarBorrar.IsEnabled = true;

                textBoxRut.IsReadOnly = true;
                textBoxDigitoVerificador.IsReadOnly = true;
            }
            else
            {
                appBarAgregar.IsEnabled = true;
                appBarModificar.IsEnabled = false;
                appBarBorrar.IsEnabled = false;

                textBoxRut.IsReadOnly = false;
                textBoxDigitoVerificador.IsReadOnly = false;
                _ = AvisoOperacionEmpresaDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de la empresa.");
            }
        }


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
                using IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                // Set the image source to the selected bitmap.
                BitmapImage bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage();

                bitmapImage.SetSource(fileStream);
                imageLogoEmpresa.Source = bitmapImage;
            }
        }
        private async void LlenarPantallaConDb()
        {
            try
            {
                LlenarBasePentalpha(EmpresaIO.PENTALPHA);
                textBoxRut.Text = EmpresaIO.RUTID;
                textBoxDigitoVerificador.Text = EmpresaIO.DIGVER;
                textBoxNombreEmpresa.Text = EmpresaIO.NOMBRE;
                textBoxUsuario.Text = EmpresaIO.USUARIO;
                passwordClave.Password = EmpresaIO.CLAVE;
                textBoxActividad1.Text = EmpresaIO.ACTIVIDAD1;
                textBoxActividad2.Text = EmpresaIO.ACTIVIDAD2;
                textBoxRepresentantes1.Text = EmpresaIO.REPRESENTANTE1;
                textBoxRepresentantes2.Text = EmpresaIO.REPRESENTANTE2;
                textBoxRepresentantes3.Text = EmpresaIO.REPRESENTANTE3;

                textBoxCalleAvenida1.Text = EmpresaIO.DOMICILIO1;
                textBoxCalleAvenida2.Text = EmpresaIO.DOMICILIO2;
                textBoxNumero.Text = EmpresaIO.NUMERO;
                textBoxPiso.Text = EmpresaIO.PISO;
                textBoxOficina.Text = EmpresaIO.OFICINA;
                textBoxCodigoPostal.Text = EmpresaIO.CODIGOPOSTAL;

                comboBoxPais.SelectedValue = EmpresaIO.PAIS;
                comboBoxEstado.SelectedValue = EmpresaIO.ESTADOREGION;
                comboBoxComuna.SelectedValue = EmpresaIO.COMUNA;
                comboBoxCiudad.SelectedValue = EmpresaIO.CIUDAD;

                textBoxTelefono1.Text = EmpresaIO.TELEFONO1;
                textBoxTelefono2.Text = EmpresaIO.TELEFONO2;
                textBoxTelefono3.Text = EmpresaIO.TELEFONO3;

                textBoxObservaciones.Text = EmpresaIO.OBSERVACIONES;

                imageLogoEmpresa.Source = Base64StringToBitmap(EmpresaIO.LOGO);
            }
            catch (ArgumentNullException)
            {
                await AvisoOperacionEmpresaDialogAsync("Acceso a Base de Datos Empresa", "Error de carga de datos desde la Base de Datos.");
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
            List<string> ListaPais = BM_Database_Empresa.GetPais();
            if (ListaPais != null)
            {
                foreach (var LocalLista in ListaPais)
                {
                    comboBoxPais.Items.Add(LocalLista);
                }
            }

            // Llenar Combo Region
            List<string> ListaEstado = BM_Database_Empresa.GetRegion();
            if (ListaEstado != null)
            {
                foreach (var LocalLista in ListaEstado)
                {
                    comboBoxEstado.Items.Add(LocalLista);
                }
            }

            // Llenar Combo Comuna
            List<string> ListaComuna = BM_Database_Empresa.GetComuna();
            if (ListaComuna != null)
            {
                foreach (var LocalLista in ListaComuna)
                {
                    comboBoxComuna.Items.Add(LocalLista);
                }
            }


            // Llenar Combo Ciudad

            List<string> ListaCiudad = BM_Database_Empresa.GetCiudad();
            if (ListaCiudad != null)
            {
                foreach (var LocalLista in ListaCiudad)
                {
                    comboBoxCiudad.Items.Add(LocalLista);
                }
            }
        }

        private async Task LlenarDbConPantallaAsync()
        {
            EmpresaIO.RUTID = textBoxRut.Text;
            EmpresaIO.DIGVER = textBoxDigitoVerificador.Text;
            EmpresaIO.NOMBRE = textBoxNombreEmpresa.Text;
            EmpresaIO.USUARIO = textBoxUsuario.Text;
            EmpresaIO.CLAVE = passwordClave.Password;
            EmpresaIO.ACTIVIDAD1 = textBoxActividad1.Text;
            EmpresaIO.ACTIVIDAD2 = textBoxActividad2.Text;
            EmpresaIO.REPRESENTANTE1 = textBoxRepresentantes1.Text;
            EmpresaIO.REPRESENTANTE2 = textBoxRepresentantes2.Text;
            EmpresaIO.REPRESENTANTE3 = textBoxRepresentantes3.Text;
            EmpresaIO.DOMICILIO1 = textBoxCalleAvenida1.Text;
            EmpresaIO.DOMICILIO2 = textBoxCalleAvenida2.Text;
            EmpresaIO.NUMERO = textBoxNumero.Text;
            EmpresaIO.PISO = textBoxPiso.Text;
            EmpresaIO.OFICINA = textBoxOficina.Text;
            EmpresaIO.CODIGOPOSTAL = textBoxCodigoPostal.Text;

            if (comboBoxPais.Text != "")
            {
                EmpresaIO.PAIS = comboBoxPais.Text;
            }

            if (comboBoxEstado.Text != "")
            {
                EmpresaIO.ESTADOREGION = comboBoxEstado.Text;
            }

            if (comboBoxComuna.Text != "")
            {
                EmpresaIO.COMUNA = comboBoxComuna.Text;
            }

            if (comboBoxCiudad.Text != "")
            {
                EmpresaIO.CIUDAD = comboBoxCiudad.Text;
            }

            EmpresaIO.TELEFONO1 = textBoxTelefono1.Text;
            EmpresaIO.TELEFONO2 = textBoxTelefono2.Text;
            EmpresaIO.TELEFONO3 = textBoxTelefono3.Text;

            EmpresaIO.OBSERVACIONES = textBoxObservaciones.Text;
            EmpresaIO.LOGO = await ConvertirImageABase64Async();
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

            await LlenarDbConPantallaAsync();

            if (BM_Database_Empresa.AgregarEmpresa(EmpresaIO))
            {
                LvrTransferVar.PENTALPHA_ID = EmpresaIO.PENTALPHA;
                LvrTransferVar.ActualizarPentalphaId();
                LvrTransferVar.EscribirValoresDeAjustes();
                LvrTransferVar.LeerValoresDeAjustes();

                bool TransaccionOK = true;
                
                appBarAgregar.IsEnabled = false;
                appBarModificar.IsEnabled = true;
                appBarBorrar.IsEnabled = true;


                textBoxRut.IsReadOnly = true;
                textBoxDigitoVerificador.IsReadOnly = true;

                if (LvrTransferVar.SincronizarWebPentalpha())
                {
                    TransaccionOK = ProRegistroEmpresa("AGREGAR");
                }

                if (LvrTransferVar.SincronizarWebPropio())
                {
                    TransaccionOK = ProRegistroEmpresa("AGREGAR");
                }

                if (TransaccionOK)
                {
                    await AvisoOperacionEmpresaDialogAsync("Agregar Empresa", "Empresa agregada exitosamente.");
                }
                else
                {
                    await AvisoOperacionEmpresaDialogAsync("Agregar Empresa", "Error en ingreso de la empresa. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            else
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

            await LlenarDbConPantallaAsync();


            if (BM_Database_Empresa.ModificarEmpresa(EmpresaIO))
            {
                bool TransaccionOK = true;

                LvrTransferVar.PENTALPHA_ID = EmpresaIO.PENTALPHA;
                LvrTransferVar.ActualizarPentalphaId();
                LvrTransferVar.EscribirValoresDeAjustes();
                LvrTransferVar.LeerValoresDeAjustes();

                if (LvrTransferVar.SincronizarWebPentalpha())
                {
                    TransaccionOK = ProRegistroEmpresa("MODIFICAR");
                }

                if (LvrTransferVar.SincronizarWebPropio())
                {
                    TransaccionOK = ProRegistroEmpresa("MODIFICAR");
                }

                if (TransaccionOK)
                {
                    await AvisoOperacionEmpresaDialogAsync("Modificar Empresa", "Empresa modificada exitosamente.");
                }
                else
                {
                    await AvisoOperacionEmpresaDialogAsync("Modificar Empresa", "Error en modificación de la empresa. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            else
            {
                await AvisoOperacionEmpresaDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de la empresa.");
            }
            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }

        private async void BtnBorrarEmpresa(object sender, RoutedEventArgs e)
        {

            BorrarSiNo = false;

            await AvisoBorrarEmpresaDialogAsync();

            if (!BorrarSiNo)
            {
                return;
            }

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

            await LlenarDbConPantallaAsync();

            if (BM_Database_Empresa.BorrarEmpresa(EmpresaIO.PENTALPHA))
            {
                bool TransaccionOK = true;

                LvrTransferVar.PENTALPHA_ID = EmpresaIO.PENTALPHA;
                LvrTransferVar.ActualizarPentalphaId();
                LvrTransferVar.EscribirValoresDeAjustes();
                LvrTransferVar.LeerValoresDeAjustes();

                appBarAgregar.IsEnabled = true;
                appBarModificar.IsEnabled = false;
                appBarBorrar.IsEnabled = false;

                textBoxRut.IsReadOnly = false;
                textBoxDigitoVerificador.IsReadOnly = false;


                if (TransaccionOK)
                {
                    if (LvrTransferVar.SincronizarWebPentalpha())
                    {
                        TransaccionOK = ProRegistroEmpresa("BORRAR");
                    }
                    if (LvrTransferVar.SincronizarWebPropio())
                    {
                        TransaccionOK = ProRegistroEmpresa("BORRAR");
                    }
                }


                if (TransaccionOK)
                {
                    await AvisoOperacionEmpresaDialogAsync("Borrar Empresa", "Empresa borrada exitosamente.");
                }
                else
                {
                    await AvisoOperacionEmpresaDialogAsync("Agregar Empresa", "Error en borrado de la empresa. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            else
            {
                await AvisoOperacionEmpresaDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de la empresa.");
            }
            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }

        private void BtnSalirEmpresa(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private async Task<string> ConvertirImageABase64Async()
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(imageLogoEmpresa);

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

        private async Task AvisoOperacionEmpresaDialogAsync(string xTitulo, string xDescripcion)
        {
            ContentDialog AvisoOperacionEmpresaDialog = new ContentDialog
            {
                Title = xTitulo,
                Content = xDescripcion,
                CloseButtonText = "Continuar"
            };
            _ = await AvisoOperacionEmpresaDialog.ShowAsync();
        }

        private async Task AvisoBorrarEmpresaDialogAsync()
        {
            ContentDialog AvisoConfirmacionPersonalDialog = new ContentDialog
            {
                Title = "Borrar Empresa",
                Content = "Confirme borrado de la Empresa!",
                PrimaryButtonText = "Borrar",
                CloseButtonText = "Cancelar"
            };

            ContentDialogResult result = await AvisoConfirmacionPersonalDialog.ShowAsync();

            BorrarSiNo = result == ContentDialogResult.Primary;
        }

        private void LvrCalculoHashEmpresa(object sender, TextChangedEventArgs e)
        {
            string TempTexto = LvrCrypto.LvrRegionGeografica() + textBoxRut.Text + "-" + textBoxDigitoVerificador.Text + LvrCrypto.LvrGenRandomData(5);
            EmpresaIO.PENTALPHA = LvrCrypto.LvrByteArrayToString(LvrCrypto.LvrCalculoSHA256(TempTexto));
        }

        private void LvrCalculoHashEmpresaDig(object sender, TextChangedEventArgs e)
        {
            string TempTexto = LvrCrypto.LvrRegionGeografica() + textBoxRut.Text + "-" + textBoxDigitoVerificador.Text + LvrCrypto.LvrGenRandomData(5);
            EmpresaIO.PENTALPHA = LvrCrypto.LvrByteArrayToString(LvrCrypto.LvrCalculoSHA256(TempTexto));
        }

        private async void BtnCodigoQR(object sender, RoutedEventArgs e)
        {

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(EmpresaIO.PENTALPHA, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);

            byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);

            InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();

            DataWriter writer = new DataWriter(stream.GetOutputStreamAt(0));
            writer.WriteBytes(qrCodeAsPngByteArr);
            await writer.StoreAsync();
            var image = new BitmapImage();
            await image.SetSourceAsync(stream);

            imageQrEmpresa.Source = image;
        }

        //**************************************************
        // Ejecuta operacion de registro de empresa
        //**************************************************
        private bool ProRegistroEmpresa(string pTipoOperacion)
        {
            string LvrPRecibirServer;
            string LvrPData;
            string LvrStringHttp = "https://finanven.ddns.net";
            string LvrStringPort = "443";
            string LvrStringController = "/Api/BikeMessengerEmpresa";

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

            LvrBKInternet.LvrInetPOST(LvrStringHttp, LvrStringPort, LvrStringController, LvrParametros);
            LvrPRecibirServer = LvrBKInternet.LvrResultadoWeb;

            if (LvrPRecibirServer != "ERROR" && LvrPRecibirServer != "" && LvrPRecibirServer != null)
            {
                // Procesar primer servidor
                RecibirJsonEmpresaArray = JsonConvert.DeserializeObject<List<JsonBikeMessengerEmpresa>>(LvrPRecibirServer); // resp será el string JSON a deserializa
                RecibirJsonEmpresa = RecibirJsonEmpresaArray[0];

                return RecibirJsonEmpresa.RESOPERACION == "OK";

            }
            return false;
        }

        private void CopiarMemoriaEnJson(string pOPERACION)
        {
            // Llenar Variables
            EnviarJsonEmpresa.OPERACION = pOPERACION;
            EnviarJsonEmpresa.PENTALPHA = EmpresaIO.PENTALPHA;
            EnviarJsonEmpresa.RUTID = EmpresaIO.RUTID;
            EnviarJsonEmpresa.DIGVER = EmpresaIO.DIGVER;
            EnviarJsonEmpresa.NOMBRE = EmpresaIO.NOMBRE;
            EnviarJsonEmpresa.USUARIO = EmpresaIO.USUARIO;
            EnviarJsonEmpresa.CLAVE = EmpresaIO.CLAVE;
            EnviarJsonEmpresa.ACTIVIDAD1 = EmpresaIO.ACTIVIDAD1;
            EnviarJsonEmpresa.ACTIVIDAD2 = EmpresaIO.ACTIVIDAD2;
            EnviarJsonEmpresa.REPRESENTANTE1 = EmpresaIO.REPRESENTANTE1;
            EnviarJsonEmpresa.REPRESENTANTE2 = EmpresaIO.REPRESENTANTE2;
            EnviarJsonEmpresa.REPRESENTANTE3 = EmpresaIO.REPRESENTANTE3;
            EnviarJsonEmpresa.DOMICILIO1 = EmpresaIO.DOMICILIO1;
            EnviarJsonEmpresa.DOMICILIO2 = EmpresaIO.DOMICILIO2;
            EnviarJsonEmpresa.NUMERO = EmpresaIO.NUMERO;
            EnviarJsonEmpresa.PISO = EmpresaIO.PISO;
            EnviarJsonEmpresa.OFICINA = EmpresaIO.OFICINA;
            EnviarJsonEmpresa.CIUDAD = EmpresaIO.CIUDAD;
            EnviarJsonEmpresa.COMUNA = EmpresaIO.COMUNA;
            EnviarJsonEmpresa.ESTADOREGION = EmpresaIO.ESTADOREGION;
            EnviarJsonEmpresa.CODIGOPOSTAL = EmpresaIO.CODIGOPOSTAL;
            EnviarJsonEmpresa.PAIS = EmpresaIO.PAIS;
            EnviarJsonEmpresa.OBSERVACIONES = EmpresaIO.OBSERVACIONES;
            EnviarJsonEmpresa.LOGO = EmpresaIO.LOGO;
            EnviarJsonEmpresa.RESOPERACION = "";
            EnviarJsonEmpresa.RESMENSAJE = "";
        }

        private void CopiarJsonEnMemoria(string pOPERACION)
        {
            // Proceso
            // RecibirJsonEmpresa.Json_OPERACION = pOPERACION;
            EmpresaIO.PENTALPHA = RecibirJsonEmpresa.PENTALPHA;
            EmpresaIO.RUTID = RecibirJsonEmpresa.RUTID;
            EmpresaIO.DIGVER = RecibirJsonEmpresa.DIGVER;
            EmpresaIO.NOMBRE = RecibirJsonEmpresa.NOMBRE;
            EmpresaIO.USUARIO = RecibirJsonEmpresa.USUARIO;
            EmpresaIO.CLAVE = RecibirJsonEmpresa.CLAVE;
            EmpresaIO.ACTIVIDAD1 = RecibirJsonEmpresa.ACTIVIDAD1;
            EmpresaIO.ACTIVIDAD2 = RecibirJsonEmpresa.ACTIVIDAD2;
            EmpresaIO.REPRESENTANTE1 = RecibirJsonEmpresa.REPRESENTANTE1;
            EmpresaIO.REPRESENTANTE2 = RecibirJsonEmpresa.REPRESENTANTE2;
            EmpresaIO.REPRESENTANTE3 = RecibirJsonEmpresa.REPRESENTANTE3;
            EmpresaIO.DOMICILIO1 = RecibirJsonEmpresa.DOMICILIO1;
            EmpresaIO.DOMICILIO2 = RecibirJsonEmpresa.DOMICILIO2;
            EmpresaIO.NUMERO = RecibirJsonEmpresa.NUMERO;
            EmpresaIO.PISO = RecibirJsonEmpresa.PISO;
            EmpresaIO.OFICINA = RecibirJsonEmpresa.OFICINA;
            EmpresaIO.CIUDAD = RecibirJsonEmpresa.CIUDAD;
            EmpresaIO.COMUNA = RecibirJsonEmpresa.COMUNA;
            EmpresaIO.ESTADOREGION = RecibirJsonEmpresa.ESTADOREGION;
            EmpresaIO.CODIGOPOSTAL = RecibirJsonEmpresa.CODIGOPOSTAL;
            EmpresaIO.PAIS = RecibirJsonEmpresa.PAIS;
            EmpresaIO.OBSERVACIONES = RecibirJsonEmpresa.OBSERVACIONES;
            EmpresaIO.LOGO = RecibirJsonEmpresa.LOGO;
            // BM_Database_Empresa = RecibirJsonEmpresa.Json_Resultado;
            // BM_Database_Empresa = RecibirJsonEmpresa.Json_ResultadoMsg;
        }

        private void LlenarBasePentalpha(string pPentalpha)
        {
            // Valores de Empresa
            LvrTransferVar.PENTALPHA_ID = pPentalpha;
            LvrTransferVar.EMP_PENTALPHA = pPentalpha;
            // Valores de Personal
            LvrTransferVar.PER_PENTALPHA = pPentalpha;
            // Valores de Recursos
            LvrTransferVar.REC_PENTALPHA = pPentalpha;
            // Valores de Clientes
            LvrTransferVar.CLI_PENTALPHA = pPentalpha;
            // Valores de SERVICIOS
            LvrTransferVar.SER_PENTALPHA = pPentalpha;
            
            LvrTransferVar.EscribirValoresDeAjustes();
            LvrTransferVar.LeerValoresDeAjustes();
        }
    }
}
