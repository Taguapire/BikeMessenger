using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;


namespace BikeMessenger
{
    internal class Bm_Cliente_Database
    {

        // public SqliteFactory BM_DB;
        // Paso de Parametros Sqlite
        // ***************************************
        private static SqlConnection BM_Conexion;
        private TransferVar BM_TrasferVar;

        private static JsonBikeMessengerCliente BK_Cliente;
        private static List<JsonBikeMessengerCliente> BK_ClienteLista;

        private string BM_CadenaConexion;

        public Bm_Cliente_Database()
        {
            BM_TrasferVar = new TransferVar();
            BM_TrasferVar.LeerDirectorio();
            BM_CadenaConexion = BM_TrasferVar.Directorio;
            BM_Conexion = new SqlConnection("Data Source=VASCON\\SQLEXPRESS;Initial Catalog=bikemessenger;MultipleActiveResultSets=true;User ID=bikemessenger; Password=Hola1974");
            BM_Conexion.Open();
        }


        public List<JsonBikeMessengerCliente> BuscarCliente()
        {
            BK_Cliente = new JsonBikeMessengerCliente();
            BK_ClienteLista = new List<JsonBikeMessengerCliente>();
            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM CLIENTES");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };
                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "CLIENTES");

                foreach (DataRow LvrCliente in BM_DataSet.Tables["CLIENTES"].Rows)
                {
                    BK_Cliente.PENTALPHA = LvrCliente["PENTALPHA"].ToString();
                    BK_Cliente.RUTID = LvrCliente["RUTID"].ToString();
                    BK_Cliente.DIGVER = LvrCliente["DIGVER"].ToString();
                    BK_Cliente.NOMBRE = LvrCliente["NOMBRE"].ToString();
                    BK_Cliente.USUARIO = LvrCliente["USUARIO"].ToString();
                    BK_Cliente.CLAVE = LvrCliente["CLAVE"].ToString();
                    BK_Cliente.ACTIVIDAD1 = LvrCliente["ACTIVIDAD1"].ToString();
                    BK_Cliente.ACTIVIDAD2 = LvrCliente["ACTIVIDAD2"].ToString();
                    BK_Cliente.REPRESENTANTE1 = LvrCliente["REPRESENTANTE1"].ToString();
                    BK_Cliente.REPRESENTANTE2 = LvrCliente["REPRESENTANTE2"].ToString();
                    BK_Cliente.EMAIL = LvrCliente["EMAIL"].ToString();
                    BK_Cliente.TELEFONO1 = LvrCliente["TELEFONO1"].ToString();
                    BK_Cliente.TELEFONO2 = LvrCliente["TELEFONO2"].ToString();
                    BK_Cliente.DOMICILIO1 = LvrCliente["DOMICILIO1"].ToString();
                    BK_Cliente.DOMICILIO2 = LvrCliente["DOMICILIO2"].ToString();
                    BK_Cliente.NUMERO = LvrCliente["NUMERO"].ToString();
                    BK_Cliente.PISO = LvrCliente["PISO"].ToString();
                    BK_Cliente.OFICINA = LvrCliente["OFICINA"].ToString();
                    BK_Cliente.CODIGOPOSTAL = LvrCliente["CODIGOPOSTAL"].ToString();
                    BK_Cliente.CIUDAD = LvrCliente["CIUDAD"].ToString();
                    BK_Cliente.COMUNA = LvrCliente["COMUNA"].ToString();
                    BK_Cliente.ESTADOREGION = LvrCliente["REGION"].ToString();
                    BK_Cliente.PAIS = LvrCliente["PAIS"].ToString();
                    BK_Cliente.OBSERVACIONES = LvrCliente["OBSERVACIONES"].ToString();
                    BK_Cliente.LOGO = LvrCliente["FOTO"].ToString();
                    BK_ClienteLista.Add(BK_Cliente);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return BK_ClienteLista;
        }

        public List<JsonBikeMessengerCliente> BuscarCliente(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            BK_Cliente = new JsonBikeMessengerCliente();
            BK_ClienteLista = new List<JsonBikeMessengerCliente>();
            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM CLIENTES WHERE PENTALPHA = '" + pPENTALPHA + "'  AND RUTID = '" + pRUTID + "' AND DIGVER = '" + pDIGVER + "'");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };
                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "CLIENTES");

