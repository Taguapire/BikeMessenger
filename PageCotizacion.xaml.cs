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
                    FECHA_ENTREGA = results[i].FECHAENTREGA,
                    HORA_ENTREGA = results[i].HORAENTREGA,
                    CLIENTE = results[i].NOMBRE,
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
                case "FECHA_ENTREGA":
                    e.Column.Header = "Fecha de Entrega";
                    break;
                case "HORA_ENTREGA":
                    e.Column.Header = "Hora de Entrega";
                    break;
                case "CLIENTE":
                    e.Column.Header = "Cliente";
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
        }
    }

    internal class GridListViewCotizaciones
    {
        public string COTIZACION { get; set; }
        public string FECHA_ENTREGA { get; set; }
        public string HORA_ENTREGA { get; set; }
        public string CLIENTE { get; set; }
        public double DISTANCIA { get; set; }

        public GridListViewCotizaciones()
        {
            COTIZACION = "";
            FECHA_ENTREGA = "";
            HORA_ENTREGA = "";
            CLIENTE = "";
            DISTANCIA = 0;
        }
    }
}
