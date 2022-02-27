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
        [Column("fechaentrega")]
        public string FECHAENTREGA { get; set; }
        [Column("horaentrega")]
        public string HORAENTREGA { get; set; }
        [Column("nombre")]
        public string NOMBRE { get; set; }
        [Column("distancia")]
        public double DISTANCIA { get; set; }
    }
}
