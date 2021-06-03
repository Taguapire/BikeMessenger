using System;
using Microsoft.Data.Sqlite;

namespace BikeMessenger
{
    class Bm_Recurso_Database
    {
        // public SQLiteFactory BM_DB;
        public SqliteConnection BM_Connection;
        private SqliteTransaction BK_Transaccion_Recurso;
        private SqliteCommand BK_Cmd_Recursos;
        private SqliteDataReader BK_Reader_Recursos;

        private SqliteCommand BK_Cmd_Recursos_Pais;
        private SqliteDataReader BK_Reader_Recursos_Pais;

        private SqliteCommand BK_Cmd_Recursos_Region;
        private SqliteDataReader BK_Reader_Recursos_Region;

        private SqliteCommand BK_Cmd_Recursos_Comuna;
        private SqliteDataReader BK_Reader_Recursos_Comuna;

        private SqliteCommand BK_Cmd_Recursos_Ciudad;
        private SqliteDataReader BK_Reader_Recursos_Ciudad;

        private SqliteCommand BK_Cmd_Personal_Grid;
        private SqliteDataReader BK_Reader_Personal_Grid;

        private SqliteCommand BK_Cmd_Recursos_Grid;
        private SqliteDataReader BK_Reader_Recursos_Grid;

        private string StrAgregar_Recursos;
        private string StrModificar_Recursos;
        private string StrBorrar_Recursos;
        private string StrBuscar_Recursos;

        private readonly string StrBuscarGrid_Personal = "SELECT RUTID||'-'||DIGVER, APELLIDOS, NOMBRES FROM PERSONAL ORDER BY APELLIDOS ASC";
        private readonly string StrBuscarGrid_Recursos = "SELECT PATENTE, TIPO, MARCA, MODELO, CIUDAD FROM RECURSOS ORDER BY PATENTE ASC";

        private readonly string StrBuscar_Recursos_Pais = "SELECT * FROM PAIS ORDER BY PAIS ASC";
        private readonly string StrBuscar_Recursos_Region = "SELECT * FROM ESTADOREGION ORDER BY REGION ASC";
        private readonly string StrBuscar_Recursos_Comuna = "SELECT * FROM COMUNA ORDER BY COMUNA ASC";
        private readonly string StrBuscar_Recursos_Ciudad = "SELECT * FROM CIUDAD ORDER BY CIUDAD ASC";

        // Campos Recursos
        public string BK_PENTALPHA { get; set; }
        public string BK_PATENTE { get; set; }
        public string BK_RUTID { get; set; }
        public string BK_DIGVER { get; set; }
        public string BK_PROPIETARIO { get; set; }
        public string BK_TIPO { get; set; }
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
        public short BK_E_CODPAIS { get; set; }
        public string BK_E_PAIS { get; set; }

        // Campos de REGION
        public short BK_E_CODREGION { get; set; }
        public string BK_E_REGION { get; set; }

        // Campos de COMUNA
        public short BK_E_CODCOMUNA { get; set; }
        public string BK_E_COMUNA { get; set; }

        // Campos de CIUDAD
        public short BK_E_CODCIUDAD { get; set; }
        public string BK_E_CIUDAD { get; set; }

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

        public Boolean BM_CreateDatabase(SqliteConnection BM_Connection)
        {
            this.BM_Connection = BM_Connection;
            return true;
        }

