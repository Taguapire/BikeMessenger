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
    public sealed partial class PageRecursos : Page
    {
        Bm_Recursos_Database BM_Database_Recursos = new Bm_Recursos_Database();
        TransferVar LvrTransferVar;
        bool BorrarSiNo;

        public PageRecursos()
        {
            this.InitializeComponent();
            comboBoxTipo.Items.Add("BICICLETA");
            comboBoxTipo.Items.Add("BICIMOTO");
            comboBoxTipo.Items.Add("MOTOCICLETA");
            comboBoxTipo.Items.Add("AUTOMOVIL");
            comboBoxTipo.Items.Add("FURGON");
            comboBoxTipo.Items.Add("CAMBION");
            comboBoxTipo.Items.Add("VEHICULO MARITIMO");
            comboBoxTipo.Items.Add("VEHICULO AEREO");
            comboBoxTipo.Items.Add("OTROS");
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
                BM_Database_Recursos.BM_CreateDatabase(LvrTransferVar.TV_Connection);

                if (LvrTransferVar.R_PAT_SER == "")
                {
                    if (BM_Database_Recursos.Bm_Recursos_Buscar())
                    {
                        LlenarPantallaConDb();
                    }
                }
                else
                {
                    if (BM_Database_Recursos.Bm_Recursos_Buscar(LvrTransferVar.R_PAT_SER))
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
            this.Frame.Navigate(typeof(PageAjustes), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarServicios(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageServicios), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarClientes(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageClientes), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarRecursos(object sender, RoutedEventArgs e)
        {
            // this.Frame.Navigate(typeof(PageRecursos), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarEmpresa(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageEmpresa), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarPersonal(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PagePersonal), LvrTransferVar, new SuppressNavigationTransitionInfo());
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
                using (Windows.Storage.Streams.IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap.
                    Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage();

                    bitmapImage.SetSource(fileStream);
                    imageFotoRecurso.Source = bitmapImage;
                }
            }
        }

        private async void LlenarPantallaConDb()
        {
            try
            {
                LvrTransferVar.R_RUTID = BM_Database_Recursos.BK_RUTID;
                LvrTransferVar.R_DIGVER = BM_Database_Recursos.BK_DIGVER;
                LvrTransferVar.R_PAT_SER = BM_Database_Recursos.BK_PATENTE;
                textBoxRut.Text = BM_Database_Recursos.BK_RUTID;
                textBoxDigitoVerificador.Text = BM_Database_Recursos.BK_DIGVER;
                textBoxPropietario.Text = BM_Database_Recursos.BK_PROPIETARIO;

                comboBoxTipo.SelectedValue = BM_Database_Recursos.BK_TIPO;

                textBoxPatenteCodigo.Text = BM_Database_Recursos.BK_PATENTE;
                textBoxMarca.Text = BM_Database_Recursos.BK_MARCA;
                textBoxModelo.Text = BM_Database_Recursos.BK_MODELO;
                textBoxVariante.Text = BM_Database_Recursos.BK_VARIANTE;
                textBoxAno.Text = BM_Database_Recursos.BK_ANO;
                textBoxColor.Text = BM_Database_Recursos.BK_COLOR;

                if (BM_Database_Recursos.Bm_E_Pais_EjecutarSelect())
                {
                    while (BM_Database_Recursos.Bm_E_Pais_Buscar())
                    {
                        comboBoxPais.Items.Add(BM_Database_Recursos.BK_E_PAIS);
                    }
                }
                comboBoxPais.Items.Add(BM_Database_Recursos.BK_PAIS);
                comboBoxPais.SelectedValue = BM_Database_Recursos.BK_PAIS;

                comboBoxEstado.Items.Add(BM_Database_Recursos.BK_REGION);
                comboBoxEstado.SelectedValue = BM_Database_Recursos.BK_REGION;

                comboBoxComuna.Items.Add(BM_Database_Recursos.BK_COMUNA);
                comboBoxComuna.SelectedValue = BM_Database_Recursos.BK_COMUNA;

                comboBoxCiudad.Items.Add(BM_Database_Recursos.BK_CIUDAD);
                comboBoxCiudad.SelectedValue = BM_Database_Recursos.BK_CIUDAD;

                textBoxObservaciones.Text = BM_Database_Recursos.BK_OBSERVACIONES;

                imageFotoRecurso.Source = Base64StringToBitmap(BM_Database_Recursos.BK_FOTO);
            }
            catch (System.ArgumentNullException)
            {
                await ErrorDeRecuperacionDialogAsync();
            }
        }

        private void LimpiarPantalla()
        {
            LvrTransferVar.R_RUTID = "";
            LvrTransferVar.R_DIGVER = "";
            LvrTransferVar.R_PAT_SER = "";
            textBoxRut.Text = "";
            textBoxDigitoVerificador.Text = "";
            textBoxPropietario.Text = "";
            comboBoxTipo.Text = "";
            textBoxPatenteCodigo.Text = "";
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

        private async System.Threading.Tasks.Task LlenarDbConPantallaAsync()
        {
            BM_Database_Recursos.BK_FOTO = await ConvertirImageABase64Async();
            BM_Database_Recursos.BK_RUTID = textBoxRut.Text;
            BM_Database_Recursos.BK_DIGVER = textBoxDigitoVerificador.Text;
            BM_Database_Recursos.BK_PROPIETARIO = textBoxPropietario.Text;
            BM_Database_Recursos.BK_TIPO = comboBoxTipo.Text;
            BM_Database_Recursos.BK_PATENTE = textBoxPatenteCodigo.Text;
            BM_Database_Recursos.BK_MARCA = textBoxMarca.Text;
            BM_Database_Recursos.BK_MODELO = textBoxModelo.Text;
            BM_Database_Recursos.BK_VARIANTE = textBoxVariante.Text;
            BM_Database_Recursos.BK_ANO = textBoxAno.Text;
            BM_Database_Recursos.BK_COLOR = textBoxColor.Text;
            BM_Database_Recursos.BK_CIUDAD = comboBoxCiudad.Text;
            BM_Database_Recursos.BK_COMUNA = comboBoxComuna.Text;
            BM_Database_Recursos.BK_REGION = comboBoxEstado.Text;
            BM_Database_Recursos.BK_PAIS = comboBoxPais.Text;
            BM_Database_Recursos.BK_OBSERVACIONES = textBoxObservaciones.Text;
        }

        private async void BtnAgregarRecursos(object sender, RoutedEventArgs e)
        {
            try
            {
                await LlenarDbConPantallaAsync();

                if (BM_Database_Recursos.Bm_Recursos_Agregar())
                {
                    LlenarListaRecursos();
                    LlenarListaPropietarios();
                    LvrTransferVar.R_RUTID = BM_Database_Recursos.BK_RUTID;
                    LvrTransferVar.R_DIGVER = BM_Database_Recursos.BK_DIGVER;
                    LvrTransferVar.R_PAT_SER = BM_Database_Recursos.BK_PATENTE;
                    await AvisoOperacionRecursosDialogAsync("Agregar Recursos", "Operación completada con exito.");
                }
                else
                {
                    await AvisoOperacionRecursosDialogAsync("Agregando Recursos", "Se a producido un error al intentar agregar recurso.");
                }
            }
            catch (System.ArgumentException)
            {
                await AvisoOperacionRecursosDialogAsync("Agregando Recursos", "Aun faltan datos por completar.");
            }
        }

        private async void BtnModificarRecursos(object sender, RoutedEventArgs e)
        {
            await LlenarDbConPantallaAsync();
            if (BM_Database_Recursos.Bm_Recursos_Modificar(BM_Database_Recursos.BK_PATENTE))
            {
                LlenarListaRecursos();
                LlenarListaPropietarios();
                LvrTransferVar.R_RUTID = BM_Database_Recursos.BK_RUTID;
                LvrTransferVar.R_DIGVER = BM_Database_Recursos.BK_DIGVER;
                LvrTransferVar.R_PAT_SER = BM_Database_Recursos.BK_PATENTE;
                await AvisoOperacionRecursosDialogAsync("Modificar Recursos", "Operación completada con exito.");
            }
            else
            {
                await AvisoOperacionRecursosDialogAsync("Modificando Recursos", "Se a producido un error al intentar modificar recurso.");
            }
        }


        private async void BtnBorrarRecursos(object sender, RoutedEventArgs e)
        {
            BorrarSiNo = false;

            await AvisoBorrarRecursoDialogAsync();

            if (!BorrarSiNo)
                return;

            try
            {
                if (BM_Database_Recursos.Bm_Recursos_Borrar(BM_Database_Recursos.BK_PATENTE))
                {
                    await AvisoOperacionRecursosDialogAsync("Borrando Recursos", "Operación completada con exito.");

                    textBoxPatenteCodigo.IsReadOnly = false;

                    if (BM_Database_Recursos.Bm_Recursos_Buscar())
                    {
                        LvrTransferVar.R_RUTID = BM_Database_Recursos.BK_RUTID;
                        LvrTransferVar.R_DIGVER = BM_Database_Recursos.BK_DIGVER;
                        LvrTransferVar.R_PAT_SER = BM_Database_Recursos.BK_PATENTE;
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
                }
                else
                {
                    await AvisoOperacionRecursosDialogAsync("Borrando Recursos", "Se a producido un error al intentar borrar personal.");
                }
            }
            catch (System.ArgumentException)
            {
                await AvisoOperacionRecursosDialogAsync("Acceso a Base de Datos", "Debe llenar los datos del personal.");
            }
            BorrarSiNo = false;
        }

        private void BtnSalirRecursos(object sender, RoutedEventArgs e)
        {
            LvrTransferVar.TV_Connection.Close();
            Application.Current.Exit();
        }

        private async System.Threading.Tasks.Task<string> ConvertirImageABase64Async()
        {
            var bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(imageFotoRecurso);

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

        private async System.Threading.Tasks.Task AvisoBorrarRecursoDialogAsync()
        {
            ContentDialog AvisoConfirmacionRecursoDialog = new ContentDialog
            {
                Title = "Borrar Recurso",
                Content = "Confirme borrado del registro actual!",
                PrimaryButtonText = "Borrar",
                CloseButtonText = "Cancelar"
            };

            ContentDialogResult result = await AvisoConfirmacionRecursoDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
                BorrarSiNo = true;
            else
                BorrarSiNo = false;
        }

        private async System.Threading.Tasks.Task ErrorDeRecuperacionDialogAsync()
        {
            ContentDialog noErrorRecuperacionDialog = new ContentDialog
            {
                Title = "Acceso a Base de Datos",
                Content = "El registro a recuperar tiene valores nulos.",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noErrorRecuperacionDialog.ShowAsync();
        }

        private void dataGridSeleccion_Propietario(object sender, SelectionChangedEventArgs e)
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
            catch (System.ArgumentOutOfRangeException)
            {
                ;
            }
        }

        private void LlenarListaPropietarios()
        {
            List<GridPropietarioIndividual> GridPropietariosLista = new List<GridPropietarioIndividual>();
            if (BM_Database_Recursos.Bm_Personal_BuscarGrid())
            {
                while (BM_Database_Recursos.Bm_Personal_BuscarGridProxima())
                {
                    GridPropietariosLista.Add(
                        new GridPropietarioIndividual
                        {
                            RUT = BM_Database_Recursos.BK_GRID_RUT,
                            APELLIDO = BM_Database_Recursos.BK_GRID_APELLIDOS,
                            NOMBRE = BM_Database_Recursos.BK_GRID_NOMBRES
                        });
                }
            }
            dataGridListadoPropietarios.IsReadOnly = true;
            dataGridListadoPropietarios.ItemsSource = GridPropietariosLista;
        }

        private void dataGridRecursos_Seleccion(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid CeldaSeleccionada = sender as DataGrid;
                GridRecursoIndividual Fila = (GridRecursoIndividual)CeldaSeleccionada.SelectedItems[0];
                if (BM_Database_Recursos.Bm_Recursos_Buscar(Fila.PATENTE))
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
            catch (System.ArgumentOutOfRangeException)
            {
                ;
            }
        }

        private void LlenarListaRecursos()
        {
            List<GridRecursoIndividual> GridRecursoLista = new List<GridRecursoIndividual>();
            if (BM_Database_Recursos.Bm_Recursos_BuscarGrid())
            {
                while (BM_Database_Recursos.Bm_Recursos_BuscarGridProxima())
                {
                    GridRecursoLista.Add(
                        new GridRecursoIndividual
                        {
                            PATENTE = BM_Database_Recursos.BK_RGRID_PATENTE,
                            TIPO = BM_Database_Recursos.BK_RGRID_TIPO,
                            MARCA = BM_Database_Recursos.BK_RGRID_MARCA,
                            MODELO = BM_Database_Recursos.BK_RGRID_MODELO,
                            CIUDAD = BM_Database_Recursos.BK_RGRID_CIUDAD
                        });
                }
            }
            dataGridListadoRecursos.IsReadOnly = true;
            dataGridListadoRecursos.ItemsSource = GridRecursoLista;
        }

        private async System.Threading.Tasks.Task AvisoOperacionRecursosDialogAsync(string xTitulo, string xDescripcion)
        {
            try
            {
                ContentDialog AvisoOperacionRecursosDialog = new ContentDialog
                {
                    Title = xTitulo,
                    Content = xDescripcion,
                    CloseButtonText = "Continuar"
                };
                ContentDialogResult result = await AvisoOperacionRecursosDialog.ShowAsync();
            }
            catch (System.Exception)
            {
                ;
            }
        }
    }

    public class GridPropietarioIndividual
    {
        public string RUT { get; set; }
        public string APELLIDO { get; set; }
        public string NOMBRE { get; set; }
    }

    public class GridRecursoIndividual
    {
        public string PATENTE { get; set; }
        public string TIPO { get; set; }
        public string MARCA { get; set; }
        public string MODELO { get; set; }
        public string CIUDAD { get; set; }
    }
}
