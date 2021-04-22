using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Data.SQLite;
using Windows.UI.Xaml.Media.Animation;

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
        public PageServicios()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
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
                LvrTransferVar = (TransferVar) e.Parameter;
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

        private void BtnAgregarServicios(object sender, RoutedEventArgs e)
        {
            LlenarDbConPantalla();
            BM_Database_Servicios.Bm_Servicios_Agregar();
        }

        private void LlenarPantallaConDb()
        {
            try
            {
                textBoxNroDeEnvio.Text = BM_Database_Servicios.BK_NROENVIO;
                textBoxGuiaDeDespacho.Text = BM_Database_Servicios.BK_GUIADESPACHO;
                textBoxRutID.Text = BM_Database_Servicios.BK_CLIENTERUT;
                textBoxDigitoVerificador.Text = BM_Database_Servicios.BK_CLIENTEDIGVER;
                textBoxOrigenDomicilio1.Text = BM_Database_Servicios.BK_ODOMICILIO1;
                textBoxOrigenDomicilio2.Text = BM_Database_Servicios.BK_ODOMICILIO2;
                textBoxOrigenNumero.Text = BM_Database_Servicios.BK_ONUMERO;
                textBoxOrigenPiso.Text = BM_Database_Servicios.BK_OPISO;
                textBoxOrigenOficina.Text = BM_Database_Servicios.BK_OOFICINA;
                // BM_Database_Servicios.BK_OCOORDENADAS = "*";
                textBoxDestinoDomicilio1.Text = BM_Database_Servicios.BK_DDOMICILIO1;
                textBoxDestinoDomicilio2.Text = BM_Database_Servicios.BK_DDOMICILIO2;
                textBoxDestinoNumero.Text = BM_Database_Servicios.BK_DNUMERO;
                textBoxDestinoPiso.Text = BM_Database_Servicios.BK_DPISO;
                textBoxDestinoOficina.Text = BM_Database_Servicios.BK_DOFICINA;
                // BM_Database_Servicios.BK_DCOORDENADAS = "*";
                textBoxDescripcion.Text = BM_Database_Servicios.BK_DESCRIPCION;
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

                if (BM_Database_Servicios.Bm_E_Pais_EjecutarSelect())
                {
                    while (BM_Database_Servicios.Bm_E_Pais_Buscar())
                    {
                        comboBoxOrigenPais.Items.Add(BM_Database_Servicios.BK_E_PAIS);
                    }
                }

                comboBoxOrigenPais.Items.Add(BM_Database_Servicios.BK_OPAIS);
                comboBoxOrigenPais.SelectedValue = BM_Database_Servicios.BK_OPAIS;

                comboBoxOrigenEstado.Items.Add(BM_Database_Servicios.BK_OESTADO);
                comboBoxOrigenEstado.SelectedValue = BM_Database_Servicios.BK_OESTADO;

                comboBoxOrigenComuna.Items.Add(BM_Database_Servicios.BK_OCOMUNA);
                comboBoxOrigenComuna.SelectedValue = BM_Database_Servicios.BK_OCOMUNA;

                comboBoxOrigenCiudad.Items.Add(BM_Database_Servicios.BK_OCIUDAD);
                comboBoxOrigenCiudad.SelectedValue = BM_Database_Servicios.BK_OCIUDAD;

                if (BM_Database_Servicios.Bm_E_Pais_EjecutarSelect())
                {
                    while (BM_Database_Servicios.Bm_E_Pais_Buscar())
                    {
                        comboBoxDestinoPais.Items.Add(BM_Database_Servicios.BK_E_PAIS);
                    }
                }

                comboBoxDestinoPais.Items.Add(BM_Database_Servicios.BK_DPAIS);
                comboBoxDestinoPais.SelectedValue = BM_Database_Servicios.BK_DPAIS;


                comboBoxDestinoEstado.Items.Add(BM_Database_Servicios.BK_DESTADO);
                comboBoxDestinoEstado.SelectedValue = BM_Database_Servicios.BK_DESTADO;

                comboBoxDestinoComuna.Items.Add(BM_Database_Servicios.BK_DCOMUNA);
                comboBoxDestinoComuna.SelectedValue = BM_Database_Servicios.BK_DCOMUNA;

                comboBoxDestinoCiudad.Items.Add(BM_Database_Servicios.BK_DCIUDAD);
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
            BM_Database_Servicios.BK_FECHA = controlFecha.Date.ToString();
            BM_Database_Servicios.BK_HORA = controlHora.Time.ToString();
            BM_Database_Servicios.BK_CLIENTERUT = textBoxRutID.Text;
            BM_Database_Servicios.BK_CLIENTEDIGVER = textBoxDigitoVerificador.Text;
            BM_Database_Servicios.BK_ODOMICILIO1 = textBoxOrigenDomicilio1.Text;
            BM_Database_Servicios.BK_ODOMICILIO2 = textBoxOrigenDomicilio2.Text;
            BM_Database_Servicios.BK_ONUMERO = textBoxOrigenNumero.Text;
            BM_Database_Servicios.BK_OPISO = textBoxOrigenPiso.Text;
            BM_Database_Servicios.BK_OOFICINA = textBoxOrigenOficina.Text;
            BM_Database_Servicios.BK_OCIUDAD = comboBoxOrigenCiudad.Text;
            BM_Database_Servicios.BK_OCOMUNA = comboBoxOrigenComuna.Text;
            BM_Database_Servicios.BK_OESTADO = comboBoxOrigenEstado.Text;
            BM_Database_Servicios.BK_OPAIS = comboBoxOrigenPais.Text;
            BM_Database_Servicios.BK_OCOORDENADAS = "*";
            BM_Database_Servicios.BK_DDOMICILIO1 = textBoxDestinoDomicilio1.Text;
            BM_Database_Servicios.BK_DDOMICILIO2 = textBoxDestinoDomicilio2.Text;
            BM_Database_Servicios.BK_DNUMERO = textBoxDestinoNumero.Text;
            BM_Database_Servicios.BK_DPISO = textBoxDestinoPiso.Text;
            BM_Database_Servicios.BK_DOFICINA = textBoxDestinoOficina.Text;
            BM_Database_Servicios.BK_DCIUDAD = comboBoxDestinoCiudad.Text;
            BM_Database_Servicios.BK_DCOMUNA = comboBoxDestinoComuna.Text;
            BM_Database_Servicios.BK_DESTADO = comboBoxDestinoEstado.Text;
            BM_Database_Servicios.BK_DPAIS = comboBoxDestinoPais.Text;
            BM_Database_Servicios.BK_DCOORDENADAS = "*";
            BM_Database_Servicios.BK_DESCRIPCION = textBoxDescripcion.Text;
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
    }
}