                foreach (DataRow LvrCliente in BM_DataSet.Tables["CLIENTES"].Rows)
                {
                    BK_Cliente.PENTALPHA = LvrCliente["PENTALPHA"].ToString();
                    BK_Cliente.RUTID = LvrCliente["RUTID"].ToString();
                    BK_Cliente.DIGVER = LvrCliente["DIGVER"].ToString();
                    BK_Cliente.NOMBRE = LvrCliente["NOMBRE"].ToString();
                    BK_Cliente.USUARIO = LvrCliente["USUARIO"].ToString();
                    BK_Cliente.CLAVE = LvrCliente["CLAVE"].ToString();
                    BK_Cliente.ACTIVIDAD1 = LvrCliente["ACTIVIDAD1"].ToString();
                    BK_Cliente.ACTIVIDAD2 = LvrCliente["ACTIVIDAD2"].ToString();
                    BK_Cliente.REPRESENTANTE1 = LvrCliente["REPRESENTANTE1"].ToString();
                    BK_Cliente.REPRESENTANTE2 = LvrCliente["REPRESENTANTE2"].ToString();
                    BK_Cliente.EMAIL = LvrCliente["EMAIL"].ToString();
                    BK_Cliente.TELEFONO1 = LvrCliente["TELEFONO1"].ToString();
                    BK_Cliente.TELEFONO2 = LvrCliente["TELEFONO2"].ToString();
                    BK_Cliente.DOMICILIO1 = LvrCliente["DOMICILIO1"].ToString();
                    BK_Cliente.DOMICILIO2 = LvrCliente["DOMICILIO2"].ToString();
                    BK_Cliente.NUMERO = LvrCliente["NUMERO"].ToString();
                    BK_Cliente.PISO = LvrCliente["PISO"].ToString();
                    BK_Cliente.OFICINA = LvrCliente["OFICINA"].ToString();
                    BK_Cliente.CODIGOPOSTAL = LvrCliente["CODIGOPOSTAL"].ToString();
                    BK_Cliente.CIUDAD = LvrCliente["CIUDAD"].ToString();
                    BK_Cliente.COMUNA = LvrCliente["COMUNA"].ToString();
                    BK_Cliente.ESTADOREGION = LvrCliente["REGION"].ToString();
                    BK_Cliente.PAIS = LvrCliente["PAIS"].ToString();
                    BK_Cliente.OBSERVACIONES = LvrCliente["OBSERVACIONES"].ToString();
                    BK_Cliente.LOGO = LvrCliente["FOTO"].ToString();
                    BK_ClienteLista.Add(BK_Cliente);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return BK_ClienteLista;
        }


        public bool AgregarCliente(JsonBikeMessengerCliente aBK_Cliente)
        {
            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;
            SqlCommandBuilder BM_Builder;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM CLIENTES");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = true
                };

                BM_DataSet = new DataSet();
                _ = BM_Adaptador.Fill(BM_DataSet, "CLIENTES");

                DataRow LvrCliente = BM_DataSet.Tables["CLIENTES"].NewRow();

                BM_Builder = new SqlCommandBuilder(BM_Adaptador);

