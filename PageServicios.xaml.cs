using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Media.Animation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageServicios : Page
    {
        private List<StructBikeMessengerServicio> ServicioIOArray = new List<StructBikeMessengerServicio>();
        private StructBikeMessengerServicio ServicioIO = new StructBikeMessengerServicio();
        private Bm_Servicio_Database BM_Database_Servicio = new Bm_Servicio_Database();
        private TransferVar LvrTransferVar = new TransferVar();
        private bool BorrarSiNo;
        private double LvrDistanciaRecorrida = 0;

        public PageServicios()
        {
            InitializeComponent();
            mapControlBikeMessenger.MapServiceToken = "kObotinnsvzUnI3k9Smn~hOr - MYZEy_DGqfvj5V0QHQ~AlzXtL5PnD05eblszjnq7bMEEwk4TSFF3szRn_yyu2GaEo9JehDSttrmHwgRFSzi";
            appBarBuscarRuta.IsEnabled = false;
            InicioPantalla();
        }

        void InicioPantalla()
        {
            RellenarCombos();

            if (LvrTransferVar.SER_NROENVIO == "")
            {
                ServicioIOArray = BM_Database_Servicio.BuscarServicio(LvrTransferVar.SER_PENTALPHA);
                if (ServicioIOArray != null && ServicioIOArray.Count > 0)
                {
                    ServicioIO = ServicioIOArray[0];
                    LlenarPantallaConDb();
                }
            }
            else
            {
                ServicioIOArray = BM_Database_Servicio.BuscarServicio(LvrTransferVar.SER_PENTALPHA, LvrTransferVar.SER_NROENVIO);
                if (ServicioIOArray != null && ServicioIOArray.Count > 0)
                {
                    ServicioIO = ServicioIOArray[0];
                    LlenarPantallaConDb();
                }
            }

            LlenarListaEnvios();
            LlenarListaClientes();
            LlenarListaMensajeros();
            LlenarListaRecursos();
        }

        private void RellenarCombos()
        {
            BM_CCRP LocalRellenarCombos = new BM_CCRP();

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
            comboBoxOrigenPais.ItemsSource = LocalRellenarCombos.BuscarPais();
            comboBoxDestinoPais.ItemsSource = comboBoxOrigenPais.ItemsSource;

            // Llenar Combo Region
            comboBoxOrigenEstado.ItemsSource = LocalRellenarCombos.BuscarRegion();
            comboBoxDestinoEstado.ItemsSource = comboBoxOrigenEstado.ItemsSource;

            // Llenar Combo Comuna
            comboBoxOrigenComuna.ItemsSource = LocalRellenarCombos.BuscarComuna();
            comboBoxDestinoComuna.ItemsSource = comboBoxOrigenComuna.ItemsSource;

            // Llenar Combo Ciudad
            comboBoxOrigenCiudad.ItemsSource = LocalRellenarCombos.BuscarCiudad();
            comboBoxDestinoCiudad.ItemsSource = comboBoxOrigenCiudad.ItemsSource;
        }

        private void ActualizarCombos()
        {
            BM_CCRP LocalRellenarCombos = new BM_CCRP();

            // Actualizar Combo Pais
            _ = LocalRellenarCombos.AgregarPais(ServicioIO.OPAIS);
            _ = LocalRellenarCombos.AgregarPais(ServicioIO.DPAIS);

            // Actualizar Combo Region
            _ = LocalRellenarCombos.AgregarRegion(ServicioIO.OESTADO);
            _ = LocalRellenarCombos.AgregarRegion(ServicioIO.DESTADO);

            // Actualizar Combo Comuna
            _ = LocalRellenarCombos.AgregarComuna(ServicioIO.OCOMUNA);
            _ = LocalRellenarCombos.AgregarComuna(ServicioIO.DCOMUNA);

            // Actualizar Combo Ciudad
            _ = LocalRellenarCombos.AgregarCiudad(ServicioIO.OCIUDAD);
            _ = LocalRellenarCombos.AgregarCiudad(ServicioIO.DCIUDAD);
        }

        private async void BtnAgregarServicios(object sender, RoutedEventArgs e)
        {
            LlenarDbConPantalla();

            if (BM_Database_Servicio.AgregarServicio(ServicioIO))
            {
                LlenarListaEnvios();
                LvrTransferVar.SER_PENTALPHA = ServicioIO.PENTALPHA;
                LvrTransferVar.SER_NROENVIO = ServicioIO.NROENVIO;
                LvrTransferVar.EscribirValoresDeAjustes();
                LvrTransferVar.LeerValoresDeAjustes();
                ActualizarCombos();
                ComunicacionXMPP AsignarServicio = new ComunicacionXMPP();
                await AsignarServicio.ProcesoEnvioMensaje();
                await AvisoOperacionServiciosDialogAsync("Agregar Servicio", "Servicio agregado exitosamente.");
            }
            else
            {
                await AvisoOperacionServiciosDialogAsync("Agregar Servicio", "Error en ingreso de servicio. Reintente o escriba a soporte contacto@pentalpha.net");
            }
        }

        private async void BtnModificarServicios(object sender, RoutedEventArgs e)
        {
            LlenarDbConPantalla();

            if (BM_Database_Servicio.ModificarServicio(ServicioIO))
            {
                LlenarListaEnvios();
                LvrTransferVar.SER_PENTALPHA = ServicioIO.PENTALPHA;
                LvrTransferVar.SER_NROENVIO = ServicioIO.NROENVIO;
                LvrTransferVar.EscribirValoresDeAjustes();
                LvrTransferVar.LeerValoresDeAjustes();
                ActualizarCombos();
                ComunicacionXMPP AsignarServicio = new ComunicacionXMPP();
                await AsignarServicio.ProcesoEnvioMensaje();
                await AvisoOperacionServiciosDialogAsync("Modificar Servicio", "Servicio modificado exitosamente.");
            }
            else
            {
                await AvisoOperacionServiciosDialogAsync("Modificar Servicio", "Error en modificación de servicio. Reintente o escriba a soporte contacto@pentalpha.net");
            }
        }

        private async void BtnBorrarServicios(object sender, RoutedEventArgs e)
        {
            BorrarSiNo = false;

            await AvisoBorrarServicioDialogAsync();

            if (!BorrarSiNo)
            {
                return;
            }

            LlenarDbConPantalla();

            if (BM_Database_Servicio.BorrarServicio(LvrTransferVar.SER_PENTALPHA, ServicioIO.NROENVIO))
            {
                await AvisoOperacionServiciosDialogAsync("Borrar Servicio", "Servicio borrado exitosamente.");

                ServicioIOArray = BM_Database_Servicio.BuscarServicio(LvrTransferVar.EMP_PENTALPHA);

                if (ServicioIOArray != null && ServicioIOArray.Count > 0)
                {
                    ServicioIO = ServicioIOArray[0];
                    LvrTransferVar.SER_PENTALPHA = ServicioIO.PENTALPHA;
                    LvrTransferVar.SER_NROENVIO = ServicioIO.NROENVIO;
                }
                else
                {
                    LvrTransferVar.SER_NROENVIO = "";
                }
                textBoxNroDeEnvio.IsReadOnly = false;
                LvrTransferVar.EscribirValoresDeAjustes();
                LvrTransferVar.LeerValoresDeAjustes();

                LlenarListaEnvios();
                LlenarPantallaConDb();
            }
            else
            {
                await AvisoOperacionServiciosDialogAsync("Borrar Servicio", "Error en borrado de servicio. Reintente o escriba a soporte contacto@pentalpha.net");
            }
        }

        private async void CalcularDistancia()
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

            MapRouteDrivingOptions OpcionDeConduccion = new MapRouteDrivingOptions
            {
                MaxAlternateRouteCount = 2,
                RouteOptimization = MapRouteOptimization.TimeWithTraffic,
                RouteRestrictions = MapRouteRestrictions.Highways
            };

            MapRouteFinderResult routeResult =
                await MapRouteFinder.GetDrivingRouteAsync(
                new Geopoint(startLocation),
                new Geopoint(endLocation),
                OpcionDeConduccion);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Use the route to initialize a MapRouteView.
                LvrDistanciaRecorrida = routeResult.Route.LengthInMeters;
            }
            else
            {
                LvrDistanciaRecorrida = 0;
            }
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

            MapRouteDrivingOptions OpcionDeConduccion = new MapRouteDrivingOptions
            {
                MaxAlternateRouteCount = 2,
                RouteOptimization = MapRouteOptimization.TimeWithTraffic,
                RouteRestrictions = MapRouteRestrictions.Highways
            };

            MapRouteFinderResult routeResult =
                await MapRouteFinder.GetDrivingRouteAsync(
                new Geopoint(startLocation),
                new Geopoint(endLocation),
                OpcionDeConduccion);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Use the route to initialize a MapRouteView.
                LvrDistanciaRecorrida = routeResult.Route.LengthInMeters;

                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route)
                {
                    RouteColor = Colors.YellowGreen,
                    OutlineColor = Colors.AliceBlue
                };

                // Preparacion de Inicio y Final

                var InicioDeRecorrido = new MapIcon
                {
                    Location = new Geopoint(startLocation),
                    ZIndex = 0,
                    Title = "Partida"
                };

                var FinDeRecorrido = new MapIcon
                {
                    Location = new Geopoint(endLocation),
                    ZIndex = 0,
                    Title = "Llegada"
                };

                // Add the new MapRouteView to the Routes collection
                // of the MapControl.

                mapControlBikeMessenger.MapElements.Add(InicioDeRecorrido);
                mapControlBikeMessenger.MapElements.Add(FinDeRecorrido);
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
            mapControlBikeMessenger.Style = MapStyle.Terrain;
            mapControlBikeMessenger.BusinessLandmarksVisible = true;
            mapControlBikeMessenger.LandmarksVisible = true;

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
                textBoxNroDeEnvio.Text = ServicioIO.NROENVIO;
                textBoxGuiaDeDespacho.Text = ServicioIO.GUIADESPACHO;
                BtnFechaServicio.Content = ServicioIO.FECHA;
                controlHora.SelectedTime = TimeSpan.Parse(ServicioIO.HORA.ToString());
                textBoxRutID.Text = ServicioIO.CLIENTERUT;
                textBoxDigitoVerificador.Text = ServicioIO.CLIENTEDIGVER;
                textBoxCliente.Text = BM_Database_Servicio.Bm_BuscarNombreCliente(ServicioIO.PENTALPHA, ServicioIO.CLIENTERUT, ServicioIO.CLIENTEDIGVER);
                textBoxIdMensajero.Text = ServicioIO.MENSAJERORUT + "-" + ServicioIO.MENSAJERODIGVER;
                textBoxNombreMensajero.Text = BM_Database_Servicio.Bm_BuscarNombreMensajero(ServicioIO.PENTALPHA, ServicioIO.MENSAJERORUT, ServicioIO.MENSAJERODIGVER);
                textBoxIdRecurso.Text = ServicioIO.RECURSOID;
                textBoxNombreRecurso.Text = BM_Database_Servicio.Bm_BuscarNombreRecurso(ServicioIO.PENTALPHA, ServicioIO.RECURSOID);
                textBoxOrigenDomicilio1.Text = ServicioIO.ODOMICILIO1;
                textBoxOrigenNumero.Text = ServicioIO.ONUMERO;
                textBoxOrigenPiso.Text = ServicioIO.OPISO;
                textBoxOrigenOficina.Text = ServicioIO.OOFICINA;
                textBoxDestinoDomicilio1.Text = ServicioIO.DDOMICILIO1;
                textBoxDestinoNumero.Text = ServicioIO.DNUMERO;
                textBoxDestinoPiso.Text = ServicioIO.DPISO;
                textBoxDestinoOficina.Text = ServicioIO.DOFICINA;
                textBoxDescripcion.Text = ServicioIO.DESCRIPCION;
                textBoxFacturas.Text = ServicioIO.FACTURAS.ToString() == "0" ? "" : ServicioIO.FACTURAS.ToString();
                textBoxBultos.Text = ServicioIO.BULTOS.ToString() == "0" ? "" : ServicioIO.BULTOS.ToString();
                textBoxCompras.Text = ServicioIO.COMPRAS.ToString() == "0" ? "" : ServicioIO.COMPRAS.ToString();
                textBoxCheques.Text = ServicioIO.CHEQUES.ToString() == "0" ? "" : ServicioIO.CHEQUES.ToString();
                textBoxSobres.Text = ServicioIO.SOBRES.ToString() == "0" ? "" : ServicioIO.SOBRES.ToString();
                textBoxOtros.Text = ServicioIO.OTROS.ToString() == "0" ? "" : ServicioIO.OTROS.ToString();
                textBoxObservaciones.Text = ServicioIO.OBSERVACIONES;
                // textBoxEntrega.Text = ServicioIO.ENTREGA;
                textBoxRecepcion.Text = ServicioIO.RECEPCION;
                textBoxTiempoDeEspera.Text = ServicioIO.TESPERA.ToString();

                // Llenado de Pais
                comboBoxOrigenPais.SelectedValue = ServicioIO.OPAIS;

                // Llenado de Estado o Region
                comboBoxOrigenEstado.SelectedValue = ServicioIO.OESTADO;

                // Llenado de Comuna o Municipio
                comboBoxOrigenComuna.SelectedValue = ServicioIO.OCOMUNA;

                // Llenado de Cuidad
                comboBoxOrigenCiudad.SelectedValue = ServicioIO.OCIUDAD;


                // Llenado de Pais
                comboBoxDestinoPais.SelectedValue = ServicioIO.DPAIS;

                // Llenado de Estado o Region
                comboBoxDestinoEstado.SelectedValue = ServicioIO.DESTADO;

                // Llenado de Comuna o Municipio
                comboBoxDestinoComuna.SelectedValue = ServicioIO.DCOMUNA;

                // Llenado de Cuidad
                comboBoxDestinoCiudad.SelectedValue = ServicioIO.DCIUDAD;
            }
            catch (ArgumentNullException)
            {
                ErrorDeRecuperacionDialog();
            }
        }

        private void LlenarDbConPantalla()
        {
            ServicioIO.PENTALPHA = LvrTransferVar.SER_PENTALPHA;
            ServicioIO.NROENVIO = textBoxNroDeEnvio.Text;
            ServicioIO.PKSERVICIO = ServicioIO.PENTALPHA + ServicioIO.NROENVIO;
            ServicioIO.NROENVIO = textBoxNroDeEnvio.Text;
            ServicioIO.GUIADESPACHO = textBoxGuiaDeDespacho.Text;
            ServicioIO.FECHA = (string)BtnFechaServicio.Content;
            ServicioIO.HORA = controlHora.Time.ToString();
            ServicioIO.CLIENTERUT = textBoxRutID.Text;
            ServicioIO.CLIENTEDIGVER = textBoxDigitoVerificador.Text;
            string xyRutCompleto = textBoxIdMensajero.Text; ;
            string[] CadenaDividida = xyRutCompleto.Split("-", 2, StringSplitOptions.None);
            try
            {
                ServicioIO.MENSAJERORUT = CadenaDividida[0];
                ServicioIO.MENSAJERODIGVER = CadenaDividida[1];
            }
            catch(IndexOutOfRangeException)
            {
                ServicioIO.MENSAJERORUT = "";
                ServicioIO.MENSAJERODIGVER = "";
            }
            ServicioIO.RECURSOID = textBoxIdRecurso.Text;
            ServicioIO.ODOMICILIO1 = textBoxOrigenDomicilio1.Text;
            ServicioIO.ONUMERO = textBoxOrigenNumero.Text;
            ServicioIO.OPISO = textBoxOrigenPiso.Text;
            ServicioIO.OOFICINA = textBoxOrigenOficina.Text;
            if (comboBoxOrigenCiudad.Text != "")
            {
                ServicioIO.OCIUDAD = comboBoxOrigenCiudad.Text;
            }

            if (comboBoxOrigenComuna.Text != "")
            {
                ServicioIO.OCOMUNA = comboBoxOrigenComuna.Text;
            }

            if (comboBoxOrigenEstado.Text != "")
            {
                ServicioIO.OESTADO = comboBoxOrigenEstado.Text;
            }

            if (comboBoxOrigenPais.Text != "")
            {
                ServicioIO.OPAIS = comboBoxOrigenPais.Text;
            }

            ServicioIO.OLATITUD = 0;
            ServicioIO.OLONGITUD = 0;

            ServicioIO.DDOMICILIO1 = textBoxDestinoDomicilio1.Text;
            ServicioIO.DNUMERO = textBoxDestinoNumero.Text;
            ServicioIO.DPISO = textBoxDestinoPiso.Text;
            ServicioIO.DOFICINA = textBoxDestinoOficina.Text;

            if (comboBoxDestinoCiudad.Text != "")
            {
                ServicioIO.DCIUDAD = comboBoxDestinoCiudad.Text;
            }

            if (comboBoxDestinoComuna.Text != "")
            {
                ServicioIO.DCOMUNA = comboBoxDestinoComuna.Text;
            }

            if (comboBoxDestinoEstado.Text != "")
            {
                ServicioIO.DESTADO = comboBoxDestinoEstado.Text;
            }

            if (comboBoxDestinoPais.Text != "")
            {
                ServicioIO.DPAIS = comboBoxDestinoPais.Text;
            }

            ServicioIO.DLATITUD = 0;
            ServicioIO.DLONGITUD = 0;

            ServicioIO.DESCRIPCION = textBoxDescripcion.Text;
            ServicioIO.FACTURAS = int.Parse(textBoxFacturas.Text != "" ? textBoxFacturas.Text : "0");
            ServicioIO.BULTOS = int.Parse(textBoxBultos.Text != "" ? textBoxBultos.Text : "0");
            ServicioIO.COMPRAS = int.Parse(textBoxCompras.Text != "" ? textBoxCompras.Text : "0");
            ServicioIO.CHEQUES = int.Parse(textBoxCheques.Text != "" ? textBoxCheques.Text : "0");
            ServicioIO.SOBRES = int.Parse(textBoxSobres.Text != "" ? textBoxSobres.Text : "0");
            ServicioIO.OTROS = int.Parse(textBoxOtros.Text != "" ? textBoxOtros.Text : "0");

            ServicioIO.OBSERVACIONES = textBoxObservaciones.Text;
            // ServicioIO.ENTREGA = textBoxEntrega.Text;
            ServicioIO.RECEPCION = textBoxRecepcion.Text; ;
            ServicioIO.TESPERA = controlHora.Time.ToString();
            ServicioIO.FECHAENTREGA = (string)BtnFechaServicio.Content;
            ServicioIO.HORAENTREGA = controlHora.Time.ToString();
            ServicioIO.DISTANCIA = LvrDistanciaRecorrida;
            ServicioIO.PROGRAMADO = "*";
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
            List<ClaseServicioGrid> GridServicioDbArray = new List<ClaseServicioGrid>();
            List<GridEnvioIndividualServicios> GridEnviosLista = new List<GridEnvioIndividualServicios>();

            GridServicioDbArray = BM_Database_Servicio.BuscarGridServicios(LvrTransferVar.EMP_PENTALPHA);
            if (GridServicioDbArray != null && GridServicioDbArray.Count > 0)
            {

                for (int i = 0; i < GridServicioDbArray.Count; i++)
                {
                    GridEnviosLista.Add(
                        new GridEnvioIndividualServicios
                        {
                            ENVIO = GridServicioDbArray[i].ENVIO,
                            FECHA = GridServicioDbArray[i].FECHA,
                            CLIENTE = BM_Database_Servicio.Bm_BuscarNombreCliente(LvrTransferVar.SER_PENTALPHA, GridServicioDbArray[i].CLIENTERUT, GridServicioDbArray[i].CLIENTEDIGVER),
                            ENTREGA = GridServicioDbArray[i].ENTREGA
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
                ServicioIOArray = BM_Database_Servicio.BuscarServicio(LvrTransferVar.SER_PENTALPHA, Envio.ENVIO);
                if (ServicioIOArray != null && ServicioIOArray.Count > 0)
                {
                    ServicioIO = ServicioIOArray[0];
                    LlenarPantallaConDb();
                    LvrTransferVar.SER_PENTALPHA = ServicioIO.PENTALPHA;
                    LvrTransferVar.SER_NROENVIO = ServicioIO.NROENVIO;
                    LvrTransferVar.EscribirValoresDeAjustes();
                    LvrTransferVar.LeerValoresDeAjustes();
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
            List<ClaseClientesGrid> GridClienteDbArray = new List<ClaseClientesGrid>();
            List<GridClienteIndividualServicios> GridClientesLista = new List<GridClienteIndividualServicios>();

            GridClienteDbArray = BM_Database_Servicio.BuscarGridClientes(LvrTransferVar.EMP_PENTALPHA);
            if (GridClienteDbArray != null && GridClienteDbArray.Count > 0)
            {
                for (int i = 0; i < GridClienteDbArray.Count; i++)
                {
                    GridClientesLista.Add(
                        new GridClienteIndividualServicios
                        {
                            RUTID = GridClienteDbArray[i].RUTID + "-" + GridClienteDbArray[i].DIGVER,
                            CLIENTE = BM_Database_Servicio.Bm_BuscarNombreCliente(LvrTransferVar.CLI_PENTALPHA, GridClienteDbArray[i].RUTID, GridClienteDbArray[i].DIGVER)
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
            List<ClasePersonalGrid> GridMensajerosDbArray = new List<ClasePersonalGrid>();
            List<GridMensajeroIndividualServicios> GridMensajerosLista = new List<GridMensajeroIndividualServicios>();

            GridMensajerosDbArray = BM_Database_Servicio.BuscarGridPersonal(LvrTransferVar.EMP_PENTALPHA);
            if (GridMensajerosDbArray != null && GridMensajerosDbArray.Count > 0)
            {
                for (int i = 0; i < GridMensajerosDbArray.Count; i++)
                {
                    GridMensajerosLista.Add(
                        new GridMensajeroIndividualServicios
                        {
                            RUTID = GridMensajerosDbArray[i].RUTID + "-" + GridMensajerosDbArray[i].DIGVER,
                            MENSAJERO = BM_Database_Servicio.Bm_BuscarNombreMensajero(LvrTransferVar.PER_PENTALPHA, GridMensajerosDbArray[i].RUTID, GridMensajerosDbArray[i].DIGVER)
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
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
        }

        private void LlenarListaRecursos()
        {
            List<ClaseRecursoGrid> GridRecursoDbArray = new List<ClaseRecursoGrid>();
            List<GridRecursoIndividualServicios> GridRecursosLista = new List<GridRecursoIndividualServicios>();


            GridRecursoDbArray = BM_Database_Servicio.BuscarGridRecurso(LvrTransferVar.EMP_PENTALPHA);
            if (GridRecursoDbArray != null && GridRecursoDbArray.Count > 0)
            {
                for (int i = 0; i < GridRecursoDbArray.Count; i++)
                {
                    GridRecursosLista.Add(
                        new GridRecursoIndividualServicios
                        {
                            PATENTE = GridRecursoDbArray[i].PATENTE,
                            TIPO = GridRecursoDbArray[i].TIPO,
                            MARCA = GridRecursoDbArray[i].MARCA,
                            MODELO = GridRecursoDbArray[i].MODELO,
                            PROPIETARIO = BM_Database_Servicio.Bm_BuscarNombreMensajero(LvrTransferVar.REC_PENTALPHA, GridRecursoDbArray[i].RUTID, GridRecursoDbArray[i].DIGVER)
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

        private void BtnListarServicio(object sender, RoutedEventArgs e)
        {
            {
                LvrTransferVar.PantallaAnterior = "SERVICIO";
                _ = Frame.Navigate(typeof(ListadosGenerales), LvrTransferVar, new SuppressNavigationTransitionInfo());
            }
        }

        private void ValidarValorNumerico(UIElement sender, Windows.UI.Xaml.Input.CharacterReceivedRoutedEventArgs args)
        {
            TextBox Validador = (TextBox)sender;
            if (!(args.Character >= '0' && args.Character <= '9'))
            {
                Validador.Text = "";
            }
        }

        private void CambioDeFechaServicio(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            try
            {
                DateTimeOffset FechaInicial = args.AddedDates.First();
                BtnFechaServicio.Content = FechaInicial.DateTime.ToString("d", new CultureInfo("es-ES"));
                PopUpFechaInicial.IsOpen = false;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.InnerException);
            }
        }

        private void BtnCambioFechaInicial(object sender, RoutedEventArgs e)
        {
            PopUpFechaInicial.IsOpen = true;
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
