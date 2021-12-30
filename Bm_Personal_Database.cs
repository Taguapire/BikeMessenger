using System;
using System.Collections.Generic;
using SQLite;

namespace BikeMessenger
{
    internal class Bm_Personal_Database
    {
        private TransferVar BM_TransferVar = new TransferVar();

        // Valode de Memoria a Bases de Datos

        private StructBikeMessengerPersonal BK_Personal;
        private List<StructBikeMessengerPersonal> BK_PersonalLista;

        private string CompletoNombreBD = "";

        public Bm_Personal_Database()
        {
            CompletoNombreBD = BM_TransferVar.DIRECTORIO_BASE_LOCAL + "\\BikeMessenger.db";
        }

        // Busqueda por Muchos
        public List<StructBikeMessengerPersonal> BuscarPersonal(string pPENTALPHA)
        {
            BK_PersonalLista = new List<StructBikeMessengerPersonal>();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerPersonal> results = BM_ConexionLite.Table<TbBikeMessengerPersonal>().Where(t => t.PENTALPHA == pPENTALPHA).ToList();

            if (results.Count > 0)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    BK_Personal = new StructBikeMessengerPersonal();
                    BK_Personal.OPERACION = "BUSCAR";
                    BK_Personal.PKPERSONAL = results[i].PKPERSONAL;
                    BK_Personal.PENTALPHA = results[i].PENTALPHA;
                    BK_Personal.RUTID = results[i].RUTID;
                    BK_Personal.DIGVER = results[i].DIGVER;
                    BK_Personal.APELLIDOS = results[i].APELLIDOS;
                    BK_Personal.NOMBRES = results[i].NOMBRES;
                    BK_Personal.TELEFONO1 = results[i].TELEFONO1;
                    BK_Personal.TELEFONO2 = results[i].TELEFONO2;
                    BK_Personal.EMAIL = results[i].EMAIL;
                    BK_Personal.AUTORIZACION = results[i].AUTORIZACION;
                    BK_Personal.CARGO = results[i].CARGO;
                    BK_Personal.DOMICILIO = results[i].DOMICILIO;
                    BK_Personal.NUMERO = results[i].NUMERO;
                    BK_Personal.PISO = results[i].PISO;
                    BK_Personal.DPTO = results[i].DPTO;
                    BK_Personal.CODIGOPOSTAL = results[i].CODIGOPOSTAL;
                    BK_Personal.CIUDAD = results[i].CIUDAD;
                    BK_Personal.COMUNA = results[i].COMUNA;
                    BK_Personal.REGION = results[i].REGION;
                    BK_Personal.PAIS = results[i].PAIS;
                    BK_Personal.OBSERVACIONES = results[i].OBSERVACIONES;
                    BK_Personal.FOTO = results[i].FOTO;
                    BK_Personal.RESMENSAJE = "OK";
                    BK_Personal.RESOPERACION = "OK";
                    BK_PersonalLista.Add(BK_Personal);
                }
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return BK_PersonalLista;
        }

