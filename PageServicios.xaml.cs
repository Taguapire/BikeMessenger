using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Windows.Services.Maps;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageServicios : Page
    {
        private readonly JsonBikeMessengerServicio EnviarJsonServicio = new JsonBikeMessengerServicio();
        private JsonBikeMessengerServicio RecibirJsonServicio = new JsonBikeMessengerServicio();
        private readonly Bm_Servicio_Database BM_Database_Servicio = new Bm_Servicio_Database();
        private TransferVar LvrTransferVar;
        private bool BorrarSiNo;

        public PageServicios()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;
            mapControlBikeMessenger.MapServiceToken = "kObotinnsvzUnI3k9Smn~hOr - MYZEy_DGqfvj5V0QHQ~AlzXtL5PnD05eblszjnq7bMEEwk4TSFF3szRn_yyu2GaEo9JehDSttrmHwgRFSzi";
            appBarBuscarRuta.IsEnabled = false;
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
                _ = BM_Database_Servicio.BM_CreateDatabase(LvrTransferVar.TV_Connection);
                RellenarCombos();

                if (LvrTransferVar.X_NROENVIO == "")
                {
                    if (BM_Database_Servicio.Bm_Servicios_Buscar())
                    {
                        LlenarPantallaConDb();
                    }
                }
                else
                {
                    if (BM_Database_Servicio.Bm_Servicios_Buscar(LvrTransferVar.R_PENTALPHA, LvrTransferVar.X_NROENVIO))
                    {
                        LlenarPantallaConDb();
                    }
                }
                LlenarListaEnvios();
                LlenarListaClientes();
                LlenarListaMensajeros();
                LlenarListaRecursos();
            }
        }

        private void BtnSeleccionarAjustes(object sender, RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(PageAjustes), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarServicios(object sender, RoutedEventArgs e)
        {
            // _ = Frame.Navigate(typeof(PageServicios), LvrTransferVar, new SuppressNavigationTransitionInfo());
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
            _ = Frame.Navigate(typeof(PagePersonal), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void RellenarCombos()
        {
            // Limpiar Combo Box
            comboBoxOrigenPais.Items.Clear();
            comboBoxOrigenEstado.Items.Clear();
            comboBoxOrigenComuna.Items.Clear();
            comboBoxOrigenCiudad.Items.Clear();

            comboBoxDestinoPais.Items.Clear();
            comboBoxDestinoEstado.Items.Clear();
            comboBoxDestinoComuna.Items.Clear();
            comboBoxDestinoCiudad.Items.Clear();

            // Llenar Combo Pais
            if (BM_Database_Servicio.Bm_E_Pais_EjecutarSelect())
            {
                while (BM_Database_Servicio.Bm_E_Pais_Buscar())
                {
                    comboBoxOrigenPais.Items.Add(BM_Database_Servicio.BK_E_PAIS);
                    comboBoxDestinoPais.Items.Add(BM_Database_Servicio.BK_E_PAIS);
                }
            }

            // Llenar Combo Region
            if (BM_Database_Servicio.Bm_E_Region_EjecutarSelect())
            {
                while (BM_Database_Servicio.Bm_E_Region_Buscar())
                {
                    comboBoxOrigenEstado.Items.Add(BM_Database_Servicio.BK_E_REGION);
                    comboBoxDestinoEstado.Items.Add(BM_Database_Servicio.BK_E_REGION);
                }
            }

            // Llenar Combo Comuna
            if (BM_Database_Servicio.Bm_E_Comuna_EjecutarSelect())
            {
                while (BM_Database_Servicio.Bm_E_Comuna_Buscar())
                {
                    comboBoxOrigenComuna.Items.Add(BM_Database_Servicio.BK_E_COMUNA);
                    comboBoxDestinoComuna.Items.Add(BM_Database_Servicio.BK_E_COMUNA);
                }
            }

            // Llenar Combo Ciudad
            if (BM_Database_Servicio.Bm_E_Ciudad_EjecutarSelect())
            {
                while (BM_Database_Servicio.Bm_E_Ciudad_Buscar())
                {
                    comboBoxOrigenCiudad.Items.Add(BM_Database_Servicio.BK_E_CIUDAD);
                    comboBoxDestinoCiudad.Items.Add(BM_Database_Servicio.BK_E_CIUDAD);
                }
            }
        }

        private async void BtnAgregarServicios(object sender, RoutedEventArgs e)
        {
            try
            {
                // Muestra de espera
                LvrProgresRing.IsActive = true;
                await Task.Delay(500); // .5 sec delay

                LlenarDbConPantalla();

                if (BM_Database_Servicio.Bm_Servicios_Agregar())
                {
                    LlenarListaEnvios();
                    LvrTransferVar.X_NROENVIO = BM_Database_Servicio.BK_NROENVIO;

                    // Agregar en Servidor Remoto
                    ProRegistroServicio("AGREGAR");
                    // Verificar Operación

                    await AvisoOperacionServiciosDialogAsync("Agregar Servicios", "Operación completada con exito.");
                }
                else
                {
                    await AvisoOperacionServiciosDialogAsync("Agregando Servicios", "Se a producido un error al intentar agregar recurso.");
                }
            }
            catch (ArgumentException)
            {
                await AvisoOperacionServiciosDialogAsync("Agregando Servicios", "Aun faltan datos por completar.");
            }
            catch (IndexOutOfRangeException)
            {
                await AvisoOperacionServiciosDialogAsync("Agregando Servicios", "Datos de ingresos incorrectos o no estan completos.");
            }

            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // .5 sec delay
        }

        private async void BtnModificarServicios(object sender, RoutedEventArgs e)
        {
            // Muestra de espera
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            try
            {
                LlenarDbConPantalla();
                if (BM_Database_Servicio.Bm_Recursos_Modificar(LvrTransferVar.X_PENTALPHA, BM_Database_Servicio.BK_NROENVIO))
                {
                    LlenarListaEnvios();
                    ProRegistroServicio("MODIFICAR");
                    LvrTransferVar.X_NROENVIO = BM_Database_Servicio.BK_NROENVIO;
                    await AvisoOperacionServiciosDialogAsync("Modificar Servicios", "Operación completada con exito.");
                }
                else
                {
                    await AvisoOperacionServiciosDialogAsync("Modificando Servicios", "Se a producido un error al intentar modificar recurso.");
                }
            }
            catch (ArgumentException)
            {
                await AvisoOperacionServiciosDialogAsync("Modificando Servicios", "Aun faltan datos por completar.");
            }
            catch (IndexOutOfRangeException)
            {
                await AvisoOperacionServiciosDialogAsync("Modificando Servicios", "Datos de ingresos incorrectos o no estan completos.");
            }

            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // .5 sec delay
        }

        private async void BtnBorrarServicios(object sender, RoutedEventArgs e)
        {
            BorrarSiNo = false;

            await AvisoBorrarServicioDialogAsync();

            if (!BorrarSiNo)
            {
                return;
            }

            // Muestra de espera
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay

            try
            {
                if (BM_Database_Servicio.Bm_Servicios_Borrar(LvrTransferVar.X_PENTALPHA, BM_Database_Servicio.BK_NROENVIO))
                {
                    ProRegistroServicio("BORRAR");

                    await AvisoOperacionServiciosDialogAsync("Borrando Servicios", "Operación completada con exito.");

                    textBoxNroDeEnvio.IsReadOnly = false;

                    if (BM_Database_Servicio.Bm_Servicios_Buscar())
                    {
                        LvrTransferVar.X_NROENVIO = BM_Database_Servicio.BK_NROENVIO;
                    }
                    else
                    {
                        LvrTransferVar.X_NROENVIO = "";
                    }
                    LlenarListaEnvios();
                    LlenarPantallaConDb();
                }
                else
                {
                    await AvisoOperacionServiciosDialogAsync("Borrando Servicios", "Se a producido un error al intentar borrar personal.");
                }
            }
            catch (ArgumentException)
            {
                await AvisoOperacionServiciosDialogAsync("Acceso a Base de Datos", "Debe llenar los datos del personal.");
            }
            BorrarSiNo = false;
            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // .5 sec delay
        }

        private async void BtnMapaBuscarRutaServicios(object sender, RoutedEventArgs e)
        {
            double lvrlatOrigen = 0;
            double lvrlonOrigen = 0;
            double lvrlatDestino = 0;
            double lvrlonDestino = 0;

            if (textBoxOrigenDomicilio1.Text == "" || textBoxOrigenDomicilio1.Text == "" || comboBoxOrigenComuna.Text == "" || comboBoxOrigenCiudad.Text == "" || comboBoxOrigenPais.Text == "")
            {
                await AvisoOperacionRecursosDialogAsync("Dirección de Origen", "Aun faltan datos por completar.");
                return;
            }

            if (textBoxDestinoDomicilio1.Text == "" || textBoxDestinoDomicilio1.Text == "" || comboBoxDestinoComuna.Text == "" || comboBoxDestinoCiudad.Text == "" || comboBoxDestinoPais.Text == "")
            {
                await AvisoOperacionRecursosDialogAsync("Dirección de Destino", "Aun faltan datos por completar.");
                return;
            }

            string addressInicio = textBoxOrigenDomicilio1.Text + " " + textBoxOrigenNumero.Text + "," + comboBoxOrigenComuna.Text + "," + comboBoxOrigenCiudad.Text + "," + comboBoxOrigenPais.Text;
            string addressFinal = textBoxDestinoDomicilio1.Text + " " + textBoxDestinoNumero.Text + "," + comboBoxDestinoComuna.Text + "," + comboBoxDestinoCiudad.Text + "," + comboBoxDestinoPais.Text;

            MapLocationFinderResult resultsOrigen = await MapLocationFinder.FindLocationsAsync(addressInicio, null);

            if (resultsOrigen.Status == MapLocationFinderStatus.Success)
            {
                lvrlatOrigen = resultsOrigen.Locations[0].Point.Position.Latitude;
                lvrlonOrigen = resultsOrigen.Locations[0].Point.Position.Longitude;
            }

            MapLocationFinderResult resultsDestino = await MapLocationFinder.FindLocationsAsync(addressFinal, null);

            if (resultsDestino.Status == MapLocationFinderStatus.Success)
            {
                lvrlatDestino = resultsDestino.Locations[0].Point.Position.Latitude;
                lvrlonDestino = resultsDestino.Locations[0].Point.Position.Longitude;
            }

            BasicGeoposition startLocation = new BasicGeoposition() { Latitude = lvrlatOrigen, Longitude = lvrlonOrigen };
            BasicGeoposition endLocation = new BasicGeoposition() { Latitude = lvrlatDestino, Longitude = lvrlonDestino };

            MapRouteFinderResult routeResult =
                await MapRouteFinder.GetDrivingRouteAsync(
                new Geopoint(startLocation),
                new Geopoint(endLocation),
                MapRouteOptimization.Time,
                MapRouteRestrictions.None);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route)
                {
                    RouteColor = Colors.Yellow,
                    OutlineColor = Colors.Black
                };

                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                mapControlBikeMessenger.Routes.Add(viewOfRoute);

                // Fit the MapControl to the route.
                _ = await mapControlBikeMessenger.TrySetViewBoundsAsync(
                      routeResult.Route.BoundingBox,
                      null,
                      MapAnimationKind.None);
            }
        }

        private void BtnMapaServicios(object sender, RoutedEventArgs e)
        {
            mapControlBikeMessenger.Style = MapStyle.Road;
            if (mapControlBikeMessenger.Visibility == Visibility.Visible)
            {
                mapControlBikeMessenger.Visibility = Visibility.Collapsed;
                appBarBuscarRuta.IsEnabled = false;
            }
            else
            {
                //mapControlBikeMessenger;
                mapControlBikeMessenger.Visibility = Visibility.Visible;
                appBarBuscarRuta.IsEnabled = true;
            }
        }
        private void LlenarPantallaConDb()
        {
            try
            {
                textBoxNroDeEnvio.Text = BM_Database_Servicio.BK_NROENVIO;
                textBoxGuiaDeDespacho.Text = BM_Database_Servicio.BK_GUIADESPACHO;
                controlFecha.SelectedDate = DateTimeOffset.Parse(BM_Database_Servicio.BK_FECHA);
                controlHora.SelectedTime = TimeSpan.Parse(BM_Database_Servicio.BK_HORA);
                textBoxRutID.Text = BM_Database_Servicio.BK_CLIENTERUT;
                textBoxDigitoVerificador.Text = BM_Database_Servicio.BK_CLIENTEDIGVER;
                textBoxCliente.Text = BM_Database_Servicio.BK_CLIENTE;
                textBoxIdMensajero.Text = BM_Database_Servicio.BK_MENSAJERORUT + "-" + BM_Database_Servicio.BK_MENSAJERODIGVER;
                textBoxNombreMensajero.Text = BM_Database_Servicio.BK_MENSAJERO;
                textBoxIdRecurso.Text = BM_Database_Servicio.BK_RECURSOID;
                textBoxNombreRecurso.Text = BM_Database_Servicio.BK_RECURSO;
                textBoxOrigenDomicilio1.Text = BM_Database_Servicio.BK_ODOMICILIO1;
                //textBoxOrigenDomicilio2.Text = BM_Database_Servicio.BK_ODOMICILIO2;
                textBoxOrigenNumero.Text = BM_Database_Servicio.BK_ONUMERO;
                textBoxOrigenPiso.Text = BM_Database_Servicio.BK_OPISO;
                textBoxOrigenOficina.Text = BM_Database_Servicio.BK_OOFICINA;
                // BM_Database_Servicio.BK_OCOORDENADAS = "*";
                textBoxDestinoDomicilio1.Text = BM_Database_Servicio.BK_DDOMICILIO1;
                // textBoxDestinoDomicilio2.Text = BM_Database_Servicio.BK_DDOMICILIO2;
                textBoxDestinoNumero.Text = BM_Database_Servicio.BK_DNUMERO;
                textBoxDestinoPiso.Text = BM_Database_Servicio.BK_DPISO;
                textBoxDestinoOficina.Text = BM_Database_Servicio.BK_DOFICINA;
                // BM_Database_Servicio.BK_DCOORDENADAS = "*";
                textBoxDescripcion.Text = BM_Database_Servicio.BK_DESCRIPCION;

                textBoxFacturas.Text = BM_Database_Servicio.BK_FACTURAS.ToString();
                textBoxBultos.Text = BM_Database_Servicio.BK_BULTOS.ToString();
                textBoxCompras.Text = BM_Database_Servicio.BK_COMPRAS.ToString();
                textBoxCheques.Text = BM_Database_Servicio.BK_CHEQUES.ToString();
                textBoxSobres.Text = BM_Database_Servicio.BK_SOBRES.ToString();
                textBoxOtros.Text = BM_Database_Servicio.BK_OTROS.ToString();

                textBoxObservaciones.Text = BM_Database_Servicio.BK_OBSERVACIONES;
                textBoxEntrega.Text = BM_Database_Servicio.BK_ENTREGA;
                textBoxRecepcion.Text = BM_Database_Servicio.BK_RECEPCION;
                textBoxTiempoDeEspera.Text = BM_Database_Servicio.BK_TESPERA;
                // BM_Database_Servicio.BK_FECHAENTREGA = "*";
                // BM_Database_Servicio.BK_HORAENTREGA = "*";
                // BM_Database_Servicio.BK_DISTANCIA = "*";
                // BM_Database_Servicio.BK_PROGRAMADO = "*";

                // Asignaciones que deben revisarse
                // BM_Database_Servicio.BK_FECHA = controlFecha.Date.ToString();
                // BM_Database_Servicio.BK_HORA = controlHora.Time.ToString();

                comboBoxOrigenPais.SelectedValue = BM_Database_Servicio.BK_OPAIS;
                comboBoxOrigenEstado.SelectedValue = BM_Database_Servicio.BK_OESTADO;
                comboBoxOrigenComuna.SelectedValue = BM_Database_Servicio.BK_OCOMUNA;
                comboBoxOrigenCiudad.SelectedValue = BM_Database_Servicio.BK_OCIUDAD;

                comboBoxDestinoPais.SelectedValue = BM_Database_Servicio.BK_DPAIS;
                comboBoxDestinoEstado.SelectedValue = BM_Database_Servicio.BK_DESTADO;
                comboBoxDestinoComuna.SelectedValue = BM_Database_Servicio.BK_DCOMUNA;
                comboBoxDestinoCiudad.SelectedValue = BM_Database_Servicio.BK_DCIUDAD;
            }
            catch (ArgumentNullException)
            {
                ErrorDeRecuperacionDialog();
            }
        }

        private void LlenarDbConPantalla()
        {
            BM_Database_Servicio.BK_NROENVIO = textBoxNroDeEnvio.Text;
            BM_Database_Servicio.BK_GUIADESPACHO = textBoxGuiaDeDespacho.Text;
            BM_Database_Servicio.BK_FECHA = controlFecha.Date.Date.ToShortDateString().ToString();
            BM_Database_Servicio.BK_HORA = controlHora.Time.ToString();
            BM_Database_Servicio.BK_CLIENTERUT = textBoxRutID.Text;
            BM_Database_Servicio.BK_CLIENTEDIGVER = textBoxDigitoVerificador.Text;
            BM_Database_Servicio.BK_CLIENTE = textBoxCliente.Text;
            // Desglosar RUT Mensajero
            string xyRutCompleto = textBoxIdMensajero.Text; ;
            string[] CadenaDividida = xyRutCompleto.Split("-", 2, StringSplitOptions.None);
            BM_Database_Servicio.BK_MENSAJERORUT = CadenaDividida[0];
            BM_Database_Servicio.BK_MENSAJERODIGVER = CadenaDividida[1];
            BM_Database_Servicio.BK_MENSAJERO = textBoxNombreMensajero.Text;
            BM_Database_Servicio.BK_RECURSOID = textBoxIdRecurso.Text;
            BM_Database_Servicio.BK_RECURSO = textBoxNombreRecurso.Text;
            BM_Database_Servicio.BK_MENSAJERO = textBoxNombreMensajero.Text;
            BM_Database_Servicio.BK_ODOMICILIO1 = textBoxOrigenDomicilio1.Text;
            // BM_Database_Servicio.BK_ODOMICILIO2 = textBoxOrigenDomicilio2.Text;
            BM_Database_Servicio.BK_ONUMERO = textBoxOrigenNumero.Text;
            BM_Database_Servicio.BK_OPISO = textBoxOrigenPiso.Text;
            BM_Database_Servicio.BK_OOFICINA = textBoxOrigenOficina.Text;
            if (comboBoxOrigenCiudad.Text != "")
            {
                BM_Database_Servicio.BK_OCIUDAD = comboBoxOrigenCiudad.Text;
            }

            if (comboBoxOrigenComuna.Text != "")
            {
                BM_Database_Servicio.BK_OCOMUNA = comboBoxOrigenComuna.Text;
            }

            if (comboBoxOrigenEstado.Text != "")
            {
                BM_Database_Servicio.BK_OESTADO = comboBoxOrigenEstado.Text;
            }

            if (comboBoxOrigenPais.Text != "")
            {
                BM_Database_Servicio.BK_OPAIS = comboBoxOrigenPais.Text;
            }

            BM_Database_Servicio.BK_OCOORDENADAS = "*";
            BM_Database_Servicio.BK_DDOMICILIO1 = textBoxDestinoDomicilio1.Text;
            // BM_Database_Servicio.BK_DDOMICILIO2 = textBoxDestinoDomicilio2.Text;
            BM_Database_Servicio.BK_DNUMERO = textBoxDestinoNumero.Text;
            BM_Database_Servicio.BK_DPISO = textBoxDestinoPiso.Text;
            BM_Database_Servicio.BK_DOFICINA = textBoxDestinoOficina.Text;
            if (comboBoxDestinoCiudad.Text != "")
            {
                BM_Database_Servicio.BK_DCIUDAD = comboBoxDestinoCiudad.Text;
            }

            if (comboBoxDestinoComuna.Text != "")
            {
                BM_Database_Servicio.BK_DCOMUNA = comboBoxDestinoComuna.Text;
            }

            if (comboBoxDestinoEstado.Text != "")
            {
                BM_Database_Servicio.BK_DESTADO = comboBoxDestinoEstado.Text;
            }

            if (comboBoxDestinoPais.Text != "")
            {
                BM_Database_Servicio.BK_DPAIS = comboBoxDestinoPais.Text;
            }

            BM_Database_Servicio.BK_DCOORDENADAS = "*";
            BM_Database_Servicio.BK_DESCRIPCION = textBoxDescripcion.Text;

            BM_Database_Servicio.BK_FACTURAS = short.Parse(textBoxFacturas.Text);
            BM_Database_Servicio.BK_BULTOS = short.Parse(textBoxBultos.Text);
            BM_Database_Servicio.BK_COMPRAS = short.Parse(textBoxCompras.Text);
            BM_Database_Servicio.BK_CHEQUES = short.Parse(textBoxCheques.Text);
            BM_Database_Servicio.BK_SOBRES = short.Parse(textBoxSobres.Text);
            BM_Database_Servicio.BK_OTROS = short.Parse(textBoxOtros.Text);

            BM_Database_Servicio.BK_OBSERVACIONES = textBoxObservaciones.Text;
            BM_Database_Servicio.BK_ENTREGA = textBoxEntrega.Text;
            BM_Database_Servicio.BK_RECEPCION = textBoxRecepcion.Text; ;
            BM_Database_Servicio.BK_TESPERA = textBoxTiempoDeEspera.Text;
            BM_Database_Servicio.BK_FECHAENTREGA = "*";
            BM_Database_Servicio.BK_HORAENTREGA = "*";
            BM_Database_Servicio.BK_DISTANCIA = "*";
            BM_Database_Servicio.BK_PROGRAMADO = "*";
        }

        private async void ErrorDeRecuperacionDialog()
        {
            ContentDialog noErrorRecuperacionDialog = new ContentDialog
            {
                Title = "Acceso a Base de Datos",
                Content = "El registro a recuperar tiene valores nulos.",
                CloseButtonText = "Ok"
            };
            _ = await noErrorRecuperacionDialog.ShowAsync();
        }

        private void BtnSalirServicios(object sender, RoutedEventArgs e)
        {
            LvrTransferVar.TV_Connection.Close();
            Application.Current.Exit();
        }

        private async Task AvisoBorrarServicioDialogAsync()
        {
            ContentDialog AvisoConfirmacionServicioDialog = new ContentDialog
            {
                Title = "Borrar Servicio",
                Content = "Confirme borrado del registro actual!",
                PrimaryButtonText = "Borrar",
                CloseButtonText = "Cancelar"
            };

            ContentDialogResult result = await AvisoConfirmacionServicioDialog.ShowAsync();

            BorrarSiNo = result == ContentDialogResult.Primary;
        }

        private async Task AvisoOperacionServiciosDialogAsync(string xTitulo, string xDescripcion)
        {
            ContentDialog AvisoOperacionServiciosDialog = new ContentDialog
                {
                    Title = xTitulo,
                    Content = xDescripcion,
                    CloseButtonText = "Continuar"
                };
            _ = await AvisoOperacionServiciosDialog.ShowAsync();
        }

        // ******************************************************************
        // Proceso Seleccion Nro de Envio
        // ******************************************************************
        private void BtnEventoListarEnvios(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                LlenarListaEnvios();
                BtnFlyoutNroEnvios.ShowAt(element);
            }
        }

        private void LlenarListaEnvios()
        {
            List<GridEnvioIndividualServicios> GridEnviosLista = new List<GridEnvioIndividualServicios>();

            if (BM_Database_Servicio.Bm_Envios_BuscarGrid())
            {
                while (BM_Database_Servicio.Bm_Envios_BuscarGridProxima())
                {
                    GridEnviosLista.Add(
                        new GridEnvioIndividualServicios
                        {
                            ENVIO = BM_Database_Servicio.BK_GRID_ENVIO_NRO,
                            FECHA = BM_Database_Servicio.BK_GRID_ENVIO_FECHA,
                            CLIENTE = BM_Database_Servicio.BK_GRID_ENVIO_CLIENTE,
                            ENTREGA = BM_Database_Servicio.BK_GRID_ENVIO_ENTREGA
                        });
                }
            }
            dataGridListadoNroEnvios.IsReadOnly = true;
            dataGridListadoNroEnvios.ItemsSource = GridEnviosLista;
        }


        private void DataGridSeleccion_NroDeEnvios(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid CeldaSeleccionada = sender as DataGrid;
                GridEnvioIndividualServicios Envio = (GridEnvioIndividualServicios)CeldaSeleccionada.SelectedItems[0];
                textBoxNroDeEnvio.Text = Envio.ENVIO;
                if (BM_Database_Servicio.Bm_Servicios_Buscar(LvrTransferVar.X_PENTALPHA, Envio.ENVIO))
                {
                    // LimpiarPantalla();
                    LlenarPantallaConDb();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                ; // No hacer nada, es un control vacio de error
            }
        }

        // ******************************************************************
        // Proceso seleccion Cliente
        // ******************************************************************

        private void BtnEventoListarClientes(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                LlenarListaClientes();
                BtnFlyoutIdCliente.ShowAt(element);
            }
        }

        private void LlenarListaClientes()
        {
            List<GridClienteIndividualServicios> GridClientesLista = new List<GridClienteIndividualServicios>();

            if (BM_Database_Servicio.Bm_Clientes_BuscarGrid())
            {
                while (BM_Database_Servicio.Bm_Clientes_BuscarGridProxima())
                {
                    GridClientesLista.Add(
                        new GridClienteIndividualServicios
                        {
                            RUTID = BM_Database_Servicio.BK_GRID_CLIENTE_RUTID,
                            CLIENTE = BM_Database_Servicio.BK_GRID_CLIENTE_NOMBRE
                        });
                }
            }
            dataGridListadoClientes.IsReadOnly = true;
            dataGridListadoClientes.ItemsSource = GridClientesLista;
        }


        private void DataGridSeleccion_IdCliente(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid CeldaSeleccionada = sender as DataGrid;
                GridClienteIndividualServicios Cliente = (GridClienteIndividualServicios)CeldaSeleccionada.SelectedItems[0];
                string[] CadenaDividida = Cliente.RUTID.Split("-", 2, StringSplitOptions.None);
                textBoxRutID.Text = CadenaDividida[0];
                textBoxDigitoVerificador.Text = CadenaDividida[1];
                textBoxCliente.Text = Cliente.CLIENTE;


                //if (BM_Database_Servicio.Bm_Servicio_BuscarEnvio(CadenaDividida[0], CadenaDividida[1]))
                //{
                //    // LimpiarPantalla();
                //    LlenarPantallaConDb();
                //    // LlenarListaPersonal();
                //}
                //else
                //{
                //    // textBoxNombreEmpresa.Text = "Sin Empresa";
                //}
            }
            catch (ArgumentOutOfRangeException)
            {
                ; // No hacer nada, es un control vacio de error
            }
        }

        // ******************************************************************
        // Proceso seleccion Mensajeros
        // ******************************************************************
        private void BtnEventoListarMensajeros(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                LlenarListaMensajeros();
                BtnFlyoutMensajeros.ShowAt(element);
            }
        }

        private void DataGridSeleccion_Mensajeros(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid CeldaSeleccionada = sender as DataGrid;
                GridMensajeroIndividualServicios Mensajero = (GridMensajeroIndividualServicios)CeldaSeleccionada.SelectedItems[0];
                string[] CadenaDividida = Mensajero.RUTID.Split("-", 2, StringSplitOptions.None);
                textBoxIdMensajero.Text = Mensajero.RUTID;
                textBoxNombreMensajero.Text = Mensajero.MENSAJERO;
                //if (BM_Database_Servicio.Bm_Servicio_BuscarEnvio(CadenaDividida[0], CadenaDividida[1]))
                //{
                //    // LimpiarPantalla();
                //    LlenarPantallaConDb();
                //    // LlenarListaPersonal();
                //}
                //else
                //{
                //    // textBoxNombreEmpresa.Text = "Sin Empresa";
                //}
            }
            catch (ArgumentOutOfRangeException)
            {
                ; // No hacer nada, es un control vacio de error
            }
        }

        private void LlenarListaMensajeros()
        {
            List<GridMensajeroIndividualServicios> GridMensajerosLista = new List<GridMensajeroIndividualServicios>();

            if (BM_Database_Servicio.Bm_Mensajeros_BuscarGrid())
            {
                while (BM_Database_Servicio.Bm_Mensajeros_BuscarGridProxima())
                {
                    GridMensajerosLista.Add(
                        new GridMensajeroIndividualServicios
                        {
                            RUTID = BM_Database_Servicio.BK_GRID_MENSAJERO_RUTID,
                            MENSAJERO = BM_Database_Servicio.BK_GRID_MENSAJERO_NOMBRE
                        });
                }
            }
            dataGridListadoMensajeros.IsReadOnly = true;
            dataGridListadoMensajeros.ItemsSource = GridMensajerosLista;
        }

        // ******************************************************************
        // Proceso seleccion Recursos
        // ******************************************************************
        private void BtnEventoListarRecursos(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                LlenarListaRecursos();
                BtnFlyoutMensajeros.ShowAt(element);
            }
        }

        private void DataGridSeleccion_Recursos(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid CeldaSeleccionada = sender as DataGrid;
                GridRecursoIndividualServicios Recurso = (GridRecursoIndividualServicios)CeldaSeleccionada.SelectedItems[0];
                textBoxIdRecurso.Text = Recurso.PATENTE;
                textBoxNombreRecurso.Text = Recurso.TIPO + " - " + Recurso.MARCA + " - " + Recurso.MODELO;

                //if (BM_Database_Servicio.Bm_Servicio_BuscarEnvio(CadenaDividida[0], CadenaDividida[1]))
                //{
                //    LimpiarPantalla();
                //    LlenarPantallaConDb();
                //    LlenarListaPersonal();
                //}
                //else
                //{
                //    // textBoxNombreEmpresa.Text = "Sin Empresa";
                //}
            }
            catch (ArgumentOutOfRangeException)
            {
                ; // No hacer nada, es un control vacio de error
            }
        }

        private void LlenarListaRecursos()
        {
            List<GridRecursoIndividualServicios> GridRecursosLista = new List<GridRecursoIndividualServicios>();

            if (BM_Database_Servicio.Bm_Recursos_BuscarGrid())
            {
                while (BM_Database_Servicio.Bm_Recursos_BuscarGridProxima())
                {
                    GridRecursosLista.Add(
                        new GridRecursoIndividualServicios
                        {
                            PATENTE = BM_Database_Servicio.BK_GRID_RECURSO_PATENTE,
                            TIPO = BM_Database_Servicio.BK_GRID_RECURSO_TIPO,
                            MARCA = BM_Database_Servicio.BK_GRID_RECURSO_MARCA,
                            MODELO = BM_Database_Servicio.BK_GRID_RECURSO_MODELO,
                            PROPIETARIO = BM_Database_Servicio.BK_GRID_RECURSO_PROPIETARIO
                        });
                }
            }
            dataGridListadoRecursos.IsReadOnly = true;
            dataGridListadoRecursos.ItemsSource = GridRecursosLista;
        }

        private async Task AvisoOperacionRecursosDialogAsync(string xTitulo, string xDescripcion)
        {
            ContentDialog AvisoOperacionRecursosDialog = new ContentDialog
            {
                Title = xTitulo,
                Content = xDescripcion,
                CloseButtonText = "Continuar"
            };
            _ = await AvisoOperacionRecursosDialog.ShowAsync();
        }

        //**************************************************
        // Ejecuta operacion de registro de Servicios
        //**************************************************
        private void ProRegistroServicio(string pTipoOperacion)
        {
            string LvrPRecibirServer;
            string LvrPData;
            string LvrStringHttp1 = "https://finanven.ddns.net/Api/BikeMessengerServicio";

            LvrInternet LvrBKInternet = new LvrInternet();
            string LvrParametros;

            List<JsonBikeMessengerServicio> EnviarJsonServicioArray = new List<JsonBikeMessengerServicio>();
            List<JsonBikeMessengerServicio> RecibirJsonServicioArray = new List<JsonBikeMessengerServicio>();

            // Llenar estructura Json
            CopiarMemoriaEnJson(pTipoOperacion);

            // Proceso Serializar

            EnviarJsonServicioArray.Add(EnviarJsonServicio);
            LvrPData = JsonConvert.SerializeObject(EnviarJsonServicioArray);

            // Preparar Parametros
            LvrParametros = LvrPData;

            LvrBKInternet.LvrInetPOST(LvrStringHttp1, LvrParametros);
            LvrPRecibirServer = LvrBKInternet.LvrResultadoWeb;

            if (LvrPRecibirServer != "ERROR" && LvrPRecibirServer != "" && LvrPRecibirServer != null)
            {
                // Procesar primer servidor
                RecibirJsonServicioArray = JsonConvert.DeserializeObject<List<JsonBikeMessengerServicio>>(LvrPRecibirServer); // resp será el string JSON a deserializa
                RecibirJsonServicio = RecibirJsonServicioArray[0];

                if (RecibirJsonServicio.RESOPERACION == "OK")
                {
                    //CopiarJsonEnMemoria(pTipoOperacion);
                    _ = AvisoOperacionServiciosDialogAsync("Estado Envio", RecibirJsonServicio.RESMENSAJE);
                }
                else
                {
                    _ = AvisoOperacionServiciosDialogAsync("Estado Envio", RecibirJsonServicio.RESMENSAJE);
                }

                return;
            }
            _ = AvisoOperacionServiciosDialogAsync("Registro de Servicios", "Problemas durante el registro remoto de Servicio. Debe repetir la operación");
        }

        private void CopiarMemoriaEnJson(string pOPERACION)
        {
            // Limpiar Variables
            EnviarJsonServicio.OPERACION = "";
            EnviarJsonServicio.PENTALPHA = "";
            EnviarJsonServicio.NROENVIO = "";
            EnviarJsonServicio.GUIADESPACHO = "";
            EnviarJsonServicio.FECHA = "";
            EnviarJsonServicio.HORA = "";
            EnviarJsonServicio.CLIENTERUT = "";
            EnviarJsonServicio.CLIENTEDIGVER = "";
            EnviarJsonServicio.CLIENTE = "";
            EnviarJsonServicio.MENSAJERORUT = "";
            EnviarJsonServicio.MENSAJERODIGVER = "";
            EnviarJsonServicio.MENSAJERO = "";
            EnviarJsonServicio.RECURSOID = "";
            EnviarJsonServicio.RECURSO = "";
            EnviarJsonServicio.ODOMICILIO1 = "";
            EnviarJsonServicio.ODOMICILIO2 = "";
            EnviarJsonServicio.ONUMERO = "";
            EnviarJsonServicio.OPISO = "";
            EnviarJsonServicio.OOFICINA = "";
            EnviarJsonServicio.OCIUDAD = "";
            EnviarJsonServicio.OCOMUNA = "";
            EnviarJsonServicio.OESTADO = "";
            EnviarJsonServicio.OPAIS = "";
            EnviarJsonServicio.OCOORDENADAS = "";
            EnviarJsonServicio.DDOMICILIO1 = "";
            EnviarJsonServicio.DDOMICILIO2 = "";
            EnviarJsonServicio.DNUMERO = "";
            EnviarJsonServicio.DPISO = "";
            EnviarJsonServicio.DOFICINA = "";
            EnviarJsonServicio.DCIUDAD = "";
            EnviarJsonServicio.DCOMUNA = "";
            EnviarJsonServicio.DESTADO = "";
            EnviarJsonServicio.DPAIS = "";
            EnviarJsonServicio.DCOORDENADAS = "";
            EnviarJsonServicio.DISTANCIA_KM = "";
            EnviarJsonServicio.DESCRIPCION = "";
            EnviarJsonServicio.FACTURAS = "";
            EnviarJsonServicio.BULTOS = "";
            EnviarJsonServicio.COMPRAS = "";
            EnviarJsonServicio.CHEQUES = "";
            EnviarJsonServicio.SOBRES = "";
            EnviarJsonServicio.OTROS = "";
            EnviarJsonServicio.OBSERVACIONES = "";
            EnviarJsonServicio.ENTREGA = "";
            EnviarJsonServicio.RECEPCION = "";
            EnviarJsonServicio.TESPERA = "";
            EnviarJsonServicio.FECHAENTREGA = "";
            EnviarJsonServicio.HORAENTREGA = "";
            EnviarJsonServicio.DISTANCIA = "";
            EnviarJsonServicio.PROGRAMADO = "";
            EnviarJsonServicio.RESOPERACION = "";
            EnviarJsonServicio.RESMENSAJE = "";

            // Llenar Variables
            EnviarJsonServicio.OPERACION = pOPERACION;
            EnviarJsonServicio.PENTALPHA = BM_Database_Servicio.BK_PENTALPHA;
            EnviarJsonServicio.NROENVIO = BM_Database_Servicio.BK_NROENVIO;
            EnviarJsonServicio.GUIADESPACHO = BM_Database_Servicio.BK_GUIADESPACHO;
            EnviarJsonServicio.FECHA = BM_Database_Servicio.BK_FECHA;
            EnviarJsonServicio.HORA = BM_Database_Servicio.BK_HORA;
            EnviarJsonServicio.CLIENTERUT = BM_Database_Servicio.BK_CLIENTERUT;
            EnviarJsonServicio.CLIENTEDIGVER = BM_Database_Servicio.BK_CLIENTEDIGVER;
            EnviarJsonServicio.CLIENTE = BM_Database_Servicio.BK_CLIENTE;
            EnviarJsonServicio.MENSAJERORUT = BM_Database_Servicio.BK_MENSAJERORUT;
            EnviarJsonServicio.MENSAJERODIGVER = BM_Database_Servicio.BK_MENSAJERODIGVER;
            EnviarJsonServicio.MENSAJERO = BM_Database_Servicio.BK_MENSAJERO;
            EnviarJsonServicio.RECURSOID = BM_Database_Servicio.BK_RECURSOID;
            EnviarJsonServicio.RECURSO = BM_Database_Servicio.BK_RECURSO;
            EnviarJsonServicio.ODOMICILIO1 = BM_Database_Servicio.BK_ODOMICILIO1;
            EnviarJsonServicio.ODOMICILIO2 = BM_Database_Servicio.BK_ODOMICILIO2;
            EnviarJsonServicio.ONUMERO = BM_Database_Servicio.BK_ONUMERO;
            EnviarJsonServicio.OPISO = BM_Database_Servicio.BK_OPISO;
            EnviarJsonServicio.OOFICINA = BM_Database_Servicio.BK_OOFICINA;
            EnviarJsonServicio.OCIUDAD = BM_Database_Servicio.BK_OCIUDAD;
            EnviarJsonServicio.OCOMUNA = BM_Database_Servicio.BK_OCOMUNA;
            EnviarJsonServicio.OESTADO = BM_Database_Servicio.BK_OESTADO;
            EnviarJsonServicio.OPAIS = BM_Database_Servicio.BK_OPAIS;
            EnviarJsonServicio.OCOORDENADAS = BM_Database_Servicio.BK_OCOORDENADAS;
            EnviarJsonServicio.DDOMICILIO1 = BM_Database_Servicio.BK_DDOMICILIO1;
            EnviarJsonServicio.DDOMICILIO2 = BM_Database_Servicio.BK_DDOMICILIO2;
            EnviarJsonServicio.DNUMERO = BM_Database_Servicio.BK_DNUMERO;
            EnviarJsonServicio.DPISO = BM_Database_Servicio.BK_DPISO;
            EnviarJsonServicio.DOFICINA = BM_Database_Servicio.BK_DOFICINA;
            EnviarJsonServicio.DCIUDAD = BM_Database_Servicio.BK_DCIUDAD;
            EnviarJsonServicio.DCOMUNA = BM_Database_Servicio.BK_DCOMUNA;
            EnviarJsonServicio.DESTADO = BM_Database_Servicio.BK_DESTADO;
            EnviarJsonServicio.DPAIS = BM_Database_Servicio.BK_DPAIS;
            EnviarJsonServicio.DCOORDENADAS = BM_Database_Servicio.BK_DCOORDENADAS;
            EnviarJsonServicio.DISTANCIA_KM = BM_Database_Servicio.BK_DISTANCIA_KM.ToString("G", CultureInfo.InvariantCulture);
            EnviarJsonServicio.DESCRIPCION = BM_Database_Servicio.BK_DESCRIPCION;
            EnviarJsonServicio.FACTURAS = BM_Database_Servicio.BK_FACTURAS.ToString();
            EnviarJsonServicio.BULTOS = BM_Database_Servicio.BK_BULTOS.ToString();
            EnviarJsonServicio.COMPRAS = BM_Database_Servicio.BK_COMPRAS.ToString();
            EnviarJsonServicio.CHEQUES = BM_Database_Servicio.BK_CHEQUES.ToString();
            EnviarJsonServicio.SOBRES = BM_Database_Servicio.BK_SOBRES.ToString();
            EnviarJsonServicio.OTROS = BM_Database_Servicio.BK_OTROS.ToString();
            EnviarJsonServicio.OBSERVACIONES = BM_Database_Servicio.BK_OBSERVACIONES;
            EnviarJsonServicio.ENTREGA = BM_Database_Servicio.BK_ENTREGA;
            EnviarJsonServicio.RECEPCION = BM_Database_Servicio.BK_RECEPCION;
            EnviarJsonServicio.TESPERA = BM_Database_Servicio.BK_TESPERA;
            EnviarJsonServicio.FECHAENTREGA = BM_Database_Servicio.BK_FECHAENTREGA;
            EnviarJsonServicio.HORAENTREGA = BM_Database_Servicio.BK_HORAENTREGA;
            EnviarJsonServicio.DISTANCIA = BM_Database_Servicio.BK_DISTANCIA;
            EnviarJsonServicio.PROGRAMADO = BM_Database_Servicio.BK_PROGRAMADO;
            EnviarJsonServicio.RESOPERACION = "";
            EnviarJsonServicio.RESMENSAJE = "";
        }

        private void CopiarJsonEnMemoria(string pOPERACION)
        {
            // Proceso
            // EnviarJsonServicio.OPERACION = pOPERACION;
            BM_Database_Servicio.BK_PENTALPHA = EnviarJsonServicio.PENTALPHA;
            BM_Database_Servicio.BK_NROENVIO = EnviarJsonServicio.NROENVIO;
            BM_Database_Servicio.BK_GUIADESPACHO = EnviarJsonServicio.GUIADESPACHO;
            BM_Database_Servicio.BK_FECHA = EnviarJsonServicio.FECHA;
            BM_Database_Servicio.BK_HORA = EnviarJsonServicio.HORA;
            BM_Database_Servicio.BK_CLIENTERUT = EnviarJsonServicio.CLIENTERUT;
            BM_Database_Servicio.BK_CLIENTEDIGVER = EnviarJsonServicio.CLIENTEDIGVER;
            BM_Database_Servicio.BK_CLIENTE = EnviarJsonServicio.CLIENTE;
            BM_Database_Servicio.BK_MENSAJERORUT = EnviarJsonServicio.MENSAJERORUT;
            BM_Database_Servicio.BK_MENSAJERODIGVER = EnviarJsonServicio.MENSAJERODIGVER;
            BM_Database_Servicio.BK_MENSAJERO = EnviarJsonServicio.MENSAJERO;
            BM_Database_Servicio.BK_RECURSOID = EnviarJsonServicio.RECURSOID;
            BM_Database_Servicio.BK_RECURSO = EnviarJsonServicio.RECURSO;
            BM_Database_Servicio.BK_ODOMICILIO1 = EnviarJsonServicio.ODOMICILIO1;
            BM_Database_Servicio.BK_ODOMICILIO2 = EnviarJsonServicio.ODOMICILIO2;
            BM_Database_Servicio.BK_ONUMERO = EnviarJsonServicio.ONUMERO;
            BM_Database_Servicio.BK_OPISO = EnviarJsonServicio.OPISO;
            BM_Database_Servicio.BK_OOFICINA = EnviarJsonServicio.OOFICINA;
            BM_Database_Servicio.BK_OCIUDAD = EnviarJsonServicio.OCIUDAD;
            BM_Database_Servicio.BK_OCOMUNA = EnviarJsonServicio.OCOMUNA;
            BM_Database_Servicio.BK_OESTADO = EnviarJsonServicio.OESTADO;
            BM_Database_Servicio.BK_OPAIS = EnviarJsonServicio.OPAIS;
            BM_Database_Servicio.BK_OCOORDENADAS = EnviarJsonServicio.OCOORDENADAS;
            BM_Database_Servicio.BK_DDOMICILIO1 = EnviarJsonServicio.DDOMICILIO1;
            BM_Database_Servicio.BK_DDOMICILIO2 = EnviarJsonServicio.DDOMICILIO2;
            BM_Database_Servicio.BK_DNUMERO = EnviarJsonServicio.DNUMERO;
            BM_Database_Servicio.BK_DPISO = EnviarJsonServicio.DPISO;
            BM_Database_Servicio.BK_DOFICINA = EnviarJsonServicio.DOFICINA;
            BM_Database_Servicio.BK_DCIUDAD = EnviarJsonServicio.DCIUDAD;
            BM_Database_Servicio.BK_DCOMUNA = EnviarJsonServicio.DCOMUNA;
            BM_Database_Servicio.BK_DESTADO = EnviarJsonServicio.DESTADO;
            BM_Database_Servicio.BK_DPAIS = EnviarJsonServicio.DPAIS;
            BM_Database_Servicio.BK_DCOORDENADAS = EnviarJsonServicio.DCOORDENADAS;
            BM_Database_Servicio.BK_DISTANCIA_KM = double.Parse(EnviarJsonServicio.DISTANCIA_KM, CultureInfo.InvariantCulture);
            BM_Database_Servicio.BK_DESCRIPCION = EnviarJsonServicio.DESCRIPCION;
            BM_Database_Servicio.BK_FACTURAS = short.Parse(EnviarJsonServicio.FACTURAS);
            BM_Database_Servicio.BK_BULTOS = short.Parse(EnviarJsonServicio.BULTOS);
            BM_Database_Servicio.BK_COMPRAS = short.Parse(EnviarJsonServicio.COMPRAS);
            BM_Database_Servicio.BK_CHEQUES = short.Parse(EnviarJsonServicio.CHEQUES);
            BM_Database_Servicio.BK_SOBRES = short.Parse(EnviarJsonServicio.SOBRES);
            BM_Database_Servicio.BK_OTROS = short.Parse(EnviarJsonServicio.OTROS);
            BM_Database_Servicio.BK_OBSERVACIONES = EnviarJsonServicio.OBSERVACIONES;
            BM_Database_Servicio.BK_ENTREGA = EnviarJsonServicio.ENTREGA;
            BM_Database_Servicio.BK_RECEPCION = EnviarJsonServicio.RECEPCION;
            BM_Database_Servicio.BK_TESPERA = EnviarJsonServicio.TESPERA;
            BM_Database_Servicio.BK_FECHAENTREGA = EnviarJsonServicio.FECHAENTREGA;
            BM_Database_Servicio.BK_HORAENTREGA = EnviarJsonServicio.HORAENTREGA;
            BM_Database_Servicio.BK_DISTANCIA = EnviarJsonServicio.DISTANCIA;
            BM_Database_Servicio.BK_PROGRAMADO = EnviarJsonServicio.PROGRAMADO;
            // EnviarJsonServicio.RESOPERACION = "";
            // EnviarJsonServicio.RESMENSAJE = "";
        }
    }

    internal class GridEnvioIndividualServicios
    {
        public string ENVIO { get; set; }
        public string FECHA { get; set; }
        public string CLIENTE { get; set; }
        public string ENTREGA { get; set; }
    }

    internal class GridClienteIndividualServicios
    {
        public string RUTID { get; set; }
        public string CLIENTE { get; set; }
    }

    internal class GridMensajeroIndividualServicios
    {
        public string RUTID { get; set; }
        public string MENSAJERO { get; set; }
    }

    internal class GridRecursoIndividualServicios
    {
        public string PATENTE { get; set; }
        public string TIPO { get; set; }
        public string MARCA { get; set; }
        public string MODELO { get; set; }
        public string PROPIETARIO { get; set; }
    }
}
