
using Newtonsoft.Json;

namespace BikeMessenger
{
    internal class ComunicacionXMPP
    {
        private AvisoDeEnvio AvisoEnvioXMPP = new AvisoDeEnvio();
        public string CadenaJsonXMPP { get; private set; }
        
        public ComunicacionXMPP()
        {

        }

        public void ProcesarJson(AvisoDeEnvio pServicioXMPP)
        {
            AvisoEnvioXMPP.ENVIO = pServicioXMPP.ENVIO;
            AvisoEnvioXMPP.FECHA = pServicioXMPP.FECHA;
            AvisoEnvioXMPP.HORA = pServicioXMPP.HORA;
            AvisoEnvioXMPP.CLIENTE = pServicioXMPP.CLIENTE;
            AvisoEnvioXMPP.DESCRIPCION = pServicioXMPP.DESCRIPCION;
            AvisoEnvioXMPP.LATITUDORIGEN = pServicioXMPP.LATITUDORIGEN;
            AvisoEnvioXMPP.LONGITUDORIGEN = pServicioXMPP.LONGITUDORIGEN;
            AvisoEnvioXMPP.LATITUDDESTINO = pServicioXMPP.LATITUDDESTINO;
            AvisoEnvioXMPP.LONGITUDDESTINO = pServicioXMPP.LONGITUDDESTINO;
            AvisoEnvioXMPP.DISTANCIA = pServicioXMPP.DISTANCIA;
            CadenaJsonXMPP = JsonConvert.SerializeObject(AvisoEnvioXMPP);
        }
    }

    internal class AvisoDeEnvio
    {
        public string ENVIO { get; set; }
        public string FECHA { get; set; }
        public string HORA { get; set; }
        public string CLIENTE { get; set; }
        public string DESCRIPCION { get; set; }
        public double LATITUDORIGEN { get; set; }
        public double LONGITUDORIGEN { get; set; }
        public double LATITUDDESTINO { get; set; }
        public double LONGITUDDESTINO { get; set; }
        public double DISTANCIA { get; set; }

        public AvisoDeEnvio()
        {
            ENVIO = "";
            FECHA = "";
            HORA = "";
            CLIENTE = "";
            DESCRIPCION = "";
            LATITUDORIGEN = 0;
            LONGITUDORIGEN = 0;
            LATITUDDESTINO = 0;
            LONGITUDDESTINO = 0;
            DISTANCIA = 0;
        }
    }
}
