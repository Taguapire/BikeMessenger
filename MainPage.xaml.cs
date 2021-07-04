using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BikeMessenger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {

            InitializeComponent();
        }

        private void BM_NavPag_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
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

        }

        private void BM_NavPag_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string BM_ItemContent = "";

            try
            {
                BM_ItemContent = (string) args.InvokedItem;

                if (BM_ItemContent != "" || BM_ItemContent != null)
                {
                    switch (BM_ItemContent)
                    {
                        case "Inicio":
                            CuadroDeContenido.Navigate(typeof(PageInicio));
                            break;

                        case "Empresa":
                            CuadroDeContenido.Navigate(typeof(PageEmpresa));
                            break;

                        case "Personal":
                            CuadroDeContenido.Navigate(typeof(PagePersonal));
                            break;

                        case "Recursos":
                            CuadroDeContenido.Navigate(typeof(PageRecursos));
                            break;
                        case "Clientes":
                            CuadroDeContenido.Navigate(typeof(PageClientes));
                            break;
                        case "Servicios":
                            CuadroDeContenido.Navigate(typeof(PageServicios));
                            break;
                        case "Ajustes":
                            CuadroDeContenido.Navigate(typeof(PageAjustes));
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
    }
}