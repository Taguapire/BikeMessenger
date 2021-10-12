using Microsoft.Toolkit.Uwp.UI.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private List<JsonBikeMessengerServicio> ServicioIOArray = new List<JsonBikeMessengerServicio>();
        private JsonBikeMessengerServicio ServicioIO = new JsonBikeMessengerServicio();
        private JsonBikeMessengerServicio EnviarJsonServicio = new JsonBikeMessengerServicio();
        private JsonBikeMessengerServicio RecibirJsonServicio = new JsonBikeMessengerServicio();
        private Bm_Servicio_Database BM_Database_Servicio = new Bm_Servicio_Database();
        private TransferVar LvrTransferVar = new TransferVar();
        private bool BorrarSiNo;

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
                ServicioIOArray = BM_Database_Servicio.BuscarServicio();
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
            List<string> ListaPais = BM_Database_Servicio.GetPais();
            if (ListaPais != null)
            {
                try
                {
                    for (int i = 0; i < ListaPais.Count; i++)
                    {
                        comboBoxOrigenPais.Items.Add(ListaPais[i]);
                        comboBoxDestinoPais.Items.Add(ListaPais[i]);
                    }
                }
                catch (NullReferenceException)
                {

                }
            }

            // Llenar Combo Region
            List<string> ListaEstado = BM_Database_Servicio.GetRegion();

            if (ListaEstado != null)
            {
                try
                {
                    for (int i = 0; i < ListaEstado.Count; i++)
                    {
                        comboBoxOrigenEstado.Items.Add(ListaEstado[i]);
                        comboBoxDestinoEstado.Items.Add(ListaEstado[i]);
                    }
                }
                catch (NullReferenceException)
                {

                }
            }


            // Llenar Combo Comuna
            List<string> ListaComuna = BM_Database_Servicio.GetComuna();

            if (ListaComuna != null)
            {
                try
                {
                    for (int i = 0; i < ListaComuna.Count; i++)
                    {
                        comboBoxOrigenComuna.Items.Add(ListaComuna[i]);
                        comboBoxDestinoComuna.Items.Add(ListaComuna[i]);
                    }
                }
                catch (NullReferenceException)
                {

                }
            }

            // Llenar Combo Ciudad
            List<string> ListaCiudad = BM_Database_Servicio.GetCiudad();

            if (ListaCiudad != null)
            {
                try
                {
                    for (int i = 0; i < ListaCiudad.Count; i++)
                    {
                        comboBoxOrigenCiudad.Items.Add(ListaCiudad[i]);
                        comboBoxDestinoCiudad.Items.Add(ListaCiudad[i]);
                    }
                }
                catch (NullReferenceException)
                {

                }
            }
        }

        private async void BtnAgregarServicios(object sender, RoutedEventArgs e)
        {
            LvrProgresRing.IsActive = true;

            await Task.Delay(500);

            LlenarDbConPantalla();

            if (BM_Database_Servicio.AgregarServicio(ServicioIO))
            {

                // Muestra de espera
                bool TransaccionOK = true;
                if (LvrTransferVar.SincronizarWebPentalpha())
                {
                    TransaccionOK = ProRegistroServicio("AGREGAR");
                }

                if (LvrTransferVar.SincronizarWebPropio())
                {
                    TransaccionOK = ProRegistroServicio("AGREGAR");
                }

                if (TransaccionOK)
                {
                    await AvisoOperacionServiciosDialogAsync("Agregar Servicio", "Servicio agregado exitosamente.");
                    LlenarListaEnvios();
                    LvrTransferVar.SER_PENTALPHA = ServicioIO.PENTALPHA;
                    LvrTransferVar.SER_NROENVIO = ServicioIO.NROENVIO;
                    LvrTransferVar.EscribirValoresDeAjustes();
                    LvrTransferVar.LeerValoresDeAjustes();
                }

                else
                {
                    await AvisoOperacionServiciosDialogAsync("Agregar Servicio", "Error en ingreso de servicio. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            else
            {
                await AvisoOperacionServiciosDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de cliente.");
            }

            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }

        private async void BtnModificarServicios(object sender, RoutedEventArgs e)
        {
            // Muestra de espera
            bool TransaccionOK = false;
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // .5 sec delay


            LlenarDbConPantalla();

            if (BM_Database_Servicio.ModificarServicio(ServicioIO))
            {
                TransaccionOK = true;

                if (LvrTransferVar.SincronizarWebPentalpha())
                {
                    TransaccionOK = ProRegistroServicio("MODIFICAR");
                }

                if (LvrTransferVar.SincronizarWebPropio())
                {
                    TransaccionOK = ProRegistroServicio("MODIFICAR");
                }

                if (TransaccionOK)
                {
                    LlenarListaEnvios();
                    LvrTransferVar.SER_PENTALPHA = ServicioIO.PENTALPHA;
                    LvrTransferVar.SER_NROENVIO = ServicioIO.NROENVIO;
                    LvrTransferVar.EscribirValoresDeAjustes();
                    LvrTransferVar.LeerValoresDeAjustes();
                    await AvisoOperacionServiciosDialogAsync("Modificar Servicio", "Servicio modificado exitosamente.");
                }
                else
                {
                    await AvisoOperacionServiciosDialogAsync("Modificar Servicio", "Error en modificación de servicio. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            else
            {
                await AvisoOperacionServiciosDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de cliente.");
            }

            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
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

            LlenarDbConPantalla();

            bool TransaccionOK = false;

            if (BM_Database_Servicio.BorrarServicio(LvrTransferVar.SER_PENTALPHA, ServicioIO.NROENVIO))
            {
                TransaccionOK = true;

                if (LvrTransferVar.SincronizarWebPentalpha())
                {
                    TransaccionOK = ProRegistroServicio("BORRAR");
                }

                if (LvrTransferVar.SincronizarWebPropio())
                {
                    TransaccionOK = ProRegistroServicio("BORRAR");
                }

                if (TransaccionOK)
                {
                    ServicioIOArray = BM_Database_Servicio.BuscarServicio();

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

                    await AvisoOperacionServiciosDialogAsync("Borrar Servicio", "Servicio borrado exitosamente.");
                }
                else
                {
                    await AvisoOperacionServiciosDialogAsync("Borrar Servicio", "Error en borrado de servicio. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            else
            {
                await AvisoOperacionServiciosDialogAsync("Acceso a Base de Datos", "Debe llenar los datos de cliente.");
            }
            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
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
                textBoxNroDeEnvio.Text = ServicioIO.NROENVIO;
                textBoxGuiaDeDespacho.Text = ServicioIO.GUIADESPACHO;
                controlFecha.SelectedDate = DateTimeOffset.Parse(ServicioIO.FECHA.ToString());
                controlHora.SelectedTime = TimeSpan.Parse(ServicioIO.HORA.ToString());
                textBoxRutID.Text = ServicioIO.CLIENTERUT;
                textBoxDigitoVerificador.Text = ServicioIO.CLIENTEDIGVER;
                textBoxCliente.Text = ""; // OJO
                textBoxIdMensajero.Text = ServicioIO.MENSAJERORUT + "-" + ServicioIO.MENSAJERODIGVER;
                textBoxNombreMensajero.Text = ""; // OJO;
                textBoxIdRecurso.Text = ServicioIO.RECURSOID;
                textBoxNombreRecurso.Text = ""; // OJO;
                textBoxOrigenDomicilio1.Text = ServicioIO.ODOMICILIO1;
                textBoxOrigenNumero.Text = ServicioIO.ONUMERO;
                textBoxOrigenPiso.Text = ServicioIO.OPISO;
                textBoxOrigenOficina.Text = ServicioIO.OOFICINA;
                textBoxDestinoDomicilio1.Text = ServicioIO.DDOMICILIO1;
                textBoxDestinoNumero.Text = ServicioIO.DNUMERO;
                textBoxDestinoPiso.Text = ServicioIO.DPISO;
                textBoxDestinoOficina.Text = ServicioIO.DOFICINA;
                textBoxDescripcion.Text = ServicioIO.DESCRIPCION;
                textBoxFacturas.Text = ServicioIO.FACTURAS.ToString();
                textBoxBultos.Text = ServicioIO.BULTOS.ToString();
                textBoxCompras.Text = ServicioIO.COMPRAS.ToString();
                textBoxCheques.Text = ServicioIO.CHEQUES.ToString();
                textBoxSobres.Text = ServicioIO.SOBRES.ToString();
                textBoxOtros.Text = ServicioIO.OTROS.ToString();
                textBoxObservaciones.Text = ServicioIO.OBSERVACIONES;
                textBoxEntrega.Text = ServicioIO.ENTREGA;
                textBoxRecepcion.Text = ServicioIO.RECEPCION;
                textBoxTiempoDeEspera.Text = ServicioIO.TESPERA.ToString();
                comboBoxOrigenPais.SelectedValue = ServicioIO.OPAIS;
                comboBoxOrigenEstado.SelectedValue = ServicioIO.OESTADO;
                comboBoxOrigenComuna.SelectedValue = ServicioIO.OCOMUNA;
                comboBoxOrigenCiudad.SelectedValue = ServicioIO.OCIUDAD;
                comboBoxDestinoPais.SelectedValue = ServicioIO.DPAIS;
                comboBoxDestinoEstado.SelectedValue = ServicioIO.DESTADO;
                comboBoxDestinoComuna.SelectedValue = ServicioIO.DCOMUNA;
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
            ServicioIO.GUIADESPACHO = textBoxGuiaDeDespacho.Text;
            ServicioIO.FECHA = controlFecha.Date.Date.ToShortDateString();
            ServicioIO.HORA = controlHora.Time.ToString();
            ServicioIO.CLIENTERUT = textBoxRutID.Text;
            ServicioIO.CLIENTEDIGVER = textBoxDigitoVerificador.Text;
            string xyRutCompleto = textBoxIdMensajero.Text; ;
            string[] CadenaDividida = xyRutCompleto.Split("-", 2, StringSplitOptions.None);
            ServicioIO.MENSAJERORUT = CadenaDividida[0];
            ServicioIO.MENSAJERODIGVER = CadenaDividida[1];
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

            ServicioIO.OCOORDENADAS = "*";
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

            ServicioIO.DCOORDENADAS = "*";
            ServicioIO.DESCRIPCION = textBoxDescripcion.Text;

            ServicioIO.FACTURAS = textBoxFacturas.Text;
            ServicioIO.BULTOS = textBoxBultos.Text;
            ServicioIO.COMPRAS = textBoxCompras.Text;
            ServicioIO.CHEQUES = textBoxCheques.Text;
            ServicioIO.SOBRES = textBoxSobres.Text;
            ServicioIO.OTROS = textBoxOtros.Text;

            ServicioIO.OBSERVACIONES = textBoxObservaciones.Text;
            ServicioIO.ENTREGA = textBoxEntrega.Text;
            ServicioIO.RECEPCION = textBoxRecepcion.Text; ;
            ServicioIO.TESPERA = controlHora.Time.ToString();
            ServicioIO.FECHAENTREGA = controlFecha.Date.Date.ToShortDateString();
            ServicioIO.HORAENTREGA = controlHora.Time.ToString();
            ServicioIO.DISTANCIA = "0";
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

            GridServicioDbArray = BM_Database_Servicio.BuscarGridServicios();
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

            GridClienteDbArray = BM_Database_Servicio.BuscarGridClientes();
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

            GridMensajerosDbArray = BM_Database_Servicio.BuscarGridPersonal();
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


            GridRecursoDbArray = BM_Database_Servicio.BuscarGridRecurso();
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

        //**************************************************
        // Ejecuta operacion de registro de Servicio
        //**************************************************
        private bool ProRegistroServicio(string pTipoOperacion)
        {
            string LvrPRecibirServer;
            string LvrPData;
            string LvrStringHttp = "https://finanven.ddns.net";
            string LvrStringPort = "443";
            string LvrStringController = "/Api/BikeMessengerServicio";

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

            LvrBKInternet.LvrInetPOST(LvrStringHttp, LvrStringPort, LvrStringController, LvrParametros);
            LvrPRecibirServer = LvrBKInternet.LvrResultadoWeb;

            if (LvrPRecibirServer != "ERROR" && LvrPRecibirServer != "" && LvrPRecibirServer != null)
            {
                // Procesar primer servidor
                RecibirJsonServicioArray = JsonConvert.DeserializeObject<List<JsonBikeMessengerServicio>>(LvrPRecibirServer); // resp será el string JSON a deserializa
                RecibirJsonServicio = RecibirJsonServicioArray[0];

                return RecibirJsonServicio.RESOPERACION == "OK";
            }
            return false;
        }

        private void CopiarMemoriaEnJson(string pOPERACION)
        {
            // Llenar Variables
            EnviarJsonServicio.OPERACION = pOPERACION;
            EnviarJsonServicio.PENTALPHA = ServicioIO.PENTALPHA;
            EnviarJsonServicio.NROENVIO = ServicioIO.NROENVIO;
            EnviarJsonServicio.GUIADESPACHO = ServicioIO.GUIADESPACHO;
            EnviarJsonServicio.FECHA = ServicioIO.FECHA;
            EnviarJsonServicio.HORA = ServicioIO.HORA;
            EnviarJsonServicio.CLIENTERUT = ServicioIO.CLIENTERUT;
            EnviarJsonServicio.CLIENTEDIGVER = ServicioIO.CLIENTEDIGVER;
            EnviarJsonServicio.MENSAJERORUT = ServicioIO.MENSAJERORUT;
            EnviarJsonServicio.MENSAJERODIGVER = ServicioIO.MENSAJERODIGVER;
            EnviarJsonServicio.RECURSOID = ServicioIO.RECURSOID;
            EnviarJsonServicio.ODOMICILIO1 = ServicioIO.ODOMICILIO1;
            EnviarJsonServicio.ODOMICILIO2 = ServicioIO.ODOMICILIO2;
            EnviarJsonServicio.ONUMERO = ServicioIO.ONUMERO;
            EnviarJsonServicio.OPISO = ServicioIO.OPISO;
            EnviarJsonServicio.OOFICINA = ServicioIO.OOFICINA;
            EnviarJsonServicio.OCIUDAD = ServicioIO.OCIUDAD;
            EnviarJsonServicio.OCOMUNA = ServicioIO.OCOMUNA;
            EnviarJsonServicio.OESTADO = ServicioIO.OESTADO;
            EnviarJsonServicio.OPAIS = ServicioIO.OPAIS;
            EnviarJsonServicio.OCOORDENADAS = ServicioIO.OCOORDENADAS;
            EnviarJsonServicio.DDOMICILIO1 = ServicioIO.DDOMICILIO1;
            EnviarJsonServicio.DDOMICILIO2 = ServicioIO.DDOMICILIO2;
            EnviarJsonServicio.DNUMERO = ServicioIO.DNUMERO;
            EnviarJsonServicio.DPISO = ServicioIO.DPISO;
            EnviarJsonServicio.DOFICINA = ServicioIO.DOFICINA;
            EnviarJsonServicio.DCIUDAD = ServicioIO.DCIUDAD;
            EnviarJsonServicio.DCOMUNA = ServicioIO.DCOMUNA;
            EnviarJsonServicio.DESTADO = ServicioIO.DESTADO;
            EnviarJsonServicio.DPAIS = ServicioIO.DPAIS;
            EnviarJsonServicio.DCOORDENADAS = ServicioIO.DCOORDENADAS;
            EnviarJsonServicio.DESCRIPCION = ServicioIO.DESCRIPCION;
            EnviarJsonServicio.FACTURAS = ServicioIO.FACTURAS;
            EnviarJsonServicio.BULTOS = ServicioIO.BULTOS;
            EnviarJsonServicio.COMPRAS = ServicioIO.COMPRAS;
            EnviarJsonServicio.CHEQUES = ServicioIO.CHEQUES;
            EnviarJsonServicio.SOBRES = ServicioIO.SOBRES;
            EnviarJsonServicio.OTROS = ServicioIO.OTROS;
            EnviarJsonServicio.OBSERVACIONES = ServicioIO.OBSERVACIONES;
            EnviarJsonServicio.ENTREGA = ServicioIO.ENTREGA;
            EnviarJsonServicio.RECEPCION = ServicioIO.RECEPCION;
            EnviarJsonServicio.TESPERA = ServicioIO.TESPERA;
            EnviarJsonServicio.FECHAENTREGA = ServicioIO.FECHAENTREGA;
            EnviarJsonServicio.HORAENTREGA = ServicioIO.HORAENTREGA;
            EnviarJsonServicio.DISTANCIA = ServicioIO.DISTANCIA;
            EnviarJsonServicio.PROGRAMADO = ServicioIO.PROGRAMADO;
            EnviarJsonServicio.RESOPERACION = "";
            EnviarJsonServicio.RESMENSAJE = "";
        }

        private void CopiarJsonEnMemoria(string pOPERACION)
        {
            // Proceso
            // EnviarJsonServicio.OPERACION = pOPERACION;
            ServicioIO.PENTALPHA = EnviarJsonServicio.PENTALPHA;
            ServicioIO.NROENVIO = EnviarJsonServicio.NROENVIO;
            ServicioIO.GUIADESPACHO = EnviarJsonServicio.GUIADESPACHO;
            ServicioIO.FECHA = EnviarJsonServicio.FECHA;
            ServicioIO.HORA = EnviarJsonServicio.HORA;
            ServicioIO.CLIENTERUT = EnviarJsonServicio.CLIENTERUT;
            ServicioIO.CLIENTEDIGVER = EnviarJsonServicio.CLIENTEDIGVER;
            ServicioIO.MENSAJERORUT = EnviarJsonServicio.MENSAJERORUT;
            ServicioIO.MENSAJERODIGVER = EnviarJsonServicio.MENSAJERODIGVER;
            ServicioIO.RECURSOID = EnviarJsonServicio.RECURSOID;
            ServicioIO.ODOMICILIO1 = EnviarJsonServicio.ODOMICILIO1;
            ServicioIO.ODOMICILIO2 = EnviarJsonServicio.ODOMICILIO2;
            ServicioIO.ONUMERO = EnviarJsonServicio.ONUMERO;
            ServicioIO.OPISO = EnviarJsonServicio.OPISO;
            ServicioIO.OOFICINA = EnviarJsonServicio.OOFICINA;
            ServicioIO.OCIUDAD = EnviarJsonServicio.OCIUDAD;
            ServicioIO.OCOMUNA = EnviarJsonServicio.OCOMUNA;
            ServicioIO.OESTADO = EnviarJsonServicio.OESTADO;
            ServicioIO.OPAIS = EnviarJsonServicio.OPAIS;
            ServicioIO.OCOORDENADAS = EnviarJsonServicio.OCOORDENADAS;
            ServicioIO.DDOMICILIO1 = EnviarJsonServicio.DDOMICILIO1;
            ServicioIO.DDOMICILIO2 = EnviarJsonServicio.DDOMICILIO2;
            ServicioIO.DNUMERO = EnviarJsonServicio.DNUMERO;
            ServicioIO.DPISO = EnviarJsonServicio.DPISO;
            ServicioIO.DOFICINA = EnviarJsonServicio.DOFICINA;
            ServicioIO.DCIUDAD = EnviarJsonServicio.DCIUDAD;
            ServicioIO.DCOMUNA = EnviarJsonServicio.DCOMUNA;
            ServicioIO.DESTADO = EnviarJsonServicio.DESTADO;
            ServicioIO.DPAIS = EnviarJsonServicio.DPAIS;
            ServicioIO.DCOORDENADAS = EnviarJsonServicio.DCOORDENADAS;
            ServicioIO.DESCRIPCION = EnviarJsonServicio.DESCRIPCION;
            ServicioIO.FACTURAS = EnviarJsonServicio.FACTURAS;
            ServicioIO.BULTOS = EnviarJsonServicio.BULTOS;
            ServicioIO.COMPRAS = EnviarJsonServicio.COMPRAS;
            ServicioIO.CHEQUES = EnviarJsonServicio.CHEQUES;
            ServicioIO.SOBRES = EnviarJsonServicio.SOBRES;
            ServicioIO.OTROS = EnviarJsonServicio.OTROS;
            ServicioIO.OBSERVACIONES = EnviarJsonServicio.OBSERVACIONES;
            ServicioIO.ENTREGA = EnviarJsonServicio.ENTREGA;
            ServicioIO.RECEPCION = EnviarJsonServicio.RECEPCION;
            ServicioIO.TESPERA = EnviarJsonServicio.TESPERA;
            ServicioIO.FECHAENTREGA = EnviarJsonServicio.FECHAENTREGA;
            ServicioIO.HORAENTREGA = EnviarJsonServicio.HORAENTREGA;
            ServicioIO.DISTANCIA = EnviarJsonServicio.DISTANCIA;
            ServicioIO.PROGRAMADO = EnviarJsonServicio.PROGRAMADO;
            // EnviarJsonServicio.RESOPERACION = "";
            // EnviarJsonServicio.RESMENSAJE = "";
        }

        private void BtnListarServicio(object sender, RoutedEventArgs e)
        {
            {
                LvrTransferVar.PantallaAnterior = "SERVICIO";
                _ = Frame.Navigate(typeof(ListadosGenerales), LvrTransferVar, new SuppressNavigationTransitionInfo());
            }
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
