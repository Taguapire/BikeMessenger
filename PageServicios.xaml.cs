using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageServicios : Page
    {
        TransferVar LvrTransferVar;
        readonly Bm_Servicios_Database BM_Database_Servicios = new Bm_Servicios_Database();
        bool BorrarSiNo;
        public PageServicios()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            mapControlBikeMessenger.MapServiceToken = "kObotinnsvzUnI3k9Smn~hOr - MYZEy_DGqfvj5V0QHQ~AlzXtL5PnD05eblszjnq7bMEEwk4TSFF3szRn_yyu2GaEo9JehDSttrmHwgRFSzi";
            appBarBuscarRuta.IsEnabled = false;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationCacheMode = NavigationCacheMode.Disabled;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            {
                //greeting.Text = $"Hi, {e.Parameter.ToString()}";
            }
            else
            {
                LvrTransferVar = (TransferVar)e.Parameter;
                if (BM_Database_Servicios.BM_CreateDatabase(LvrTransferVar.TV_Connection))
                {
                    if (BM_Database_Servicios.Bm_Servicios_Buscar())
                    {
                        LlenarPantallaConDb();
                        // BM_Existe_Empresa = true;
                    }
                    else
                    {
                        // textBoxNombreEmpresa.Text = "Sin Empresa";
                    }
                }
            }
            base.OnNavigatedTo(e);
        }

        private void BtnSeleccionarAjustes(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageAjustes), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarServicios(object sender, RoutedEventArgs e)
        {
            // this.Frame.Navigate(typeof(PageServicios), LvrTransferVar, new SuppressNavigationTransitionInfo());
        }

        private void BtnSeleccionarClientes(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageClientes), LvrTransferVar, new SuppressNavigationTransitionInfo());
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

        private async void BtnAgregarServicios(object sender, RoutedEventArgs e)
        {
            try
            {
                LlenarDbConPantalla();

                if (BM_Database_Servicios.Bm_Servicios_Agregar())
                {
                    LvrTransferVar.S_NROENVIO = BM_Database_Servicios.BK_NROENVIO;
                    await AvisoOperacionServiciosDialogAsync("Agregar Servicios", "Operación completada con exito.");
                }
                else
                {
                    await AvisoOperacionServiciosDialogAsync("Agregando Servicios", "Se a producido un error al intentar agregar recurso.");
                }
            }
            catch (System.ArgumentException)
            {
                await AvisoOperacionServiciosDialogAsync("Agregando Servicios", "Aun faltan datos por completar.");
            }
            catch (System.IndexOutOfRangeException)
            {
                await AvisoOperacionServiciosDialogAsync("Agregando Servicios", "Datos de ingresos incorrectos o no estan completos.");
            }
        }

        private async void BtnModificarServicios(object sender, RoutedEventArgs e)
        {
            try
            {
                LlenarDbConPantalla();
                if (BM_Database_Servicios.Bm_Recursos_Modificar(BM_Database_Servicios.BK_NROENVIO))
                {
                    LvrTransferVar.S_NROENVIO = BM_Database_Servicios.BK_NROENVIO;
                    await AvisoOperacionServiciosDialogAsync("Modificar Servicios", "Operación completada con exito.");
                }
                else
                {
                    await AvisoOperacionServiciosDialogAsync("Modificando Servicios", "Se a producido un error al intentar modificar recurso.");
                }
            }
            catch (System.ArgumentException)
            {
                await AvisoOperacionServiciosDialogAsync("Modificando Servicios", "Aun faltan datos por completar.");
            }
            catch (System.IndexOutOfRangeException)
            {
                await AvisoOperacionServiciosDialogAsync("Modificando Servicios", "Datos de ingresos incorrectos o no estan completos.");
            }
        }

        private async void BtnBorrarServicios(object sender, RoutedEventArgs e)
        {
            BorrarSiNo = false;

            await AvisoBorrarServicioDialogAsync();

            if (!BorrarSiNo)
                return;

            try
            {
                if (BM_Database_Servicios.Bm_Servicios_Borrar(BM_Database_Servicios.BK_NROENVIO))
                {
                    await AvisoOperacionServiciosDialogAsync("Borrando Servicios", "Operación completada con exito.");

                    textBoxNroDeEnvio.IsReadOnly = false;

                    if (BM_Database_Servicios.Bm_Servicios_Buscar())
                    {
                        LvrTransferVar.S_NROENVIO = BM_Database_Servicios.BK_NROENVIO;
                    }
                    else
                    {
                        LvrTransferVar.S_NROENVIO = "";
                    }
                    LlenarPantallaConDb();
                }
                else
                {
                    await AvisoOperacionServiciosDialogAsync("Borrando Servicios", "Se a producido un error al intentar borrar personal.");
                }
            }
            catch (System.ArgumentException)
            {
                await AvisoOperacionServiciosDialogAsync("Acceso a Base de Datos", "Debe llenar los datos del personal.");
            }
            BorrarSiNo = false;
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

            var resultsOrigen = await MapLocationFinder.FindLocationsAsync(addressInicio, null);

            if (resultsOrigen.Status == MapLocationFinderStatus.Success)
            {
                lvrlatOrigen = resultsOrigen.Locations[0].Point.Position.Latitude;
                lvrlonOrigen = resultsOrigen.Locations[0].Point.Position.Longitude;
            }

            var resultsDestino = await MapLocationFinder.FindLocationsAsync(addressFinal, null);

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
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Yellow;
                viewOfRoute.OutlineColor = Colors.Black;

                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                mapControlBikeMessenger.Routes.Add(viewOfRoute);

                // Fit the MapControl to the route.
                await mapControlBikeMessenger.TrySetViewBoundsAsync(
                      routeResult.Route.BoundingBox,
                      null,
                      Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
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
                textBoxNroDeEnvio.Text = BM_Database_Servicios.BK_NROENVIO;
                textBoxGuiaDeDespacho.Text = BM_Database_Servicios.BK_GUIADESPACHO;
                controlFecha.SelectedDate = DateTimeOffset.Parse(BM_Database_Servicios.BK_FECHA);
                controlHora.SelectedTime = TimeSpan.Parse(BM_Database_Servicios.BK_HORA);
                textBoxRutID.Text = BM_Database_Servicios.BK_CLIENTERUT;
                textBoxDigitoVerificador.Text = BM_Database_Servicios.BK_CLIENTEDIGVER;
                textBoxCliente.Text = BM_Database_Servicios.BK_CLIENTE;
                textBoxIdMensajero.Text = BM_Database_Servicios.BK_MENSAJERORUT + "-" + BM_Database_Servicios.BK_MENSAJERODIGVER;
                textBoxNombreMensajero.Text = BM_Database_Servicios.BK_MENSAJERO;
                textBoxIdRecurso.Text = BM_Database_Servicios.BK_RECURSOID;
                textBoxNombreRecurso.Text = BM_Database_Servicios.BK_RECURSO;
                textBoxOrigenDomicilio1.Text = BM_Database_Servicios.BK_ODOMICILIO1;
                //textBoxOrigenDomicilio2.Text = BM_Database_Servicios.BK_ODOMICILIO2;
                textBoxOrigenNumero.Text = BM_Database_Servicios.BK_ONUMERO;
                textBoxOrigenPiso.Text = BM_Database_Servicios.BK_OPISO;
                textBoxOrigenOficina.Text = BM_Database_Servicios.BK_OOFICINA;
                // BM_Database_Servicios.BK_OCOORDENADAS = "*";
                textBoxDestinoDomicilio1.Text = BM_Database_Servicios.BK_DDOMICILIO1;
                // textBoxDestinoDomicilio2.Text = BM_Database_Servicios.BK_DDOMICILIO2;
                textBoxDestinoNumero.Text = BM_Database_Servicios.BK_DNUMERO;
                textBoxDestinoPiso.Text = BM_Database_Servicios.BK_DPISO;
                textBoxDestinoOficina.Text = BM_Database_Servicios.BK_DOFICINA;
                // BM_Database_Servicios.BK_DCOORDENADAS = "*";
                textBoxDescripcion.Text = BM_Database_Servicios.BK_DESCRIPCION;

                textBoxFacturas.Text = BM_Database_Servicios.BK_FACTURAS.ToString();
                textBoxBultos.Text = BM_Database_Servicios.BK_BULTOS.ToString();
                textBoxCompras.Text = BM_Database_Servicios.BK_COMPRAS.ToString();
                textBoxCheques.Text = BM_Database_Servicios.BK_CHEQUES.ToString();
                textBoxSobres.Text = BM_Database_Servicios.BK_SOBRES.ToString();
                textBoxOtros.Text = BM_Database_Servicios.BK_OTROS.ToString();

                textBoxObservaciones.Text = BM_Database_Servicios.BK_OBSERVACIONES;
                textBoxEntrega.Text = BM_Database_Servicios.BK_ENTREGA;
                textBoxRecepcion.Text = BM_Database_Servicios.BK_RECEPCION;
                textBoxTiempoDeEspera.Text = BM_Database_Servicios.BK_TESPERA;
                // BM_Database_Servicios.BK_FECHAENTREGA = "*";
                // BM_Database_Servicios.BK_HORAENTREGA = "*";
                // BM_Database_Servicios.BK_DISTANCIA = "*";
                // BM_Database_Servicios.BK_PROGRAMADO = "*";

                // Asignaciones que deben revisarse
                // BM_Database_Servicios.BK_FECHA = controlFecha.Date.ToString();
                // BM_Database_Servicios.BK_HORA = controlHora.Time.ToString();

                comboBoxOrigenPais.SelectedValue = BM_Database_Servicios.BK_OPAIS;
                comboBoxOrigenEstado.SelectedValue = BM_Database_Servicios.BK_OESTADO;
                comboBoxOrigenComuna.SelectedValue = BM_Database_Servicios.BK_OCOMUNA;
                comboBoxOrigenCiudad.SelectedValue = BM_Database_Servicios.BK_OCIUDAD;

                comboBoxDestinoPais.SelectedValue = BM_Database_Servicios.BK_DPAIS;
                comboBoxDestinoEstado.SelectedValue = BM_Database_Servicios.BK_DESTADO;
                comboBoxDestinoComuna.SelectedValue = BM_Database_Servicios.BK_DCOMUNA;
                comboBoxDestinoCiudad.SelectedValue = BM_Database_Servicios.BK_DCIUDAD;
            }
            catch (System.ArgumentNullException)
            {
                ErrorDeRecuperacionDialog();
            }
        }

        private void LlenarDbConPantalla()
        {
            BM_Database_Servicios.BK_NROENVIO = textBoxNroDeEnvio.Text;
            BM_Database_Servicios.BK_GUIADESPACHO = textBoxGuiaDeDespacho.Text;
            BM_Database_Servicios.BK_FECHA = controlFecha.Date.Date.ToShortDateString().ToString();
            BM_Database_Servicios.BK_HORA = controlHora.Time.ToString();
            BM_Database_Servicios.BK_CLIENTERUT = textBoxRutID.Text;
            BM_Database_Servicios.BK_CLIENTEDIGVER = textBoxDigitoVerificador.Text;
            BM_Database_Servicios.BK_CLIENTE = textBoxCliente.Text;
            // Desglosar RUT Mensajero
            string xyRutCompleto = textBoxIdMensajero.Text; ;
            string[] CadenaDividida = xyRutCompleto.Split("-", 2, StringSplitOptions.None);
            BM_Database_Servicios.BK_MENSAJERORUT = CadenaDividida[0];
            BM_Database_Servicios.BK_MENSAJERODIGVER = CadenaDividida[1];
            BM_Database_Servicios.BK_MENSAJERO = textBoxNombreMensajero.Text;
            BM_Database_Servicios.BK_RECURSOID = textBoxIdRecurso.Text;
            BM_Database_Servicios.BK_RECURSO = textBoxNombreRecurso.Text;
            BM_Database_Servicios.BK_MENSAJERO = textBoxNombreMensajero.Text;
            BM_Database_Servicios.BK_ODOMICILIO1 = textBoxOrigenDomicilio1.Text;
            // BM_Database_Servicios.BK_ODOMICILIO2 = textBoxOrigenDomicilio2.Text;
            BM_Database_Servicios.BK_ONUMERO = textBoxOrigenNumero.Text;
            BM_Database_Servicios.BK_OPISO = textBoxOrigenPiso.Text;
            BM_Database_Servicios.BK_OOFICINA = textBoxOrigenOficina.Text;
            if (comboBoxOrigenCiudad.Text != "")
                BM_Database_Servicios.BK_OCIUDAD = comboBoxOrigenCiudad.Text;
            if (comboBoxOrigenComuna.Text != "")
                BM_Database_Servicios.BK_OCOMUNA = comboBoxOrigenComuna.Text;
            if (comboBoxOrigenEstado.Text != "")
                BM_Database_Servicios.BK_OESTADO = comboBoxOrigenEstado.Text;
            if (comboBoxOrigenPais.Text != "")
                BM_Database_Servicios.BK_OPAIS = comboBoxOrigenPais.Text;
            BM_Database_Servicios.BK_OCOORDENADAS = "*";
            BM_Database_Servicios.BK_DDOMICILIO1 = textBoxDestinoDomicilio1.Text;
            // BM_Database_Servicios.BK_DDOMICILIO2 = textBoxDestinoDomicilio2.Text;
            BM_Database_Servicios.BK_DNUMERO = textBoxDestinoNumero.Text;
            BM_Database_Servicios.BK_DPISO = textBoxDestinoPiso.Text;
            BM_Database_Servicios.BK_DOFICINA = textBoxDestinoOficina.Text;
            if (comboBoxDestinoCiudad.Text != "")
                BM_Database_Servicios.BK_DCIUDAD = comboBoxDestinoCiudad.Text;
            if (comboBoxDestinoComuna.Text != "")
                BM_Database_Servicios.BK_DCOMUNA = comboBoxDestinoComuna.Text;
            if (comboBoxDestinoEstado.Text != "")
                BM_Database_Servicios.BK_DESTADO = comboBoxDestinoEstado.Text;
            if (comboBoxDestinoPais.Text != "")
                BM_Database_Servicios.BK_DPAIS = comboBoxDestinoPais.Text;
            BM_Database_Servicios.BK_DCOORDENADAS = "*";
            BM_Database_Servicios.BK_DESCRIPCION = textBoxDescripcion.Text;

            BM_Database_Servicios.BK_FACTURAS = Convert.ToInt32(textBoxFacturas.Text);
            BM_Database_Servicios.BK_BULTOS = Convert.ToInt32(textBoxBultos.Text);
            BM_Database_Servicios.BK_COMPRAS = Convert.ToInt32(textBoxCompras.Text);
            BM_Database_Servicios.BK_CHEQUES = Convert.ToInt32(textBoxCheques.Text);
            BM_Database_Servicios.BK_SOBRES = Convert.ToInt32(textBoxSobres.Text);
            BM_Database_Servicios.BK_OTROS = Convert.ToInt32(textBoxOtros.Text);

            BM_Database_Servicios.BK_OBSERVACIONES = textBoxObservaciones.Text;
            BM_Database_Servicios.BK_ENTREGA = textBoxEntrega.Text;
            BM_Database_Servicios.BK_RECEPCION = textBoxRecepcion.Text; ;
            BM_Database_Servicios.BK_TESPERA = textBoxTiempoDeEspera.Text;
            BM_Database_Servicios.BK_FECHAENTREGA = "*";
            BM_Database_Servicios.BK_HORAENTREGA = "*";
            BM_Database_Servicios.BK_DISTANCIA = "*";
            BM_Database_Servicios.BK_PROGRAMADO = "*";
        }

        private async void ErrorDeRecuperacionDialog()
        {
            ContentDialog noErrorRecuperacionDialog = new ContentDialog
            {
                Title = "Acceso a Base de Datos",
                Content = "El registro a recuperar tiene valores nulos.",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noErrorRecuperacionDialog.ShowAsync();
        }

        private void BtnSalirServicios(object sender, RoutedEventArgs e)
        {
            LvrTransferVar.TV_Connection.Close();
            Application.Current.Exit();
        }

        private async System.Threading.Tasks.Task AvisoBorrarServicioDialogAsync()
        {
            ContentDialog AvisoConfirmacionServicioDialog = new ContentDialog
            {
                Title = "Borrar Servicio",
                Content = "Confirme borrado del registro actual!",
                PrimaryButtonText = "Borrar",
                CloseButtonText = "Cancelar"
            };

            ContentDialogResult result = await AvisoConfirmacionServicioDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
                BorrarSiNo = true;
            else
                BorrarSiNo = false;
        }

        private async System.Threading.Tasks.Task AvisoOperacionServiciosDialogAsync(string xTitulo, string xDescripcion)
        {
            try
            {
                ContentDialog AvisoOperacionServiciosDialog = new ContentDialog
                {
                    Title = xTitulo,
                    Content = xDescripcion,
                    CloseButtonText = "Continuar"
                };
                ContentDialogResult result = await AvisoOperacionServiciosDialog.ShowAsync();
            }
            catch (System.Exception)
            {
                ;
            }
        }

        // ******************************************************************
        // Proceso Seleccion Nro de Envio
        // ******************************************************************

        private void BtnEventoListarEnvios(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                LlenarListaEnvios();
                BtnFlyoutNroEnvios.ShowAt(element);
            }
        }

        private void LlenarListaEnvios()
        {
            List<GridEnvioIndividualServicios> GridEnviosLista = new List<GridEnvioIndividualServicios>();

            if (BM_Database_Servicios.Bm_Envios_BuscarGrid())
            {
                while (BM_Database_Servicios.Bm_Envios_BuscarGridProxima())
                {
                    GridEnviosLista.Add(
                        new GridEnvioIndividualServicios
                        {
                            ENVIO = BM_Database_Servicios.BK_GRID_ENVIO_NRO,
                            FECHA = BM_Database_Servicios.BK_GRID_ENVIO_FECHA,
                            CLIENTE = BM_Database_Servicios.BK_GRID_ENVIO_CLIENTE,
                            ENTREGA = BM_Database_Servicios.BK_GRID_ENVIO_ENTREGA

                        });
                }
            }
            dataGridListadoNroEnvios.IsReadOnly = true;
            dataGridListadoNroEnvios.ItemsSource = GridEnviosLista;
        }


        private void dataGridSeleccion_NroDeEnvios(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid CeldaSeleccionada = sender as DataGrid;
                GridEnvioIndividualServicios Envio = (GridEnvioIndividualServicios)CeldaSeleccionada.SelectedItems[0];

                //if (BM_Database_Servicios.Bm_Servicio_BuscarEnvio(CadenaDividida[0], CadenaDividida[1]))
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
            catch (System.ArgumentOutOfRangeException)
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

            if (BM_Database_Servicios.Bm_Clientes_BuscarGrid())
            {
                while (BM_Database_Servicios.Bm_Clientes_BuscarGridProxima())
                {
                    GridClientesLista.Add(
                        new GridClienteIndividualServicios
                        {
                            RUTID = BM_Database_Servicios.BK_GRID_CLIENTE_RUTID,
                            CLIENTE = BM_Database_Servicios.BK_GRID_CLIENTE_NOMBRE
                        });
                }
            }
            dataGridListadoClientes.IsReadOnly = true;
            dataGridListadoClientes.ItemsSource = GridClientesLista;
        }


        private void dataGridSeleccion_IdCliente(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid CeldaSeleccionada = sender as DataGrid;
                GridClienteIndividualServicios Cliente = (GridClienteIndividualServicios)CeldaSeleccionada.SelectedItems[0];

                //if (BM_Database_Servicios.Bm_Servicio_BuscarEnvio(CadenaDividida[0], CadenaDividida[1]))
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
            catch (System.ArgumentOutOfRangeException)
            {
                ; // No hacer nada, es un control vacio de error
            }
        }

        // ******************************************************************
        // Proceso seleccion Mensajeros
        // ******************************************************************
        private void BtnEventoListarMensajeros(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                LlenarListaMensajeros();
                BtnFlyoutMensajeros.ShowAt(element);
            }
        }

        private void dataGridSeleccion_Mensajeros(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid CeldaSeleccionada = sender as DataGrid;
                GridMensajeroIndividualServicios Mensajero = (GridMensajeroIndividualServicios)CeldaSeleccionada.SelectedItems[0];

                //if (BM_Database_Servicios.Bm_Servicio_BuscarEnvio(CadenaDividida[0], CadenaDividida[1]))
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
            catch (System.ArgumentOutOfRangeException)
            {
                ; // No hacer nada, es un control vacio de error
            }
        }

        private void LlenarListaMensajeros()
        {
            List<GridMensajeroIndividualServicios> GridMensajerosLista = new List<GridMensajeroIndividualServicios>();

            if (BM_Database_Servicios.Bm_Mensajeros_BuscarGrid())
            {
                while (BM_Database_Servicios.Bm_Mensajeros_BuscarGridProxima())
                {
                    GridMensajerosLista.Add(
                        new GridMensajeroIndividualServicios
                        {
                            RUTID = BM_Database_Servicios.BK_GRID_MENSAJERO_RUTID,
                            MENSAJERO = BM_Database_Servicios.BK_GRID_MENSAJERO_NOMBRE
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
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                LlenarListaRecursos();
                BtnFlyoutMensajeros.ShowAt(element);
            }
        }

        private void dataGridSeleccion_Recursos(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid CeldaSeleccionada = sender as DataGrid;
                GridRecursoIndividualServicios Recurso = (GridRecursoIndividualServicios)CeldaSeleccionada.SelectedItems[0];

                //if (BM_Database_Servicios.Bm_Servicio_BuscarEnvio(CadenaDividida[0], CadenaDividida[1]))
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
            catch (System.ArgumentOutOfRangeException)
            {
                ; // No hacer nada, es un control vacio de error
            }
        }

        private void LlenarListaRecursos()
        {
            List<GridRecursoIndividualServicios> GridRecursosLista = new List<GridRecursoIndividualServicios>();

            if (BM_Database_Servicios.Bm_Recursos_BuscarGrid())
            {
                while (BM_Database_Servicios.Bm_Recursos_BuscarGridProxima())
                {
                    GridRecursosLista.Add(
                        new GridRecursoIndividualServicios
                        {
                            PATENTE = BM_Database_Servicios.BK_GRID_RECURSO_PATENTE,
                            TIPO = BM_Database_Servicios.BK_GRID_RECURSO_TIPO,
                            MARCA = BM_Database_Servicios.BK_GRID_RECURSO_MARCA,
                            MODELO = BM_Database_Servicios.BK_GRID_RECURSO_MODELO,
                            PROPIETARIO = BM_Database_Servicios.BK_GRID_RECURSO_PROPIETARIO
                        });
                }
            }
            dataGridListadoRecursos.IsReadOnly = true;
            dataGridListadoRecursos.ItemsSource = GridRecursosLista;
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

    public class GridEnvioIndividualServicios
    {
        public string ENVIO { get; set; }
        public string FECHA { get; set; }
        public string CLIENTE { get; set; }
        public string ENTREGA { get; set; }
    }

    public class GridClienteIndividualServicios
    {
        public string RUTID { get; set; }
        public string CLIENTE { get; set; }
    }

    public class GridMensajeroIndividualServicios
    {
        public string RUTID { get; set; }
        public string MENSAJERO { get; set; }
    }

    public class GridRecursoIndividualServicios
    {
        public string PATENTE { get; set; }
        public string TIPO { get; set; }
        public string MARCA { get; set; }
        public string MODELO { get; set; }
        public string PROPIETARIO { get; set; }
    }
}
