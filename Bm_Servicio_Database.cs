using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeMessenger
{
    internal class Bm_Servicio_Database
    {
        private static JsonBikeMessengerServicio BK_Servicio;
        private static List<JsonBikeMessengerServicio> BK_ServicioLista;

        private static List<string> BK_Pais;
        private static List<string> BK_Region;
        private static List<string> BK_Comuna;
        private static List<string> BK_Ciudad;

        // Busqueda por Muchos
        public List<JsonBikeMessengerServicio> BuscarServicio()
        {
            BK_ServicioLista = null;
            using (var db = new BK_SQliteContext())
            {
                foreach (var LvrServicio in db.SERVICIOs)
                {
                    BK_Servicio = null;
                    BK_Servicio.PENTALPHA = LvrServicio.PENTALPHA;
                    BK_Servicio.NROENVIO = LvrServicio.NROENVIO;
                    BK_Servicio.GUIADESPACHO = LvrServicio.GUIADESPACHO;
                    BK_Servicio.FECHA = LvrServicio.FECHA;
                    BK_Servicio.HORA = LvrServicio.HORA;
                    BK_Servicio.CLIENTERUT = LvrServicio.CLIENTERUT;
                    BK_Servicio.CLIENTEDIGVER = LvrServicio.CLIENTEDIGVER;
                    // BK_Servicio.CLIENTE = LvrServicio.CLIENTE;
                    BK_Servicio.MENSAJERORUT = LvrServicio.MENSAJERORUT;
                    BK_Servicio.MENSAJERODIGVER = LvrServicio.MENSAJERODIGVER;
                    // BK_Servicio.MENSAJERO = LvrServicio.MENSAJERO;
                    BK_Servicio.RECURSOID = LvrServicio.RECURSOID;
                    // BK_Servicio.RECURSO = LvrServicio.RECURSO;
                    BK_Servicio.ODOMICILIO1 = LvrServicio.ODOMICILIO1;
                    BK_Servicio.ODOMICILIO2 = LvrServicio.ODOMICILIO2;
                    BK_Servicio.ONUMERO = LvrServicio.ONUMERO;
                    BK_Servicio.OPISO = LvrServicio.OPISO;
                    BK_Servicio.OOFICINA = LvrServicio.OOFICINA;
                    BK_Servicio.OCIUDAD = LvrServicio.OCIUDAD;
                    BK_Servicio.OCOMUNA = LvrServicio.OCOMUNA;
                    BK_Servicio.OESTADO = LvrServicio.OESTADO;
                    BK_Servicio.OPAIS = LvrServicio.OPAIS;
                    BK_Servicio.OCOORDENADAS = LvrServicio.OCOORDENADAS;
                    BK_Servicio.DDOMICILIO1 = LvrServicio.DDOMICILIO1;
                    BK_Servicio.DDOMICILIO2 = LvrServicio.DDOMICILIO2;
                    BK_Servicio.DNUMERO = LvrServicio.DNUMERO;
                    BK_Servicio.DPISO = LvrServicio.DPISO;
                    BK_Servicio.DOFICINA = LvrServicio.DOFICINA;
                    BK_Servicio.DCIUDAD = LvrServicio.DCIUDAD;
                    BK_Servicio.DCOMUNA = LvrServicio.DCOMUNA;
                    BK_Servicio.DESTADO = LvrServicio.DESTADO;
                    BK_Servicio.DPAIS = LvrServicio.DPAIS;
                    BK_Servicio.DCOORDENADAS = LvrServicio.DCOORDENADAS;
                    BK_Servicio.DESCRIPCION = LvrServicio.DESCRIPCION;
                    BK_Servicio.FACTURAS = LvrServicio.FACTURAS;
                    BK_Servicio.BULTOS = LvrServicio.BULTOS;
                    BK_Servicio.COMPRAS = LvrServicio.COMPRAS;
                    BK_Servicio.CHEQUES = LvrServicio.CHEQUES;
                    BK_Servicio.SOBRES = LvrServicio.SOBRES;
                    BK_Servicio.OTROS = LvrServicio.OTROS;
                    BK_Servicio.OBSERVACIONES = LvrServicio.OBSERVACIONES;
                    BK_Servicio.ENTREGA = LvrServicio.ENTREGA;
                    BK_Servicio.RECEPCION = LvrServicio.RECEPCION;
                    BK_Servicio.TESPERA = LvrServicio.TESPERA;
                    BK_Servicio.FECHAENTREGA = LvrServicio.FECHAENTREGA;
                    BK_Servicio.HORAENTREGA = LvrServicio.HORAENTREGA;
                    BK_Servicio.DISTANCIA = LvrServicio.DISTANCIA;
                    BK_Servicio.PROGRAMADO = LvrServicio.PROGRAMADO;
                    BK_ServicioLista.Add(BK_Servicio);
                }
            }
            return BK_ServicioLista;
        }

        public List<JsonBikeMessengerServicio> BuscarServicio(string pPENTALPHA, string pPATENTE)
        {
            BK_ServicioLista = null;
            using (var db = new BK_SQliteContext())
            {
                BK_Servicio = null;
                SERVICIO LvrServicio;
                LvrServicio = db.SERVICIOs.Find(pPENTALPHA, pPATENTE);
                BK_Servicio = null;
                BK_Servicio.PENTALPHA = LvrServicio.PENTALPHA;
                BK_Servicio.NROENVIO = LvrServicio.NROENVIO;
                BK_Servicio.GUIADESPACHO = LvrServicio.GUIADESPACHO;
                BK_Servicio.FECHA = LvrServicio.FECHA;
                BK_Servicio.HORA = LvrServicio.HORA;
                BK_Servicio.CLIENTERUT = LvrServicio.CLIENTERUT;
                BK_Servicio.CLIENTEDIGVER = LvrServicio.CLIENTEDIGVER;
                // BK_Servicio.CLIENTE = LvrServicio.CLIENTE;
                BK_Servicio.MENSAJERORUT = LvrServicio.MENSAJERORUT;
                BK_Servicio.MENSAJERODIGVER = LvrServicio.MENSAJERODIGVER;
                // BK_Servicio.MENSAJERO = LvrServicio.MENSAJERO;
                BK_Servicio.RECURSOID = LvrServicio.RECURSOID;
                // BK_Servicio.RECURSO = LvrServicio.RECURSO;
                BK_Servicio.ODOMICILIO1 = LvrServicio.ODOMICILIO1;
                BK_Servicio.ODOMICILIO2 = LvrServicio.ODOMICILIO2;
                BK_Servicio.ONUMERO = LvrServicio.ONUMERO;
                BK_Servicio.OPISO = LvrServicio.OPISO;
                BK_Servicio.OOFICINA = LvrServicio.OOFICINA;
                BK_Servicio.OCIUDAD = LvrServicio.OCIUDAD;
                BK_Servicio.OCOMUNA = LvrServicio.OCOMUNA;
                BK_Servicio.OESTADO = LvrServicio.OESTADO;
                BK_Servicio.OPAIS = LvrServicio.OPAIS;
                BK_Servicio.OCOORDENADAS = LvrServicio.OCOORDENADAS;
                BK_Servicio.DDOMICILIO1 = LvrServicio.DDOMICILIO1;
                BK_Servicio.DDOMICILIO2 = LvrServicio.DDOMICILIO2;
                BK_Servicio.DNUMERO = LvrServicio.DNUMERO;
                BK_Servicio.DPISO = LvrServicio.DPISO;
                BK_Servicio.DOFICINA = LvrServicio.DOFICINA;
                BK_Servicio.DCIUDAD = LvrServicio.DCIUDAD;
                BK_Servicio.DCOMUNA = LvrServicio.DCOMUNA;
                BK_Servicio.DESTADO = LvrServicio.DESTADO;
                BK_Servicio.DPAIS = LvrServicio.DPAIS;
                BK_Servicio.DCOORDENADAS = LvrServicio.DCOORDENADAS;
                BK_Servicio.DESCRIPCION = LvrServicio.DESCRIPCION;
                BK_Servicio.FACTURAS = LvrServicio.FACTURAS;
                BK_Servicio.BULTOS = LvrServicio.BULTOS;
                BK_Servicio.COMPRAS = LvrServicio.COMPRAS;
                BK_Servicio.CHEQUES = LvrServicio.CHEQUES;
                BK_Servicio.SOBRES = LvrServicio.SOBRES;
                BK_Servicio.OTROS = LvrServicio.OTROS;
                BK_Servicio.OBSERVACIONES = LvrServicio.OBSERVACIONES;
                BK_Servicio.ENTREGA = LvrServicio.ENTREGA;
                BK_Servicio.RECEPCION = LvrServicio.RECEPCION;
                BK_Servicio.TESPERA = LvrServicio.TESPERA;
                BK_Servicio.FECHAENTREGA = LvrServicio.FECHAENTREGA;
                BK_Servicio.HORAENTREGA = LvrServicio.HORAENTREGA;
                BK_Servicio.DISTANCIA = LvrServicio.DISTANCIA;
                BK_Servicio.PROGRAMADO = LvrServicio.PROGRAMADO;
                BK_ServicioLista.Add(BK_Servicio);
            }
            return BK_ServicioLista;
        }


        public bool AgregarServicio(JsonBikeMessengerServicio aBK_Servicio)
        {
            try
            {
                using (var db = new BK_SQliteContext())
                {
                    var LvrServicio = new SERVICIO()
                    {
                        PENTALPHA = aBK_Servicio.PENTALPHA,
                        NROENVIO = aBK_Servicio.NROENVIO,
                        GUIADESPACHO = aBK_Servicio.GUIADESPACHO,
                        FECHA = aBK_Servicio.FECHA,
                        HORA = aBK_Servicio.HORA,
                        CLIENTERUT = aBK_Servicio.CLIENTERUT,
                        CLIENTEDIGVER = aBK_Servicio.CLIENTEDIGVER,
                        // CLIENTE = aBK_Servicio.CLIENTE,
                        MENSAJERORUT = aBK_Servicio.MENSAJERORUT,
                        MENSAJERODIGVER = aBK_Servicio.MENSAJERODIGVER,
                        // MENSAJERO = aBK_Servicio.MENSAJERO,
                        RECURSOID = aBK_Servicio.RECURSOID,
                        // RECURSO = aBK_Servicio.RECURSO,
                        ODOMICILIO1 = aBK_Servicio.ODOMICILIO1,
                        ODOMICILIO2 = aBK_Servicio.ODOMICILIO2,
                        ONUMERO = aBK_Servicio.ONUMERO,
                        OPISO = aBK_Servicio.OPISO,
                        OOFICINA = aBK_Servicio.OOFICINA,
                        OCIUDAD = aBK_Servicio.OCIUDAD,
                        OCOMUNA = aBK_Servicio.OCOMUNA,
                        OESTADO = aBK_Servicio.OESTADO,
                        OPAIS = aBK_Servicio.OPAIS,
                        OCOORDENADAS = aBK_Servicio.OCOORDENADAS,
                        DDOMICILIO1 = aBK_Servicio.DDOMICILIO1,
                        DDOMICILIO2 = aBK_Servicio.DDOMICILIO2,
                        DNUMERO = aBK_Servicio.DNUMERO,
                        DPISO = aBK_Servicio.DPISO,
                        DOFICINA = aBK_Servicio.DOFICINA,
                        DCIUDAD = aBK_Servicio.DCIUDAD,
                        DCOMUNA = aBK_Servicio.DCOMUNA,
                        DESTADO = aBK_Servicio.DESTADO,
                        DPAIS = aBK_Servicio.DPAIS,
                        DCOORDENADAS = aBK_Servicio.DCOORDENADAS,
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
                        PROGRAMADO = aBK_Servicio.PROGRAMADO
                    };
                    _ = db.Add(LvrServicio);
                    _ = db.SaveChanges();
                };
                return true;
            }
            catch (DbUpdateException Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return false;
            }
        }


        public bool ModificarServicio(JsonBikeMessengerServicio mBK_Servicio)
        {
            try
            {
                using (var db = new BK_SQliteContext())
                {
                    var LvrServicio = new SERVICIO()
                    {
                        PENTALPHA = mBK_Servicio.PENTALPHA,
                        NROENVIO = mBK_Servicio.NROENVIO,
                        GUIADESPACHO = mBK_Servicio.GUIADESPACHO,
                        FECHA = mBK_Servicio.FECHA,
                        HORA = mBK_Servicio.HORA,
                        CLIENTERUT = mBK_Servicio.CLIENTERUT,
                        CLIENTEDIGVER = mBK_Servicio.CLIENTEDIGVER,
                        // CLIENTE = mBK_Servicio.CLIENTE,
                        MENSAJERORUT = mBK_Servicio.MENSAJERORUT,
                        MENSAJERODIGVER = mBK_Servicio.MENSAJERODIGVER,
                        // MENSAJERO = mBK_Servicio.MENSAJERO,
                        RECURSOID = mBK_Servicio.RECURSOID,
                        // RECURSO = mBK_Servicio.RECURSO,
                        ODOMICILIO1 = mBK_Servicio.ODOMICILIO1,
                        ODOMICILIO2 = mBK_Servicio.ODOMICILIO2,
                        ONUMERO = mBK_Servicio.ONUMERO,
                        OPISO = mBK_Servicio.OPISO,
                        OOFICINA = mBK_Servicio.OOFICINA,
                        OCIUDAD = mBK_Servicio.OCIUDAD,
                        OCOMUNA = mBK_Servicio.OCOMUNA,
                        OESTADO = mBK_Servicio.OESTADO,
                        OPAIS = mBK_Servicio.OPAIS,
                        OCOORDENADAS = mBK_Servicio.OCOORDENADAS,
                        DDOMICILIO1 = mBK_Servicio.DDOMICILIO1,
                        DDOMICILIO2 = mBK_Servicio.DDOMICILIO2,
                        DNUMERO = mBK_Servicio.DNUMERO,
                        DPISO = mBK_Servicio.DPISO,
                        DOFICINA = mBK_Servicio.DOFICINA,
                        DCIUDAD = mBK_Servicio.DCIUDAD,
                        DCOMUNA = mBK_Servicio.DCOMUNA,
                        DESTADO = mBK_Servicio.DESTADO,
                        DPAIS = mBK_Servicio.DPAIS,
                        DCOORDENADAS = mBK_Servicio.DCOORDENADAS,
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
                        PROGRAMADO = mBK_Servicio.PROGRAMADO
                    };
                    _ = db.Update(LvrServicio);
                    _ = db.SaveChanges();
                };
                return true;
            }
            catch (DbUpdateException Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return false;
            }
        }

        public bool BorrarServicio(string pPENTALPHA, string pNROENVIO)
        {
            try
            {
                using (var db = new BK_SQliteContext())
                {
                    SERVICIO LvrServicio;
                    LvrServicio = db.SERVICIOs.Find(pPENTALPHA, pNROENVIO);
                    _ = db.Remove(LvrServicio);
                    _ = db.SaveChanges();
                }
                return true;
            }
            catch (DbUpdateException Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return false;
            }
        }

        // Utilitarios
        public List<JsonBikeMessengerRecurso> BuscarGridRecurso()
        {
            JsonBikeMessengerRecurso BK_Recurso;
            List<JsonBikeMessengerRecurso> BK_RecursoLista;
            BK_RecursoLista = null;
            using (var db = new BK_SQliteContext())
            {
                foreach (var LvrRecurso in db.RECURSOs)
                {
                    BK_Recurso = null;
                    BK_Recurso.PATENTE = LvrRecurso.PATENTE;
                    BK_Recurso.TIPO = LvrRecurso.TIPO;
                    BK_Recurso.MARCA = LvrRecurso.MARCA;
                    BK_Recurso.MODELO = LvrRecurso.MODELO;
                    BK_Recurso.CIUDAD = LvrRecurso.CIUDAD;
                    BK_RecursoLista.Add(BK_Recurso);
                }
            }
            return BK_RecursoLista;
        }

        public List<JsonBikeMessengerPersonal> BuscarGridPersonal()
        {
            JsonBikeMessengerPersonal BK_Personal;
            List<JsonBikeMessengerPersonal> BK_PersonalLista;
            BK_PersonalLista = null;
            using (var db = new BK_SQliteContext())
            {
                foreach (var LvrPersonal in db.PERSONALs)
                {
                    BK_Personal = null;
                    BK_Personal.RUTID = LvrPersonal.RUTID;
                    BK_Personal.DIGVER = LvrPersonal.DIGVER;
                    BK_Personal.APELLIDOS = LvrPersonal.APELLIDOS;
                    BK_Personal.NOMBRES = LvrPersonal.NOMBRES;
                    BK_PersonalLista.Add(BK_Personal);
                }
            }
            return BK_PersonalLista;
        }


        public List<JsonBikeMessengerCliente> BuscarGridCliente()
        {
            List<JsonBikeMessengerCliente> BK_ClienteLista;
            JsonBikeMessengerCliente BK_Cliente;

            BK_ClienteLista = null;
            using (var db = new BK_SQliteContext())
            {
                foreach (var LvrCliente in db.CLIENTEs)
                {
                    BK_Cliente = null;
                    BK_Cliente.RUTID = LvrCliente.RUTID;
                    BK_Cliente.DIGVER = LvrCliente.DIGVER;
                    BK_Cliente.NOMBRE = LvrCliente.NOMBRE;
                    BK_ClienteLista.Add(BK_Cliente);
                }
            }
            return BK_ClienteLista;
        }

        private string Bm_BuscarNombreCliente(string pPENTALPHA, string pRUT, string pDIGVER)
        {
            string lRetorno = "";
            using (var db = new BK_SQliteContext())
            {
                CLIENTE LvrCliente;
                LvrCliente = db.CLIENTEs.Find(pPENTALPHA, pRUT, pDIGVER);
                lRetorno = LvrCliente.NOMBRE;
            }
            return lRetorno;
        }

        private string Bm_BuscarNombreMensajero(string pPENTALPHA, string pRUT, string pDIGVER)
        {
            string lRetorno = "";
            using (var db = new BK_SQliteContext())
            {
                PERSONAL LvrPersonal;
                LvrPersonal = db.PERSONALs.Find(pPENTALPHA, pRUT, pDIGVER);
                lRetorno = LvrPersonal.APELLIDOS + ", " + LvrPersonal.NOMBRES;
            }
            return lRetorno;
        }

        private string Bm_BuscarNombreRecurso(string pPENTALPHA, string pPATENTE)
        {
            string lRetorno = "";
            using (var db = new BK_SQliteContext())
            {
                RECURSO LvrRecurso;
                LvrRecurso = db.RECURSOs.Find(pPENTALPHA, pPATENTE);
                lRetorno = LvrRecurso.TIPO + "-" + LvrRecurso.MARCA + "-" + LvrRecurso.MODELO;
            }
            return lRetorno;
        }

        public List<string> GetPais()
        {
            BK_Pais = null;

            using (var db = new BK_SQliteContext())
            {
                foreach (var LvrPais in db.PAIs)
                {
                    string Pais = LvrPais.NOMBRE;
                    BK_Pais.Add(Pais);
                }
            }
            return BK_Pais;
        }

        public List<string> GetRegion()
        {
            BK_Region = null;

            using (var db = new BK_SQliteContext())
            {
                foreach (var LvrRegion in db.ESTADOREGIONs)
                {
                    string Region = LvrRegion.NOMBRE;
                    BK_Region.Add(Region);
                }
            }
            return BK_Region;
        }

        public List<string> GetComuna()
        {
            BK_Comuna = null;

            using (var db = new BK_SQliteContext())
            {
                foreach (var LvrComuna in db.COMUNAs)
                {
                    string Comuna = LvrComuna.NOMBRE;
                    BK_Comuna.Add(Comuna);
                }
            }
            return BK_Comuna;
        }

        public List<string> GetCiudad()
        {
            BK_Ciudad = null;

            using (var db = new BK_SQliteContext())
            {
                foreach (var LvrCiudad in db.CIUDADs)
                {
                    string Ciudad = LvrCiudad.NOMBRE;
                    BK_Ciudad.Add(Ciudad);
                }
            }
            return BK_Ciudad;
        }


        public string Bm_Servicio_Listado()
        {
            // Pendientes

            LvrTablaHtml DocumentoHtml = new LvrTablaHtml();

            DocumentoHtml.CrearTexto("ENVIOS");
            DocumentoHtml.InicioDocumento();
            DocumentoHtml.AgregarTituloTabla("Listado de Envios");
            DocumentoHtml.AbrirEncabezado();
            DocumentoHtml.AgregarEncabezado("Nro de Envío");
            DocumentoHtml.AgregarEncabezado("Guia de Despacho");
            DocumentoHtml.AgregarEncabezado("Fecha de Ingreso");
            DocumentoHtml.AgregarEncabezado("Hora de Ingreso");
            DocumentoHtml.AgregarEncabezado("Cliente");
            DocumentoHtml.AgregarEncabezado("Mensajero");
            DocumentoHtml.AgregarEncabezado("Entrega");
            DocumentoHtml.AgregarEncabezado("Recepción");
            DocumentoHtml.CerrarEncabezado();

            BK_ServicioLista = null;
            using (var db = new BK_SQliteContext())
            {
                foreach (var LvrServicio in db.SERVICIOs)
                {
                    DocumentoHtml.AbrirFila();
                    DocumentoHtml.AgregarCampo(LvrServicio.NROENVIO, false);
                    DocumentoHtml.AgregarCampo(LvrServicio.GUIADESPACHO, false);
                    DocumentoHtml.AgregarCampo(LvrServicio.FECHA.ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrServicio.HORA.ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrServicio.CLIENTERUT + "-" + LvrServicio.CLIENTEDIGVER, false);
                    DocumentoHtml.AgregarCampo(LvrServicio.MENSAJERORUT + "-" + LvrServicio.MENSAJERODIGVER, false);
                    DocumentoHtml.AgregarCampo(LvrServicio.ENTREGA, false);
                    DocumentoHtml.AgregarCampo(LvrServicio.RECEPCION, false);
                    DocumentoHtml.CerrarFila();
                }
            }
            DocumentoHtml.FinDocumento();
            return DocumentoHtml.BufferHtml;
        }
    }
}
