using System;

namespace BikeMessenger
{
    internal class LvrTablaHtml
    {
        public string BufferHtml { get; set; }

        private string TituloDocumento;
        private string Escribir;

        public void CrearTexto(string pTituloDocumento)
        {
            Escribir = "";
            TituloDocumento = pTituloDocumento;
        }

        public void InicioDocumento()
        {
            Escribir += "<!DOCTYPE html>";
            Escribir += "<html>";
            Escribir += "<head>";
            Escribir += "<title>" + TituloDocumento + "</title>";
            Escribir += "<style>";
            Escribir += "table {";
            Escribir += "    font-family: arial, sans-serif;";
            Escribir += "    border-collapse: collapse;";
            Escribir += "width: 100 %;";
            Escribir += "}";
            Escribir += "td, th {";
            Escribir += "  border: 1px solid #dddddd;";
            Escribir += "  text - align: left;";
            Escribir += "  padding: 8px;";
            Escribir += "}";
            Escribir += "tr: nth-child(even) {";
            Escribir += "    background-color: #dddddd;";
            Escribir += "}";
            Escribir += "</style>";
            Escribir += "</head>";
            Escribir += "<body>";
        }

        public void FinDocumento()
        {
            Escribir += "</table>";
            Escribir += "</body>";
            Escribir += "</html>";
            BufferHtml = Escribir;
        }

        public void AbrirFila()
        {
            Escribir += "<tr>";
        }

        public void CerrarFila()
        {
            Escribir += "</tr>";
        }

        public void AgregarCampo(string Campo, bool Centrar)
        {
            if (Centrar)
            {
                Escribir += "<td>" + Campo + "</td>";
            }
            else
            {
                Escribir += "<td>" + Campo + "</td>";
            }
        }

        public void AbrirEncabezado()
        {
            Escribir += "<table>";
            Escribir += "<tr>";
        }

        public void CerrarEncabezado()
        {
            Escribir += "</tr>";
        }

        public void AgregarEncabezado(String Campo)
        {
            Escribir += "<th>" + Campo + "</th>";
        }

        public void AgregarTituloTabla(String Campo)
        {

            Escribir += "<h2><CENTER>" + Campo + "</CENTER></h2>";
        }

        public void AgregarFechaTabla(string Campo)
        {
            Escribir += "<h3><CENTER>" + Campo + "</CENTER></h3>";
        }
    }
}
