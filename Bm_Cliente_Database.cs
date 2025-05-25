using System.Collections.Generic;
using SQLite;

namespace BikeMessenger
{
    internal class Bm_Cliente_Database
    {
        private TransferVar BM_TransferVar = new TransferVar();

        // Valode de Memoria a Bases de Datos

        private StructBikeMessengerCliente BK_Cliente;
        private List<StructBikeMessengerCliente> BK_ClienteLista;

        private string CompletoNombreBD = "";

        public Bm_Cliente_Database()
        {
            CompletoNombreBD = BM_TransferVar.DIRECTORIO_BASE_LOCAL + "\\BikeMessenger.db";
        }

        public List<StructBikeMessengerCliente> BuscarCliente(string pPENTALPHA)
        {
            BK_ClienteLista = new List<StructBikeMessengerCliente>();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerCliente> results = BM_ConexionLite.Table<TbBikeMessengerCliente>().Where(t => t.PENTALPHA == pPENTALPHA).ToList();

            if (results.Count > 0)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    BK_Cliente = new StructBikeMessengerCliente
                    {
                        OPERACION = "BUSCAR",
                        PKCLIENTE = results[i].PKCLIENTE,
                        PENTALPHA = results[i].PENTALPHA,
                        RUTID = results[i].RUTID,
                        DIGVER = results[i].DIGVER,
                        NOMBRE = results[i].NOMBRE,
                        ACTIVIDAD1 = results[i].ACTIVIDAD1,
                        ACTIVIDAD2 = results[i].ACTIVIDAD2,
                        REPRESENTANTE1 = results[i].REPRESENTANTE1,
                        REPRESENTANTE2 = results[i].REPRESENTANTE2,
                        EMAIL = results[i].EMAIL,
                        TELEFONO1 = results[i].TELEFONO1,
                        TELEFONO2 = results[i].TELEFONO2,
                        DOMICILIO1 = results[i].DOMICILIO1,
                        DOMICILIO2 = results[i].DOMICILIO2,
                        NUMERO = results[i].NUMERO,
                        PISO = results[i].PISO,
                        OFICINA = results[i].OFICINA,
                        CODIGOPOSTAL = results[i].CODIGOPOSTAL,
                        CIUDAD = results[i].CIUDAD,
                        COMUNA = results[i].COMUNA,
                        ESTADOREGION = results[i].ESTADOREGION,
                        PAIS = results[i].PAIS,
                        OBSERVACIONES = results[i].OBSERVACIONES,
                        LOGO = results[i].LOGO,
                        RESMENSAJE = "OK",
                        RESOPERACION = "OK"
                    };
                    BK_ClienteLista.Add(BK_Cliente);
                }
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();
            BM_ConexionLite = null;

            return BK_ClienteLista;
        }

