using System;
using System.Data.SQLite;

namespace BikeMessenger
{
    class Bm_Recursos_Database
    {
        // public SQLiteFactory BM_DB;
        public SQLiteConnection BM_Connection;
        SQLiteCommand BK_Cmd_Recursos;
        SQLiteDataReader BK_Reader_Recursos;

        SQLiteCommand BK_Cmd_Recursos_Pais;
        SQLiteDataReader BK_Reader_Recursos_Pais;

        SQLiteCommand BK_Cmd_Personal_Grid;
        SQLiteDataReader BK_Reader_Personal_Grid;

        SQLiteCommand BK_Cmd_Recursos_Grid;
        SQLiteDataReader BK_Reader_Recursos_Grid;

        string StrAgregar_Recursos;
        string StrModificar_Recursos;
        string StrBorrar_Recursos;
        String StrBuscar_Recursos;
        readonly String StrBuscar_Recursos_Pais = "SELECT * FROM PAIS ORDER BY PAIS ASC";
        readonly string StrBuscarGrid_Personal = "SELECT RUTID||'-'||DIGVER, APELLIDOS, NOMBRES FROM PERSONAL ORDER BY APELLIDOS ASC";
        readonly string StrBuscarGrid_Recursos = "SELECT PATENTE, TIPO, MARCA, MODELO, CIUDAD FROM RECURSOS ORDER BY PATENTE ASC";
        // Campos Recursos
        public string BK_RUTID { get; set; }
        public string BK_DIGVER { get; set; }
        public string BK_PROPIETARIO { get; set; }
        public string BK_TIPO { get; set; }
        public string BK_PATENTE { get; set; }
        public string BK_MARCA { get; set; }
        public string BK_MODELO { get; set; }
        public string BK_VARIANTE { get; set; }
        public string BK_ANO { get; set; }
        public string BK_COLOR { get; set; }
        public string BK_CIUDAD { get; set; }
        public string BK_COMUNA { get; set; }
        public string BK_REGION { get; set; }
        public string BK_PAIS { get; set; }
        public string BK_OBSERVACIONES { get; set; }
        public string BK_FOTO { get; set; }

        // Campos de PAIS
        public Int16 BK_E_CODPAIS { get; set; }
        public string BK_E_PAIS { get; set; }

        // Campos Grilla Propietarios
        public string BK_GRID_RUT { get; set; }
        public string BK_GRID_APELLIDOS { get; set; }
        public string BK_GRID_NOMBRES { get; set; }

        // Campos Grilla Recursos
        public string BK_RGRID_PATENTE { get; set; }
        public string BK_RGRID_TIPO { get; set; }
        public string BK_RGRID_MARCA { get; set; }
        public string BK_RGRID_MODELO { get; set; }
        public string BK_RGRID_CIUDAD { get; set; }

        public Boolean BM_CreateDatabase(SQLiteConnection BM_Connection)
        {
            this.BM_Connection = BM_Connection;
            return true;
        }

