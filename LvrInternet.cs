using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using System.Net.Http;


namespace BikeMessenger
{
    class LvrInternet
    {
        public string LvrResultadoWeb { get; set; }

        /*
              void LvrGET(string url)
              {
                  HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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
        */
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

            /*
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            data.Headers.ContentType.CharSet = string.Empty;

            string url = purl;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await client.PostAsync(url, data);
            if (Res.IsSuccessStatusCode)
            {
                LvrResultadoWeb = Res.Content.ReadAsStringAsync().Result;
            }
            else
            {
                LvrResultadoWeb = "ERROR";
            }
            */
        }

        /*
        public void GetInternetServerPrimario(String pParametros)
        {
            LvrGET("https://finanven.ddns.net/Api/BikeMessengerEmpresa?" + pParametros);
        }

        public void GetInternetServerSecundario(String pParametros)
        {
            LvrGET("https://finanven.ddns.net/Api/BikeMessengerEmpresa?" + pParametros);
        }
        */
    }
}