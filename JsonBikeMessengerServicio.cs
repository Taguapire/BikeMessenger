using SQLite;

namespace BikeMessenger
{
    internal class StructBikeMessengerServicio
    {
        public string OPERACION { get; set; }
        public string PKSERVICIO { get; set; }
        public string PENTALPHA { get; set; }
        public string NROENVIO { get; set; }
        public string GUIADESPACHO { get; set; }
        public string FECHA { get; set; }
        public string HORA { get; set; }
        public string CLIENTERUT { get; set; }
        public string CLIENTEDIGVER { get; set; }
        public string MENSAJERORUT { get; set; }
        public string MENSAJERODIGVER { get; set; }
        public string RECURSOID { get; set; }
        public string ODOMICILIO1 { get; set; }
        public string ONUMERO { get; set; }
        public string OPISO { get; set; }
        public string OOFICINA { get; set; }
        public string OCIUDAD { get; set; }
        public string OCOMUNA { get; set; }
        public string OESTADO { get; set; }
        public string OPAIS { get; set; }
        public double OLATITUD { get; set; }
        public double OLONGITUD { get; set; }
        public string DDOMICILIO1 { get; set; }
        public string DNUMERO { get; set; }
        public string DPISO { get; set; }
        public string DOFICINA { get; set; }
        public string DCIUDAD { get; set; }
        public string DCOMUNA { get; set; }
        public string DESTADO { get; set; }
        public string DPAIS { get; set; }
        public double DLATITUD { get; set; }
        public double DLONGITUD { get; set; }
        public string DESCRIPCION { get; set; }
        public int FACTURAS { get; set; }
        public int BULTOS { get; set; }
        public int COMPRAS { get; set; }
        public int CHEQUES { get; set; }
        public int SOBRES { get; set; }
        public int OTROS { get; set; }
        public string OBSERVACIONES { get; set; }
        public string ENTREGA { get; set; }
        public string FECHAENTREGA { get; set; }
        public string HORAENTREGA { get; set; }
        public string RECEPCION { get; set; }
        public string TESPERA { get; set; }
        public double DISTANCIA { get; set; }
        public string PROGRAMADO { get; set; }
        public string RESOPERACION { get; set; }
        public string RESMENSAJE { get; set; }

        public StructBikeMessengerServicio()
        {
            OPERACION = "";
            PENTALPHA = "";
            NROENVIO = "";
            GUIADESPACHO = "";
            FECHA = "";
            HORA = "";
            CLIENTERUT = "";
            CLIENTEDIGVER = "";
            MENSAJERORUT = "";
            MENSAJERODIGVER = "";
            RECURSOID = "";
            ODOMICILIO1 = "";
            ONUMERO = "";
            OPISO = "";
            OOFICINA = "";
            OCIUDAD = "";
            OCOMUNA = "";
            OESTADO = "";
            OPAIS = "";
            OLATITUD = 0;
            OLONGITUD = 0;
            DDOMICILIO1 = "";
            DNUMERO = "";
            DPISO = "";
            DOFICINA = "";
            DCIUDAD = "";
            DCOMUNA = "";
            DESTADO = "";
            DPAIS = "";
            OLATITUD = 0;
            OLONGITUD = 0;
            DESCRIPCION = "";
            FACTURAS = 0;
            BULTOS = 0;
            COMPRAS = 0;
            CHEQUES = 0;
            SOBRES = 0;
            OTROS = 0;
            OBSERVACIONES = "";
            ENTREGA = "";
            FECHAENTREGA = "";
            HORAENTREGA = "";
            RECEPCION = "";
            TESPERA = "";
            DISTANCIA = 0;
            PROGRAMADO = "";
            RESOPERACION = "";
            RESMENSAJE = "";
        }
    }

