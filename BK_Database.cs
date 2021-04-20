using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using Windows.UI.Xaml;

namespace BikeMessenger
{
    class BK_Database
    {
        // Principales de Uso de Bases de Datos
        // Crear la Base de Datos
        // Verifica que todos los objetos existan

        public SQLiteFactory BM_DB;
        public SQLiteConnection BM_Connection;
        public Bm_Empresa_Database Db_Empresa;

        // Comandos de acceso por Area

        public Boolean BM_CreateDatabase()
        {
            try
            {
                BM_DB = new SQLiteFactory();
                // Crear Automaticamente la Base de Datos
                BM_Connection = (SQLiteConnection) BM_DB.CreateConnection();

                BM_Connection.ConnectionString = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\BikeMessenger.db; Version=3";

                BM_Connection.Open();

                // Verificar que Base es nueva o ya esta creada con objetos
                Db_Empresa = new Bm_Empresa_Database(BM_Connection);
                
                // Si es nueva deben crearse los objetos

                return true;
            }
            catch (System.Data.SQLite.SQLiteException e)
            {
                return false;
            }
        }

        public bool Buscar_Empresa()
        {
            // Buscar Datos Empresa
            if (Db_Empresa.Bm_Empresa_Buscar())
            {
                return true; // Empresa encontrada
            }
            else
            {
                return false;
            }
        }
    }
}
