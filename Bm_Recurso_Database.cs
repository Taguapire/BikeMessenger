using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BikeMessenger
{
    internal class Bm_Recurso_Database
    {
        // public SqliteFactory BM_DB;
        // Paso de Parametros Sqlite
        // ***************************************
        private static SqlConnection BM_Conexion;
        private TransferVar BM_TransferVar = new TransferVar();

        private static JsonBikeMessengerRecurso BK_Recurso;
        private static List<JsonBikeMessengerRecurso> BK_RecursoLista;

        public Bm_Recurso_Database()
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return;
            }

            BM_Conexion = new SqlConnection(BM_TransferVar.BM_Sql_String_Builder.ConnectionString);
            BM_Conexion.Open();
        }

        // Busqueda por Muchos
        public List<JsonBikeMessengerRecurso> BuscarRecurso()
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

            BK_Recurso = new JsonBikeMessengerRecurso();
            BK_RecursoLista = new List<JsonBikeMessengerRecurso>();
            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM RECURSOS");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };

                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "RECURSOS");

                foreach (DataRow LvrRecurso in BM_DataSet.Tables["RECURSOS"].Rows)
                {
                    BK_Recurso.PENTALPHA = LvrRecurso["PENTALPHA"].ToString();
                    BK_Recurso.PATENTE = LvrRecurso["PATENTE"].ToString();
                    BK_Recurso.RUTID = LvrRecurso["RUTID"].ToString();
                    BK_Recurso.DIGVER = LvrRecurso["DIGVER"].ToString();
                    BK_Recurso.TIPO = LvrRecurso["TIPO"].ToString();
                    BK_Recurso.MARCA = LvrRecurso["MARCA"].ToString();
                    BK_Recurso.MODELO = LvrRecurso["MODELO"].ToString();
                    BK_Recurso.VARIANTE = LvrRecurso["VARIANTE"].ToString();
                    BK_Recurso.ANO = LvrRecurso["ANO"].ToString();
                    BK_Recurso.COLOR = LvrRecurso["COLOR"].ToString();
                    BK_Recurso.CIUDAD = LvrRecurso["CIUDAD"].ToString();
                    BK_Recurso.COMUNA = LvrRecurso["COMUNA"].ToString();
                    BK_Recurso.REGION = LvrRecurso["REGION"].ToString();
                    BK_Recurso.PAIS = LvrRecurso["PAIS"].ToString();
                    BK_Recurso.OBSERVACIONES = LvrRecurso["OBSERVACIONES"].ToString();
                    BK_Recurso.FOTO = LvrRecurso["FOTO"].ToString();
                    BK_RecursoLista.Add(BK_Recurso);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return BK_RecursoLista;
        }

        public List<JsonBikeMessengerRecurso> BuscarRecurso(string pPENTALPHA, string pPATENTE)
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

            BK_Recurso = new JsonBikeMessengerRecurso();
            BK_RecursoLista = new List<JsonBikeMessengerRecurso>();
            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM RECURSOS WHERE PENTALPHA = '" + pPENTALPHA + "' AND PATENTE = '" + pPATENTE + "'");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };

                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "RECURSOS");

                foreach (DataRow LvrRecurso in BM_DataSet.Tables["RECURSOS"].Rows)
                {
                    BK_Recurso.PENTALPHA = LvrRecurso["PENTALPHA"].ToString();
                    BK_Recurso.PATENTE = LvrRecurso["PATENTE"].ToString();
                    BK_Recurso.RUTID = LvrRecurso["RUTID"].ToString();
                    BK_Recurso.DIGVER = LvrRecurso["DIGVER"].ToString();
                    BK_Recurso.TIPO = LvrRecurso["TIPO"].ToString();
                    BK_Recurso.MARCA = LvrRecurso["MARCA"].ToString();
                    BK_Recurso.MODELO = LvrRecurso["MODELO"].ToString();
                    BK_Recurso.VARIANTE = LvrRecurso["VARIANTE"].ToString();
                    BK_Recurso.ANO = LvrRecurso["ANO"].ToString();
                    BK_Recurso.COLOR = LvrRecurso["COLOR"].ToString();
                    BK_Recurso.CIUDAD = LvrRecurso["CIUDAD"].ToString();
                    BK_Recurso.COMUNA = LvrRecurso["COMUNA"].ToString();
                    BK_Recurso.REGION = LvrRecurso["REGION"].ToString();
                    BK_Recurso.PAIS = LvrRecurso["PAIS"].ToString();
                    BK_Recurso.OBSERVACIONES = LvrRecurso["OBSERVACIONES"].ToString();
                    BK_Recurso.FOTO = LvrRecurso["FOTO"].ToString();
                    BK_RecursoLista.Add(BK_Recurso);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return BK_RecursoLista;
        }


        public bool AgregarRecurso(JsonBikeMessengerRecurso aBK_Recurso)
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return false;
            }

            BK_Recurso = new JsonBikeMessengerRecurso();
            BK_RecursoLista = new List<JsonBikeMessengerRecurso>();

            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;
            SqlCommandBuilder BM_Builder;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM RECURSOS");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = true
                };

                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "RECURSOS");

                DataRow LvrRecurso = BM_DataSet.Tables["RECURSOS"].NewRow();
                BM_Builder = new SqlCommandBuilder(BM_Adaptador);

                LvrRecurso["PENTALPHA"] = aBK_Recurso.PENTALPHA;
                LvrRecurso["PATENTE"] = aBK_Recurso.PATENTE;
                LvrRecurso["RUTID"] = aBK_Recurso.RUTID;
                LvrRecurso["DIGVER"] = aBK_Recurso.DIGVER;
                LvrRecurso["TIPO"] = aBK_Recurso.TIPO;
                LvrRecurso["MARCA"] = aBK_Recurso.MARCA;
                LvrRecurso["MODELO"] = aBK_Recurso.MODELO;
                LvrRecurso["VARIANTE"] = aBK_Recurso.VARIANTE;
                LvrRecurso["ANO"] = aBK_Recurso.ANO;
                LvrRecurso["COLOR"] = aBK_Recurso.COLOR;
                LvrRecurso["CIUDAD"] = aBK_Recurso.CIUDAD;
                LvrRecurso["COMUNA"] = aBK_Recurso.COMUNA;
                LvrRecurso["REGION"] = aBK_Recurso.REGION;
                LvrRecurso["PAIS"] = aBK_Recurso.PAIS;
                LvrRecurso["OBSERVACIONES"] = aBK_Recurso.OBSERVACIONES;
                LvrRecurso["FOTO"] = aBK_Recurso.FOTO;
                BM_DataSet.Tables["RECURSOS"].Rows.Add(LvrRecurso);
                _ = BM_Builder.GetInsertCommand(true);
                _ = BM_Adaptador.Update(BM_DataSet, "RECURSOS");
                _ = BM_Adaptador.InsertCommand;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return false;
            }
            return true;
        }


        public bool ModificarRecurso(JsonBikeMessengerRecurso mBK_Recurso)
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return false;
            }

            BK_Recurso = new JsonBikeMessengerRecurso();
            BK_RecursoLista = new List<JsonBikeMessengerRecurso>();
            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;
            SqlCommandBuilder BM_Builder;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;
                BM_Comando.CommandText = string.Format("SELECT * FROM RECURSOS WHERE PENTALPHA = '" + mBK_Recurso.PENTALPHA + "' AND PATENTE = '" + mBK_Recurso.PATENTE + "'");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = true
                };

                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "RECURSOS");

                DataRow LvrRecurso = BM_DataSet.Tables["RECURSOS"].Rows[0];

                BM_Builder = new SqlCommandBuilder(BM_Adaptador);

                LvrRecurso["PENTALPHA"] = mBK_Recurso.PENTALPHA;
                LvrRecurso["PATENTE"] = mBK_Recurso.PATENTE;
                LvrRecurso["RUTID"] = mBK_Recurso.RUTID;
                LvrRecurso["DIGVER"] = mBK_Recurso.DIGVER;
                LvrRecurso["TIPO"] = mBK_Recurso.TIPO;
                LvrRecurso["MARCA"] = mBK_Recurso.MARCA;
                LvrRecurso["MODELO"] = mBK_Recurso.MODELO;
                LvrRecurso["VARIANTE"] = mBK_Recurso.VARIANTE;
                LvrRecurso["ANO"] = mBK_Recurso.ANO;
                LvrRecurso["COLOR"] = mBK_Recurso.COLOR;
                LvrRecurso["CIUDAD"] = mBK_Recurso.CIUDAD;
                LvrRecurso["COMUNA"] = mBK_Recurso.COMUNA;
                LvrRecurso["REGION"] = mBK_Recurso.REGION;
                LvrRecurso["PAIS"] = mBK_Recurso.PAIS;
                LvrRecurso["OBSERVACIONES"] = mBK_Recurso.OBSERVACIONES;
                LvrRecurso["FOTO"] = mBK_Recurso.FOTO;
                _ = BM_Builder.GetUpdateCommand(true);
                _ = BM_Adaptador.Update(BM_DataSet, "RECURSOS");
                _ = BM_Adaptador.UpdateCommand;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return false;
            }
            return true;
        }

        public bool BorrarRecurso(string pPENTALPHA, string pPATENTE)
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return false;
            }

            BK_Recurso = new JsonBikeMessengerRecurso();
            BK_RecursoLista = new List<JsonBikeMessengerRecurso>();
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("DELETE FROM RECURSOS WHERE PENTALPHA = '" + pPENTALPHA + "' AND PATENTE = '" + pPATENTE + "'");
                _ = BM_Comando.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return false;
            }
            return true;
        }

        public List<ClaseRecursoGrid> BuscarGridRecurso()
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

            List<ClaseRecursoGrid> GridLocalRecursoLista = new List<ClaseRecursoGrid>();
            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM RECURSOS_VISTA_GRID");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = true
                };

                BM_DataSet = new DataSet();
                _ = BM_Adaptador.Fill(BM_DataSet, "RECURSOS_VISTA_GRID");

                foreach (DataRowView LvrRecurso in BM_DataSet.Tables["RECURSOS_VISTA_GRID"].DefaultView)
                {
                    ClaseRecursoGrid GridLocalRecurso = new ClaseRecursoGrid
                    {
                        PATENTE = LvrRecurso["PATENTE"].ToString(),
                        TIPO = LvrRecurso["TIPO"].ToString(),
                        MARCA = LvrRecurso["MARCA"].ToString(),
                        MODELO = LvrRecurso["MODELO"].ToString(),
                        RUTID = LvrRecurso["RUTID"].ToString(),
                        DIGVER = LvrRecurso["DIGVER"].ToString(),
                        CIUDAD = LvrRecurso["CIUDAD"].ToString()
                    };
                    GridLocalRecursoLista.Add(GridLocalRecurso);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return GridLocalRecursoLista;
        }

        public List<ClasePersonalGrid> BuscarGridPersonal()
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

            List<ClasePersonalGrid> GridLocalPersonalLista = new List<ClasePersonalGrid>();

            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM PERSONAL_VISTA_GRID");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };
                BM_DataSet = new DataSet();
                _ = BM_Adaptador.Fill(BM_DataSet, "PERSONAL_VISTA_GRID");

                foreach (DataRowView LvrPersonal in BM_DataSet.Tables["PERSONAL_VISTA_GRID"].DefaultView)
                {
                    ClasePersonalGrid GridLocalPersonal = new ClasePersonalGrid
                    {
                        RUTID = LvrPersonal["RUTID"].ToString(),
                        DIGVER = LvrPersonal["DIGVER"].ToString(),
                        APELLIDOS = LvrPersonal["APELLIDOS"].ToString(),
                        NOMBRES = LvrPersonal["NOMBRES"].ToString()
                    };
                    GridLocalPersonalLista.Add(GridLocalPersonal);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return GridLocalPersonalLista;
        }

        public string Bm_BuscarNombrePropietario(string pPENTALPHA, string pRUT, string pDIGVER)
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

            string NombrePersonal = "No existe";
            SqlCommand BM_ComandoLocal;
            SqlDataReader BM_ReaderLocal;

            try
            {
                BM_ComandoLocal = BM_Conexion.CreateCommand();
                BM_ComandoLocal.CommandText = string.Format("SELECT APELLIDOS + ', ' + NOMBRES FROM PERSONAL WHERE PENTALPHA = '" + pPENTALPHA + "'  AND RUTID = '" + pRUT + "' AND DIGVER = '" + pDIGVER + "'");
                BM_ReaderLocal = BM_ComandoLocal.ExecuteReader();
                if (BM_ReaderLocal.Read())
                    NombrePersonal = BM_ReaderLocal.GetString(0);
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
            }
            return NombrePersonal;
        }

        public List<string> GetPais()
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

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
                _ = BM_AdaptadorPais.Fill(BM_DataSetPais, "PAIS");

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
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

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
                _ = BM_AdaptadorRegion.Fill(BM_DataSetRegion, "ESTADOREGION");

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
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

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
                _ = BM_AdaptadorComuna.Fill(BM_DataSetComuna, "COMUNA");

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
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

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
                _ = BM_AdaptadorCiudad.Fill(BM_DataSetCiudad, "CIUDAD");

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


        public string Bm_Recurso_Listado()
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

            BK_Recurso = new JsonBikeMessengerRecurso();
            BK_RecursoLista = new List<JsonBikeMessengerRecurso>();
            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;

            LvrTablaHtml DocumentoHtml = new LvrTablaHtml();

            DocumentoHtml.CrearTexto("RECURSOS");
            DocumentoHtml.InicioDocumento();
            DocumentoHtml.AgregarTituloTabla("Listado de Recursos");
            DocumentoHtml.AbrirEncabezado();
            DocumentoHtml.AgregarEncabezado("TIPO");
            DocumentoHtml.AgregarEncabezado("PATENTE");
            DocumentoHtml.AgregarEncabezado("MARCA");
            DocumentoHtml.AgregarEncabezado("MODELO");
            DocumentoHtml.AgregarEncabezado("VARIANTE");
            DocumentoHtml.AgregarEncabezado("AÑO");
            DocumentoHtml.AgregarEncabezado("PROPIETARIO"); // OJO
            DocumentoHtml.AgregarEncabezado("CIUDAD");
            DocumentoHtml.AgregarEncabezado("COMUNA");
            DocumentoHtml.AgregarEncabezado("REGION");
            DocumentoHtml.AgregarEncabezado("PAIS");
            DocumentoHtml.CerrarEncabezado();

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM RECURSOS_VISTA_LISTADO");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };
                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "RECURSOS_VISTA_LISTADO");

                foreach (DataRowView LvrRecurso in BM_DataSet.Tables["RECURSOS_VISTA_LISTADO"].DefaultView)
                {
                    DocumentoHtml.AbrirFila();
                    DocumentoHtml.AgregarCampo(LvrRecurso["TIPO"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrRecurso["PATENTE"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrRecurso["MARCA"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrRecurso["MODELO"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrRecurso["VARIANTE"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrRecurso["APELLIDOS"].ToString() +", "+ LvrRecurso["NOMBRES"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrRecurso["ANO"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrRecurso["CIUDAD"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrRecurso["COMUNA"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrRecurso["REGION"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrRecurso["PAIS"].ToString(), false);
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

    public class ClaseRecursoGrid
    {
        public string PATENTE { get; set; }
        public string TIPO { get; set; }
        public string MARCA { get; set; }
        public string MODELO { get; set; }
        public string RUTID { get; set; }
        public string DIGVER { get; set; }
        public string CIUDAD { get; set; }
    }
}