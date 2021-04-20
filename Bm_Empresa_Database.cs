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
        string StrModificar_Empresa_Pais;

        // Campos de Empresa
        public string BK_RUTID { get; set; }
        public string BK_DIGVER { get; set; }
        public string BK_LOGO { get; set; }
        public string BK_NOMBRE { get; set; }
        public string BK_ACTIVIDAD1 { get; set; }
        public string BK_ACTIVIDAD2 { get; set; }
        public string BK_REPRESENTANTE1 { get; set; }
        public string BK_REPRESENTANTE2 { get; set; }
        public string BK_REPRESENTANTE3 { get; set; }
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
        public string BK_OBSERVACIONES { get; set; }


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
                BK_ACTIVIDAD1 = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("ACTIVIDAD1"));
                BK_ACTIVIDAD2 = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("ACTIVIDAD2"));
                BK_REPRESENTANTE1 = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("REPRESENTANTE1"));
                BK_REPRESENTANTE2 = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("REPRESENTANTE2"));
                BK_REPRESENTANTE3 = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("REPRESENTANTE3"));
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
                BK_OBSERVACIONES = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("OBSERVACIONES"));
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
        public bool Bm_Modificar_Empresa()
        {
            StrModificar_Empresa_Pais = "UPDATE EMPRESA SET ";
            StrModificar_Empresa_Pais += "RUTID = '" + BK_RUTID + "',";
            StrModificar_Empresa_Pais += "DIGVER = '" + BK_DIGVER + "',";
            StrModificar_Empresa_Pais += "LOGO = '" + BK_LOGO + "',";
            StrModificar_Empresa_Pais += "NOMBRE = '" + BK_NOMBRE + "',";
            StrModificar_Empresa_Pais += "ACTIVIDAD1 = '" + BK_ACTIVIDAD1 + "',";
            StrModificar_Empresa_Pais += "ACTIVIDAD2 = '" + BK_ACTIVIDAD2 + "',";
            StrModificar_Empresa_Pais += "REPRESENTANTE1 = '" + BK_REPRESENTANTE1 + "',";
            StrModificar_Empresa_Pais += "REPRESENTANTE2 = '" + BK_REPRESENTANTE2 + "',";
            StrModificar_Empresa_Pais += "REPRESENTANTE3 = '" + BK_REPRESENTANTE3 + "',";
            StrModificar_Empresa_Pais += "DOMICILIO1 = '" + BK_DOMICILIO1 + "',";
            StrModificar_Empresa_Pais += "DOMICILIO2 = '" + BK_DOMICILIO2 + "',";
            StrModificar_Empresa_Pais += "NUMERO = '" + BK_NUMERO + "',";
            StrModificar_Empresa_Pais += "PISO = '" + BK_PISO + "',";
            StrModificar_Empresa_Pais += "OFICINA = '" + BK_OFICINA + "',";
            StrModificar_Empresa_Pais += "CIUDAD = '" + BK_CIUDAD + "',";
            StrModificar_Empresa_Pais += "COMUNA = '" + BK_COMUNA + "',";
            StrModificar_Empresa_Pais += "ESTADOREGION = '" + BK_ESTADOREGION + "',";
            StrModificar_Empresa_Pais += "CODIGOPOSTAL = '" + BK_CODIGOPOSTAL + "',";
            StrModificar_Empresa_Pais += "PAIS = '" + BK_PAIS + "',";
            StrModificar_Empresa_Pais += "OBSERVACIONES = '" + BK_OBSERVACIONES + "'";
            BK_Cmd_Empresa = new SQLiteCommand(StrModificar_Empresa_Pais, BM_Connection);
            BK_Cmd_Empresa.ExecuteNonQuery();
            return true;
        }
        // Procedimiento Eliminar Empresa
        // Procedimientos Buscar Siguente
        // Procedimientos Buscar Anterior
        // 
    }
}
