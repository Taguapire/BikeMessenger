using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using Windows.Storage;

namespace BikeMessenger
{
    class TransferVar
    {
        // Valores de Base de Datos
        public SqliteFactory TV_Factory;
        public SqliteConnection TV_Connection;

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

        public string Directorio { get; set; }

        public TransferVar()
        {
            //TV_Factory = new SqliteFactory;
            // Crear Automaticamente la Base de Datos

            if (!VerificarDirectorio())
                CrearDirectorio(Windows.Storage.ApplicationData.Current.LocalFolder.Path);

            LeerDirectorio();

            //TV_Connection = (SqliteConnection)TV_Factory.CreateConnection();
            TV_Connection = (SqliteConnection) SqliteFactory.Instance.CreateConnection();
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
