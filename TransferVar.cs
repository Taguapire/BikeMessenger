using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Data;
using System.Data.SqlClient;

namespace BikeMessenger
{
    internal class TransferVar
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        // Valores de Base de Datos

        public string ESTADOPARAMETROS { get; set; }        // "NADA/SQLITE/SQLSERVER/WEBPENTALPHA/WEBPROPIO"
        public string SINCRONIZACIONWEB { get; set; }       // "S/N"
        //--------------------------------------------------------------------
        public string DIRECTORIO_ALMACEN { get; set; }      // "ApplicationData.Current.LocalFolder.Path";
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
        //--------------------------------------------------------------------
        public SqlConnectionStringBuilder BM_Sql_String_Builder = new SqlConnectionStringBuilder();
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
            localSettings.Values.Remove("PENTALPHA");
            localSettings.Values.Remove("PENTALPHA_RESPALDOS");
            localSettings.Values.Remove("PENTALPHA_REMOTO");
            localSettings.Values.Remove("PENTALPHA_PROPIO");
            // Se Intenta Leer Estructura de Pentalpha
            ESTADOPARAMETROS = "";
            ESTADOPARAMETROS = (string)localSettings.Values["ESTADOPARAMETROS"];
            if (ESTADOPARAMETROS == null || ESTADOPARAMETROS == "" || ESTADOPARAMETROS == "NADA")
            {
                ESTADOPARAMETROS = "NADA";
                SINCRONIZACIONWEB = "N";
                DIRECTORIO_ALMACEN = ApplicationData.Current.LocalFolder.Path;
                DIRECTORIO_BASE_LOCAL = ApplicationData.Current.LocalFolder.Path;
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

                localSettings.Values["ESTADOPARAMETROS"] = ESTADOPARAMETROS;
                localSettings.Values["SINCRONIZACIONWEB"] = SINCRONIZACIONWEB;
                localSettings.Values["DIRECTORIO_ALMACEN"] = DIRECTORIO_ALMACEN;
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
            }
            else
            {
                ESTADOPARAMETROS = (string)localSettings.Values["ESTADOPARAMETROS"];
                SINCRONIZACIONWEB = (string)localSettings.Values["SINCRONIZACIONWEB"];
                DIRECTORIO_ALMACEN = ApplicationData.Current.LocalFolder.Path;
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

                if (BDSQLSERVER == "S")
                {
                    BM_Sql_String_Builder["Data Source"] = BDSQLSERVERINSTANCIA;
                    BM_Sql_String_Builder["Initial Catalog"] = BDSQLSERVERCATALOGO;
                    BM_Sql_String_Builder["MultipleActiveResultSets"] = true;
                    BM_Sql_String_Builder["User ID"] = BDSQLSERVERUSUARIO;
                    BM_Sql_String_Builder["Password"] = BDSQLSERVERCLAVE;
                }
            }
        }

        public void EscribirValoresDeAjustes()
        {
            localSettings.Values["ESTADOPARAMETROS"] = ESTADOPARAMETROS;
            localSettings.Values["SINCRONIZACIONWEB"] = SINCRONIZACIONWEB;
            localSettings.Values["DIRECTORIO_ALMACEN"] = DIRECTORIO_ALMACEN;
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
        }

        public void LeerValoresDeAjustes()
        {
            ESTADOPARAMETROS = (string)localSettings.Values["ESTADOPARAMETROS"];
            SINCRONIZACIONWEB = (string)localSettings.Values["SINCRONIZACIONWEB"];
            DIRECTORIO_ALMACEN = (string)localSettings.Values["DIRECTORIO_ALMACEN"];
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
        }

        public bool SincronizarBaseLocal()
        {
            return BASEDEDATOSLOCAL == "S";
        }

        public bool SincronizarBaseSQLServer()
        {
            return BDSQLSERVER == "S";
        }

        public bool SincronizarWebRemoto()
        {
            return SINCRONIZACIONWEB == "R";
        }

        public bool SincronizarWebPropio()
        {
            return SINCRONIZACIONWEB == "P";
        }

        public void CrearSincronizarRemoto(string pSINO)
        {
            SINCRONIZACIONWEB = pSINO;djj
            localSettings.Values["SINCRONIZACIONWEB"] = SINCRONIZACIONWEB;
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
    }
}
