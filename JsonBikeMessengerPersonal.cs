using SQLite;

namespace BikeMessenger
{
    internal class StructBikeMessengerPersonal
    {
        // Campos de Cliente Json
        public string OPERACION { get; set; }
        public string PKPERSONAL { get; set; }
        public string PENTALPHA { get; set; }
        public string RUTID { get; set; }
        public string DIGVER { get; set; }
        public string APELLIDOS { get; set; }
        public string NOMBRES { get; set; }
        public string TELEFONO1 { get; set; }
        public string TELEFONO2 { get; set; }
        public string EMAIL { get; set; }
        public string AUTORIZACION { get; set; }
        public string CARGO { get; set; }
        public string DOMICILIO { get; set; }
        public string NUMERO { get; set; }
        public string PISO { get; set; }
        public string DPTO { get; set; }
        public string CODIGOPOSTAL { get; set; }
        public string CIUDAD { get; set; }
        public string COMUNA { get; set; }
        public string REGION { get; set; }
        public string PAIS { get; set; }
        public string OBSERVACIONES { get; set; }
        public string FOTO { get; set; }
        public string RESOPERACION { get; set; }
        public string RESMENSAJE { get; set; }

        public StructBikeMessengerPersonal()
        {
            OPERACION = "";
            PKPERSONAL = "";
            PENTALPHA = "";
            RUTID = "";
            DIGVER = "";
            APELLIDOS = "";
            NOMBRES = "";
            TELEFONO1 = "";
            TELEFONO2 = "";
            EMAIL = "";
            AUTORIZACION = "";
            CARGO = "";
            DOMICILIO = "";
            NUMERO = "";
            PISO = "";
            DPTO = "";
            CODIGOPOSTAL = "";
            CIUDAD = "";
            COMUNA = "";
            REGION = "";
            PAIS = "";
            OBSERVACIONES = "";
            FOTO = "";
            RESOPERACION = "";
            RESMENSAJE = "";
        }
    }

    internal class TbBikeMessengerPersonal
    {
        [Column("operacion")]
        public string OPERACION { get; set; }
        [PrimaryKey]
        [Column("pkpersonal")]
        public string PKPERSONAL { get; set; }
        [Column("pentalpha")]
        public string PENTALPHA { get; set; }
        [Column("rutid")]
        public string RUTID { get; set; }
        [Column("digver")]
        public string DIGVER { get; set; }
        [Column("apellidos")]
        public string APELLIDOS { get; set; }
        [Column("nombres")]
        public string NOMBRES { get; set; }
        [Column("telefono1")]
        public string TELEFONO1 { get; set; }
        [Column("telefono2")]
        public string TELEFONO2 { get; set; }
        [Column("email")]
        public string EMAIL { get; set; }
        [Column("autorizacion")]
        public string AUTORIZACION { get; set; }
        [Column("cargo")]
        public string CARGO { get; set; }
        [Column("domicilio")]
        public string DOMICILIO { get; set; }
        [Column("numero")]
        public string NUMERO { get; set; }
        [Column("piso")]
        public string PISO { get; set; }
        [Column("dpto")]
        public string DPTO { get; set; }
        [Column("codigopostal")]
        public string CODIGOPOSTAL { get; set; }
        [Column("ciudad")]
        public string CIUDAD { get; set; }
        [Column("comuna")]
        public string COMUNA { get; set; }
        [Column("region")]
        public string REGION { get; set; }
        [Column("pais")]
        public string PAIS { get; set; }
        [Column("observaciones")]
        public string OBSERVACIONES { get; set; }
        [Column("foto")]
        public string FOTO { get; set; }
        [Column("resoperacion")]
        public string RESOPERACION { get; set; }
        [Column("resmensaje")]
        public string RESMENSAJE { get; set; }
    }
}
