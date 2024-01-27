using System.Collections.Generic;
using SQLite;

namespace BikeMessenger
{
    internal class Bm_Empresa_Database
    {
        private TransferVar BM_TransferVar = new TransferVar();

        // Valode de Memoria a Bases de Datos

        private StructBikeMessengerEmpresa BK_Empresa;
        private List<StructBikeMessengerEmpresa> BK_EmpresaLista;

        private string CompletoNombreBD = "";

        public Bm_Empresa_Database()
        {
            CompletoNombreBD = BM_TransferVar.DIRECTORIO_BASE_LOCAL + "\\BikeMessenger.db";
        }

        // Busqueda General
        // Busqueda

        public List<StructBikeMessengerEmpresa> BuscarEmpresa()
        {
            BK_Empresa = new StructBikeMessengerEmpresa();
            BK_EmpresaLista = new List<StructBikeMessengerEmpresa>();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerEmpresa> results = BM_ConexionLite.Table<TbBikeMessengerEmpresa>().ToList();

            if (results.Count > 0)
            {
                BK_Empresa.OPERACION = "CONSULTAR";
                BK_Empresa.PENTALPHA = results[0].PENTALPHA;
                BK_Empresa.RUTID = results[0].RUTID;
                BK_Empresa.DIGVER = results[0].DIGVER;
                BK_Empresa.NOMBRE = results[0].NOMBRE;
                BK_Empresa.ACTIVIDAD1 = results[0].ACTIVIDAD1;
                BK_Empresa.ACTIVIDAD2 = results[0].ACTIVIDAD2;
                BK_Empresa.REPRESENTANTE1 = results[0].REPRESENTANTE1;
                BK_Empresa.REPRESENTANTE2 = results[0].REPRESENTANTE2;
                BK_Empresa.REPRESENTANTE3 = results[0].REPRESENTANTE3;
                BK_Empresa.DOMICILIO1 = results[0].DOMICILIO1;
                BK_Empresa.DOMICILIO2 = results[0].DOMICILIO2;
                BK_Empresa.NUMERO = results[0].NUMERO;
                BK_Empresa.PISO = results[0].PISO;
                BK_Empresa.OFICINA = results[0].OFICINA;
                BK_Empresa.CIUDAD = results[0].CIUDAD;
                BK_Empresa.COMUNA = results[0].COMUNA;
                BK_Empresa.ESTADOREGION = results[0].ESTADOREGION;
                BK_Empresa.CODIGOPOSTAL = results[0].CODIGOPOSTAL;
                BK_Empresa.PAIS = results[0].PAIS;
                BK_Empresa.TELEFONO1 = results[0].TELEFONO1;
                BK_Empresa.TELEFONO2 = results[0].TELEFONO2;
                BK_Empresa.TELEFONO3 = results[0].TELEFONO3;
                BK_Empresa.OBSERVACIONES = results[0].OBSERVACIONES;
                BK_Empresa.LOGO = results[0].LOGO;
                BK_Empresa.RESOPERACION = "OK";
                BK_Empresa.RESMENSAJE = results[0].RESMENSAJE;
                BK_EmpresaLista.Add(BK_Empresa);
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return BK_EmpresaLista;
        }

        // Busqueda con Clave
        public List<StructBikeMessengerEmpresa> BuscarEmpresa(string pPENTALPHA)
        {
            BK_Empresa = new StructBikeMessengerEmpresa();
            BK_EmpresaLista = new List<StructBikeMessengerEmpresa>();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerEmpresa> results = BM_ConexionLite.Table<TbBikeMessengerEmpresa>().Where(t => t.PENTALPHA == pPENTALPHA).ToList();

            if (results.Count > 0)
            {
                BK_Empresa.OPERACION = "CONSULTAR";
                BK_Empresa.PENTALPHA = results[0].PENTALPHA;
                BK_Empresa.RUTID = results[0].RUTID;
                BK_Empresa.DIGVER = results[0].DIGVER;
                BK_Empresa.NOMBRE = results[0].NOMBRE;
                BK_Empresa.ACTIVIDAD1 = results[0].ACTIVIDAD1;
                BK_Empresa.ACTIVIDAD2 = results[0].ACTIVIDAD2;
                BK_Empresa.REPRESENTANTE1 = results[0].REPRESENTANTE1;
                BK_Empresa.REPRESENTANTE2 = results[0].REPRESENTANTE2;
                BK_Empresa.REPRESENTANTE3 = results[0].REPRESENTANTE3;
                BK_Empresa.DOMICILIO1 = results[0].DOMICILIO1;
                BK_Empresa.DOMICILIO2 = results[0].DOMICILIO2;
                BK_Empresa.NUMERO = results[0].NUMERO;
                BK_Empresa.PISO = results[0].PISO;
                BK_Empresa.OFICINA = results[0].OFICINA;
                BK_Empresa.CIUDAD = results[0].CIUDAD;
                BK_Empresa.COMUNA = results[0].COMUNA;
                BK_Empresa.ESTADOREGION = results[0].ESTADOREGION;
                BK_Empresa.CODIGOPOSTAL = results[0].CODIGOPOSTAL;
                BK_Empresa.PAIS = results[0].PAIS;
                BK_Empresa.TELEFONO1 = results[0].TELEFONO1;
                BK_Empresa.TELEFONO2 = results[0].TELEFONO2;
                BK_Empresa.TELEFONO3 = results[0].TELEFONO3;
                BK_Empresa.OBSERVACIONES = results[0].OBSERVACIONES;
                BK_Empresa.LOGO = results[0].LOGO;
                BK_Empresa.RESOPERACION = "OK";
                BK_Empresa.RESMENSAJE = results[0].RESMENSAJE;
                BK_EmpresaLista.Add(BK_Empresa);
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return BK_EmpresaLista;
        }

        public bool AgregarEmpresa(StructBikeMessengerEmpresa aBK_Empresa)
        {
            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                TbBikeMessengerEmpresa record = new TbBikeMessengerEmpresa
                {
                    OPERACION = "AGREGAR",
                    PENTALPHA = aBK_Empresa.PENTALPHA,
                    RUTID = aBK_Empresa.RUTID,
                    DIGVER = aBK_Empresa.DIGVER,
                    NOMBRE = aBK_Empresa.NOMBRE,
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
                    LOGO = aBK_Empresa.LOGO,
                    RESOPERACION = aBK_Empresa.RESOPERACION,
                    RESMENSAJE = aBK_Empresa.RESMENSAJE
                };
                _ = BM_ConexionLite.InsertOrReplace(record);

            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return true;
        }

        public bool ModificarEmpresa(StructBikeMessengerEmpresa mBK_Empresa)
        {
            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                TbBikeMessengerEmpresa record = new TbBikeMessengerEmpresa
                {
                    OPERACION = "MODIFICAR",
                    PENTALPHA = mBK_Empresa.PENTALPHA,
                    RUTID = mBK_Empresa.RUTID,
                    DIGVER = mBK_Empresa.DIGVER,
                    NOMBRE = mBK_Empresa.NOMBRE,
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
                    LOGO = mBK_Empresa.LOGO,
                    RESOPERACION = mBK_Empresa.RESOPERACION,
                    RESMENSAJE = mBK_Empresa.RESMENSAJE
                };
                _ = BM_ConexionLite.InsertOrReplace(record);
            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return true;
        }

        public bool BorrarEmpresa(string pPENTALPHA)
        {
            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                _ = BM_ConexionLite.Delete<TbBikeMessengerEmpresa>(pPENTALPHA);
            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return true;
        }
    }
}