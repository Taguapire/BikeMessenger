using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageCotizacion : Page
    {
        private TransferVar LvrTransferVar = new TransferVar();
        private bool ContinuarSiNo;
        private readonly Uri PaginaCotizacion = new Uri("ms-appx-web:///html/EditorCotizaciones.html");
        private StructBikeMessengerCotizacion DbCotizacion = new StructBikeMessengerCotizacion();

        public PageCotizacion()
        {
            InitializeComponent();
            InciarlistViewServicios();
            ContinuarSiNo = false;
            if (LvrTransferVar.ESTADOPARAMETROS == "NADA")
            {
                LvrTransferVar.ESTADOPARAMETROS = "S";
                LvrTransferVar.BASEDEDATOSLOCAL = "S";
                LvrTransferVar.EscribirValoresDeAjustes();
            }
        }

        private async Task AvisoIniciarParemetrosDialogAsync()
        {
            ContentDialog AvisoConfirmacionPersonalDialog = new ContentDialog
            {
                Title = "Iniciar en Ajuste",
                Content = "Usted aun no a definido Base de Datos.",
                PrimaryButtonText = "Aceptar Usar SQLite",
                CloseButtonText = "Cancelar y definir valores en Ajustes"
            };

            ContentDialogResult result = await AvisoConfirmacionPersonalDialog.ShowAsync();

            ContinuarSiNo = result == ContentDialogResult.Primary;
        }

        private void InciarlistViewServicios()
        {

            List<GridListViewCotizaciones> GridCotizacionesLista = new List<GridListViewCotizaciones>();

            string CompletoNombreBD = LvrTransferVar.DIRECTORIO_BASE_LOCAL + "\\BikeMessenger.db";

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            if (LvrTransferVar.ESTADOPARAMETROS == "NADA")
            {
                return;
            }

            List<TbVistaCotizacionCliMen> results = BM_ConexionLite.Query<TbVistaCotizacionCliMen>("select * from Vista_Cotizacion_CliMen");

            for (int i = 0; i < results.Count; i++)
            {
                GridCotizacionesLista.Add(new GridListViewCotizaciones
                {
                    COTIZACION = results[i].COTIZACION,
                    CLIENTE = results[i].NOMBRE,
                    TIPOCARGA = results[i].TIPOCARGA,
                    ORIGEN = results[i].ORIGEN,
                    DESTINO = results[i].DESTINO,
                    FECHA_ENTREGA = results[i].FECHAENTREGA,
                    HORA_ENTREGA = results[i].HORAENTREGA,
                    DISTANCIA = results[i].DISTANCIA
                });
            }

            DGViewCotizacion.ItemsSource = GridCotizacionesLista;

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();
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
            VisorDetalleCotizaciones.Visibility = Visibility.Visible;
            DGViewCotizacion.Visibility = Visibility.Collapsed;
        }

        private void BtnNuevaCotizacion(object sender, RoutedEventArgs e)
        {
            VisorDetalleCotizaciones.Visibility = Visibility.Visible;
            DGViewCotizacion.Visibility = Visibility.Collapsed;
            try
            {
                VisorDetalleCotizaciones.Navigate(PaginaCotizacion);
            }
            catch (System.ArgumentException)
            {
                ;
            }
            catch (System.Exception)
            {
                ;
            }
        }

        private void BtnEditarCotizacion(object sender, RoutedEventArgs e)
        {
            VisorDetalleCotizaciones.Visibility = Visibility.Visible;
            DGViewCotizacion.Visibility = Visibility.Collapsed;
            try
            {
                VisorDetalleCotizaciones.Navigate(PaginaCotizacion);
            }
            catch (System.ArgumentException)
            {
                ;
            }
            catch (System.Exception)
            {
                ;
            }
        }

        private void BtnEliminarCotizacion(object sender, RoutedEventArgs e)
        {
            VisorDetalleCotizaciones.Visibility = Visibility.Visible;
            DGViewCotizacion.Visibility = Visibility.Collapsed;
        }

        private void BtnListaDeCotizaciones(object sender, RoutedEventArgs e)
        {
            DGViewCotizacion.Visibility = Visibility.Visible;
            VisorDetalleCotizaciones.Visibility = Visibility.Collapsed;
        }

        private void VisorCotizacionScriptNotify(object sender, NotifyEventArgs e)
        {
            string RetornoCotizacion = e.Value;
            string[] ParametrosCotizacion = RetornoCotizacion.Split("&&");

            DbCotizacion.OPERACION = "ACTUALIZAR";
            DbCotizacion.PENTALPHA = LvrTransferVar.PENTALPHA_ID + "1";
            DbCotizacion.PENTALPHA = LvrTransferVar.PENTALPHA_ID;
            DbCotizacion.COTIZACION = "1";
            DbCotizacion.NOMBRE = ParametrosCotizacion[0];
            DbCotizacion.TELEFONO = ParametrosCotizacion[1];
            DbCotizacion.EMAIL = ParametrosCotizacion[2];
            DbCotizacion.OCALLE = ParametrosCotizacion[3];
            DbCotizacion.ONUMERO = ParametrosCotizacion[4];
            DbCotizacion.ODPTO = ParametrosCotizacion[5];
            DbCotizacion.OOFICINA = ParametrosCotizacion[6];
            DbCotizacion.OCASA = ParametrosCotizacion[7];
            DbCotizacion.OCOMUNA = ParametrosCotizacion[8];
            DbCotizacion.ONOMBRE = ParametrosCotizacion[9];
            DbCotizacion.OTELEFONO = ParametrosCotizacion[10];
            DbCotizacion.DCALLE = ParametrosCotizacion[11];
            DbCotizacion.DNUMERO = ParametrosCotizacion[12];
            DbCotizacion.DDPTO = ParametrosCotizacion[13];
            DbCotizacion.DOFICINA = ParametrosCotizacion[14];
            DbCotizacion.DCASA = ParametrosCotizacion[15];
            DbCotizacion.DCOMUNA = ParametrosCotizacion[16];
            DbCotizacion.DNOMBRE = ParametrosCotizacion[17];
            DbCotizacion.DTELEFONO = ParametrosCotizacion[18];
            DbCotizacion.REGRESODE = ParametrosCotizacion[19];
            DbCotizacion.TIPOCARGA = ParametrosCotizacion[20];
            DbCotizacion.LARGO = Double.Parse(ParametrosCotizacion[21]);
            DbCotizacion.ANCHO = Double.Parse(ParametrosCotizacion[22]);
            DbCotizacion.ALTO = Double.Parse(ParametrosCotizacion[23]);
            DbCotizacion.PESO = Double.Parse(ParametrosCotizacion[24]);
            DbCotizacion.DESCRIPCION = ParametrosCotizacion[25];
            DbCotizacion.FECHAENTREGA = ParametrosCotizacion[26];
            DbCotizacion.HORAENTREGA = ParametrosCotizacion[27];
            DbCotizacion.DISTANCIA = Double.Parse(ParametrosCotizacion[28]);
            DbCotizacion.RESOPERACION = "OK";
            DbCotizacion.RESMENSAJE = "OK";
        }

        private async void VisorCotizacionLlenadoFormulario(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            string[] Parametros = new string[29];
            Parametros[0] = DbCotizacion.NOMBRE ?? "";
            Parametros[1] = DbCotizacion.TELEFONO ?? "";
            Parametros[2] = DbCotizacion.EMAIL ?? "";
            Parametros[3] = DbCotizacion.OCALLE ?? "";
            Parametros[4] = DbCotizacion.ONUMERO ?? "";
            Parametros[5] = DbCotizacion.ODPTO ?? "";
            Parametros[6] = DbCotizacion.OOFICINA ?? "";
            Parametros[7] = DbCotizacion.OCASA ?? "";
            Parametros[8] = DbCotizacion.OCOMUNA ?? "";
            Parametros[9] = DbCotizacion.ONOMBRE ?? "";
            Parametros[10] = DbCotizacion.OTELEFONO ?? "";
            Parametros[11] = DbCotizacion.DCALLE ?? "";
            Parametros[12] = DbCotizacion.DNUMERO ?? "";
            Parametros[13] = DbCotizacion.DDPTO ?? "";
            Parametros[14] = DbCotizacion.DOFICINA ?? "";
            Parametros[15] = DbCotizacion.DCASA ?? "";
            Parametros[16] = DbCotizacion.DCOMUNA ?? "";
            Parametros[17] = DbCotizacion.DNOMBRE ?? "";
            Parametros[18] = DbCotizacion.DTELEFONO ?? "";
            Parametros[19] = DbCotizacion.REGRESODE ?? "NO";
            Parametros[20] = DbCotizacion.TIPOCARGA ?? "OTROS";
            Parametros[21] = DbCotizacion.LARGO.ToString() ?? "0";
            Parametros[22] = DbCotizacion.ANCHO.ToString() ?? "0";
            Parametros[23] = DbCotizacion.ALTO.ToString() ?? "0";
            Parametros[24] = DbCotizacion.PESO.ToString() ?? "0";
            Parametros[25] = DbCotizacion.DESCRIPCION ?? "";
            Parametros[26] = DbCotizacion.FECHAENTREGA ?? "";
            Parametros[27] = DbCotizacion.HORAENTREGA ?? "";
            Parametros[28] = DbCotizacion.DISTANCIA.ToString() ?? "0";
            _ = await VisorDetalleCotizaciones.InvokeScriptAsync("llenadoFormulario", Parametros);
        }
    }

    internal class GridListViewCotizaciones
    {
        public string COTIZACION { get; set; }
        public string CLIENTE { get; set; }
        public string TIPOCARGA { get; set; }
        public string ORIGEN { get; set; }
        public string DESTINO { get; set; }
        public string FECHA_ENTREGA { get; set; }
        public string HORA_ENTREGA { get; set; }
        public double DISTANCIA { get; set; }

        public GridListViewCotizaciones()
        {
            COTIZACION = "";
            CLIENTE = "";
            TIPOCARGA = "";
            ORIGEN = "";
            DESTINO = "";
            FECHA_ENTREGA = "";
            HORA_ENTREGA = "";
            DISTANCIA = 0;
        }
    }
}
