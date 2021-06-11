using System;
using Microsoft.Data.Sqlite;

namespace BikeMessenger
{
    internal class Bm_Empresa_Database
    {
        // public SQLiteFactory BM_DB;
        public SqliteConnection BM_Connection;
        private SqliteCommand BK_Cmd_Empresa;
        private SqliteTransaction BK_Transaccion_Empresa;

        private SqliteCommand BK_Cmd_Empresa_Pais;
        private SqliteDataReader BK_Reader_Empresa_Pais;

        private SqliteCommand BK_Cmd_Empresa_Region;
        private SqliteDataReader BK_Reader_Empresa_Region;

        private SqliteCommand BK_Cmd_Empresa_Comuna;
        private SqliteDataReader BK_Reader_Empresa_Comuna;

        private SqliteCommand BK_Cmd_Empresa_Ciudad;
        private SqliteDataReader BK_Reader_Empresa_Ciudad;

        private SqliteDataReader BK_Reader_Empresa;
        private readonly string StrBuscar_Empresa = "SELECT * FROM EMPRESA";

        private readonly string StrBuscar_Empresa_Pais = "SELECT * FROM PAIS ORDER BY PAIS ASC";
        private readonly string StrBuscar_Empresa_Region = "SELECT * FROM ESTADOREGION ORDER BY REGION ASC";
        private readonly string StrBuscar_Empresa_Comuna = "SELECT * FROM COMUNA ORDER BY COMUNA ASC";
        private readonly string StrBuscar_Empresa_Ciudad = "SELECT * FROM CIUDAD ORDER BY CIUDAD ASC";

        private string StrAgregar_Empresa;
        private string StrModificar_Empresa;
        private string StrBorrar_Empresa;

        // Campos de Empresa
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
        public string BK_TELEFONO1 { get; set; }
        public string BK_TELEFONO2 { get; set; }
        public string BK_TELEFONO3 { get; set; }
        public string BK_OBSERVACIONES { get; set; }
        public string BK_LOGO { get; set; }

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

        // Comandos de acceso por Area

        public bool BM_CreateDatabase(SqliteConnection BM_Connection)
        {
            this.BM_Connection = BM_Connection;
            return true;
        }

