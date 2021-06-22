using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BikeMessenger
{
    internal class Bm_Cliente_Database
    {

        private static JsonBikeMessengerCliente BK_Cliente;
        private static List<JsonBikeMessengerCliente> BK_ClienteLista;

        private static List<string> BK_Pais;
        private static List<string> BK_Region;
        private static List<string> BK_Comuna;
        private static List<string> BK_Ciudad;

        // Cambio de Operador
        // Busqueda por Muchos

        public List<JsonBikeMessengerCliente> BuscarCliente()
        {
            BK_ClienteLista = null;
            using (var db = new BK_SQliteContext())
            {
                foreach (var LvrCliente in db.CLIENTEs)
                {
                    BK_Cliente = null;
                    BK_Cliente.PENTALPHA = LvrCliente.PENTALPHA;
                    BK_Cliente.RUTID = LvrCliente.RUTID;
                    BK_Cliente.DIGVER = LvrCliente.DIGVER;
                    BK_Cliente.NOMBRE = LvrCliente.NOMBRE;
                    BK_Cliente.USUARIO = LvrCliente.USUARIO;
                    BK_Cliente.CLAVE = LvrCliente.CLAVE;
                    BK_Cliente.ACTIVIDAD1 = LvrCliente.ACTIVIDAD1;
                    BK_Cliente.ACTIVIDAD2 = LvrCliente.ACTIVIDAD2;
                    BK_Cliente.REPRESENTANTE1 = LvrCliente.REPRESENTANTE1;
                    BK_Cliente.REPRESENTANTE2 = LvrCliente.REPRESENTANTE2;
                    BK_Cliente.TELEFONO1 = LvrCliente.TELEFONO1;
                    BK_Cliente.TELEFONO2 = LvrCliente.TELEFONO2;
                    BK_Cliente.DOMICILIO1 = LvrCliente.DOMICILIO1;
                    BK_Cliente.DOMICILIO2 = LvrCliente.DOMICILIO2;
                    BK_Cliente.NUMERO = LvrCliente.NUMERO;
                    BK_Cliente.PISO = LvrCliente.PISO;
                    BK_Cliente.OFICINA = LvrCliente.OFICINA;
                    BK_Cliente.CODIGOPOSTAL = LvrCliente.CODIGOPOSTAL;
                    BK_Cliente.CIUDAD = LvrCliente.CIUDAD;
                    BK_Cliente.COMUNA = LvrCliente.COMUNA;
                    BK_Cliente.ESTADOREGION = LvrCliente.REGION;
                    BK_Cliente.PAIS = LvrCliente.PAIS;
                    BK_Cliente.OBSERVACIONES = LvrCliente.OBSERVACIONES;
                    BK_Cliente.LOGO = LvrCliente.FOTO;
                    BK_ClienteLista.Add(BK_Cliente);
                }
            }
            return BK_ClienteLista;
        }

        public List<JsonBikeMessengerCliente> BuscarCliente(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            BK_ClienteLista = null;
            using (var db = new BK_SQliteContext())
            {
                BK_Cliente = null;
                CLIENTE LvrCliente;
                LvrCliente = db.CLIENTEs.Find(pPENTALPHA, pRUTID, pDIGVER);
                BK_Cliente.PENTALPHA = LvrCliente.PENTALPHA;
                BK_Cliente.RUTID = LvrCliente.RUTID;
                BK_Cliente.DIGVER = LvrCliente.DIGVER;
                BK_Cliente.NOMBRE = LvrCliente.NOMBRE;
                BK_Cliente.USUARIO = LvrCliente.USUARIO;
                BK_Cliente.CLAVE = LvrCliente.CLAVE;
                BK_Cliente.ACTIVIDAD1 = LvrCliente.ACTIVIDAD1;
                BK_Cliente.ACTIVIDAD2 = LvrCliente.ACTIVIDAD2;
                BK_Cliente.REPRESENTANTE1 = LvrCliente.REPRESENTANTE1;
                BK_Cliente.REPRESENTANTE2 = LvrCliente.REPRESENTANTE2;
                BK_Cliente.TELEFONO1 = LvrCliente.TELEFONO1;
                BK_Cliente.TELEFONO2 = LvrCliente.TELEFONO2;
                BK_Cliente.DOMICILIO1 = LvrCliente.DOMICILIO1;
                BK_Cliente.DOMICILIO2 = LvrCliente.DOMICILIO2;
                BK_Cliente.NUMERO = LvrCliente.NUMERO;
                BK_Cliente.PISO = LvrCliente.PISO;
                BK_Cliente.OFICINA = LvrCliente.OFICINA;
                BK_Cliente.CODIGOPOSTAL = LvrCliente.CODIGOPOSTAL;
                BK_Cliente.CIUDAD = LvrCliente.CIUDAD;
                BK_Cliente.COMUNA = LvrCliente.COMUNA;
                BK_Cliente.ESTADOREGION = LvrCliente.REGION;
                BK_Cliente.PAIS = LvrCliente.PAIS;
                BK_Cliente.OBSERVACIONES = LvrCliente.OBSERVACIONES;
                BK_Cliente.LOGO = LvrCliente.FOTO;
                BK_ClienteLista.Add(BK_Cliente);
            }
            return BK_ClienteLista;
        }


        public bool AgregarCliente(JsonBikeMessengerCliente aBK_Cliente)
        {
            try
            {
                using (var db = new BK_SQliteContext())
                {
                    var LvrCliente = new CLIENTE()
                    {
                        PENTALPHA = aBK_Cliente.PENTALPHA,
                        RUTID = aBK_Cliente.RUTID,
                        DIGVER = aBK_Cliente.DIGVER,
                        NOMBRE = aBK_Cliente.NOMBRE,
                        USUARIO = aBK_Cliente.USUARIO,
                        CLAVE = aBK_Cliente.CLAVE,
                        ACTIVIDAD1 = aBK_Cliente.ACTIVIDAD1,
                        ACTIVIDAD2 = aBK_Cliente.ACTIVIDAD2,
                        REPRESENTANTE1 = aBK_Cliente.REPRESENTANTE1,
                        REPRESENTANTE2 = aBK_Cliente.REPRESENTANTE2,
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
                        REGION = aBK_Cliente.ESTADOREGION,
                        PAIS = aBK_Cliente.PAIS,
                        OBSERVACIONES = aBK_Cliente.OBSERVACIONES,
                        FOTO = aBK_Cliente.LOGO
                    };
                    db.Add(LvrCliente);
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


        public bool ModificarCliente(JsonBikeMessengerCliente mBK_Cliente)
        {
            try
            {

                using (var db = new BK_SQliteContext())
                {
                    var LvrCliente = new CLIENTE()
                    {
                        PENTALPHA = mBK_Cliente.PENTALPHA,
                        RUTID = mBK_Cliente.RUTID,
                        DIGVER = mBK_Cliente.DIGVER,
                        NOMBRE = mBK_Cliente.NOMBRE,
                        USUARIO = mBK_Cliente.USUARIO,
                        CLAVE = mBK_Cliente.CLAVE,
                        ACTIVIDAD1 = mBK_Cliente.ACTIVIDAD1,
                        ACTIVIDAD2 = mBK_Cliente.ACTIVIDAD2,
                        REPRESENTANTE1 = mBK_Cliente.REPRESENTANTE1,
                        REPRESENTANTE2 = mBK_Cliente.REPRESENTANTE2,
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
                        REGION = mBK_Cliente.ESTADOREGION,
                        PAIS = mBK_Cliente.PAIS,
                        OBSERVACIONES = mBK_Cliente.OBSERVACIONES,
                        FOTO = mBK_Cliente.LOGO
                    };
                    db.Update(LvrCliente);
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

        public bool BorrarCliente(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            try
            {
                using (var db = new BK_SQliteContext())
                {
                    CLIENTE LvrCliente;
                    LvrCliente = db.CLIENTEs.Find(pPENTALPHA, pRUTID, pDIGVER);
                    db.Remove(LvrCliente);
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

        public List<JsonBikeMessengerCliente> BuscarGridCliente()
        {
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


        public string Bm_Cliente_Listado()
        {
            // Pendientes

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

            BK_ClienteLista = null;
            using (var db = new BK_SQliteContext())
            {
                foreach (var LvrCliente in db.CLIENTEs)
                {
                    DocumentoHtml.AbrirFila();
                    DocumentoHtml.AgregarCampo(LvrCliente.RUTID + "-" + LvrCliente.DIGVER, false);
                    DocumentoHtml.AgregarCampo(LvrCliente.NOMBRE, false);
                    DocumentoHtml.AgregarCampo(LvrCliente.ACTIVIDAD1, false);
                    DocumentoHtml.AgregarCampo(LvrCliente.REPRESENTANTE1, false);
                    DocumentoHtml.AgregarCampo(LvrCliente.TELEFONO2, false);
                    DocumentoHtml.AgregarCampo(LvrCliente.DOMICILIO1, false);
                    DocumentoHtml.AgregarCampo(LvrCliente.NUMERO, false);
                    DocumentoHtml.AgregarCampo(LvrCliente.OFICINA, false);
                    DocumentoHtml.AgregarCampo(LvrCliente.CIUDAD, false);
                    DocumentoHtml.AgregarCampo(LvrCliente.COMUNA, false);
                    DocumentoHtml.AgregarCampo(LvrCliente.PAIS, false);
                    DocumentoHtml.CerrarFila();
                }
            }

            DocumentoHtml.FinDocumento();
            return DocumentoHtml.BufferHtml;
        }
    }
}