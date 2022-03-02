using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageCotizacion : Page
    {
        private List<StructBikeMessengerCotizacion> CotizacionIOArray = new List<StructBikeMessengerCotizacion>();
        private StructBikeMessengerCotizacion CotizacionIO = new StructBikeMessengerCotizacion();
        private Bm_Cotizacion_Database BM_Database_Cotizacion = new Bm_Cotizacion_Database();
        private TransferVar LvrTransferVar = new TransferVar();
        private bool BorrarSiNo;
        private bool ContinuarSiNo;
        private readonly Uri PaginaCotizacion = new Uri("ms-appx-web:///html/EditorCotizaciones.html");
        private string OperacionActual = "";

        public PageCotizacion()
        {
            InitializeComponent();
            BorrarSiNo = false;
            if (LvrTransferVar.ESTADOPARAMETROS == "NADA")
            {
                LvrTransferVar.ESTADOPARAMETROS = "S";
                LvrTransferVar.BASEDEDATOSLOCAL = "S";
                LvrTransferVar.EscribirValoresDeAjustes();
            }
            InciarlistViewCotizaciones();
        }

        private void InciarlistViewCotizaciones()
        {
            List<GridListViewCotizaciones> GridCotizacionesLista = new List<GridListViewCotizaciones>();
            GridCotizacionesLista = BM_Database_Cotizacion.ListaViewCotizaciones();
            DGViewCotizacion.ItemsSource = GridCotizacionesLista;
        }

        private void GeneracionDeColumnas(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header.ToString())
            {
                case "COTIZACION":
                    e.Column.Header = "Nro Cotización";
                    break;
                case "CLIENTE":
                    e.Column.Header = "Cliente";
                    break;
                case "TIPOCARGA":
                    e.Column.Header = "Tipo";
                    break;
                case "ORIGEN":
                    e.Column.Header = "Dirección Origen";
                    break;
                case "DESTINO":
                    e.Column.Header = "Dirección Destino";
                    break;
                case "FECHA_ENTREGA":
                    e.Column.Header = "Fecha de Entrega";
                    break;
                case "HORA_ENTREGA":
                    e.Column.Header = "Hora de Entrega";
                    break;
                case "DISTANCIA":
                    e.Column.Header = "Distancia";
                    break;
                default:
                    break;
            }
        }

        private void BtnSalirVisorDeCotizaciones(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void RevisarDetalleCotizacionServicio(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            // Tomar Valor de PENTALPHA_ID y NRO DE COTIZACION
            string VCOTIZACION = "";
            OperacionActual = "EDITAR";
            appBarNuevo.IsEnabled = false;
            appBarEditar.IsEnabled = true;
            appBarEliminar.IsEnabled = false;
            appBarListado.IsEnabled = true;
            try
            {
                DataGrid CeldaSeleccionada = sender as DataGrid;
                GridListViewCotizaciones Fila = (GridListViewCotizaciones)CeldaSeleccionada.SelectedItems[0];
                VCOTIZACION = Fila.COTIZACION;

                // Buscar en Tabla de Cotizaciones
                CotizacionIOArray = BM_Database_Cotizacion.BuscarCotizacion(LvrTransferVar.PENTALPHA_ID, VCOTIZACION);

                // Llenar CotizacionIO
                if (CotizacionIOArray.Count > 0)
                {
                    CotizacionIO = CotizacionIOArray[0];
                    VisorDetalleCotizaciones.Visibility = Visibility.Visible;
                    DGViewCotizacion.Visibility = Visibility.Collapsed;
                    VisorDetalleCotizaciones.Navigate(PaginaCotizacion);
                }
            }
            catch (System.NullReferenceException)
            {
                ActivarBotones();
            }
            catch (System.ArgumentException)
            {
                ActivarBotones();
            }
            catch (System.Exception)
            {
                ActivarBotones();
            }
        }

        private void BtnNuevaCotizacion(object sender, RoutedEventArgs e)
        {
            OperacionActual = "AGREGAR";
            appBarNuevo.IsEnabled = true;
            appBarEditar.IsEnabled = false;
            appBarEliminar.IsEnabled = false;
            appBarListado.IsEnabled = true;
            VisorDetalleCotizaciones.Visibility = Visibility.Visible;
            DGViewCotizacion.Visibility = Visibility.Collapsed;
            try
            {
                CotizacionIO = new StructBikeMessengerCotizacion();
                VisorDetalleCotizaciones.Navigate(PaginaCotizacion);
            }
            catch (System.NullReferenceException)
            {
                ActivarBotones();
            }
            catch (System.ArgumentException)
            {
                ActivarBotones();
            }
            catch (System.Exception)
            {
                ActivarBotones();
            }
        }

        private void BtnEditarCotizacion(object sender, RoutedEventArgs e)
        {
            OperacionActual = "EDITAR";
            appBarNuevo.IsEnabled = false;
            appBarEditar.IsEnabled = true;
            appBarEliminar.IsEnabled = false;
            appBarListado.IsEnabled = true;
            VisorDetalleCotizaciones.Visibility = Visibility.Visible;
            DGViewCotizacion.Visibility = Visibility.Collapsed;
            try
            {
                VisorDetalleCotizaciones.Navigate(PaginaCotizacion);
            }
            catch (System.NullReferenceException)
            {
                ActivarBotones();
            }
            catch (System.ArgumentException)
            {
                ActivarBotones();
            }
            catch (System.Exception)
            {
                ActivarBotones();
            }
        }

        private void BtnEliminarCotizacion(object sender, RoutedEventArgs e)
        {
            OperacionActual = "ELIMINAR";
            appBarNuevo.IsEnabled = false;
            appBarEditar.IsEnabled = false;
            appBarEliminar.IsEnabled = true;
            appBarListado.IsEnabled = true;
            VisorDetalleCotizaciones.Visibility = Visibility.Visible;
            DGViewCotizacion.Visibility = Visibility.Collapsed;
            try
            {
                VisorDetalleCotizaciones.Navigate(PaginaCotizacion);
            }
            catch (System.NullReferenceException)
            {
                ActivarBotones();
            }
            catch (System.ArgumentException)
            {
                ActivarBotones();
            }
            catch (System.Exception)
            {
                ActivarBotones();
            }
        }

        private void BtnListaDeCotizaciones(object sender, RoutedEventArgs e)
        {
            DGViewCotizacion.Visibility = Visibility.Visible;
            VisorDetalleCotizaciones.Visibility = Visibility.Collapsed;
            InciarlistViewCotizaciones();
            ActivarBotones();
        }

        private void ActivarBotones()
        {
            appBarNuevo.IsEnabled = true;
            appBarEditar.IsEnabled = true;
            appBarEliminar.IsEnabled = true;
            appBarListado.IsEnabled = true;
        }
        private async void VisorCotizacionScriptNotify(object sender, NotifyEventArgs e)
        {
            string RetornoCotizacion = e.Value;
            string[] ParametrosCotizacion = RetornoCotizacion.Split("&&");

            CotizacionIO.OPERACION = OperacionActual;
            CotizacionIO.PKCOTIZACION = LvrTransferVar.PENTALPHA_ID + ParametrosCotizacion[0];
            CotizacionIO.PENTALPHA = LvrTransferVar.PENTALPHA_ID;
            CotizacionIO.COTIZACION = ParametrosCotizacion[0];
            CotizacionIO.NOMBRE = ParametrosCotizacion[1];
            CotizacionIO.TELEFONO = ParametrosCotizacion[2];
            CotizacionIO.EMAIL = ParametrosCotizacion[3];
            CotizacionIO.OCALLE = ParametrosCotizacion[4];
            CotizacionIO.ONUMERO = ParametrosCotizacion[5];
            CotizacionIO.ODPTO = ParametrosCotizacion[6];
            CotizacionIO.OOFICINA = ParametrosCotizacion[7];
            CotizacionIO.OCASA = ParametrosCotizacion[8];
            CotizacionIO.OCOMUNA = ParametrosCotizacion[9];
            CotizacionIO.ONOMBRE = ParametrosCotizacion[10];
            CotizacionIO.OTELEFONO = ParametrosCotizacion[11];
            CotizacionIO.DCALLE = ParametrosCotizacion[12];
            CotizacionIO.DNUMERO = ParametrosCotizacion[13];
            CotizacionIO.DDPTO = ParametrosCotizacion[14];
            CotizacionIO.DOFICINA = ParametrosCotizacion[15];
            CotizacionIO.DCASA = ParametrosCotizacion[16];
            CotizacionIO.DCOMUNA = ParametrosCotizacion[17];
            CotizacionIO.DNOMBRE = ParametrosCotizacion[18];
            CotizacionIO.DTELEFONO = ParametrosCotizacion[19];
            CotizacionIO.REGRESODE = ParametrosCotizacion[20];
            CotizacionIO.TIPOCARGA = ParametrosCotizacion[21];
            
            try
            {
                CotizacionIO.LARGO = Double.Parse(ParametrosCotizacion[22]);
            }
            catch (System.FormatException)
            {
                CotizacionIO.LARGO = 0;
            }
            
            try
            {
                CotizacionIO.ANCHO = Double.Parse(ParametrosCotizacion[23]);
            }
            catch (System.FormatException)
            {
                CotizacionIO.ANCHO = 0;
            }

            try
            {
                CotizacionIO.ALTO = Double.Parse(ParametrosCotizacion[24]);
            }
            catch (System.FormatException)
            {
                CotizacionIO.ALTO = 0;
            }

            try
            {
                CotizacionIO.PESO = Double.Parse(ParametrosCotizacion[25]);
            }
            catch (System.FormatException)
            {
                CotizacionIO.PESO = 0;
            }

            CotizacionIO.DESCRIPCION = ParametrosCotizacion[26];
            CotizacionIO.FECHAENTREGA = ParametrosCotizacion[27];
            CotizacionIO.HORAENTREGA = ParametrosCotizacion[28];

            try
            {
                CotizacionIO.DISTANCIA = Double.Parse(ParametrosCotizacion[29]);
            }
            catch (System.FormatException)
            {
                CotizacionIO.DISTANCIA = 0;
            }

            CotizacionIO.RESOPERACION = "OK";
            CotizacionIO.RESMENSAJE = "OK";
            if (OperacionActual.Equals("AGREGAR"))
            {
                if (BM_Database_Cotizacion.AgregarCotizacion(CotizacionIO))
                {
                    await AvisoOperacionCotizacionDialogAsync("Agregar Cotización", "Cotización agregada exitosamente.");                   
                }
                else
                {
                    await AvisoOperacionCotizacionDialogAsync("Agregar Cotización", "Error en ingreso de Cotización. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            else if (OperacionActual.Equals("EDITAR"))
            {
                if (BM_Database_Cotizacion.ModificarCotizacion(CotizacionIO))
                {
                    await AvisoOperacionCotizacionDialogAsync("Modificar Cotización", "Cotización agregada exitosamente.");
                }
                else
                {
                    await AvisoOperacionCotizacionDialogAsync("Modificar Cotización", "Error en ingreso de Cotización. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            else if (OperacionActual.Equals("ELIMINAR"))
            {
                BorrarSiNo = false;

                await AvisoBorrarCotizacionDialogAsync();

                if (!BorrarSiNo)
                {
                    return;
                }

                if (BM_Database_Cotizacion.BorrarCotizacion(LvrTransferVar.PENTALPHA_ID, CotizacionIO.PKCOTIZACION))
                {
                    await AvisoOperacionCotizacionDialogAsync("Borrar Cotizacion", "Cotización borrada exitosamente.");

                }
                else
                {
                    await AvisoOperacionCotizacionDialogAsync("Borrar Cotizacion", "Error en ingreso de Cotización. Reintente o escriba a soporte contacto@pentalpha.net");
                }
            }
            else
            {
                OperacionActual = "";
            }
            OperacionActual = "";
        }

        private async Task AvisoOperacionCotizacionDialogAsync(string xTitulo, string xDescripcion)
        {
            ContentDialog AvisoOperacionCotizacionDialog = new ContentDialog
            {
                Title = xTitulo,
                Content = xDescripcion,
                CloseButtonText = "Continuar"
            };
            _ = await AvisoOperacionCotizacionDialog.ShowAsync();
        }


        private async Task AvisoBorrarCotizacionDialogAsync()
        {
            ContentDialog AvisoConfirmacionCotizacionDialog = new ContentDialog
            {
                Title = "Borrar Cotización",
                Content = "Confirme borrado del registro actual!",
                PrimaryButtonText = "Borrar",
                CloseButtonText = "Cancelar"
            };

            ContentDialogResult result = await AvisoConfirmacionCotizacionDialog.ShowAsync();

            BorrarSiNo = result == ContentDialogResult.Primary;
        }

        private async void VisorCotizacionLlenadoFormulario(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            string[] Parametros = new string[30];

            Parametros[0] = CotizacionIO.COTIZACION ?? "";
            Parametros[1] = CotizacionIO.NOMBRE ?? "";
            Parametros[2] = CotizacionIO.TELEFONO ?? "";
            Parametros[3] = CotizacionIO.EMAIL ?? "";
            Parametros[4] = CotizacionIO.OCALLE ?? "";
            Parametros[5] = CotizacionIO.ONUMERO ?? "";
            Parametros[6] = CotizacionIO.ODPTO ?? "";
            Parametros[7] = CotizacionIO.OOFICINA ?? "";
            Parametros[8] = CotizacionIO.OCASA ?? "";
            Parametros[9] = CotizacionIO.OCOMUNA ?? "";
            Parametros[10] = CotizacionIO.ONOMBRE ?? "";
            Parametros[11] = CotizacionIO.OTELEFONO ?? "";
            Parametros[12] = CotizacionIO.DCALLE ?? "";
            Parametros[13] = CotizacionIO.DNUMERO ?? "";
            Parametros[14] = CotizacionIO.DDPTO ?? "";
            Parametros[15] = CotizacionIO.DOFICINA ?? "";
            Parametros[16] = CotizacionIO.DCASA ?? "";
            Parametros[17] = CotizacionIO.DCOMUNA ?? "";
            Parametros[18] = CotizacionIO.DNOMBRE ?? "";
            Parametros[19] = CotizacionIO.DTELEFONO ?? "";
            Parametros[20] = CotizacionIO.REGRESODE ?? "";
            Parametros[21] = CotizacionIO.TIPOCARGA ?? "";
            Parametros[22] = CotizacionIO.LARGO.ToString() ?? "0";
            Parametros[23] = CotizacionIO.ANCHO.ToString() ?? "0";
            Parametros[24] = CotizacionIO.ALTO.ToString() ?? "0";
            Parametros[25] = CotizacionIO.PESO.ToString() ?? "0";
            Parametros[26] = CotizacionIO.DESCRIPCION ?? "";
            Parametros[27] = CotizacionIO.FECHAENTREGA ?? "";
            Parametros[28] = CotizacionIO.HORAENTREGA ?? "";
            Parametros[29] = CotizacionIO.DISTANCIA.ToString() ?? "0";
            _ = await VisorDetalleCotizaciones.InvokeScriptAsync("llenadoFormulario", Parametros);
        }
    }
}
