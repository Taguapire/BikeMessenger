using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMessenger
{
    class LvrProcesoInternet
    {
        /*
        //**************************************************
        // Ejecuta operacion de envio de servicios
        //**************************************************
        private async void BtnEnviarOrdenDeServicio(object sender, object e)
        {
            string LvrPClientePentalpha = "PENTALPHA=";
            string LvrPOperacion = "&OPERACION=";
            string LvrPCliente = "&CLIENTE=";
            string LvrPUsuario = "&USUARIO=";
            string LvrPClave = "&CLAVE=";
            string LvrPJSON = "&JSON=";
            string LvrPEnvioServer;
            string LvrPRecibirServer;
            string LvrPData;

            CecomInternet LvrCecomInternet = new CecomInternet();

            List<CecomLogisTransJson> EnviarJsonCecomArray = new List<CecomLogisTransJson>();
            List<CecomLogisTransJson> RecibirJsonCecomArray = new List<CecomLogisTransJson>();

            if (boxTextUsuario.Text == "" || passwordBoxClave.Password == "")
            {
                _ = AvisoOperacionPersonalDialogAsync("Identificación de Usuario", "Debe completar su usuario y clave para el envio de la Orden de Servicio.");
                return;
            }

            // Proceso Pantalla a Memoria
            if (!ProcesarPantallaMemoria())
            {
                return;
            }

            // Muestra de espera
            appBarEnviar.IsEnabled = false;
            LvrProgresRing.IsActive = true;
            await Task.Delay(500); // 1 sec delay

            // Proceso Serializar

            EnviarJsonCecomArray.Add(EnviarJsonCecom);
            LvrPData = JsonConvert.SerializeObject(EnviarJsonCecomArray);

            LvrTansferVar.GrabarJson(LvrPData);

            // Agregar Cliente Pentalpha BikeMessenger
            LvrPClientePentalpha += EnviarJsonCecom.Pentalpha_Cliente;
            // Agregar Operacion
            LvrPOperacion += EnviarJsonCecom.Transaccion_Tipo;
            // Agregar Cliente
            LvrPCliente += EnviarJsonCecom.CeComCliente;
            // Agregar Usuario
            LvrPUsuario += EnviarJsonCecom.CeComUsuario;
            // Agregar Clave
            LvrPClave += EnviarJsonCecom.CeComClave;
            // Agregar JSON
            LvrPJSON += LvrPData;

            LvrPEnvioServer = LvrPClientePentalpha + LvrPOperacion + LvrPCliente + LvrPUsuario + LvrPClave + LvrPJSON;

            LvrCecomInternet.AccesoInternetServerPrimario(LvrPEnvioServer);
            LvrPRecibirServer = LvrCecomInternet.LvrResultadoWeb;

            if (LvrPRecibirServer != "ERROR" && LvrPRecibirServer != "" && LvrPRecibirServer != null)
            {
                // Procesar primer servidor
                RecibirJsonCecomArray = JsonConvert.DeserializeObject<List<CecomLogisTransJson>>(LvrPRecibirServer); // resp será el string JSON a deserializa
                RecibirJsonCecom = RecibirJsonCecomArray[0];

                _ = AvisoOperacionPersonalDialogAsync("Estado Envio", RecibirJsonCecom.CeComResultadoMsg);

                appBarEnviar.IsEnabled = true;
                LvrProgresRing.IsActive = false;
                await Task.Delay(500); // 1 sec delay

                return;
            }
            else
            {
                LvrCecomInternet.AccesoInternetServerSecundario(LvrPEnvioServer);
                LvrPRecibirServer = LvrCecomInternet.LvrResultadoWeb;

                if (LvrPRecibirServer != "ERROR" && LvrPRecibirServer != "" && LvrPRecibirServer != null)
                {
                    // Procesar segundo servidor
                    RecibirJsonCecomArray = JsonConvert.DeserializeObject<List<CecomLogisTransJson>>(LvrPRecibirServer); // resp será el string JSON a deserializa
                    RecibirJsonCecom = RecibirJsonCecomArray[0];

                    _ = AvisoOperacionPersonalDialogAsync("Estado Envio", RecibirJsonCecom.CeComResultadoMsg);

                    appBarEnviar.IsEnabled = true;
                    LvrProgresRing.IsActive = false;
                    await Task.Delay(500); // 1 sec delay

                    return;
                }
            }

            _ = AvisoOperacionPersonalDialogAsync("Estado Envio", "Error en Procedimientos de Envios, debe reintentarlo.");

            appBarEnviar.IsEnabled = true;
            LvrProgresRing.IsActive = false;
            await Task.Delay(500); // 1 sec delay
        }
        */
    }
}
