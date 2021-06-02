using System;
using Microsoft.Data.Sqlite;

namespace BikeMessenger
{
    internal class Bm_Cliente_Database
    {
        // public SQLiteFactory BM_DB;
        public SqliteConnection BM_Connection;
        private SqliteCommand BK_Cmd_Clientes;
        private SqliteDataReader BK_Reader_Clientes;

        private SqliteCommand BK_Cmd_Clientes_Pais;
        private SqliteDataReader BK_Reader_Clientes_Pais;

        private SqliteCommand BK_Cmd_Clientes_Region;
        private SqliteDataReader BK_Reader_Clientes_Region;

        private SqliteCommand BK_Cmd_Clientes_Comuna;
        private SqliteDataReader BK_Reader_Clientes_Comuna;

        private SqliteCommand BK_Cmd_Clientes_Ciudad;
        private SqliteDataReader BK_Reader_Clientes_Ciudad;

        private SqliteCommand BK_Cmd_Clientes_Grid;
        private SqliteDataReader BK_Reader_Clientes_Grid;

        private string StrAgregar_Clientes;
        private string StrModificar_Clientes;
        private string StrBorrar_Clientes;

        private readonly string StrBuscarGrid_Clientes = "SELECT RUTID||'-'||DIGVER, NOMBRE FROM CLIENTES ORDER BY NOMBRE ASC";
        private readonly string StrBuscar_Clientes = "SELECT * FROM CLIENTES";

        private readonly string StrBuscar_Clientes_Pais = "SELECT * FROM PAIS ORDER BY PAIS ASC";
        private readonly string StrBuscar_Clientes_Region = "SELECT * FROM ESTADOREGION ORDER BY REGION ASC";
        private readonly string StrBuscar_Clientes_Comuna = "SELECT * FROM COMUNA ORDER BY COMUNA ASC";
        private readonly string StrBuscar_Clientes_Ciudad = "SELECT * FROM CIUDAD ORDER BY CIUDAD ASC";


        public string BK_PENTALPHA { get; set; }
        public string BK_RUTID { get; set; }
        public string BK_DIGVER { get; set; }
        public string BK_NOMBRE { get; set; }
        public string BK_USUARIO { get; set; }
        public string BK_CLAVE { get; set; }
        public string BK_ACTIVIDAD1 { get; set; }
        public string BK_ACTIVIDAD2 { get; set; }
        public string BK_REPRESENTANTE1 { get; set; }
        public string BK_REPRESENTANTE2 { get; set; }
        public string BK_REPRESENTANTE3 { get; set; }
        public string BK_TELEFONO1 { get; set; }
        public string BK_TELEFONO2 { get; set; }
        public string BK_DOMICILIO1 { get; set; }
        public string BK_DOMICILIO2 { get; set; }
        public string BK_NUMERO { get; set; }
        public string BK_PISO { get; set; }
        public string BK_OFICINA { get; set; }
        public string BK_CODIGOPOSTAL { get; set; }
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

        // LISTADO CLIENTES
        public string BK_GRID_RUT { get; set; }
        public string BK_GRID_NOMBRES { get; set; }