        public bool Bm_Recursos_Agregar()
        {
            try
            {
                StrAgregar_Recursos = "INSERT INTO RECURSOS (";
                StrAgregar_Recursos += "RUTID,";
                StrAgregar_Recursos += "DIGVER,";
                StrAgregar_Recursos += "PROPIETARIO,";
                StrAgregar_Recursos += "TIPO,";
                StrAgregar_Recursos += "PATENTE,";
                StrAgregar_Recursos += "MARCA,";
                StrAgregar_Recursos += "MODELO,";
                StrAgregar_Recursos += "VARIANTE,";
                StrAgregar_Recursos += "ANO,";
                StrAgregar_Recursos += "COLOR,";
                StrAgregar_Recursos += "CIUDAD,";
                StrAgregar_Recursos += "COMUNA,";
                StrAgregar_Recursos += "REGION,";
                StrAgregar_Recursos += "PAIS,";
                StrAgregar_Recursos += "OBSERVACIONES,";
                StrAgregar_Recursos += "FOTO) VALUES (";
                StrAgregar_Recursos += "'" + BK_RUTID + "',";
                StrAgregar_Recursos += "'" + BK_DIGVER + "',";
                StrAgregar_Recursos += "'" + BK_PROPIETARIO + "',";
                StrAgregar_Recursos += "'" + BK_TIPO + "',";
                StrAgregar_Recursos += "'" + BK_PATENTE + "',";
                StrAgregar_Recursos += "'" + BK_MARCA + "',";
                StrAgregar_Recursos += "'" + BK_MODELO + "',";
                StrAgregar_Recursos += "'" + BK_VARIANTE + "',";
                StrAgregar_Recursos += "'" + BK_ANO + "',";
                StrAgregar_Recursos += "'" + BK_COLOR + "',";
                StrAgregar_Recursos += "'" + BK_CIUDAD + "',";
                StrAgregar_Recursos += "'" + BK_COMUNA + "',";
                StrAgregar_Recursos += "'" + BK_REGION + "',";
                StrAgregar_Recursos += "'" + BK_PAIS + "',";
                StrAgregar_Recursos += "'" + BK_OBSERVACIONES + "',";
                StrAgregar_Recursos += "'" + BK_FOTO + "')";
                BK_Cmd_Recursos = new SQLiteCommand(StrAgregar_Recursos, BM_Connection);
                BK_Cmd_Recursos.ExecuteNonQuery();
                return true;
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                return false;
            }
        }

