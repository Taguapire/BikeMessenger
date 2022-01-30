using Windows.Storage;
using SQLite;

namespace BikeMessenger
{
    internal class TransferVar
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        // Valores de Base de Datos

        public string ESTADOPARAMETROS { get; set; }        // "NADA/SQLITE/SQLSERVER/WEBPENTALPHA/WEBPROPIO"
        //--------------------------------------------------------------------
        public string DIRECTORIO_RESPALDOS { get; set; }    // "ApplicationData.Current.LocalFolder.Path";
        public string DIRECTORIO_BASE_LOCAL { get; set; }   // "ApplicationData.Current.LocalFolder.Path";
        //--------------------------------------------------------------------
        public string PENTALPHA_ID { get; set; }            // "N";
        //--------------------------------------------------------------------
        public string BASEDEDATOSLOCAL { get; set; }        // "N";
        //--------------------------------------------------------------------
                
        // Valores de Empresa
        public string EMP_PENTALPHA { get; set; }
        public string EMP_RUTID { get; set; }
        public string EMCLI_DIGVER { get; set; }

        // Valores de Personal
        public string PER_PENTALPHA { get; set; }
        public string PER_RUTID { get; set; }
        public string PER_DIGVER { get; set; }

        // Valores de Recursos
        public string REC_PENTALPHA { get; set; }
        public string REC_RUTID { get; set; }
        public string REC_DIGVER { get; set; }
        public string REC_PAT_SER { get; set; }

        // Valores de Clientes
        public string CLI_PENTALPHA { get; set; }
        public string CLI_RUTID { get; set; }
        public string CLI_DIGVER { get; set; }

        // Valores de SERVICIOS
        public string SER_PENTALPHA { get; set; }
        public string SER_NROENVIO { get; set; }

        public string PantallaAnterior { get; set; }

