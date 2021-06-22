using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BikeMessenger
{
    internal class Bm_Empresa_Database
    {

        // public SqliteFactory BM_DB;
        // Paso de Parametros Sqlite
        // ***************************************
        private static JsonBikeMessengerEmpresa BK_Empresa;
        private static List<JsonBikeMessengerEmpresa> BK_EmpresaLista;

        private static List<string> BK_Pais;
        private static List<string> BK_Region;
        private static List<string> BK_Comuna;
        private static List<string> BK_Ciudad;

        public Bm_Empresa_Database()
        {

        }

        // Busqueda por Muchos
        public List<JsonBikeMessengerEmpresa> BuscarEmpresa()
        {
            BK_EmpresaLista = new List<JsonBikeMessengerEmpresa>();
            try
            {
                using (var db = new BK_SQliteContext())
                {
                    foreach (var LvrEmpresa in db.EMPRESAs)
                    {
                        BK_Empresa = new JsonBikeMessengerEmpresa();
                        BK_Empresa.PENTALPHA = LvrEmpresa.PENTALPHA;
                        BK_Empresa.RUTID = LvrEmpresa.RUTID;
                        BK_Empresa.DIGVER = LvrEmpresa.DIGVER;
                        BK_Empresa.NOMBRE = LvrEmpresa.NOMBRE;
                        BK_Empresa.USUARIO = LvrEmpresa.USUARIO;
                        BK_Empresa.CLAVE = LvrEmpresa.CLAVE;
                        BK_Empresa.ACTIVIDAD1 = LvrEmpresa.ACTIVIDAD1;
                        BK_Empresa.ACTIVIDAD2 = LvrEmpresa.ACTIVIDAD2;
                        BK_Empresa.REPRESENTANTE1 = LvrEmpresa.REPRESENTANTE1;
                        BK_Empresa.REPRESENTANTE2 = LvrEmpresa.REPRESENTANTE2;
                        BK_Empresa.REPRESENTANTE3 = LvrEmpresa.REPRESENTANTE3;
                        BK_Empresa.DOMICILIO1 = LvrEmpresa.DOMICILIO1;
                        BK_Empresa.DOMICILIO2 = LvrEmpresa.DOMICILIO2;
                        BK_Empresa.NUMERO = LvrEmpresa.NUMERO;
                        BK_Empresa.PISO = LvrEmpresa.PISO;
                        BK_Empresa.OFICINA = LvrEmpresa.OFICINA;
                        BK_Empresa.CIUDAD = LvrEmpresa.CIUDAD;
                        BK_Empresa.COMUNA = LvrEmpresa.COMUNA;
                        BK_Empresa.ESTADOREGION = LvrEmpresa.ESTADOREGION;
                        BK_Empresa.CODIGOPOSTAL = LvrEmpresa.CODIGOPOSTAL;
                        BK_Empresa.PAIS = LvrEmpresa.PAIS;
                        BK_Empresa.TELEFONO1 = LvrEmpresa.TELEFONO1;
                        BK_Empresa.TELEFONO2 = LvrEmpresa.TELEFONO2;
                        BK_Empresa.TELEFONO3 = LvrEmpresa.TELEFONO3;
                        BK_Empresa.OBSERVACIONES = LvrEmpresa.OBSERVACIONES;
                        BK_Empresa.LOGO = LvrEmpresa.LOGO;
                        BK_EmpresaLista.Add(BK_Empresa);
                    }
                }
            }
            catch (DbUpdateException Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return BK_EmpresaLista;
        }



        public List<JsonBikeMessengerEmpresa> BuscarEmpresa(string pPENTALPHA)
        {
            try
            {
                BK_EmpresaLista = null;
                using (var db = new BK_SQliteContext())
                {
                    EMPRESA LvrEmpresa;
                    LvrEmpresa = db.EMPRESAs.Find(pPENTALPHA);
                    BK_Empresa.PENTALPHA = LvrEmpresa.PENTALPHA;
                    BK_Empresa.RUTID = LvrEmpresa.RUTID;
                    BK_Empresa.DIGVER = LvrEmpresa.DIGVER;
                    BK_Empresa.NOMBRE = LvrEmpresa.NOMBRE;
                    BK_Empresa.USUARIO = LvrEmpresa.USUARIO;
                    BK_Empresa.CLAVE = LvrEmpresa.CLAVE;
                    BK_Empresa.ACTIVIDAD1 = LvrEmpresa.ACTIVIDAD1;
                    BK_Empresa.ACTIVIDAD2 = LvrEmpresa.ACTIVIDAD2;
                    BK_Empresa.REPRESENTANTE1 = LvrEmpresa.REPRESENTANTE1;
                    BK_Empresa.REPRESENTANTE2 = LvrEmpresa.REPRESENTANTE2;
                    BK_Empresa.REPRESENTANTE3 = LvrEmpresa.REPRESENTANTE3;
                    BK_Empresa.DOMICILIO1 = LvrEmpresa.DOMICILIO1;
                    BK_Empresa.DOMICILIO2 = LvrEmpresa.DOMICILIO2;
                    BK_Empresa.NUMERO = LvrEmpresa.NUMERO;
                    BK_Empresa.PISO = LvrEmpresa.PISO;
                    BK_Empresa.OFICINA = LvrEmpresa.OFICINA;
                    BK_Empresa.CIUDAD = LvrEmpresa.CIUDAD;
                    BK_Empresa.COMUNA = LvrEmpresa.COMUNA;
                    BK_Empresa.ESTADOREGION = LvrEmpresa.ESTADOREGION;
                    BK_Empresa.CODIGOPOSTAL = LvrEmpresa.CODIGOPOSTAL;
                    BK_Empresa.PAIS = LvrEmpresa.PAIS;
                    BK_Empresa.TELEFONO1 = LvrEmpresa.TELEFONO1;
                    BK_Empresa.TELEFONO2 = LvrEmpresa.TELEFONO2;
                    BK_Empresa.TELEFONO3 = LvrEmpresa.TELEFONO3;
                    BK_Empresa.OBSERVACIONES = LvrEmpresa.OBSERVACIONES;
                    BK_Empresa.LOGO = LvrEmpresa.LOGO;
                    BK_EmpresaLista.Add(BK_Empresa);
                }
            }
            catch (DbUpdateException Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return BK_EmpresaLista;
        }

        public bool AgregarEmpresa(JsonBikeMessengerEmpresa aBK_Empresa)
        {
            try
            {
                using (var db = new BK_SQliteContext())
                {
                    var LvrEmpresa = new EMPRESA()
                    {
                        PENTALPHA = aBK_Empresa.PENTALPHA,
                        RUTID = aBK_Empresa.RUTID,
                        DIGVER = aBK_Empresa.DIGVER,
                        NOMBRE = aBK_Empresa.NOMBRE,
                        USUARIO = aBK_Empresa.USUARIO,
                        CLAVE = aBK_Empresa.CLAVE,
                        ACTIVIDAD1 = aBK_Empresa.ACTIVIDAD1,
                        ACTIVIDAD2 = aBK_Empresa.ACTIVIDAD2,
                        REPRESENTANTE1 = aBK_Empresa.REPRESENTANTE1,
                        REPRESENTANTE2 = aBK_Empresa.REPRESENTANTE2,
                        REPRESENTANTE3 = aBK_Empresa.REPRESENTANTE3,
                        DOMICILIO1 = aBK_Empresa.DOMICILIO1,
                        DOMICILIO2 = aBK_Empresa.DOMICILIO2,
                        NUMERO = aBK_Empresa.NUMERO,
                        PISO = aBK_Empresa.PISO,
                        OFICINA = aBK_Empresa.OFICINA,
                        CIUDAD = aBK_Empresa.CIUDAD,
                        COMUNA = aBK_Empresa.COMUNA,
                        ESTADOREGION = aBK_Empresa.ESTADOREGION,
                        CODIGOPOSTAL = aBK_Empresa.CODIGOPOSTAL,
                        PAIS = aBK_Empresa.PAIS,
                        TELEFONO1 = aBK_Empresa.TELEFONO1,
                        TELEFONO2 = aBK_Empresa.TELEFONO2,
                        TELEFONO3 = aBK_Empresa.TELEFONO3,
                        OBSERVACIONES = aBK_Empresa.OBSERVACIONES,
                        LOGO = aBK_Empresa.LOGO
                    };
                    db.Add(LvrEmpresa);
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

        public bool ModificarEmpresa(JsonBikeMessengerEmpresa mBK_Empresa)
        {
            try
            {
                using (var db = new BK_SQliteContext())
                {
                    var LvrEmpresa = new EMPRESA()
                    {
                        PENTALPHA = mBK_Empresa.PENTALPHA,
                        RUTID = mBK_Empresa.RUTID,
                        DIGVER = mBK_Empresa.DIGVER,
                        NOMBRE = mBK_Empresa.NOMBRE,
                        USUARIO = mBK_Empresa.USUARIO,
                        CLAVE = mBK_Empresa.CLAVE,
                        ACTIVIDAD1 = mBK_Empresa.ACTIVIDAD1,
                        ACTIVIDAD2 = mBK_Empresa.ACTIVIDAD2,
                        REPRESENTANTE1 = mBK_Empresa.REPRESENTANTE1,
                        REPRESENTANTE2 = mBK_Empresa.REPRESENTANTE2,
                        REPRESENTANTE3 = mBK_Empresa.REPRESENTANTE3,
                        DOMICILIO1 = mBK_Empresa.DOMICILIO1,
                        DOMICILIO2 = mBK_Empresa.DOMICILIO2,
                        NUMERO = mBK_Empresa.NUMERO,
                        PISO = mBK_Empresa.PISO,
                        OFICINA = mBK_Empresa.OFICINA,
                        CIUDAD = mBK_Empresa.CIUDAD,
                        COMUNA = mBK_Empresa.COMUNA,
                        ESTADOREGION = mBK_Empresa.ESTADOREGION,
                        CODIGOPOSTAL = mBK_Empresa.CODIGOPOSTAL,
                        PAIS = mBK_Empresa.PAIS,
                        TELEFONO1 = mBK_Empresa.TELEFONO1,
                        TELEFONO2 = mBK_Empresa.TELEFONO2,
                        TELEFONO3 = mBK_Empresa.TELEFONO3,
                        OBSERVACIONES = mBK_Empresa.OBSERVACIONES,
                        LOGO = mBK_Empresa.LOGO
                    };
                    db.Update(LvrEmpresa);
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

        public bool BorrarEmpresa(string pPENTALPHA)
        {
            try
            {
                using (var db = new BK_SQliteContext())
                {
                    EMPRESA LvrEmpresa;
                    LvrEmpresa = db.EMPRESAs.Find(pPENTALPHA);
                    db.Remove(LvrEmpresa);
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

        public List<string> GetPais()
        {
            BK_Pais = null;

            try
            {
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
            catch (DbUpdateException)
            {
                return null;
            }
        }

        public List<string> GetRegion()
        {
            BK_Region = null;
            try
            {
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
            catch (DbUpdateException)
            {
                return null;
            }
        }

        public List<string> GetComuna()
        {
            BK_Comuna = null;
            try
            {
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
            catch (DbUpdateException)
            {
                return null;
            }
        }

        public List<string> GetCiudad()
        {
            BK_Ciudad = null;

            try
            {
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

            catch (DbUpdateException)
            {
                return null;
            }
        }
    }
}
