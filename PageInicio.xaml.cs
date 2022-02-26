using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageInicio : Page
    {
        private TransferVar LvrTransferVar = new TransferVar();
        private bool ContinuarSiNo;

        public PageInicio()
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

            List<GridListViewServicios> GridServiciosLista = new List<GridListViewServicios>();

            string CompletoNombreBD = LvrTransferVar.DIRECTORIO_BASE_LOCAL + "\\BikeMessenger.db";

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            if (LvrTransferVar.ESTADOPARAMETROS == "NADA")
            {
                return;
            }

            List<TbVistaServicioCliMen> results = BM_ConexionLite.Query<TbVistaServicioCliMen>("select * from Vista_Servicio_CliMen");

            for (int i = 0; i < results.Count; i++)
            {
                GridServiciosLista.Add(new GridListViewServicios
                {
                    NRO_ENVIO = results[i].NROENVIO,
                    GUIA_DESPACHO = results[i].GUIADESPACHO,
                    FECHA_ENTREGA = results[i].FECHAENTREGA,
                    HORA_ENTREGA = results[i].HORAENTREGA,
                    CLIENTE = results[i].NOMBRE,
                    MENSAJERO = results[i].APELLIDOS + "," + results[i].NOMBRES,
                    ENTREGA = results[i].ENTREGA,
                    RECEPCION = results[i].RECEPCION,
                    DISTANCIA = results[i].DISTANCIA
                });
            }

            DGViewServicios.ItemsSource = GridServiciosLista;

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();
        }

        private void GeneracionDeColumnas(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header.ToString())
            {
                case "NRO_ENVIO":
                    e.Column.Header = "Envio";
                    break;
                case "GUIA_DESPACHO":
                    e.Column.Header = "Guia";
                    break;
                case "FECHA_ENTREGA":
                    e.Column.Header = "Fecha Entrega";
                    break;
                case "HORA_ENTREGA":
                    e.Column.Header = "Hora Entrega";
                    break;
                case "CLIENTE":
                    e.Column.Header = "Cliente";
                    break;
                case "MENSAJERO":
                    e.Column.Header = "Mensajero";
                    break;
                case "ENTREGA":
                    e.Column.Header = "Entregado";
                    break;
                case "RECEPCION":
                    e.Column.Header = "Recibido";
                    break;
                case "DISTANCIA":
                    e.Column.Header = "Distancia";
                    break;
                default:
                    break;
            }
        }

        private void BtnSalirVisorDeServicios(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void RevisarDetalleCotizacionServicio(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {

        }
    }

    internal class GridListViewServicios
    {
        public string NRO_ENVIO { get; set; }
        public string GUIA_DESPACHO { get; set; }
        public string FECHA_ENTREGA { get; set; }
        public string HORA_ENTREGA { get; set; }
        public string CLIENTE { get; set; }
        public string MENSAJERO { get; set; }
        public string ENTREGA { get; set; }
        public string RECEPCION { get; set; }
        public double DISTANCIA { get; set; }

        public GridListViewServicios()
        {
            NRO_ENVIO = "";
            GUIA_DESPACHO = "";
            FECHA_ENTREGA = "";
            HORA_ENTREGA = "";
            CLIENTE = "";
            MENSAJERO = "";
            ENTREGA = "";
            RECEPCION = "";
            DISTANCIA = 0;
        }
    }
}
