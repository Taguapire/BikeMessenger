﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using QRCoder;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageEmpresa : Page
    {
        private List<StructBikeMessengerEmpresa> EmpresaIOArray = new List<StructBikeMessengerEmpresa>();
        private StructBikeMessengerEmpresa EmpresaIO = new StructBikeMessengerEmpresa();
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

            // EmpresaIOArray = BM_Database_Empresa.BuscarEmpresa(LvrTransferVar.PENTALPHA_ID);
            EmpresaIOArray = BM_Database_Empresa.BuscarEmpresa();
            if (EmpresaIOArray != null && EmpresaIOArray.Count > 0)
            {
                EmpresaIO = EmpresaIOArray[0];

                if (EmpresaIO.RESOPERACION == "OK")
                {
                    LlenarPantallaConDb();

                    LvrTransferVar.PENTALPHA_ID = EmpresaIO.PENTALPHA;
                    LvrTransferVar.EMP_PENTALPHA = EmpresaIO.PENTALPHA;
                    LvrTransferVar.EMP_NOMBRE = EmpresaIO.NOMBRE;
                    LvrTransferVar.EMP_RUTID = EmpresaIO.RUTID;
                    LvrTransferVar.EMCLI_DIGVER = EmpresaIO.DIGVER;

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

                // Llenado de Pais
                comboBoxPais.SelectedValue = EmpresaIO.PAIS;

                // Llenado de Estado o Region
                comboBoxEstado.SelectedValue = EmpresaIO.ESTADOREGION;

                // Llenado de Comuna o Municipio
                comboBoxComuna.SelectedValue = EmpresaIO.COMUNA;

                // Llenado de Ciudad
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
            _ = LocalRellenarCombos.AgregarPais(EmpresaIO.PAIS);

            // Actualizar Combo Region
            _ = LocalRellenarCombos.AgregarRegion(EmpresaIO.ESTADOREGION);

            // Actualizar Combo Comuna
            _ = LocalRellenarCombos.AgregarComuna(EmpresaIO.COMUNA);

            // Actualizar Combo Ciudad
            _ = LocalRellenarCombos.AgregarCiudad(EmpresaIO.CIUDAD);
        }

        private async Task LlenarDbConPantallaAsync()
        {
            EmpresaIO.RUTID = textBoxRut.Text;
            EmpresaIO.DIGVER = textBoxDigitoVerificador.Text;
            EmpresaIO.NOMBRE = textBoxNombreEmpresa.Text;
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

            try
            {
                EmpresaIO.LOGO = await ConvertirImageABase64Async();
            }
            catch (ArgumentException)
            {
                EmpresaIO.LOGO = "";
            }
        }

        private async void BtnAgregarEmpresa(object sender, RoutedEventArgs e)
        {
            await LlenarDbConPantallaAsync();

            if (BM_Database_Empresa.AgregarEmpresa(EmpresaIO))
            {
                LvrTransferVar.PENTALPHA_ID = EmpresaIO.PENTALPHA;
                LvrTransferVar.PENTALPHA_ID = EmpresaIO.PENTALPHA;
                LvrTransferVar.EMP_PENTALPHA = EmpresaIO.PENTALPHA;
                LvrTransferVar.EMP_NOMBRE = EmpresaIO.NOMBRE;
                LvrTransferVar.EMP_RUTID = EmpresaIO.RUTID;
                LvrTransferVar.EMCLI_DIGVER = EmpresaIO.DIGVER;

                LvrTransferVar.ActualizarPentalphaId();
                LvrTransferVar.EscribirValoresDeAjustes();
                LvrTransferVar.LeerValoresDeAjustes();

                ActualizarCombos();

                appBarAgregar.IsEnabled = false;
                appBarModificar.IsEnabled = true;
                appBarBorrar.IsEnabled = true;


                textBoxRut.IsReadOnly = true;
                textBoxDigitoVerificador.IsReadOnly = true;

                await AvisoOperacionEmpresaDialogAsync("Agregar Empresa", "Empresa agregada exitosamente.");
            }
            else
            {
                await AvisoOperacionEmpresaDialogAsync("Agregar Empresa", "Error en ingreso de la empresa. Reintente o escriba a soporte contacto@pentalpha.net");
            }
        }

        private async void BtnModificarEmpresa(object sender, RoutedEventArgs e)
        {
            await LlenarDbConPantallaAsync();

            if (BM_Database_Empresa.ModificarEmpresa(EmpresaIO))
            {
                LvrTransferVar.PENTALPHA_ID = EmpresaIO.PENTALPHA;
                LvrTransferVar.EMP_PENTALPHA = EmpresaIO.PENTALPHA;
                LvrTransferVar.EMP_NOMBRE = EmpresaIO.NOMBRE;
                LvrTransferVar.EMP_RUTID = EmpresaIO.RUTID;
                LvrTransferVar.EMCLI_DIGVER = EmpresaIO.DIGVER;
                LvrTransferVar.ActualizarPentalphaId();
                LvrTransferVar.EscribirValoresDeAjustes();
                LvrTransferVar.LeerValoresDeAjustes();

                await AvisoOperacionEmpresaDialogAsync("Modificar Empresa", "Empresa modificada exitosamente.");
            }
            else
            {
                await AvisoOperacionEmpresaDialogAsync("Modificar Empresa", "Error en modificación de la empresa. Reintente o escriba a soporte contacto@pentalpha.net");
            }
        }

        private async void BtnBorrarEmpresa(object sender, RoutedEventArgs e)
        {
            BorrarSiNo = false;

            await AvisoBorrarEmpresaDialogAsync();

            if (!BorrarSiNo)
            {
                return;
            }

            await LlenarDbConPantallaAsync();

            if (BM_Database_Empresa.BorrarEmpresa(EmpresaIO.PENTALPHA))
            {
                LvrTransferVar.PENTALPHA_ID = "";
                LvrTransferVar.EMP_PENTALPHA = "";
                LvrTransferVar.EMP_NOMBRE = "";
                LvrTransferVar.EMP_RUTID = "";
                LvrTransferVar.EMCLI_DIGVER = "";
                LvrTransferVar.ActualizarPentalphaId();
                LvrTransferVar.EscribirValoresDeAjustes();
                LvrTransferVar.LeerValoresDeAjustes();

                appBarAgregar.IsEnabled = true;
                appBarModificar.IsEnabled = false;
                appBarBorrar.IsEnabled = false;

                textBoxRut.IsReadOnly = false;
                textBoxDigitoVerificador.IsReadOnly = false;

                await AvisoOperacionEmpresaDialogAsync("Borrar Empresa", "Empresa borrada exitosamente.");
            }
            else
            {
                await AvisoOperacionEmpresaDialogAsync("Borrar Empresa", "Error en borrado de la empresa. Reintente o escriba a soporte contacto@pentalpha.net");
            }
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
