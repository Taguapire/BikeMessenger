namespace BikeMessenger
{
    public class PentalphaJson
    {
        public string PENTALPHA { get; set; }
        public string EMPRESA { get; set; }
        public string USUARIO { get; set; }
        public string CLAVE { get; set; }
        public string RUTID { get; set; }
        public string DIGVER { get; set; }
        public string REMOTO { get; set; }
        public string PROPIO { get; set; }
        public string LICENCIA { get; set; }

        public PentalphaJson()
        {
            PENTALPHA = "";     // Identificacion de la empresa (Generado por BikeMessenger)
            EMPRESA = "";       // Nombre de la Empresa
            USUARIO = "";       // Identificacion de usuario
            CLAVE = "";         // Clave de usuario
            RUTID = "";         // Identificacion tributaria o de usuario
            DIGVER = "";        // Identificacion tributaria o de usuario
            REMOTO = "";        // Permiso para usar Pentalpha Server (S,N)
            PROPIO = "";        // Permiso para usar Servidor Propio (S,N)
            LICENCIA = "";      // Tipo o Estado de la licencia de uso (GRATIS, DEMO, REMOTO, PROPIO);
        }
    }
}
