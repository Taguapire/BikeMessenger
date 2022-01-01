using System;
using System.Collections.Generic;
using SQLite;

namespace BikeMessenger
{
    internal class Bm_Servicio_Database
    {
        private TransferVar BM_TransferVar = new TransferVar();

        // Valode de Memoria a Bases de Datos

        private StructBikeMessengerServicio BK_Servicio;
        private List<StructBikeMessengerServicio> BK_ServicioLista;

        private string CompletoNombreBD = "";

        public Bm_Servicio_Database()
        {
            CompletoNombreBD = BM_TransferVar.DIRECTORIO_BASE_LOCAL + "\\BikeMessenger.db";
        }

        // Busqueda por Muchos
        public List<StructBikeMessengerServicio> BuscarServicio(string pPENTALPHA)
        {
            BK_ServicioLista = new List<StructBikeMessengerServicio>();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerServicio> results = BM_ConexionLite.Table<TbBikeMessengerServicio>().Where(t => t.PENTALPHA == pPENTALPHA).ToList();

            if (results.Count > 0)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    BK_Servicio = new StructBikeMessengerServicio
                    {
                        OPERACION = "BUSCAR",
                        PKSERVICIO = results[i].PKSERVICIO,
                        PENTALPHA = results[i].PENTALPHA,
                        NROENVIO = results[i].NROENVIO,
                        GUIADESPACHO = results[i].GUIADESPACHO,
                        FECHA = results[i].FECHA,
                        HORA = results[i].HORA,
                        CLIENTERUT = results[i].CLIENTERUT,
                        CLIENTEDIGVER = results[i].CLIENTEDIGVER,
                        MENSAJERORUT = results[i].MENSAJERORUT,
                        MENSAJERODIGVER = results[i].MENSAJERODIGVER,
                        RECURSOID = results[i].RECURSOID,
                        ODOMICILIO1 = results[i].ODOMICILIO1,
                        ONUMERO = results[i].ONUMERO,
                        OPISO = results[i].OPISO,
                        OOFICINA = results[i].OOFICINA,
                        OCIUDAD = results[i].OCIUDAD,
                        OCOMUNA = results[i].OCOMUNA,
                        OESTADO = results[i].OESTADO,
                        OPAIS = results[i].OPAIS,
                        OLATITUD = results[i].OLATITUD,
                        OLONGITUD = results[i].OLONGITUD,
                        DDOMICILIO1 = results[i].DDOMICILIO1,
                        DNUMERO = results[i].DNUMERO,
                        DPISO = results[i].DPISO,
                        DOFICINA = results[i].DOFICINA,
                        DCIUDAD = results[i].DCIUDAD,
                        DCOMUNA = results[i].DCOMUNA,
                        DESTADO = results[i].DESTADO,
                        DPAIS = results[i].DPAIS,
                        DLATITUD = results[i].DLATITUD,
                        DLONGITUD = results[i].DLONGITUD,
                        DESCRIPCION = results[i].DESCRIPCION,
                        FACTURAS = results[i].FACTURAS,
                        BULTOS = results[i].BULTOS,
                        COMPRAS = results[i].COMPRAS,
                        CHEQUES = results[i].CHEQUES,
                        SOBRES = results[i].SOBRES,
                        OTROS = results[i].OTROS,
                        OBSERVACIONES = results[i].OBSERVACIONES,
                        ENTREGA = results[i].ENTREGA,
                        RECEPCION = results[i].RECEPCION,
                        TESPERA = results[i].TESPERA,
                        FECHAENTREGA = results[i].FECHAENTREGA,
                        HORAENTREGA = results[i].HORAENTREGA,
                        DISTANCIA = results[i].DISTANCIA,
                        PROGRAMADO = results[i].PROGRAMADO,
                        RESMENSAJE = "OK",
                        RESOPERACION = "OK"
                    };
                    BK_ServicioLista.Add(BK_Servicio);
                }
            }
            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();
            BM_ConexionLite = null;

