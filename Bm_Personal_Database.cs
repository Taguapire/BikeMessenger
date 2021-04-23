using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace BikeMessenger
{
    class Bm_Personal_Database
    {
        // public SQLiteFactory BM_DB;
        public SQLiteConnection BM_Connection;
        SQLiteCommand BK_Cmd_Personal;
        SQLiteDataReader BK_Reader_Personal;
    
        SQLiteCommand BK_Cmd_Personal_Pais;
        SQLiteDataReader BK_Reader_Personal_Pais;
     
        SQLiteCommand BK_Cmd_Personal_Grid;
        SQLiteDataReader BK_Reader_Personal_Grid;
        string StrAgregar_Personal;
        string StrModificar_Personal;
        string StrBorrar_Personal;
        readonly string StrBuscarGrid_Personal = "SELECT RUTID||'-'||DIGVER, APELLIDOS, NOMBRES FROM PERSONAL ORDER BY APELLIDOS ASC";
        readonly string StrBuscar_Personal = "SELECT * FROM PERSONAL";
        readonly String StrBuscar_Personal_Pais = "SELECT * FROM PAIS ORDER BY PAIS ASC";

        // Campos Personal
        // Campos de Empresa
        public string BK_RUTID { get; set; }
        public string BK_DIGVER { get; set; }
        public string BK_APELLIDOS { get; set; }
        public string BK_NOMBRES { get; set; }
        public string BK_TELEFONO1 { get; set; }
        public string BK_TELEFONO2 { get; set; }
        public string BK_EMAIL { get; set; }
        public string BK_AUTORIZACION { get; set; }
        public string BK_CARGO { get; set; }
        public string BK_DOMICILIO { get; set; }
        public string BK_NUMERO { get; set; }
        public string BK_PISO { get; set; }
        public string BK_DPTO { get; set; }
        public string BK_CODIGOPOSTAL { get; set; }
        public string BK_CIUDAD { get; set; }
        public string BK_COMUNA { get; set; }
        public string BK_REGION { get; set; }
        public string BK_PAIS { get; set; }
        public string BK_OBSERVACIONES { get; set; }
        public string BK_FOTO { get; set; }

        public string BK_GRID_RUT { get; set; }
        public string BK_GRID_APELLIDOS { get; set; }
        public string BK_GRID_NOMBRES { get; set; }

        // Campos de PAIS
        public Int16 BK_E_CODPAIS { get; set; }
        public string BK_E_PAIS { get; set; }

        public void BM_CreateDatabase(SQLiteConnection BM_Connection)
        {
            this.BM_Connection = BM_Connection;
        }

        public bool Bm_Personal_Agregar()
        {
            try
            {
                StrAgregar_Personal = "INSERT INTO PERSONAL (";
                StrAgregar_Personal += "RUTID,";
                StrAgregar_Personal += "DIGVER,";
                StrAgregar_Personal += "APELLIDOS,";
                StrAgregar_Personal += "NOMBRES,";
                StrAgregar_Personal += "TELEFONO1,";
                StrAgregar_Personal += "TELEFONO2,";
                StrAgregar_Personal += "EMAIL,";
                StrAgregar_Personal += "AUTORIZACION,";
                StrAgregar_Personal += "CARGO,";
                StrAgregar_Personal += "DOMICILIO,";
                StrAgregar_Personal += "NUMERO,";
                StrAgregar_Personal += "PISO,";
                StrAgregar_Personal += "DPTO,";
                StrAgregar_Personal += "CODIGOPOSTAL,";
                StrAgregar_Personal += "CIUDAD,";
                StrAgregar_Personal += "COMUNA,";
                StrAgregar_Personal += "REGION,";
                StrAgregar_Personal += "PAIS,";
                StrAgregar_Personal += "OBSERVACIONES,";
                StrAgregar_Personal += "FOTO) VALUES (";
                StrAgregar_Personal += "'" + BK_RUTID + "',";
                StrAgregar_Personal += "'" + BK_DIGVER + "',";
                StrAgregar_Personal += "'" + BK_APELLIDOS + "',";
                StrAgregar_Personal += "'" + BK_NOMBRES + "',";
                StrAgregar_Personal += "'" + BK_TELEFONO1 + "',";
                StrAgregar_Personal += "'" + BK_TELEFONO2 + "',";
                StrAgregar_Personal += "'" + BK_EMAIL + "',";
                StrAgregar_Personal += "'" + BK_AUTORIZACION + "',";
                StrAgregar_Personal += "'" + BK_CARGO + "',";
                StrAgregar_Personal += "'" + BK_DOMICILIO + "',";
                StrAgregar_Personal += "'" + BK_NUMERO + "',";
                StrAgregar_Personal += "'" + BK_PISO + "',";
                StrAgregar_Personal += "'" + BK_DPTO + "',";
                StrAgregar_Personal += "'" + BK_CODIGOPOSTAL + "',";
                StrAgregar_Personal += "'" + BK_CIUDAD + "',";
                StrAgregar_Personal += "'" + BK_COMUNA + "',";
                StrAgregar_Personal += "'" + BK_REGION + "',";
                StrAgregar_Personal += "'" + BK_PAIS + "',";
                StrAgregar_Personal += "'" + BK_OBSERVACIONES + "',";
                StrAgregar_Personal += "'" + BK_FOTO + "')";
                BK_Cmd_Personal = new SQLiteCommand(StrAgregar_Personal, BM_Connection);
                BK_Cmd_Personal.ExecuteNonQuery();
                return true;
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                return false;
            }
        }

        public bool Bm_Personal_Buscar()
        {
            try
            {
                BK_Cmd_Personal = new SQLiteCommand(StrBuscar_Personal, BM_Connection);
                BK_Reader_Personal = BK_Cmd_Personal.ExecuteReader();
                if (BK_Reader_Personal.Read())
                {
                    // Llenar Valores de Personal
                    BK_RUTID = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("RUTID"));
                    BK_DIGVER = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("DIGVER"));
                    BK_APELLIDOS = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("APELLIDOS"));
                    BK_NOMBRES = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("NOMBRES"));
                    BK_TELEFONO1 = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("TELEFONO1"));
                    BK_TELEFONO2 = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("TELEFONO2"));
                    BK_EMAIL = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("EMAIL"));
                    BK_AUTORIZACION = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("AUTORIZACION"));
                    BK_CARGO = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("CARGO"));
                    BK_DOMICILIO = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("DOMICILIO"));
                    BK_NUMERO = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("NUMERO"));
                    BK_PISO = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("PISO"));
                    BK_DPTO = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("DPTO"));
                    BK_CODIGOPOSTAL = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("CODIGOPOSTAL"));
                    BK_CIUDAD = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("CIUDAD"));
                    BK_COMUNA = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("COMUNA"));
                    BK_REGION = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("REGION"));
                    BK_PAIS = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("PAIS"));
                    BK_OBSERVACIONES = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("OBSERVACIONES"));
                    BK_FOTO = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("FOTO"));
                    return true;
                }
                else
                {
                    // No existe la Personal
                    return false;
                }
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                return false;
            }
            catch (System.InvalidCastException)
            {
                return false;
            }
        }

        public bool Bm_Personal_Buscar(string pRUTID, string pDIGVER)
        {
            try
            {
                BK_Cmd_Personal = new SQLiteCommand(StrBuscar_Personal + " WHERE RUTID = '" + pRUTID + "' AND DIGVER = '" + pDIGVER + "'", BM_Connection);
                BK_Reader_Personal = BK_Cmd_Personal.ExecuteReader();

                if (BK_Reader_Personal.Read())
                {
                    // Llenar Valores de Personal

                    BK_RUTID = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("RUTID"));
                    BK_DIGVER = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("DIGVER"));
                    BK_APELLIDOS = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("APELLIDOS"));
                    BK_NOMBRES = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("NOMBRES"));
                    BK_TELEFONO1 = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("TELEFONO1"));
                    BK_TELEFONO2 = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("TELEFONO2"));
                    BK_EMAIL = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("EMAIL"));
                    BK_AUTORIZACION = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("AUTORIZACION"));
                    BK_CARGO = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("CARGO"));
                    BK_DOMICILIO = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("DOMICILIO"));
                    BK_NUMERO = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("NUMERO"));
                    BK_PISO = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("PISO"));
                    BK_DPTO = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("DPTO"));
                    BK_CODIGOPOSTAL = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("CODIGOPOSTAL"));
                    BK_CIUDAD = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("CIUDAD"));
                    BK_COMUNA = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("COMUNA"));
                    BK_REGION = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("REGION"));
                    BK_PAIS = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("PAIS"));
                    BK_OBSERVACIONES = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("OBSERVACIONES"));
                    BK_FOTO = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("FOTO"));
                    return true;
                }
                else
                {
                    // No existe la persona
                    return false;
                }
            }
            catch (System.Data.SQLite.SQLiteException) {
                return false;
            }
            catch (System.InvalidCastException)
            {
                return false;
            }
        }

        // Procedimiento Modificar Personal
        public bool Bm_Personal_Modificar(string pRUTID, string pDIGVER)
        {
            try
            {
                StrModificar_Personal = "UPDATE PERSONAL SET ";
                StrModificar_Personal += "APELLIDOS = '" + BK_APELLIDOS + "',";
                StrModificar_Personal += "NOMBRES = '" + BK_NOMBRES + "',";
                StrModificar_Personal += "TELEFONO1 = '" + BK_TELEFONO1 + "',";
                StrModificar_Personal += "TELEFONO2 = '" + BK_TELEFONO2 + "',";
                StrModificar_Personal += "EMAIL = '" + BK_EMAIL + "',";
                StrModificar_Personal += "AUTORIZACION = '" + BK_AUTORIZACION + "',";
                StrModificar_Personal += "CARGO = '" + BK_CARGO + "',";
                StrModificar_Personal += "DOMICILIO = '" + BK_DOMICILIO + "',";
                StrModificar_Personal += "NUMERO = '" + BK_NUMERO + "',";
                StrModificar_Personal += "PISO = '" + BK_PISO + "',";
                StrModificar_Personal += "DPTO = '" + BK_DPTO + "',";
                StrModificar_Personal += "CODIGOPOSTAL = '" + BK_CODIGOPOSTAL + "',";
                StrModificar_Personal += "CIUDAD = '" + BK_CIUDAD + "',";
                StrModificar_Personal += "COMUNA = '" + BK_COMUNA + "',";
                StrModificar_Personal += "REGION = '" + BK_REGION + "',";
                StrModificar_Personal += "PAIS = '" + BK_PAIS + "',";
                StrModificar_Personal += "OBSERVACIONES = '" + BK_OBSERVACIONES + "',";
                StrModificar_Personal += "FOTO = '" + BK_FOTO + "' ";
                StrModificar_Personal += "WHERE ";
                StrModificar_Personal += "RUTID = '" + pRUTID + "' AND ";
                StrModificar_Personal += "DIGVER = '" + pDIGVER + "'";
                BK_Cmd_Personal = new SQLiteCommand(StrModificar_Personal, BM_Connection);
                BK_Cmd_Personal.ExecuteNonQuery();
                return true;
            }
            catch (System.Data.SQLite.SQLiteException) {
                return false;
            }
        }

        // Procedimiento Borrar Personal
        public bool Bm_Personal_Borrar(string pRUTID, string pDIGVER)
        {
            StrBorrar_Personal = "DELETE FROM PERSONAL ";
            StrBorrar_Personal += "WHERE ";
            StrBorrar_Personal += "RUTID = '" + pRUTID + "' AND ";
            StrBorrar_Personal += "DIGVER = '" + pDIGVER + "'";
            BK_Cmd_Personal = new SQLiteCommand(StrBorrar_Personal, BM_Connection);
            BK_Cmd_Personal.ExecuteNonQuery();
            return true;
        }

        // Procedimiento Buscar Pais
        public bool Bm_E_Pais_EjecutarSelect()
        {
            BK_Cmd_Personal_Pais = new SQLiteCommand(StrBuscar_Personal_Pais, BM_Connection);
            BK_Reader_Personal_Pais = BK_Cmd_Personal_Pais.ExecuteReader();
            return true;
        }

        public bool Bm_E_Pais_Buscar()
        {
            if (BK_Reader_Personal_Pais.Read())
            {
                // Llenar Valores de la Empresa
                BK_E_CODPAIS = BK_Reader_Personal_Pais.GetInt16(BK_Reader_Personal_Pais.GetOrdinal("CODPAIS"));
                BK_E_PAIS = BK_Reader_Personal_Pais.GetString(BK_Reader_Personal_Pais.GetOrdinal("PAIS"));
                return true;
            }
            else
            {
                // No existe la empresa
                return false;
            }
        }

        public bool Bm_Personal_BuscarGrid()
        {
            BK_Cmd_Personal_Grid = new SQLiteCommand(StrBuscarGrid_Personal, BM_Connection);
            BK_Reader_Personal_Grid = BK_Cmd_Personal_Grid.ExecuteReader();
            return true;
        }

        public bool Bm_Personal_BuscarGridProxima()
        {
            if (BK_Reader_Personal_Grid.Read())
            {
                // Llenar Valores de la Empresa
                BK_GRID_RUT = BK_Reader_Personal_Grid.GetString(0);
                BK_GRID_APELLIDOS = BK_Reader_Personal_Grid.GetString(1);
                BK_GRID_NOMBRES = BK_Reader_Personal_Grid.GetString(2);
                return true;
            }
            else
            {
                // No existe la empresa
                return false;
            }
        }
    }
}