        // Procedimiento Buscar Empresa
        public bool Bm_Empresa_Buscar()
        {
            try
            {
                BK_Cmd_Empresa = new SqliteCommand(StrBuscar_Empresa, BM_Connection);
                BK_Reader_Empresa = BK_Cmd_Empresa.ExecuteReader();

                if (BK_Reader_Empresa.Read())
                {
                    // Llenar Valores de la Empresa
                    BK_PENTALPHA = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("PENTALPHA"));
                    BK_RUTID = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("RUTID"));
                    BK_DIGVER = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("DIGVER"));
                    BK_NOMBRE = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("NOMBRE"));
                    BK_USUARIO = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("USUARIO"));
                    BK_CLAVE = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("CLAVE"));
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
                    BK_TELEFONO1 = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("TELEFONO1"));
                    BK_TELEFONO2 = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("TELEFONO2"));
                    BK_TELEFONO3 = BK_Reader_Empresa.GetString(BK_Reader_Empresa.GetOrdinal("TELEFONO3"));
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
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_Iniciar_Transaccion()
        {
            try
            {
                BK_Transaccion_Empresa = BM_Connection.BeginTransaction();
                return true;
            }
            catch (SqliteException)
            {
                BK_Transaccion_Empresa.Dispose();
                return false;
            }
        }

        public object Bm_Commit_Transaccion()
        {
            try
            {
                BK_Transaccion_Empresa.Commit();
                return true;
            }
            catch (SqliteException)
            {
                BK_Transaccion_Empresa.Dispose();
                return false;
            }
        }

        public bool Bm_Rollback_Transaccion()
        {
            try
            {
                BK_Transaccion_Empresa.Rollback();
                return true;
            }
            catch (SqliteException)
            {
                BK_Transaccion_Empresa.Dispose();
                return false;
            }
        }

        // Procedimiento Buscar Pais
        public bool Bm_E_Pais_EjecutarSelect()
        {
            try
            {
                BK_Cmd_Empresa_Pais = new SqliteCommand(StrBuscar_Empresa_Pais, BM_Connection);
                BK_Reader_Empresa_Pais = BK_Cmd_Empresa_Pais.ExecuteReader();
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
                if (BK_Reader_Empresa_Pais.Read())
                {
                    BK_E_CODPAIS = BK_Reader_Empresa_Pais.GetInt16(BK_Reader_Empresa_Pais.GetOrdinal("CODPAIS"));
                    BK_E_PAIS = BK_Reader_Empresa_Pais.GetString(BK_Reader_Empresa_Pais.GetOrdinal("PAIS"));
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
                BK_Cmd_Empresa_Region = new SqliteCommand(StrBuscar_Empresa_Region, BM_Connection);
                BK_Reader_Empresa_Region = BK_Cmd_Empresa_Region.ExecuteReader();
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
                if (BK_Reader_Empresa_Region.Read())
                {
                    BK_E_CODREGION = BK_Reader_Empresa_Region.GetInt16(BK_Reader_Empresa_Region.GetOrdinal("CODREGION"));
                    BK_E_REGION = BK_Reader_Empresa_Region.GetString(BK_Reader_Empresa_Region.GetOrdinal("REGION"));
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
                BK_Cmd_Empresa_Comuna = new SqliteCommand(StrBuscar_Empresa_Comuna, BM_Connection);
                BK_Reader_Empresa_Comuna = BK_Cmd_Empresa_Comuna.ExecuteReader();
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
                if (BK_Reader_Empresa_Comuna.Read())
                {
                    BK_E_CODCOMUNA = BK_Reader_Empresa_Comuna.GetInt16(BK_Reader_Empresa_Comuna.GetOrdinal("CODCOMU"));
                    BK_E_COMUNA = BK_Reader_Empresa_Comuna.GetString(BK_Reader_Empresa_Comuna.GetOrdinal("COMUNA"));
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
                BK_Cmd_Empresa_Ciudad = new SqliteCommand(StrBuscar_Empresa_Ciudad, BM_Connection);
                BK_Reader_Empresa_Ciudad = BK_Cmd_Empresa_Ciudad.ExecuteReader();
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
                if (BK_Reader_Empresa_Ciudad.Read())
                {
                    BK_E_CODCIUDAD = BK_Reader_Empresa_Ciudad.GetInt16(BK_Reader_Empresa_Ciudad.GetOrdinal("CODCIUDAD"));
                    BK_E_CIUDAD = BK_Reader_Empresa_Ciudad.GetString(BK_Reader_Empresa_Ciudad.GetOrdinal("CIUDAD"));
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

        // Procedimiento Insertar Empresa

        public bool Bm_Empresa_Agregar()
        {
            StrAgregar_Empresa = "INSERT INTO EMPRESA (";
            StrAgregar_Empresa += "PENTALPHA,";
            StrAgregar_Empresa += "RUTID,";
            StrAgregar_Empresa += "DIGVER,";
            StrAgregar_Empresa += "NOMBRE,";
            StrAgregar_Empresa += "USUARIO,";
            StrAgregar_Empresa += "CLAVE,";
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
            StrAgregar_Empresa += "TELEFONO1,";
            StrAgregar_Empresa += "TELEFONO2,";
            StrAgregar_Empresa += "TELEFONO3,";
            StrAgregar_Empresa += "OBSERVACIONES,";
            StrAgregar_Empresa += "LOGO) VALUES (";
            StrAgregar_Empresa += "'" + BK_PENTALPHA + "',";
            StrAgregar_Empresa += "'" + BK_RUTID + "',";
            StrAgregar_Empresa += "'" + BK_DIGVER + "',";
            StrAgregar_Empresa += "'" + BK_NOMBRE + "',";
            StrAgregar_Empresa += "'" + BK_USUARIO + "',";
            StrAgregar_Empresa += "'" + BK_CLAVE + "',";
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
            StrAgregar_Empresa += "'" + BK_TELEFONO1 + "',";
            StrAgregar_Empresa += "'" + BK_TELEFONO2 + "',";
            StrAgregar_Empresa += "'" + BK_TELEFONO3 + "',";
            StrAgregar_Empresa += "'" + BK_OBSERVACIONES + "',";
            StrAgregar_Empresa += "'" + BK_LOGO + "')";

            try
            {
                BK_Cmd_Empresa = new SqliteCommand(StrAgregar_Empresa, BM_Connection)
                {
                    Transaction = BK_Transaccion_Empresa
                };
                _ = BK_Cmd_Empresa.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        // Procedimiento Modificar Empresa
        public bool Bm_Empresa_Modificar()
        {
            StrModificar_Empresa = "UPDATE EMPRESA SET ";
            StrModificar_Empresa += "PENTALPHA = '" + BK_PENTALPHA + "',";
            StrModificar_Empresa += "RUTID = '" + BK_RUTID + "',";
            StrModificar_Empresa += "DIGVER = '" + BK_DIGVER + "',";
            StrModificar_Empresa += "NOMBRE = '" + BK_NOMBRE + "',";
            StrModificar_Empresa += "USUARIO = '" + BK_USUARIO + "',";
            StrModificar_Empresa += "CLAVE = '" + BK_CLAVE + "',";
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
            StrModificar_Empresa += "TELEFONO1 = '" + BK_TELEFONO1 + "',";
            StrModificar_Empresa += "TELEFONO2 = '" + BK_TELEFONO2 + "',";
            StrModificar_Empresa += "TELEFONO3 = '" + BK_TELEFONO3 + "',";
            StrModificar_Empresa += "OBSERVACIONES = '" + BK_OBSERVACIONES + "',";
            StrModificar_Empresa += "LOGO = '" + BK_LOGO + "'";

            try
            {
                BK_Cmd_Empresa = new SqliteCommand(StrModificar_Empresa, BM_Connection)
                {
                    Transaction = BK_Transaccion_Empresa
                };
                _ = BK_Cmd_Empresa.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Procedimiento Eliminar Empresa

        public bool Bm_Empresa_Borrar()
        {
            StrBorrar_Empresa = "DELETE FROM EMPRESA";
            try
            {
                BK_Cmd_Empresa = new SqliteCommand(StrBorrar_Empresa, BM_Connection)
                {
                    Transaction = BK_Transaccion_Empresa
                };
                _ = BK_Cmd_Empresa.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }
        // Procedimientos Buscar Siguente
        // Procedimientos Buscar Anterior
        // 
    }
}
