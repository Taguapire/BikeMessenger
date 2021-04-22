using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMessenger
{
    class TransferVar
    {
        // Valores de Base de Datos
        public SQLiteFactory TV_Factory;
        public SQLiteConnection TV_Connection;

        // Valores de Empresa
        public string E_RUTID;
        public string E_DIGVER;

        // Valores de Personal
        public string P_RUTID;
        public string P_DIGVER;

        // Valores de Recursos
        public string R_RUTID;
        public string R_DIGVER;
        public string R_PAT_SER;

        // Valores de Clientes
        public string C_RUTID;
        public string C_DIGVER;

        // Valores de SERVICIOS
        public string S_NROENVIO;

        public TransferVar()
        {
            TV_Factory = new SQLiteFactory();
            // Crear Automaticamente la Base de Datos
            TV_Connection = (SQLiteConnection)TV_Factory.CreateConnection();
            TV_Connection.ConnectionString = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\BikeMessenger.db; Version = 3";
            TV_Connection.Open();
        }
    }
}
