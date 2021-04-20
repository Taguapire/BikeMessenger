using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace BikeMessenger
{
    class Bm_Empresa_Database
    {
        readonly SQLiteConnection BM_Connection;
        SQLiteCommand BK_Cmd_Empresa;
        SQLiteCommand BK_Cmd_Empresa_Pais;
        SQLiteDataReader BK_Reader_Empresa;
        SQLiteDataReader BK_Reader_Empresa_Pais;
        readonly String StrBuscar_Empresa = "SELECT * FROM EMPRESA";
        readonly String StrBuscar_Empresa_Pais = "SELECT * FROM PAIS ORDER BY PAIS ASC";

        // Campos de Empresa
        public string BK_RUTID { get; set; }
        public string BK_DIGVER { get; set; }
        public string BK_LOGO { get; set; }
        public string BK_NOMBRE { get; set; }
        public string BK_ACTIVIDAD { get; set; }
        public string BK_REPRESENTANTES { get; set; }
        public string BK_DOMICILIO1 { get; set; }
        public string BK_DOMICILIO2 { get; set; }
        public string BK_NUMERO { get; set; }
        public string BK_PISO { get; set; }
        public string BK_OFICINA { get; set; }
        public string BK_CIUDAD { get; set; }
        public string BK_COMUNA { get; set; }
        public string BK_ESTADOREGION { get; set; }
        public string BK_CODIGOPOSTAL { get; set; }
        public string BK_PAIS { get; set; }

        // Campos de PAIS
        public Int16 BK_E_CODPAIS { get; set; }
        public string BK_E_PAIS { get; set; }


        public Bm_Empresa_Database (SQLiteConnection BM_Connection)
        {
            this.BM_Connection = BM_Connection;
        }

        // Procedimiento Buscar Empresa
        public bool Bm_Empresa_Buscar()
        {
            BK_Cmd_Empresa = new SQLiteCommand(StrBuscar_Empresa, BM_Connection);
            BK_Reader_Empresa = BK_Cmd_Empresa.ExecuteReader();

            if (BK_Reader_Empresa.Read())
            {
                // Llenar Valores de la Empresa
                BK_RUTID = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("RUTID"));
                BK_DIGVER = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("DIGVER"));
                BK_LOGO = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("LOGO"));
                BK_NOMBRE = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("NOMBRE"));
                BK_ACTIVIDAD = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("ACTIVIDAD"));
                BK_REPRESENTANTES = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("REPRESENTANTES"));
                BK_DOMICILIO1 = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("DOMICILIO1"));
                BK_DOMICILIO2 = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("DOMICILIO2"));
                BK_NUMERO = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("NUMERO"));
                BK_PISO = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("PISO"));
                BK_OFICINA = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("OFICINA"));
                BK_CIUDAD = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("CIUDAD"));
                BK_COMUNA = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("COMUNA"));
                BK_ESTADOREGION = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("ESTADOREGION"));
                BK_CODIGOPOSTAL = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("CODIGOPOSTAL"));
                BK_PAIS = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("PAIS"));
                return true;
            }
            else
            {
                // No existe la empresa
                return false;
            }
        }

        // Procedimiento Buscar Pais
        public bool Bm_E_Pais_EjecutarSelect()
        {
            BK_Cmd_Empresa_Pais = new SQLiteCommand(StrBuscar_Empresa_Pais, BM_Connection);
            BK_Reader_Empresa_Pais = BK_Cmd_Empresa_Pais.ExecuteReader();
            return true;
        }

        public bool Bm_E_Pais_Buscar()
        {
            if (BK_Reader_Empresa_Pais.Read())
            {
                // Llenar Valores de la Empresa
                BK_E_CODPAIS = BK_Reader_Empresa_Pais.GetInt16(BK_Reader_Empresa_Pais.GetOrdinal("CODPAIS"));
                BK_E_PAIS = BK_Reader_Empresa_Pais.GetString(BK_Reader_Empresa_Pais.GetOrdinal("PAIS"));
                return true;
            }
            else
            {
                // No existe la empresa
                return false;
            }
        }

        // Procedimiento Insertar Empresa
        // Procedimiento Modificar Empresa
        // Procedimiento Eliminar Empresa
        // Procedimientos Buscar Siguente
        // Procedimientos Buscar Anterior
        // 
    }
}
