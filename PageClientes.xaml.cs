using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        Bm_Clientes_Database BM_Database_Clientes = new Bm_Clientes_Database();
        TransferVar LvrTransferVar;
        bool BorrarSiNo;

        public PageClientes()
        {
            // this.BM_Connection = BM_Connection;
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs navigationEvent)
        {
            // call the original OnNavigatingFrom
            base.OnNavigatingFrom(navigationEvent);

            // when the dialog is removed from navigation stack 
            if (navigationEvent.NavigationMode == NavigationMode.Back)
            {
                // set the cache mode
                this.NavigationCacheMode = NavigationCacheMode.Disabled;

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

            if (navigationEvent.Parameter is string && !string.IsNullOrWhiteSpace((string)navigationEvent.Parameter))
            {
                //greeting.Text = $"Hi, {e.Parameter.ToString()}";
            }
            else
            {
                LvrTransferVar = (TransferVar)navigationEvent.Parameter;
                BM_Database_Clientes.BM_CreateDatabase(LvrTransferVar.TV_Connection);

                if (LvrTransferVar.C_RUTID == "")
                {
                    if (BM_Database_Clientes.Bm_Clientes_Buscar())
                    {
                        LlenarPantallaConDb();
                    }
                }
                else
                {
                    if (BM_Database_Clientes.Bm_Clientes_Buscar(LvrTransferVar.C_RUTID, LvrTransferVar.C_DIGVER))
                    {
                        LlenarPantallaConDb();
                    }
                }
                LlenarListaClientes();
            }
        }


        private void BtnSeleccionarAjustes(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageAjustes), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarServicios(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageServicios), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarClientes(object sender, RoutedEventArgs e)
        {
            // this.Frame.Navigate(typeof(PageClientes), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarRecursos(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageRecursos), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarEmpresa(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageEmpresa), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarPersonal(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PagePersonal), LvrTransferVar, new SuppressNavigationTransitionInfo());
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
                using (Windows.Storage.Streams.IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap.
                    Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage();

                    bitmapImage.SetSource(fileStream);
                    imageLogoCliente.Source = bitmapImage;
                }
            }
        }

        private async void LlenarPantallaConDb()
        {
            try
            {
                textBoxRut.Text = BM_Database_Clientes.BK_RUTID;
                textBoxDigitoVerificador.Text = BM_Database_Clientes.BK_DIGVER;
                textBoxNombreCliente.Text = BM_Database_Clientes.BK_NOMBRE;
                textBoxActividad1.Text = BM_Database_Clientes.BK_ACTIVIDAD1;
                textBoxActividad2.Text = BM_Database_Clientes.BK_ACTIVIDAD2;
                textBoxRepresentantes1.Text = BM_Database_Clientes.BK_REPRESENTANTE1;
                textBoxRepresentantes2.Text = BM_Database_Clientes.BK_REPRESENTANTE2;
                textBoxRepresentantes3.Text = BM_Database_Clientes.BK_REPRESENTANTE3;
                textBoxTelefono1.Text = BM_Database_Clientes.BK_TELEFONO1;
                textBoxTelefono2.Text = BM_Database_Clientes.BK_TELEFONO2;
                textBoxDomicilio1.Text = BM_Database_Clientes.BK_DOMICILIO1;
                textBoxDomicilio2.Text = BM_Database_Clientes.BK_DOMICILIO2;
                textBoxNumero.Text = BM_Database_Clientes.BK_NUMERO;
                textBoxPiso.Text = BM_Database_Clientes.BK_PISO;
                textBoxOficina.Text = BM_Database_Clientes.BK_OFICINA;
                textBoxCodigoPostal.Text = BM_Database_Clientes.BK_CODIGOPOSTAL;
                textBoxObservaciones.Text = BM_Database_Clientes.BK_OBSERVACIONES;

                comboBoxPais.SelectedValue = BM_Database_Clientes.BK_PAIS;
                comboBoxEstado.SelectedValue = BM_Database_Clientes.BK_REGION;
                comboBoxComuna.SelectedValue = BM_Database_Clientes.BK_COMUNA;
                comboBoxCiudad.SelectedValue = BM_Database_Clientes.BK_CIUDAD;

                textBoxObservaciones.Text = BM_Database_Clientes.BK_OBSERVACIONES;

                imageLogoCliente.Source = Base64StringToBitmap(BM_Database_Clientes.BK_FOTO);
            }
            catch (System.ArgumentNullException)
            {
                await ErrorDeRecuperacionDialogAsync();
            }
        }

        private void LimpiarPantalla()
        {
            LvrTransferVar.C_RUTID = "";
            LvrTransferVar.C_DIGVER = "";
            textBoxRut.Text = "";
            textBoxDigitoVerificador.Text = "";
            textBoxNombreCliente.Text = "";
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

        private async System.Threading.Tasks.Task LlenarDbConPantallaAsync()
        {
            BM_Database_Clientes.BK_FOTO = await ConvertirImageABase64Async();
            BM_Database_Clientes.BK_RUTID = textBoxRut.Text;
            BM_Database_Clientes.BK_DIGVER = textBoxDigitoVerificador.Text;
            BM_Database_Clientes.BK_NOMBRE = textBoxNombreCliente.Text;
            BM_Database_Clientes.BK_ACTIVIDAD1 = textBoxActividad1.Text;
            BM_Database_Clientes.BK_ACTIVIDAD2 = textBoxActividad2.Text;
            BM_Database_Clientes.BK_REPRESENTANTE1 = textBoxRepresentantes1.Text;
            BM_Database_Clientes.BK_REPRESENTANTE2 = textBoxRepresentantes2.Text;
            BM_Database_Clientes.BK_REPRESENTANTE3 = textBoxRepresentantes3.Text;
            BM_Database_Clientes.BK_TELEFONO1 = textBoxTelefono1.Text;
            BM_Database_Clientes.BK_TELEFONO2 = textBoxTelefono2.Text;
            BM_Database_Clientes.BK_DOMICILIO1 = textBoxDomicilio1.Text;
            BM_Database_Clientes.BK_DOMICILIO2 = textBoxDomicilio2.Text;
            BM_Database_Clientes.BK_NUMERO = textBoxNumero.Text;
            BM_Database_Clientes.BK_PISO = textBoxPiso.Text;
            BM_Database_Clientes.BK_OFICINA = textBoxOficina.Text;
            BM_Database_Clientes.BK_CODIGOPOSTAL = textBoxCodigoPostal.Text;
            BM_Database_Clientes.BK_PAIS = comboBoxPais.Text;
            BM_Database_Clientes.BK_REGION = comboBoxEstado.Text;
            BM_Database_Clientes.BK_COMUNA = comboBoxComuna.Text;
            BM_Database_Clientes.BK_CIUDAD = comboBoxCiudad.Text;
            BM_Database_Clientes.BK_OBSERVACIONES = textBoxObservaciones.Text;
        }

        private async void BtnAgregarClientes(object sender, RoutedEventArgs e)
        {
            try
            {
                await LlenarDbConPantallaAsync();
                if (BM_Database_Clientes.Bm_Clientes_Agregar())
                {
                    LlenarListaClientes();
                    LvrTransferVar.C_RUTID = BM_Database_Clientes.BK_RUTID;
                    LvrTransferVar.C_DIGVER = BM_Database_Clientes.BK_DIGVER;
                    await AvisoOperacionClientesDialogAsync("Agregar Clientes", "Operación completada con exito.");
                }
                else
                {
                    await AvisoOperacionClientesDialogAsync("Agregando Clientes", "Se a producido un error al intentar agregar clientes.");
                }
            }
            catch (System.ArgumentException)
            {
                await AvisoOperacionClientesDialogAsync("Agregando Clientes", "Aun faltan datos por completar.");
            }
        }

        private async void BtnModificarClientes(object sender, RoutedEventArgs e)
        {
            await LlenarDbConPantallaAsync();
            if (BM_Database_Clientes.Bm_Clientes_Modificar(BM_Database_Clientes.BK_RUTID, BM_Database_Clientes.BK_DIGVER))
            {
                LlenarListaClientes();
                LvrTransferVar.C_RUTID = BM_Database_Clientes.BK_RUTID;
                LvrTransferVar.C_DIGVER = BM_Database_Clientes.BK_DIGVER;
                await AvisoOperacionClientesDialogAsync("Modificar Clientes", "Operación completada con exito.");
            }
            else
            {
                await AvisoOperacionClientesDialogAsync("Modificando Clientes", "Se a producido un error al intentar modificar clientes.");
            }
        }

        private async void BtnBorrarClientes(object sender, RoutedEventArgs e)
        {
            BorrarSiNo = false;

            await AvisoBorrarClientesDialogAsync();

            if (!BorrarSiNo)
                return;

            try
            {
                if (BM_Database_Clientes.Bm_Clientes_Borrar(BM_Database_Clientes.BK_RUTID, BM_Database_Clientes.BK_DIGVER))
                {
                    await AvisoOperacionClientesDialogAsync("Borrando Clientes", "Operación completada con exito.");

                    textBoxRut.IsReadOnly = false;
                    textBoxDigitoVerificador.IsReadOnly = false;

                    if (BM_Database_Clientes.Bm_Clientes_Buscar())
                    {
                        LvrTransferVar.C_RUTID = BM_Database_Clientes.BK_RUTID;
                        LvrTransferVar.C_DIGVER = BM_Database_Clientes.BK_DIGVER;
                    }
                    else
                    {
                        LvrTransferVar.P_RUTID = "";
                        LvrTransferVar.P_DIGVER = "";
                    }

                    LlenarPantallaConDb();
                    LlenarListaClientes();
                }
                else
                {
                    await AvisoOperacionClientesDialogAsync("Borrando Clientes", "Se a producido un error al intentar borrar cliente.");
                }
            }
            catch (System.ArgumentException)
            {
                await AvisoOperacionClientesDialogAsync("Acceso a Base de Datos", "Debe llenar los datos del cliente.");
            }
            BorrarSiNo = false;
        }

        private void BtnSalirClientes(object sender, RoutedEventArgs e)
        {
            LvrTransferVar.TV_Connection.Close();
            Application.Current.Exit();
        }

        private async System.Threading.Tasks.Task<string> ConvertirImageABase64Async()
        {
            var bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(imageLogoCliente);

            var image = (await bitmap.GetPixelsAsync()).ToArray();
            var width = (uint)bitmap.PixelWidth;
            var height = (uint)bitmap.PixelHeight;

            double dpiX = 96;
            double dpiY = 96;

            var encoded = new Windows.Storage.Streams.InMemoryRandomAccessStream();
            var encoder = await Windows.Graphics.Imaging.BitmapEncoder.CreateAsync(Windows.Graphics.Imaging.BitmapEncoder.PngEncoderId, encoded);

            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, width, height, dpiX, dpiY, image);
            await encoder.FlushAsync();
            encoded.Seek(0);

            var bytes = new byte[encoded.Size];
            await encoded.AsStream().ReadAsync(bytes, 0, bytes.Length);

            var base64String = Convert.ToBase64String(bytes);

            return base64String;
        }

        private BitmapImage Base64StringToBitmap(string source)
        {
            var ims = new InMemoryRandomAccessStream();
            var bytes = Convert.FromBase64String(source);
            var dataWriter = new DataWriter(ims);
            dataWriter.WriteBytes(bytes);
            _ = dataWriter.StoreAsync();
            ims.Seek(0);
            var img = new BitmapImage();
            img.SetSource(ims);
            return img;
        }


        private async System.Threading.Tasks.Task ErrorDeRecuperacionDialogAsync()
        {
            ContentDialog noErrorRecuperacionDialog = new ContentDialog
            {
                Title = "Error",
                Content = "El registro a recuperar tiene valores nulos.",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noErrorRecuperacionDialog.ShowAsync();
        }

        private async System.Threading.Tasks.Task AvisoOperacionClientesDialogAsync(string xTitulo, string xDescripcion)
        {
            try
            {
                ContentDialog AvisoOperacionClientesDialog = new ContentDialog
                {
                    Title = xTitulo,
                    Content = xDescripcion,
                    CloseButtonText = "Continuar"
                };
                ContentDialogResult result = await AvisoOperacionClientesDialog.ShowAsync();
            }
            catch (System.Exception)
            {
                ;
            }
        }

        private async System.Threading.Tasks.Task AvisoBorrarClientesDialogAsync()
        {
            ContentDialog AvisoConfirmacionClientesDialog = new ContentDialog
            {
                Title = "Borrar Persona",
                Content = "Confirme borrado del registro actual!",
                PrimaryButtonText = "Borrar",
                CloseButtonText = "Cancelar"
            };

            ContentDialogResult result = await AvisoConfirmacionClientesDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
                BorrarSiNo = true;
            else
                BorrarSiNo = false;
        }

        private void LlenarListaClientes()
        {
            List<GridClientesIndividual> GridClientesLista = new List<GridClientesIndividual>();
            if (BM_Database_Clientes.Bm_Clientes_BuscarGrid())
            {
                while (BM_Database_Clientes.Bm_Clientes_BuscarGridProxima())
                {
                    GridClientesLista.Add(
                        new GridClientesIndividual
                        {
                            RUT = BM_Database_Clientes.BK_GRID_RUT,
                            CLIENTE = BM_Database_Clientes.BK_GRID_NOMBRES
                        });
                }
            }
            dataGridListadoClientes.IsReadOnly = true;
            dataGridListadoClientes.ItemsSource = GridClientesLista;
        }
        private void dataGridSeleccion_Clientes(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid CeldaSeleccionada = sender as DataGrid;
                GridClientesIndividual Fila = (GridClientesIndividual)CeldaSeleccionada.SelectedItems[0];
                string[] CadenaDividida = Fila.RUT.Split("-", 2, StringSplitOptions.None);

                if (BM_Database_Clientes.Bm_Clientes_Buscar(CadenaDividida[0], CadenaDividida[1]))
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
            catch (System.ArgumentOutOfRangeException)
            {
                ; // No hacer nada, es un control vacio de error
            }
        }
    }

    public class GridClientesIndividual
    {
        public string RUT { get; set; }
        public string CLIENTE { get; set; }
    }
}
