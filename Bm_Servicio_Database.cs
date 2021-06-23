using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace BikeMessenger
{
    internal class Bm_Servicio_Database
    {
        private static SQLiteConnection BM_Conexion;
        private TransferVar BM_TrasferVar;

        private static JsonBikeMessengerServicio BK_Servicio;
        private static List<JsonBikeMessengerServicio> BK_ServicioLista;

        private static List<string> BK_Pais;
        private static List<string> BK_Region;
        private static List<string> BK_Comuna;
        private static List<string> BK_Ciudad;

        // Busqueda por Muchos
        public List<JsonBikeMessengerServicio> BuscarServicio()
        {
            BK_Servicio = new JsonBikeMessengerServicio();
            BK_ServicioLista = new List<JsonBikeMessengerServicio>();
            DataSet BM_DataSet;
            SQLiteDataAdapter BM_Adaptador;
            SQLiteCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM SERVICIOS");

                BM_Adaptador = new SQLiteDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };
                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "SERVICIOS");

                foreach (DataRow LvrServicio in BM_DataSet.Tables["SERVICIOS"].Rows)
                {
                    BK_Servicio.PENTALPHA = LvrServicio["PENTALPHA"].ToString();
                    BK_Servicio.NROENVIO = LvrServicio["NROENVIO"].ToString();
                    BK_Servicio.GUIADESPACHO = LvrServicio["GUIADESPACHO"].ToString();
                    BK_Servicio.FECHA = LvrServicio["FECHA"].ToString();
                    BK_Servicio.HORA = LvrServicio["HORA"].ToString();
                    BK_Servicio.CLIENTERUT = LvrServicio["CLIENTERUT"].ToString();
                    BK_Servicio.CLIENTEDIGVER = LvrServicio["CLIENTEDIGVER"].ToString();
                    // BK_Servicio.CLIENTE = LvrServicio["CLIENTE"].ToString();
                    BK_Servicio.MENSAJERORUT = LvrServicio["MENSAJERORUT"].ToString();
                    BK_Servicio.MENSAJERODIGVER = LvrServicio["MENSAJERODIGVER"].ToString();
                    // BK_Servicio.MENSAJERO = LvrServicio["MENSAJERO"].ToString();
                    BK_Servicio.RECURSOID = LvrServicio["RECURSOID"].ToString();
                    // BK_Servicio.RECURSO = LvrServicio["RECURSO"].ToString();
                    BK_Servicio.ODOMICILIO1 = LvrServicio["ODOMICILIO1"].ToString();
                    BK_Servicio.ODOMICILIO2 = LvrServicio["ODOMICILIO2"].ToString();
                    BK_Servicio.ONUMERO = LvrServicio["ONUMERO"].ToString();
                    BK_Servicio.OPISO = LvrServicio["OPISO"].ToString();
                    BK_Servicio.OOFICINA = LvrServicio["OOFICINA"].ToString();
                    BK_Servicio.OCIUDAD = LvrServicio["OCIUDAD"].ToString();
                    BK_Servicio.OCOMUNA = LvrServicio["OCOMUNA"].ToString();
                    BK_Servicio.OESTADO = LvrServicio["OESTADO"].ToString();
                    BK_Servicio.OPAIS = LvrServicio["OPAIS"].ToString();
                    BK_Servicio.OCOORDENADAS = LvrServicio["OCOORDENADAS"].ToString();
                    BK_Servicio.DDOMICILIO1 = LvrServicio["DDOMICILIO1"].ToString();
                    BK_Servicio.DDOMICILIO2 = LvrServicio["DDOMICILIO2"].ToString();
                    BK_Servicio.DNUMERO = LvrServicio["DNUMERO"].ToString();
                    BK_Servicio.DPISO = LvrServicio["DPISO"].ToString();
                    BK_Servicio.DOFICINA = LvrServicio["DOFICINA"].ToString();
                    BK_Servicio.DCIUDAD = LvrServicio["DCIUDAD"].ToString();
                    BK_Servicio.DCOMUNA = LvrServicio["DCOMUNA"].ToString();
                    BK_Servicio.DESTADO = LvrServicio["DESTADO"].ToString();
                    BK_Servicio.DPAIS = LvrServicio["DPAIS"].ToString();
                    BK_Servicio.DCOORDENADAS = LvrServicio["DCOORDENADAS"].ToString();
                    BK_Servicio.DESCRIPCION = LvrServicio["DESCRIPCION"].ToString();
                    BK_Servicio.FACTURAS = LvrServicio["FACTURAS"].ToString();
                    BK_Servicio.BULTOS = LvrServicio["BULTOS"].ToString();
                    BK_Servicio.COMPRAS = LvrServicio["COMPRAS"].ToString();
                    BK_Servicio.CHEQUES = LvrServicio["CHEQUES"].ToString();
                    BK_Servicio.SOBRES = LvrServicio["SOBRES"].ToString();
                    BK_Servicio.OTROS = LvrServicio["OTROS"].ToString();
                    BK_Servicio.OBSERVACIONES = LvrServicio["OBSERVACIONES"].ToString();
                    BK_Servicio.ENTREGA = LvrServicio["ENTREGA"].ToString();
                    BK_Servicio.RECEPCION = LvrServicio["RECEPCION"].ToString();
                    BK_Servicio.TESPERA = LvrServicio["TESPERA"].ToString();
                    BK_Servicio.FECHAENTREGA = LvrServicio["FECHAENTREGA"].ToString();
                    BK_Servicio.HORAENTREGA = LvrServicio["HORAENTREGA"].ToString();
                    BK_Servicio.DISTANCIA = LvrServicio["DISTANCIA"].ToString();
                    BK_Servicio.PROGRAMADO = LvrServicio["PROGRAMADO"].ToString();
                    BK_ServicioLista.Add(BK_Servicio);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return BK_ServicioLista;
        }

        public List<JsonBikeMessengerServicio> BuscarServicio(string pPENTALPHA, string pNROENVIO)
        {
            BK_Servicio = new JsonBikeMessengerServicio();
            BK_ServicioLista = new List<JsonBikeMessengerServicio>();
            DataSet BM_DataSet;
            SQLiteDataAdapter BM_Adaptador;
            SQLiteCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM SERVICIOS WHERE PENTALPHA = '" + pPENTALPHA + "' AND NROENVIO = '" + pNROENVIO + "'");

                BM_Adaptador = new SQLiteDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };
                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "SERVICIOS");

                foreach (DataRow LvrServicio in BM_DataSet.Tables["SERVICIOS"].Rows)
                {
                    BK_Servicio.PENTALPHA = LvrServicio["PENTALPHA"].ToString();
                    BK_Servicio.NROENVIO = LvrServicio["NROENVIO"].ToString();
                    BK_Servicio.GUIADESPACHO = LvrServicio["GUIADESPACHO"].ToString();
                    BK_Servicio.FECHA = LvrServicio["FECHA"].ToString();
                    BK_Servicio.HORA = LvrServicio["HORA"].ToString();
                    BK_Servicio.CLIENTERUT = LvrServicio["CLIENTERUT"].ToString();
                    BK_Servicio.CLIENTEDIGVER = LvrServicio["CLIENTEDIGVER"].ToString();
                    // BK_Servicio.CLIENTE = LvrServicio["CLIENTE"].ToString();
                    BK_Servicio.MENSAJERORUT = LvrServicio["MENSAJERORUT"].ToString();
                    BK_Servicio.MENSAJERODIGVER = LvrServicio["MENSAJERODIGVER"].ToString();
                    // BK_Servicio.MENSAJERO = LvrServicio["MENSAJERO"].ToString();
                    BK_Servicio.RECURSOID = LvrServicio["RECURSOID"].ToString();
                    // BK_Servicio.RECURSO = LvrServicio["RECURSO"].ToString();
                    BK_Servicio.ODOMICILIO1 = LvrServicio["ODOMICILIO1"].ToString();
                    BK_Servicio.ODOMICILIO2 = LvrServicio["ODOMICILIO2"].ToString();
                    BK_Servicio.ONUMERO = LvrServicio["ONUMERO"].ToString();
                    BK_Servicio.OPISO = LvrServicio["OPISO"].ToString();
                    BK_Servicio.OOFICINA = LvrServicio["OOFICINA"].ToString();
                    BK_Servicio.OCIUDAD = LvrServicio["OCIUDAD"].ToString();
                    BK_Servicio.OCOMUNA = LvrServicio["OCOMUNA"].ToString();
                    BK_Servicio.OESTADO = LvrServicio["OESTADO"].ToString();
                    BK_Servicio.OPAIS = LvrServicio["OPAIS"].ToString();
                    BK_Servicio.OCOORDENADAS = LvrServicio["OCOORDENADAS"].ToString();
                    BK_Servicio.DDOMICILIO1 = LvrServicio["DDOMICILIO1"].ToString();
                    BK_Servicio.DDOMICILIO2 = LvrServicio["DDOMICILIO2"].ToString();
                    BK_Servicio.DNUMERO = LvrServicio["DNUMERO"].ToString();
                    BK_Servicio.DPISO = LvrServicio["DPISO"].ToString();
                    BK_Servicio.DOFICINA = LvrServicio["DOFICINA"].ToString();
                    BK_Servicio.DCIUDAD = LvrServicio["DCIUDAD"].ToString();
                    BK_Servicio.DCOMUNA = LvrServicio["DCOMUNA"].ToString();
                    BK_Servicio.DESTADO = LvrServicio["DESTADO"].ToString();
                    BK_Servicio.DPAIS = LvrServicio["DPAIS"].ToString();
                    BK_Servicio.DCOORDENADAS = LvrServicio["DCOORDENADAS"].ToString();
                    BK_Servicio.DESCRIPCION = LvrServicio["DESCRIPCION"].ToString();
                    BK_Servicio.FACTURAS = LvrServicio["FACTURAS"].ToString();
                    BK_Servicio.BULTOS = LvrServicio["BULTOS"].ToString();
                    BK_Servicio.COMPRAS = LvrServicio["COMPRAS"].ToString();
                    BK_Servicio.CHEQUES = LvrServicio["CHEQUES"].ToString();
                    BK_Servicio.SOBRES = LvrServicio["SOBRES"].ToString();
                    BK_Servicio.OTROS = LvrServicio["OTROS"].ToString();
                    BK_Servicio.OBSERVACIONES = LvrServicio["OBSERVACIONES"].ToString();
                    BK_Servicio.ENTREGA = LvrServicio["ENTREGA"].ToString();
                    BK_Servicio.RECEPCION = LvrServicio["RECEPCION"].ToString();
                    BK_Servicio.TESPERA = LvrServicio["TESPERA"].ToString();
                    BK_Servicio.FECHAENTREGA = LvrServicio["FECHAENTREGA"].ToString();
                    BK_Servicio.HORAENTREGA = LvrServicio["HORAENTREGA"].ToString();
                    BK_Servicio.DISTANCIA = LvrServicio["DISTANCIA"].ToString();
                    BK_Servicio.PROGRAMADO = LvrServicio["PROGRAMADO"].ToString();
                    BK_ServicioLista.Add(BK_Servicio);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return BK_ServicioLista;
        }

        public bool AgregarServicio(JsonBikeMessengerServicio aBK_Servicio)
        {
            BK_Servicio = new JsonBikeMessengerServicio();
            BK_ServicioLista = new List<JsonBikeMessengerServicio>();

            DataSet BM_DataSet;
            SQLiteDataAdapter BM_Adaptador;
            SQLiteCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM SERVICIOS");

                BM_Adaptador = new SQLiteDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };

                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "SERVICIOS");

                DataRow LvrServicio = BM_DataSet.Tables["SERVICIOS"].NewRow();

                LvrServicio["PENTALPHA"] = aBK_Servicio.PENTALPHA;
                LvrServicio["NROENVIO"] = aBK_Servicio.NROENVIO;
                LvrServicio["GUIADESPACHO"] = aBK_Servicio.GUIADESPACHO;
                LvrServicio["FECHA"] = aBK_Servicio.FECHA;
                LvrServicio["HORA"] = aBK_Servicio.HORA;
                LvrServicio["CLIENTERUT"] = aBK_Servicio.CLIENTERUT;
                LvrServicio["CLIENTEDIGVER"] = aBK_Servicio.CLIENTEDIGVER;
                // LvrServicio["CLIENTE"] = aBK_Servicio.CLIENTE;
                LvrServicio["MENSAJERORUT"] = aBK_Servicio.MENSAJERORUT;
                LvrServicio["MENSAJERODIGVER"] = aBK_Servicio.MENSAJERODIGVER;
                // LvrServicio["MENSAJERO"] = aBK_Servicio.MENSAJERO;
                LvrServicio["RECURSOID"] = aBK_Servicio.RECURSOID;
                // LvrServicio["RECURSO"] = aBK_Servicio.RECURSO;
                LvrServicio["ODOMICILIO1"] = aBK_Servicio.ODOMICILIO1;
                LvrServicio["ODOMICILIO2"] = aBK_Servicio.ODOMICILIO2;
                LvrServicio["ONUMERO"] = aBK_Servicio.ONUMERO;
                LvrServicio["OPISO"] = aBK_Servicio.OPISO;
                LvrServicio["OOFICINA"] = aBK_Servicio.OOFICINA;
                LvrServicio["OCIUDAD"] = aBK_Servicio.OCIUDAD;
                LvrServicio["OCOMUNA"] = aBK_Servicio.OCOMUNA;
                LvrServicio["OESTADO"] = aBK_Servicio.OESTADO;
                LvrServicio["OPAIS"] = aBK_Servicio.OPAIS;
                LvrServicio["OCOORDENADAS"] = aBK_Servicio.OCOORDENADAS;
                LvrServicio["DDOMICILIO1"] = aBK_Servicio.DDOMICILIO1;
                LvrServicio["DDOMICILIO2"] = aBK_Servicio.DDOMICILIO2;
                LvrServicio["DNUMERO"] = aBK_Servicio.DNUMERO;
                LvrServicio["DPISO"] = aBK_Servicio.DPISO;
                LvrServicio["DOFICINA"] = aBK_Servicio.DOFICINA;
                LvrServicio["DCIUDAD"] = aBK_Servicio.DCIUDAD;
                LvrServicio["DCOMUNA"] = aBK_Servicio.DCOMUNA;
                LvrServicio["DESTADO"] = aBK_Servicio.DESTADO;
                LvrServicio["DPAIS"] = aBK_Servicio.DPAIS;
                LvrServicio["DCOORDENADAS"] = aBK_Servicio.DCOORDENADAS;
                LvrServicio["DESCRIPCION"] = aBK_Servicio.DESCRIPCION;
                LvrServicio["FACTURAS"] = aBK_Servicio.FACTURAS;
                LvrServicio["BULTOS"] = aBK_Servicio.BULTOS;
                LvrServicio["COMPRAS"] = aBK_Servicio.COMPRAS;
                LvrServicio["CHEQUES"] = aBK_Servicio.CHEQUES;
                LvrServicio["SOBRES"] = aBK_Servicio.SOBRES;
                LvrServicio["OTROS"] = aBK_Servicio.OTROS;
                LvrServicio["OBSERVACIONES"] = aBK_Servicio.OBSERVACIONES;
                LvrServicio["ENTREGA"] = aBK_Servicio.ENTREGA;
                LvrServicio["RECEPCION"] = aBK_Servicio.RECEPCION;
                LvrServicio["TESPERA"] = aBK_Servicio.TESPERA;
                LvrServicio["FECHAENTREGA"] = aBK_Servicio.FECHAENTREGA;
                LvrServicio["HORAENTREGA"] = aBK_Servicio.HORAENTREGA;
                LvrServicio["DISTANCIA"] = aBK_Servicio.DISTANCIA;
                LvrServicio["PROGRAMADO"] = aBK_Servicio.PROGRAMADO;
                BM_DataSet.Tables["SERVICIOS"].Rows.Add(LvrServicio);
                BM_Adaptador.Update(BM_DataSet, "SERVICIOS");
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return false;
            }
            return true;
        }


        public bool ModificarServicio(JsonBikeMessengerServicio mBK_Servicio)
        {
            BK_Servicio = new JsonBikeMessengerServicio();
            BK_ServicioLista = new List<JsonBikeMessengerServicio>();
            DataSet BM_DataSet;
            SQLiteDataAdapter BM_Adaptador;
            SQLiteCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM SERVICIOS WHERE PENTALPHA = '" + mBK_Servicio.PENTALPHA + "' AND NROENVIO = '" + mBK_Servicio.NROENVIO + "'");

                BM_Adaptador = new SQLiteDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };

                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "SERVICIOS");

                DataRow LvrServicio = BM_DataSet.Tables["SERVICIOS"].NewRow();
                LvrServicio["PENTALPHA"] = mBK_Servicio.PENTALPHA;
                LvrServicio["NROENVIO"] = mBK_Servicio.NROENVIO;
                LvrServicio["GUIADESPACHO"] = mBK_Servicio.GUIADESPACHO;
                LvrServicio["FECHA"] = mBK_Servicio.FECHA;
                LvrServicio["HORA"] = mBK_Servicio.HORA;
                LvrServicio["CLIENTERUT"] = mBK_Servicio.CLIENTERUT;
                LvrServicio["CLIENTEDIGVER"] = mBK_Servicio.CLIENTEDIGVER;
                // LvrServicio["CLIENTE"] = mBK_Servicio.CLIENTE;
                LvrServicio["MENSAJERORUT"] = mBK_Servicio.MENSAJERORUT;
                LvrServicio["MENSAJERODIGVER"] = mBK_Servicio.MENSAJERODIGVER;
                // LvrServicio["MENSAJERO"] = mBK_Servicio.MENSAJERO;
                LvrServicio["RECURSOID"] = mBK_Servicio.RECURSOID;
                // LvrServicio["RECURSO"] = mBK_Servicio.RECURSO;
                LvrServicio["ODOMICILIO1"] = mBK_Servicio.ODOMICILIO1;
                LvrServicio["ODOMICILIO2"] = mBK_Servicio.ODOMICILIO2;
                LvrServicio["ONUMERO"] = mBK_Servicio.ONUMERO;
                LvrServicio["OPISO"] = mBK_Servicio.OPISO;
                LvrServicio["OOFICINA"] = mBK_Servicio.OOFICINA;
                LvrServicio["OCIUDAD"] = mBK_Servicio.OCIUDAD;
                LvrServicio["OCOMUNA"] = mBK_Servicio.OCOMUNA;
                LvrServicio["OESTADO"] = mBK_Servicio.OESTADO;
                LvrServicio["OPAIS"] = mBK_Servicio.OPAIS;
                LvrServicio["OCOORDENADAS"] = mBK_Servicio.OCOORDENADAS;
                LvrServicio["DDOMICILIO1"] = mBK_Servicio.DDOMICILIO1;
                LvrServicio["DDOMICILIO2"] = mBK_Servicio.DDOMICILIO2;
                LvrServicio["DNUMERO"] = mBK_Servicio.DNUMERO;
                LvrServicio["DPISO"] = mBK_Servicio.DPISO;
                LvrServicio["DOFICINA"] = mBK_Servicio.DOFICINA;
                LvrServicio["DCIUDAD"] = mBK_Servicio.DCIUDAD;
                LvrServicio["DCOMUNA"] = mBK_Servicio.DCOMUNA;
                LvrServicio["DESTADO"] = mBK_Servicio.DESTADO;
                LvrServicio["DPAIS"] = mBK_Servicio.DPAIS;
                LvrServicio["DCOORDENADAS"] = mBK_Servicio.DCOORDENADAS;
                LvrServicio["DESCRIPCION"] = mBK_Servicio.DESCRIPCION;
                LvrServicio["FACTURAS"] = mBK_Servicio.FACTURAS;
                LvrServicio["BULTOS"] = mBK_Servicio.BULTOS;
                LvrServicio["COMPRAS"] = mBK_Servicio.COMPRAS;
                LvrServicio["CHEQUES"] = mBK_Servicio.CHEQUES;
                LvrServicio["SOBRES"] = mBK_Servicio.SOBRES;
                LvrServicio["OTROS"] = mBK_Servicio.OTROS;
                LvrServicio["OBSERVACIONES"] = mBK_Servicio.OBSERVACIONES;
                LvrServicio["ENTREGA"] = mBK_Servicio.ENTREGA;
                LvrServicio["RECEPCION"] = mBK_Servicio.RECEPCION;
                LvrServicio["TESPERA"] = mBK_Servicio.TESPERA;
                LvrServicio["FECHAENTREGA"] = mBK_Servicio.FECHAENTREGA;
                LvrServicio["HORAENTREGA"] = mBK_Servicio.HORAENTREGA;
                LvrServicio["DISTANCIA"] = mBK_Servicio.DISTANCIA;
                LvrServicio["PROGRAMADO"] = mBK_Servicio.PROGRAMADO;
                BM_DataSet.Tables["SERVICIOS"].Rows.Add(LvrServicio);
                BM_Adaptador.Update(BM_DataSet, "RECURSOS");
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return false;
            }
            return true;
        }


        public bool BorrarServicio(string pPENTALPHA, string pNROENVIO)
        {
            BK_Servicio = new JsonBikeMessengerServicio();
            BK_ServicioLista = new List<JsonBikeMessengerServicio>();
            SQLiteCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("DELETE FROM SERVICIOS WHERE PENTALPHA = '" + pPENTALPHA + "' AND NROENVIO = '" + pNROENVIO + "'");
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return false;
            }
            return true;
        }

        // Utilitarios
        public List<JsonBikeMessengerRecurso> BuscarGridRecurso()
        {
            JsonBikeMessengerRecurso BK_Recurso;
            List<JsonBikeMessengerRecurso> BK_RecursoLista;
            BK_RecursoLista = null;
            /*
            using (var db = new BK_SQliteContext())
            {
                foreach (var LvrServicio in db.RECURSOs)
                {
                    BK_Recurso = null;
                    BK_Recurso.PATENTE = LvrServicio.PATENTE;
                    BK_Recurso.TIPO = LvrServicio.TIPO;
                    BK_Recurso.MARCA = LvrServicio.MARCA;
                    BK_Recurso.MODELO = LvrServicio.MODELO;
                    BK_Recurso.CIUDAD = LvrServicio.CIUDAD;
                    BK_RecursoLista.Add(BK_Recurso);
                }
            }
            */
            return BK_RecursoLista;
        }

        public List<JsonBikeMessengerPersonal> BuscarGridPersonal()
        {
            JsonBikeMessengerPersonal BK_Personal;
            List<JsonBikeMessengerPersonal> BK_PersonalLista;
            BK_PersonalLista = null;
            /*
            using (var db = new BK_SQliteContext())
            {
                foreach (var LvrPersonal in db.PERSONALs)
                {
                    BK_Personal = null;
                    BK_Personal.RUTID = LvrPersonal.RUTID;
                    BK_Personal.DIGVER = LvrPersonal.DIGVER;
                    BK_Personal.APELLIDOS = LvrPersonal.APELLIDOS;
                    BK_Personal.NOMBRES = LvrPersonal.NOMBRES;
                    BK_PersonalLista.Add(BK_Personal);
                }
            }
            */
            return BK_PersonalLista;
        }


        public List<JsonBikeMessengerCliente> BuscarGridCliente()
        {
            List<JsonBikeMessengerCliente> BK_ClienteLista;
            JsonBikeMessengerCliente BK_Cliente;

            BK_ClienteLista = null;
            /*
            using (var db = new BK_SQliteContext())
            {
                foreach (var LvrCliente in db.CLIENTEs)
                {
                    BK_Cliente = null;
                    BK_Cliente.RUTID = LvrCliente.RUTID;
                    BK_Cliente.DIGVER = LvrCliente.DIGVER;
                    BK_Cliente.NOMBRE = LvrCliente.NOMBRE;
                    BK_ClienteLista.Add(BK_Cliente);
                }
            }
            */
            return BK_ClienteLista;
        }

        private string Bm_BuscarNombreCliente(string pPENTALPHA, string pRUT, string pDIGVER)
        {
            string lRetorno = "";
            /*
            using (var db = new BK_SQliteContext())
            {
                CLIENTE LvrCliente;
                LvrCliente = db.CLIENTEs.Find(pPENTALPHA, pRUT, pDIGVER);
                lRetorno = LvrCliente.NOMBRE;
            }
            */
            return lRetorno;
        }

        private string Bm_BuscarNombreMensajero(string pPENTALPHA, string pRUT, string pDIGVER)
        {
            string lRetorno = "";
            /*
            using (var db = new BK_SQliteContext())
            {
                PERSONAL LvrPersonal;
                LvrPersonal = db.PERSONALs.Find(pPENTALPHA, pRUT, pDIGVER);
                lRetorno = LvrPersonal.APELLIDOS + ", " + LvrPersonal.NOMBRES;
            }
            */
            return lRetorno;
        }

        private string Bm_BuscarNombreRecurso(string pPENTALPHA, string pPATENTE)
        {
            string lRetorno = "";
            /*
            using (var db = new BK_SQliteContext())
            {
                RECURSO LvrServicio;
                LvrServicio = db.RECURSOs.Find(pPENTALPHA, pPATENTE);
                lRetorno = LvrServicio.TIPO + "-" + LvrServicio.MARCA + "-" + LvrServicio.MODELO;
            }
            */
            return lRetorno;
        }

        public List<string> GetPais()
        {
            List<string> BK_PaisLista = new List<string>();
            DataSet BM_DataSetPais;
            SQLiteDataAdapter BM_AdaptadorPais;
            SQLiteCommand BM_ComandoPais;
            string BK_Pais;

            try
            {
                BM_ComandoPais = BM_Conexion.CreateCommand();
                BM_ComandoPais.CommandText = string.Format("SELECT * FROM PAIS ORDER BY NOMBEW");

                BM_AdaptadorPais = new SQLiteDataAdapter(BM_ComandoPais);

                BM_DataSetPais = new DataSet();
                BM_AdaptadorPais.Fill(BM_DataSetPais, "PAIS");

                foreach (DataRow LvrPais in BM_DataSetPais.Tables["PAIS"].Rows)
                {
                    BK_Pais = LvrPais["NOMBRE"].ToString();
                    BK_PaisLista.Add(BK_Pais);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return BK_PaisLista;
        }

        public List<string> GetRegion()
        {
            List<string> BK_RegionLista = new List<string>();
            DataSet BM_DataSetRegion;
            SQLiteDataAdapter BM_AdaptadorRegion;
            SQLiteCommand BM_ComandoRegion;
            string BK_Region;

            try
            {
                BM_ComandoRegion = BM_Conexion.CreateCommand();
                BM_ComandoRegion.CommandText = string.Format("SELECT * FROM ESTADOREGION ORDER BY NOMBRE");

                BM_AdaptadorRegion = new SQLiteDataAdapter(BM_ComandoRegion);

                BM_DataSetRegion = new DataSet();
                BM_AdaptadorRegion.Fill(BM_DataSetRegion, "ESTADOREGION");

                foreach (DataRow LvrRegion in BM_DataSetRegion.Tables["ESTADOREGION"].Rows)
                {
                    BK_Region = LvrRegion["NOMBRE"].ToString();
                    BK_RegionLista.Add(BK_Region);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return BK_RegionLista;
        }

        public List<string> GetComuna()
        {
            List<string> BK_ComunaLista = new List<string>();
            DataSet BM_DataSetComuna;
            SQLiteDataAdapter BM_AdaptadorComuna;
            SQLiteCommand BM_ComandoComuna;
            string BK_Comuna;

            try
            {
                BM_ComandoComuna = BM_Conexion.CreateCommand();
                BM_ComandoComuna.CommandText = string.Format("SELECT * FROM COMUNA ORDER BY NOMBRE");

                BM_AdaptadorComuna = new SQLiteDataAdapter(BM_ComandoComuna);

                BM_DataSetComuna = new DataSet();
                BM_AdaptadorComuna.Fill(BM_DataSetComuna, "COMUNA");

                foreach (DataRow LvrComuna in BM_DataSetComuna.Tables["COMUNA"].Rows)
                {
                    BK_Comuna = LvrComuna["NOMBRE"].ToString();
                    BK_ComunaLista.Add(BK_Comuna);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return BK_ComunaLista;
        }

        public List<string> GetCiudad()
        {
            List<string> BK_RegionLista = new List<string>();
            DataSet BM_DataSetRegion;
            SQLiteDataAdapter BM_AdaptadorRegion;
            SQLiteCommand BM_ComandoRegion;
            string BK_Region;

            try
            {
                BM_ComandoRegion = BM_Conexion.CreateCommand();
                BM_ComandoRegion.CommandText = string.Format("SELECT * FROM ESTADOREGION ORDER BY NOMBRE");

                BM_AdaptadorRegion = new SQLiteDataAdapter(BM_ComandoRegion);

                BM_DataSetRegion = new DataSet();
                BM_AdaptadorRegion.Fill(BM_DataSetRegion, "ESTADOREGION");

                foreach (DataRow LvrPais in BM_DataSetRegion.Tables["ESTADOREGION"].Rows)
                {
                    BK_Region = LvrPais["NOMBRE"].ToString();
                    BK_RegionLista.Add(BK_Region);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return BK_RegionLista;
        }


        public string Bm_Servicio_Listado()
        {

            BK_Servicio = new JsonBikeMessengerServicio();
            BK_ServicioLista = new List<JsonBikeMessengerServicio>();
            DataSet BM_DataSet;
            SQLiteDataAdapter BM_Adaptador;
            SQLiteCommand BM_Comando;

            LvrTablaHtml DocumentoHtml = new LvrTablaHtml();

            DocumentoHtml.CrearTexto("ENVIOS");
            DocumentoHtml.InicioDocumento();
            DocumentoHtml.AgregarTituloTabla("Listado de Envios");
            DocumentoHtml.AbrirEncabezado();
            DocumentoHtml.AgregarEncabezado("Nro de Envío");
            DocumentoHtml.AgregarEncabezado("Guia de Despacho");
            DocumentoHtml.AgregarEncabezado("Fecha de Ingreso");
            DocumentoHtml.AgregarEncabezado("Hora de Ingreso");
            DocumentoHtml.AgregarEncabezado("Cliente");
            DocumentoHtml.AgregarEncabezado("Mensajero");
            DocumentoHtml.AgregarEncabezado("Entrega");
            DocumentoHtml.AgregarEncabezado("Recepción");
            DocumentoHtml.CerrarEncabezado();

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM SERVICIOS");

                BM_Adaptador = new SQLiteDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };
                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "SERVICIOS");

                foreach (DataRow LvrServicio in BM_DataSet.Tables["SERVICIOS"].Rows)
                {
                    DocumentoHtml.AbrirFila();
                    DocumentoHtml.AgregarCampo(LvrServicio["NROENVIO"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrServicio["GUIADESPACHO"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrServicio["FECHA"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrServicio["HORA"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrServicio["CLIENTERUT"].ToString() + " - " + LvrServicio["CLIENTEDIGVER"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrServicio["MENSAJERORUT"].ToString() + " - " + LvrServicio["MENSAJERODIGVER"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrServicio["ENTREGA"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrServicio["RECEPCION"].ToString(), false);
                    DocumentoHtml.CerrarFila();
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }

            DocumentoHtml.FinDocumento();
            return DocumentoHtml.BufferHtml;
        }
    }
    public class ClaseServicioGrid
    {
        public string PATENTE { get; set; }
        public string TIPO { get; set; }
        public string MARCA { get; set; }
        public string MODELO { get; set; }
        public string CIUDAD { get; set; }
    }
}
