using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BikeMessenger
{
    internal class TransferVar
    {
        // Valores de Base de Datos

        // Valores de Empresa
        public string E_PENTALPHA { get; set; }
        public string E_RUTID { get; set; }
        public string E_DIGVER { get; set; }

        // Valores de Personal
        public string P_PENTALPHA { get; set; }
        public string P_RUTID { get; set; }
        public string P_DIGVER { get; set; }

        // Valores de Recursos
        public string R_PENTALPHA { get; set; }
        public string R_RUTID { get; set; }
        public string R_DIGVER { get; set; }
        public string R_PAT_SER { get; set; }

        // Valores de Clientes
        public string C_PENTALPHA { get; set; }
        public string C_RUTID { get; set; }
        public string C_DIGVER { get; set; }

        // Valores de SERVICIOS
        public string X_PENTALPHA { get; set; }
        public string X_NROENVIO { get; set; }

        // Valores Directorio
        public string Directorio { get; set; }
        public string DirectorioRespaldos { get; set; }

        public string SincronizarRemoto { get; set; }

        public string SincronizarPropio { get; set; }

        public string PantallaAnterior { get; set; }
        // public string TipoDeInforme;

        public TransferVar()
        {
            CrearDirectorio(ApplicationData.Current.LocalFolder.Path);
            CrearDirectorioRespaldo(ApplicationData.Current.LocalFolder.Path);
 
            LeerDirectorio();

            if (!VerificarSincronizarRemoto())
            {
                CrearSincronizarRemoto("S");
            }

            LeerSincronizarRemoto();

            if (!VerificarSincronizarPropio())
            {
                CrearSincronizarPropio("N");
            }

            LeerSincronizarPropio();

        }

        private async Task AvisoDeError()
        {
            ContentDialog AbrirBasedeDatos = new ContentDialog
            {
                Title = "Error en Apertura de Base de Datos",
                Content = "Error en la apertura de la Base de Datos",
                CloseButtonText = "Salir"
            };
            _ = await AbrirBasedeDatos.ShowAsync();
            Application.Current.Exit();
            return;
        }

        public void CrearDirectorio(string pDirectorio)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["PENTALPHA"] = pDirectorio;
            return;
        }

        public void CrearDirectorioRespaldo(string pDirectorioRespaldos)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

            localSettings.Values["PENTALPHA_RESPALDOS"] = pDirectorioRespaldos;
            return;
        }

        public void LeerDirectorio()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            Directorio = (string)localSettings.Values["PENTALPHA"];
            DirectorioRespaldos = (string)localSettings.Values["PENTALPHA_RESPALDOS"];
            return;
        }

        public bool VerificarSincronizarRemoto()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            SincronizarRemoto = (string)localSettings.Values["PENTALPHA_REMOTO"];

            return SincronizarRemoto != null && SincronizarRemoto != "";
        }

        public bool VerificarSincronizarPropio()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            SincronizarPropio = (string)localSettings.Values["PENTALPHA_PROPIO"];

            return SincronizarPropio != null && SincronizarPropio != "";
        }

        public void CrearSincronizarRemoto(string pSincronizarRemoto)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["PENTALPHA_REMOTO"] = pSincronizarRemoto;
            return;
        }

        public void CrearSincronizarPropio(string pSincronizarPropio)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["PENTALPHA_PROPIO"] = pSincronizarPropio;
            return;
        }

        public void LeerSincronizarRemoto()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            SincronizarRemoto = (string)localSettings.Values["PENTALPHA_REMOTO"];
            return;
        }

        public void LeerSincronizarPropio()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            SincronizarPropio = (string)localSettings.Values["PENTALPHA_PROPIO"];
            return;
        }

        public bool SincronizarWebRemoto()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            SincronizarRemoto = (string)localSettings.Values["PENTALPHA_REMOTO"];

            return SincronizarRemoto == "S";
        }

        public bool SincronizarWebPropio()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            SincronizarPropio = (string)localSettings.Values["PENTALPHA_PROPIO"];

            return SincronizarPropio == "S";
        }

    }
}