        public List<StructBikeMessengerPersonal> BuscarPersonal(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            string VPKPERSONAL = pPENTALPHA + pRUTID + pDIGVER;

            BK_PersonalLista = new List<StructBikeMessengerPersonal>();
            BK_Personal = new StructBikeMessengerPersonal();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerPersonal> results = BM_ConexionLite.Table<TbBikeMessengerPersonal>().Where(t => t.PKPERSONAL == VPKPERSONAL).ToList();

            if (results.Count > 0)
            {
                BK_Personal.OPERACION = "BUSCAR";
                BK_Personal.PKPERSONAL = results[0].PKPERSONAL;
                BK_Personal.PENTALPHA = results[0].PENTALPHA;
                BK_Personal.RUTID = results[0].RUTID;
                BK_Personal.DIGVER = results[0].DIGVER;
                BK_Personal.APELLIDOS = results[0].APELLIDOS;
                BK_Personal.NOMBRES = results[0].NOMBRES;
                BK_Personal.TELEFONO1 = results[0].TELEFONO1;
                BK_Personal.TELEFONO2 = results[0].TELEFONO2;
                BK_Personal.EMAIL = results[0].EMAIL;
                BK_Personal.AUTORIZACION = results[0].AUTORIZACION;
                BK_Personal.CARGO = results[0].CARGO;
                BK_Personal.DOMICILIO = results[0].DOMICILIO;
                BK_Personal.NUMERO = results[0].NUMERO;
                BK_Personal.PISO = results[0].PISO;
                BK_Personal.DPTO = results[0].DPTO;
                BK_Personal.CODIGOPOSTAL = results[0].CODIGOPOSTAL;
                BK_Personal.CIUDAD = results[0].CIUDAD;
                BK_Personal.COMUNA = results[0].COMUNA;
                BK_Personal.REGION = results[0].REGION;
                BK_Personal.PAIS = results[0].PAIS;
                BK_Personal.OBSERVACIONES = results[0].OBSERVACIONES;
                BK_Personal.FOTO = results[0].FOTO;
                BK_Personal.RESMENSAJE = "OK";
                BK_Personal.RESOPERACION = "OK";
                BK_PersonalLista.Add(BK_Personal);
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return BK_PersonalLista;
        }

        public bool AgregarPersonal(StructBikeMessengerPersonal aBK_Personal)
        {
            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                TbBikeMessengerPersonal record = new TbBikeMessengerPersonal
                {
                    OPERACION = "AGREGAR",
                    PKPERSONAL = aBK_Personal.PKPERSONAL,
                    PENTALPHA = aBK_Personal.PENTALPHA,
                    RUTID = aBK_Personal.RUTID,
                    DIGVER = aBK_Personal.DIGVER,
                    APELLIDOS = aBK_Personal.APELLIDOS,
                    NOMBRES = aBK_Personal.NOMBRES,
                    TELEFONO1 = aBK_Personal.TELEFONO1,
                    TELEFONO2 = aBK_Personal.TELEFONO2,
                    EMAIL = aBK_Personal.EMAIL,
                    AUTORIZACION = aBK_Personal.AUTORIZACION,
                    CARGO = aBK_Personal.CARGO,
                    DOMICILIO = aBK_Personal.DOMICILIO,
                    NUMERO = aBK_Personal.NUMERO,
                    PISO = aBK_Personal.PISO,
                    DPTO = aBK_Personal.DPTO,
                    CODIGOPOSTAL = aBK_Personal.CODIGOPOSTAL,
                    CIUDAD = aBK_Personal.CIUDAD,
                    COMUNA = aBK_Personal.COMUNA,
                    REGION = aBK_Personal.REGION,
                    PAIS = aBK_Personal.PAIS,
                    OBSERVACIONES = aBK_Personal.OBSERVACIONES,
                    FOTO = aBK_Personal.FOTO,
                    RESMENSAJE = "OK",
                    RESOPERACION = "OK"
                };
                _ = BM_ConexionLite.InsertOrReplace(record);

            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return true;
        }


        public bool ModificarPersonal(StructBikeMessengerPersonal mBK_Personal)
        {
            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                TbBikeMessengerPersonal record = new TbBikeMessengerPersonal
                {
                    OPERACION = "MODIFICAR",
                    PKPERSONAL = mBK_Personal.PKPERSONAL,
                    PENTALPHA = mBK_Personal.PENTALPHA,
                    RUTID = mBK_Personal.RUTID,
                    DIGVER = mBK_Personal.DIGVER,
                    APELLIDOS = mBK_Personal.APELLIDOS,
                    NOMBRES = mBK_Personal.NOMBRES,
                    TELEFONO1 = mBK_Personal.TELEFONO1,
                    TELEFONO2 = mBK_Personal.TELEFONO2,
                    EMAIL = mBK_Personal.EMAIL,
                    AUTORIZACION = mBK_Personal.AUTORIZACION,
                    CARGO = mBK_Personal.CARGO,
                    DOMICILIO = mBK_Personal.DOMICILIO,
                    NUMERO = mBK_Personal.NUMERO,
                    PISO = mBK_Personal.PISO,
                    DPTO = mBK_Personal.DPTO,
                    CODIGOPOSTAL = mBK_Personal.CODIGOPOSTAL,
                    CIUDAD = mBK_Personal.CIUDAD,
                    COMUNA = mBK_Personal.COMUNA,
                    REGION = mBK_Personal.REGION,
                    PAIS = mBK_Personal.PAIS,
                    OBSERVACIONES = mBK_Personal.OBSERVACIONES,
                    FOTO = mBK_Personal.FOTO,
                    RESMENSAJE = "OK",
                    RESOPERACION = "OK"
                };
                _ = BM_ConexionLite.InsertOrReplace(record);

            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return true;
        }

        public bool BorrarPersonal(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            string VPKPERSONAL = pPENTALPHA + pRUTID + pDIGVER;

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                _ = BM_ConexionLite.Delete<TbBikeMessengerPersonal>(VPKPERSONAL);
            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();
            return true;
        }

        public List<ClasePersonalGrid> BuscarGridPersonal(string pPENTALPHA)
        {
            List<ClasePersonalGrid> GridLocalPersonalLista = new List<ClasePersonalGrid>();
            List<StructBikeMessengerPersonal> ListaLocalPersonal = new List<StructBikeMessengerPersonal>();

            ListaLocalPersonal = BuscarPersonal(pPENTALPHA);

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
                return GridLocalPersonalLista;
            }
            return null;
        }

        public string Bm_Personal_Listado(string pPENTALPHA)
        {
            List<StructBikeMessengerPersonal> ListaLocalPersonal = new List<StructBikeMessengerPersonal>();
            LvrTablaHtml DocumentoHtml = new LvrTablaHtml();

            ListaLocalPersonal = BuscarPersonal(pPENTALPHA);

            if (ListaLocalPersonal.Count > 0)
            {
                DocumentoHtml.CrearTexto("PERSONAL");
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

                for (int i = 0; i < ListaLocalPersonal.Count; i++)
                {
                    DocumentoHtml.AbrirFila();
                    DocumentoHtml.AgregarCampo(ListaLocalPersonal[i].RUTID + "-" + ListaLocalPersonal[i].DIGVER, false);
                    DocumentoHtml.AgregarCampo(ListaLocalPersonal[i].APELLIDOS, false);
                    DocumentoHtml.AgregarCampo(ListaLocalPersonal[i].NOMBRES, false);
                    DocumentoHtml.AgregarCampo(ListaLocalPersonal[i].TELEFONO1, false);
                    DocumentoHtml.AgregarCampo(ListaLocalPersonal[i].TELEFONO2, false);
                    DocumentoHtml.AgregarCampo(ListaLocalPersonal[i].EMAIL, false);
                    DocumentoHtml.AgregarCampo(ListaLocalPersonal[i].AUTORIZACION, false);
                    DocumentoHtml.AgregarCampo(ListaLocalPersonal[i].CARGO, false);
                    DocumentoHtml.AgregarCampo(ListaLocalPersonal[i].CIUDAD, false);
                    DocumentoHtml.CerrarFila();
                }

                DocumentoHtml.FinDocumento();
                return DocumentoHtml.BufferHtml;
            }
            return null;
        }
    }

    public class ClasePersonalGrid
    {
        public string RUTID { get; set; }
        public string DIGVER { get; set; }
        public string APELLIDOS { get; set; }
        public string NOMBRES { get; set; }
    }
}

