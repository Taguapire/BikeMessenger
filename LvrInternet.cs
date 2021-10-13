using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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

        public void LvrInetPOST(string pUrl, string pPort, string pController, string jSon)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler
            {
                UseProxy = false
            });

            try
            {
                // Construct the HttpClient and Uri. This endpoint is for test purposes only.

                string httpsCadena = "";
                string httpResponseBody = "";

                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

                httpsCadena = pUrl;

                if (pPort != "")
                {
                    httpsCadena += ":";
                    httpsCadena += pPort;
                }

                if (pController != "")
                {
                    httpsCadena += pController;
                }

                Uri uri = new Uri(httpsCadena);

                // Construct the JSON to post.
                StringContent content = new StringContent(jSon, System.Text.Encoding.UTF8, "application/json");

                // Post the JSON and wait for a response.
                httpResponseMessage = httpClient.PostAsync(uri, content).Result;

                // Make sure the post succeeded, and write out the response.
                httpResponseBody = httpResponseMessage.Content.ReadAsStringAsync().Result;

                LvrResultadoWeb = httpResponseBody;

                httpClient.CancelPendingRequests();
                httpClient.Dispose();
            }
            catch (HttpRequestException ee)
            {
                httpClient.CancelPendingRequests();
                httpClient.Dispose();
                Console.WriteLine(ee.InnerException.Message);
                Console.WriteLine(ee.Message);
                LvrResultadoWeb = "ERROR";
                return;
            }
            catch (IOException ee)
            {
                httpClient.CancelPendingRequests();
                httpClient.Dispose();
                Console.WriteLine(ee.InnerException.Message);
                Console.WriteLine(ee.Message);
                LvrResultadoWeb = "ERROR";
                return;
            }
            catch (OperationCanceledException ee)
            {
                httpClient.CancelPendingRequests();
                httpClient.Dispose();
                Console.WriteLine(ee.InnerException.Message);
                Console.WriteLine(ee.Message);
                LvrResultadoWeb = "ERROR";
                return;
            }
            catch (AggregateException ee)
            {
                httpClient.CancelPendingRequests();
                httpClient.Dispose();
                Console.WriteLine(ee.InnerException.Message);
                Console.WriteLine(ee.Message);
                LvrResultadoWeb = "ERROR";
                return;
            }
            catch (Exception ee)
            {
                httpClient.CancelPendingRequests();
                httpClient.Dispose();
                Console.WriteLine(ee.InnerException.Message);
                Console.WriteLine(ee.Message);
                LvrResultadoWeb = "ERROR";
                return;
            }
        }
    }
}