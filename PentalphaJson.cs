using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMessenger
{
    internal class PentalphaJson
    {
        public string Pentalpha { get; set; }
        public string Rutid { get; set; }
        public string Digver { get; set; }
        public string Remoto { get; set; }
        public string Propio { get; set; }
        public string Estado { get; set; }
        public string Nombre { get; set; }
        public string Pais { get; set; }

        public PentalphaJson()
        {
            Pentalpha = ""; // Identificacion de la empresa (Generado por BikeMessenger)
            Rutid = "";     // Identificacion tributaria o de usuario
            Digver = "";    // Identificacion tributaria o de usuario
            Remoto = "";    // Permiso para usar Pentalpha Server (S,N)
            Propio = "";    // Permiso para usar Servidor Propio (S,N)
            Estado = "";    // Tipo o Estado de la licencia de uso (GRATIS, REMOTO, PROPIO, TODOS);
            Nombre = "";    // Nombre de la Empresa
            Pais = "";      // Codigo de Pais o Pais (En estudio)
        }
    }
}