        public TransferVar()
        {
            localSettings.Values["PENTALPHA_ID"] = "EF00F8E2DA87E88D591BC033402208CA2C08A849298019366E23AB370A711BA6";
            // Se Intenta Leer Estructura de Pentalpha
            ESTADOPARAMETROS = "";
            ESTADOPARAMETROS = (string)localSettings.Values["ESTADOPARAMETROS"];
            if (ESTADOPARAMETROS == null || ESTADOPARAMETROS == "" || ESTADOPARAMETROS == "NADA")
            {
                ESTADOPARAMETROS = "NADA";
                DIRECTORIO_BASE_LOCAL = ApplicationData.Current.LocalFolder.Path;
                DIRECTORIO_RESPALDOS = ApplicationData.Current.LocalFolder.Path;
                PENTALPHA_ID = "N";                                 // "N";
                BASEDEDATOSLOCAL = "N";                             // "N";

                // Valores de Empresa
                EMP_PENTALPHA = "";
                EMP_RUTID = "";
                EMCLI_DIGVER = "";

                // Valores de Personal
                PER_PENTALPHA = "";
                PER_RUTID = "";
                PER_DIGVER = "";

                // Valores de Recursos
                REC_PENTALPHA = "";
                REC_RUTID = "";
                REC_DIGVER = "";
                REC_PAT_SER = "";

                // Valores de Clientes
                CLI_PENTALPHA = "";
                CLI_RUTID = "";
                CLI_DIGVER = "";

                // Valores de SERVICIOS
                SER_PENTALPHA = "";
                SER_NROENVIO = "";

                localSettings.Values["ESTADOPARAMETROS"] = ESTADOPARAMETROS;
                localSettings.Values["DIRECTORIO_RESPALDOS"] = DIRECTORIO_RESPALDOS;
                localSettings.Values["DIRECTORIO_BASE_LOCAL"] = DIRECTORIO_BASE_LOCAL;
                localSettings.Values["PENTALPHA_ID"] = PENTALPHA_ID;
                localSettings.Values["BASEDEDATOSLOCAL"] = BASEDEDATOSLOCAL;
                
                // Valores de Empresa
                localSettings.Values["EMP_PENTALPHA"] = EMP_PENTALPHA;
                localSettings.Values["EMP_RUTID"] = EMP_RUTID;
                localSettings.Values["EMCLI_DIGVER"] = EMCLI_DIGVER;

                // Valores de Personal
                localSettings.Values["PER_PENTALPHA"] = PER_PENTALPHA;
                localSettings.Values["PER_RUTID"] = PER_RUTID;
                localSettings.Values["PER_DIGVER"] = PER_DIGVER;

                // Valores de Recursos
                localSettings.Values["REC_PENTALPHA"] = REC_PENTALPHA;
                localSettings.Values["REC_RUTID"] = REC_RUTID;
                localSettings.Values["REC_DIGVER"] = REC_DIGVER;
                localSettings.Values["REC_PAT_SER"] = REC_PAT_SER;

                // Valores de Clientes
                localSettings.Values["CLI_PENTALPHA"] = CLI_PENTALPHA;
                localSettings.Values["CLI_RUTID"] = CLI_RUTID;
                localSettings.Values["CLI_DIGVER"] = CLI_DIGVER;

                // Valores de SERVICIOS
                localSettings.Values["SER_PENTALPHA"] = SER_PENTALPHA;
                localSettings.Values["SER_NROENVIO"] = SER_NROENVIO;
            }
            else
            {
                ESTADOPARAMETROS = (string)localSettings.Values["ESTADOPARAMETROS"];
                DIRECTORIO_RESPALDOS = (string)localSettings.Values["DIRECTORIO_RESPALDOS"];
                DIRECTORIO_BASE_LOCAL = ApplicationData.Current.LocalFolder.Path;
                PENTALPHA_ID = (string)localSettings.Values["PENTALPHA_ID"];
                BASEDEDATOSLOCAL = (string)localSettings.Values["BASEDEDATOSLOCAL"];

                // Valores de Empresa
                EMP_PENTALPHA = (string)localSettings.Values["EMP_PENTALPHA"];
                EMP_RUTID = (string)localSettings.Values["EMP_RUTID"];
                EMCLI_DIGVER = (string)localSettings.Values["EMCLI_DIGVER"];

                // Valores de Personal
                PER_PENTALPHA = (string)localSettings.Values["PER_PENTALPHA"];
                PER_RUTID = (string)localSettings.Values["PER_RUTID"];
                PER_DIGVER = (string)localSettings.Values["PER_DIGVER"];

                // Valores de Recursos
                REC_PENTALPHA = (string)localSettings.Values["REC_PENTALPHA"];
                REC_RUTID = (string)localSettings.Values["REC_RUTID"];
                REC_DIGVER = (string)localSettings.Values["REC_DIGVER"];
                REC_PAT_SER = (string)localSettings.Values["REC_PAT_SER"];

                // Valores de Clientes
                CLI_PENTALPHA = (string)localSettings.Values["CLI_PENTALPHA"];
                CLI_RUTID = (string)localSettings.Values["CLI_RUTID"];
                CLI_DIGVER = (string)localSettings.Values["CLI_DIGVER"];

                // Valores de SERVICIOS
                SER_PENTALPHA = (string)localSettings.Values["SER_PENTALPHA"];
                SER_NROENVIO = (string)localSettings.Values["SER_NROENVIO"];
            }
        }

