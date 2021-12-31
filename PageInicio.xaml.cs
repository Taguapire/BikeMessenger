using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            this.InitializeComponent();
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
            //listViewServicios.Items.Add("Hola");
            //listViewServicios.Items.Add("Luis");
            //listViewServicios.Items.Add("Vasquez");
        }
    }

    internal class GridListViewServicios
    {
        public string NROENVIO;
        public string GUIADESPACHO;
        public string FECHA;
        public string HORA;
        public string NOMBRE;
        public string APELLIDOS;
        public string ENTREGA;
        public string RECEPCION;

        public GridListViewServicios()
        {
            NROENVIO = "";
            GUIADESPACHO = "";
            FECHA = "";
            HORA = "";
            NOMBRE = "";
            APELLIDOS = "";
            ENTREGA = "";
            RECEPCION = "";
        }
    }
}