            return BK_ServicioLista;
        }

        public List<StructBikeMessengerServicio> BuscarServicio(string pPENTALPHA, string pNROENVIO)
        {
            string VPKSERVICIO = pPENTALPHA + pNROENVIO;

            BK_ServicioLista = new List<StructBikeMessengerServicio>();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerServicio> results = BM_ConexionLite.Table<TbBikeMessengerServicio>().Where(t => t.PKSERVICIO == VPKSERVICIO).ToList();

            if (results.Count > 0)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    BK_Servicio = new StructBikeMessengerServicio
                    {
                        OPERACION = "BUSCAR",
                        PKSERVICIO = results[i].PKSERVICIO,
                        PENTALPHA = results[i].PENTALPHA,
                        NROENVIO = results[i].NROENVIO,
                        GUIADESPACHO = results[i].GUIADESPACHO,
                        FECHA = results[i].FECHA,
                        HORA = results[i].HORA,
                        CLIENTERUT = results[i].CLIENTERUT,
                        CLIENTEDIGVER = results[i].CLIENTEDIGVER,
                        MENSAJERORUT = results[i].MENSAJERORUT,
                        MENSAJERODIGVER = results[i].MENSAJERODIGVER,
                        RECURSOID = results[i].RECURSOID,
                        ODOMICILIO1 = results[i].ODOMICILIO1,
                        ONUMERO = results[i].ONUMERO,
                        OPISO = results[i].OPISO,
                        OOFICINA = results[i].OOFICINA,
                        OCIUDAD = results[i].OCIUDAD,
                        OCOMUNA = results[i].OCOMUNA,
                        OESTADO = results[i].OESTADO,
                        OPAIS = results[i].OPAIS,
                        OLATITUD = results[i].OLATITUD,
                        OLONGITUD = results[i].OLONGITUD,
                        DDOMICILIO1 = results[i].DDOMICILIO1,
                        DNUMERO = results[i].DNUMERO,
                        DPISO = results[i].DPISO,
                        DOFICINA = results[i].DOFICINA,
                        DCIUDAD = results[i].DCIUDAD,
                        DCOMUNA = results[i].DCOMUNA,
                        DESTADO = results[i].DESTADO,
                        DPAIS = results[i].DPAIS,
                        DLATITUD = results[i].DLATITUD,
                        DLONGITUD = results[i].DLONGITUD,
                        DESCRIPCION = results[i].DESCRIPCION,
                        FACTURAS = results[i].FACTURAS,
                        BULTOS = results[i].BULTOS,
                        COMPRAS = results[i].COMPRAS,
                        CHEQUES = results[i].CHEQUES,
                        SOBRES = results[i].SOBRES,
                        OTROS = results[i].OTROS,
                        OBSERVACIONES = results[i].OBSERVACIONES,
                        ENTREGA = results[i].ENTREGA,
                        RECEPCION = results[i].RECEPCION,
                        TESPERA = results[i].TESPERA,
                        FECHAENTREGA = results[i].FECHAENTREGA,
                        HORAENTREGA = results[i].HORAENTREGA,
                        DISTANCIA = results[i].DISTANCIA,
                        PROGRAMADO = results[i].PROGRAMADO,
                        RESMENSAJE = "OK",
                        RESOPERACION = "OK"
                    };
                    BK_ServicioLista.Add(BK_Servicio);
                }
            }
            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();
            BM_ConexionLite = null;

            return BK_ServicioLista;
        }

        public bool AgregarServicio(StructBikeMessengerServicio aBK_Servicio)
        {
            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                TbBikeMessengerServicio record = new TbBikeMessengerServicio
                {
                    OPERACION = "AGREGAR",
                    PKSERVICIO = aBK_Servicio.PKSERVICIO,
                    PENTALPHA = aBK_Servicio.PENTALPHA,
                    NROENVIO = aBK_Servicio.NROENVIO,
                    GUIADESPACHO = aBK_Servicio.GUIADESPACHO,
                    FECHA = aBK_Servicio.FECHA,
                    HORA = aBK_Servicio.HORA,
                    CLIENTERUT = aBK_Servicio.CLIENTERUT,
                    CLIENTEDIGVER = aBK_Servicio.CLIENTEDIGVER,
                    MENSAJERORUT = aBK_Servicio.MENSAJERORUT,
                    MENSAJERODIGVER = aBK_Servicio.MENSAJERODIGVER,
                    RECURSOID = aBK_Servicio.RECURSOID,
                    ODOMICILIO1 = aBK_Servicio.ODOMICILIO1,
                    ONUMERO = aBK_Servicio.ONUMERO,
                    OPISO = aBK_Servicio.OPISO,
                    OOFICINA = aBK_Servicio.OOFICINA,
                    OCIUDAD = aBK_Servicio.OCIUDAD,
                    OCOMUNA = aBK_Servicio.OCOMUNA,
                    OESTADO = aBK_Servicio.OESTADO,
                    OPAIS = aBK_Servicio.OPAIS,
                    OLATITUD = aBK_Servicio.OLATITUD,
                    OLONGITUD = aBK_Servicio.OLONGITUD,
                    DDOMICILIO1 = aBK_Servicio.DDOMICILIO1,
                    DNUMERO = aBK_Servicio.DNUMERO,
                    DPISO = aBK_Servicio.DPISO,
                    DOFICINA = aBK_Servicio.DOFICINA,
                    DCIUDAD = aBK_Servicio.DCIUDAD,
                    DCOMUNA = aBK_Servicio.DCOMUNA,
                    DESTADO = aBK_Servicio.DESTADO,
                    DPAIS = aBK_Servicio.DPAIS,
                    DLATITUD = aBK_Servicio.DLATITUD,
                    DLONGITUD = aBK_Servicio.DLONGITUD,
                    DESCRIPCION = aBK_Servicio.DESCRIPCION,
                    FACTURAS = aBK_Servicio.FACTURAS,
                    BULTOS = aBK_Servicio.BULTOS,
                    COMPRAS = aBK_Servicio.COMPRAS,
                    CHEQUES = aBK_Servicio.CHEQUES,
                    SOBRES = aBK_Servicio.SOBRES,
                    OTROS = aBK_Servicio.OTROS,
                    OBSERVACIONES = aBK_Servicio.OBSERVACIONES,
                    ENTREGA = aBK_Servicio.ENTREGA,
                    RECEPCION = aBK_Servicio.RECEPCION,
                    TESPERA = aBK_Servicio.TESPERA,
                    FECHAENTREGA = aBK_Servicio.FECHAENTREGA,
                    HORAENTREGA = aBK_Servicio.HORAENTREGA,
                    DISTANCIA = aBK_Servicio.DISTANCIA,
                    PROGRAMADO = aBK_Servicio.PROGRAMADO,
                    RESMENSAJE = "OK",
                    RESOPERACION = "OK"
                };
                _ = BM_ConexionLite.InsertOrReplace(record);

            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();
            BM_ConexionLite = null;

            return true;
        }


        public bool ModificarServicio(StructBikeMessengerServicio mBK_Servicio)
        {
            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                TbBikeMessengerServicio record = new TbBikeMessengerServicio
                {
                    OPERACION = "AGREGAR",
                    PKSERVICIO = mBK_Servicio.PKSERVICIO,
                    PENTALPHA = mBK_Servicio.PENTALPHA,
                    NROENVIO = mBK_Servicio.NROENVIO,
                    GUIADESPACHO = mBK_Servicio.GUIADESPACHO,
                    FECHA = mBK_Servicio.FECHA,
                    HORA = mBK_Servicio.HORA,
                    CLIENTERUT = mBK_Servicio.CLIENTERUT,
                    CLIENTEDIGVER = mBK_Servicio.CLIENTEDIGVER,
                    MENSAJERORUT = mBK_Servicio.MENSAJERORUT,
                    MENSAJERODIGVER = mBK_Servicio.MENSAJERODIGVER,
                    RECURSOID = mBK_Servicio.RECURSOID,
                    ODOMICILIO1 = mBK_Servicio.ODOMICILIO1,
                    ONUMERO = mBK_Servicio.ONUMERO,
                    OPISO = mBK_Servicio.OPISO,
                    OOFICINA = mBK_Servicio.OOFICINA,
                    OCIUDAD = mBK_Servicio.OCIUDAD,
                    OCOMUNA = mBK_Servicio.OCOMUNA,
                    OESTADO = mBK_Servicio.OESTADO,
                    OPAIS = mBK_Servicio.OPAIS,
                    OLATITUD = mBK_Servicio.OLATITUD,
                    OLONGITUD = mBK_Servicio.OLONGITUD,
                    DDOMICILIO1 = mBK_Servicio.DDOMICILIO1,
                    DNUMERO = mBK_Servicio.DNUMERO,
                    DPISO = mBK_Servicio.DPISO,
                    DOFICINA = mBK_Servicio.DOFICINA,
                    DCIUDAD = mBK_Servicio.DCIUDAD,
                    DCOMUNA = mBK_Servicio.DCOMUNA,
                    DESTADO = mBK_Servicio.DESTADO,
                    DPAIS = mBK_Servicio.DPAIS,
                    DLATITUD = mBK_Servicio.DLATITUD,
                    DLONGITUD = mBK_Servicio.DLONGITUD,
                    DESCRIPCION = mBK_Servicio.DESCRIPCION,
                    FACTURAS = mBK_Servicio.FACTURAS,
                    BULTOS = mBK_Servicio.BULTOS,
                    COMPRAS = mBK_Servicio.COMPRAS,
                    CHEQUES = mBK_Servicio.CHEQUES,
                    SOBRES = mBK_Servicio.SOBRES,
                    OTROS = mBK_Servicio.OTROS,
                    OBSERVACIONES = mBK_Servicio.OBSERVACIONES,
                    ENTREGA = mBK_Servicio.ENTREGA,
                    RECEPCION = mBK_Servicio.RECEPCION,
                    TESPERA = mBK_Servicio.TESPERA,
                    FECHAENTREGA = mBK_Servicio.FECHAENTREGA,
                    HORAENTREGA = mBK_Servicio.HORAENTREGA,
                    DISTANCIA = mBK_Servicio.DISTANCIA,
                    PROGRAMADO = mBK_Servicio.PROGRAMADO,
                    RESMENSAJE = "OK",
                    RESOPERACION = "OK"
                };
                _ = BM_ConexionLite.InsertOrReplace(record);

            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();
            BM_ConexionLite = null;

            return true;
        }


        public bool BorrarServicio(string pPENTALPHA, string pNROENVIO)
        {
            string VPKSERVICIO = pPENTALPHA + pNROENVIO;

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                _ = BM_ConexionLite.Delete<TbBikeMessengerServicio>(VPKSERVICIO);
            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();
            return true;
        }

        // Utilitarios

        public List<ClaseServicioGrid> BuscarGridServicios(string pPENTALPHA)
        {
            List<ClaseServicioGrid> GridLocalServicioLista = new List<ClaseServicioGrid>();
            List<TbBikeMessengerServicio> ListaLocalServicio = new List<TbBikeMessengerServicio>();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            ListaLocalServicio = BM_ConexionLite.Table<TbBikeMessengerServicio>().Where(t => t.PENTALPHA == pPENTALPHA).ToList();

            if (ListaLocalServicio == null)
            {
                return null;
            }

            if (ListaLocalServicio.Count > 0)
            {
                for (int i = 0; i < ListaLocalServicio.Count; i++)
                {
                    ClaseServicioGrid GridLocalServicio = new ClaseServicioGrid
                    {
                        ENVIO = ListaLocalServicio[i].NROENVIO,
                        FECHA = ListaLocalServicio[i].FECHA,
                        CLIENTERUT = ListaLocalServicio[i].CLIENTERUT,
                        CLIENTEDIGVER = ListaLocalServicio[i].CLIENTEDIGVER,
                        ENTREGA = ListaLocalServicio[i].ENTREGA
                    };
                    GridLocalServicioLista.Add(GridLocalServicio);
                }


            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return GridLocalServicioLista;
        }




        public List<ClaseRecursoGrid> BuscarGridRecurso(string pPENTALPHA)
        {
            List<ClaseRecursoGrid> GridLocalRecursoLista = new List<ClaseRecursoGrid>();
            List<TbBikeMessengerRecurso> ListaLocalRecurso = new List<TbBikeMessengerRecurso>();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            ListaLocalRecurso = BM_ConexionLite.Table<TbBikeMessengerRecurso>().Where(t => t.PENTALPHA == pPENTALPHA).ToList();

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
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return GridLocalRecursoLista;
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


        public List<ClaseClientesGrid> BuscarGridClientes(string pPENTALPHA)
        {
            List<ClaseClientesGrid> GridLocalClienteLista = new List<ClaseClientesGrid>();
            List<TbBikeMessengerCliente> ListaLocalCliente = new List<TbBikeMessengerCliente>();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            ListaLocalCliente = BM_ConexionLite.Table<TbBikeMessengerCliente>().Where(t => t.PENTALPHA == pPENTALPHA).ToList();

            if (ListaLocalCliente == null || ListaLocalCliente.Count <= 0)
            {
                return null;
            }

            if (ListaLocalCliente.Count > 0)
            {
                for (int i = 0; i < ListaLocalCliente.Count; i++)
                {
                    ClaseClientesGrid GridLocalClientes = new ClaseClientesGrid
                    {
                        RUTID = ListaLocalCliente[i].RUTID,
                        DIGVER = ListaLocalCliente[i].DIGVER,
                        NOMBRE = ListaLocalCliente[i].NOMBRE
                    };
                    GridLocalClienteLista.Add(GridLocalClientes);
                }
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return GridLocalClienteLista;
        }

        public string Bm_BuscarNombreCliente(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            string VNOMBRE = "";
            string VPKCLIENTE = pPENTALPHA + pRUTID + pDIGVER;

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerCliente> results = BM_ConexionLite.Table<TbBikeMessengerCliente>().Where(t => t.PKCLIENTE == VPKCLIENTE).ToList();

            if (results.Count > 0)
            {
                VNOMBRE = results[0].NOMBRE;
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return VNOMBRE;
        }

        public string Bm_BuscarNombreMensajero(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            string VNOMBRE = "";
            string VPKMENSAJERO = pPENTALPHA + pRUTID + pDIGVER;

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerPersonal> results = BM_ConexionLite.Table<TbBikeMessengerPersonal>().Where(t => t.PKPERSONAL == VPKMENSAJERO).ToList();

            if (results.Count > 0)
            {
                VNOMBRE = results[0].APELLIDOS + " " + results[0].NOMBRES;
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return VNOMBRE;
        }

        public string Bm_BuscarNombreRecurso(string pPENTALPHA, string pPATENTE)
        {
            string VNOMBRE = "";
            string VPKRECURSO = pPENTALPHA + pPATENTE;

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerRecurso> results = BM_ConexionLite.Table<TbBikeMessengerRecurso>().Where(t => t.PKRECURSO == VPKRECURSO).ToList();

            if (results.Count > 0)
            {
                VNOMBRE = results[0].TIPO + " " + results[0].MARCA + " " + results[0].MODELO + " " + results[0].ANO;
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return VNOMBRE;
        }

        public string Bm_Servicio_Listado()
        {
            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

            BK_Servicio = new StructBikeMessengerServicio();
            BK_ServicioLista = new List<StructBikeMessengerServicio>();

            LvrTablaHtml DocumentoHtml = new LvrTablaHtml();

            DocumentoHtml.CrearTexto("SERVICIOS");
            DocumentoHtml.InicioDocumento();
            DocumentoHtml.AgregarTituloTabla("Listado de Servicios");
            DocumentoHtml.AbrirEncabezado();
            DocumentoHtml.AgregarEncabezado("Nro de Envío");
            DocumentoHtml.AgregarEncabezado("Guia de Despacho");
            DocumentoHtml.AgregarEncabezado("Fecha de Ingreso");
            DocumentoHtml.AgregarEncabezado("Hora de Ingreso");
            DocumentoHtml.AgregarEncabezado("Cliente");
            DocumentoHtml.AgregarEncabezado("Mensajero");
            DocumentoHtml.AgregarEncabezado("Entrega");
            DocumentoHtml.AgregarEncabezado("Recepción");
            DocumentoHtml.AgregarEncabezado("Distancia");
            DocumentoHtml.CerrarEncabezado();

            List<TbVistaServicioCliMen> results = BM_ConexionLite.Query<TbVistaServicioCliMen>("select * from Vista_Servicio_CliMen");

            for (int i = 0; i < results.Count; i++)
            {
                DocumentoHtml.AbrirFila();
                DocumentoHtml.AgregarCampo(results[i].NROENVIO, false);
                DocumentoHtml.AgregarCampo(results[i].GUIADESPACHO, false);
                DocumentoHtml.AgregarCampo(results[i].FECHAENTREGA, false);
                DocumentoHtml.AgregarCampo(results[i].HORAENTREGA, false);
                DocumentoHtml.AgregarCampo(results[i].NOMBRE, false);
                DocumentoHtml.AgregarCampo(results[i].APELLIDOS + ", " + results[i].NOMBRES, false);
                DocumentoHtml.AgregarCampo(results[i].ENTREGA, false);
                DocumentoHtml.AgregarCampo(results[i].RECEPCION, false);
                DocumentoHtml.AgregarCampo(results[i].DISTANCIA.ToString(), false);
                DocumentoHtml.CerrarFila();
            }

            DocumentoHtml.FinDocumento();

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return DocumentoHtml.BufferHtml;
        }
    }

    public class ClaseServicioGrid
    {
        public string ENVIO { get; set; }
        public string FECHA { get; set; }
        public string CLIENTERUT { get; set; }
        public string CLIENTEDIGVER { get; set; }
        public string ENTREGA { get; set; }
    }
}

