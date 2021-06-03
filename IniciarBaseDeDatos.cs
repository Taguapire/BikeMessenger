using Microsoft.Data.Sqlite;

namespace BikeMessenger
{
    internal class IniciarBaseDeDatos
    {
        private string TabESTADOREGION { get; set; }
        private string TabCOMUNA { get; set; }
        private string TabCIUDAD { get; set; }
        private string TabPAIS { get; set; }
        private string TabEMPRESA { get; set; }
        private string TabCLIENTES { get; set; }
        private string TabPERSONAL { get; set; }
        private string TabRECURSOS { get; set; }
        private string TabSERVICIOS { get; set; }
        private string IndRECURSOS_IDX1 { get; set; }

        private string EmpresaTriggerPaisInsert { get; set; }
        private string EmpresaTriggerPaisUpdate { get; set; }
        private string EmpresaTriggerRegionInsert { get; set; }
        private string EmpresaTriggerRegionUpdate { get; set; }
        private string EmpresaTriggerComunaInsert { get; set; }
        private string EmpresaTriggerComunaUpdate { get; set; }
        private string EmpresaTriggerCiudadInsert { get; set; }
        private string EmpresaTriggerCiudadUpdate { get; set; }

        private string PersonalTriggerPaisInsert { get; set; }
        private string PersonalTriggerPaisUpdate { get; set; }
        private string PersonalTriggerRegionInsert { get; set; }
        private string PersonalTriggerRegionUpdate { get; set; }
        private string PersonalTriggerComunaInsert { get; set; }
        private string PersonalTriggerComunaUpdate { get; set; }
        private string PersonalTriggerCiudadInsert { get; set; }
        private string PersonalTriggerCiudadUpdate { get; set; }

        private string RecursosTriggerPaisInsert { get; set; }
        private string RecursosTriggerPaisUpdate { get; set; }
        private string RecursosTriggerRegionInsert { get; set; }
        private string RecursosTriggerRegionUpdate { get; set; }
        private string RecursosTriggerComunaInsert { get; set; }
        private string RecursosTriggerComunaUpdate { get; set; }
        private string RecursosTriggerCiudadInsert { get; set; }
        private string RecursosTriggerCiudadUpdate { get; set; }

        private string ClientesTriggerPaisInsert { get; set; }
        private string ClientesTriggerPaisUpdate { get; set; }
        private string ClientesTriggerRegionInsert { get; set; }
        private string ClientesTriggerRegionUpdate { get; set; }
        private string ClientesTriggerComunaInsert { get; set; }
        private string ClientesTriggerComunaUpdate { get; set; }
        private string ClientesTriggerCiudadInsert { get; set; }
        private string ClientesTriggerCiudadUpdate { get; set; }

        private readonly SqliteConnection IDB_Connection;

        public IniciarBaseDeDatos(SqliteConnection pIDB_Connection)
        {
            IDB_Connection = pIDB_Connection;
            AsignarValores();
            ProcTabPAIS();
            ProcTabESTADOREGION();
            ProcTabCOMUNA();
            ProcTabCIUDAD();
            ProcTabEMPRESA();
            ProcTabCLIENTES();
            ProcTabPERSONAL();
            ProcTabRECURSOS();
            ProcTabSERVICIOS();
            ProcIndINDICES();
            ProcTRIGGERSEmpresa();
            ProcTRIGGERSPersonal();
            ProcTRIGGERSRecursos();
            ProcTRIGGERSClientes();
            // ProcTRIGGERSServicios();
        }

