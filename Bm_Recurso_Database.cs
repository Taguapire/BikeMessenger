using System;
using System.Collections.Generic;
using SQLite;

namespace BikeMessenger
{
    internal class Bm_Recurso_Database
    {
        private TransferVar BM_TransferVar = new TransferVar();

        // Valode de Memoria a Bases de Datos

        private StructBikeMessengerRecurso BK_Recurso;
        private List<StructBikeMessengerRecurso> BK_RecursoLista;

        private string CompletoNombreBD = "";

        public Bm_Recurso_Database()
        {
            CompletoNombreBD = BM_TransferVar.DIRECTORIO_BASE_LOCAL + "\\BikeMessenger.db";
        }

        // Busqueda por Muchos
        public List<StructBikeMessengerRecurso> BuscarRecurso(string pPENTALPHA)
        {
            BK_RecursoLista = new List<StructBikeMessengerRecurso>();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerRecurso> results = BM_ConexionLite.Table<TbBikeMessengerRecurso>().Where(t => t.PENTALPHA == pPENTALPHA).ToList();

            if (results.Count > 0)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    BK_Recurso = new StructBikeMessengerRecurso();
                    BK_Recurso.OPERACION = results[i].OPERACION;
                    BK_Recurso.PKRECURSO = results[i].PKRECURSO;
                    BK_Recurso.PENTALPHA = results[i].PENTALPHA;
                    BK_Recurso.PATENTE = results[i].PATENTE;
                    BK_Recurso.RUTID = results[i].RUTID;
                    BK_Recurso.DIGVER = results[i].DIGVER;
                    BK_Recurso.TIPO = results[i].TIPO;
                    BK_Recurso.MARCA = results[i].MARCA;
                    BK_Recurso.MODELO = results[i].MODELO;
                    BK_Recurso.VARIANTE = results[i].VARIANTE;
                    BK_Recurso.ANO = results[i].ANO;
                    BK_Recurso.COLOR = results[i].COLOR;
                    BK_Recurso.CIUDAD = results[i].CIUDAD;
                    BK_Recurso.COMUNA = results[i].COMUNA;
                    BK_Recurso.REGION = results[i].REGION;
                    BK_Recurso.PAIS = results[i].PAIS;
                    BK_Recurso.OBSERVACIONES = results[i].OBSERVACIONES;
                    BK_Recurso.FOTO = results[i].FOTO;
                    BK_Recurso.RESMENSAJE = "OK";
                    BK_Recurso.RESOPERACION = "OK";
                    BK_RecursoLista.Add(BK_Recurso);
                }

            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return BK_RecursoLista;
        }

