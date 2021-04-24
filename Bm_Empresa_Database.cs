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
        // public SQLiteFactory BM_DB;
        public SQLiteConnection BM_Connection;
        SQLiteCommand BK_Cmd_Empresa;
        SQLiteCommand BK_Cmd_Empresa_Pais;
        SQLiteDataReader BK_Reader_Empresa;
        SQLiteDataReader BK_Reader_Empresa_Pais;
        readonly String StrBuscar_Empresa = "SELECT * FROM EMPRESA";
        readonly String StrBuscar_Empresa_Pais = "SELECT * FROM PAIS ORDER BY PAIS ASC";

        string StrAgregar_Empresa;
        string StrModificar_Empresa;
        string StrBorrar_Empresa;

        // Campos de Empresa
        public string BK_RUTID { get; set; }
        public string BK_DIGVER { get; set; }
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
        public string BK_LOGO { get; set; }

        // Campos de PAIS
        public Int16 BK_E_CODPAIS { get; set; }
        public string BK_E_PAIS { get; set; }

        // Comandos de acceso por Area

        public Boolean BM_CreateDatabase(SQLiteConnection BM_Connection)
        {
            this.BM_Connection = BM_Connection;
            return true;
        }

        // Procedimiento Buscar Empresa
        public bool Bm_Empresa_Buscar()
        {
            try
            {
                BK_Cmd_Empresa = new SQLiteCommand(StrBuscar_Empresa, BM_Connection);
                BK_Reader_Empresa = BK_Cmd_Empresa.ExecuteReader();

                if (BK_Reader_Empresa.Read())
                {
                    // Llenar Valores de la Empresa
                    BK_RUTID = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("RUTID"));
                    BK_DIGVER = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("DIGVER"));
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
                    BK_LOGO = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("LOGO"));
                    return true;
                }
                else
                {
                    // No existe la empresa
                    return false;
                }
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                return false;
            }
        }

        // Procedimiento Buscar Pais
        public bool Bm_E_Pais_EjecutarSelect()
        {
            try {
                BK_Cmd_Empresa_Pais = new SQLiteCommand(StrBuscar_Empresa_Pais, BM_Connection);
                BK_Reader_Empresa_Pais = BK_Cmd_Empresa_Pais.ExecuteReader();
                return true;
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                return false;
            }
        }

        public bool Bm_E_Pais_Buscar()
        {
            try {
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
            catch (System.Data.SQLite.SQLiteException)
            {
                return false;
            }
        }

        // Procedimiento Insertar Empresa

        public bool Bm_Empresa_Agregar()
        {
            StrAgregar_Empresa = "INSERT INTO EMPRESA (";
            StrAgregar_Empresa += "RUTID,";
            StrAgregar_Empresa += "DIGVER,";
            StrAgregar_Empresa += "NOMBRE,";
            StrAgregar_Empresa += "ACTIVIDAD1,";
            StrAgregar_Empresa += "ACTIVIDAD2,";
            StrAgregar_Empresa += "REPRESENTANTE1,";
            StrAgregar_Empresa += "REPRESENTANTE2,";
            StrAgregar_Empresa += "REPRESENTANTE3,";
            StrAgregar_Empresa += "DOMICILIO1,";
            StrAgregar_Empresa += "DOMICILIO2,";
            StrAgregar_Empresa += "NUMERO,";
            StrAgregar_Empresa += "PISO,";
            StrAgregar_Empresa += "OFICINA,";
            StrAgregar_Empresa += "CIUDAD,";
            StrAgregar_Empresa += "COMUNA,";
            StrAgregar_Empresa += "ESTADOREGION,";
            StrAgregar_Empresa += "CODIGOPOSTAL,";
            StrAgregar_Empresa += "PAIS,";
            StrAgregar_Empresa += "OBSERVACIONES,";
            StrAgregar_Empresa += "LOGO) VALUES (";
            StrAgregar_Empresa += "'" + BK_RUTID + "',";
            StrAgregar_Empresa += "'" + BK_DIGVER + "',";
            StrAgregar_Empresa += "'" + BK_NOMBRE + "',";
            StrAgregar_Empresa += "'" + BK_ACTIVIDAD1 + "',";
            StrAgregar_Empresa += "'" + BK_ACTIVIDAD2 + "',";
            StrAgregar_Empresa += "'" + BK_REPRESENTANTE1 + "',";
            StrAgregar_Empresa += "'" + BK_REPRESENTANTE2 + "',";
            StrAgregar_Empresa += "'" + BK_REPRESENTANTE3 + "',";
            StrAgregar_Empresa += "'" + BK_DOMICILIO1 + "',";
            StrAgregar_Empresa += "'" + BK_DOMICILIO2 + "',";
            StrAgregar_Empresa += "'" + BK_NUMERO + "',";
            StrAgregar_Empresa += "'" + BK_PISO + "',";
            StrAgregar_Empresa += "'" + BK_OFICINA + "',";
            StrAgregar_Empresa += "'" + BK_CIUDAD + "',";
            StrAgregar_Empresa += "'" + BK_COMUNA + "',";
            StrAgregar_Empresa += "'" + BK_ESTADOREGION + "',";
            StrAgregar_Empresa += "'" + BK_CODIGOPOSTAL + "',";
            StrAgregar_Empresa += "'" + BK_PAIS + "',";
            StrAgregar_Empresa += "'" + BK_OBSERVACIONES + "',";
            StrAgregar_Empresa += "'" + BK_LOGO + "')";
            
            try
            {
                BK_Cmd_Empresa = new SQLiteCommand(StrAgregar_Empresa, BM_Connection);
                BK_Cmd_Empresa.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        // Procedimiento Modificar Empresa
        public bool Bm_Empresa_Modificar()
        {
            StrModificar_Empresa = "UPDATE EMPRESA SET ";
            StrModificar_Empresa += "RUTID = '" + BK_RUTID + "',";
            StrModificar_Empresa += "DIGVER = '" + BK_DIGVER + "',";
            StrModificar_Empresa += "NOMBRE = '" + BK_NOMBRE + "',";
            StrModificar_Empresa += "ACTIVIDAD1 = '" + BK_ACTIVIDAD1 + "',";
            StrModificar_Empresa += "ACTIVIDAD2 = '" + BK_ACTIVIDAD2 + "',";
            StrModificar_Empresa += "REPRESENTANTE1 = '" + BK_REPRESENTANTE1 + "',";
            StrModificar_Empresa += "REPRESENTANTE2 = '" + BK_REPRESENTANTE2 + "',";
            StrModificar_Empresa += "REPRESENTANTE3 = '" + BK_REPRESENTANTE3 + "',";
            StrModificar_Empresa += "DOMICILIO1 = '" + BK_DOMICILIO1 + "',";
            StrModificar_Empresa += "DOMICILIO2 = '" + BK_DOMICILIO2 + "',";
            StrModificar_Empresa += "NUMERO = '" + BK_NUMERO + "',";
            StrModificar_Empresa += "PISO = '" + BK_PISO + "',";
            StrModificar_Empresa += "OFICINA = '" + BK_OFICINA + "',";
            StrModificar_Empresa += "CIUDAD = '" + BK_CIUDAD + "',";
            StrModificar_Empresa += "COMUNA = '" + BK_COMUNA + "',";
            StrModificar_Empresa += "ESTADOREGION = '" + BK_ESTADOREGION + "',";
            StrModificar_Empresa += "CODIGOPOSTAL = '" + BK_CODIGOPOSTAL + "',";
            StrModificar_Empresa += "PAIS = '" + BK_PAIS + "',";
            StrModificar_Empresa += "OBSERVACIONES = '" + BK_OBSERVACIONES + "',";
            StrModificar_Empresa += "LOGO = '" + BK_LOGO + "'";
 
            try
            {
                BK_Cmd_Empresa = new SQLiteCommand(StrModificar_Empresa, BM_Connection);
                BK_Cmd_Empresa.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }
        // Procedimiento Eliminar Empresa
        
        public bool Bm_Empresa_Borrar()
        {
            StrBorrar_Empresa = "DELETE FROM EMPRESA";
            try
            {
                BK_Cmd_Empresa = new SQLiteCommand(StrBorrar_Empresa, BM_Connection);
                BK_Cmd_Empresa.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }
        // Procedimientos Buscar Siguente
        // Procedimientos Buscar Anterior
        // 
    }
}
