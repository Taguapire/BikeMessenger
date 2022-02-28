
using Matrix;
using Newtonsoft.Json;

namespace BikeMessenger
{
    // Procesar aqui el mensaje recivido
    // Tipos de Mensajes:
    //  01.- Aviso General del Administrador
    //  02.- Aviso de Aceptación de Servicio
    //  03.- Aviso de Rechazo de Servicio
    //  04.- Aviso de Entrega de Servicio
    //  05.- Aviso de No entrega de Servicio
    //  06.- Aviso de Cancelación de Servicio
    //  07.- Aviso de Solicitud de Cotización
    //  08.- Aviso de Costo de Servicio Cotizado
    //  09.- Aviso de Aceptación de Cotización
    //  10.- Aviso de Solicitud de Soporte
    //  11.- Aviso de Reclamo de Cliente
    //  12.- Aviso de Puntaje de Evaluación de Servicio
    //  13.- Aviso de Pago de Servicio por Tarjeta
    //  14.- Aviso de Pago de Servicio por Transferencia
    //  15.- Aviso de Pago de Servicio en Efectivo
    //  16.- Aviso de Pago Fallido
    //  17.- Aviso de Traspaso de Servicio

    internal class ComunicacionXMPP
    {
        public XmppClient xmppClient { get; set; }
        public string CadenaJsonXMPP { get; private set; }
        
        public ComunicacionXMPP()
        {

        }
        // 01.- Aviso General del Administrador
        public void ProcesarJsonAvisoDeAdministrador(string pAviso)
        {
            ;
        }

        //  02.- Aviso de Asignación/Aceptación de Servicio
        //  Aviso a Cliente y a Mensajero
        public void ProcesarJsoAsignacionDeServicio(StructAsignacionDeServicio pServicioXMPP)
        {
            StructAsignacionDeServicio AvisoEnvioXMPP = new StructAsignacionDeServicio();
            AvisoEnvioXMPP.ENVIO = pServicioXMPP.ENVIO;
            AvisoEnvioXMPP.FECHA = pServicioXMPP.FECHA;
            AvisoEnvioXMPP.HORA = pServicioXMPP.HORA;
            AvisoEnvioXMPP.CLIENTE = pServicioXMPP.CLIENTE;
            AvisoEnvioXMPP.DESCRIPCION = pServicioXMPP.DESCRIPCION;
            AvisoEnvioXMPP.LATITUDORIGEN = pServicioXMPP.LATITUDORIGEN;
            AvisoEnvioXMPP.LONGITUDORIGEN = pServicioXMPP.LONGITUDORIGEN;
            AvisoEnvioXMPP.LATITUDDESTINO = pServicioXMPP.LATITUDDESTINO;
            AvisoEnvioXMPP.LONGITUDDESTINO = pServicioXMPP.LONGITUDDESTINO;
            AvisoEnvioXMPP.DISTANCIA = pServicioXMPP.DISTANCIA;
            CadenaJsonXMPP = JsonConvert.SerializeObject(AvisoEnvioXMPP);
        }

        //  03.- Aviso de Rechazo de Servicio
        public void ProcesarJsonRechazoDeServicio(string pAviso)
        {
            ;
        }

        //  04.- Aviso de Entrega de Servicio
        public void ProcesarJsonEntregaDeServicio(string pAviso)
        {
            ;
        }

        //  05.- Aviso de No entrega de Servicio
        public void ProcesarJsonNoEntregaDeServicio(string pAviso)
        {
            ;
        }

        //  06.- Aviso de Cancelación de Servicio
        public void ProcesarJsonNoCancelacionDeServicio(string pAviso)
        {
            ;
        }

        //  07.- Aviso de Solicitud de Cotización
        public void ProcesarJsonSolicitudDeCotizacion(string pAviso)
        {
            ;
        }

        //  08.- Aviso de Costo de Servicio Cotizado
        public void ProcesarJsonCostoServicioCotizado(string pAviso)
        {
            ;
        }

        //  09.- Aviso de Aceptación de Cotización
        public void ProcesarJsonAceptacionDeCotizacion(string pAviso)
        {
            ;
        }

        //  10.- Aviso de Solicitud de Soporte
        public void ProcesarJsonSolicitudDeSoporte(string pAviso)
        {
            // Verificar si lo solicita el Mensajero o el Cliente
            ;
        }

        //  11.- Aviso de Reclamo de Cliente
        public void ProcesarJsonReclamoDeCliente(string pAviso)
        {
            ;
        }

        //  12.- Aviso de Puntaje de Evaluación de Servicio
        public void ProcesarJsonEvaluacionDeServicio(string pAviso)
        {
            ;
        }

        //  13.- Aviso de Pago de Servicio por Tarjeta
        public void ProcesarJsonPagoConTarjeta(string pAviso)
        {
            ;
        }

        //  14.- Aviso de Pago de Servicio por Transferencia
        public void ProcesarJsonPagoPorTranferencia(string pAviso)
        {
            ;
        }

        //  15.- Aviso de Pago de Servicio en Efectivo
        public void ProcesarJsonPagoEnEfectivo(string pAviso)
        {
            ;
        }

        //  16.- Aviso de Pago Fallido
        public void ProcesarJsonPagoFallido(string pAviso)
        {
            ;
        }

        //  17.- Aviso de Traspaso de Servicio
        public void ProcesarJsonTraspasoDeServicio(string pAviso)
        {
            ;
        }
    }

    internal class StructAsignacionDeServicio
    {
        public string ENVIO { get; set; }
        public string FECHA { get; set; }
        public string HORA { get; set; }
        public string CLIENTE { get; set; }
        public string DESCRIPCION { get; set; }
        public double LATITUDORIGEN { get; set; }
        public double LONGITUDORIGEN { get; set; }
        public double LATITUDDESTINO { get; set; }
        public double LONGITUDDESTINO { get; set; }
        public double DISTANCIA { get; set; }

        public StructAsignacionDeServicio()
        {
            ENVIO = "";
            FECHA = "";
            HORA = "";
            CLIENTE = "";
            DESCRIPCION = "";
            LATITUDORIGEN = 0;
            LONGITUDORIGEN = 0;
            LATITUDDESTINO = 0;
            LONGITUDDESTINO = 0;
            DISTANCIA = 0;
        }
    }


    /**
     * AsignarServicio.ProcesarJson(AvisoEnvioData);
       if (LvrTransferVar.MOBILES_XMPP == "S")
       {
            string lMensajero = "";
            lMensajero += ServicioIO.MENSAJERORUT;
            lMensajero += "-";
            lMensajero += ServicioIO.MENSAJERODIGVER;
            lMensajero += "@";
            lMensajero +=
            lMensajero += LvrTransferVar.DOMINIO_XMPP;
            _ = xmppClient.SendChatMessageAsync(lMensajero, AsignarServicio.CadenaJsonXMPP);
        }
    **/
}
