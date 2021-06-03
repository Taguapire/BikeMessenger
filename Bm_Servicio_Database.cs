using System;
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace BikeMessenger
{
    class Bm_Servicio_Database
    {
        // public SQLiteFactory BM_DB;
        public SqliteConnection BM_Connection;
        private SqliteTransaction BK_Transaccion_Servicio;
        private SqliteCommand BK_Cmd_Servicios;
        private SqliteDataReader BK_Reader_Servicios;

        // Envios
        private SqliteCommand BK_Cmd_Envios_Grid;
        private SqliteDataReader BK_Reader_Servicios_Envios;
        readonly string StrBuscar_Servicios_Envios = "SELECT NROENVIO, FECHA, CLIENTERUT, CLIENTEDIGVER, ENTREGA FROM SERVICIOS ORDER BY NROENVIO ASC";

        // Clientes
        private SqliteCommand BK_Cmd_Clientes_Grid;
        private SqliteDataReader BK_Reader_Servicios_Clientes;
        readonly string StrBuscar_Servicios_Clientes = "SELECT RUTID||'-'||DIGVER, NOMBRE FROM CLIENTES ORDER BY NOMBRE ASC";

        // Mensajeros
        private SqliteCommand BK_Cmd_Mensajeros_Grid;
        private SqliteDataReader BK_Reader_Servicios_Mensajeros;
        readonly string StrBuscar_Servicios_Mensajeros = "SELECT RUTID||'-'||DIGVER, APELLIDOS||', '||NOMBRES FROM PERSONAL ORDER BY APELLIDOS ASC";

        // Mensajeros
        private SqliteCommand BK_Cmd_Recursos_Grid;
        private SqliteDataReader BK_Reader_Servicios_Recursos;
        readonly string StrBuscar_Servicios_Recursos = "SELECT PATENTE,TIPO,MARCA,MODELO,PROPIETARIO FROM RECURSOS ORDER BY TIPO ASC,MARCA ASC,MODELO ASC";

        private SqliteCommand BK_Cmd_Servicios_Pais;
        private SqliteDataReader BK_Reader_Servicios_Pais;

        private SqliteCommand BK_Cmd_Servicios_Region;
        private SqliteDataReader BK_Reader_Servicios_Region;

        private SqliteCommand BK_Cmd_Servicios_Comuna;
        private SqliteDataReader BK_Reader_Servicios_Comuna;

        private SqliteCommand BK_Cmd_Servicios_Ciudad;
        private SqliteDataReader BK_Reader_Servicios_Ciudad;

        private string StrAgregar_Servicios;
        private string StrModificar_Servicios;
        private string StrBorrar_Servicios;

        private readonly string StrBuscar_Servicios = "SELECT * FROM SERVICIOS";

        private readonly string StrBuscar_Servicios_Pais = "SELECT * FROM PAIS ORDER BY PAIS ASC";
        private readonly string StrBuscar_Servicios_Region = "SELECT * FROM ESTADOREGION ORDER BY REGION ASC";
        private readonly string StrBuscar_Servicios_Comuna = "SELECT * FROM COMUNA ORDER BY COMUNA ASC";
        private readonly string StrBuscar_Servicios_Ciudad = "SELECT * FROM CIUDAD ORDER BY CIUDAD ASC";

        // Campos Servicios
        public string BK_PENTALPHA { get; set; }
        public string BK_NROENVIO { get; set; }
        public string BK_GUIADESPACHO { get; set; }
        public string BK_FECHA { get; set; }
        public string BK_HORA { get; set; }
        public string BK_CLIENTERUT { get; set; }
        public string BK_CLIENTEDIGVER { get; set; }
        public string BK_CLIENTE { get; set; }
        public string BK_MENSAJERORUT { get; set; }
        public string BK_MENSAJERODIGVER { get; set; }
        public string BK_MENSAJERO { get; set; }
        public string BK_RECURSOID { get; set; }
        public string BK_RECURSO { get; set; }
        public string BK_ODOMICILIO1 { get; set; }
        public string BK_ODOMICILIO2 { get; set; }
        public string BK_ONUMERO { get; set; }
        public string BK_OPISO { get; set; }
        public string BK_OOFICINA { get; set; }
        public string BK_OCIUDAD { get; set; }
        public string BK_OCOMUNA { get; set; }
        public string BK_OESTADO { get; set; }
        public string BK_OPAIS { get; set; }
        public string BK_OCOORDENADAS { get; set; }
        public string BK_DDOMICILIO1 { get; set; }
        public string BK_DDOMICILIO2 { get; set; }
        public string BK_DNUMERO { get; set; }
        public string BK_DPISO { get; set; }
        public string BK_DOFICINA { get; set; }
        public string BK_DCIUDAD { get; set; }
        public string BK_DCOMUNA { get; set; }
        public string BK_DESTADO { get; set; }
        public string BK_DPAIS { get; set; }
        public string BK_DCOORDENADAS { get; set; }
        public double BK_DISTANCIA_KM { get; set; }
        public string BK_DESCRIPCION { get; set; }
        public int BK_FACTURAS { get; set; }
        public int BK_BULTOS { get; set; }
        public int BK_COMPRAS { get; set; }
        public int BK_CHEQUES { get; set; }
        public int BK_SOBRES { get; set; }
        public int BK_OTROS { get; set; }
        public string BK_OBSERVACIONES { get; set; }
        public string BK_ENTREGA { get; set; }
        public string BK_RECEPCION { get; set; }
        public string BK_TESPERA { get; set; }
        public string BK_FECHAENTREGA { get; set; }
        public string BK_HORAENTREGA { get; set; }
        public string BK_DISTANCIA { get; set; }
        public string BK_PROGRAMADO { get; set; }

        // Campos Envios Grid
        public string BK_GRID_ENVIO_NRO { get; set; }
        public string BK_GRID_ENVIO_FECHA { get; set; }
        public string BK_GRID_ENVIO_CLIENTE { get; set; }
        public string BK_GRID_ENVIO_ENTREGA { get; set; }

        // Campos Clientes Grid
        public string BK_GRID_CLIENTE_RUTID { get; set; }
        public string BK_GRID_CLIENTE_NOMBRE { get; set; }

        // Campos Mensajeros Grid
        public string BK_GRID_MENSAJERO_RUTID { get; set; }
        public string BK_GRID_MENSAJERO_NOMBRE { get; set; }

        // Campos Mensajeros 
        public string BK_GRID_RECURSO_PATENTE { get; set; }
        public string BK_GRID_RECURSO_TIPO { get; set; }
        public string BK_GRID_RECURSO_MARCA { get; set; }
        public string BK_GRID_RECURSO_MODELO { get; set; }
        public string BK_GRID_RECURSO_PROPIETARIO { get; set; }

        // Campos de PAIS
        public short BK_E_CODPAIS { get; set; }
        public string BK_E_PAIS { get; set; }

        // Campos de REGION
        public short BK_E_CODREGION { get; set; }
        public string BK_E_REGION { get; set; }

        // Campos de COMUNA
        public short BK_E_CODCOMUNA { get; set; }
        public string BK_E_COMUNA { get; set; }

        // Campos de CIUDAD
        public short BK_E_CODCIUDAD { get; set; }
        public string BK_E_CIUDAD { get; set; }

        public bool BM_CreateDatabase(SqliteConnection BM_Connection)
        {
            this.BM_Connection = BM_Connection;
            return true;
        }

        public bool Bm_Servicios_Agregar()
        {
            StrAgregar_Servicios = "INSERT INTO SERVICIOS (";
            StrAgregar_Servicios += "PENTALPHA,";
            StrAgregar_Servicios += "NROENVIO,";
            StrAgregar_Servicios += "GUIADESPACHO,";
            StrAgregar_Servicios += "FECHA,";
            StrAgregar_Servicios += "HORA,";
            StrAgregar_Servicios += "CLIENTERUT,";
            StrAgregar_Servicios += "CLIENTEDIGVER,";
            StrAgregar_Servicios += "MENSAJERORUT,";
            StrAgregar_Servicios += "MENSAJERODIGVER,";
            StrAgregar_Servicios += "RECURSOID,";
            StrAgregar_Servicios += "ODOMICILIO1,";
            StrAgregar_Servicios += "ODOMICILIO2,";
            StrAgregar_Servicios += "ONUMERO,";
            StrAgregar_Servicios += "OPISO,";
            StrAgregar_Servicios += "OOFICINA,";
            StrAgregar_Servicios += "OCIUDAD,";
            StrAgregar_Servicios += "OCOMUNA,";
            StrAgregar_Servicios += "OESTADO,";
            StrAgregar_Servicios += "OPAIS,";
            StrAgregar_Servicios += "OCOORDENADAS,";
            StrAgregar_Servicios += "DDOMICILIO1,";
            StrAgregar_Servicios += "DDOMICILIO2,";
            StrAgregar_Servicios += "DNUMERO,";
            StrAgregar_Servicios += "DPISO,";
            StrAgregar_Servicios += "DOFICINA,";
            StrAgregar_Servicios += "DCIUDAD,";
            StrAgregar_Servicios += "DCOMUNA,";
            StrAgregar_Servicios += "DESTADO,";
            StrAgregar_Servicios += "DPAIS,";
            StrAgregar_Servicios += "DCOORDENADAS,";
            StrAgregar_Servicios += "DISTANCIA_KM,";
            StrAgregar_Servicios += "DESCRIPCION,";
            StrAgregar_Servicios += "FACTURAS,";
            StrAgregar_Servicios += "BULTOS,";
            StrAgregar_Servicios += "COMPRAS,";
            StrAgregar_Servicios += "CHEQUES,";
            StrAgregar_Servicios += "SOBRES,";
            StrAgregar_Servicios += "OTROS,";
            StrAgregar_Servicios += "OBSERVACIONES,";
            StrAgregar_Servicios += "ENTREGA,";
            StrAgregar_Servicios += "RECEPCION,";
            StrAgregar_Servicios += "TESPERA,";
            StrAgregar_Servicios += "FECHAENTREGA,";
            StrAgregar_Servicios += "HORAENTREGA,";
            StrAgregar_Servicios += "DISTANCIA,";
            StrAgregar_Servicios += "PROGRAMADO) VALUES (";
            StrAgregar_Servicios += "'" + BK_PENTALPHA + "',";
            StrAgregar_Servicios += "'" + BK_NROENVIO + "',";
            StrAgregar_Servicios += "'" + BK_GUIADESPACHO + "',";
            StrAgregar_Servicios += "'" + BK_FECHA + "',";
            StrAgregar_Servicios += "'" + BK_HORA + "',";
            StrAgregar_Servicios += "'" + BK_CLIENTERUT + "',";
            StrAgregar_Servicios += "'" + BK_CLIENTEDIGVER + "',";
            StrAgregar_Servicios += "'" + BK_MENSAJERORUT + "',";
            StrAgregar_Servicios += "'" + BK_MENSAJERODIGVER + "',";
            StrAgregar_Servicios += "'" + BK_RECURSOID + "',";
            StrAgregar_Servicios += "'" + BK_ODOMICILIO1 + "',";
            StrAgregar_Servicios += "'" + BK_ODOMICILIO2 + "',";
            StrAgregar_Servicios += "'" + BK_ONUMERO + "',";
            StrAgregar_Servicios += "'" + BK_OPISO + "',";
            StrAgregar_Servicios += "'" + BK_OOFICINA + "',";
            StrAgregar_Servicios += "'" + BK_OCIUDAD + "',";
            StrAgregar_Servicios += "'" + BK_OCOMUNA + "',";
            StrAgregar_Servicios += "'" + BK_OESTADO + "',";
            StrAgregar_Servicios += "'" + BK_OPAIS + "',";
            StrAgregar_Servicios += "'" + BK_OCOORDENADAS + "',";
            StrAgregar_Servicios += "'" + BK_DDOMICILIO1 + "',";
            StrAgregar_Servicios += "'" + BK_DDOMICILIO2 + "',";
            StrAgregar_Servicios += "'" + BK_DNUMERO + "',";
            StrAgregar_Servicios += "'" + BK_DPISO + "',";
            StrAgregar_Servicios += "'" + BK_DOFICINA + "',";
            StrAgregar_Servicios += "'" + BK_DCIUDAD + "',";
            StrAgregar_Servicios += "'" + BK_DCOMUNA + "',";
            StrAgregar_Servicios += "'" + BK_DESTADO + "',";
            StrAgregar_Servicios += "'" + BK_DPAIS + "',";
            StrAgregar_Servicios += "'" + BK_DCOORDENADAS + "',";
            StrAgregar_Servicios += " " + BK_DISTANCIA_KM.ToString("G", CultureInfo.InvariantCulture) + " ,";
            StrAgregar_Servicios += "'" + BK_DESCRIPCION + "',";
            StrAgregar_Servicios += " " + BK_FACTURAS.ToString() + " ,";
            StrAgregar_Servicios += " " + BK_BULTOS.ToString() + " ,";
            StrAgregar_Servicios += " " + BK_COMPRAS.ToString() + " ,";
            StrAgregar_Servicios += " " + BK_CHEQUES.ToString() + " ,";
            StrAgregar_Servicios += " " + BK_SOBRES.ToString() + " ,";
            StrAgregar_Servicios += " " + BK_OTROS.ToString() + " ,";
            StrAgregar_Servicios += "'" + BK_OBSERVACIONES + "',";
            StrAgregar_Servicios += "'" + BK_ENTREGA + "',";
            StrAgregar_Servicios += "'" + BK_RECEPCION + "',";
            StrAgregar_Servicios += "'" + BK_TESPERA + "',";
            StrAgregar_Servicios += "'" + BK_FECHAENTREGA + "',";
            StrAgregar_Servicios += "'" + BK_HORAENTREGA + "',";
            StrAgregar_Servicios += "'" + BK_DISTANCIA + "',";
            StrAgregar_Servicios += "'" + BK_PROGRAMADO + "')";
            try
            {
                BK_Cmd_Servicios = new SqliteCommand(StrAgregar_Servicios, BM_Connection);
                _ = BK_Cmd_Servicios.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_Servicios_Buscar()
        {
            try
            {
                BK_Cmd_Servicios = new SqliteCommand(StrBuscar_Servicios, BM_Connection);
                BK_Reader_Servicios = BK_Cmd_Servicios.ExecuteReader();

                if (BK_Reader_Servicios.Read())
                {
                    // Llenar Valores de Servicios
                    BK_NROENVIO = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("NROENVIO"));
                    BK_GUIADESPACHO = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("GUIADESPACHO"));
                    BK_FECHA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("FECHA"));
                    BK_HORA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("HORA"));
                    BK_CLIENTERUT = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("CLIENTERUT"));
                    BK_CLIENTEDIGVER = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("CLIENTEDIGVER"));
                    BK_CLIENTE = Bm_BuscarNombreCliente(BK_CLIENTERUT, BK_CLIENTEDIGVER);
                    BK_MENSAJERORUT = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("MENSAJERORUT"));
                    BK_MENSAJERODIGVER = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("MENSAJERODIGVER"));
                    BK_MENSAJERO = Bm_BuscarNombreMensajero(BK_MENSAJERORUT, BK_MENSAJERODIGVER);
                    BK_RECURSOID = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("RECURSOID"));
                    BK_RECURSO = Bm_BuscarNombreRecurso(BK_RECURSOID);
                    BK_ODOMICILIO1 = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("ODOMICILIO1"));
                    BK_ODOMICILIO2 = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("ODOMICILIO2"));
                    BK_ONUMERO = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("ONUMERO"));
                    BK_OPISO = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("OPISO"));
                    BK_OOFICINA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("OOFICINA"));
                    BK_OCIUDAD = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("OCIUDAD"));
                    BK_OCOMUNA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("OCOMUNA"));
                    BK_OESTADO = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("OESTADO"));
                    BK_OPAIS = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("OPAIS"));
                    BK_OCOORDENADAS = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("OCOORDENADAS"));
                    BK_DDOMICILIO1 = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DDOMICILIO1"));
                    BK_DDOMICILIO2 = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DDOMICILIO2"));
                    BK_DNUMERO = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DNUMERO"));
                    BK_DPISO = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("OPISO"));
                    BK_DOFICINA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DOFICINA"));
                    BK_DCIUDAD = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DCIUDAD"));
                    BK_DCOMUNA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DCOMUNA"));
                    BK_DESTADO = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DESTADO"));
                    BK_DPAIS = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DPAIS"));
                    BK_DCOORDENADAS = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DCOORDENADAS"));
                    BK_DISTANCIA_KM = BK_Reader_Servicios.GetDouble(BK_Reader_Servicios.GetOrdinal("DISTANCIA_KM"));
                    BK_DESCRIPCION = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DESCRIPCION"));
                    BK_FACTURAS = BK_Reader_Servicios.GetInt32(BK_Reader_Servicios.GetOrdinal("FACTURAS"));
                    BK_BULTOS = BK_Reader_Servicios.GetInt32(BK_Reader_Servicios.GetOrdinal("BULTOS"));
                    BK_COMPRAS = BK_Reader_Servicios.GetInt32(BK_Reader_Servicios.GetOrdinal("COMPRAS"));
                    BK_CHEQUES = BK_Reader_Servicios.GetInt32(BK_Reader_Servicios.GetOrdinal("CHEQUES"));
                    BK_SOBRES = BK_Reader_Servicios.GetInt32(BK_Reader_Servicios.GetOrdinal("SOBRES"));
                    BK_OTROS = BK_Reader_Servicios.GetInt32(BK_Reader_Servicios.GetOrdinal("OTROS"));
                    BK_OBSERVACIONES = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("OBSERVACIONES"));
                    BK_ENTREGA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("ENTREGA"));
                    BK_RECEPCION = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("RECEPCION"));
                    BK_TESPERA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("TESPERA"));
                    BK_FECHAENTREGA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("FECHAENTREGA"));
                    BK_HORAENTREGA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("HORAENTREGA"));
                    BK_DISTANCIA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DISTANCIA"));
                    BK_PROGRAMADO = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("PROGRAMADO"));
                    return true;
                }
                else
                {
                    LimpiarVariables();
                    return false;
                }
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_Servicios_Buscar(string pPENTALPHA, string pNROENVIO)
        {
            try
            {
                BK_Cmd_Servicios = new SqliteCommand(StrBuscar_Servicios + " WHERE PENTALPHA = '" + pPENTALPHA + "' AND NROENVIO = '" + pNROENVIO + "'", BM_Connection);
                BK_Reader_Servicios = BK_Cmd_Servicios.ExecuteReader();

                if (BK_Reader_Servicios.Read())
                {
                    // Llenar Valores de Servicios
                    BK_NROENVIO = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("NROENVIO"));
                    BK_GUIADESPACHO = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("GUIADESPACHO"));
                    BK_FECHA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("FECHA"));
                    BK_HORA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("HORA"));
                    BK_CLIENTERUT = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("CLIENTERUT"));
                    BK_CLIENTEDIGVER = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("CLIENTEDIGVER"));
                    BK_CLIENTE = Bm_BuscarNombreCliente(BK_CLIENTERUT, BK_CLIENTEDIGVER);
                    BK_MENSAJERORUT = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("MENSAJERORUT"));
                    BK_MENSAJERODIGVER = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("MENSAJERODIGVER"));
                    BK_MENSAJERO = Bm_BuscarNombreMensajero(BK_MENSAJERORUT, BK_MENSAJERODIGVER);
                    BK_RECURSOID = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("RECURSOID"));
                    BK_RECURSO = Bm_BuscarNombreRecurso(BK_RECURSOID);
                    BK_ODOMICILIO1 = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("ODOMICILIO1"));
                    BK_ODOMICILIO2 = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("ODOMICILIO2"));
                    BK_ONUMERO = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("ONUMERO"));
                    BK_OPISO = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("OPISO"));
                    BK_OOFICINA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("OOFICINA"));
                    BK_OCIUDAD = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("OCIUDAD"));
                    BK_OCOMUNA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("OCOMUNA"));
                    BK_OESTADO = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("OESTADO"));
                    BK_OPAIS = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("OPAIS"));
                    BK_OCOORDENADAS = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("OCOORDENADAS"));
                    BK_DDOMICILIO1 = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DDOMICILIO1"));
                    BK_DDOMICILIO2 = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DDOMICILIO2"));
                    BK_DNUMERO = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DNUMERO"));
                    BK_DPISO = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("OPISO"));
                    BK_DOFICINA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DOFICINA"));
                    BK_DCIUDAD = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DCIUDAD"));
                    BK_DCOMUNA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DCOMUNA"));
                    BK_DESTADO = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DESTADO"));
                    BK_DPAIS = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DPAIS"));
                    BK_DCOORDENADAS = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DCOORDENADAS"));
                    BK_DISTANCIA_KM = BK_Reader_Servicios.GetDouble(BK_Reader_Servicios.GetOrdinal("DISTANCIA_KM"));
                    BK_DESCRIPCION = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DESCRIPCION"));
                    BK_FACTURAS = BK_Reader_Servicios.GetInt32(BK_Reader_Servicios.GetOrdinal("FACTURAS"));
                    BK_BULTOS = BK_Reader_Servicios.GetInt32(BK_Reader_Servicios.GetOrdinal("BULTOS"));
                    BK_COMPRAS = BK_Reader_Servicios.GetInt32(BK_Reader_Servicios.GetOrdinal("COMPRAS"));
                    BK_CHEQUES = BK_Reader_Servicios.GetInt32(BK_Reader_Servicios.GetOrdinal("CHEQUES"));
                    BK_SOBRES = BK_Reader_Servicios.GetInt32(BK_Reader_Servicios.GetOrdinal("SOBRES"));
                    BK_OTROS = BK_Reader_Servicios.GetInt32(BK_Reader_Servicios.GetOrdinal("OTROS"));
                    BK_OBSERVACIONES = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("OBSERVACIONES"));
                    BK_ENTREGA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("ENTREGA"));
                    BK_RECEPCION = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("RECEPCION"));
                    BK_TESPERA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("TESPERA"));
                    BK_FECHAENTREGA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("FECHAENTREGA"));
                    BK_HORAENTREGA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("HORAENTREGA"));
                    BK_DISTANCIA = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DISTANCIA"));
                    BK_PROGRAMADO = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("PROGRAMADO"));
                    return true;
                }
                else
                {
                    LimpiarVariables();
                    return false;
                }
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        // Procedimiento Modificar Servicio
        public bool Bm_Recursos_Modificar(string pPENTALPHA, string pNROENVIO)
        {
            try
            {
                StrModificar_Servicios = "UPDATE SERVICIOS SET ";
                // StrModificar_Servicios += "NROENVIO = '" + BK_NROENVIO + "',";
                StrModificar_Servicios += "GUIADESPACHO = '" + BK_GUIADESPACHO + "',";
                StrModificar_Servicios += "FECHA = '" + BK_FECHA + "',";
                StrModificar_Servicios += "HORA = '" + BK_HORA + "',";
                StrModificar_Servicios += "CLIENTERUT = '" + BK_CLIENTERUT + "',";
                StrModificar_Servicios += "CLIENTEDIGVER = '" + BK_CLIENTEDIGVER + "',";
                StrModificar_Servicios += "MENSAJERORUT = '" + BK_MENSAJERORUT + "',";
                StrModificar_Servicios += "MENSAJERODIGVER = '" + BK_MENSAJERODIGVER + "',";
                StrModificar_Servicios += "RECURSOID = '" + BK_RECURSOID + "',";
                StrModificar_Servicios += "ODOMICILIO1 = '" + BK_ODOMICILIO1 + "',";
                StrModificar_Servicios += "ODOMICILIO2 = '" + BK_ODOMICILIO2 + "',";
                StrModificar_Servicios += "ONUMERO = '" + BK_ONUMERO + "',";
                StrModificar_Servicios += "OPISO = '" + BK_OPISO + "',";
                StrModificar_Servicios += "OOFICINA = '" + BK_OOFICINA + "',";
                StrModificar_Servicios += "OCIUDAD = '" + BK_OCIUDAD + "',";
                StrModificar_Servicios += "OCOMUNA = '" + BK_OCOMUNA + "',";
                StrModificar_Servicios += "OESTADO = '" + BK_OESTADO + "',";
                StrModificar_Servicios += "OPAIS = '" + BK_OPAIS + "',";
                StrModificar_Servicios += "OCOORDENADAS = '" + BK_OCOORDENADAS + "',";
                StrModificar_Servicios += "DDOMICILIO1 = '" + BK_DDOMICILIO1 + "',";
                StrModificar_Servicios += "DDOMICILIO2 = '" + BK_DDOMICILIO2 + "',";
                StrModificar_Servicios += "DNUMERO = '" + BK_DNUMERO + "',";
                StrModificar_Servicios += "DPISO = '" + BK_DPISO + "',";
                StrModificar_Servicios += "DOFICINA = '" + BK_DOFICINA + "',";
                StrModificar_Servicios += "DCIUDAD = '" + BK_DCIUDAD + "',";
                StrModificar_Servicios += "DCOMUNA = '" + BK_DCOMUNA + "',";
                StrModificar_Servicios += "DESTADO = '" + BK_DESTADO + "',";
                StrModificar_Servicios += "DPAIS = '" + BK_DPAIS + "',";
                StrModificar_Servicios += "DCOORDENADAS = '" + BK_DCOORDENADAS + "',";
                StrModificar_Servicios += "DISTANCIA_KM = " + BK_DISTANCIA_KM.ToString("G", CultureInfo.InvariantCulture) + " ,";
                StrModificar_Servicios += "DESCRIPCION = '" + BK_DESCRIPCION + "',";
                StrModificar_Servicios += "FACTURAS = " + BK_FACTURAS.ToString() + " ,";
                StrModificar_Servicios += "BULTOS = " + BK_BULTOS.ToString() + " ,";
                StrModificar_Servicios += "COMPRAS = " + BK_COMPRAS.ToString() + " ,";
                StrModificar_Servicios += "CHEQUES = " + BK_CHEQUES.ToString() + " ,";
                StrModificar_Servicios += "SOBRES = " + BK_SOBRES.ToString() + " ,";
                StrModificar_Servicios += "OTROS = " + BK_OTROS.ToString() + " ,";
                StrModificar_Servicios += "OBSERVACIONES = '" + BK_OBSERVACIONES + "',";
                StrModificar_Servicios += "ENTREGA = '" + BK_ENTREGA + "',";
                StrModificar_Servicios += "RECEPCION = '" + BK_RECEPCION + "',";
                StrModificar_Servicios += "TESPERA = '" + BK_TESPERA + "',";
                StrModificar_Servicios += "FECHAENTREGA = '" + BK_FECHAENTREGA + "',";
                StrModificar_Servicios += "HORAENTREGA = '" + BK_HORAENTREGA + "',";
                StrModificar_Servicios += "DISTANCIA = '" + BK_DISTANCIA + "',";
                StrModificar_Servicios += "PROGRAMADO = '" + BK_PROGRAMADO + "' ";
                StrModificar_Servicios += "WHERE ";
                StrModificar_Servicios += "PENTALPHA = '" + pPENTALPHA + "' AND ";
                StrModificar_Servicios += "NROENVIO = '" + pNROENVIO + "'";
                BK_Cmd_Servicios = new SqliteCommand(StrModificar_Servicios, BM_Connection);
                _ = BK_Cmd_Servicios.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }


        // Procedimiento Borrar Servicios
        public bool Bm_Servicios_Borrar(string pPENTALPHA, string pNROENVIO)
        {
            try
            {
                StrBorrar_Servicios = "DELETE FROM SERVICIOS ";
                StrBorrar_Servicios += "WHERE ";
                StrBorrar_Servicios += "PENTALPHA = '" + pPENTALPHA + "' AND ";
                StrBorrar_Servicios += "NROENVIO = '" + pNROENVIO + "'";
                BK_Cmd_Servicios = new SqliteCommand(StrBorrar_Servicios, BM_Connection);
                BK_Cmd_Servicios.ExecuteNonQuery();
                LimpiarVariables();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_Iniciar_Transaccion()
        {
            try
            {
                BK_Transaccion_Servicio = BM_Connection.BeginTransaction();
                return true;
            }
            catch (SqliteException)
            {
                BK_Transaccion_Servicio.Dispose();
                return false;
            }
        }

        public object Bm_Commit_Transaccion()
        {
            try
            {
                BK_Transaccion_Servicio.Commit();
                return true;
            }
            catch (SqliteException)
            {
                BK_Transaccion_Servicio.Dispose();
                return false;
            }
        }

        public bool Bm_Rollback_Transaccion()
        {
            try
            {
                BK_Transaccion_Servicio.Rollback();
                return true;
            }
            catch (SqliteException)
            {
                BK_Transaccion_Servicio.Dispose();
                return false;
            }
        }


        // Procedimiento Buscar Pais
        public bool Bm_E_Pais_EjecutarSelect()
        {
            try
            {
                BK_Cmd_Servicios_Pais = new SqliteCommand(StrBuscar_Servicios_Pais, BM_Connection);
                BK_Reader_Servicios_Pais = BK_Cmd_Servicios_Pais.ExecuteReader();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_E_Pais_Buscar()
        {
            try
            {
                if (BK_Reader_Servicios_Pais.Read())
                {
                    BK_E_CODPAIS = BK_Reader_Servicios_Pais.GetInt16(BK_Reader_Servicios_Pais.GetOrdinal("CODPAIS"));
                    BK_E_PAIS = BK_Reader_Servicios_Pais.GetString(BK_Reader_Servicios_Pais.GetOrdinal("PAIS"));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        // Procedimiento Buscar Region
        public bool Bm_E_Region_EjecutarSelect()
        {
            try
            {
                BK_Cmd_Servicios_Region = new SqliteCommand(StrBuscar_Servicios_Region, BM_Connection);
                BK_Reader_Servicios_Region = BK_Cmd_Servicios_Region.ExecuteReader();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_E_Region_Buscar()
        {
            try
            {
                if (BK_Reader_Servicios_Region.Read())
                {
                    BK_E_CODREGION = BK_Reader_Servicios_Region.GetInt16(BK_Reader_Servicios_Region.GetOrdinal("CODREGION"));
                    BK_E_REGION = BK_Reader_Servicios_Region.GetString(BK_Reader_Servicios_Region.GetOrdinal("REGION"));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqliteException)
            {
                return false;
            }
        }


        // Procedimiento Buscar Comuna
        public bool Bm_E_Comuna_EjecutarSelect()
        {
            try
            {
                BK_Cmd_Servicios_Comuna = new SqliteCommand(StrBuscar_Servicios_Comuna, BM_Connection);
                BK_Reader_Servicios_Comuna = BK_Cmd_Servicios_Comuna.ExecuteReader();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_E_Comuna_Buscar()
        {
            try
            {
                if (BK_Reader_Servicios_Comuna.Read())
                {
                    BK_E_CODCOMUNA = BK_Reader_Servicios_Comuna.GetInt16(BK_Reader_Servicios_Comuna.GetOrdinal("CODCOMU"));
                    BK_E_COMUNA = BK_Reader_Servicios_Comuna.GetString(BK_Reader_Servicios_Comuna.GetOrdinal("COMUNA"));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        // Procedimiento Buscar Ciudad
        public bool Bm_E_Ciudad_EjecutarSelect()
        {
            try
            {
                BK_Cmd_Servicios_Ciudad = new SqliteCommand(StrBuscar_Servicios_Ciudad, BM_Connection);
                BK_Reader_Servicios_Ciudad = BK_Cmd_Servicios_Ciudad.ExecuteReader();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_E_Ciudad_Buscar()
        {
            try
            {
                if (BK_Reader_Servicios_Ciudad.Read())
                {
                    BK_E_CODCIUDAD = BK_Reader_Servicios_Ciudad.GetInt16(BK_Reader_Servicios_Ciudad.GetOrdinal("CODCIUDAD"));
                    BK_E_CIUDAD = BK_Reader_Servicios_Ciudad.GetString(BK_Reader_Servicios_Ciudad.GetOrdinal("CIUDAD"));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public void LimpiarVariables()
        {
            BK_NROENVIO = "";
            BK_GUIADESPACHO = "";
            BK_FECHA = "";
            BK_HORA = "";
            BK_CLIENTERUT = "";
            BK_CLIENTEDIGVER = "";
            BK_CLIENTE = "";
            BK_MENSAJERORUT = "";
            BK_MENSAJERODIGVER = "";
            BK_MENSAJERO = "";
            BK_RECURSOID = "";
            BK_RECURSO = "";
            BK_ODOMICILIO1 = "";
            BK_ODOMICILIO2 = "";
            BK_ONUMERO = "";
            BK_OPISO = "";
            BK_OOFICINA = "";
            BK_OCIUDAD = "";
            BK_OCOMUNA = "";
            BK_OESTADO = "";
            BK_OPAIS = "";
            BK_OCOORDENADAS = "";
            BK_DDOMICILIO1 = "";
            BK_DDOMICILIO2 = "";
            BK_DNUMERO = "";
            BK_DPISO = "";
            BK_DOFICINA = "";
            BK_DCIUDAD = "";
            BK_DCOMUNA = "";
            BK_DESTADO = "";
            BK_DPAIS = "";
            BK_DCOORDENADAS = "";
            BK_DISTANCIA_KM = 0;
            BK_DESCRIPCION = "";
            BK_FACTURAS = 0;
            BK_BULTOS = 0;
            BK_COMPRAS = 0;
            BK_CHEQUES = 0;
            BK_SOBRES = 0;
            BK_OTROS = 0;
            BK_OBSERVACIONES = "";
            BK_ENTREGA = "";
            BK_RECEPCION = "";
            BK_TESPERA = "";
            BK_FECHAENTREGA = "";
            BK_HORAENTREGA = "";
            BK_DISTANCIA = "";
            BK_PROGRAMADO = "";
        }

        private string Bm_BuscarNombreCliente(string pRUT, string pDIGVER)
        {
            SqliteCommand BK_Cmd_BuscarCliente;
            SqliteDataReader BK_Reader_BuscarCliente;
            string lRetorno;
            try
            {
                BK_Cmd_BuscarCliente = new SqliteCommand("SELECT NOMBRE FROM CLIENTES WHERE RUTID = '" + pRUT + "' AND DIGVER = '" + pDIGVER + "'", BM_Connection);
                BK_Reader_BuscarCliente = BK_Cmd_BuscarCliente.ExecuteReader();
                if (BK_Reader_BuscarCliente.Read())
                    lRetorno = BK_Reader_BuscarCliente.GetString(0);
                else
                    lRetorno = "CLIENTE NO REGISTRADO";
                BK_Reader_BuscarCliente.Close();
                BK_Cmd_BuscarCliente.Dispose();
            }
            catch (SqliteException)
            {
                lRetorno = "CLIENTE NO REGISTRADO";
            }
            return lRetorno;
        }

        private string Bm_BuscarNombreMensajero(string pRUT, string pDIGVER)
        {
            SqliteCommand BK_Cmd_BuscarMensajero;
            SqliteDataReader BK_Reader_BuscarMensajero;
            string lRetorno;
            try
            {
                BK_Cmd_BuscarMensajero = new SqliteCommand("SELECT APELLIDOS||'. '||NOMBRES FROM PERSONAL WHERE RUTID = '" + pRUT + "' AND DIGVER = '" + pDIGVER + "'", BM_Connection);
                BK_Reader_BuscarMensajero = BK_Cmd_BuscarMensajero.ExecuteReader();
                if (BK_Reader_BuscarMensajero.Read())
                    lRetorno = BK_Reader_BuscarMensajero.GetString(0);
                else
                    lRetorno = "MENSAJERO NO REGISTRADO";
                BK_Reader_BuscarMensajero.Close();
                BK_Cmd_BuscarMensajero.Dispose();
            }
            catch (SqliteException)
            {
                lRetorno = "MENSAJERO NO REGISTRADO";
            }
            return lRetorno;
        }

        private string Bm_BuscarNombreRecurso(string pRECURSOID)
        {
            SqliteCommand BK_Cmd_BuscarRecurso;
            SqliteDataReader BK_Reader_BuscarRecurso;
            string lRetorno;
            try
            {
                BK_Cmd_BuscarRecurso = new SqliteCommand("SELECT TIPO||'-'||MARCA||'-'||MODELO FROM RECURSOS WHERE PATENTE = '" + pRECURSOID + "'", BM_Connection);
                BK_Reader_BuscarRecurso = BK_Cmd_BuscarRecurso.ExecuteReader();
                if (BK_Reader_BuscarRecurso.Read())
                    lRetorno = BK_Reader_BuscarRecurso.GetString(0);
                else
                    lRetorno = "RECURSO NO REGISTRADO";
                BK_Reader_BuscarRecurso.Close();
                BK_Cmd_BuscarRecurso.Dispose();
            }
            catch (SqliteException)
            {
                lRetorno = "RECURSO NO REGISTRADO";
            }
            return lRetorno;
        }

        // ****************************************************
        // Procedimientos de Envios
        //*****************************************************
        public bool Bm_Envios_BuscarGrid()
        {
            try
            {
                BK_Cmd_Envios_Grid = new SqliteCommand(StrBuscar_Servicios_Envios, BM_Connection);
                BK_Reader_Servicios_Envios = BK_Cmd_Envios_Grid.ExecuteReader();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_Envios_BuscarGridProxima()
        {
            try
            {
                if (BK_Reader_Servicios_Envios.Read())
                {
                    // Llenar Valores de la Empresa
                    BK_GRID_ENVIO_NRO = BK_Reader_Servicios_Envios.GetString(0);
                    BK_GRID_ENVIO_FECHA = DateTime.Parse(BK_Reader_Servicios_Envios.GetString(1)).ToShortDateString();
                    BK_GRID_ENVIO_CLIENTE = Bm_BuscarNombreCliente(BK_Reader_Servicios_Envios.GetString(2), BK_Reader_Servicios_Envios.GetString(3));
                    BK_GRID_ENVIO_ENTREGA = BK_Reader_Servicios_Envios.GetString(4);
                    return true;
                }
                else
                {
                    // No existe la empresa
                    return false;
                }
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        //********************************************************
        // Procedimientos de Clientes
        //********************************************************
        public bool Bm_Clientes_BuscarGrid()
        {
            try
            {
                BK_Cmd_Clientes_Grid = new SqliteCommand(StrBuscar_Servicios_Clientes, BM_Connection);
                BK_Reader_Servicios_Clientes = BK_Cmd_Clientes_Grid.ExecuteReader();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_Clientes_BuscarGridProxima()
        {
            try
            {
                if (BK_Reader_Servicios_Clientes.Read())
                {
                    // Llenar Valores de la Empresa
                    BK_GRID_CLIENTE_RUTID = BK_Reader_Servicios_Clientes.GetString(0);
                    BK_GRID_CLIENTE_NOMBRE = BK_Reader_Servicios_Clientes.GetString(1);
                    return true;
                }
                else
                {
                    // No existe la empresa
                    return false;
                }
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        //********************************************************
        // Procedimientos de Mensajeros
        //********************************************************
        public bool Bm_Mensajeros_BuscarGrid()
        {
            try
            {
                BK_Cmd_Mensajeros_Grid = new SqliteCommand(StrBuscar_Servicios_Mensajeros, BM_Connection);
                BK_Reader_Servicios_Mensajeros = BK_Cmd_Mensajeros_Grid.ExecuteReader();
                return true;
            }
            catch (Microsoft.Data.Sqlite.SqliteException)
            {
                return false;
            }
        }

        public bool Bm_Mensajeros_BuscarGridProxima()
        {
            try
            {
                if (BK_Reader_Servicios_Mensajeros.Read())
                {
                    // Llenar Valores de la Empresa
                    BK_GRID_MENSAJERO_RUTID = BK_Reader_Servicios_Mensajeros.GetString(0);
                    BK_GRID_MENSAJERO_NOMBRE = BK_Reader_Servicios_Mensajeros.GetString(1);
                    return true;
                }
                else
                {
                    // No existe la empresa
                    return false;
                }
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        //********************************************************
        // Procedimientos de Recursos
        //********************************************************
        public bool Bm_Recursos_BuscarGrid()
        {
            try
            {
                BK_Cmd_Recursos_Grid = new SqliteCommand(StrBuscar_Servicios_Recursos, BM_Connection);
                BK_Reader_Servicios_Recursos = BK_Cmd_Recursos_Grid.ExecuteReader();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        public bool Bm_Recursos_BuscarGridProxima()
        {
            try
            {
                if (BK_Reader_Servicios_Recursos.Read())
                {
                    // Llenar Valores de la Empresa
                    //
                    BK_GRID_RECURSO_PATENTE = BK_Reader_Servicios_Recursos.GetString(0);
                    BK_GRID_RECURSO_TIPO = BK_Reader_Servicios_Recursos.GetString(1);
                    BK_GRID_RECURSO_MARCA = BK_Reader_Servicios_Recursos.GetString(2);
                    BK_GRID_RECURSO_MODELO = BK_Reader_Servicios_Recursos.GetString(3);
                    BK_GRID_RECURSO_PROPIETARIO = BK_Reader_Servicios_Recursos.GetString(4);
                    return true;
                }
                else
                {
                    // No existe la empresa
                    return false;
                }
            }
            catch (SqliteException)
            {
                return false;
            }
        }
    }
}
