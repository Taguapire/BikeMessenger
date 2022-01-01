using SQLite;

namespace BikeMessenger
{
    internal class StructBikeMessengerCliente
    {
        // Campos de Cliente Json
        public string OPERACION { get; set; }
        public string PKCLIENTE { get; set; }
        public string PENTALPHA { get; set; }
        public string RUTID { get; set; }
        public string DIGVER { get; set; }
        public string NOMBRE { get; set; }
        public string ACTIVIDAD1 { get; set; }
        public string ACTIVIDAD2 { get; set; }
        public string REPRESENTANTE1 { get; set; }
        public string REPRESENTANTE2 { get; set; }
        public string EMAIL { get; set; }
        public string TELEFONO1 { get; set; }
        public string TELEFONO2 { get; set; }
        public string DOMICILIO1 { get; set; }
        public string DOMICILIO2 { get; set; }
        public string NUMERO { get; set; }
        public string PISO { get; set; }
        public string OFICINA { get; set; }
        public string CIUDAD { get; set; }
        public string COMUNA { get; set; }
        public string ESTADOREGION { get; set; }
        public string CODIGOPOSTAL { get; set; }
        public string PAIS { get; set; }
        public string OBSERVACIONES { get; set; }
        public string LOGO { get; set; }
        public string RESOPERACION { get; set; }
        public string RESMENSAJE { get; set; }

        public StructBikeMessengerCliente()
        {
            OPERACION = "";
            PKCLIENTE = "";
            PENTALPHA = "";
            RUTID = "";
            DIGVER = "";
            NOMBRE = "";
            ACTIVIDAD1 = "";
            ACTIVIDAD2 = "";
            REPRESENTANTE1 = "";
            REPRESENTANTE2 = "";
            EMAIL = "";
            TELEFONO1 = "";
            TELEFONO2 = "";
            DOMICILIO1 = "";
            DOMICILIO2 = "";
            NUMERO = "";
            PISO = "";
            OFICINA = "";
            CIUDAD = "";
            COMUNA = "";
            ESTADOREGION = "";
            CODIGOPOSTAL = "";
            PAIS = "";
            OBSERVACIONES = "";
            LOGO = "";
            RESOPERACION = "";
            RESMENSAJE = "";
        }
    }

    internal class TbBikeMessengerCliente
    {
        [Column("operacion")]
        public string OPERACION { get; set; }
        [PrimaryKey]
        [Column("pkcliente")]
        public string PKCLIENTE { get; set; }
        [Column("pentalpha")]
        public string PENTALPHA { get; set; }
        [Column("rutid")]
        public string RUTID { get; set; }
        [Column("digver")]
        public string DIGVER { get; set; }
        [Column("nombre")]
        public string NOMBRE { get; set; }
        [Column("actividad1")]
        public string ACTIVIDAD1 { get; set; }
        [Column("actividad2")]
        public string ACTIVIDAD2 { get; set; }
        [Column("representante1")]
        public string REPRESENTANTE1 { get; set; }
        [Column("representante2")]
        public string REPRESENTANTE2 { get; set; }
        [Column("email")]
        public string EMAIL { get; set; }
        [Column("telefono1")]
        public string TELEFONO1 { get; set; }
        [Column("telefono2")]
        public string TELEFONO2 { get; set; }
        [Column("domicilio1")]
        public string DOMICILIO1 { get; set; }
        [Column("domicilio2")]
        public string DOMICILIO2 { get; set; }
        [Column("numero")]
        public string NUMERO { get; set; }
        [Column("piso")]
        public string PISO { get; set; }
        [Column("oficina")]
        public string OFICINA { get; set; }
        [Column("ciudad")]
        public string CIUDAD { get; set; }
        [Column("comuna")]
        public string COMUNA { get; set; }
        [Column("estadoregion")]
        public string ESTADOREGION { get; set; }
        [Column("codigopostal")]
        public string CODIGOPOSTAL { get; set; }
        [Column("pais")]
        public string PAIS { get; set; }
        [Column("observaciones")]
        public string OBSERVACIONES { get; set; }
        [Column("logo")]
        public string LOGO { get; set; }
        [Column("resoperacion")]
        public string RESOPERACION { get; set; }
        [Column("resmensaje")]
        public string RESMENSAJE { get; set; }
    }
}

