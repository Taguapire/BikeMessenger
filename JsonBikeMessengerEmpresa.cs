using SQLite;

namespace BikeMessenger
{
    internal class StructBikeMessengerEmpresa
    {
        // Campos de Empresa Json
        public string OPERACION { get; set; }
        public string PENTALPHA { get; set; }
        public string RUTID { get; set; }
        public string DIGVER { get; set; }
        public string NOMBRE { get; set; }
        public string ACTIVIDAD1 { get; set; }
        public string ACTIVIDAD2 { get; set; }
        public string REPRESENTANTE1 { get; set; }
        public string REPRESENTANTE2 { get; set; }
        public string REPRESENTANTE3 { get; set; }
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
        public string TELEFONO1 { get; set; }
        public string TELEFONO2 { get; set; }
        public string TELEFONO3 { get; set; }
        public string OBSERVACIONES { get; set; }
        public string LOGO { get; set; }
        public string RESOPERACION { get; set; }
        public string RESMENSAJE { get; set; }

        public StructBikeMessengerEmpresa()
        {
            OPERACION = "";
            PENTALPHA = "";
            RUTID = "";
            DIGVER = "";
            NOMBRE = "";
            ACTIVIDAD1 = "";
            ACTIVIDAD2 = "";
            REPRESENTANTE1 = "";
            REPRESENTANTE2 = "";
            REPRESENTANTE3 = "";
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
            TELEFONO1 = "";
            TELEFONO2 = "";
            TELEFONO3 = "";
            OBSERVACIONES = "";
            LOGO = "";
            RESOPERACION = "";
            RESMENSAJE = "";
        }
    }

    internal class TbBikeMessengerEmpresa
    {
        [Column("operacion")]
        public string OPERACION { get; set; }
        [PrimaryKey]
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
        [Column("representante3")]
        public string REPRESENTANTE3 { get; set; }
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
        [Column("telefono1")]
        public string TELEFONO1 { get; set; }
        [Column("telefono2")]
        public string TELEFONO2 { get; set; }
        [Column("telefono3")]
        public string TELEFONO3 { get; set; }
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