                LvrCliente["PENTALPHA"] = aBK_Cliente.PENTALPHA;
                LvrCliente["RUTID"] = aBK_Cliente.RUTID;
                LvrCliente["DIGVER"] = aBK_Cliente.DIGVER;
                LvrCliente["NOMBRE"] = aBK_Cliente.NOMBRE;
                LvrCliente["USUARIO"] = aBK_Cliente.USUARIO;
                LvrCliente["CLAVE"] = aBK_Cliente.CLAVE;
                LvrCliente["ACTIVIDAD1"] = aBK_Cliente.ACTIVIDAD1;
                LvrCliente["ACTIVIDAD2"] = aBK_Cliente.ACTIVIDAD2;
                LvrCliente["REPRESENTANTE1"] = aBK_Cliente.REPRESENTANTE1;
                LvrCliente["REPRESENTANTE2"] = aBK_Cliente.REPRESENTANTE2;
                LvrCliente["EMAIL"] = aBK_Cliente.EMAIL;
                LvrCliente["TELEFONO1"] = aBK_Cliente.TELEFONO1;
                LvrCliente["TELEFONO2"] = aBK_Cliente.TELEFONO2;
                LvrCliente["DOMICILIO1"] = aBK_Cliente.DOMICILIO1;
                LvrCliente["DOMICILIO2"] = aBK_Cliente.DOMICILIO2;
                LvrCliente["NUMERO"] = aBK_Cliente.NUMERO;
                LvrCliente["PISO"] = aBK_Cliente.PISO;
                LvrCliente["OFICINA"] = aBK_Cliente.OFICINA;
                LvrCliente["CODIGOPOSTAL"] = aBK_Cliente.CODIGOPOSTAL;
                LvrCliente["CIUDAD"] = aBK_Cliente.CIUDAD;
                LvrCliente["COMUNA"] = aBK_Cliente.COMUNA;
                LvrCliente["REGION"] = aBK_Cliente.ESTADOREGION;
                LvrCliente["PAIS"] = aBK_Cliente.PAIS;
                LvrCliente["OBSERVACIONES"] = aBK_Cliente.OBSERVACIONES;
                LvrCliente["FOTO"] = aBK_Cliente.LOGO;
                BM_DataSet.Tables["CLIENTES"].Rows.Add(LvrCliente);
                _ = BM_Builder.GetInsertCommand(true);
                _ = BM_Adaptador.Update(BM_DataSet, "CLIENTES");
                _ = BM_Adaptador.InsertCommand;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return false;
            }
            return true;
        }


        public bool ModificarCliente(JsonBikeMessengerCliente mBK_Cliente)
        {
            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;
            SqlCommandBuilder BM_Builder;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;
                BM_Comando.CommandText = string.Format("SELECT * FROM CLIENTES WHERE PENTALPHA = '" + mBK_Cliente.PENTALPHA + "' AND RUTID = '" + mBK_Cliente.RUTID + "' AND DIGVER = '" + mBK_Cliente.DIGVER + "'");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = true
                };

                BM_DataSet = new DataSet();
                _ = BM_Adaptador.Fill(BM_DataSet, "CLIENTES");

                DataRow LvrCliente = BM_DataSet.Tables["CLIENTES"].Rows[0];

                BM_Builder = new SqlCommandBuilder(BM_Adaptador);

                LvrCliente["PENTALPHA"] = mBK_Cliente.PENTALPHA;
                LvrCliente["RUTID"] = mBK_Cliente.RUTID;
                LvrCliente["DIGVER"] = mBK_Cliente.DIGVER;
                LvrCliente["NOMBRE"] = mBK_Cliente.NOMBRE;
                LvrCliente["USUARIO"] = mBK_Cliente.USUARIO;
                LvrCliente["CLAVE"] = mBK_Cliente.CLAVE;
                LvrCliente["ACTIVIDAD1"] = mBK_Cliente.ACTIVIDAD1;
                LvrCliente["ACTIVIDAD2"] = mBK_Cliente.ACTIVIDAD2;
                LvrCliente["REPRESENTANTE1"] = mBK_Cliente.REPRESENTANTE1;
                LvrCliente["REPRESENTANTE2"] = mBK_Cliente.REPRESENTANTE2;
                LvrCliente["EMAIL"] = mBK_Cliente.EMAIL;
                LvrCliente["TELEFONO1"] = mBK_Cliente.TELEFONO1;
                LvrCliente["TELEFONO2"] = mBK_Cliente.TELEFONO2;
                LvrCliente["DOMICILIO1"] = mBK_Cliente.DOMICILIO1;
                LvrCliente["DOMICILIO2"] = mBK_Cliente.DOMICILIO2;
                LvrCliente["NUMERO"] = mBK_Cliente.NUMERO;
                LvrCliente["PISO"] = mBK_Cliente.PISO;
                LvrCliente["OFICINA"] = mBK_Cliente.OFICINA;
                LvrCliente["CODIGOPOSTAL"] = mBK_Cliente.CODIGOPOSTAL;
                LvrCliente["CIUDAD"] = mBK_Cliente.CIUDAD;
                LvrCliente["COMUNA"] = mBK_Cliente.COMUNA;
                LvrCliente["REGION"] = mBK_Cliente.ESTADOREGION;
                LvrCliente["PAIS"] = mBK_Cliente.PAIS;
                LvrCliente["OBSERVACIONES"] = mBK_Cliente.OBSERVACIONES;
                LvrCliente["FOTO"] = mBK_Cliente.LOGO;
                _ = BM_Builder.GetUpdateCommand(true);
                _ = BM_Adaptador.Update(BM_DataSet, "CLIENTES");
                _ = BM_Adaptador.UpdateCommand;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return false;
            }
            return true;
        }

        public bool BorrarCliente(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            BK_Cliente = new JsonBikeMessengerCliente();
            BK_ClienteLista = new List<JsonBikeMessengerCliente>();
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("DELETE FROM CLIENTES WHERE PENTALPHA = '" + pPENTALPHA + "' AND RUTID = '" + pRUTID + "' AND DIGVER = '" + pDIGVER + "'");
                _ = BM_Comando.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return false;
            }
            return true;
        }

        public List<ClaseClientesGrid> BuscarGridClientes()
        {
            List<ClaseClientesGrid> GridLocalClientesLista = new List<ClaseClientesGrid>();

            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM CLIENTES_VISTA_GRID");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };

                BM_DataSet = new DataSet();
                _ = BM_Adaptador.Fill(BM_DataSet, "CLIENTES_VISTA_GRID");

                foreach (DataRowView LvrClientes in BM_DataSet.Tables["CLIENTES_VISTA_GRID"].DefaultView)
                {
                    ClaseClientesGrid GridLocalClientes = new ClaseClientesGrid
                    {
                        RUTID = LvrClientes["RUTID"].ToString(),
                        DIGVER = LvrClientes["DIGVER"].ToString(),
                        NOMBRE = LvrClientes["NOMBRE"].ToString()
                    };

                    GridLocalClientesLista.Add(GridLocalClientes);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return GridLocalClientesLista;
        }

        public List<string> GetPais()
        {
            List<string> BK_PaisLista = new List<string>();
            DataSet BM_DataSetPais;
            SqlDataAdapter BM_AdaptadorPais;
            SqlCommand BM_ComandoPais;
            string BK_Pais;

            try
            {
                BM_ComandoPais = BM_Conexion.CreateCommand();
                BM_ComandoPais.CommandText = string.Format("SELECT * FROM PAIS ORDER BY NOMBRE");

                BM_AdaptadorPais = new SqlDataAdapter(BM_ComandoPais);

                BM_DataSetPais = new DataSet();
                BM_AdaptadorPais.Fill(BM_DataSetPais, "PAIS");

                foreach (DataRow LvrPais in BM_DataSetPais.Tables["PAIS"].Rows)
                {
                    BK_Pais = LvrPais["NOMBRE"].ToString();
                    BK_PaisLista.Add(BK_Pais);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return BK_PaisLista;
        }

        public List<string> GetRegion()
        {
            List<string> BK_RegionLista = new List<string>();
            DataSet BM_DataSetRegion;
            SqlDataAdapter BM_AdaptadorRegion;
            SqlCommand BM_ComandoRegion;
            string BK_Region;

            try
            {
                BM_ComandoRegion = BM_Conexion.CreateCommand();
                BM_ComandoRegion.CommandText = string.Format("SELECT * FROM ESTADOREGION ORDER BY NOMBRE");

                BM_AdaptadorRegion = new SqlDataAdapter(BM_ComandoRegion);

                BM_DataSetRegion = new DataSet();
                BM_AdaptadorRegion.Fill(BM_DataSetRegion, "ESTADOREGION");

                foreach (DataRow LvrRegion in BM_DataSetRegion.Tables["ESTADOREGION"].Rows)
                {
                    BK_Region = LvrRegion["NOMBRE"].ToString();
                    BK_RegionLista.Add(BK_Region);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return BK_RegionLista;
        }

        public List<string> GetComuna()
        {
            List<string> BK_ComunaLista = new List<string>();
            DataSet BM_DataSetComuna;
            SqlDataAdapter BM_AdaptadorComuna;
            SqlCommand BM_ComandoComuna;
            string BK_Comuna;

            try
            {
                BM_ComandoComuna = BM_Conexion.CreateCommand();
                BM_ComandoComuna.CommandText = string.Format("SELECT * FROM COMUNA ORDER BY NOMBRE");

                BM_AdaptadorComuna = new SqlDataAdapter(BM_ComandoComuna);

                BM_DataSetComuna = new DataSet();
                BM_AdaptadorComuna.Fill(BM_DataSetComuna, "COMUNA");

                foreach (DataRow LvrComuna in BM_DataSetComuna.Tables["COMUNA"].Rows)
                {
                    BK_Comuna = LvrComuna["NOMBRE"].ToString();
                    BK_ComunaLista.Add(BK_Comuna);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return BK_ComunaLista;
        }

        public List<string> GetCiudad()
        {
            List<string> BK_CiudadLista = new List<string>();
            DataSet BM_DataSetCiudad;
            SqlDataAdapter BM_AdaptadorCiudad;
            SqlCommand BM_ComandoCiudad;
            string BK_Ciudad;

            try
            {
                BM_ComandoCiudad = BM_Conexion.CreateCommand();
                BM_ComandoCiudad.CommandText = string.Format("SELECT * FROM CIUDAD ORDER BY NOMBRE");

                BM_AdaptadorCiudad = new SqlDataAdapter(BM_ComandoCiudad);

                BM_DataSetCiudad = new DataSet();
                BM_AdaptadorCiudad.Fill(BM_DataSetCiudad, "CIUDAD");

                foreach (DataRow LvrCiudad in BM_DataSetCiudad.Tables["CIUDAD"].Rows)
                {
                    BK_Ciudad = LvrCiudad["NOMBRE"].ToString();
                    BK_CiudadLista.Add(BK_Ciudad);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return BK_CiudadLista;
        }

        public string Bm_Cliente_Listado()
        {
            BK_Cliente = new JsonBikeMessengerCliente();
            BK_ClienteLista = new List<JsonBikeMessengerCliente>();
            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;
            LvrTablaHtml DocumentoHtml = new LvrTablaHtml();
            DocumentoHtml.CrearTexto("CLIENTES");
            DocumentoHtml.InicioDocumento();
            DocumentoHtml.AgregarTituloTabla("Listado de Clientes");
            DocumentoHtml.AbrirEncabezado();
            DocumentoHtml.AgregarEncabezado("RUT-DIGVER");
            DocumentoHtml.AgregarEncabezado("NOMBRE");
            DocumentoHtml.AgregarEncabezado("ACTIVIDAD");
            DocumentoHtml.AgregarEncabezado("REPRESENTANTE");
            DocumentoHtml.AgregarEncabezado("TELEFONO");
            DocumentoHtml.AgregarEncabezado("DOMICILIO");
            DocumentoHtml.AgregarEncabezado("NUMERO");
            DocumentoHtml.AgregarEncabezado("OFICINA");
            DocumentoHtml.AgregarEncabezado("CIUDAD");
            DocumentoHtml.AgregarEncabezado("COMUNA");
            DocumentoHtml.AgregarEncabezado("PAIS");
            DocumentoHtml.CerrarEncabezado();

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM CLIENTES_VISTA_LISTADO");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };
                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "CLIENTES_VISTA_LISTADO");

                foreach (DataRowView LvrCliente in BM_DataSet.Tables["CLIENTES_VISTA_LISTADO"].DefaultView)
                {
                    DocumentoHtml.AbrirFila();
                    DocumentoHtml.AgregarCampo(LvrCliente["RUTID"].ToString() + "-" + LvrCliente["DIGVER"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrCliente["NOMBRE"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrCliente["ACTIVIDAD1"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrCliente["REPRESENTANTE1"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrCliente["TELEFONO1"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrCliente["DOMICILIO1"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrCliente["NUMERO"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrCliente["OFICINA"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrCliente["CIUDAD"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrCliente["COMUNA"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrCliente["PAIS"].ToString(), false);
                    DocumentoHtml.CerrarFila();
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            DocumentoHtml.FinDocumento();
            return DocumentoHtml.BufferHtml;
        }
    }
    public class ClaseClientesGrid
    {
        public string RUTID { get; set; }
        public string DIGVER { get; set; }
        public string NOMBRE { get; set; }
    }
}