        private void AsignarValores()
        {
            TabESTADOREGION = "CREATE TABLE IF NOT EXISTS ESTADOREGION (";
            TabESTADOREGION += "CODREGION INTEGER NOT NULL,";
            TabESTADOREGION += "REGION	TEXT NOT NULL,";
            TabESTADOREGION += "PRIMARY KEY(CODREGION AUTOINCREMENT)";
            TabESTADOREGION += ")";

            TabCOMUNA = "CREATE TABLE IF NOT EXISTS COMUNA (";
            TabCOMUNA += "CODCOMU	INTEGER NOT NULL,";
            TabCOMUNA += "COMUNA	TEXT NOT NULL,";
            TabCOMUNA += "PRIMARY KEY(CODCOMU AUTOINCREMENT)";
            TabCOMUNA += ")";

            TabCIUDAD = "CREATE TABLE IF NOT EXISTS CIUDAD (";
            TabCIUDAD += "CODCIUDAD	INTEGER NOT NULL,";
            TabCIUDAD += "CIUDAD	TEXT NOT NULL,";
            TabCIUDAD += "PRIMARY KEY(CODCIUDAD AUTOINCREMENT)";
            TabCIUDAD += ")";

            TabPAIS = "CREATE TABLE IF NOT EXISTS PAIS (";
            TabPAIS += "CODPAIS	INTEGER NOT NULL,";
            TabPAIS += "PAIS	TEXT NOT NULL,";
            TabPAIS += "PRIMARY KEY(CODPAIS AUTOINCREMENT)";
            TabPAIS += ")";

            TabEMPRESA = "CREATE TABLE IF NOT EXISTS EMPRESA (";
            TabEMPRESA += "PENTALPHA	TEXT NOT NULL,";
            TabEMPRESA += "RUTID	TEXT NOT NULL,";
            TabEMPRESA += "DIGVER	TEXT NOT NULL,";
            TabEMPRESA += "NOMBRE	TEXT,";
            TabEMPRESA += "USUARIO	TEXT,";
            TabEMPRESA += "CLAVE	TEXT,";
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
            TabEMPRESA += "TELEFONO1,";
            TabEMPRESA += "TELEFONO2,";
            TabEMPRESA += "TELEFONO3,";
            TabEMPRESA += "OBSERVACIONES	TEXT,";
            TabEMPRESA += "LOGO	BLOB,";
            TabEMPRESA += "PRIMARY KEY(PENTALPHA,RUTID,DIGVER)";
            TabEMPRESA += ")";

            TabCLIENTES = "CREATE TABLE IF NOT EXISTS CLIENTES (";
            TabCLIENTES += "PENTALPHA	TEXT NOT NULL,";
            TabCLIENTES += "RUTID	TEXT NOT NULL,";
            TabCLIENTES += "DIGVER	TEXT NOT NULL,";
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
            TabCLIENTES += "PRIMARY KEY(PENTALPHA,RUTID,DIGVER)";
            TabCLIENTES += ")";

            TabPERSONAL = "CREATE TABLE IF NOT EXISTS PERSONAL (";
            TabPERSONAL += "PENTALPHA	TEXT NOT NULL,";
            TabPERSONAL += "RUTID	TEXT NOT NULL,";
            TabPERSONAL += "DIGVER	TEXT NOT NULL,";
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
            TabPERSONAL += "PRIMARY KEY(PENTALPHA,RUTID,DIGVER)";
            TabPERSONAL += ")";

            TabRECURSOS = "CREATE TABLE IF NOT EXISTS RECURSOS (";
            TabRECURSOS += "PENTALPHA	TEXT NOT NULL,";
            TabRECURSOS += "RUTID	TEXT NOT NULL,";
            TabRECURSOS += "DIGVER	TEXT NOT NULL,";
            TabRECURSOS += "PROPIETARIO	TEXT,";
            TabRECURSOS += "TIPO	TEXT,";
            TabRECURSOS += "PATENTE	TEXT NOT NULL,";
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
            TabRECURSOS += "PRIMARY KEY(PENTALPHA,PATENTE)";
            TabRECURSOS += ")";

            TabSERVICIOS = "CREATE TABLE IF NOT EXISTS SERVICIOS (";
            TabSERVICIOS += "PENTALPHA	TEXT NOT NULL,";
            TabSERVICIOS += "NROENVIO	TEXT NOT NULL,";
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
            TabSERVICIOS += "DISTANCIA_KM  REAL,";
            TabSERVICIOS += "DESCRIPCION	TEXT,";
            TabSERVICIOS += "FACTURAS  INTEGER,";
            TabSERVICIOS += "BULTOS    INTEGER,";
            TabSERVICIOS += "COMPRAS   INTEGER,";
            TabSERVICIOS += "CHEQUES   INTEGER,";
            TabSERVICIOS += "SOBRES    INTEGER,";
            TabSERVICIOS += "OTROS INTEGER,";
            TabSERVICIOS += "OBSERVACIONES	TEXT,";
            TabSERVICIOS += "ENTREGA	TEXT,";
            TabSERVICIOS += "RECEPCION	TEXT,";
            TabSERVICIOS += "TESPERA	TEXT,";
            TabSERVICIOS += "FECHAENTREGA	TEXT,";
            TabSERVICIOS += "HORAENTREGA	TEXT,";
            TabSERVICIOS += "DISTANCIA	TEXT,";
            TabSERVICIOS += "PROGRAMADO	TEXT,";
            TabSERVICIOS += "PRIMARY KEY(PENTALPHA,NROENVIO)";
            TabSERVICIOS += ")";

            IndRECURSOS_IDX1 = "CREATE UNIQUE INDEX IF NOT EXISTS RECURSOS_IDX1 ON RECURSOS (";
            IndRECURSOS_IDX1 += "RUTID,";
            IndRECURSOS_IDX1 += "DIGVER,";
            IndRECURSOS_IDX1 += "PATENTE";
            IndRECURSOS_IDX1 += ")";

            // ********************************************************
            // Operaciones con Triggers Empresas
            // ********************************************************
            EmpresaTriggerPaisInsert = "CREATE TRIGGER IF NOT EXISTS EMP_INS_PAIS ";
            EmpresaTriggerPaisInsert += "AFTER INSERT ";
            EmpresaTriggerPaisInsert += "ON EMPRESA ";
            EmpresaTriggerPaisInsert += "WHEN NOT EXISTS(SELECT PAIS FROM PAIS WHERE PAIS = NEW.PAIS) AND NEW.PAIS <> '' ";
            EmpresaTriggerPaisInsert += "BEGIN ";
            EmpresaTriggerPaisInsert += "INSERT INTO PAIS(PAIS) VALUES(NEW.PAIS); ";
            EmpresaTriggerPaisInsert += "END ";

            EmpresaTriggerPaisUpdate = "CREATE TRIGGER IF NOT EXISTS EMP_UPD_PAIS ";
            EmpresaTriggerPaisUpdate += "AFTER UPDATE ";
            EmpresaTriggerPaisUpdate += "ON EMPRESA ";
            EmpresaTriggerPaisUpdate += "WHEN NOT EXISTS(SELECT PAIS FROM PAIS WHERE PAIS = NEW.PAIS) AND NEW.PAIS <> '' ";
            EmpresaTriggerPaisUpdate += "BEGIN ";
            EmpresaTriggerPaisUpdate += "INSERT INTO PAIS(PAIS) VALUES(NEW.PAIS); ";
            EmpresaTriggerPaisUpdate += "END ";

            EmpresaTriggerRegionInsert = "CREATE TRIGGER IF NOT EXISTS EMP_INS_REGION ";
            EmpresaTriggerRegionInsert += "AFTER INSERT ";
            EmpresaTriggerRegionInsert += "ON EMPRESA ";
            EmpresaTriggerRegionInsert += "WHEN NOT EXISTS(SELECT REGION FROM ESTADOREGION WHERE REGION = NEW.ESTADOREGION) AND NEW.ESTADOREGION <> '' ";
            EmpresaTriggerRegionInsert += "BEGIN ";
            EmpresaTriggerRegionInsert += "INSERT INTO ESTADOREGION(REGION) VALUES(NEW.ESTADOREGION); ";
            EmpresaTriggerRegionInsert += "END ";

            EmpresaTriggerRegionUpdate = "CREATE TRIGGER IF NOT EXISTS EMP_UPD_REGION ";
            EmpresaTriggerRegionUpdate += "AFTER UPDATE ";
            EmpresaTriggerRegionUpdate += "ON EMPRESA ";
            EmpresaTriggerRegionUpdate += "WHEN NOT EXISTS(SELECT REGION FROM ESTADOREGION WHERE REGION = NEW.ESTADOREGION) AND NEW.ESTADOREGION <> '' ";
            EmpresaTriggerRegionUpdate += "BEGIN ";
            EmpresaTriggerRegionUpdate += "INSERT INTO ESTADOREGION(REGION) VALUES(NEW.ESTADOREGION); ";
            EmpresaTriggerRegionUpdate += "END ";

            EmpresaTriggerComunaInsert = "CREATE TRIGGER IF NOT EXISTS EMP_INS_COMUNA ";
            EmpresaTriggerComunaInsert += "AFTER INSERT ";
            EmpresaTriggerComunaInsert += "ON EMPRESA ";
            EmpresaTriggerComunaInsert += "WHEN NOT EXISTS(SELECT COMUNA FROM COMUNA WHERE COMUNA = NEW.COMUNA) AND NEW.COMUNA <> '' ";
            EmpresaTriggerComunaInsert += "BEGIN ";
            EmpresaTriggerComunaInsert += "INSERT INTO COMUNA(COMUNA) VALUES(NEW.COMUNA); ";
            EmpresaTriggerComunaInsert += "END ";

            EmpresaTriggerComunaUpdate = "CREATE TRIGGER IF NOT EXISTS EMP_UPD_COMUNA ";
            EmpresaTriggerComunaUpdate += "AFTER UPDATE ";
            EmpresaTriggerComunaUpdate += "ON EMPRESA ";
            EmpresaTriggerComunaUpdate += "WHEN NOT EXISTS(SELECT COMUNA FROM COMUNA WHERE COMUNA = NEW.COMUNA) AND NEW.COMUNA <> '' ";
            EmpresaTriggerComunaUpdate += "BEGIN ";
            EmpresaTriggerComunaUpdate += "INSERT INTO COMUNA(COMUNA) VALUES(NEW.COMUNA); ";
            EmpresaTriggerComunaUpdate += "END ";

            EmpresaTriggerCiudadInsert = "CREATE TRIGGER IF NOT EXISTS EMP_INS_CIUDAD ";
            EmpresaTriggerCiudadInsert += "AFTER INSERT ";
            EmpresaTriggerCiudadInsert += "ON EMPRESA ";
            EmpresaTriggerCiudadInsert += "WHEN NOT EXISTS(SELECT CIUDAD FROM CIUDAD WHERE CIUDAD = NEW.CIUDAD) AND NEW.CIUDAD <> '' ";
            EmpresaTriggerCiudadInsert += "BEGIN ";
            EmpresaTriggerCiudadInsert += "INSERT INTO CIUDAD(CIUDAD) VALUES(NEW.CIUDAD); ";
            EmpresaTriggerCiudadInsert += "END ";

            EmpresaTriggerCiudadUpdate = "CREATE TRIGGER IF NOT EXISTS EMP_UPD_CIUDAD ";
            EmpresaTriggerCiudadUpdate += "AFTER UPDATE ";
            EmpresaTriggerCiudadUpdate += "ON EMPRESA ";
            EmpresaTriggerCiudadUpdate += "WHEN NOT EXISTS(SELECT CIUDAD FROM CIUDAD WHERE CIUDAD = NEW.CIUDAD) AND NEW.CIUDAD <> '' ";
            EmpresaTriggerCiudadUpdate += "BEGIN ";
            EmpresaTriggerCiudadUpdate += "INSERT INTO CIUDAD(CIUDAD) VALUES(NEW.CIUDAD); ";
            EmpresaTriggerCiudadUpdate += "END ";

            // ********************************************************
            // Operaciones con Triggers Personal
            // ********************************************************
            PersonalTriggerPaisInsert = "CREATE TRIGGER IF NOT EXISTS PER_INS_PAIS ";
            PersonalTriggerPaisInsert += "AFTER INSERT ";
            PersonalTriggerPaisInsert += "ON PERSONAL ";
            PersonalTriggerPaisInsert += "WHEN NOT EXISTS(SELECT PAIS FROM PAIS WHERE PAIS = NEW.PAIS) AND NEW.PAIS <> '' ";
            PersonalTriggerPaisInsert += "BEGIN ";
            PersonalTriggerPaisInsert += "INSERT INTO PAIS(PAIS) VALUES(NEW.PAIS); ";
            PersonalTriggerPaisInsert += "END ";

            PersonalTriggerPaisUpdate = "CREATE TRIGGER IF NOT EXISTS PER_UPD_PAIS ";
            PersonalTriggerPaisUpdate += "AFTER UPDATE ";
            PersonalTriggerPaisUpdate += "ON PERSONAL ";
            PersonalTriggerPaisUpdate += "WHEN NOT EXISTS(SELECT PAIS FROM PAIS WHERE PAIS = NEW.PAIS) AND NEW.PAIS <> '' ";
            PersonalTriggerPaisUpdate += "BEGIN ";
            PersonalTriggerPaisUpdate += "INSERT INTO PAIS(PAIS) VALUES(NEW.PAIS); ";
            PersonalTriggerPaisUpdate += "END ";

            PersonalTriggerRegionInsert = "CREATE TRIGGER IF NOT EXISTS PER_INS_REGION ";
            PersonalTriggerRegionInsert += "AFTER INSERT ";
            PersonalTriggerRegionInsert += "ON PERSONAL ";
            PersonalTriggerRegionInsert += "WHEN NOT EXISTS(SELECT REGION FROM ESTADOREGION WHERE REGION = NEW.REGION) AND NEW.REGION <> '' ";
            PersonalTriggerRegionInsert += "BEGIN ";
            PersonalTriggerRegionInsert += "INSERT INTO ESTADOREGION(REGION) VALUES(NEW.REGION); ";
            PersonalTriggerRegionInsert += "END ";

            PersonalTriggerRegionUpdate = "CREATE TRIGGER IF NOT EXISTS PER_UPD_REGION ";
            PersonalTriggerRegionUpdate += "AFTER UPDATE ";
            PersonalTriggerRegionUpdate += "ON PERSONAL ";
            PersonalTriggerRegionUpdate += "WHEN NOT EXISTS(SELECT REGION FROM ESTADOREGION WHERE REGION = NEW.REGION) AND NEW.REGION <> '' ";
            PersonalTriggerRegionUpdate += "BEGIN ";
            PersonalTriggerRegionUpdate += "INSERT INTO ESTADOREGION(REGION) VALUES(NEW.REGION); ";
            PersonalTriggerRegionUpdate += "END ";

            PersonalTriggerComunaInsert = "CREATE TRIGGER IF NOT EXISTS PER_INS_COMUNA ";
            PersonalTriggerComunaInsert += "AFTER INSERT ";
            PersonalTriggerComunaInsert += "ON PERSONAL ";
            PersonalTriggerComunaInsert += "WHEN NOT EXISTS(SELECT COMUNA FROM COMUNA WHERE COMUNA = NEW.COMUNA) AND NEW.COMUNA <> '' ";
            PersonalTriggerComunaInsert += "BEGIN ";
            PersonalTriggerComunaInsert += "INSERT INTO COMUNA(COMUNA) VALUES(NEW.COMUNA); ";
            PersonalTriggerComunaInsert += "END ";

            PersonalTriggerComunaUpdate = "CREATE TRIGGER IF NOT EXISTS PER_UPD_COMUNA ";
            PersonalTriggerComunaUpdate += "AFTER UPDATE ";
            PersonalTriggerComunaUpdate += "ON PERSONAL ";
            PersonalTriggerComunaUpdate += "WHEN NOT EXISTS(SELECT COMUNA FROM COMUNA WHERE COMUNA = NEW.COMUNA) AND NEW.COMUNA <> '' ";
            PersonalTriggerComunaUpdate += "BEGIN ";
            PersonalTriggerComunaUpdate += "INSERT INTO COMUNA(COMUNA) VALUES(NEW.COMUNA); ";
            PersonalTriggerComunaUpdate += "END ";

            PersonalTriggerCiudadInsert = "CREATE TRIGGER IF NOT EXISTS PER_INS_CIUDAD ";
            PersonalTriggerCiudadInsert += "AFTER INSERT ";
            PersonalTriggerCiudadInsert += "ON PERSONAL ";
            PersonalTriggerCiudadInsert += "WHEN NOT EXISTS(SELECT CIUDAD FROM CIUDAD WHERE CIUDAD = NEW.CIUDAD) AND NEW.CIUDAD <> '' ";
            PersonalTriggerCiudadInsert += "BEGIN ";
            PersonalTriggerCiudadInsert += "INSERT INTO CIUDAD(CIUDAD) VALUES(NEW.CIUDAD); ";
            PersonalTriggerCiudadInsert += "END ";

            PersonalTriggerCiudadUpdate = "CREATE TRIGGER IF NOT EXISTS PER_UPD_CIUDAD ";
            PersonalTriggerCiudadUpdate += "AFTER UPDATE ";
            PersonalTriggerCiudadUpdate += "ON PERSONAL ";
            PersonalTriggerCiudadUpdate += "WHEN NOT EXISTS(SELECT CIUDAD FROM CIUDAD WHERE CIUDAD = NEW.CIUDAD) AND NEW.CIUDAD <> '' ";
            PersonalTriggerCiudadUpdate += "BEGIN ";
            PersonalTriggerCiudadUpdate += "INSERT INTO CIUDAD(CIUDAD) VALUES(NEW.CIUDAD); ";
            PersonalTriggerCiudadUpdate += "END ";

            // ********************************************************
            // Operaciones con Triggers Recursos
            // ********************************************************
            RecursosTriggerPaisInsert = "CREATE TRIGGER IF NOT EXISTS REC_INS_PAIS ";
            RecursosTriggerPaisInsert += "AFTER INSERT ";
            RecursosTriggerPaisInsert += "ON RECURSOS ";
            RecursosTriggerPaisInsert += "WHEN NOT EXISTS(SELECT PAIS FROM PAIS WHERE PAIS = NEW.PAIS) AND NEW.PAIS <> '' ";
            RecursosTriggerPaisInsert += "BEGIN ";
            RecursosTriggerPaisInsert += "INSERT INTO PAIS(PAIS) VALUES(NEW.PAIS); ";
            RecursosTriggerPaisInsert += "END ";

            RecursosTriggerPaisUpdate = "CREATE TRIGGER IF NOT EXISTS REC_UPD_PAIS ";
            RecursosTriggerPaisUpdate += "AFTER UPDATE ";
            RecursosTriggerPaisUpdate += "ON RECURSOS ";
            RecursosTriggerPaisUpdate += "WHEN NOT EXISTS(SELECT PAIS FROM PAIS WHERE PAIS = NEW.PAIS) AND NEW.PAIS <> '' ";
            RecursosTriggerPaisUpdate += "BEGIN ";
            RecursosTriggerPaisUpdate += "INSERT INTO PAIS(PAIS) VALUES(NEW.PAIS); ";
            RecursosTriggerPaisUpdate += "END ";

            RecursosTriggerRegionInsert = "CREATE TRIGGER IF NOT EXISTS REC_INS_REGION ";
            RecursosTriggerRegionInsert += "AFTER INSERT ";
            RecursosTriggerRegionInsert += "ON RECURSOS ";
            RecursosTriggerRegionInsert += "WHEN NOT EXISTS(SELECT REGION FROM ESTADOREGION WHERE REGION = NEW.REGION) AND NEW.REGION <> '' ";
            RecursosTriggerRegionInsert += "BEGIN ";
            RecursosTriggerRegionInsert += "INSERT INTO ESTADOREGION(REGION) VALUES(NEW.REGION); ";
            RecursosTriggerRegionInsert += "END ";

            RecursosTriggerRegionUpdate = "CREATE TRIGGER IF NOT EXISTS REC_UPD_REGION ";
            RecursosTriggerRegionUpdate += "AFTER UPDATE ";
            RecursosTriggerRegionUpdate += "ON RECURSOS ";
            RecursosTriggerRegionUpdate += "WHEN NOT EXISTS(SELECT REGION FROM ESTADOREGION WHERE REGION = NEW.REGION) AND NEW.REGION <> '' ";
            RecursosTriggerRegionUpdate += "BEGIN ";
            RecursosTriggerRegionUpdate += "INSERT INTO ESTADOREGION(REGION) VALUES(NEW.REGION); ";
            RecursosTriggerRegionUpdate += "END ";

            RecursosTriggerComunaInsert = "CREATE TRIGGER IF NOT EXISTS REC_INS_COMUNA ";
            RecursosTriggerComunaInsert += "AFTER INSERT ";
            RecursosTriggerComunaInsert += "ON RECURSOS ";
            RecursosTriggerComunaInsert += "WHEN NOT EXISTS(SELECT COMUNA FROM COMUNA WHERE COMUNA = NEW.COMUNA) AND NEW.COMUNA <> '' ";
            RecursosTriggerComunaInsert += "BEGIN ";
            RecursosTriggerComunaInsert += "INSERT INTO COMUNA(COMUNA) VALUES(NEW.COMUNA); ";
            RecursosTriggerComunaInsert += "END ";

            RecursosTriggerComunaUpdate = "CREATE TRIGGER IF NOT EXISTS REC_UPD_COMUNA ";
            RecursosTriggerComunaUpdate += "AFTER UPDATE ";
            RecursosTriggerComunaUpdate += "ON RECURSOS ";
            RecursosTriggerComunaUpdate += "WHEN NOT EXISTS(SELECT COMUNA FROM COMUNA WHERE COMUNA = NEW.COMUNA) AND NEW.COMUNA <> '' ";
            RecursosTriggerComunaUpdate += "BEGIN ";
            RecursosTriggerComunaUpdate += "INSERT INTO COMUNA(COMUNA) VALUES(NEW.COMUNA); ";
            RecursosTriggerComunaUpdate += "END ";

            RecursosTriggerCiudadInsert = "CREATE TRIGGER IF NOT EXISTS REC_INS_CIUDAD ";
            RecursosTriggerCiudadInsert += "AFTER INSERT ";
            RecursosTriggerCiudadInsert += "ON RECURSOS ";
            RecursosTriggerCiudadInsert += "WHEN NOT EXISTS(SELECT CIUDAD FROM CIUDAD WHERE CIUDAD = NEW.CIUDAD) AND NEW.CIUDAD <> '' ";
            RecursosTriggerCiudadInsert += "BEGIN ";
            RecursosTriggerCiudadInsert += "INSERT INTO CIUDAD(CIUDAD) VALUES(NEW.CIUDAD); ";
            RecursosTriggerCiudadInsert += "END ";

            RecursosTriggerCiudadUpdate = "CREATE TRIGGER IF NOT EXISTS REC_UPD_CIUDAD ";
            RecursosTriggerCiudadUpdate += "AFTER UPDATE ";
            RecursosTriggerCiudadUpdate += "ON RECURSOS ";
            RecursosTriggerCiudadUpdate += "WHEN NOT EXISTS(SELECT CIUDAD FROM CIUDAD WHERE CIUDAD = NEW.CIUDAD) AND NEW.CIUDAD <> '' ";
            RecursosTriggerCiudadUpdate += "BEGIN ";
            RecursosTriggerCiudadUpdate += "INSERT INTO CIUDAD(CIUDAD) VALUES(NEW.CIUDAD); ";
            RecursosTriggerCiudadUpdate += "END ";

            // ********************************************************
            // Operaciones con Triggers Clientes
            // ********************************************************
            ClientesTriggerPaisInsert = "CREATE TRIGGER IF NOT EXISTS CLI_INS_PAIS ";
            ClientesTriggerPaisInsert += "AFTER INSERT ";
            ClientesTriggerPaisInsert += "ON CLIENTES ";
            ClientesTriggerPaisInsert += "WHEN NOT EXISTS(SELECT PAIS FROM PAIS WHERE PAIS = NEW.PAIS) AND NEW.PAIS <> '' ";
            ClientesTriggerPaisInsert += "BEGIN ";
            ClientesTriggerPaisInsert += "INSERT INTO PAIS(PAIS) VALUES(NEW.PAIS); ";
            ClientesTriggerPaisInsert += "END ";

            ClientesTriggerPaisUpdate = "CREATE TRIGGER IF NOT EXISTS CLI_UPD_PAIS ";
            ClientesTriggerPaisUpdate += "AFTER UPDATE ";
            ClientesTriggerPaisUpdate += "ON CLIENTES ";
            ClientesTriggerPaisUpdate += "WHEN NOT EXISTS(SELECT PAIS FROM PAIS WHERE PAIS = NEW.PAIS) AND NEW.PAIS <> '' ";
            ClientesTriggerPaisUpdate += "BEGIN ";
            ClientesTriggerPaisUpdate += "INSERT INTO PAIS(PAIS) VALUES(NEW.PAIS); ";
            ClientesTriggerPaisUpdate += "END ";

            ClientesTriggerRegionInsert = "CREATE TRIGGER IF NOT EXISTS CLI_INS_REGION ";
            ClientesTriggerRegionInsert += "AFTER INSERT ";
            ClientesTriggerRegionInsert += "ON CLIENTES ";
            ClientesTriggerRegionInsert += "WHEN NOT EXISTS(SELECT REGION FROM ESTADOREGION WHERE REGION = NEW.REGION) AND NEW.REGION <> '' ";
            ClientesTriggerRegionInsert += "BEGIN ";
            ClientesTriggerRegionInsert += "INSERT INTO ESTADOREGION(REGION) VALUES(NEW.REGION); ";
            ClientesTriggerRegionInsert += "END ";

            ClientesTriggerRegionUpdate = "CREATE TRIGGER IF NOT EXISTS CLI_UPD_REGION ";
            ClientesTriggerRegionUpdate += "AFTER UPDATE ";
            ClientesTriggerRegionUpdate += "ON CLIENTES ";
            ClientesTriggerRegionUpdate += "WHEN NOT EXISTS(SELECT REGION FROM ESTADOREGION WHERE REGION = NEW.REGION) AND NEW.REGION <> '' ";
            ClientesTriggerRegionUpdate += "BEGIN ";
            ClientesTriggerRegionUpdate += "INSERT INTO ESTADOREGION(REGION) VALUES(NEW.REGION); ";
            ClientesTriggerRegionUpdate += "END ";

            ClientesTriggerComunaInsert = "CREATE TRIGGER IF NOT EXISTS CLI_INS_COMUNA ";
            ClientesTriggerComunaInsert += "AFTER INSERT ";
            ClientesTriggerComunaInsert += "ON CLIENTES ";
            ClientesTriggerComunaInsert += "WHEN NOT EXISTS(SELECT COMUNA FROM COMUNA WHERE COMUNA = NEW.COMUNA) AND NEW.COMUNA <> '' ";
            ClientesTriggerComunaInsert += "BEGIN ";
            ClientesTriggerComunaInsert += "INSERT INTO COMUNA(COMUNA) VALUES(NEW.COMUNA); ";
            ClientesTriggerComunaInsert += "END ";

            ClientesTriggerComunaUpdate = "CREATE TRIGGER IF NOT EXISTS CLI_UPD_COMUNA ";
            ClientesTriggerComunaUpdate += "AFTER UPDATE ";
            ClientesTriggerComunaUpdate += "ON CLIENTES ";
            ClientesTriggerComunaUpdate += "WHEN NOT EXISTS(SELECT COMUNA FROM COMUNA WHERE COMUNA = NEW.COMUNA) AND NEW.COMUNA <> '' ";
            ClientesTriggerComunaUpdate += "BEGIN ";
            ClientesTriggerComunaUpdate += "INSERT INTO COMUNA(COMUNA) VALUES(NEW.COMUNA); ";
            ClientesTriggerComunaUpdate += "END ";

            ClientesTriggerCiudadInsert = "CREATE TRIGGER IF NOT EXISTS CLI_INS_CIUDAD ";
            ClientesTriggerCiudadInsert += "AFTER INSERT ";
            ClientesTriggerCiudadInsert += "ON CLIENTES ";
            ClientesTriggerCiudadInsert += "WHEN NOT EXISTS(SELECT CIUDAD FROM CIUDAD WHERE CIUDAD = NEW.CIUDAD) AND NEW.CIUDAD <> '' ";
            ClientesTriggerCiudadInsert += "BEGIN ";
            ClientesTriggerCiudadInsert += "INSERT INTO CIUDAD(CIUDAD) VALUES(NEW.CIUDAD); ";
            ClientesTriggerCiudadInsert += "END ";

            ClientesTriggerCiudadUpdate = "CREATE TRIGGER IF NOT EXISTS CLI_UPD_CIUDAD ";
            ClientesTriggerCiudadUpdate += "AFTER UPDATE ";
            ClientesTriggerCiudadUpdate += "ON CLIENTES ";
            ClientesTriggerCiudadUpdate += "WHEN NOT EXISTS(SELECT CIUDAD FROM CIUDAD WHERE CIUDAD = NEW.CIUDAD) AND NEW.CIUDAD <> '' ";
            ClientesTriggerCiudadUpdate += "BEGIN ";
            ClientesTriggerCiudadUpdate += "INSERT INTO CIUDAD(CIUDAD) VALUES(NEW.CIUDAD); ";
            ClientesTriggerCiudadUpdate += "END ";
        }

