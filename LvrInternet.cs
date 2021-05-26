using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace BikeMessenger
{
    class LvrInternet
    {
        public string LvrResultadoWeb { get; set; }

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

        public void AccesoInternetServerPrimario(String pParametros)
        {
            LvrGET("https://finanven.ddns.net/Api/BikeMessenger?" + pParametros);
        }

        public void AccesoInternetServerSecundario(String pParametros)
        {
            LvrGET("https://finanven.ddns.net/Api/BikeMessenger?" + pParametros);
        }
    }
}