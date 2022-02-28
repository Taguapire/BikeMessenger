using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMessenger
{
    internal class StructBikeMessengerCotizacion
    {
        public string OPERACION { get; set; }
        public string PKCOTIZACION { get; set; }
        public string PENTALPHA { get; set; }
        public string COTIZACION { get; set; }
        public string NOMBRE { get; set; }
        public string TELEFONO { get; set; }
        public string EMAIL { get; set; }
        public string OCALLE { get; set; }
        public string ONUMERO { get; set; }
        public string ODPTO { get; set; }
        public string OOFICINA { get; set; }
        public string OCASA { get; set; }
        public string OCOMUNA { get; set; }
        public string ONOMBRE { get; set; }
        public string OTELEFONO { get; set; }
        public string DCALLE { get; set; }
        public string DNUMERO { get; set; }
        public string DDPTO { get; set; }
        public string DOFICINA { get; set; }
        public string DCASA { get; set; }
        public string DCOMUNA { get; set; }
        public string DNOMBRE { get; set; }
        public string DTELEFONO { get; set; }
        public string REGRESODE { get; set; }
        public string TIPOCARGA { get; set; }
        public double LARGO { get; set; }
        public double ANCHO { get; set; }
        public double ALTO { get; set; }
        public double PESO { get; set; }
        public string DESCRIPCION { get; set; }
        public string FECHAENTREGA { get; set; }
        public string HORAENTREGA { get; set; }
        public double DISTANCIA { get; set; }
        public string RESOPERACION { get; set; }
        public string RESMENSAJE { get; set; }

        public StructBikeMessengerCotizacion()
        {
            OPERACION = "";
            PENTALPHA = "";
            COTIZACION = "";
            NOMBRE = "";
            TELEFONO = "";
            EMAIL = "";
            OCALLE = "";
            ONUMERO = "";
            ODPTO = "";
            OOFICINA = "";
            OCASA = "";
            OCOMUNA = "";
            ONOMBRE = "";
            OTELEFONO = "";
            DCALLE = "";
            DNUMERO = "";
            DDPTO = "";
            DOFICINA = "";
            DCASA = "";
            DCOMUNA = "";
            DNOMBRE = "";
            DTELEFONO = "";
            REGRESODE = "";
            TIPOCARGA = "";
            LARGO = 0;
            ANCHO = 0;
            ALTO = 0;
            PESO = 0;
            DESCRIPCION = "";
            FECHAENTREGA = "";
            HORAENTREGA = "";
            DISTANCIA = 0;
            RESOPERACION = "";
            RESMENSAJE = "";
        }
    }

    internal class TbBikeMessengerCotizacion
    {
        [Column("operacion")]
        public string OPERACION { get; set; }
        [PrimaryKey]
        [Column("pkcotizacion")]
        public string PKCOTIZACION { get; set; }
        [Column("pentalpha")]
        public string PENTALPHA { get; set; }
        [Column("cotizacion")]
        public string COTIZACION { get; set; }
        [Column("nombre")]
        public string NOMBRE { get; set; }
        [Column("telefono")]
        public string TELEFONO { get; set; }
        [Column("email")]
        public string EMAIL { get; set; }
        [Column("ocalle")]
        public string OCALLE { get; set; }
        [Column("onumero")]
        public string ONUMERO { get; set; }
        [Column("odpto")]
        public string ODPTO { get; set; }
        [Column("ooficina")]
        public string OOFICINA { get; set; }
        [Column("ocasa")]
        public string OCASA { get; set; }
        [Column("ocomuna")]
        public string OCOMUNA { get; set; }
        [Column("onombre")]
        public string ONOMBRE { get; set; }
        [Column("otelefono")]
        public string OTELEFONO { get; set; }
        [Column("dcalle")]
        public string DCALLE { get; set; }
        [Column("dnumero")]
        public string DNUMERO { get; set; }
        [Column("ddpto")]
        public string DDPTO { get; set; }
        [Column("doficina")]
        public string DOFICINA { get; set; }
        [Column("dcasa")]
        public string DCASA { get; set; }
        [Column("dcomuna")]
        public string DCOMUNA { get; set; }
        [Column("dnombre")]
        public string DNOMBRE { get; set; }
        [Column("dtelefono")]
        public string DTELEFONO { get; set; }
        [Column("regresode")]
        public string REGRESODE { get; set; }
        [Column("tipocarga")]
        public string TIPOCARGA { get; set; }
        [Column("largo")]
        public double LARGO { get; set; }
        [Column("ancho")]
        public double ANCHO { get; set; }
        [Column("alto")]
        public double ALTO { get; set; }
        [Column("peso")]
        public double PESO { get; set; }
        [Column("descripcion")]
        public string DESCRIPCION { get; set; }
        [Column("fechaentrega")]
        public string FECHAENTREGA { get; set; }
        [Column("horaentrega")]
        public string HORAENTREGA { get; set; }
        [Column("distancia")]
        public double DISTANCIA { get; set; }
        [Column("resoperacion")]
        public string RESOPERACION { get; set; }
        [Column("resmensaje")]
        public string RESMENSAJE { get; set; }
    }

    internal class TbVistaCotizacionCliMen
    {
        [Column("cotizacion")]
        public string COTIZACION { get; set; }
        [Column("nombre")]
        public string NOMBRE { get; set; }
        [Column("tipocarga")]
        public string TIPOCARGA { get; set; }
        [Column("origen")]
        public string ORIGEN { get; set; }
        [Column("destino")]
        public string DESTINO { get; set; }
        [Column("fechaentrega")]
        public string FECHAENTREGA { get; set; }
        [Column("horaentrega")]
        public string HORAENTREGA { get; set; }
        [Column("distancia")]
        public double DISTANCIA { get; set; }
    }
}
