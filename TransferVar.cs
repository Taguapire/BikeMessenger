using Microsoft.Data.Sqlite;
using Windows.Storage;

namespace BikeMessenger
{
    internal class TransferVar
    {
        // Valores de Base de Datos

        public SqliteConnection TV_Connection { get; set; }

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

        public string Directorio { get; set; }
        public string PENTALPHA { get; internal set; }

        public TransferVar()
        {
            //TV_Factory = new SqliteFactory;
            // Crear Automaticamente la Base de Datos

            if (!VerificarDirectorio())
                CrearDirectorio(ApplicationData.Current.LocalFolder.Path);

            LeerDirectorio();

            //TV_Connection = (SqliteConnection)TV_Factory.CreateConnection();
            TV_Connection = (SqliteConnection)SqliteFactory.Instance.CreateConnection();
            TV_Connection.ConnectionString = "Data Source=" + Directorio + "\\BikeMessenger.db";
            TV_Connection.Open();

            // Console.WriteLine(Directorio);
        }

        public bool VerificarDirectorio()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            Directorio = (string)localSettings.Values["PENTALPHA"];

            if (Directorio == null || Directorio == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void CrearDirectorio(string pDirectorio)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["PENTALPHA"] = pDirectorio;
            return;
        }

        public void LeerDirectorio()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            Directorio = (string)localSettings.Values["PENTALPHA"];
            return;
        }
    }
}
