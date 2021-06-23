using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace BikeMessenger
{
    internal class Bm_Recurso_Database
    {
        // public SqliteFactory BM_DB;
        // Paso de Parametros Sqlite
        // ***************************************
        private static SQLiteConnection BM_Conexion;
        private TransferVar BM_TrasferVar;

        private static JsonBikeMessengerRecurso BK_Recurso;
        private static List<JsonBikeMessengerRecurso> BK_RecursoLista;

        private string BM_CadenaConexion;
        private static List<string> BK_Pais;
        private static List<string> BK_Region;
        private static List<string> BK_Comuna;
        private static List<string> BK_Ciudad;

        public Bm_Recurso_Database()
        {
            BM_TrasferVar = new TransferVar();
            BM_TrasferVar.LeerDirectorio();
            BM_CadenaConexion = BM_TrasferVar.Directorio;
            BM_Conexion = new SQLiteConnection("Data Source=" + BM_CadenaConexion + "\\BikeMessenger.db");
            BM_Conexion.Open();
        }

        // Busqueda por Muchos
        public List<JsonBikeMessengerRecurso> BuscarRecurso()
        {
            BK_Recurso = new JsonBikeMessengerRecurso();
            BK_RecursoLista = new List<JsonBikeMessengerRecurso>();
            DataSet BM_DataSet;
            SQLiteDataAdapter BM_Adaptador;
            SQLiteCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM RECURSOS");

                BM_Adaptador = new SQLiteDataAdapter(BM_Comando)
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
                    BK_Recurso.PROPIETARIO = LvrRecurso["PROPIETARIO"].ToString();
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
            BK_Recurso = new JsonBikeMessengerRecurso();
            BK_RecursoLista = new List<JsonBikeMessengerRecurso>();
            DataSet BM_DataSet;
            SQLiteDataAdapter BM_Adaptador;
            SQLiteCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM RECURSOS WHERE PENTALPHA = '" + pPENTALPHA + "' AND PATENTE = '" + pPATENTE + "'");

                BM_Adaptador = new SQLiteDataAdapter(BM_Comando)
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
                    BK_Recurso.PROPIETARIO = LvrRecurso["PROPIETARIO"].ToString();
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
            BK_Recurso = new JsonBikeMessengerRecurso();
            BK_RecursoLista = new List<JsonBikeMessengerRecurso>();

            DataSet BM_DataSet;
            SQLiteDataAdapter BM_Adaptador;
            SQLiteCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM RECURSOS");

                BM_Adaptador = new SQLiteDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };

                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "RECURSOS");

                DataRow LvrRecurso = BM_DataSet.Tables["RECURSOS"].NewRow();

                LvrRecurso["PENTALPHA"] = aBK_Recurso.PENTALPHA;
                LvrRecurso["PATENTE"] = aBK_Recurso.PATENTE;
                LvrRecurso["RUTID"] = aBK_Recurso.RUTID;
                LvrRecurso["DIGVER"] = aBK_Recurso.DIGVER;
                LvrRecurso["PROPIETARIO"] = aBK_Recurso.PROPIETARIO;
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
                BM_Adaptador.Update(BM_DataSet, "RECURSOS");
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
            BK_Recurso = new JsonBikeMessengerRecurso();
            BK_RecursoLista = new List<JsonBikeMessengerRecurso>();
            DataSet BM_DataSet;
            SQLiteDataAdapter BM_Adaptador;
            SQLiteCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM RECURSOS WHERE PENTALPHA = '" + mBK_Recurso.PENTALPHA + "' AND PATENTE = '" + mBK_Recurso.PATENTE + "'");

                BM_Adaptador = new SQLiteDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };

                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "RECURSOS");

                DataRow LvrRecurso = BM_DataSet.Tables["RECURSOS"].NewRow();
                LvrRecurso["PENTALPHA"] = mBK_Recurso.PENTALPHA;
                LvrRecurso["PATENTE"] = mBK_Recurso.PATENTE;
                LvrRecurso["RUTID"] = mBK_Recurso.RUTID;
                LvrRecurso["DIGVER"] = mBK_Recurso.DIGVER;
                LvrRecurso["PROPIETARIO"] = mBK_Recurso.PROPIETARIO;
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
                BM_DataSet.Tables["RECURSOS"].Rows.Add(LvrRecurso);
                BM_Adaptador.Update(BM_DataSet, "RECURSOS");
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
            BK_Recurso = new JsonBikeMessengerRecurso();
            BK_RecursoLista = new List<JsonBikeMessengerRecurso>();
            SQLiteCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("DELETE FROM RECURSOS WHERE PENTALPHA = '" + pPENTALPHA + "' AND PATENTE = '" + pPATENTE + "'");
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
            List<ClaseRecursoGrid> GridLocalRecursoLista = new List<ClaseRecursoGrid>();
            ClaseRecursoGrid GridLocalRecurso = new ClaseRecursoGrid();
            DataSet BM_DataSet;
            SQLiteDataAdapter BM_Adaptador;
            SQLiteCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM RECURSOS");

                BM_Adaptador = new SQLiteDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };
                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "RECURSOS");

                foreach (DataRow LvrRecurso in BM_DataSet.Tables["RECURSOS"].Rows)
                {
                    GridLocalRecurso = new ClaseRecursoGrid();
                    GridLocalRecurso.PATENTE = LvrRecurso["PATENTE"].ToString();
                    GridLocalRecurso.TIPO = LvrRecurso[".TIPO"].ToString();
                    GridLocalRecurso.MARCA = LvrRecurso[".MARCA"].ToString();
                    GridLocalRecurso.MODELO = LvrRecurso[".MODELO"].ToString();
                    GridLocalRecurso.CIUDAD = LvrRecurso[".CIUDAD"].ToString();
                    BK_RecursoLista.Add(BK_Recurso);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return GridLocalRecursoLista;
        }

        public List<ClasePersonalGrid> BuscarGridPersonal()
        {
            List<ClasePersonalGrid> GridLocalPersonalLista = new List<ClasePersonalGrid>();

            DataSet BM_DataSet;
            SQLiteDataAdapter BM_Adaptador;
            SQLiteCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM PERSONAL");

                BM_Adaptador = new SQLiteDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };
                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "PERSONAL");

                foreach (DataRow LvrPersonal in BM_DataSet.Tables["PERSONAL"].Rows)
                {
                    ClasePersonalGrid GridLocalPersonal = new ClasePersonalGrid();
                    GridLocalPersonal.RUTID = LvrPersonal["RUTID"].ToString();
                    GridLocalPersonal.DIGVER = LvrPersonal["DIGVER"].ToString();
                    GridLocalPersonal.APELLIDOS = LvrPersonal["APELLIDOS"].ToString();
                    GridLocalPersonal.NOMBRES = LvrPersonal["NOMBRES"].ToString();
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


        public string Bm_Recurso_Listado()
        {

            BK_Recurso = new JsonBikeMessengerRecurso();
            BK_RecursoLista = new List<JsonBikeMessengerRecurso>();
            DataSet BM_DataSet;
            SQLiteDataAdapter BM_Adaptador;
            SQLiteCommand BM_Comando;

            LvrTablaHtml DocumentoHtml = new LvrTablaHtml();

            DocumentoHtml.CrearTexto("RECURSOS");
            DocumentoHtml.InicioDocumento();
            DocumentoHtml.AgregarTituloTabla("Listado de Recursos");
            DocumentoHtml.AbrirEncabezado();
            DocumentoHtml.AgregarEncabezado("RUT-DIGVER");
            DocumentoHtml.AgregarEncabezado("PROPIETARIO");
            DocumentoHtml.AgregarEncabezado("TIPO");
            DocumentoHtml.AgregarEncabezado("PATENTE");
            DocumentoHtml.AgregarEncabezado("MARCA");
            DocumentoHtml.AgregarEncabezado("MODELO");
            DocumentoHtml.AgregarEncabezado("VARIANTE");
            DocumentoHtml.AgregarEncabezado("AÑO");
            DocumentoHtml.AgregarEncabezado("CIUDAD");
            DocumentoHtml.AgregarEncabezado("COMUNA");
            DocumentoHtml.AgregarEncabezado("REGION");
            DocumentoHtml.AgregarEncabezado("PAIS");
            DocumentoHtml.CerrarEncabezado();

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM RECURSOS");

                BM_Adaptador = new SQLiteDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };
                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "RECURSOS");

                foreach (DataRow LvrRecurso in BM_DataSet.Tables["RECURSOS"].Rows)
                {
                    DocumentoHtml.AbrirFila();
                    DocumentoHtml.AgregarCampo(LvrRecurso["RUTID"].ToString() + "-" + LvrRecurso["DIGVER"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrRecurso["PROPIETARIO"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrRecurso["TIPO"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrRecurso["PATENTE"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrRecurso["MARCA"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrRecurso["MODELO"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrRecurso["VARIANTE"].ToString(), false);
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
        public string CIUDAD { get; set; }
    }
}