        public bool Bm_Recursos_Agregar()
        {
            try
            {
                StrAgregar_Recursos = "INSERT INTO RECURSOS (";
                StrAgregar_Recursos += "PENTALPHA,";
                StrAgregar_Recursos += "PATENTE,";
                StrAgregar_Recursos += "RUTID,";
                StrAgregar_Recursos += "DIGVER,";
                StrAgregar_Recursos += "PROPIETARIO,";
                StrAgregar_Recursos += "TIPO,";
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
                StrAgregar_Recursos += "'" + BK_PENTALPHA + "',";
                StrAgregar_Recursos += "'" + BK_PATENTE + "',";
                StrAgregar_Recursos += "'" + BK_RUTID + "',";
                StrAgregar_Recursos += "'" + BK_DIGVER + "',";
                StrAgregar_Recursos += "'" + BK_PROPIETARIO + "',";
                StrAgregar_Recursos += "'" + BK_TIPO + "',";
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
                BK_Cmd_Recursos = new SqliteCommand(StrAgregar_Recursos, BM_Connection);
                _ = BK_Cmd_Recursos.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_Recursos_Buscar()
        {
            try
            {
                StrBuscar_Recursos = "SELECT * FROM RECURSOS";
                BK_Cmd_Recursos = new SqliteCommand(StrBuscar_Recursos, BM_Connection);
                BK_Reader_Recursos = BK_Cmd_Recursos.ExecuteReader();

                if (BK_Reader_Recursos.Read())
                {
                    // Llenar Valores de Recursos
                    BK_PATENTE = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("PATENTE"));
                    BK_RUTID = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("RUTID"));
                    BK_DIGVER = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("DIGVER"));
                    BK_PROPIETARIO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("PROPIETARIO"));
                    BK_TIPO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("TIPO"));
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
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_Recursos_Buscar(string pPENTALPHA, string pPATENTE)
        {
            try
            {
                StrBuscar_Recursos = "SELECT * FROM RECURSOS";
                StrBuscar_Recursos += " WHERE PENTALPHA = '" + BK_PENTALPHA + " AND PATENTE = '" + pPATENTE + "'";

                BK_Cmd_Recursos = new SqliteCommand(StrBuscar_Recursos, BM_Connection);
                BK_Reader_Recursos = BK_Cmd_Recursos.ExecuteReader();

                if (BK_Reader_Recursos.Read())
                {
                    // Llenar Valores de Recursos
                    BK_PATENTE = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("PATENTE"));
                    BK_RUTID = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("RUTID"));
                    BK_DIGVER = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("DIGVER"));
                    BK_PROPIETARIO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("PROPIETARIO"));
                    BK_TIPO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("TIPO"));
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
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_Recursos_Buscar(string pPENTALPHA, string pRUTID, string pDIGVER, string pPATENTE)
        {
            try
            {
                StrBuscar_Recursos = "SELECT * FROM RECURSOS";
                StrBuscar_Recursos += " WHERE ";
                StrBuscar_Recursos += " PENTALPHA = '" + pPENTALPHA + "' AND ";
                StrBuscar_Recursos += " RUTID = '" + pRUTID + "' AND ";
                StrBuscar_Recursos += " DIGVER = '" + pDIGVER + "' AND ";
                StrBuscar_Recursos += " PATENTE = '" + pPATENTE + "'";

                BK_Cmd_Recursos = new SqliteCommand(StrBuscar_Recursos, BM_Connection);
                BK_Reader_Recursos = BK_Cmd_Recursos.ExecuteReader();

                if (BK_Reader_Recursos.Read())
                {
                    // Llenar Valores de Recursos
                    BK_PATENTE = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("PATENTE"));
                    BK_RUTID = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("RUTID"));
                    BK_DIGVER = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("DIGVER"));
                    BK_PROPIETARIO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("PROPIETARIO"));
                    BK_TIPO = BK_Reader_Recursos.GetString(BK_Reader_Recursos.GetOrdinal("TIPO"));
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
            catch (SqliteException)
            {
                return false;
            }
        }

        // Procedimiento Modificar Recursos
        public bool Bm_Recursos_Modificar(string pPENTALPHA, string pPATENTE)
        {
            try
            {
                StrModificar_Recursos = "UPDATE RECURSOS SET ";
                StrModificar_Recursos += "RUTID = '" + BK_RUTID + "',";
                StrModificar_Recursos += "DIGVER = '" + BK_DIGVER+ "',";
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
                StrModificar_Recursos += "PENTALPHA = '" + pPENTALPHA + "' AND ";
                StrModificar_Recursos += "PATENTE = '" + pPATENTE + "'";
                BK_Cmd_Recursos = new SqliteCommand(StrModificar_Recursos, BM_Connection);
                _ = BK_Cmd_Recursos.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        // Procedimiento Borrar Recursos
        public bool Bm_Recursos_Borrar(string pPENTALPHA, string pPATENTE)
        {
            try
            {
                StrBorrar_Recursos = "DELETE FROM RECURSOS ";
                StrBorrar_Recursos += "WHERE ";
                StrBorrar_Recursos += "PENTALPHA = '" + pPENTALPHA + "'";
                StrBorrar_Recursos += "PATENTE = '" + pPATENTE + "'";
                BK_Cmd_Recursos = new SqliteCommand(StrBorrar_Recursos, BM_Connection);
                _ = BK_Cmd_Recursos.ExecuteNonQuery();
                LimpiarVariables();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_Iniciar_Transaccion()
        {
            try
            {
                BK_Transaccion_Recurso = BM_Connection.BeginTransaction();
                return true;
            }
            catch (SqliteException)
            {
                BK_Transaccion_Recurso.Dispose();
                return false;
            }
        }

        public object Bm_Commit_Transaccion()
        {
            try
            {
                BK_Transaccion_Recurso.Commit();
                return true;
            }
            catch (SqliteException)
            {
                BK_Transaccion_Recurso.Dispose();
                return false;
            }
        }

        public bool Bm_Rollback_Transaccion()
        {
            try
            {
                BK_Transaccion_Recurso.Rollback();
                return true;
            }
            catch (SqliteException)
            {
                BK_Transaccion_Recurso.Dispose();
                return false;
            }
        }

        // Procedimiento Buscar Pais
        public bool Bm_E_Pais_EjecutarSelect()
        {
            try
            {
                BK_Cmd_Recursos_Pais = new SqliteCommand(StrBuscar_Recursos_Pais, BM_Connection);
                BK_Reader_Recursos_Pais = BK_Cmd_Recursos_Pais.ExecuteReader();
                return true;
            }
            catch (SqliteException)
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
                    BK_E_CODPAIS = BK_Reader_Recursos_Pais.GetInt16(BK_Reader_Recursos_Pais.GetOrdinal("CODPAIS"));
                    BK_E_PAIS = BK_Reader_Recursos_Pais.GetString(BK_Reader_Recursos_Pais.GetOrdinal("PAIS"));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        // Procedimiento Buscar Region
        public bool Bm_E_Region_EjecutarSelect()
        {
            try
            {
                BK_Cmd_Recursos_Region = new SqliteCommand(StrBuscar_Recursos_Region, BM_Connection);
                BK_Reader_Recursos_Region = BK_Cmd_Recursos_Region.ExecuteReader();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_E_Region_Buscar()
        {
            try
            {
                if (BK_Reader_Recursos_Region.Read())
                {
                    BK_E_CODREGION = BK_Reader_Recursos_Region.GetInt16(BK_Reader_Recursos_Region.GetOrdinal("CODREGION"));
                    BK_E_REGION = BK_Reader_Recursos_Region.GetString(BK_Reader_Recursos_Region.GetOrdinal("REGION"));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqliteException)
            {
                return false;
            }
        }


        // Procedimiento Buscar Comuna
        public bool Bm_E_Comuna_EjecutarSelect()
        {
            try
            {
                BK_Cmd_Recursos_Comuna = new SqliteCommand(StrBuscar_Recursos_Comuna, BM_Connection);
                BK_Reader_Recursos_Comuna = BK_Cmd_Recursos_Comuna.ExecuteReader();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_E_Comuna_Buscar()
        {
            try
            {
                if (BK_Reader_Recursos_Comuna.Read())
                {
                    BK_E_CODCOMUNA = BK_Reader_Recursos_Comuna.GetInt16(BK_Reader_Recursos_Comuna.GetOrdinal("CODCOMU"));
                    BK_E_COMUNA = BK_Reader_Recursos_Comuna.GetString(BK_Reader_Recursos_Comuna.GetOrdinal("COMUNA"));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        // Procedimiento Buscar Ciudad
        public bool Bm_E_Ciudad_EjecutarSelect()
        {
            try
            {
                BK_Cmd_Recursos_Ciudad = new SqliteCommand(StrBuscar_Recursos_Ciudad, BM_Connection);
                BK_Reader_Recursos_Ciudad = BK_Cmd_Recursos_Ciudad.ExecuteReader();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_E_Ciudad_Buscar()
        {
            try
            {
                if (BK_Reader_Recursos_Ciudad.Read())
                {
                    BK_E_CODCIUDAD = BK_Reader_Recursos_Ciudad.GetInt16(BK_Reader_Recursos_Ciudad.GetOrdinal("CODCIUDAD"));
                    BK_E_CIUDAD = BK_Reader_Recursos_Ciudad.GetString(BK_Reader_Recursos_Ciudad.GetOrdinal("CIUDAD"));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_Personal_BuscarGrid()
        {
            try
            {
                BK_Cmd_Personal_Grid = new SqliteCommand(StrBuscarGrid_Personal, BM_Connection);
                BK_Reader_Personal_Grid = BK_Cmd_Personal_Grid.ExecuteReader();
                return true;
            }
            catch (SqliteException)
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
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_Recursos_BuscarGrid()
        {
            try
            {
                BK_Cmd_Recursos_Grid = new SqliteCommand(StrBuscarGrid_Recursos, BM_Connection);
                BK_Reader_Recursos_Grid = BK_Cmd_Recursos_Grid.ExecuteReader();
                return true;
            }
            catch (SqliteException)
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
            catch (SqliteException)
            {
                return false;
            }
        }

        public void LimpiarVariables()
        {
            BK_PENTALPHA = "";
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