        private bool ProcTabPAIS()
        {
            SqliteCommand IDB_Command;
            try
            {
                IDB_Command = new SqliteCommand(TabPAIS, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        private bool ProcTabESTADOREGION()
        {
            SqliteCommand IDB_Command;
            try
            {
                IDB_Command = new SqliteCommand(TabESTADOREGION, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        private bool ProcTabCOMUNA()
        {
            SqliteCommand IDB_Command;
            try
            {
                IDB_Command = new SqliteCommand(TabCOMUNA, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        private bool ProcTabCIUDAD()
        {
            SqliteCommand IDB_Command;
            try
            {
                IDB_Command = new SqliteCommand(TabCIUDAD, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        private bool ProcTabEMPRESA()
        {
            SqliteCommand IDB_Command;
            try
            {
                IDB_Command = new SqliteCommand(TabEMPRESA, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        private bool ProcTabCLIENTES()
        {
            SqliteCommand IDB_Command;
            try
            {
                IDB_Command = new SqliteCommand(TabCLIENTES, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        private bool ProcTabPERSONAL()
        {
            SqliteCommand IDB_Command;
            try
            {
                IDB_Command = new SqliteCommand(TabPERSONAL, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        private bool ProcTabRECURSOS()
        {
            SqliteCommand IDB_Command;
            try
            {
                IDB_Command = new SqliteCommand(TabRECURSOS, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        private bool ProcTabSERVICIOS()
        {
            SqliteCommand IDB_Command;
            try
            {
                IDB_Command = new SqliteCommand(TabSERVICIOS, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        private bool ProcIndINDICES()
        {
            SqliteCommand IDB_Command;
            try
            {
                IDB_Command = new SqliteCommand(IndRECURSOS_IDX1, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        private bool ProcTRIGGERSEmpresa()
        {
            SqliteCommand IDB_Command;
            try
            {
                IDB_Command = new SqliteCommand(EmpresaTriggerPaisInsert, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                IDB_Command = new SqliteCommand(EmpresaTriggerPaisUpdate, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();

                IDB_Command = new SqliteCommand(EmpresaTriggerRegionInsert, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                IDB_Command = new SqliteCommand(EmpresaTriggerRegionUpdate, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();

                IDB_Command = new SqliteCommand(EmpresaTriggerComunaInsert, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                IDB_Command = new SqliteCommand(EmpresaTriggerComunaUpdate, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();

                IDB_Command = new SqliteCommand(EmpresaTriggerCiudadInsert, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                IDB_Command = new SqliteCommand(EmpresaTriggerCiudadUpdate, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();

                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        private bool ProcTRIGGERSPersonal()
        {
            SqliteCommand IDB_Command;
            try
            {
                IDB_Command = new SqliteCommand(PersonalTriggerPaisInsert, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                IDB_Command = new SqliteCommand(PersonalTriggerPaisUpdate, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();

                IDB_Command = new SqliteCommand(PersonalTriggerRegionInsert, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                IDB_Command = new SqliteCommand(PersonalTriggerRegionUpdate, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();

                IDB_Command = new SqliteCommand(PersonalTriggerComunaInsert, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                IDB_Command = new SqliteCommand(PersonalTriggerComunaUpdate, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();

                IDB_Command = new SqliteCommand(PersonalTriggerCiudadInsert, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                IDB_Command = new SqliteCommand(PersonalTriggerCiudadUpdate, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();

                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        private bool ProcTRIGGERSRecursos()
        {
            SqliteCommand IDB_Command;
            try
            {
                IDB_Command = new SqliteCommand(RecursosTriggerPaisInsert, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                IDB_Command = new SqliteCommand(RecursosTriggerPaisUpdate, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();

                IDB_Command = new SqliteCommand(RecursosTriggerRegionInsert, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                IDB_Command = new SqliteCommand(RecursosTriggerRegionUpdate, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();

                IDB_Command = new SqliteCommand(RecursosTriggerComunaInsert, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                IDB_Command = new SqliteCommand(RecursosTriggerComunaUpdate, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();

                IDB_Command = new SqliteCommand(RecursosTriggerCiudadInsert, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                IDB_Command = new SqliteCommand(RecursosTriggerCiudadUpdate, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();

                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        private bool ProcTRIGGERSClientes()
        {
            SqliteCommand IDB_Command;
            try
            {
                IDB_Command = new SqliteCommand(ClientesTriggerPaisInsert, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                IDB_Command = new SqliteCommand(ClientesTriggerPaisUpdate, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();

                IDB_Command = new SqliteCommand(ClientesTriggerRegionInsert, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                IDB_Command = new SqliteCommand(ClientesTriggerRegionUpdate, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();

                IDB_Command = new SqliteCommand(ClientesTriggerComunaInsert, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                IDB_Command = new SqliteCommand(ClientesTriggerComunaUpdate, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();

                IDB_Command = new SqliteCommand(ClientesTriggerCiudadInsert, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();
                IDB_Command = new SqliteCommand(ClientesTriggerCiudadUpdate, IDB_Connection);
                _ = IDB_Command.ExecuteNonQuery();

                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

    }
}