    internal class TbBikeMessengerServicio
    {
        [Column("operacion")]
        public string OPERACION { get; set; }
        [PrimaryKey]
        [Column("pkservicio")]
        public string PKSERVICIO { get; set; }
        [Column("pentalpha")]
        public string PENTALPHA { get; set; }
        [Column("nroenvio")]
        public string NROENVIO { get; set; }
        [Column("guiadespacho")]
        public string GUIADESPACHO { get; set; }
        [Column("fecha")]
        public string FECHA { get; set; }
        [Column("hora")]
        public string HORA { get; set; }
        [Column("clienterut")]
        public string CLIENTERUT { get; set; }
        [Column("clientedigver")]
        public string CLIENTEDIGVER { get; set; }
        [Column("mensajerorut")]
        public string MENSAJERORUT { get; set; }
        [Column("mensajerodigver")]
        public string MENSAJERODIGVER { get; set; }
        [Column("recursoid")]
        public string RECURSOID { get; set; }
        [Column("odomicilio1")]
        public string ODOMICILIO1 { get; set; }
        [Column("onumero")]
        public string ONUMERO { get; set; }
        [Column("opiso")]
        public string OPISO { get; set; }
        [Column("ooficina")]
        public string OOFICINA { get; set; }
        [Column("ociudad")]
        public string OCIUDAD { get; set; }
        [Column("ocomuna")]
        public string OCOMUNA { get; set; }
        [Column("oestado")]
        public string OESTADO { get; set; }
        [Column("opais")]
        public string OPAIS { get; set; }
        [Column("olatitud")]
        public double OLATITUD { get; set; }
        [Column("olongitud")]
        public double OLONGITUD { get; set; }
        [Column("ddomicilio1")]
        public string DDOMICILIO1 { get; set; }
        [Column("dnumero")]
        public string DNUMERO { get; set; }
        [Column("dpiso")]
        public string DPISO { get; set; }
        [Column("doficina")]
        public string DOFICINA { get; set; }
        [Column("dciudad")]
        public string DCIUDAD { get; set; }
        [Column("dcomuna")]
        public string DCOMUNA { get; set; }
        [Column("destado")]
        public string DESTADO { get; set; }
        [Column("dpais")]
        public string DPAIS { get; set; }
        [Column("dlatitud")]
        public double DLATITUD { get; set; }
        [Column("dlongitud")]
        public double DLONGITUD { get; set; }
        [Column("descripcion")]
        public string DESCRIPCION { get; set; }
        [Column("facturas")]
        public int FACTURAS { get; set; }
        [Column("bultos")]
        public int BULTOS { get; set; }
        [Column("compras")]
        public int COMPRAS { get; set; }
        [Column("cheques")]
        public int CHEQUES { get; set; }
        [Column("sobres")]
        public int SOBRES { get; set; }
        [Column("otros")]
        public int OTROS { get; set; }
        [Column("observaciones")]
        public string OBSERVACIONES { get; set; }
        [Column("entrega")]
        public string ENTREGA { get; set; }
        [Column("fechaentrega")]
        public string FECHAENTREGA { get; set; }
        [Column("horaentrega")]
        public string HORAENTREGA { get; set; }
        [Column("recepcion")]
        public string RECEPCION { get; set; }
        [Column("tespera")]
        public string TESPERA { get; set; }
        [Column("distancia")]
        public double DISTANCIA { get; set; }
        [Column("programado")]
        public string PROGRAMADO { get; set; }
        [Column("resoperacion")]
        public string RESOPERACION { get; set; }
        [Column("resmensaje")]
        public string RESMENSAJE { get; set; }
    }

    internal class TbVistaServicioCliMen
    {
        [Column("nroenvio")]
        public string NROENVIO { get; set; }
        [Column("guiadespacho")]
        public string GUIADESPACHO { get; set; }
        [Column("fechaentrega")]
        public string FECHAENTREGA { get; set; }
        [Column("horaentrega")]
        public string HORAENTREGA { get; set; }
        [Column("nombre")]
        public string NOMBRE { get; set; }
        [Column("apellidos")]
        public string APELLIDOS { get; set; }
        [Column("nombres")]
        public string NOMBRES { get; set; }
        [Column("entrega")]
        public string ENTREGA { get; set; }
        [Column("recepcion")]
        public string RECEPCION { get; set; }
        [Column("distancia")]
        public double DISTANCIA { get; set; }
    }
}
