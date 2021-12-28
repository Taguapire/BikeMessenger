using Windows.Storage;
using SQLite;

namespace BikeMessenger
{
    internal class TransferVar
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        // Valores de Base de Datos

        public string ESTADOPARAMETROS { get; set; }        // "NADA/SQLITE/SQLSERVER/WEBPENTALPHA/WEBPROPIO"
        public string SINCRONIZACIONWEBPENTALPHA { get; set; }
        public string SINCRONIZACIONWEBPROPIO { get; set; }
        //--------------------------------------------------------------------
        public string DIRECTORIO_RESPALDOS { get; set; }      // "ApplicationData.Current.LocalFolder.Path";
        public string DIRECTORIO_BASE_LOCAL { get; set; }   // "ApplicationData.Current.LocalFolder.Path";
        public string DIRECTORIO_USB_MEMORIA { get; set; }  // "N";
        //--------------------------------------------------------------------
        public string PENTALPHA_ID { get; set; }            // "N";
        //--------------------------------------------------------------------
        public string BASEDEDATOSLOCAL { get; set; }        // "N";
        //--------------------------------------------------------------------
        public string BDSQLSERVER { get; set; }             // "N";
        public string BDSQLSERVERINSTANCIA { get; set; }    // "SERVER/INSTANCIA";
        public string BDSQLSERVERPUERTO { get; set; }       // "PUERTO";
        public string BDSQLSERVERUSUARIO { get; set; }      // "USUARIO";
        public string BDSQLSERVERCLAVE { get; set; }        // "PASSWORD";
        public string BDSQLSERVERCATALOGO { get; set; }     // "CATALOGO";
        //--------------------------------------------------------------------
        public string BDREMOTAPENTALPHA { get; set; }       // "N";
        //--------------------------------------------------------------------
        public string BDREMOTACLIENTE { get; set; }         // "N";
        public string BDREMOTACLIENTEPUERTO { get; set; }         // "N";
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
            // localSettings.Values["PENTALPHA_ID"] = "EF00F8E2DA87E88D591BC033402208CA2C08A849298019366E23AB370A711BA6";
            // Se Intenta Leer Estructura de Pentalpha
            ESTADOPARAMETROS = "";
            ESTADOPARAMETROS = (string)localSettings.Values["ESTADOPARAMETROS"];
            if (ESTADOPARAMETROS == null || ESTADOPARAMETROS == "" || ESTADOPARAMETROS == "NADA")
            {
                ESTADOPARAMETROS = "NADA";
                SINCRONIZACIONWEBPENTALPHA = "N";
                SINCRONIZACIONWEBPROPIO = "N";
                DIRECTORIO_BASE_LOCAL = ApplicationData.Current.LocalFolder.Path;
                DIRECTORIO_RESPALDOS = ApplicationData.Current.LocalFolder.Path;
                DIRECTORIO_USB_MEMORIA = "";
                PENTALPHA_ID = "N";                                 // "N";
                BASEDEDATOSLOCAL = "N";                             // "N";
                BDSQLSERVER = "N";                                  // "N";
                BDSQLSERVERINSTANCIA = "SERVER/INSTANCIA";          // "SERVER/INSTANCIA";
                BDSQLSERVERPUERTO = "PUERTO";                       // "PUERTO";
                BDSQLSERVERUSUARIO = "USUARIO";                     // "USUARIO";
                BDSQLSERVERCLAVE = "CLAVE";                         // "PASSWORD";
                BDSQLSERVERCATALOGO = "BASE DE DATOS";              // "BASE DE DATOS";
                BDREMOTAPENTALPHA = "https://finanven.ddns.net";    // "N";
                BDREMOTACLIENTE = "";                               // "N";

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
                localSettings.Values["SINCRONIZACIONWEBPENTALPHA"] = SINCRONIZACIONWEBPENTALPHA;
                localSettings.Values["SINCRONIZACIONWEBPROPIO"] = SINCRONIZACIONWEBPENTALPHA;
                localSettings.Values["DIRECTORIO_RESPALDOS"] = DIRECTORIO_RESPALDOS;
                localSettings.Values["DIRECTORIO_BASE_LOCAL"] = DIRECTORIO_BASE_LOCAL;
                localSettings.Values["DIRECTORIO_USB_MEMORIA"] = DIRECTORIO_USB_MEMORIA;
                localSettings.Values["PENTALPHA_ID"] = PENTALPHA_ID;
                localSettings.Values["BASEDEDATOSLOCAL"] = BASEDEDATOSLOCAL;
                localSettings.Values["BDSQLSERVER"] = BDSQLSERVER;
                localSettings.Values["BDSQLSERVERINSTANCIA"] = BDSQLSERVERINSTANCIA;
                localSettings.Values["BDSQLSERVERPUERTO"] = BDSQLSERVERPUERTO;
                localSettings.Values["BDSQLSERVERUSUARIO"] = BDSQLSERVERUSUARIO;
                localSettings.Values["BDSQLSERVERCLAVE"] = BDSQLSERVERCLAVE;
                localSettings.Values["BDSQLSERVERCATALOGO"] = BDSQLSERVERCATALOGO;
                localSettings.Values["BDREMOTAPENTALPHA"] = BDREMOTAPENTALPHA;
                localSettings.Values["BDREMOTACLIENTE"] = BDREMOTACLIENTE;
                localSettings.Values["BDREMOTACLIENTEPUERTO"] = BDREMOTACLIENTEPUERTO;
                
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
                SINCRONIZACIONWEBPENTALPHA = (string)localSettings.Values["SINCRONIZACIONWEBPENTALPHA"];
                SINCRONIZACIONWEBPROPIO = (string)localSettings.Values["SINCRONIZACIONWEBPROPIO"];
                DIRECTORIO_RESPALDOS = ApplicationData.Current.LocalFolder.Path;
                DIRECTORIO_BASE_LOCAL = ApplicationData.Current.LocalFolder.Path;
                DIRECTORIO_USB_MEMORIA = (string)localSettings.Values["DIRECTORIO_USB_MEMORIA"];
                PENTALPHA_ID = (string)localSettings.Values["PENTALPHA_ID"];
                BASEDEDATOSLOCAL = (string)localSettings.Values["BASEDEDATOSLOCAL"];
                BDSQLSERVER = (string)localSettings.Values["BDSQLSERVER"];
                BDSQLSERVERINSTANCIA = (string)localSettings.Values["BDSQLSERVERINSTANCIA"];
                BDSQLSERVERPUERTO = (string)localSettings.Values["BDSQLSERVERPUERTO"];
                BDSQLSERVERUSUARIO = (string)localSettings.Values["BDSQLSERVERUSUARIO"];
                BDSQLSERVERCLAVE = (string)localSettings.Values["BDSQLSERVERCLAVE"];
                BDSQLSERVERCATALOGO = (string)localSettings.Values["BDSQLSERVERCATALOGO"];
                BDREMOTAPENTALPHA = (string)localSettings.Values["BDREMOTAPENTALPHA"];
                BDREMOTACLIENTE = (string)localSettings.Values["BDREMOTACLIENTE"];
                BDREMOTACLIENTEPUERTO = (string)localSettings.Values["BDREMOTACLIENTEPUERTO"];

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

                if (BDSQLSERVER == "S")
                {
                    //BM_Sql_String_Builder["Data Source"] = BDSQLSERVERINSTANCIA;
                    //BM_Sql_String_Builder["Initial Catalog"] = BDSQLSERVERCATALOGO;
                    //BM_Sql_String_Builder["MultipleActiveResultSets"] = true;
                    //BM_Sql_String_Builder["User ID"] = BDSQLSERVERUSUARIO;
                    //BM_Sql_String_Builder["Password"] = BDSQLSERVERCLAVE;
                }
            }
        }

        public void EscribirValoresDeAjustes()
        {
            localSettings.Values["ESTADOPARAMETROS"] = ESTADOPARAMETROS;

            localSettings.Values["SINCRONIZACIONWEBPENTALPHA"] = SINCRONIZACIONWEBPENTALPHA;
            localSettings.Values["SINCRONIZACIONWEBPROPIO"] = SINCRONIZACIONWEBPROPIO;
            
            localSettings.Values["DIRECTORIO_RESPALDOS"] = DIRECTORIO_RESPALDOS;
            localSettings.Values["DIRECTORIO_BASE_LOCAL"] = DIRECTORIO_BASE_LOCAL;
            localSettings.Values["DIRECTORIO_USB_MEMORIA"] = DIRECTORIO_USB_MEMORIA;
            
            localSettings.Values["PENTALPHA_ID"] = PENTALPHA_ID;
            
            localSettings.Values["BASEDEDATOSLOCAL"] = BASEDEDATOSLOCAL;
            
            localSettings.Values["BDSQLSERVER"] = BDSQLSERVER;
            localSettings.Values["BDSQLSERVERINSTANCIA"] = BDSQLSERVERINSTANCIA;
            localSettings.Values["BDSQLSERVERPUERTO"] = BDSQLSERVERPUERTO;
            localSettings.Values["BDSQLSERVERUSUARIO"] = BDSQLSERVERUSUARIO;
            localSettings.Values["BDSQLSERVERCLAVE"] = BDSQLSERVERCLAVE;
            localSettings.Values["BDSQLSERVERCATALOGO"] = BDSQLSERVERCATALOGO;
            
            localSettings.Values["BDREMOTAPENTALPHA"] = BDREMOTAPENTALPHA;
            localSettings.Values["BDREMOTACLIENTE"] = BDREMOTACLIENTE;
            localSettings.Values["BDREMOTACLIENTEPUERTO"] = BDREMOTACLIENTEPUERTO;

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

            if (BDSQLSERVER == "S")
            {
                //BM_Sql_String_Builder["Data Source"] = BDSQLSERVERINSTANCIA;
                //BM_Sql_String_Builder["Initial Catalog"] = BDSQLSERVERCATALOGO;
                //BM_Sql_String_Builder["MultipleActiveResultSets"] = true;
                //BM_Sql_String_Builder["User ID"] = BDSQLSERVERUSUARIO;
                //BM_Sql_String_Builder["Password"] = BDSQLSERVERCLAVE;
            }
        }

        public void LeerValoresDeAjustes()
        {
            ESTADOPARAMETROS = (string)localSettings.Values["ESTADOPARAMETROS"];
            SINCRONIZACIONWEBPENTALPHA = (string)localSettings.Values["SINCRONIZACIONWEBPENTALPHA"];
            SINCRONIZACIONWEBPROPIO = (string)localSettings.Values["SINCRONIZACIONWEBPROPIO"];
            DIRECTORIO_RESPALDOS = (string)localSettings.Values["DIRECTORIO_RESPALDOS"];
            DIRECTORIO_BASE_LOCAL = (string)localSettings.Values["DIRECTORIO_BASE_LOCAL"];
            DIRECTORIO_USB_MEMORIA = (string)localSettings.Values["DIRECTORIO_USB_MEMORIA"];
            PENTALPHA_ID = (string)localSettings.Values["PENTALPHA_ID"];
            BASEDEDATOSLOCAL = (string)localSettings.Values["BASEDEDATOSLOCAL"];
            BDSQLSERVER = (string)localSettings.Values["BDSQLSERVER"];
            BDSQLSERVERINSTANCIA = (string)localSettings.Values["BDSQLSERVERINSTANCIA"];
            BDSQLSERVERPUERTO = (string)localSettings.Values["BDSQLSERVERPUERTO"];
            BDSQLSERVERUSUARIO = (string)localSettings.Values["BDSQLSERVERUSUARIO"];
            BDSQLSERVERCLAVE = (string)localSettings.Values["BDSQLSERVERCLAVE"];
            BDSQLSERVERCATALOGO = (string)localSettings.Values["BDSQLSERVERCATALOGO"];
            BDREMOTAPENTALPHA = (string)localSettings.Values["BDREMOTAPENTALPHA"];
            BDREMOTACLIENTE = (string)localSettings.Values["BDREMOTACLIENTE"];
            BDREMOTACLIENTEPUERTO = (string)localSettings.Values["BDREMOTACLIENTEPUERTO"];
            
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

            if (BDSQLSERVER == "S")
            {
                //BM_Sql_String_Builder["Data Source"] = BDSQLSERVERINSTANCIA;
                //BM_Sql_String_Builder["Initial Catalog"] = BDSQLSERVERCATALOGO;
                //BM_Sql_String_Builder["MultipleActiveResultSets"] = true;
                //BM_Sql_String_Builder["User ID"] = BDSQLSERVERUSUARIO;
                //BM_Sql_String_Builder["Password"] = BDSQLSERVERCLAVE;
            }
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

        public bool SincronizarBaseSQLServer()
        {
            return (ESTADOPARAMETROS == "S") && (BDSQLSERVER == "S");
        }

        public bool SincronizarWebPentalpha()
        {
            return (ESTADOPARAMETROS == "S") && (SINCRONIZACIONWEBPENTALPHA == "S");
        }

        public bool SincronizarWebPropio()
        {
            return (ESTADOPARAMETROS == "S") && (SINCRONIZACIONWEBPROPIO == "S");
        }

        /*
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
        */
    }
}
