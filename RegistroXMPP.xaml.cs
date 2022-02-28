using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    public sealed partial class RegistroXMPP : ContentDialog
    {
        public PentalphaJson localPentalphaJson;

        public RegistroXMPP()
        {
            this.InitializeComponent();
            localPentalphaJson = new PentalphaJson();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            localPentalphaJson.PENTALPHA = TBoxPentalphaId.Text;
            localPentalphaJson.EMPRESA = TBoxEmpresa.Text;
            localPentalphaJson.RUTID = TBoxRutId.Text;
            localPentalphaJson.DIGVER = TBoxDigVer.Text;
            localPentalphaJson.USUARIO = TBoxUsuario.Text;
            localPentalphaJson.CLAVE = TBoxClave.Text;
            localPentalphaJson.REMOTO = TBoxRemoto.Text;
            localPentalphaJson.PROPIO = TBoxPropio.Text;
            localPentalphaJson.LICENCIA = TBoxLicencia.Text;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        public void MemoriaPantalla()
        {
            TBoxPentalphaId.Text = localPentalphaJson.PENTALPHA ?? "";
            TBoxEmpresa.Text = localPentalphaJson.EMPRESA ?? "";
            TBoxRutId.Text = localPentalphaJson.RUTID ?? "";
            TBoxDigVer.Text = localPentalphaJson.DIGVER ?? "";
            TBoxUsuario.Text = localPentalphaJson.USUARIO ?? "";
            TBoxClave.Text = localPentalphaJson.CLAVE ?? "";
            TBoxRemoto.Text = localPentalphaJson.REMOTO ?? "";
            TBoxPropio.Text = localPentalphaJson.PROPIO ?? "";
            TBoxLicencia.Text = localPentalphaJson.LICENCIA ?? "";
        }
    }
}