        public void EscribirValoresDeAjustes()
        {
            localSettings.Values["ESTADOPARAMETROS"] = ESTADOPARAMETROS;

            localSettings.Values["DIRECTORIO_RESPALDOS"] = DIRECTORIO_RESPALDOS;
            localSettings.Values["DIRECTORIO_BASE_LOCAL"] = DIRECTORIO_BASE_LOCAL;
            
            localSettings.Values["PENTALPHA_ID"] = PENTALPHA_ID;
            
            localSettings.Values["BASEDEDATOSLOCAL"] = BASEDEDATOSLOCAL;
            
            // Valores de Empresa
            localSettings.Values["EMP_PENTALPHA"] = EMP_PENTALPHA;
            localSettings.Values["EMP_RUTID"] = EMP_RUTID;
            localSettings.Values["EMCLI_DIGVER"] = EMCLI_DIGVER;

            // Valores de Personal
            localSettings.Values["PER_PENTALPHA"] = PER_PENTALPHA;
            localSettings.Values["PER_RUTID"] = PER_RUTID;
            localSettings.Values["PER_DIGVER"] = PER_DIGVER;

            // Valores de Recursos
            localSettings.Values["REC_PENTALPHA"] = REC_PENTALPHA;
            localSettings.Values["REC_RUTID"] = REC_RUTID;
            localSettings.Values["REC_DIGVER"] = REC_DIGVER;
            localSettings.Values["REC_PAT_SER"] = REC_PAT_SER;

            // Valores de Clientes
            localSettings.Values["CLI_PENTALPHA"] = CLI_PENTALPHA;
            localSettings.Values["CLI_RUTID"] = CLI_RUTID;
            localSettings.Values["CLI_DIGVER"] = CLI_DIGVER;

            // Valores de SERVICIOS
            localSettings.Values["SER_PENTALPHA"] = SER_PENTALPHA;
            localSettings.Values["SER_NROENVIO"] = SER_NROENVIO;
        }

        public void LeerValoresDeAjustes()
        {
            ESTADOPARAMETROS = (string)localSettings.Values["ESTADOPARAMETROS"];
            DIRECTORIO_RESPALDOS = (string)localSettings.Values["DIRECTORIO_RESPALDOS"];
            DIRECTORIO_BASE_LOCAL = (string)localSettings.Values["DIRECTORIO_BASE_LOCAL"];
            PENTALPHA_ID = (string)localSettings.Values["PENTALPHA_ID"];
            BASEDEDATOSLOCAL = (string)localSettings.Values["BASEDEDATOSLOCAL"];
            
            // Valores de Empresa
            EMP_PENTALPHA = (string)localSettings.Values["EMP_PENTALPHA"];
            EMP_RUTID = (string)localSettings.Values["EMP_RUTID"];
            EMCLI_DIGVER = (string)localSettings.Values["EMCLI_DIGVER"];

            // Valores de Personal
            PER_PENTALPHA = (string)localSettings.Values["PER_PENTALPHA"];
            PER_RUTID = (string)localSettings.Values["PER_RUTID"];
            PER_DIGVER = (string)localSettings.Values["PER_DIGVER"];

            // Valores de Recursos
            REC_PENTALPHA = (string)localSettings.Values["REC_PENTALPHA"];
            REC_RUTID = (string)localSettings.Values["REC_RUTID"];
            REC_DIGVER = (string)localSettings.Values["REC_DIGVER"];
            REC_PAT_SER = (string)localSettings.Values["REC_PAT_SER"];

            // Valores de Clientes
            CLI_PENTALPHA = (string)localSettings.Values["CLI_PENTALPHA"];
            CLI_RUTID = (string)localSettings.Values["CLI_RUTID"];
            CLI_DIGVER = (string)localSettings.Values["CLI_DIGVER"];

            // Valores de SERVICIOS
            SER_PENTALPHA = (string)localSettings.Values["SER_PENTALPHA"];
            SER_NROENVIO = (string)localSettings.Values["SER_NROENVIO"];
        }

        public void ActualizarPentalphaId()
        {
            // Valores de Empresa
            EMP_PENTALPHA = PENTALPHA_ID;

            // Valores de Personal
            PER_PENTALPHA = PENTALPHA_ID;

            // Valores de Recursos
            REC_PENTALPHA = PENTALPHA_ID;

            // Valores de Clientes
            CLI_PENTALPHA = PENTALPHA_ID;

            // Valores de SERVICIOS
            SER_PENTALPHA = PENTALPHA_ID;
        }

        public bool SincronizarBaseLocal()
        {
            return (ESTADOPARAMETROS == "S") && (BASEDEDATOSLOCAL == "S");
        }
    }
}
