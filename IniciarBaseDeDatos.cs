using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMessenger
{
    class IniciarBaseDeDatos
    {
        string TabESTADOREGION;
        string TabCOMUNA;
        string TabCIUDAD;
        string TabPAIS;
        string TabEMPRESA;
        string TabCLIENTES;
        string TabPERSONAL;
        string TabRECURSOS;
        string TabSERVICIOS;
        string IndRECURSOS_IDX1;

        void AsignarValores()
        {
            TabESTADOREGION = "CREATE TABLE IF NOT EXISTS ESTADOREGION (";
            TabESTADOREGION += "CODPAIS	INTEGER NOT NULL,";
            TabESTADOREGION += "CODREGION	INTEGER NOT NULL,";
            TabESTADOREGION += "REGION	TEXT NOT NULL,";
            TabESTADOREGION += "PRIMARY KEY(CODPAIS,CODREGION)";
            TabESTADOREGION += ")";

            TabCOMUNA = "CREATE TABLE IF NOT EXISTS COMUNA (";
            TabCOMUNA += "CODPAIS	INTEGER NOT NULL,";
            TabCOMUNA += "CODREGION	INTEGER NOT NULL,";
            TabCOMUNA += "CODCOMU	INTEGER NOT NULL,";
            TabCOMUNA += "COMUNA	TEXT NOT NULL,";
            TabCOMUNA += "PRIMARY KEY(CODPAIS,CODREGION,CODCOMU)";
            TabCOMUNA += ")";

            TabCIUDAD = "CREATE TABLE IF NOT EXISTS CIUDAD (";
            TabCIUDAD += "PAIS	INTEGER NOT NULL,";
            TabCIUDAD += "REGION	INTEGER NOT NULL,";
            TabCIUDAD += "COMUNA	INTEGER NOT NULL,";
            TabCIUDAD += "CODCIUDAD	INTEGER NOT NULL,";
            TabCIUDAD += "CIUDAD	TEXT NOT NULL,";
            TabCIUDAD += "PRIMARY KEY(PAIS,REGION,COMUNA,CODCIUDAD)";
            TabCIUDAD += ")";

            TabPAIS = "CREATE TABLE IF NOT EXISTS PAIS (";
            TabPAIS += "CODPAIS	INTEGER NOT NULL,";
            TabPAIS += "PAIS	TEXT NOT NULL,";
            TabPAIS += "PRIMARY KEY(CODPAIS)";
            TabPAIS += ")";

            TabEMPRESA = "CREATE TABLE IF NOT EXISTS EMPRESA (";
            TabEMPRESA += "RUTID	TEXT NOT NULL,";
            TabEMPRESA += "DIGVER	TEXT NOT NULL,";
            TabEMPRESA += "NOMBRE	TEXT,";
            TabEMPRESA += "ACTIVIDAD1	TEXT,";
            TabEMPRESA += "ACTIVIDAD2	TEXT,";
            TabEMPRESA += "REPRESENTANTE1	TEXT,";
            TabEMPRESA += "REPRESENTANTE2	TEXT,";
            TabEMPRESA += "REPRESENTANTE3	TEXT,";
            TabEMPRESA += "DOMICILIO1	TEXT,";
            TabEMPRESA += "DOMICILIO2	TEXT,";
            TabEMPRESA += "NUMERO	TEXT,";
            TabEMPRESA += "PISO	TEXT,";
            TabEMPRESA += "OFICINA	TEXT,";
            TabEMPRESA += "CIUDAD	TEXT,";
            TabEMPRESA += "COMUNA	TEXT,";
            TabEMPRESA += "ESTADOREGION	TEXT,";
            TabEMPRESA += "CODIGOPOSTAL	TEXT,";
            TabEMPRESA += "PAIS	TEXT,";
            TabEMPRESA += "OBSERVACIONES	TEXT,";
            TabEMPRESA += "LOGO	BLOB,";
            TabEMPRESA += "PRIMARY KEY(RUTID,DIGVER)";
            TabEMPRESA += ")";

            TabCLIENTES = "CREATE TABLE IF NOT EXISTS CLIENTES (";
            TabCLIENTES += "RUTID	TEXT,";
            TabCLIENTES += "DIGVER	TEXT,";
            TabCLIENTES += "NOMBRE	TEXT,";
            TabCLIENTES += "ACTIVIDAD1	TEXT,";
            TabCLIENTES += "ACTIVIDAD2	TEXT,";
            TabCLIENTES += "REPRESENTANTE1	TEXT,";
            TabCLIENTES += "REPRESENTANTE2	TEXT,";
            TabCLIENTES += "REPRESENTANTE3	TEXT,";
            TabCLIENTES += "TELEFONO1	TEXT,";
            TabCLIENTES += "TELEFONO2	TEXT,";
            TabCLIENTES += "DOMICILIO1	TEXT,";
            TabCLIENTES += "DOMICILIO2	TEXT,";
            TabCLIENTES += "NUMERO	TEXT,";
            TabCLIENTES += "PISO	TEXT,";
            TabCLIENTES += "OFICINA	TEXT,";
            TabCLIENTES += "CODIGOPOSTAL	TEXT,";
            TabCLIENTES += "CIUDAD	TEXT,";
            TabCLIENTES += "COMUNA	TEXT,";
            TabCLIENTES += "REGION	TEXT,";
            TabCLIENTES += "PAIS	TEXT,";
            TabCLIENTES += "OBSERVACIONES	TEXT,";
            TabCLIENTES += "FOTO	BLOB,";
            TabCLIENTES += "PRIMARY KEY(RUTID,DIGVER)";
            TabCLIENTES += ")";

            TabPERSONAL = "CREATE TABLE IF NOT EXISTS PERSONAL (";
            TabPERSONAL += "RUTID	TEXT,";
            TabPERSONAL += "DIGVER	TEXT,";
            TabPERSONAL += "APELLIDOS	TEXT,";
            TabPERSONAL += "NOMBRES	TEXT,";
            TabPERSONAL += "TELEFONO1	TEXT,";
            TabPERSONAL += "TELEFONO2	TEXT,";
            TabPERSONAL += "EMAIL	TEXT,";
            TabPERSONAL += "AUTORIZACION	TEXT,";
            TabPERSONAL += "CARGO	TEXT,";
            TabPERSONAL += "DOMICILIO	TEXT,";
            TabPERSONAL += "NUMERO	TEXT,";
            TabPERSONAL += "PISO	TEXT,";
            TabPERSONAL += "DPTO	TEXT,";
            TabPERSONAL += "CODIGOPOSTAL	TEXT,";
            TabPERSONAL += "CIUDAD	TEXT,";
            TabPERSONAL += "COMUNA	TEXT,";
            TabPERSONAL += "REGION	TEXT,";
            TabPERSONAL += "PAIS	TEXT,";
            TabPERSONAL += "OBSERVACIONES	TEXT,";
            TabPERSONAL += "FOTO	BLOB,";
            TabPERSONAL += "PRIMARY KEY(RUTID,DIGVER)";
            TabPERSONAL += ")";

            TabRECURSOS = "CREATE TABLE IF NOT EXISTS RECURSOS (";
            TabRECURSOS += "RUTID	TEXT,";
            TabRECURSOS += "DIGVER	TEXT,";
            TabRECURSOS += "PROPIETARIO	TEXT,";
            TabRECURSOS += "TIPO	TEXT,";
            TabRECURSOS += "PATENTE	TEXT,";
            TabRECURSOS += "MARCA	TEXT,";
            TabRECURSOS += "MODELO	TEXT,";
            TabRECURSOS += "VARIANTE	TEXT,";
            TabRECURSOS += "ANO	TEXT,";
            TabRECURSOS += "COLOR	TEXT,";
            TabRECURSOS += "CIUDAD	TEXT,";
            TabRECURSOS += "COMUNA	TEXT,";
            TabRECURSOS += "REGION	TEXT,";
            TabRECURSOS += "PAIS	TEXT,";
            TabRECURSOS += "OBSERVACIONES	TEXT,";
            TabRECURSOS += "FOTO	BLOB,";
            TabRECURSOS += "PRIMARY KEY(PATENTE)";
            TabRECURSOS += ")";

            TabSERVICIOS = "CREATE TABLE IF NOT EXISTS SERVICIOS (";
            TabSERVICIOS += "NROENVIO	TEXT,";
            TabSERVICIOS += "GUIADESPACHO	TEXT,";
            TabSERVICIOS += "FECHA	TEXT,";
            TabSERVICIOS += "HORA	TEXT,";
            TabSERVICIOS += "CLIENTERUT	TEXT,";
            TabSERVICIOS += "CLIENTEDIGVER	TEXT,";
            TabSERVICIOS += "MENSAJERORUT	TEXT,";
            TabSERVICIOS += "MENSAJERODIGVER	TEXT,";
            TabSERVICIOS += "RECURSOID	TEXT,";
            TabSERVICIOS += "ODOMICILIO1	TEXT,";
            TabSERVICIOS += "ODOMICILIO2	TEXT,";
            TabSERVICIOS += "ONUMERO	TEXT,";
            TabSERVICIOS += "OPISO	TEXT,";
            TabSERVICIOS += "OOFICINA	TEXT,";
            TabSERVICIOS += "OCIUDAD	TEXT,";
            TabSERVICIOS += "OCOMUNA	TEXT,";
            TabSERVICIOS += "OESTADO	TEXT,";
            TabSERVICIOS += "OPAIS	TEXT,";
            TabSERVICIOS += "OCOORDENADAS	TEXT,";
            TabSERVICIOS += "DDOMICILIO1	TEXT,";
            TabSERVICIOS += "DDOMICILIO2	TEXT,";
            TabSERVICIOS += "DNUMERO	TEXT,";
            TabSERVICIOS += "DPISO	TEXT,";
            TabSERVICIOS += "DOFICINA	TEXT,";
            TabSERVICIOS += "DCIUDAD	TEXT,";
            TabSERVICIOS += "DCOMUNA	TEXT,";
            TabSERVICIOS += "DESTADO	TEXT,";
            TabSERVICIOS += "DPAIS	TEXT,";
            TabSERVICIOS += "DCOORDENADAS	TEXT,";
            TabSERVICIOS += "DESCRIPCION	TEXT,";
            TabSERVICIOS += "OBSERVACIONES	TEXT,";
            TabSERVICIOS += "ENTREGA	TEXT,";
            TabSERVICIOS += "RECEPCION	TEXT,";
            TabSERVICIOS += "TESPERA	TEXT,";
            TabSERVICIOS += "FECHAENTREGA	TEXT,";
            TabSERVICIOS += "HORAENTREGA	TEXT,";
            TabSERVICIOS += "DISTANCIA	TEXT,";
            TabSERVICIOS += "PROGRAMADO	TEXT,";
            TabSERVICIOS += "PRIMARY KEY(NROENVIO)";
            TabSERVICIOS += ")";

            IndRECURSOS_IDX1 = "CREATE UNIQUE INDEX IF NOT EXISTS RECURSOS_IDX1 ON RECURSOS (";
            IndRECURSOS_IDX1 += "RUTID,";
            IndRECURSOS_IDX1 += "DIGVER,";
            IndRECURSOS_IDX1 += "PATENTE";
            IndRECURSOS_IDX1 += ")";
        }
    }
}