        public bool Bm_Recursos_Buscar()
        {
            try
            {
                StrBuscar_Recursos = "SELECT * FROM RECURSOS";
                BK_Cmd_Recursos = new SQLiteCommand(StrBuscar_Recursos, BM_Connection);
                BK_Reader_Recursos = BK_Cmd_Recursos.ExecuteReader();

                if (BK_Reader_Recursos.Read())
                {
                    // Llenar Valores de Recursos

                    BK_RUTID = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("RUTID"));
                    BK_DIGVER = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("DIGVER"));
                    BK_PROPIETARIO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("PROPIETARIO"));
                    BK_TIPO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("TIPO"));
                    BK_PATENTE = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("PATENTE"));
                    BK_MARCA = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("MARCA"));
                    BK_MODELO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("MODELO"));
                    BK_VARIANTE = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("VARIANTE"));
                    BK_ANO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("ANO"));
                    BK_COLOR = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("COLOR"));
                    BK_CIUDAD = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("CIUDAD"));
                    BK_COMUNA = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("COMUNA"));
                    BK_REGION = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("REGION"));
                    BK_PAIS = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("PAIS"));
                    BK_OBSERVACIONES = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("OBSERVACIONES"));
                    BK_FOTO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("FOTO"));
                    return true;
                }
                else
                {
                    LimpiarVariables();
                    return false;
                }
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                return false;
            }
        }

        public bool Bm_Recursos_Buscar(string pPATENTE)
        {
            try
            {
                StrBuscar_Recursos = "SELECT * FROM RECURSOS";
                StrBuscar_Recursos += " WHERE PATENTE = '" + pPATENTE + "'";

                BK_Cmd_Recursos = new SQLiteCommand(StrBuscar_Recursos, BM_Connection);
                BK_Reader_Recursos = BK_Cmd_Recursos.ExecuteReader();

                if (BK_Reader_Recursos.Read())
                {
                    // Llenar Valores de Recursos
                    BK_RUTID = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("RUTID"));
                    BK_DIGVER = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("DIGVER"));
                    BK_PROPIETARIO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("PROPIETARIO"));
                    BK_TIPO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("TIPO"));
                    BK_PATENTE = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("PATENTE"));
                    BK_MARCA = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("MARCA"));
                    BK_MODELO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("MODELO"));
                    BK_VARIANTE = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("VARIANTE"));
                    BK_ANO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("ANO"));
                    BK_COLOR = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("COLOR"));
                    BK_CIUDAD = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("CIUDAD"));
                    BK_COMUNA = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("COMUNA"));
                    BK_REGION = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("REGION"));
                    BK_PAIS = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("PAIS"));
                    BK_OBSERVACIONES = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("OBSERVACIONES"));
                    BK_FOTO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("FOTO"));
                    return true;
                }
                else
                {
                    LimpiarVariables();
                    return false;
                }
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                return false;
            }

        }

        public bool Bm_Recursos_Buscar(string pRUTID, string pDIGVER, string pPATENTE)
        {
            try
            {
                StrBuscar_Recursos = "SELECT * FROM RECURSOS";
                StrBuscar_Recursos += " WHERE ";
                StrBuscar_Recursos += " RUTID = '" + pRUTID + "' AND ";
                StrBuscar_Recursos += " DIGVER = '" + pDIGVER + "' AND ";
                StrBuscar_Recursos += " PATENTE = '" + pPATENTE + "'";

                BK_Cmd_Recursos = new SQLiteCommand(StrBuscar_Recursos, BM_Connection);
                BK_Reader_Recursos = BK_Cmd_Recursos.ExecuteReader();

                if (BK_Reader_Recursos.Read())
                {
                    // Llenar Valores de Recursos
                    BK_RUTID = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("RUTID"));
                    BK_DIGVER = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("DIGVER"));
                    BK_PROPIETARIO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("PROPIETARIO"));
                    BK_TIPO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("TIPO"));
                    BK_PATENTE = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("PATENTE"));
                    BK_MARCA = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("MARCA"));
                    BK_MODELO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("MODELO"));
                    BK_VARIANTE = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("VARIANTE"));
                    BK_ANO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("ANO"));
                    BK_COLOR = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("COLOR"));
                    BK_CIUDAD = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("CIUDAD"));
                    BK_COMUNA = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("COMUNA"));
                    BK_REGION = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("REGION"));
                    BK_PAIS = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("PAIS"));
                    BK_OBSERVACIONES = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("OBSERVACIONES"));
                    BK_FOTO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("FOTO"));
                    return true;
                }
                else
                {
                    LimpiarVariables();
                    return false;
                }
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                return false;
            }
        }

        // Procedimiento Modificar Recursos
        public bool Bm_Recursos_Modificar(string pPATENTE)
        {
            try
            {
                StrModificar_Recursos = "UPDATE RECURSOS SET ";
                StrModificar_Recursos += "PROPIETARIO = '" + BK_PROPIETARIO + "',";
                StrModificar_Recursos += "TIPO = '" + BK_TIPO + "',";
                StrModificar_Recursos += "MARCA = '" + BK_MARCA + "',";
                StrModificar_Recursos += "MODELO = '" + BK_MODELO + "',";
                StrModificar_Recursos += "VARIANTE = '" + BK_VARIANTE + "',";
                StrModificar_Recursos += "ANO = '" + BK_ANO + "',";
                StrModificar_Recursos += "COLOR = '" + BK_COLOR + "',";
                StrModificar_Recursos += "CIUDAD = '" + BK_CIUDAD + "',";
                StrModificar_Recursos += "COMUNA = '" + BK_COMUNA + "',";
                StrModificar_Recursos += "REGION = '" + BK_REGION + "',";
                StrModificar_Recursos += "PAIS = '" + BK_PAIS + "',";
                StrModificar_Recursos += "OBSERVACIONES = '" + BK_OBSERVACIONES + "',";
                StrModificar_Recursos += "FOTO = '" + BK_FOTO + "' ";
                StrModificar_Recursos += "WHERE ";
                StrModificar_Recursos += "PATENTE = '" + pPATENTE + "'";
                BK_Cmd_Recursos = new SQLiteCommand(StrModificar_Recursos, BM_Connection);
                BK_Cmd_Recursos.ExecuteNonQuery();
                return true;
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                return false;
            }
        }

        // Procedimiento Borrar Recursos
        public bool Bm_Recursos_Borrar(string pPATENTE)
        {
            try
            {
                StrBorrar_Recursos = "DELETE FROM RECURSOS ";
                StrBorrar_Recursos += "WHERE ";
                StrBorrar_Recursos += "PATENTE = '" + pPATENTE + "'";
                BK_Cmd_Recursos = new SQLiteCommand(StrBorrar_Recursos, BM_Connection);
                BK_Cmd_Recursos.ExecuteNonQuery();
                LimpiarVariables();
                return true;
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                return false;
            }
        }

        // Procedimiento Buscar Pais
        public bool Bm_E_Pais_EjecutarSelect()
        {
            try
            {
                BK_Cmd_Recursos_Pais = new SQLiteCommand(StrBuscar_Recursos_Pais, BM_Connection);
                BK_Reader_Recursos_Pais = BK_Cmd_Recursos_Pais.ExecuteReader();
                return true;
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                return false;
            }
        }

        public bool Bm_E_Pais_Buscar()
        {
            try
            {
                if (BK_Reader_Recursos_Pais.Read())
                {
                    // Llenar Valores de Recursos
                    BK_E_CODPAIS = BK_Reader_Recursos_Pais.GetInt16(BK_Reader_Recursos_Pais.GetOrdinal("CODPAIS"));
                    BK_E_PAIS = BK_Reader_Recursos_Pais.GetString(BK_Reader_Recursos_Pais.GetOrdinal("PAIS"));
                    return true;
                }
                else
                {
                    // No existe Recursos
                    return false;
                }
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                return false;
            }
        }

        public bool Bm_Personal_BuscarGrid()
        {
            try
            {
                BK_Cmd_Personal_Grid = new SQLiteCommand(StrBuscarGrid_Personal, BM_Connection);
                BK_Reader_Personal_Grid = BK_Cmd_Personal_Grid.ExecuteReader();
                return true;
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                return false;
            }
        }

        public bool Bm_Personal_BuscarGridProxima()
        {
            try
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
            catch (System.Data.SQLite.SQLiteException)
            {
                return false;
            }
        }

        public bool Bm_Recursos_BuscarGrid()
        {
            try
            {
                BK_Cmd_Recursos_Grid = new SQLiteCommand(StrBuscarGrid_Recursos, BM_Connection);
                BK_Reader_Recursos_Grid = BK_Cmd_Recursos_Grid.ExecuteReader();
                return true;
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                return false;
            }
        }

        public bool Bm_Recursos_BuscarGridProxima()
        {
            try
            {
                if (BK_Reader_Recursos_Grid.Read())
                {
                    // Llenar Valores de la Empresa
                    BK_RGRID_PATENTE = BK_Reader_Recursos_Grid.GetString(0);
                    BK_RGRID_TIPO = BK_Reader_Recursos_Grid.GetString(1);
                    BK_RGRID_MARCA = BK_Reader_Recursos_Grid.GetString(2);
                    BK_RGRID_MODELO = BK_Reader_Recursos_Grid.GetString(3);
                    BK_RGRID_CIUDAD = BK_Reader_Recursos_Grid.GetString(4);
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

        public void LimpiarVariables()
        {
            BK_RUTID = "";
            BK_DIGVER = "";
            BK_PROPIETARIO = "";
            BK_TIPO = "";
            BK_PATENTE = "";
            BK_MARCA = "";
            BK_MODELO = "";
            BK_VARIANTE = "";
            BK_ANO = "";
            BK_COLOR = "";
            BK_CIUDAD = "";
            BK_COMUNA = "";
            BK_REGION = "";
            BK_PAIS = "";
            BK_OBSERVACIONES = "";
            BK_FOTO = "";
        }
    }
}
