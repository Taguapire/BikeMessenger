namespace BikeMessenger
{
    internal class PentalphaJson
    {
        public string PENTALPHA { get; set; }
        public string USUARIO { get; set; }
        public string CLAVE { get; set; }
        public string RUTID { get; set; }
        public string DIGVER { get; set; }
        public string REMOTO { get; set; }
        public string PROPIO { get; set; }
        public string ESTADO { get; set; }
        public string NOMBRE { get; set; }
        public string PAIS { get; set; }

        public PentalphaJson()
        {
            PENTALPHA = ""; // Identificacion de la empresa (Generado por BikeMessenger)
            USUARIO = "";   // Identificacion de usuario
            CLAVE = "";     // Clave de usuario
            RUTID = "";     // Identificacion tributaria o de usuario
            DIGVER = "";    // Identificacion tributaria o de usuario
            REMOTO = "";    // Permiso para usar Pentalpha Server (S,N)
            PROPIO = "";   // Permiso para usar Servidor Propio (S,N)
            ESTADO = "";    // Tipo o Estado de la licencia de uso (GRATIS, REMOTO, PROPIO, TODOS);
            NOMBRE = "";    // Nombre de la Empresa
            PAIS = "";      // Codigo de Pais o Pais (En estudio)
        }
    }
}
