using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BikeMessenger
{
    public sealed partial class RegistroXMPP : ContentDialog
    {
        PentalphaJson localPentalphaJson = new PentalphaJson();

        public string lPENTALPHA { get; set; }
        public string lEMPRESA { get; set; }
        public string lPAIS { get; set; }
        public string lUSUARIO { get; set; }
        public string lCLAVE { get; set; }
        public string lRUTID { get; set; }
        public string lDIGVER { get; set; }
        public string lREMOTO { get; set; }
        public string lPROPIO { get; set; }
        public string lLICENCIA { get; set; }

        public RegistroXMPP()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        public void MemoriaPantalla()
        {
            localPentalphaJson.PENTALPHA = lPENTALPHA;
            localPentalphaJson.EMPRESA = lEMPRESA;
            localPentalphaJson.RUTID = lRUTID;
            localPentalphaJson.DIGVER = lDIGVER;
            localPentalphaJson.USUARIO = lUSUARIO;
            localPentalphaJson.CLAVE = lCLAVE;
            localPentalphaJson.REMOTO = lREMOTO;
            localPentalphaJson.PROPIO = lPROPIO;
            localPentalphaJson.LICENCIA = lLICENCIA;

            TBoxPentalphaId.Text = localPentalphaJson.PENTALPHA;
            TBoxEmpresa.Text = localPentalphaJson.EMPRESA;
            TBoxRutId.Text = localPentalphaJson.RUTID;
            TBoxDigVer.Text = localPentalphaJson.DIGVER;
            TBoxUsuario.Text = localPentalphaJson.USUARIO;
            TBoxClave.Text = localPentalphaJson.CLAVE;
            TBoxRemoto.Text = localPentalphaJson.REMOTO;
            TBoxPropio.Text = localPentalphaJson.PROPIO;
            TBoxLicencia.Text = localPentalphaJson.LICENCIA;
        }
    }
}
