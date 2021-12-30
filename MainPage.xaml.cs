using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SQLite;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string BM_Ultimo_Item = "";
        private TransferVar LvrTransferVar = new TransferVar();
        public MainPage()
        {
            InitializeComponent();
            InicializarBasesDeDatos();
        }

        private void BM_NavPag_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LvrTransferVar.LeerValoresDeAjustes();
            // set the initial SelectedItem
            foreach (NavigationViewItemBase item in BM_NavPag.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "Inicio_Page")
                {
                    BM_NavPag.SelectedItem = item;
                    break;
                }
            }
            _ = CuadroDeContenido.Navigate(typeof(PageInicio));
        }

        private void BM_NavPag_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            LvrTransferVar.LeerValoresDeAjustes();
        }

        private void BM_NavPag_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string BM_ItemContent = "";
            LvrTransferVar.LeerValoresDeAjustes();

            try
            {
                BM_ItemContent = (string) args.InvokedItem;

                if (BM_ItemContent != "" || BM_ItemContent != null)
                {
                    switch (BM_ItemContent)
                    {
                        case "Inicio":
                            if (BM_Ultimo_Item != BM_ItemContent)
                            {
                                BM_Ultimo_Item = BM_ItemContent;
                                _ = CuadroDeContenido.Navigate(typeof(PageInicio));
                            }
                            break;

                        case "Empresa":
                            if (LvrTransferVar.ESTADOPARAMETROS == "NADA")
                            {
                                break;
                            }

                            if (BM_Ultimo_Item != BM_ItemContent)
                            {
                                BM_Ultimo_Item = BM_ItemContent;
                                BM_NavPag.IsBackEnabled = true;
                                _ = CuadroDeContenido.Navigate(typeof(PageEmpresa));
                            }
                            break;

                        case "Personal":
                            if (LvrTransferVar.ESTADOPARAMETROS == "NADA")
                            {
                                break;
                            }

                            if (BM_Ultimo_Item != BM_ItemContent)
                            {
                                BM_Ultimo_Item = BM_ItemContent;
                                BM_NavPag.IsBackEnabled = true;
                                _ = CuadroDeContenido.Navigate(typeof(PagePersonal));
                            }
                            break;

                        case "Recursos":
                            if (LvrTransferVar.ESTADOPARAMETROS == "NADA")
                            {
                                break;
                            }

                            if (BM_Ultimo_Item != BM_ItemContent)
                            {
                                BM_Ultimo_Item = BM_ItemContent;
                                BM_NavPag.IsBackEnabled = true;
                                _ = CuadroDeContenido.Navigate(typeof(PageRecursos));
                            }
                            break;
                        case "Clientes":
                            if (LvrTransferVar.ESTADOPARAMETROS == "NADA")
                            {
                                break;
                            }

                            if (BM_Ultimo_Item != BM_ItemContent)
                            {
                                BM_Ultimo_Item = BM_ItemContent;
                                BM_NavPag.IsBackEnabled = true;
                                _ = CuadroDeContenido.Navigate(typeof(PageClientes));
                            }
                            break;
                        case "Servicios":
                            if (LvrTransferVar.ESTADOPARAMETROS == "NADA")
                            {
                                break;
                            }

                            if (BM_Ultimo_Item != BM_ItemContent)
                            {
                                BM_Ultimo_Item = BM_ItemContent;
                                BM_NavPag.IsBackEnabled = true;
                                _ = CuadroDeContenido.Navigate(typeof(PageServicios));
                            }
                            break;
                        case "Ajustes":
                            if (BM_Ultimo_Item != BM_ItemContent)
                            {
                                BM_Ultimo_Item = BM_ItemContent;
                                BM_NavPag.IsBackEnabled = true;
                                //    _ = CuadroDeContenido.Navigate(typeof(PageAjustes));
                            }
                            break;
                        case "Salir":
                            Application.Current.Exit();
                            break;
                    }
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.InnerException.Message);
                Console.WriteLine(ee.Message);
            }
        }
        
        private void BM_NavPag_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (CuadroDeContenido.CanGoBack)
            {
                CuadroDeContenido.GoBack();
            }
            else
            {
                BM_NavPag.IsBackEnabled = false;
            }
        }

        private void InicializarBasesDeDatos()
        {
            try
            {
                SQLiteConnection BM_ConexionLite = new SQLiteConnection(LvrTransferVar.DIRECTORIO_BASE_LOCAL + "\\BikeMessenger.db");

                _ = BM_ConexionLite.CreateTable<TbBikeMessengerEmpresa>();
                _ = BM_ConexionLite.CreateTable<TbBikeMessengerPersonal>();
                _ = BM_ConexionLite.CreateTable<TbBikeMessengerRecurso>();
                _ = BM_ConexionLite.CreateTable<TbBikeMessengerCliente>();
                _ = BM_ConexionLite.CreateTable<TbBikeMessengerServicio>();
                _ = BM_ConexionLite.CreateTable<TbBikeMessengerComuna>();
                _ = BM_ConexionLite.CreateTable<TbBikeMessengerCiudad>();
                _ = BM_ConexionLite.CreateTable<TbBikeMessengerRegion>();
                _ = BM_ConexionLite.CreateTable<TbBikeMessengerPais>();

                BM_ConexionLite.Close();
                BM_ConexionLite.Dispose();
                BM_ConexionLite = null;
            }
            catch (SQLiteException Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
            }
        }
    }
}