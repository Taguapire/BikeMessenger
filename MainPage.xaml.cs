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
            CuadroDeContenido.Navigate(typeof(PageInicio));
        }

        private void BM_NavPag_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {

        }

        private void BM_NavPag_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {

        }
    }
}
