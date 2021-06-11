using System;
using Microsoft.Data.Sqlite;

namespace BikeMessenger
{
    internal class Bm_Personal_Database
    {
        // public SQLiteFactory BM_DB;
        public SqliteConnection BM_Connection;
        private SqliteTransaction BK_Transaccion_Personal;
        private SqliteCommand BK_Cmd_Personal;
        private SqliteDataReader BK_Reader_Personal;

        private SqliteCommand BK_Cmd_Personal_Pais;
        private SqliteDataReader BK_Reader_Personal_Pais;

        private SqliteCommand BK_Cmd_Personal_Region;
        private SqliteDataReader BK_Reader_Personal_Region;

        private SqliteCommand BK_Cmd_Personal_Comuna;
        private SqliteDataReader BK_Reader_Personal_Comuna;

        private SqliteCommand BK_Cmd_Personal_Ciudad;
        private SqliteDataReader BK_Reader_Personal_Ciudad;

        private SqliteCommand BK_Cmd_Personal_Grid;
        private SqliteDataReader BK_Reader_Personal_Grid;

        private string StrAgregar_Personal;
        private string StrModificar_Personal;
        private string StrBorrar_Personal;

        private readonly string StrBuscarGrid_Personal = "SELECT RUTID||'-'||DIGVER, APELLIDOS, NOMBRES FROM PERSONAL ORDER BY APELLIDOS ASC";
        private readonly string StrBuscar_Personal = "SELECT * FROM PERSONAL";
        private readonly string StrBuscar_Personal_Pais = "SELECT * FROM PAIS ORDER BY PAIS ASC";
        private readonly string StrBuscar_Personal_Region = "SELECT * FROM ESTADOREGION ORDER BY REGION ASC";
        private readonly string StrBuscar_Personal_Comuna = "SELECT * FROM COMUNA ORDER BY COMUNA ASC";
        private readonly string StrBuscar_Personal_Ciudad = "SELECT * FROM CIUDAD ORDER BY CIUDAD ASC";

        // Campos Personal
        // Campos de Empresa
        public string BK_PENTALPHA { get; set; }
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

        // Listado Personal
        public string BK_GRID_RUT { get; set; }
        public string BK_GRID_APELLIDOS { get; set; }
        public string BK_GRID_NOMBRES { get; set; }

        public void BM_CreateDatabase(SqliteConnection BM_Connection)
        {
            this.BM_Connection = BM_Connection;
        }