        public List<StructBikeMessengerRecurso> BuscarRecurso(string pPENTALPHA, string pPATENTE)
        {
            string VPKRECURSO = pPENTALPHA + pPATENTE;

            BK_RecursoLista = new List<StructBikeMessengerRecurso>();
            BK_Recurso = new StructBikeMessengerRecurso();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerRecurso> results = BM_ConexionLite.Table<TbBikeMessengerRecurso>().Where(t => t.PKRECURSO == VPKRECURSO).ToList();

            if (results.Count > 0)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    BK_Recurso = new StructBikeMessengerRecurso();
                    BK_Recurso.OPERACION = results[i].OPERACION;
                    BK_Recurso.PKRECURSO = results[i].PKRECURSO;
                    BK_Recurso.PENTALPHA = results[i].PENTALPHA;
                    BK_Recurso.PATENTE = results[i].PATENTE;
                    BK_Recurso.RUTID = results[i].RUTID;
                    BK_Recurso.DIGVER = results[i].DIGVER;
                    BK_Recurso.TIPO = results[i].TIPO;
                    BK_Recurso.MARCA = results[i].MARCA;
                    BK_Recurso.MODELO = results[i].MODELO;
                    BK_Recurso.VARIANTE = results[i].VARIANTE;
                    BK_Recurso.ANO = results[i].ANO;
                    BK_Recurso.COLOR = results[i].COLOR;
                    BK_Recurso.CIUDAD = results[i].CIUDAD;
                    BK_Recurso.COMUNA = results[i].COMUNA;
                    BK_Recurso.REGION = results[i].REGION;
                    BK_Recurso.PAIS = results[i].PAIS;
                    BK_Recurso.OBSERVACIONES = results[i].OBSERVACIONES;
                    BK_Recurso.FOTO = results[i].FOTO;
                    BK_Recurso.RESMENSAJE = "OK";
                    BK_Recurso.RESOPERACION = "OK";
                    BK_RecursoLista.Add(BK_Recurso);
                }
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return BK_RecursoLista;
        }

        public bool AgregarRecurso(StructBikeMessengerRecurso aBK_Recurso)
        {
            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                TbBikeMessengerRecurso record = new TbBikeMessengerRecurso
                {
                    OPERACION = aBK_Recurso.OPERACION,
                    PKRECURSO = aBK_Recurso.PKRECURSO,
                    PENTALPHA = aBK_Recurso.PENTALPHA,
                    PATENTE = aBK_Recurso.PATENTE,
                    RUTID = aBK_Recurso.RUTID,
                    DIGVER = aBK_Recurso.DIGVER,
                    TIPO = aBK_Recurso.TIPO,
                    MARCA = aBK_Recurso.MARCA,
                    MODELO = aBK_Recurso.MODELO,
                    VARIANTE = aBK_Recurso.VARIANTE,
                    ANO = aBK_Recurso.ANO,
                    COLOR = aBK_Recurso.COLOR,
                    CIUDAD = aBK_Recurso.CIUDAD,
                    COMUNA = aBK_Recurso.COMUNA,
                    REGION = aBK_Recurso.REGION,
                    PAIS = aBK_Recurso.PAIS,
                    OBSERVACIONES = aBK_Recurso.OBSERVACIONES,
                    FOTO = aBK_Recurso.FOTO,
                    RESMENSAJE = "OK",
                    RESOPERACION = "OK"
                };
                _ = BM_ConexionLite.InsertOrReplace(record);
            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return true;
        }


        public bool ModificarRecurso(StructBikeMessengerRecurso mBK_Recurso)
        {
            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                TbBikeMessengerRecurso record = new TbBikeMessengerRecurso
                {
                    OPERACION = mBK_Recurso.OPERACION,
                    PKRECURSO = mBK_Recurso.PKRECURSO,
                    PENTALPHA = mBK_Recurso.PENTALPHA,
                    PATENTE = mBK_Recurso.PATENTE,
                    RUTID = mBK_Recurso.RUTID,
                    DIGVER = mBK_Recurso.DIGVER,
                    TIPO = mBK_Recurso.TIPO,
                    MARCA = mBK_Recurso.MARCA,
                    MODELO = mBK_Recurso.MODELO,
                    VARIANTE = mBK_Recurso.VARIANTE,
                    ANO = mBK_Recurso.ANO,
                    COLOR = mBK_Recurso.COLOR,
                    CIUDAD = mBK_Recurso.CIUDAD,
                    COMUNA = mBK_Recurso.COMUNA,
                    REGION = mBK_Recurso.REGION,
                    PAIS = mBK_Recurso.PAIS,
                    OBSERVACIONES = mBK_Recurso.OBSERVACIONES,
                    FOTO = mBK_Recurso.FOTO,
                    RESMENSAJE = "OK",
                    RESOPERACION = "OK"
                };
                _ = BM_ConexionLite.InsertOrReplace(record);

            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return true;
        }

        public bool BorrarRecurso(string pPENTALPHA, string pPATENTE)
        {
            string VPKRECURSO = pPENTALPHA + pPATENTE;

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                _ = BM_ConexionLite.Delete<TbBikeMessengerRecurso>(VPKRECURSO);
            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return true;
        }

        public List<ClaseRecursoGrid> BuscarGridRecurso(string pPENTALPHA)
        {

            List<ClaseRecursoGrid> GridLocalRecursoLista = new List<ClaseRecursoGrid>();
            List<StructBikeMessengerRecurso> ListaLocalRecurso = new List<StructBikeMessengerRecurso>();

            ListaLocalRecurso = BuscarRecurso(pPENTALPHA);

            if (ListaLocalRecurso == null)
            {
                return null;
            }

            if (ListaLocalRecurso.Count > 0)
            {
                for (int i = 0; i < ListaLocalRecurso.Count; i++)
                {
                    ClaseRecursoGrid GridLocalRecurso = new ClaseRecursoGrid
                    {
                        PATENTE = ListaLocalRecurso[i].PATENTE,
                        TIPO = ListaLocalRecurso[i].TIPO,
                        MARCA = ListaLocalRecurso[i].MARCA,
                        MODELO = ListaLocalRecurso[i].MODELO,
                        RUTID = ListaLocalRecurso[i].RUTID,
                        DIGVER = ListaLocalRecurso[i].DIGVER,
                        CIUDAD = ListaLocalRecurso[i].CIUDAD
                    };
                    GridLocalRecursoLista.Add(GridLocalRecurso);
                }
                return GridLocalRecursoLista;

            }
            return null;
        }

        public List<ClasePersonalGrid> BuscarGridPersonal(string pPENTALPHA)
        {
            List<ClasePersonalGrid> GridLocalPersonalLista = new List<ClasePersonalGrid>();
            List<TbBikeMessengerPersonal> ListaLocalPersonal = new List<TbBikeMessengerPersonal>();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            ListaLocalPersonal = BM_ConexionLite.Table<TbBikeMessengerPersonal>().Where(t => t.PENTALPHA == pPENTALPHA).ToList();

            if (ListaLocalPersonal == null)
            {
                return null;
            }

            if (ListaLocalPersonal.Count > 0)
            {
                for (int i = 0; i < ListaLocalPersonal.Count; i++)
                {
                    ClasePersonalGrid GridLocalPersonal = new ClasePersonalGrid
                    {
                        RUTID = ListaLocalPersonal[i].RUTID,
                        DIGVER = ListaLocalPersonal[i].DIGVER,
                        APELLIDOS = ListaLocalPersonal[i].APELLIDOS,
                        NOMBRES = ListaLocalPersonal[i].NOMBRES
                    };
                    GridLocalPersonalLista.Add(GridLocalPersonal);
                }

                
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();
            
            return GridLocalPersonalLista;
        }

        public string Bm_BuscarNombrePropietario(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            string VNOMBRE = null;
            string VPKPERSONAL = pPENTALPHA + pRUTID + pDIGVER;

            List<StructBikeMessengerPersonal> BK_PersonalLista = new List<StructBikeMessengerPersonal>();
            StructBikeMessengerPersonal BK_Personal = new StructBikeMessengerPersonal();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerPersonal> results = BM_ConexionLite.Table<TbBikeMessengerPersonal>().Where(t => t.PKPERSONAL == VPKPERSONAL).ToList();

            if (results.Count > 0)
            {
                VNOMBRE = results[0].APELLIDOS + " " + results[0].NOMBRES;
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return VNOMBRE;
        }

        public string Bm_Recurso_Listado(string pPENTALPHA)
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

            BK_Recurso = new StructBikeMessengerRecurso();
            BK_RecursoLista = new List<StructBikeMessengerRecurso>();

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

            /*
            if (BM_TransferVar.SincronizarBaseLocal())
            {
                return null;
            }
            else if (BM_TransferVar.SincronizarBaseSQLServer())
            {
                DataSet BM_DataSet;
                SqlDataAdapter BM_Adaptador;
                SqlCommand BM_Comando;

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
                        DocumentoHtml.AgregarCampo(LvrRecurso["APELLIDOS"].ToString() + ", " + LvrRecurso["NOMBRES"].ToString(), false);
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
            }
            else if (BM_TransferVar.SincronizarWebPentalpha())
            {
                return null;
            }
            else if (BM_TransferVar.SincronizarWebPropio())
            {
                return null;
            }
            */
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