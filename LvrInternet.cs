using System;
using System.Net.Http;
using System.Net;
using System.IO;

namespace BikeMessenger
{
    class LvrInternet
    {
        // Realiza los llamados a internet
        // Empresa      = https://finanven.ddns.net/Api/BikeMessengerEmpresa
        // Cliente      = https://finanven.ddns.net/Api/BikeMessengerCliente
        // Personal     = https://finanven.ddns.net/Api/BikeMessengerPersonal
        // Recurso      = https://finanven.ddns.net/Api/BikeMessengerRecurso
        // Servicio     = https://finanven.ddns.net/Api/BikeMessengerServicio
        // Ajuste       = https://finanven.ddns.net/Api/BikeMessengerAjuste
        // Factura      = https://finanven.ddns.net/Api/BikeMessengerFactura
        // Soporte      = https://finanven.ddns.net/Api/BikeMessengerSoporte

        public string LvrResultadoWeb { get; set; }

        void LvrInetGET(string purl, string json)
        {
            string pParametrosWeb = purl + "?" + json;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(pParametrosWeb);
            request.ContinueTimeout = 60000;
            request.ReadWriteTimeout = 60000;
            request.Timeout = 60000;
            try
            {
                LvrResultadoWeb = "";
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                LvrResultadoWeb = reader.ReadToEnd();
                reader.Close();
                responseStream.Dispose();
                response.Close();
            }
            catch (WebException)
            {
                // Tratar la excepcion de null
                LvrResultadoWeb = "ERROR";
            }
        }

        public void LvrInetPOST(string purl, string json)
        {
            try
            {
                // Construct the HttpClient and Uri. This endpoint is for test purposes only.


                System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage httpResponseMessage;
                string httpResponseBody;
                Uri uri = new Uri(purl);

                // Construct the JSON to post.
                StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                // Post the JSON and wait for a response.
                httpResponseMessage = httpClient.PostAsync(purl, content).Result;

                // Make sure the post succeeded, and write out the response.
                httpResponseBody = httpResponseMessage.Content.ReadAsStringAsync().Result;

                LvrResultadoWeb = httpResponseBody;
            }
            catch (Exception)
            {
                LvrResultadoWeb = "ERROR";
            }
        }
    }
}