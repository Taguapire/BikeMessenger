using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMessenger
{
    internal class Bm_Cotizacion_Database
    {
        private TransferVar BM_TransferVar = new TransferVar();

        // Valode de Memoria a Bases de Datos

        private StructBikeMessengerCotizacion BK_Cotizacion;
        private List<StructBikeMessengerCotizacion> BK_CotizacionLista;

        private string CompletoNombreBD = "";

        public Bm_Cotizacion_Database()
        {
            CompletoNombreBD = BM_TransferVar.DIRECTORIO_BASE_LOCAL + "\\BikeMessenger.db";
        }

        // Busqueda por Muchos
        public List<StructBikeMessengerCotizacion> BuscarCotizacion(string pPENTALPHA)
        {
            BK_CotizacionLista = new List<StructBikeMessengerCotizacion>();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerCotizacion> results = BM_ConexionLite.Table<TbBikeMessengerCotizacion>().Where(t => t.PENTALPHA == pPENTALPHA).ToList();

            if (results.Count > 0)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    BK_Cotizacion = new StructBikeMessengerCotizacion
                    {
                        OPERACION = "BUSCAR",
                        PKCOTIZACION = results[i].PKCOTIZACION,
                        PENTALPHA = results[i].PENTALPHA,
                        COTIZACION = results[i].COTIZACION,
                        NOMBRE = results[i].NOMBRE,
                        TELEFONO = results[i].TELEFONO,
                        EMAIL = results[i].EMAIL,
                        OCALLE = results[i].OCALLE,
                        ONUMERO = results[i].ONUMERO,
                        ODPTO = results[i].ODPTO,
                        OOFICINA = results[i].OOFICINA,
                        OCASA = results[i].OCASA,
                        OCOMUNA = results[i].OCOMUNA,
                        ONOMBRE = results[i].ONOMBRE,
                        OTELEFONO = results[i].OTELEFONO,
                        DCALLE = results[i].DCALLE,
                        DNUMERO = results[i].DNUMERO,
                        DDPTO = results[i].DDPTO,
                        DOFICINA = results[i].DOFICINA,
                        DCASA = results[i].DCASA,
                        DCOMUNA = results[i].DCOMUNA,
                        DNOMBRE = results[i].DNOMBRE,
                        DTELEFONO = results[i].DTELEFONO,
                        REGRESODE = results[i].REGRESODE,
                        TIPOCARGA = results[i].TIPOCARGA,
                        LARGO = results[i].LARGO,
                        ANCHO = results[i].ANCHO,
                        ALTO = results[i].ALTO,
                        PESO = results[i].PESO,
                        DESCRIPCION = results[i].DESCRIPCION,
                        FECHAENTREGA = results[i].FECHAENTREGA,
                        HORAENTREGA = results[i].HORAENTREGA,
                        DISTANCIA = results[i].DISTANCIA,
                        RESMENSAJE = "OK",
                        RESOPERACION = "OK"
                    };
                    BK_CotizacionLista.Add(BK_Cotizacion);
                }
            }
            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();
            BM_ConexionLite = null;

            return BK_CotizacionLista;
        }

        public List<StructBikeMessengerCotizacion> BuscarCotizacion(string pPENTALPHA, string pCOTIZACION)
        {
            string VPKCOTIZACION = pPENTALPHA + pCOTIZACION;

            BK_CotizacionLista = new List<StructBikeMessengerCotizacion>();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerCotizacion> results = BM_ConexionLite.Table<TbBikeMessengerCotizacion>().Where(t => t.PKCOTIZACION == VPKCOTIZACION).ToList();

            if (results.Count > 0)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    BK_Cotizacion = new StructBikeMessengerCotizacion
                    {
                        OPERACION = "BUSCAR",
                        PKCOTIZACION = results[i].PKCOTIZACION,
                        PENTALPHA = results[i].PENTALPHA,
                        COTIZACION = results[i].COTIZACION,
                        NOMBRE = results[i].NOMBRE,
                        TELEFONO = results[i].TELEFONO,
                        EMAIL = results[i].EMAIL,
                        OCALLE = results[i].OCALLE,
                        ONUMERO = results[i].ONUMERO,
                        ODPTO = results[i].ODPTO,
                        OOFICINA = results[i].OOFICINA,
                        OCASA = results[i].OCASA,
                        OCOMUNA = results[i].OCOMUNA,
                        ONOMBRE = results[i].ONOMBRE,
                        OTELEFONO = results[i].OTELEFONO,
                        DCALLE = results[i].DCALLE,
                        DNUMERO = results[i].DNUMERO,
                        DDPTO = results[i].DDPTO,
                        DOFICINA = results[i].DOFICINA,
                        DCASA = results[i].DCASA,
                        DCOMUNA = results[i].DCOMUNA,
                        DNOMBRE = results[i].DNOMBRE,
                        DTELEFONO = results[i].DTELEFONO,
                        REGRESODE = results[i].REGRESODE,
                        TIPOCARGA = results[i].TIPOCARGA,
                        LARGO = results[i].LARGO,
                        ANCHO = results[i].ANCHO,
                        ALTO = results[i].ALTO,
                        PESO = results[i].PESO,
                        DESCRIPCION = results[i].DESCRIPCION,
                        FECHAENTREGA = results[i].FECHAENTREGA,
                        HORAENTREGA = results[i].HORAENTREGA,
                        DISTANCIA = results[i].DISTANCIA,
                        RESMENSAJE = "OK",
                        RESOPERACION = "OK"
                    };
                    BK_CotizacionLista.Add(BK_Cotizacion);
                }
            }
            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();
            BM_ConexionLite = null;

            return BK_CotizacionLista;
        }

        public bool AgregarCotizacion(StructBikeMessengerCotizacion aBK_Cotizacion)
        {
            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                TbBikeMessengerCotizacion record = new TbBikeMessengerCotizacion
                {
                    OPERACION = "AGREGAR",
                    PKCOTIZACION = aBK_Cotizacion.PKCOTIZACION,
                    PENTALPHA = aBK_Cotizacion.PENTALPHA,
                    COTIZACION = aBK_Cotizacion.COTIZACION,
                    NOMBRE = aBK_Cotizacion.NOMBRE,
                    TELEFONO = aBK_Cotizacion.TELEFONO,
                    EMAIL = aBK_Cotizacion.EMAIL,
                    OCALLE = aBK_Cotizacion.OCALLE,
                    ONUMERO = aBK_Cotizacion.ONUMERO,
                    ODPTO = aBK_Cotizacion.ODPTO,
                    OOFICINA = aBK_Cotizacion.OOFICINA,
                    OCASA = aBK_Cotizacion.OCASA,
                    OCOMUNA = aBK_Cotizacion.OCOMUNA,
                    ONOMBRE = aBK_Cotizacion.ONOMBRE,
                    OTELEFONO = aBK_Cotizacion.OTELEFONO,
                    DCALLE = aBK_Cotizacion.DCALLE,
                    DNUMERO = aBK_Cotizacion.DNUMERO,
                    DDPTO = aBK_Cotizacion.DDPTO,
                    DOFICINA = aBK_Cotizacion.DOFICINA,
                    DCASA = aBK_Cotizacion.DCASA,
                    DCOMUNA = aBK_Cotizacion.DCOMUNA,
                    DNOMBRE = aBK_Cotizacion.DNOMBRE,
                    DTELEFONO = aBK_Cotizacion.DTELEFONO,
                    REGRESODE = aBK_Cotizacion.REGRESODE,
                    TIPOCARGA = aBK_Cotizacion.TIPOCARGA,
                    LARGO = aBK_Cotizacion.LARGO,
                    ANCHO = aBK_Cotizacion.ANCHO,
                    ALTO = aBK_Cotizacion.ALTO,
                    PESO = aBK_Cotizacion.PESO,
                    DESCRIPCION = aBK_Cotizacion.DESCRIPCION,
                    FECHAENTREGA = aBK_Cotizacion.FECHAENTREGA,
                    HORAENTREGA = aBK_Cotizacion.HORAENTREGA,
                    DISTANCIA = aBK_Cotizacion.DISTANCIA,
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

        public bool ModificarCotizacion(StructBikeMessengerCotizacion mBK_Cotizacion)
        {
            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                TbBikeMessengerCotizacion record = new TbBikeMessengerCotizacion
                {
                    OPERACION = "MODIFICAR",
                    PKCOTIZACION = mBK_Cotizacion.PKCOTIZACION,
                    PENTALPHA = mBK_Cotizacion.PENTALPHA,
                    COTIZACION = mBK_Cotizacion.COTIZACION,
                    NOMBRE = mBK_Cotizacion.NOMBRE,
                    TELEFONO = mBK_Cotizacion.TELEFONO,
                    EMAIL = mBK_Cotizacion.EMAIL,
                    OCALLE = mBK_Cotizacion.OCALLE,
                    ONUMERO = mBK_Cotizacion.ONUMERO,
                    ODPTO = mBK_Cotizacion.ODPTO,
                    OOFICINA = mBK_Cotizacion.OOFICINA,
                    OCASA = mBK_Cotizacion.OCASA,
                    OCOMUNA = mBK_Cotizacion.OCOMUNA,
                    ONOMBRE = mBK_Cotizacion.ONOMBRE,
                    OTELEFONO = mBK_Cotizacion.OTELEFONO,
                    DCALLE = mBK_Cotizacion.DCALLE,
                    DNUMERO = mBK_Cotizacion.DNUMERO,
                    DDPTO = mBK_Cotizacion.DDPTO,
                    DOFICINA = mBK_Cotizacion.DOFICINA,
                    DCASA = mBK_Cotizacion.DCASA,
                    DCOMUNA = mBK_Cotizacion.DCOMUNA,
                    DNOMBRE = mBK_Cotizacion.DNOMBRE,
                    DTELEFONO = mBK_Cotizacion.DTELEFONO,
                    REGRESODE = mBK_Cotizacion.REGRESODE,
                    TIPOCARGA = mBK_Cotizacion.TIPOCARGA,
                    LARGO = mBK_Cotizacion.LARGO,
                    ANCHO = mBK_Cotizacion.ANCHO,
                    ALTO = mBK_Cotizacion.ALTO,
                    PESO = mBK_Cotizacion.PESO,
                    DESCRIPCION = mBK_Cotizacion.DESCRIPCION,
                    FECHAENTREGA = mBK_Cotizacion.FECHAENTREGA,
                    HORAENTREGA = mBK_Cotizacion.HORAENTREGA,
                    DISTANCIA = mBK_Cotizacion.DISTANCIA,
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

        public bool BorrarCotizacion(string pPENTALPHA, string pCOTIZACION)
        {
            string VPKCOTIZACION = pPENTALPHA + pCOTIZACION;

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                _ = BM_ConexionLite.Delete<TbBikeMessengerServicio>(VPKCOTIZACION);
            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();
            return true;
        }

        // Utilitarios

        public List<GridListViewCotizaciones> ListaViewCotizaciones()
        {
            List<GridListViewCotizaciones> GridCotizacionesLista = new List<GridListViewCotizaciones>();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbVistaCotizacionCliMen> results = BM_ConexionLite.Query<TbVistaCotizacionCliMen>("select * from Vista_Cotizacion_CliMen");

            for (int i = 0; i < results.Count; i++)
            {
                GridCotizacionesLista.Add(new GridListViewCotizaciones
                {
                    COTIZACION = results[i].COTIZACION,
                    CLIENTE = results[i].NOMBRE,
                    TIPOCARGA = results[i].TIPOCARGA,
                    ORIGEN = results[i].ORIGEN,
                    DESTINO = results[i].DESTINO,
                    FECHA_ENTREGA = results[i].FECHAENTREGA,
                    HORA_ENTREGA = results[i].HORAENTREGA,
                    DISTANCIA = results[i].DISTANCIA
                });
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return GridCotizacionesLista;
        }
    }

    internal class GridListViewCotizaciones
    {
        public string COTIZACION { get; set; }
        public string CLIENTE { get; set; }
        public string TIPOCARGA { get; set; }
        public string ORIGEN { get; set; }
        public string DESTINO { get; set; }
        public string FECHA_ENTREGA { get; set; }
        public string HORA_ENTREGA { get; set; }
        public double DISTANCIA { get; set; }

        public GridListViewCotizaciones()
        {
            COTIZACION = "";
            CLIENTE = "";
            TIPOCARGA = "";
            ORIGEN = "";
            DESTINO = "";
            FECHA_ENTREGA = "";
            HORA_ENTREGA = "";
            DISTANCIA = 0;
        }
    }
}