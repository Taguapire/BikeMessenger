using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BikeMessenger
{
    internal class Bm_Servicio_Database
    {
        private static SqlConnection BM_Conexion;
        private TransferVar BM_TrasferVar;

        private static JsonBikeMessengerServicio BK_Servicio;
        private static List<JsonBikeMessengerServicio> BK_ServicioLista;

        private string BM_CadenaConexion;

        public Bm_Servicio_Database()
        {
            BM_TrasferVar = new TransferVar();
            BM_TrasferVar.LeerDirectorio();
            BM_CadenaConexion = BM_TrasferVar.Directorio;
            BM_Conexion = new SqlConnection("Data Source=VASCON\\SQLEXPRESS;Initial Catalog=bikemessenger;User ID=bikemessenger; Password=Hola1974");
            BM_Conexion.Open();
        }

        // Busqueda por Muchos
        public List<JsonBikeMessengerServicio> BuscarServicio()
        {
            BK_Servicio = new JsonBikeMessengerServicio();
            BK_ServicioLista = new List<JsonBikeMessengerServicio>();
            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM SERVICIOS");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
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
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM SERVICIOS WHERE PENTALPHA = '" + pPENTALPHA + "' AND NROENVIO = '" + pNROENVIO + "'");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
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
                    BK_Servicio.MENSAJERORUT = LvrServicio["MENSAJERORUT"].ToString();
                    BK_Servicio.MENSAJERODIGVER = LvrServicio["MENSAJERODIGVER"].ToString();
                    BK_Servicio.RECURSOID = LvrServicio["RECURSOID"].ToString();
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
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;
            SqlCommandBuilder BM_Builder;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM SERVICIOS");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = true
                };

                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "SERVICIOS");

                DataRow LvrServicio = BM_DataSet.Tables["SERVICIOS"].NewRow();
                BM_Builder = new SqlCommandBuilder(BM_Adaptador);

                LvrServicio["PENTALPHA"] = aBK_Servicio.PENTALPHA;
                LvrServicio["NROENVIO"] = aBK_Servicio.NROENVIO;
                LvrServicio["GUIADESPACHO"] = aBK_Servicio.GUIADESPACHO;
                LvrServicio["FECHA"] = aBK_Servicio.FECHA;
                LvrServicio["HORA"] = aBK_Servicio.HORA;
                LvrServicio["CLIENTERUT"] = aBK_Servicio.CLIENTERUT;
                LvrServicio["CLIENTEDIGVER"] = aBK_Servicio.CLIENTEDIGVER;
                LvrServicio["MENSAJERORUT"] = aBK_Servicio.MENSAJERORUT;
                LvrServicio["MENSAJERODIGVER"] = aBK_Servicio.MENSAJERODIGVER;
                LvrServicio["RECURSOID"] = aBK_Servicio.RECURSOID;
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
                _ = BM_Builder.GetInsertCommand(true);
                _ = BM_Adaptador.Update(BM_DataSet, "SERVICIOS");
                _ = BM_Adaptador.InsertCommand;
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
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;
            SqlCommandBuilder BM_Builder;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;
                BM_Comando.CommandText = string.Format("SELECT * FROM SERVICIOS WHERE PENTALPHA = '" + mBK_Servicio.PENTALPHA + "' AND NROENVIO = '" + mBK_Servicio.NROENVIO + "'");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = true
                };

                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "SERVICIOS");

                DataRow LvrServicio = BM_DataSet.Tables["SERVICIOS"].Rows[0];

                BM_Builder = new SqlCommandBuilder(BM_Adaptador);
                LvrServicio["PENTALPHA"] = mBK_Servicio.PENTALPHA;
                LvrServicio["NROENVIO"] = mBK_Servicio.NROENVIO;
                LvrServicio["GUIADESPACHO"] = mBK_Servicio.GUIADESPACHO;
                LvrServicio["FECHA"] = mBK_Servicio.FECHA;
                LvrServicio["HORA"] = mBK_Servicio.HORA;
                LvrServicio["CLIENTERUT"] = mBK_Servicio.CLIENTERUT;
                LvrServicio["CLIENTEDIGVER"] = mBK_Servicio.CLIENTEDIGVER;
                LvrServicio["MENSAJERORUT"] = mBK_Servicio.MENSAJERORUT;
                LvrServicio["MENSAJERODIGVER"] = mBK_Servicio.MENSAJERODIGVER;
                LvrServicio["RECURSOID"] = mBK_Servicio.RECURSOID;
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
                _ = BM_Builder.GetUpdateCommand(true);
                _ = BM_Adaptador.Update(BM_DataSet, "SERVICIOS");
                _ = BM_Adaptador.UpdateCommand;
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
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("DELETE FROM SERVICIOS WHERE PENTALPHA = '" + pPENTALPHA + "' AND NROENVIO = '" + pNROENVIO + "'");
                _ = BM_Comando.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return false;
            }
            return true;
        }

        // Utilitarios

        public List<ClaseServicioGrid> BuscarGridServicios()
        {
            List<ClaseServicioGrid> GridLocalServiciosLista = new List<ClaseServicioGrid>();
            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM SERVICIOS_VISTA_GRID");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };
                BM_DataSet = new DataSet();
                _ = BM_Adaptador.Fill(BM_DataSet, "SERVICIOS_VISTA_GRID");

                foreach (DataRowView LvServicios in BM_DataSet.Tables["SERVICIOS_VISTA_GRID"].DefaultView)
                {
                    ClaseServicioGrid GridLocalServicios = new ClaseServicioGrid
                    {
                        ENVIO = LvServicios["NROENVIO"].ToString(),
                        FECHA = LvServicios["FECHA"].ToString(),
                        CLIENTERUT = LvServicios["CLIENTERUT"].ToString(),
                        CLIENTEDIGVER = LvServicios["CLIENTEDIGVER"].ToString(),
                        ENTREGA = LvServicios["ENTREGA"].ToString()
                    };
                    GridLocalServiciosLista.Add(GridLocalServicios);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return GridLocalServiciosLista;
        }


        public List<ClaseRecursoGrid> BuscarGridRecurso()
        {
            List<ClaseRecursoGrid> GridLocalRecursoLista = new List<ClaseRecursoGrid>();
            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM RECURSOS_VISTA_GRID");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };
                BM_DataSet = new DataSet();
                _ = BM_Adaptador.Fill(BM_DataSet, "RECURSOS_VISTA_GRID");

                foreach (DataRowView LvrRecurso in BM_DataSet.Tables["RECURSOS_VISTA_GRID"].DefaultView)
                {
                    ClaseRecursoGrid GridLocalRecurso = new ClaseRecursoGrid
                    {
                        PATENTE = LvrRecurso["PATENTE"].ToString(),
                        TIPO = LvrRecurso["TIPO"].ToString(),
                        MARCA = LvrRecurso["MARCA"].ToString(),
                        MODELO = LvrRecurso["MODELO"].ToString(),
                        RUTID = LvrRecurso["RUTID"].ToString(),
                        DIGVER = LvrRecurso["DIGVER"].ToString()
                    };
                    GridLocalRecursoLista.Add(GridLocalRecurso);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return GridLocalRecursoLista;
        }


        public List<ClasePersonalGrid> BuscarGridPersonal()
        {
            List<ClasePersonalGrid> GridLocalPersonalLista = new List<ClasePersonalGrid>();

            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM PERSONAL_VISTA_GRID");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };
                BM_DataSet = new DataSet();
                _ = BM_Adaptador.Fill(BM_DataSet, "PERSONAL_VISTA_GRID");

                foreach (DataRowView LvrPersonal in BM_DataSet.Tables["PERSONAL_VISTA_GRID"].DefaultView)
                {
                    ClasePersonalGrid GridLocalPersonal = new ClasePersonalGrid
                    {
                        RUTID = LvrPersonal["RUTID"].ToString(),
                        DIGVER = LvrPersonal["DIGVER"].ToString(),
                        APELLIDOS = LvrPersonal["APELLIDOS"].ToString(),
                        NOMBRES = LvrPersonal["NOMBRES"].ToString()
                    };
                    GridLocalPersonalLista.Add(GridLocalPersonal);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return GridLocalPersonalLista;
        }


        public List<ClaseClientesGrid> BuscarGridClientes()
        {
            List<ClaseClientesGrid> GridLocalClientesLista = new List<ClaseClientesGrid>();

            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT * FROM CLIENTES_VISTA_GRID");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };

                BM_DataSet = new DataSet();
                _ = BM_Adaptador.Fill(BM_DataSet, "CLIENTES_VISTA_GRID");

                foreach (DataRowView LvrClientes in BM_DataSet.Tables["CLIENTES_VISTA_GRID"].DefaultView)
                {
                    ClaseClientesGrid GridLocalClientes = new ClaseClientesGrid
                    {
                        RUTID = LvrClientes["RUTID"].ToString(),
                        DIGVER = LvrClientes["DIGVER"].ToString(),
                        NOMBRE = LvrClientes["NOMBRES"].ToString()
                    };

                    GridLocalClientesLista.Add(GridLocalClientes);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return GridLocalClientesLista;
        }

        public string Bm_BuscarNombreCliente(string pPENTALPHA, string pRUT, string pDIGVER)
        {
            string NombreCliente;
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT NOMBRE FROM CLIENTES WHERE PENTALPHA = '" + pPENTALPHA + "'  AND RUTID = '" + pRUT + "' AND DIGVER = '" + pDIGVER + "'");
                NombreCliente = BM_Comando.ExecuteReader(CommandBehavior.SingleResult).ToString();
                return NombreCliente;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return "No existe";
            }
        }

        public string Bm_BuscarNombreMensajero(string pPENTALPHA, string pRUT, string pDIGVER)
        {
            string NombrePersonal;
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT APELLIDOS ||','|| NOMBRE + FROM PERSONAL WHERE PENTALPHA = '" + pPENTALPHA + "'  AND RUTID = '" + pRUT + "' AND DIGVER = '" + pDIGVER + "'");
                NombrePersonal = BM_Comando.ExecuteReader(CommandBehavior.SingleResult).ToString();
                return NombrePersonal;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return "No existe";
            }
        }

        private string Bm_BuscarNombreRecurso(string pPENTALPHA, string pPATENTE)
        {
            string NombreRecurso;
            SqlCommand BM_Comando;

            try
            {
                BM_Comando = BM_Conexion.CreateCommand();
                BM_Comando.CommandText = string.Format("SELECT TIPO ||'-'|| MARCA ||'-'|| MODELO FROM RECURSOS WHERE PENTALPHA = '" + pPENTALPHA + "'  AND PATENTE = '" + pPATENTE + "'");
                NombreRecurso = BM_Comando.ExecuteReader(CommandBehavior.SingleResult).ToString();
                return NombreRecurso;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return "No existe";
            }
        }

        public List<string> GetPais()
        {
            List<string> BK_PaisLista = new List<string>();
            DataSet BM_DataSetPais;
            SqlDataAdapter BM_AdaptadorPais;
            SqlCommand BM_ComandoPais;
            string BK_Pais;

            try
            {
                BM_ComandoPais = BM_Conexion.CreateCommand();
                BM_ComandoPais.CommandText = string.Format("SELECT * FROM PAIS ORDER BY NOMBRE");

                BM_AdaptadorPais = new SqlDataAdapter(BM_ComandoPais);

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
            SqlDataAdapter BM_AdaptadorRegion;
            SqlCommand BM_ComandoRegion;
            string BK_Region;

            try
            {
                BM_ComandoRegion = BM_Conexion.CreateCommand();
                BM_ComandoRegion.CommandText = string.Format("SELECT * FROM ESTADOREGION ORDER BY NOMBRE");

                BM_AdaptadorRegion = new SqlDataAdapter(BM_ComandoRegion);

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
            SqlDataAdapter BM_AdaptadorComuna;
            SqlCommand BM_ComandoComuna;
            string BK_Comuna;

            try
            {
                BM_ComandoComuna = BM_Conexion.CreateCommand();
                BM_ComandoComuna.CommandText = string.Format("SELECT * FROM COMUNA ORDER BY NOMBRE");

                BM_AdaptadorComuna = new SqlDataAdapter(BM_ComandoComuna);

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
            List<string> BK_CiudadLista = new List<string>();
            DataSet BM_DataSetCiudad;
            SqlDataAdapter BM_AdaptadorCiudad;
            SqlCommand BM_ComandoCiudad;
            string BK_Ciudad;

            try
            {
                BM_ComandoCiudad = BM_Conexion.CreateCommand();
                BM_ComandoCiudad.CommandText = string.Format("SELECT * FROM CIUDAD ORDER BY NOMBRE");

                BM_AdaptadorCiudad = new SqlDataAdapter(BM_ComandoCiudad);

                BM_DataSetCiudad = new DataSet();
                BM_AdaptadorCiudad.Fill(BM_DataSetCiudad, "CIUDAD");

                foreach (DataRow LvrCiudad in BM_DataSetCiudad.Tables["CIUDAD"].Rows)
                {
                    BK_Ciudad = LvrCiudad["NOMBRE"].ToString();
                    BK_CiudadLista.Add(BK_Ciudad);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.InnerException.Message);
                return null;
            }
            return BK_CiudadLista;
        }


        public string Bm_Servicio_Listado()
        {

            BK_Servicio = new JsonBikeMessengerServicio();
            BK_ServicioLista = new List<JsonBikeMessengerServicio>();
            DataSet BM_DataSet;
            SqlDataAdapter BM_Adaptador;
            SqlCommand BM_Comando;

            LvrTablaHtml DocumentoHtml = new LvrTablaHtml();

            DocumentoHtml.CrearTexto("SERVICIOS");
            DocumentoHtml.InicioDocumento();
            DocumentoHtml.AgregarTituloTabla("Listado de Servicios");
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
                BM_Comando.CommandText = string.Format("SELECT * FROM SERVICIOS_VISTA_LISTADO");

                BM_Adaptador = new SqlDataAdapter(BM_Comando)
                {
                    AcceptChangesDuringFill = false
                };
                BM_DataSet = new DataSet();
                BM_Adaptador.Fill(BM_DataSet, "SERVICIOS_VISTA_LISTADO");

                foreach (DataRowView LvrServicio in BM_DataSet.Tables["SERVICIOS_VISTA_LISTADO"].DefaultView)
                {
                    DocumentoHtml.AbrirFila();
                    DocumentoHtml.AgregarCampo(LvrServicio["NROENVIO"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrServicio["GUIADESPACHO"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrServicio["FECHA"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrServicio["HORA"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrServicio["NOMBRE"].ToString(), false);
                    DocumentoHtml.AgregarCampo(LvrServicio["APELLIDOS"].ToString() + ", " + LvrServicio["NOMBRES"].ToString(), false);
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
        public string ENVIO { get; set; }
        public string FECHA { get; set; }
        public string CLIENTERUT { get; set; }
        public string CLIENTEDIGVER { get; set; }
        public string ENTREGA { get; set; }
    }
}
