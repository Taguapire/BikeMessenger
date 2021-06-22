using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BikeMessenger
{
    internal class Bm_Personal_Database
    {

        private static JsonBikeMessengerPersonal BK_Personal;
        private static List<JsonBikeMessengerPersonal> BK_PersonalLista;

        private static List<string> BK_Pais;
        private static List<string> BK_Region;
        private static List<string> BK_Comuna;
        private static List<string> BK_Ciudad;

        // Busqueda por Muchos
        public List<JsonBikeMessengerPersonal> BuscarPersonal()
        {
            BK_PersonalLista = null;
            using (var db = new BK_SQliteContext())
            {
                foreach (var LvrPersonal in db.PERSONALs)
                {
                    BK_Personal = null;
                    BK_Personal.PENTALPHA = LvrPersonal.PENTALPHA;
                    BK_Personal.RUTID = LvrPersonal.RUTID;
                    BK_Personal.DIGVER = LvrPersonal.DIGVER;
                    BK_Personal.APELLIDOS = LvrPersonal.APELLIDOS;
                    BK_Personal.NOMBRES = LvrPersonal.NOMBRES;
                    BK_Personal.TELEFONO1 = LvrPersonal.TELEFONO1;
                    BK_Personal.TELEFONO2 = LvrPersonal.TELEFONO2;
                    BK_Personal.EMAIL = LvrPersonal.EMAIL;
                    BK_Personal.AUTORIZACION = LvrPersonal.AUTORIZACION;
                    BK_Personal.CARGO = LvrPersonal.CARGO;
                    BK_Personal.DOMICILIO = LvrPersonal.DOMICILIO;
                    BK_Personal.NUMERO = LvrPersonal.NUMERO;
                    BK_Personal.PISO = LvrPersonal.PISO;
                    BK_Personal.DPTO = LvrPersonal.DPTO;
                    BK_Personal.CODIGOPOSTAL = LvrPersonal.CODIGOPOSTAL;
                    BK_Personal.CIUDAD = LvrPersonal.CIUDAD;
                    BK_Personal.COMUNA = LvrPersonal.COMUNA;
                    BK_Personal.REGION = LvrPersonal.REGION;
                    BK_Personal.PAIS = LvrPersonal.PAIS;
                    BK_Personal.OBSERVACIONES = LvrPersonal.OBSERVACIONES;
                    BK_Personal.FOTO = LvrPersonal.FOTO;
                    BK_PersonalLista.Add(BK_Personal);
                }
            }
            return BK_PersonalLista;
        }

        public List<JsonBikeMessengerPersonal> BuscarPersonal(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            BK_PersonalLista = null;
            using (var db = new BK_SQliteContext())
            {
                BK_Personal = null;
                PERSONAL LvrPersonal;
                LvrPersonal = db.PERSONALs.Find(pPENTALPHA, pRUTID, pDIGVER);
                BK_Personal.PENTALPHA = LvrPersonal.PENTALPHA;
                BK_Personal.RUTID = LvrPersonal.RUTID;
                BK_Personal.DIGVER = LvrPersonal.DIGVER;
                BK_Personal.APELLIDOS = LvrPersonal.APELLIDOS;
                BK_Personal.NOMBRES = LvrPersonal.NOMBRES;
                BK_Personal.TELEFONO1 = LvrPersonal.TELEFONO1;
                BK_Personal.TELEFONO2 = LvrPersonal.TELEFONO2;
                BK_Personal.EMAIL = LvrPersonal.EMAIL;
                BK_Personal.AUTORIZACION = LvrPersonal.AUTORIZACION;
                BK_Personal.CARGO = LvrPersonal.CARGO;
                BK_Personal.DOMICILIO = LvrPersonal.DOMICILIO;
                BK_Personal.NUMERO = LvrPersonal.NUMERO;
                BK_Personal.PISO = LvrPersonal.PISO;
                BK_Personal.DPTO = LvrPersonal.DPTO;
                BK_Personal.CODIGOPOSTAL = LvrPersonal.CODIGOPOSTAL;
                BK_Personal.CIUDAD = LvrPersonal.CIUDAD;
                BK_Personal.COMUNA = LvrPersonal.COMUNA;
                BK_Personal.REGION = LvrPersonal.REGION;
                BK_Personal.PAIS = LvrPersonal.PAIS;
                BK_Personal.OBSERVACIONES = LvrPersonal.OBSERVACIONES;
                BK_Personal.FOTO = LvrPersonal.FOTO;
                BK_PersonalLista.Add(BK_Personal);
            }
            return BK_PersonalLista;
        }


        public bool AgregarPersonal(JsonBikeMessengerPersonal aBK_Personal)
        {
            try
            {
                using (var db = new BK_SQliteContext())
                {
                    var LvrPersonal = new PERSONAL()
                    {
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
                        FOTO = aBK_Personal.FOTO
                    };
                    db.Add(LvrPersonal);
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


        public bool ModificarPersonal(JsonBikeMessengerPersonal mBK_Personal)
        {
            try
            {
                using (var db = new BK_SQliteContext())
                {
                    var LvrPersonal = new PERSONAL()
                    {
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
                        FOTO = mBK_Personal.FOTO
                    };
                    db.Update(LvrPersonal);
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

        public bool BorrarPersonal(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            try
            {
                using (var db = new BK_SQliteContext())
                {
                    PERSONAL LvrPersonal;
                    LvrPersonal = db.PERSONALs.Find(pPENTALPHA, pRUTID, pDIGVER);
                    db.Remove(LvrPersonal);
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

        public List<JsonBikeMessengerPersonal> BuscarGridPersonal()
        {
            BK_PersonalLista = null;
            using (BK_SQliteContext db = new BK_SQliteContext())
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


        public string Bm_Personal_Listado()
        {
            // Pendientes

            LvrTablaHtml DocumentoHtml = new LvrTablaHtml();

            DocumentoHtml.CrearTexto("ENVIOS");
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

            BK_PersonalLista = null;
            using (var db = new BK_SQliteContext())
            {
                foreach (var LvrPersonal in db.PERSONALs)
                {
                    DocumentoHtml.AbrirFila();
                    DocumentoHtml.AgregarCampo(LvrPersonal.RUTID + "-" + LvrPersonal.DIGVER, false);
                    DocumentoHtml.AgregarCampo(LvrPersonal.APELLIDOS, false);
                    DocumentoHtml.AgregarCampo(LvrPersonal.NOMBRES, false);
                    DocumentoHtml.AgregarCampo(LvrPersonal.TELEFONO1, false);
                    DocumentoHtml.AgregarCampo(LvrPersonal.TELEFONO2, false);
                    DocumentoHtml.AgregarCampo(LvrPersonal.EMAIL, false);
                    DocumentoHtml.AgregarCampo(LvrPersonal.AUTORIZACION, false);
                    DocumentoHtml.AgregarCampo(LvrPersonal.CARGO, false);
                    DocumentoHtml.AgregarCampo(LvrPersonal.CIUDAD, false);
                    DocumentoHtml.CerrarFila();
                }
            }

            DocumentoHtml.FinDocumento();
            return DocumentoHtml.BufferHtml;
        }
    }
}