        public List<StructBikeMessengerCliente> BuscarCliente(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            string VPKCLIENTE = pPENTALPHA + pRUTID + pDIGVER;

            BK_ClienteLista = new List<StructBikeMessengerCliente>();
            BK_Cliente = new StructBikeMessengerCliente();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerCliente> results = BM_ConexionLite.Table<TbBikeMessengerCliente>().Where(t => t.PKCLIENTE == VPKCLIENTE).ToList();

            if (results.Count > 0)
            {
                BK_Cliente = new StructBikeMessengerCliente
                {
                    OPERACION = "BUSCAR",
                    PKCLIENTE = results[0].PKCLIENTE,
                    PENTALPHA = results[0].PENTALPHA,
                    RUTID = results[0].RUTID,
                    DIGVER = results[0].DIGVER,
                    NOMBRE = results[0].NOMBRE,
                    ACTIVIDAD1 = results[0].ACTIVIDAD1,
                    ACTIVIDAD2 = results[0].ACTIVIDAD2,
                    REPRESENTANTE1 = results[0].REPRESENTANTE1,
                    REPRESENTANTE2 = results[0].REPRESENTANTE2,
                    EMAIL = results[0].EMAIL,
                    TELEFONO1 = results[0].TELEFONO1,
                    TELEFONO2 = results[0].TELEFONO2,
                    DOMICILIO1 = results[0].DOMICILIO1,
                    DOMICILIO2 = results[0].DOMICILIO2,
                    NUMERO = results[0].NUMERO,
                    PISO = results[0].PISO,
                    OFICINA = results[0].OFICINA,
                    CODIGOPOSTAL = results[0].CODIGOPOSTAL,
                    CIUDAD = results[0].CIUDAD,
                    COMUNA = results[0].COMUNA,
                    ESTADOREGION = results[0].ESTADOREGION,
                    PAIS = results[0].PAIS,
                    OBSERVACIONES = results[0].OBSERVACIONES,
                    LOGO = results[0].LOGO,
                    RESMENSAJE = "OK",
                    RESOPERACION = "OK"
                };
                BK_ClienteLista.Add(BK_Cliente);
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();
            BM_ConexionLite = null;

            return BK_ClienteLista;
        }



        public bool AgregarCliente(StructBikeMessengerCliente aBK_Cliente)
        {
            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                TbBikeMessengerCliente record = new TbBikeMessengerCliente
                {
                    OPERACION = "AGREGAR",
                    PKCLIENTE = aBK_Cliente.PKCLIENTE,
                    PENTALPHA = aBK_Cliente.PENTALPHA,
                    RUTID = aBK_Cliente.RUTID,
                    DIGVER = aBK_Cliente.DIGVER,
                    NOMBRE = aBK_Cliente.NOMBRE,
                    ACTIVIDAD1 = aBK_Cliente.ACTIVIDAD1,
                    ACTIVIDAD2 = aBK_Cliente.ACTIVIDAD2,
                    REPRESENTANTE1 = aBK_Cliente.REPRESENTANTE1,
                    REPRESENTANTE2 = aBK_Cliente.REPRESENTANTE2,
                    EMAIL = aBK_Cliente.EMAIL,
                    TELEFONO1 = aBK_Cliente.TELEFONO1,
                    TELEFONO2 = aBK_Cliente.TELEFONO2,
                    DOMICILIO1 = aBK_Cliente.DOMICILIO1,
                    DOMICILIO2 = aBK_Cliente.DOMICILIO2,
                    NUMERO = aBK_Cliente.NUMERO,
                    PISO = aBK_Cliente.PISO,
                    OFICINA = aBK_Cliente.OFICINA,
                    CODIGOPOSTAL = aBK_Cliente.CODIGOPOSTAL,
                    CIUDAD = aBK_Cliente.CIUDAD,
                    COMUNA = aBK_Cliente.COMUNA,
                    ESTADOREGION = aBK_Cliente.ESTADOREGION,
                    PAIS = aBK_Cliente.PAIS,
                    OBSERVACIONES = aBK_Cliente.OBSERVACIONES,
                    LOGO = aBK_Cliente.LOGO,
                    RESMENSAJE = "OK",
                    RESOPERACION = "OK"
                };
                _ = BM_ConexionLite.InsertOrReplace(record);

            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return true;
        }


        public bool ModificarCliente(StructBikeMessengerCliente mBK_Cliente)
        {
            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                TbBikeMessengerCliente record = new TbBikeMessengerCliente
                {
                    OPERACION = "MODIFICAR",
                    PKCLIENTE = mBK_Cliente.PKCLIENTE,
                    PENTALPHA = mBK_Cliente.PENTALPHA,
                    RUTID = mBK_Cliente.RUTID,
                    DIGVER = mBK_Cliente.DIGVER,
                    NOMBRE = mBK_Cliente.NOMBRE,
                    ACTIVIDAD1 = mBK_Cliente.ACTIVIDAD1,
                    ACTIVIDAD2 = mBK_Cliente.ACTIVIDAD2,
                    REPRESENTANTE1 = mBK_Cliente.REPRESENTANTE1,
                    REPRESENTANTE2 = mBK_Cliente.REPRESENTANTE2,
                    EMAIL = mBK_Cliente.EMAIL,
                    TELEFONO1 = mBK_Cliente.TELEFONO1,
                    TELEFONO2 = mBK_Cliente.TELEFONO2,
                    DOMICILIO1 = mBK_Cliente.DOMICILIO1,
                    DOMICILIO2 = mBK_Cliente.DOMICILIO2,
                    NUMERO = mBK_Cliente.NUMERO,
                    PISO = mBK_Cliente.PISO,
                    OFICINA = mBK_Cliente.OFICINA,
                    CODIGOPOSTAL = mBK_Cliente.CODIGOPOSTAL,
                    CIUDAD = mBK_Cliente.CIUDAD,
                    COMUNA = mBK_Cliente.COMUNA,
                    ESTADOREGION = mBK_Cliente.ESTADOREGION,
                    PAIS = mBK_Cliente.PAIS,
                    OBSERVACIONES = mBK_Cliente.OBSERVACIONES,
                    LOGO = mBK_Cliente.LOGO,
                    RESMENSAJE = "OK",
                    RESOPERACION = "OK"
                };
                _ = BM_ConexionLite.InsertOrReplace(record);

            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return true;
        }


        public bool BorrarCliente(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            string VPKCLIENTE = pPENTALPHA + pRUTID + pDIGVER;

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                _ = BM_ConexionLite.Delete<TbBikeMessengerCliente>(VPKCLIENTE);
            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return true;
        }

        public List<ClaseClientesGrid> BuscarGridClientes(string pPENTALPHA)
        {
            List<ClaseClientesGrid> GridLocalClientesLista = new List<ClaseClientesGrid>();
            List<StructBikeMessengerCliente> ListaLocalClientes = new List<StructBikeMessengerCliente>();

            ListaLocalClientes = BuscarCliente(pPENTALPHA);

            if (ListaLocalClientes == null || ListaLocalClientes.Count <= 0)
            {
                return null;
            }

            if (ListaLocalClientes.Count > 0)
            {
                for (int i = 0; i < ListaLocalClientes.Count; i++)
                {
                    ClaseClientesGrid GridLocalClientes = new ClaseClientesGrid
                    {
                        RUTID = ListaLocalClientes[i].RUTID,
                        DIGVER = ListaLocalClientes[i].DIGVER,
                        NOMBRE = ListaLocalClientes[i].NOMBRE
                    };
                    GridLocalClientesLista.Add(GridLocalClientes);
                }
            }

            return GridLocalClientesLista;
        }

        public string Bm_Cliente_Listado(string pPENTALPHA)
        {
            List<StructBikeMessengerCliente> ListaLocalClientes = new List<StructBikeMessengerCliente>();

            ListaLocalClientes = BuscarCliente(pPENTALPHA);

            if (ListaLocalClientes == null || ListaLocalClientes.Count <= 0)
            {
                return null;
            }

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

            for (int i = 0; i < ListaLocalClientes.Count; i++)
            {
                DocumentoHtml.AbrirFila();
                DocumentoHtml.AgregarCampo(ListaLocalClientes[i].RUTID + " -" + ListaLocalClientes[i].DIGVER, false);
                DocumentoHtml.AgregarCampo(ListaLocalClientes[i].NOMBRE, false);
                DocumentoHtml.AgregarCampo(ListaLocalClientes[i].ACTIVIDAD1, false);
                DocumentoHtml.AgregarCampo(ListaLocalClientes[i].REPRESENTANTE1, false);
                DocumentoHtml.AgregarCampo(ListaLocalClientes[i].TELEFONO1, false);
                DocumentoHtml.AgregarCampo(ListaLocalClientes[i].DOMICILIO1, false);
                DocumentoHtml.AgregarCampo(ListaLocalClientes[i].NUMERO, false);
                DocumentoHtml.AgregarCampo(ListaLocalClientes[i].OFICINA, false);
                DocumentoHtml.AgregarCampo(ListaLocalClientes[i].CIUDAD, false);
                DocumentoHtml.AgregarCampo(ListaLocalClientes[i].COMUNA, false);
                DocumentoHtml.AgregarCampo(ListaLocalClientes[i].PAIS, false);
                DocumentoHtml.CerrarFila();
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