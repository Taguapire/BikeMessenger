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
            /*
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
            */
            Escribir += "<!DOCTYPE html PUBLIC \"-//W3C//DTC XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">";
            Escribir += "<html xmlns=\"http://www.w3.org/1990/xhtml\" lang=\"en\" xml:lang=\"es\">";
            Escribir += "<head>";
            Escribir += "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />";
            Escribir += "<title>" + TituloDocumento + "</title>";
            Escribir += "</head>";
            Escribir += "<body>";
        }

        public void FinDocumento()
        {
            Escribir += "</table>";
            Escribir += "</div>";
            Escribir += "</div>";
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
                Escribir += "<td align=\"center\" style=\"border:1px solid black; background-color: #F5F5F5\">" + Campo + "</td>";
            }
            else
            {
                Escribir += "<td align =\"left\" style=\"border:1px solid black; background-color: #F5F5F5\">" + Campo + "</td>";
            }
        }

        public void AbrirEncabezado()
        {
            /*
            Escribir += "<table style=\"border:1px solid black;border-collapse:collapse;\">";
            Escribir += "<tr>";
            */
            Escribir += "<div align=\"center\" style=\"vertical-align:bottom\">";
            Escribir += "<div align=\"center\" style=\"vertical-align:bottom\">";
            Escribir += "<table style=\"border:1px solid black;border-collapse:collapse;\">";
            Escribir += "<tr>";
        }

        public void CerrarEncabezado()
        {
            Escribir += "</tr>";
        }

        public void AgregarEncabezado(string Campo)
        {
            Escribir += "<th style=\"border:1px solid black; background-color: #f1f1c1;\">" + Campo + "</th>";
        }

        public void AgregarTituloTabla(string Campo)
        {

            Escribir += "<h2><CENTER>" + Campo + "</CENTER></h2>";
        }

        public void AgregarFechaTabla(string Campo)
        {
            Escribir += "<h3><CENTER>" + Campo + "</CENTER></h3>";
        }
    }
}