        public bool BM_CreateDatabase(SqliteConnection BM_Connection)
        {
            this.BM_Connection = BM_Connection;
            try
            {
                //  BM_DB = new SQLiteFactory();
                // Crear Automaticamente la Base de Datos
                // BM_Connection = (SqliteConnection)BM_DB.CreateConnection();

                // BM_Connection.ConnectionString = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\BikeMessenger.db; PRAGMA journal_mode = WAL; Version = 3; New = True; Compress = True; Connection Timeout=0";

                // BM_Connection.Open();

                // Verificar que Base es nueva o ya esta creada con objetos
                // Db_Empresa = new Bm_Empresa_Database(BM_Connection);

                // Si es nueva deben crearse los objetos

                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_Clientes_Agregar()
        {
            StrAgregar_Clientes = "INSERT INTO CLIENTES (";
            StrAgregar_Clientes += "PENTALPHA,";
            StrAgregar_Clientes += "RUTID,";
            StrAgregar_Clientes += "DIGVER,";
            StrAgregar_Clientes += "NOMBRE,";
            StrAgregar_Clientes += "USUARIO,";
            StrAgregar_Clientes += "CLAVE,";
            StrAgregar_Clientes += "ACTIVIDAD1,";
            StrAgregar_Clientes += "ACTIVIDAD2,";
            StrAgregar_Clientes += "REPRESENTANTE1,";
            StrAgregar_Clientes += "REPRESENTANTE2,";
            StrAgregar_Clientes += "REPRESENTANTE3,";
            StrAgregar_Clientes += "TELEFONO1,";
            StrAgregar_Clientes += "TELEFONO2,";
            StrAgregar_Clientes += "DOMICILIO1,";
            StrAgregar_Clientes += "DOMICILIO2,";
            StrAgregar_Clientes += "NUMERO,";
            StrAgregar_Clientes += "PISO,";
            StrAgregar_Clientes += "OFICINA,";
            StrAgregar_Clientes += "CODIGOPOSTAL,";
            StrAgregar_Clientes += "CIUDAD,";
            StrAgregar_Clientes += "COMUNA,";
            StrAgregar_Clientes += "REGION,";
            StrAgregar_Clientes += "PAIS,";
            StrAgregar_Clientes += "OBSERVACIONES,";
            StrAgregar_Clientes += "FOTO) VALUES (";
            StrAgregar_Clientes += "'" + BK_PENTALPHA + "',";
            StrAgregar_Clientes += "'" + BK_RUTID + "',";
            StrAgregar_Clientes += "'" + BK_DIGVER + "',";
            StrAgregar_Clientes += "'" + BK_NOMBRE + "',";
            StrAgregar_Clientes += "'" + BK_USUARIO + "',";
            StrAgregar_Clientes += "'" + BK_CLAVE + "',";
            StrAgregar_Clientes += "'" + BK_ACTIVIDAD1 + "',";
            StrAgregar_Clientes += "'" + BK_ACTIVIDAD2 + "',";
            StrAgregar_Clientes += "'" + BK_REPRESENTANTE1 + "',";
            StrAgregar_Clientes += "'" + BK_REPRESENTANTE2 + "',";
            StrAgregar_Clientes += "'" + BK_REPRESENTANTE3 + "',";
            StrAgregar_Clientes += "'" + BK_TELEFONO1 + "',";
            StrAgregar_Clientes += "'" + BK_TELEFONO2 + "',";
            StrAgregar_Clientes += "'" + BK_DOMICILIO1 + "',";
            StrAgregar_Clientes += "'" + BK_DOMICILIO2 + "',";
            StrAgregar_Clientes += "'" + BK_NUMERO + "',";
            StrAgregar_Clientes += "'" + BK_PISO + "',";
            StrAgregar_Clientes += "'" + BK_OFICINA + "',";
            StrAgregar_Clientes += "'" + BK_CODIGOPOSTAL + "',";
            StrAgregar_Clientes += "'" + BK_CIUDAD + "',";
            StrAgregar_Clientes += "'" + BK_COMUNA + "',";
            StrAgregar_Clientes += "'" + BK_REGION + "',";
            StrAgregar_Clientes += "'" + BK_PAIS + "',";
            StrAgregar_Clientes += "'" + BK_OBSERVACIONES + "',";
            StrAgregar_Clientes += "'" + BK_FOTO + "')";
            try
            {
                BK_Cmd_Clientes = new SqliteCommand(StrAgregar_Clientes, BM_Connection);
                _ = BK_Cmd_Clientes.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_Clientes_Buscar()
        {
            try
            {
                BK_Cmd_Clientes = new SqliteCommand(StrBuscar_Clientes, BM_Connection);
                BK_Reader_Clientes = BK_Cmd_Clientes.ExecuteReader();

                if (BK_Reader_Clientes.Read())
                {
                    // Llenar Valores de Cliente
                    BK_PENTALPHA = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("PENTALPHA"));
                    BK_RUTID = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("RUTID"));
                    BK_DIGVER = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("DIGVER"));
                    BK_NOMBRE = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("NOMBRE"));
                    BK_USUARIO = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("USUARIO"));
                    BK_CLAVE = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("CLAVE"));
                    BK_ACTIVIDAD1 = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("ACTIVIDAD1"));
                    BK_ACTIVIDAD2 = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("ACTIVIDAD2"));
                    BK_REPRESENTANTE1 = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("REPRESENTANTE1"));
                    BK_REPRESENTANTE2 = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("REPRESENTANTE2"));
                    BK_REPRESENTANTE3 = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("REPRESENTANTE3"));
                    BK_TELEFONO1 = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("TELEFONO1"));
                    BK_TELEFONO2 = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("TELEFONO2"));
                    BK_DOMICILIO1 = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("DOMICILIO1"));
                    BK_DOMICILIO2 = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("DOMICILIO2"));
                    BK_NUMERO = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("NUMERO"));
                    BK_PISO = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("PISO"));
                    BK_OFICINA = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("OFICINA"));
                    BK_CODIGOPOSTAL = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("CODIGOPOSTAL"));
                    BK_CIUDAD = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("CIUDAD"));
                    BK_COMUNA = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("COMUNA"));
                    BK_REGION = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("REGION"));
                    BK_PAIS = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("PAIS"));
                    BK_OBSERVACIONES = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("OBSERVACIONES"));
                    BK_FOTO = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("FOTO"));
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


        public bool Bm_Clientes_Buscar(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            try
            {
                BK_Cmd_Clientes = new SqliteCommand(StrBuscar_Clientes + " WHERE PENTALPHA = '" + pPENTALPHA + "' AND RUTID = '" + pRUTID + "' AND DIGVER = '" + pDIGVER + "'", BM_Connection);
                BK_Reader_Clientes = BK_Cmd_Clientes.ExecuteReader();

                if (BK_Reader_Clientes.Read())
                {
                    // Llenar Valores de Cliente
                    BK_PENTALPHA = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("PENTALPHA"));
                    BK_RUTID = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("RUTID"));
                    BK_DIGVER = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("DIGVER"));
                    BK_NOMBRE = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("NOMBRE"));
                    BK_USUARIO = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("USUARIO"));
                    BK_CLAVE = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("CLAVE"));
                    BK_ACTIVIDAD1 = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("ACTIVIDAD1"));
                    BK_ACTIVIDAD2 = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("ACTIVIDAD2"));
                    BK_REPRESENTANTE1 = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("REPRESENTANTE1"));
                    BK_REPRESENTANTE2 = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("REPRESENTANTE2"));
                    BK_REPRESENTANTE3 = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("REPRESENTANTE3"));
                    BK_TELEFONO1 = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("TELEFONO1"));
                    BK_TELEFONO2 = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("TELEFONO2"));
                    BK_DOMICILIO1 = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("DOMICILIO1"));
                    BK_DOMICILIO2 = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("DOMICILIO2"));
                    BK_NUMERO = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("NUMERO"));
                    BK_PISO = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("PISO"));
                    BK_OFICINA = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("OFICINA"));
                    BK_CODIGOPOSTAL = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("CODIGOPOSTAL"));
                    BK_CIUDAD = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("CIUDAD"));
                    BK_COMUNA = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("COMUNA"));
                    BK_REGION = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("REGION"));
                    BK_PAIS = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("PAIS"));
                    BK_OBSERVACIONES = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("OBSERVACIONES"));
                    BK_FOTO = BK_Reader_Clientes.GetString(BK_Reader_Clientes.GetOrdinal("FOTO"));
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

        // Procedimiento Modificar Clientes
        public bool Bm_Clientes_Modificar(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            StrModificar_Clientes = "UPDATE CLIENTES SET ";
            StrModificar_Clientes += "NOMBRE = '" + BK_NOMBRE + "',";
            StrModificar_Clientes += "USUARIO = '" + BK_USUARIO + "',";
            StrModificar_Clientes += "CLAVE = '" + BK_CLAVE + "',";
            StrModificar_Clientes += "ACTIVIDAD1 = '" + BK_ACTIVIDAD1 + "',";
            StrModificar_Clientes += "ACTIVIDAD2 = '" + BK_ACTIVIDAD2 + "',";
            StrModificar_Clientes += "REPRESENTANTE1 = '" + BK_REPRESENTANTE1 + "',";
            StrModificar_Clientes += "REPRESENTANTE2 = '" + BK_REPRESENTANTE2 + "',";
            StrModificar_Clientes += "REPRESENTANTE3 = '" + BK_REPRESENTANTE3 + "',";
            StrModificar_Clientes += "TELEFONO1 = '" + BK_TELEFONO1 + "',";
            StrModificar_Clientes += "TELEFONO2 = '" + BK_TELEFONO2 + "',";
            StrModificar_Clientes += "DOMICILIO1 = '" + BK_DOMICILIO1 + "',";
            StrModificar_Clientes += "DOMICILIO2 = '" + BK_DOMICILIO2 + "',";
            StrModificar_Clientes += "NUMERO = '" + BK_NUMERO + "',";
            StrModificar_Clientes += "PISO = '" + BK_PISO + "',";
            StrModificar_Clientes += "OFICINA = '" + BK_OFICINA + "',";
            StrModificar_Clientes += "CODIGOPOSTAL = '" + BK_CODIGOPOSTAL + "',";
            StrModificar_Clientes += "CIUDAD = '" + BK_CIUDAD + "',";
            StrModificar_Clientes += "COMUNA = '" + BK_COMUNA + "',";
            StrModificar_Clientes += "REGION = '" + BK_REGION + "',";
            StrModificar_Clientes += "PAIS = '" + BK_PAIS + "',";
            StrModificar_Clientes += "OBSERVACIONES = '" + BK_OBSERVACIONES + "',";
            StrModificar_Clientes += "FOTO = '" + BK_FOTO + "'";
            StrModificar_Clientes += "WHERE ";
            StrModificar_Clientes += "PENTALPHA = '" + pPENTALPHA + "' AND ";
            StrModificar_Clientes += "RUTID = '" + pRUTID + "' AND ";
            StrModificar_Clientes += "DIGVER = '" + pDIGVER + "'";
            try
            {
                BK_Cmd_Clientes = new SqliteCommand(StrModificar_Clientes, BM_Connection);
                BK_Cmd_Clientes.ExecuteNonQuery();
                return true;
            }
            catch (Microsoft.Data.Sqlite.SqliteException)
            {
                return false;
            }
        }

        // Procedimiento Borrar Clientes
        public bool Bm_Clientes_Borrar(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            try
            {
                StrBorrar_Clientes = "DELETE FROM CLIENTES ";
                StrBorrar_Clientes += "WHERE ";
                StrBorrar_Clientes += "PENTALPHA = '" + pPENTALPHA + "' AND ";
                StrBorrar_Clientes += "RUTID = '" + pRUTID + "' AND ";
                StrBorrar_Clientes += "DIGVER = '" + pDIGVER + "'";
                BK_Cmd_Clientes = new SqliteCommand(StrBorrar_Clientes, BM_Connection);
                _ = BK_Cmd_Clientes.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        // Procedimiento Buscar Pais
        public bool Bm_E_Pais_EjecutarSelect()
        {
            try
            {
                BK_Cmd_Clientes_Pais = new SqliteCommand(StrBuscar_Clientes_Pais, BM_Connection);
                BK_Reader_Clientes_Pais = BK_Cmd_Clientes_Pais.ExecuteReader();
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
                if (BK_Reader_Clientes_Pais.Read())
                {
                    BK_E_CODPAIS = BK_Reader_Clientes_Pais.GetInt16(BK_Reader_Clientes_Pais.GetOrdinal("CODPAIS"));
                    BK_E_PAIS = BK_Reader_Clientes_Pais.GetString(BK_Reader_Clientes_Pais.GetOrdinal("PAIS"));
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
                BK_Cmd_Clientes_Region = new SqliteCommand(StrBuscar_Clientes_Region, BM_Connection);
                BK_Reader_Clientes_Region = BK_Cmd_Clientes_Region.ExecuteReader();
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
                if (BK_Reader_Clientes_Region.Read())
                {
                    BK_E_CODREGION = BK_Reader_Clientes_Region.GetInt16(BK_Reader_Clientes_Region.GetOrdinal("CODREGION"));
                    BK_E_REGION = BK_Reader_Clientes_Region.GetString(BK_Reader_Clientes_Region.GetOrdinal("REGION"));
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
                BK_Cmd_Clientes_Comuna = new SqliteCommand(StrBuscar_Clientes_Comuna, BM_Connection);
                BK_Reader_Clientes_Comuna = BK_Cmd_Clientes_Comuna.ExecuteReader();
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
                if (BK_Reader_Clientes_Comuna.Read())
                {
                    BK_E_CODCOMUNA = BK_Reader_Clientes_Comuna.GetInt16(BK_Reader_Clientes_Comuna.GetOrdinal("CODCOMU"));
                    BK_E_COMUNA = BK_Reader_Clientes_Comuna.GetString(BK_Reader_Clientes_Comuna.GetOrdinal("COMUNA"));
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
                BK_Cmd_Clientes_Ciudad = new SqliteCommand(StrBuscar_Clientes_Ciudad, BM_Connection);
                BK_Reader_Clientes_Ciudad = BK_Cmd_Clientes_Ciudad.ExecuteReader();
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
                if (BK_Reader_Clientes_Ciudad.Read())
                {
                    BK_E_CODCIUDAD = BK_Reader_Clientes_Ciudad.GetInt16(BK_Reader_Clientes_Ciudad.GetOrdinal("CODCIUDAD"));
                    BK_E_CIUDAD = BK_Reader_Clientes_Ciudad.GetString(BK_Reader_Clientes_Ciudad.GetOrdinal("CIUDAD"));
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


        // Buscar Clientes
        public bool Bm_Clientes_BuscarGrid()
        {
            try
            {
                BK_Cmd_Clientes_Grid = new SqliteCommand(StrBuscarGrid_Clientes, BM_Connection);
                BK_Reader_Clientes_Grid = BK_Cmd_Clientes_Grid.ExecuteReader();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_Clientes_BuscarGridProxima()
        {
            try
            {
                if (BK_Reader_Clientes_Grid.Read())
                {
                    // Llenar Valores de la Empresa
                    BK_GRID_RUT = BK_Reader_Clientes_Grid.GetString(0);
                    BK_GRID_NOMBRES = BK_Reader_Clientes_Grid.GetString(1);
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
            BK_NOMBRE = "";
            BK_ACTIVIDAD1 = "";
            BK_ACTIVIDAD2 = "";
            BK_REPRESENTANTE1 = "";
            BK_REPRESENTANTE2 = "";
            BK_REPRESENTANTE3 = "";
            BK_TELEFONO1 = "";
            BK_TELEFONO2 = "";
            BK_DOMICILIO1 = "";
            BK_DOMICILIO2 = "";
            BK_NUMERO = "";
            BK_PISO = "";
            BK_OFICINA = "";
            BK_CODIGOPOSTAL = "";
            BK_CIUDAD = "";
            BK_COMUNA = "";
            BK_REGION = "";
            BK_PAIS = "";
            BK_OBSERVACIONES = "";
            BK_FOTO = "";
        }
    }
}
