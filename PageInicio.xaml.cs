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
    public sealed partial class PageInicio : Page
    {
        private TransferVar LvrTransferVar = new TransferVar();
        private bool ContinuarSiNo;

        public PageInicio()
        {
            this.InitializeComponent();
            ContinuarSiNo = false;
            if (LvrTransferVar.ESTADOPARAMETROS == "NADA")
            {
                _ = AvisoIniciarParemetrosDialogAsync();

                if (!ContinuarSiNo)
                {
                    return;
                }
            }
        }

        private async Task AvisoIniciarParemetrosDialogAsync()
        {
            ContentDialog AvisoConfirmacionPersonalDialog = new ContentDialog
            {
                Title = "Iniciar en Ajuste",
                Content = "Usted aun no a definido Base de Datos SQLServer o Bases de Datos Remotas. El default sera Base de Datos Remota de Pentalpha EIRL.",
                PrimaryButtonText = "Aceptar Usar Pentalpha EIRL",
                CloseButtonText = "Cancelar y definir valores en Ajustes"
            };

            ContentDialogResult result = await AvisoConfirmacionPersonalDialog.ShowAsync();

            ContinuarSiNo = result == ContentDialogResult.Primary;
        }
    }
}
