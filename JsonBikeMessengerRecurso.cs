using SQLite;

namespace BikeMessenger
{
    internal class StructBikeMessengerRecurso
    {
        // Campos de Cliente Json
        public string OPERACION { get; set; }
        public string PKRECURSO { get; set; }
        public string PENTALPHA { get; set; }
        public string PATENTE { get; set; }
        public string RUTID { get; set; }
        public string DIGVER { get; set; }
        public string TIPO { get; set; }
        public string MARCA { get; set; }
        public string MODELO { get; set; }
        public string VARIANTE { get; set; }
        public string ANO { get; set; }
        public string COLOR { get; set; }
        public string CIUDAD { get; set; }
        public string COMUNA { get; set; }
        public string REGION { get; set; }
        public string PAIS { get; set; }
        public string OBSERVACIONES { get; set; }
        public string FOTO { get; set; }
        public string RESOPERACION { get; set; }
        public string RESMENSAJE { get; set; }

        public StructBikeMessengerRecurso()
        {
            OPERACION = "";
            PKRECURSO = "";
            PENTALPHA = "";
            PATENTE = "";
            RUTID = "";
            DIGVER = "";
            TIPO = "";
            MARCA = "";
            MODELO = "";
            VARIANTE = "";
            ANO = "";
            COLOR = "";
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

    internal class TbBikeMessengerRecurso
    {
        [Column("operacion")]
        public string OPERACION { get; set; }
        [PrimaryKey]
        [Column("pkrecurso")]
        public string PKRECURSO { get; set; }
        [Column("pentalpha")]
        public string PENTALPHA { get; set; }
        [Column("patente")]
        public string PATENTE { get; set; }
        [Column("rutid")]
        public string RUTID { get; set; }
        [Column("digver")]
        public string DIGVER { get; set; }
        [Column("tipo")]
        public string TIPO { get; set; }
        [Column("marca")]
        public string MARCA { get; set; }
        [Column("modelo")]
        public string MODELO { get; set; }
        [Column("variante")]
        public string VARIANTE { get; set; }
        [Column("ano")]
        public string ANO { get; set; }
        [Column("color")]
        public string COLOR { get; set; }
        [Column("pkpersonal")]
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
