using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BikeMessenger
{
    internal class Bm_Recurso_Database
    {
        private static JsonBikeMessengerRecurso BK_Recurso;
        private static List<JsonBikeMessengerRecurso> BK_RecursoLista;

        private static List<string> BK_Pais;
        private static List<string> BK_Region;
        private static List<string> BK_Comuna;
        private static List<string> BK_Ciudad;

        // Busqueda por Muchos
        public List<JsonBikeMessengerRecurso> BuscarRecurso()
        {
            BK_RecursoLista = null;
            using (var db = new BK_SQliteContext())
            {
                foreach (var LvrRecurso in db.RECURSOs)
                {
                    BK_Recurso = null;
                    BK_Recurso.PENTALPHA = LvrRecurso.PENTALPHA;
                    BK_Recurso.PATENTE = LvrRecurso.PATENTE;
                    BK_Recurso.RUTID = LvrRecurso.RUTID;
                    BK_Recurso.DIGVER = LvrRecurso.DIGVER;
                    BK_Recurso.PROPIETARIO = LvrRecurso.PROPIETARIO;
                    BK_Recurso.TIPO = LvrRecurso.TIPO;
                    BK_Recurso.MARCA = LvrRecurso.MARCA;
                    BK_Recurso.MODELO = LvrRecurso.MODELO;
                    BK_Recurso.VARIANTE = LvrRecurso.VARIANTE;
                    BK_Recurso.ANO = LvrRecurso.ANO;
                    BK_Recurso.COLOR = LvrRecurso.COLOR;
                    BK_Recurso.CIUDAD = LvrRecurso.CIUDAD;
                    BK_Recurso.COMUNA = LvrRecurso.COMUNA;
                    BK_Recurso.REGION = LvrRecurso.REGION;
                    BK_Recurso.PAIS = LvrRecurso.PAIS;
                    BK_Recurso.OBSERVACIONES = LvrRecurso.OBSERVACIONES;
                    BK_Recurso.FOTO = LvrRecurso.FOTO;
                    BK_RecursoLista.Add(BK_Recurso);
                }
            }
            return BK_RecursoLista;
        }

        public List<JsonBikeMessengerRecurso> BuscarRecurso(string pPENTALPHA, string pPATENTE)
        {
            BK_RecursoLista = null;
            using (var db = new BK_SQliteContext())
            {
                BK_Recurso = null;
                RECURSO LvrRecurso;
                LvrRecurso = db.RECURSOs.Find(pPENTALPHA, pPATENTE);
                BK_Recurso = null;
                BK_Recurso.PENTALPHA = LvrRecurso.PENTALPHA;
                BK_Recurso.PATENTE = LvrRecurso.PATENTE;
                BK_Recurso.RUTID = LvrRecurso.RUTID;
                BK_Recurso.DIGVER = LvrRecurso.DIGVER;
                BK_Recurso.PROPIETARIO = LvrRecurso.PROPIETARIO;
                BK_Recurso.TIPO = LvrRecurso.TIPO;
                BK_Recurso.MARCA = LvrRecurso.MARCA;
                BK_Recurso.MODELO = LvrRecurso.MODELO;
                BK_Recurso.VARIANTE = LvrRecurso.VARIANTE;
                BK_Recurso.ANO = LvrRecurso.ANO;
                BK_Recurso.COLOR = LvrRecurso.COLOR;
                BK_Recurso.CIUDAD = LvrRecurso.CIUDAD;
                BK_Recurso.COMUNA = LvrRecurso.COMUNA;
                BK_Recurso.REGION = LvrRecurso.REGION;
                BK_Recurso.PAIS = LvrRecurso.PAIS;
                BK_Recurso.OBSERVACIONES = LvrRecurso.OBSERVACIONES;
                BK_Recurso.FOTO = LvrRecurso.FOTO;
                BK_RecursoLista.Add(BK_Recurso);
            }
            return BK_RecursoLista;
        }


        public bool AgregarRecurso(JsonBikeMessengerRecurso aBK_Recurso)
        {
            try
            {
                using (var db = new BK_SQliteContext())
                {
                    var LvrRecurso = new RECURSO()
                    {
                        PENTALPHA = aBK_Recurso.PENTALPHA,
                        PATENTE = aBK_Recurso.PATENTE,
                        RUTID = aBK_Recurso.RUTID,
                        DIGVER = aBK_Recurso.DIGVER,
                        PROPIETARIO = aBK_Recurso.PROPIETARIO,
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
                        FOTO = aBK_Recurso.FOTO
                    };
                    db.Add(LvrRecurso);
                    db.SaveChanges();
                };
                return true;
            }
            catch (DbUpdateException Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return false;
            }
        }


        public bool ModificarRecurso(JsonBikeMessengerRecurso mBK_Recurso)
        {
            try
            {
                using (var db = new BK_SQliteContext())
                {
                    var LvrRecurso = new RECURSO()
                    {
                        PENTALPHA = mBK_Recurso.PENTALPHA,
                        PATENTE = mBK_Recurso.PATENTE,
                        RUTID = mBK_Recurso.RUTID,
                        DIGVER = mBK_Recurso.DIGVER,
                        PROPIETARIO = mBK_Recurso.PROPIETARIO,
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
                        FOTO = mBK_Recurso.FOTO
                    };
                    db.Update(LvrRecurso);
                    db.SaveChanges();
                };
                return true;
            }
            catch (DbUpdateException Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return false;
            }
        }

        public bool BorrarRecurso(string pPENTALPHA, string pPATENTE)
        {
            try
            {
                using (var db = new BK_SQliteContext())
                {
                    RECURSO LvrRecurso;
                    LvrRecurso = db.RECURSOs.Find(pPENTALPHA, pPATENTE);
                    db.Remove(LvrRecurso);
                    db.SaveChanges();
                }
                return true;
            }
            catch (DbUpdateException Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return false;
            }
        }

        public List<JsonBikeMessengerRecurso> BuscarGridRecurso()
        {
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
            List<JsonBikeMessengerPersonal> BK_PersonalLista = null;
            JsonBikeMessengerPersonal BK_Personal = null;
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


        public string Bm_Recurso_Listado()
        {
            // Pendientes

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

            BK_RecursoLista = null;
            using (var db = new BK_SQliteContext())
            {
                foreach (var LvrRecurso in db.RECURSOs)
                {
                    DocumentoHtml.AbrirFila();
                    DocumentoHtml.AgregarCampo(LvrRecurso.RUTID + "-" + LvrRecurso.DIGVER, false);
                    DocumentoHtml.AgregarCampo(LvrRecurso.PROPIETARIO, false);
                    DocumentoHtml.AgregarCampo(LvrRecurso.TIPO, false);
                    DocumentoHtml.AgregarCampo(LvrRecurso.PATENTE, false);
                    DocumentoHtml.AgregarCampo(LvrRecurso.MARCA, false);
                    DocumentoHtml.AgregarCampo(LvrRecurso.MODELO, false);
                    DocumentoHtml.AgregarCampo(LvrRecurso.VARIANTE, false);
                    DocumentoHtml.AgregarCampo(LvrRecurso.ANO, false);
                    DocumentoHtml.AgregarCampo(LvrRecurso.CIUDAD, false);
                    DocumentoHtml.AgregarCampo(LvrRecurso.COMUNA, false);
                    DocumentoHtml.AgregarCampo(LvrRecurso.REGION, false);
                    DocumentoHtml.AgregarCampo(LvrRecurso.PAIS, false);
                    DocumentoHtml.CerrarFila();
                }
            }
            DocumentoHtml.FinDocumento();
            return DocumentoHtml.BufferHtml;
        }
    }
}