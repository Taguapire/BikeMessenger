using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;


namespace BikeMessenger
{
    internal class Bm_Cliente_Database
    {

        // public SqliteFactory BM_DB;
        // Paso de Parametros Sqlite
        // ***************************************
        private static SQLiteConnection BM_Conexion;
        private TransferVar BM_TrasferVar;

        private static JsonBikeMessengerCliente BK_Cliente;
        private static List<JsonBikeMessengerCliente> BK_ClienteLista;

        private string BM_CadenaConexion;


        private static List<string> BK_Pais;
        private static List<string> BK_Region;
        private static List<string> BK_Comuna;
        private static List<string> BK_Ciudad;

        // Cambio de Operador
        // Busqueda por Muchos


        public Bm_Cliente_Database()
        {
            BM_TrasferVar = new TransferVar();
            BM_TrasferVar.LeerDirectorio();
            BM_CadenaConexion = BM_TrasferVar.Directorio;
            BM_Conexion = new SQLiteConnection("Data Source=" + BM_CadenaConexion + "\\BikeMessenger.db");
            BM_Conexion.Open();
        }


        public List<JsonBikeMessengerCliente> BuscarCliente()
        {
            BK_Cliente = new JsonBikeMessengerCliente();
            BK_ClienteLista = new List<JsonBikeMessengerCliente>();
            DataSet BM_DataSet;
            SQLiteDataAdapter BM_Adaptador;
            SQLiteCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM CLIENTES");

                BM_Adaptador = new SQLiteDataAdapter(BM_Comando)
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
            SQLiteDataAdapter BM_Adaptador;
            SQLiteCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM CLIENTES WHERE PENTALPHA = '" + pPENTALPHA + "'  AND RUTID = '" + pRUTID + "' AND DIGVER = '" + pDIGVER + "'");

                BM_Adaptador = new SQLiteDataAdapter(BM_Comando)
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
            BK_Cliente = new JsonBikeMessengerCliente();
            BK_ClienteLista = new List<JsonBikeMessengerCliente>();

            DataSet BM_DataSet;
            SQLiteDataAdapter BM_Adaptador;
            SQLiteCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM CLIENTES");

                BM_Adaptador = new SQLiteDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };

                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "CLIENTES");

                DataRow LvrCliente = BM_DataSet.Tables["CLIENTES"].NewRow();

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
                BM_Adaptador.Update(BM_DataSet, "CLIENTES");
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
            BK_Cliente = new JsonBikeMessengerCliente();
            BK_ClienteLista = new List<JsonBikeMessengerCliente>();
            DataSet BM_DataSet;
            SQLiteDataAdapter BM_Adaptador;
            SQLiteCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM CLIENTES WHERE PENTALPHA = '" + mBK_Cliente.PENTALPHA + "' AND RUTID = '" + mBK_Cliente.RUTID + "' AND DIGVER = '" + mBK_Cliente.DIGVER + "'");

                BM_Adaptador = new SQLiteDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };

                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "CLIENTES");

                DataRow LvrCliente = BM_DataSet.Tables["CLIENTES"].NewRow();

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
                BM_DataSet.Tables["CLIENTES"].Rows.Add(LvrCliente);
                BM_Adaptador.Update(BM_DataSet, "CLIENTES");
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
            SQLiteCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("DELETE FROM CLIENTES WHERE PENTALPHA = '" + pPENTALPHA + "' AND RUTID = '" + pRUTID + "' AND DIGVER = '" + pDIGVER + "'");
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return false;
            }
            return true;
        }

        public List<JsonBikeMessengerCliente> BuscarGridCliente()
        {
            BK_Cliente = new JsonBikeMessengerCliente();
            BK_ClienteLista = new List<JsonBikeMessengerCliente>();
            DataSet BM_DataSet;
            SQLiteDataAdapter BM_Adaptador;
            SQLiteCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM CLIENTES");

                BM_Adaptador = new SQLiteDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };
                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "CLIENTES");

                foreach (DataRow LvrCliente in BM_DataSet.Tables["CLIENTES"].Rows)
                {
                    BK_Cliente.RUTID = LvrCliente["RUTID"].ToString();
                    BK_Cliente.DIGVER = LvrCliente["DIGVER"].ToString();
                    BK_Cliente.NOMBRE = LvrCliente["NOMBRE"].ToString();
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

        public List<string> GetPais()
        {
            List<string> BK_PaisLista = new List<string>();
            DataSet BM_DataSetPais;
            SQLiteDataAdapter BM_AdaptadorPais;
            SQLiteCommand BM_ComandoPais;
            string BK_Pais;

            try
            {
                BM_ComandoPais = BM_Conexion.CreateCommand();
                BM_ComandoPais.CommandText = string.Format("SELECT * FROM PAIS ORDER BY NOMBEW");

                BM_AdaptadorPais = new SQLiteDataAdapter(BM_ComandoPais);

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
            SQLiteDataAdapter BM_AdaptadorRegion;
            SQLiteCommand BM_ComandoRegion;
            string BK_Region;

            try
            {
                BM_ComandoRegion = BM_Conexion.CreateCommand();
                BM_ComandoRegion.CommandText = string.Format("SELECT * FROM ESTADOREGION ORDER BY NOMBRE");

                BM_AdaptadorRegion = new SQLiteDataAdapter(BM_ComandoRegion);

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
            SQLiteDataAdapter BM_AdaptadorComuna;
            SQLiteCommand BM_ComandoComuna;
            string BK_Comuna;

            try
            {
                BM_ComandoComuna = BM_Conexion.CreateCommand();
                BM_ComandoComuna.CommandText = string.Format("SELECT * FROM COMUNA ORDER BY NOMBRE");

                BM_AdaptadorComuna = new SQLiteDataAdapter(BM_ComandoComuna);

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
            List<string> BK_RegionLista = new List<string>();
            DataSet BM_DataSetRegion;
            SQLiteDataAdapter BM_AdaptadorRegion;
            SQLiteCommand BM_ComandoRegion;
            string BK_Region;

            try
            {
                BM_ComandoRegion = BM_Conexion.CreateCommand();
                BM_ComandoRegion.CommandText = string.Format("SELECT * FROM ESTADOREGION ORDER BY NOMBRE");

                BM_AdaptadorRegion = new SQLiteDataAdapter(BM_ComandoRegion);

                BM_DataSetRegion = new DataSet();
                BM_AdaptadorRegion.Fill(BM_DataSetRegion, "ESTADOREGION");

                foreach (DataRow LvrPais in BM_DataSetRegion.Tables["ESTADOREGION"].Rows)
                {
                    BK_Region = LvrPais["NOMBRE"].ToString();
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

        public string Bm_Cliente_Listado()
        {
            BK_Cliente = new JsonBikeMessengerCliente();
            BK_ClienteLista = new List<JsonBikeMessengerCliente>();
            DataSet BM_DataSet;
            SQLiteDataAdapter BM_Adaptador;
            SQLiteCommand BM_Comando;
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
                BM_Comando.CommandText = string.Format("SELECT * FROM CLIENTES");

                BM_Adaptador = new SQLiteDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };
                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "CLIENTES");

                foreach (DataRow LvrCliente in BM_DataSet.Tables["CLIENTES"].Rows)
                {
                    DocumentoHtml.AbrirFila();
                    DocumentoHtml.AgregarCampo(LvrCliente["RUTID"].ToString() + "-" + LvrCliente["DIGVER"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrCliente["NOMBRE"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrCliente["ACTIVIDAD1"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrCliente["REPRESENTANTE1"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrCliente["TELEFONO2"].ToString(), false);
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
}