        public bool Bm_Personal_Agregar()
        {
            try
            {
                StrAgregar_Personal = "INSERT INTO PERSONAL (";
                StrAgregar_Personal += "PENTALPHA,";
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
                StrAgregar_Personal += "'" + BK_PENTALPHA + "',";
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
                BK_Cmd_Personal = new SqliteCommand(StrAgregar_Personal, BM_Connection)
                {
                    Transaction = BK_Transaccion_Personal
                };
                _ = BK_Cmd_Personal.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_Personal_Buscar()
        {
            try
            {
                BK_Cmd_Personal = new SqliteCommand(StrBuscar_Personal, BM_Connection);
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
                    LimpiarVariables();
                    return false;
                }
            }
            catch (SqliteException)
            {
                return false;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        public bool Bm_Personal_Buscar(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            try
            {
                BK_Cmd_Personal = new SqliteCommand(StrBuscar_Personal + " WHERE PENTALPHA = '" + pPENTALPHA + "' AND RUTID = '" + pRUTID + "' AND DIGVER = '" + pDIGVER + "'", BM_Connection);
                BK_Reader_Personal = BK_Cmd_Personal.ExecuteReader();

                if (BK_Reader_Personal.Read())
                {
                    // Llenar Valores de Personal
                    BK_PENTALPHA = BK_Reader_Personal.GetString(BK_Reader_Personal.GetOrdinal("PENTALPHA"));
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
                    LimpiarVariables();
                    return false;
                }
            }
            catch (SqliteException)
            {
                return false;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        public string Bm_Personal_Listado()
        {
            // Pendientes
            SqliteCommand BK_Cmd_Personal_Listado;
            SqliteDataReader BK_Reader_Personal_Listado;

            LvrTablaHtml DocumentoHtml = new LvrTablaHtml();

            try
            {
                BK_Cmd_Personal_Listado = new SqliteCommand(StrBuscar_Personal, BM_Connection);
                BK_Reader_Personal_Listado = BK_Cmd_Personal_Listado.ExecuteReader();

                DocumentoHtml.CrearTexto("ENVIOS");
                DocumentoHtml.InicioDocumento();
                DocumentoHtml.AgregarTituloTabla("Listado de Personal");
                DocumentoHtml.AbrirEncabezado();
                DocumentoHtml.AgregarEncabezado("RUT-DIGVER");
                DocumentoHtml.AgregarEncabezado("APELLIDOS");
                DocumentoHtml.AgregarEncabezado("NOMBRES");
                DocumentoHtml.AgregarEncabezado("TELEFONO 1");
                DocumentoHtml.AgregarEncabezado("TELEFONO 2");
                DocumentoHtml.AgregarEncabezado("EMAIL");
                DocumentoHtml.AgregarEncabezado("AUTORIZACION");
                DocumentoHtml.AgregarEncabezado("CARGO");
                DocumentoHtml.AgregarEncabezado("CIUDAD");
                DocumentoHtml.CerrarEncabezado();

                while (BK_Reader_Personal_Listado.Read())
                {
                    DocumentoHtml.AbrirFila();
                    DocumentoHtml.AgregarCampo(
                        BK_Reader_Personal_Listado.GetString(
                            BK_Reader_Personal_Listado.GetOrdinal("RUTID")) + 
                            "-" + 
                            BK_Reader_Personal_Listado.GetString(
                                BK_Reader_Personal_Listado.GetOrdinal("DIGVER")), false);
                    DocumentoHtml.AgregarCampo(
                        BK_Reader_Personal_Listado.GetString(
                            BK_Reader_Personal_Listado.GetOrdinal("APELLIDOS")), false);
                    DocumentoHtml.AgregarCampo(
                        BK_Reader_Personal_Listado.GetString(
                            BK_Reader_Personal_Listado.GetOrdinal("NOMBRES")), false);
                    DocumentoHtml.AgregarCampo(
                        BK_Reader_Personal_Listado.GetString(
                            BK_Reader_Personal_Listado.GetOrdinal("TELEFONO1")), false);
                    DocumentoHtml.AgregarCampo(
                        BK_Reader_Personal_Listado.GetString(
                            BK_Reader_Personal_Listado.GetOrdinal("TELEFONO2")), false);
                    DocumentoHtml.AgregarCampo(
                        BK_Reader_Personal_Listado.GetString(
                            BK_Reader_Personal_Listado.GetOrdinal("EMAIL")), false);
                    DocumentoHtml.AgregarCampo(
                        BK_Reader_Personal_Listado.GetString(
                            BK_Reader_Personal_Listado.GetOrdinal("AUTORIZACION")), false);
                    DocumentoHtml.AgregarCampo(
                        BK_Reader_Personal_Listado.GetString(
                            BK_Reader_Personal_Listado.GetOrdinal("CARGO")), false);
                    DocumentoHtml.AgregarCampo(
                        BK_Reader_Personal_Listado.GetString(
                            BK_Reader_Personal_Listado.GetOrdinal("CIUDAD")), false);
                    DocumentoHtml.CerrarFila();
                }

                DocumentoHtml.FinDocumento();
                return DocumentoHtml.BufferHtml;
            }
            catch (SqliteException)
            {
                return "<html><body><h2> No existen clientes </h2></body></html>";
            }
        }


        // Procedimiento Modificar Personal
        public bool Bm_Personal_Modificar(string pPENTALPHA, string pRUTID, string pDIGVER)
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
                StrModificar_Personal += "PENTALPHA = '" + pPENTALPHA + "' AND ";
                StrModificar_Personal += "RUTID = '" + pRUTID + "' AND ";
                StrModificar_Personal += "DIGVER = '" + pDIGVER + "'";
                BK_Cmd_Personal = new SqliteCommand(StrModificar_Personal, BM_Connection)
                {
                    Transaction = BK_Transaccion_Personal
                };
                _ = BK_Cmd_Personal.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                // Console.WriteLine(e.Message);
                return false;
            }
        }

        // Procedimiento Borrar Personal
        public bool Bm_Personal_Borrar(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            try
            {
                StrBorrar_Personal = "DELETE FROM PERSONAL ";
                StrBorrar_Personal += "WHERE ";
                StrBorrar_Personal += "PENTALPHA = '" + pPENTALPHA + "' AND ";
                StrBorrar_Personal += "RUTID = '" + pRUTID + "' AND ";
                StrBorrar_Personal += "DIGVER = '" + pDIGVER + "'";
                BK_Cmd_Personal = new SqliteCommand(StrBorrar_Personal, BM_Connection)
                {
                    Transaction = BK_Transaccion_Personal
                };
                _ = BK_Cmd_Personal.ExecuteNonQuery();
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
                BK_Transaccion_Personal = BM_Connection.BeginTransaction();
                return true;
            }
            catch (SqliteException)
            {
                BK_Transaccion_Personal.Dispose();
                return false;
            }
        }

        public object Bm_Commit_Transaccion()
        {
            try
            {
                BK_Transaccion_Personal.Commit();
                return true;
            }
            catch (SqliteException)
            {
                BK_Transaccion_Personal.Dispose();
                return false;
            }
        }

        public bool Bm_Rollback_Transaccion()
        {
            try
            {
                BK_Transaccion_Personal.Rollback();
                return true;
            }
            catch (SqliteException)
            {
                BK_Transaccion_Personal.Dispose();
                return false;
            }
        }

        // Procedimiento Buscar Pais
        public bool Bm_E_Pais_EjecutarSelect()
        {
            try
            {
                BK_Cmd_Personal_Pais = new SqliteCommand(StrBuscar_Personal_Pais, BM_Connection);
                BK_Reader_Personal_Pais = BK_Cmd_Personal_Pais.ExecuteReader();
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
                if (BK_Reader_Personal_Pais.Read())
                {
                    BK_E_CODPAIS = BK_Reader_Personal_Pais.GetInt16(BK_Reader_Personal_Pais.GetOrdinal("CODPAIS"));
                    BK_E_PAIS = BK_Reader_Personal_Pais.GetString(BK_Reader_Personal_Pais.GetOrdinal("PAIS"));
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
                BK_Cmd_Personal_Region = new SqliteCommand(StrBuscar_Personal_Region, BM_Connection);
                BK_Reader_Personal_Region = BK_Cmd_Personal_Region.ExecuteReader();
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
                if (BK_Reader_Personal_Region.Read())
                {
                    BK_E_CODREGION = BK_Reader_Personal_Region.GetInt16(BK_Reader_Personal_Region.GetOrdinal("CODREGION"));
                    BK_E_REGION = BK_Reader_Personal_Region.GetString(BK_Reader_Personal_Region.GetOrdinal("REGION"));
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
                BK_Cmd_Personal_Comuna = new SqliteCommand(StrBuscar_Personal_Comuna, BM_Connection);
                BK_Reader_Personal_Comuna = BK_Cmd_Personal_Comuna.ExecuteReader();
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
                if (BK_Reader_Personal_Comuna.Read())
                {
                    BK_E_CODCOMUNA = BK_Reader_Personal_Comuna.GetInt16(BK_Reader_Personal_Comuna.GetOrdinal("CODCOMU"));
                    BK_E_COMUNA = BK_Reader_Personal_Comuna.GetString(BK_Reader_Personal_Comuna.GetOrdinal("COMUNA"));
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
                BK_Cmd_Personal_Ciudad = new SqliteCommand(StrBuscar_Personal_Ciudad, BM_Connection);
                BK_Reader_Personal_Ciudad = BK_Cmd_Personal_Ciudad.ExecuteReader();
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
                if (BK_Reader_Personal_Ciudad.Read())
                {
                    BK_E_CODCIUDAD = BK_Reader_Personal_Ciudad.GetInt16(BK_Reader_Personal_Ciudad.GetOrdinal("CODCIUDAD"));
                    BK_E_CIUDAD = BK_Reader_Personal_Ciudad.GetString(BK_Reader_Personal_Ciudad.GetOrdinal("CIUDAD"));
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

        // Buscar Personal
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

        public void LimpiarVariables()
        {
            BK_RUTID = "";
            BK_DIGVER = "";
            BK_APELLIDOS = "";
            BK_NOMBRES = "";
            BK_TELEFONO1 = "";
            BK_TELEFONO2 = "";
            BK_EMAIL = "";
            BK_AUTORIZACION = "";
            BK_CARGO = "";
            BK_DOMICILIO = "";
            BK_NUMERO = "";
            BK_PISO = "";
            BK_DPTO = "";
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
