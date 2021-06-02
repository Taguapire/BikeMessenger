using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMessenger
{
    internal class JsonBikeMessengerRecurso
    {
        // Campos de Cliente Json
        public string OPERACION { get; set; }
        public string PENTALPHA { get; set; }
        public string PATENTE { get; set; }
        public string RUTID { get; set; }
        public string DIGVER { get; set; }
        public string PROPIETARIO { get; set; }
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

        public JsonBikeMessengerRecurso()
        {
            OPERACION = "";
            PENTALPHA = "";
            PATENTE = "";
            RUTID = "";
            DIGVER = "";
            PROPIETARIO = "";
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
}
