using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SQLite;
using Newtonsoft.Json;

namespace BikeMessenger
{
    internal class Bm_Empresa_Database
    {

        // public SqliteFactory BM_DB;
        // Paso de Parametros Sqlite
        // ***************************************
        private static SqlConnection BM_Conexion;
        private static SQLiteConnection BM_ConexionLite;

        private TransferVar BM_TransferVar = new TransferVar();

        // Bases de Datos
        private static JsonBikeMessengerEmpresa BK_Empresa;
        private static List<JsonBikeMessengerEmpresa> BK_EmpresaLista;

        public Bm_Empresa_Database()
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return;
            }

            if (BM_TransferVar.SincronizarBaseLocal())
            {
                try
                {
                    BM_ConexionLite = new SQLiteConnection(BM_TransferVar.DIRECTORIO_BASE_LOCAL + "\\BikeMessenger.db");
                    //BM_ConexionLite.Open();
                }
                catch (SQLite.SQLiteException Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                }

            }
            else if (BM_TransferVar.SincronizarBaseSQLServer())
            {
                try
                {
                    BM_Conexion = new SqlConnection(BM_TransferVar.BM_Sql_String_Builder.ConnectionString);
                    BM_Conexion.Open();
                }
                catch (System.Data.SqlClient.SqlException Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                }
            }
        }

        // Busqueda
        public List<JsonBikeMessengerEmpresa> BuscarEmpresa(string pPENTALPHA)
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

            BK_Empresa = new JsonBikeMessengerEmpresa();
            BK_EmpresaLista = new List<JsonBikeMessengerEmpresa>();

            if (BM_TransferVar.SincronizarBaseLocal())
            {
                return null;
            }
            else if (BM_TransferVar.SincronizarBaseSQLServer())
            {
                DataSet BM_DataSet;
                SqlDataAdapter BM_Adaptador;
                SqlCommand BM_Comando;

                try
                {
                    BM_Comando = BM_Conexion.CreateCommand();
                    BM_Comando.CommandText = string.Format("SELECT * FROM EMPRESA WHERE PENTALPHA = '" + pPENTALPHA + "'");

                    BM_Adaptador = new SqlDataAdapter(BM_Comando)
                    {
                        AcceptChangesDuringFill = false
                    };
                    BM_DataSet = new DataSet();
                    _ = BM_Adaptador.Fill(BM_DataSet, "EMPRESA");

                    foreach (DataRow LvrEmpresa in BM_DataSet.Tables["EMPRESA"].Rows)
                    {
                        BK_Empresa.OPERACION = "CONSULTAR";
                        BK_Empresa.PENTALPHA = LvrEmpresa["PENTALPHA"].ToString();
                        BK_Empresa.RUTID = LvrEmpresa["RUTID"].ToString();
                        BK_Empresa.DIGVER = LvrEmpresa["DIGVER"].ToString();
                        BK_Empresa.NOMBRE = LvrEmpresa["NOMBRE"].ToString();
                        BK_Empresa.USUARIO = LvrEmpresa["USUARIO"].ToString();
                        BK_Empresa.CLAVE = LvrEmpresa["CLAVE"].ToString();
                        BK_Empresa.ACTIVIDAD1 = LvrEmpresa["ACTIVIDAD1"].ToString();
                        BK_Empresa.ACTIVIDAD2 = LvrEmpresa["ACTIVIDAD2"].ToString();
                        BK_Empresa.REPRESENTANTE1 = LvrEmpresa["REPRESENTANTE1"].ToString();
                        BK_Empresa.REPRESENTANTE2 = LvrEmpresa["REPRESENTANTE2"].ToString();
                        BK_Empresa.REPRESENTANTE3 = LvrEmpresa["REPRESENTANTE3"].ToString();
                        BK_Empresa.DOMICILIO1 = LvrEmpresa["DOMICILIO1"].ToString();
                        BK_Empresa.DOMICILIO2 = LvrEmpresa["DOMICILIO2"].ToString();
                        BK_Empresa.NUMERO = LvrEmpresa["NUMERO"].ToString();
                        BK_Empresa.PISO = LvrEmpresa["PISO"].ToString();
                        BK_Empresa.OFICINA = LvrEmpresa["OFICINA"].ToString();
                        BK_Empresa.CIUDAD = LvrEmpresa["CIUDAD"].ToString();
                        BK_Empresa.COMUNA = LvrEmpresa["COMUNA"].ToString();
                        BK_Empresa.ESTADOREGION = LvrEmpresa["ESTADOREGION"].ToString();
                        BK_Empresa.CODIGOPOSTAL = LvrEmpresa["CODIGOPOSTAL"].ToString();
                        BK_Empresa.PAIS = LvrEmpresa["PAIS"].ToString();
                        BK_Empresa.TELEFONO1 = LvrEmpresa["TELEFONO1"].ToString();
                        BK_Empresa.TELEFONO2 = LvrEmpresa["TELEFONO2"].ToString();
                        BK_Empresa.TELEFONO3 = LvrEmpresa["TELEFONO3"].ToString();
                        BK_Empresa.OBSERVACIONES = LvrEmpresa["OBSERVACIONES"].ToString();
                        BK_Empresa.LOGO = LvrEmpresa["LOGO"].ToString();
                        BK_Empresa.RESOPERACION = "OK";
                        BK_EmpresaLista.Add(BK_Empresa);
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return null;
                }
                return BK_EmpresaLista;
            }
            else if (BM_TransferVar.SincronizarWebPentalpha())
            {
                string LvrData;

                BK_Empresa = new JsonBikeMessengerEmpresa
                {
                    OPERACION = "CONSULTAR",
                    PENTALPHA = BM_TransferVar.PENTALPHA_ID,
                    LOGO = ""
                };

                BK_EmpresaLista = new List<JsonBikeMessengerEmpresa>
                {
                    BK_Empresa
                };
                try
                {
                    LvrData = JsonConvert.SerializeObject(BK_EmpresaLista);
                    LvrData = ProRegistroEmpresa("https://finanven.ddns.net", "443", "/Api/BikeMessengerEmpresa", LvrData);
                    if (LvrData != null)
                    {
                        BK_EmpresaLista = JsonConvert.DeserializeObject<List<JsonBikeMessengerEmpresa>>(LvrData);
                        return BK_EmpresaLista;
                    }
                }
                catch (System.NullReferenceException Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return null;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return null;
                }
                return null;
            }
            else if (BM_TransferVar.SincronizarWebPropio())
            {
                string LvrData;

                BK_Empresa = new JsonBikeMessengerEmpresa
                {
                    OPERACION = "CONSULTAR",
                    PENTALPHA = BM_TransferVar.PENTALPHA_ID,
                    LOGO = ""
                };

                BK_EmpresaLista = new List<JsonBikeMessengerEmpresa>
                {
                    BK_Empresa
                };

                try
                {
                    LvrData = JsonConvert.SerializeObject(BK_EmpresaLista);
                    LvrData = ProRegistroEmpresa(BM_TransferVar.BDREMOTACLIENTE, BM_TransferVar.BDREMOTACLIENTEPUERTO, "/Api/BikeMessengerEmpresa", LvrData);
                    if (LvrData != null)
                    {
                        BK_EmpresaLista = JsonConvert.DeserializeObject<List<JsonBikeMessengerEmpresa>>(LvrData);
                        return BK_EmpresaLista;
                    }
                }
                catch (System.NullReferenceException Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return null;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return null;
                }
                return null;
            }
            return null;
        }

        public bool AgregarEmpresa(JsonBikeMessengerEmpresa aBK_Empresa)
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return false;
            }

            if (BM_TransferVar.SincronizarBaseLocal())
            {
                return false;
            }
            else if (BM_TransferVar.SincronizarBaseSQLServer())
            {
                DataSet BM_DataSet;
                SqlDataAdapter BM_Adaptador;
                SqlCommand BM_Comando;
                SqlCommandBuilder BM_Builder;

                try
                {
                    BM_Comando = BM_Conexion.CreateCommand();
                    BM_Comando.CommandText = string.Format("SELECT * FROM EMPRESA");

                    BM_Adaptador = new SqlDataAdapter(BM_Comando)
                    {
                        AcceptChangesDuringFill = true
                    };
                    BM_DataSet = new DataSet();
                    _ = BM_Adaptador.Fill(BM_DataSet, "EMPRESA");

                    DataRow LvrEmpresa = BM_DataSet.Tables["EMPRESA"].NewRow();
                    BM_Builder = new SqlCommandBuilder(BM_Adaptador);

                    LvrEmpresa["PENTALPHA"] = aBK_Empresa.PENTALPHA;
                    LvrEmpresa["RUTID"] = aBK_Empresa.RUTID;
                    LvrEmpresa["DIGVER"] = aBK_Empresa.DIGVER;
                    LvrEmpresa["NOMBRE"] = aBK_Empresa.NOMBRE;
                    LvrEmpresa["USUARIO"] = aBK_Empresa.USUARIO;
                    LvrEmpresa["CLAVE"] = aBK_Empresa.CLAVE;
                    LvrEmpresa["ACTIVIDAD1"] = aBK_Empresa.ACTIVIDAD1;
                    LvrEmpresa["ACTIVIDAD2"] = aBK_Empresa.ACTIVIDAD2;
                    LvrEmpresa["REPRESENTANTE1"] = aBK_Empresa.REPRESENTANTE1;
                    LvrEmpresa["REPRESENTANTE2"] = aBK_Empresa.REPRESENTANTE2;
                    LvrEmpresa["REPRESENTANTE3"] = aBK_Empresa.REPRESENTANTE3;
                    LvrEmpresa["DOMICILIO1"] = aBK_Empresa.DOMICILIO1;
                    LvrEmpresa["DOMICILIO2"] = aBK_Empresa.DOMICILIO2;
                    LvrEmpresa["NUMERO"] = aBK_Empresa.NUMERO;
                    LvrEmpresa["PISO"] = aBK_Empresa.PISO;
                    LvrEmpresa["OFICINA"] = aBK_Empresa.OFICINA;
                    LvrEmpresa["CIUDAD"] = aBK_Empresa.CIUDAD;
                    LvrEmpresa["COMUNA"] = aBK_Empresa.COMUNA;
                    LvrEmpresa["ESTADOREGION"] = aBK_Empresa.ESTADOREGION;
                    LvrEmpresa["CODIGOPOSTAL"] = aBK_Empresa.CODIGOPOSTAL;
                    LvrEmpresa["PAIS"] = aBK_Empresa.PAIS;
                    LvrEmpresa["TELEFONO1"] = aBK_Empresa.TELEFONO1;
                    LvrEmpresa["TELEFONO2"] = aBK_Empresa.TELEFONO2;
                    LvrEmpresa["TELEFONO3"] = aBK_Empresa.TELEFONO3;
                    LvrEmpresa["OBSERVACIONES"] = aBK_Empresa.OBSERVACIONES;
                    LvrEmpresa["LOGO"] = aBK_Empresa.LOGO;
                    BM_DataSet.Tables["EMPRESA"].Rows.Add(LvrEmpresa);
                    _ = BM_Builder.GetInsertCommand(true);
                    _ = BM_Adaptador.Update(BM_DataSet, "EMPRESA");
                    _ = BM_Adaptador.InsertCommand;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return false;
                }
                return true;
            }
            else if (BM_TransferVar.SincronizarWebPentalpha())
            {
                string LvrData;

                aBK_Empresa.OPERACION = "AGREGAR";

                BK_EmpresaLista = new List<JsonBikeMessengerEmpresa>
                {
                    aBK_Empresa
                };

                try
                {
                    LvrData = JsonConvert.SerializeObject(BK_EmpresaLista);
                    LvrData = ProRegistroEmpresa("https://finanven.ddns.net", "443", "/Api/BikeMessengerEmpresa", LvrData);
                    if (LvrData != null)
                    {
                        BK_EmpresaLista = JsonConvert.DeserializeObject<List<JsonBikeMessengerEmpresa>>(LvrData);
                        if (BK_EmpresaLista != null && BK_EmpresaLista.Count > 0)
                        {
                            BK_Empresa = new JsonBikeMessengerEmpresa();
                            BK_Empresa = BK_EmpresaLista[0];
                            if (BK_Empresa.RESOPERACION == "OK")
                            {
                                return true;
                            }
                        }
                    }
                }
                catch (System.NullReferenceException Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return false;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return false;
                }
                return false;
            }
            else if (BM_TransferVar.SincronizarWebPropio())
            {
                string LvrData;

                aBK_Empresa.OPERACION = "AGREGAR";

                BK_EmpresaLista = new List<JsonBikeMessengerEmpresa>
                {
                    aBK_Empresa
                };

                try
                {
                    LvrData = JsonConvert.SerializeObject(BK_EmpresaLista);
                    LvrData = ProRegistroEmpresa(BM_TransferVar.BDREMOTACLIENTE, BM_TransferVar.BDREMOTACLIENTEPUERTO, "/Api/BikeMessengerEmpresa", LvrData);
                    if (LvrData != null)
                    {
                        BK_EmpresaLista = JsonConvert.DeserializeObject<List<JsonBikeMessengerEmpresa>>(LvrData);
                        if (BK_EmpresaLista != null && BK_EmpresaLista.Count > 0)
                        {
                            BK_Empresa = new JsonBikeMessengerEmpresa();
                            BK_Empresa = BK_EmpresaLista[0];
                            if (BK_Empresa.RESOPERACION == "OK")
                            {
                                return true;
                            }
                        }
                    }
                }
                catch (System.NullReferenceException Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return false;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return false;
                }
            }
            return false;
        }

        public bool ModificarEmpresa(JsonBikeMessengerEmpresa mBK_Empresa)
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return false;
            }

            if (BM_TransferVar.SincronizarBaseLocal())
            {
                return false;
            }
            else if (BM_TransferVar.SincronizarBaseSQLServer())
            {
                DataSet BM_DataSet;
                SqlDataAdapter BM_Adaptador;
                SqlCommand BM_Comando;
                SqlCommandBuilder BM_Builder;

                try
                {
                    BM_Comando = BM_Conexion.CreateCommand();
                    BM_Comando.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;
                    BM_Comando.CommandText = string.Format("SELECT * FROM EMPRESA WHERE PENTALPHA = '" + mBK_Empresa.PENTALPHA + "'");

                    BM_Adaptador = new SqlDataAdapter(BM_Comando)
                    {
                        AcceptChangesDuringUpdate = true,
                    };

                    BM_DataSet = new DataSet();
                    _ = BM_Adaptador.Fill(BM_DataSet, "EMPRESA");

                    DataRow LvrEmpresa = BM_DataSet.Tables["EMPRESA"].Rows[0];

                    BM_Builder = new SqlCommandBuilder(BM_Adaptador);

                    LvrEmpresa["PENTALPHA"] = mBK_Empresa.PENTALPHA;
                    LvrEmpresa["RUTID"] = mBK_Empresa.RUTID;
                    LvrEmpresa["DIGVER"] = mBK_Empresa.DIGVER;
                    LvrEmpresa["NOMBRE"] = mBK_Empresa.NOMBRE;
                    LvrEmpresa["USUARIO"] = mBK_Empresa.USUARIO;
                    LvrEmpresa["CLAVE"] = mBK_Empresa.CLAVE;
                    LvrEmpresa["ACTIVIDAD1"] = mBK_Empresa.ACTIVIDAD1;
                    LvrEmpresa["ACTIVIDAD2"] = mBK_Empresa.ACTIVIDAD2;
                    LvrEmpresa["REPRESENTANTE1"] = mBK_Empresa.REPRESENTANTE1;
                    LvrEmpresa["REPRESENTANTE2"] = mBK_Empresa.REPRESENTANTE2;
                    LvrEmpresa["REPRESENTANTE3"] = mBK_Empresa.REPRESENTANTE3;
                    LvrEmpresa["DOMICILIO1"] = mBK_Empresa.DOMICILIO1;
                    LvrEmpresa["DOMICILIO2"] = mBK_Empresa.DOMICILIO2;
                    LvrEmpresa["NUMERO"] = mBK_Empresa.NUMERO;
                    LvrEmpresa["PISO"] = mBK_Empresa.PISO;
                    LvrEmpresa["OFICINA"] = mBK_Empresa.OFICINA;
                    LvrEmpresa["CIUDAD"] = mBK_Empresa.CIUDAD;
                    LvrEmpresa["COMUNA"] = mBK_Empresa.COMUNA;
                    LvrEmpresa["ESTADOREGION"] = mBK_Empresa.ESTADOREGION;
                    LvrEmpresa["CODIGOPOSTAL"] = mBK_Empresa.CODIGOPOSTAL;
                    LvrEmpresa["PAIS"] = mBK_Empresa.PAIS;
                    LvrEmpresa["TELEFONO1"] = mBK_Empresa.TELEFONO1;
                    LvrEmpresa["TELEFONO2"] = mBK_Empresa.TELEFONO2;
                    LvrEmpresa["TELEFONO3"] = mBK_Empresa.TELEFONO3;
                    LvrEmpresa["OBSERVACIONES"] = mBK_Empresa.OBSERVACIONES;
                    LvrEmpresa["LOGO"] = mBK_Empresa.LOGO;
                    _ = BM_Builder.GetUpdateCommand(true);
                    _ = BM_Adaptador.Update(BM_DataSet, "EMPRESA");
                    _ = BM_Adaptador.UpdateCommand;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return false;
                }
                return true;
            }
            else if (BM_TransferVar.SincronizarWebPentalpha())
            {
                string LvrData;

                mBK_Empresa.OPERACION = "MODIFICAR";

                BK_EmpresaLista = new List<JsonBikeMessengerEmpresa>
                {
                    mBK_Empresa
                };

                try
                {
                    LvrData = JsonConvert.SerializeObject(BK_EmpresaLista);
                    LvrData = ProRegistroEmpresa("https://finanven.ddns.net", "443", "/Api/BikeMessengerEmpresa", LvrData);
                    if (LvrData != null)
                    {
                        BK_EmpresaLista = JsonConvert.DeserializeObject<List<JsonBikeMessengerEmpresa>>(LvrData);
                        if (BK_EmpresaLista != null && BK_EmpresaLista.Count > 0)
                        {
                            BK_Empresa = new JsonBikeMessengerEmpresa();
                            BK_Empresa = BK_EmpresaLista[0];
                            if (BK_Empresa.RESOPERACION == "OK")
                            {
                                return true;
                            }
                        }
                    }
                }
                catch (System.NullReferenceException Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return false;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return false;
                }
                return false;
            }
            else if (BM_TransferVar.SincronizarWebPropio())
            {
                string LvrData;

                mBK_Empresa.OPERACION = "MODIFICAR";

                BK_EmpresaLista = new List<JsonBikeMessengerEmpresa>
                {
                    mBK_Empresa
                };

                try
                {
                    LvrData = JsonConvert.SerializeObject(BK_EmpresaLista);
                    LvrData = ProRegistroEmpresa(BM_TransferVar.BDREMOTACLIENTE, BM_TransferVar.BDREMOTACLIENTEPUERTO, "/Api/BikeMessengerEmpresa", LvrData);
                    if (LvrData != null)
                    {
                        BK_EmpresaLista = JsonConvert.DeserializeObject<List<JsonBikeMessengerEmpresa>>(LvrData);
                        if (BK_EmpresaLista != null && BK_EmpresaLista.Count > 0)
                        {
                            BK_Empresa = new JsonBikeMessengerEmpresa();
                            BK_Empresa = BK_EmpresaLista[0];
                            if (BK_Empresa.RESOPERACION == "OK")
                            {
                                return true;
                            }
                        }
                    }
                }
                catch (System.NullReferenceException Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return false;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return false;
                }
            }
            return false;
        }

        public bool BorrarEmpresa(string pPENTALPHA)
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return false;
            }

            BK_Empresa = new JsonBikeMessengerEmpresa();
            BK_EmpresaLista = new List<JsonBikeMessengerEmpresa>();

            if (BM_TransferVar.SincronizarBaseLocal())
            {
                return false;
            }
            else if (BM_TransferVar.SincronizarBaseSQLServer())
            {
                SqlCommand BM_Comando;
                try
                {
                    BM_Comando = BM_Conexion.CreateCommand();
                    BM_Comando.CommandText = string.Format("DELETE FROM EMPRESA WHERE PENTALPHA = '" + pPENTALPHA + "'");
                    _ = BM_Comando.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return false;
                }
                return true;
            }
            else if (BM_TransferVar.SincronizarWebPentalpha())
            {
                string LvrData;

                BK_Empresa.OPERACION = "BORRAR";
                BK_Empresa.PENTALPHA = pPENTALPHA;
                BK_Empresa.LOGO = "";

                BK_EmpresaLista = new List<JsonBikeMessengerEmpresa>
                {
                    BK_Empresa
                };

                try
                {
                    LvrData = JsonConvert.SerializeObject(BK_EmpresaLista);
                    LvrData = ProRegistroEmpresa("https://finanven.ddns.net", "443", "/Api/BikeMessengerEmpresa", LvrData);
                    if (LvrData != null)
                    {
                        BK_EmpresaLista = JsonConvert.DeserializeObject<List<JsonBikeMessengerEmpresa>>(LvrData);
                        if (BK_EmpresaLista != null && BK_EmpresaLista.Count > 0)
                        {
                            BK_Empresa = new JsonBikeMessengerEmpresa();
                            BK_Empresa = BK_EmpresaLista[0];
                            if (BK_Empresa.RESOPERACION == "OK")
                            {
                                return true;
                            }
                        }
                    }
                }
                catch (System.NullReferenceException Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return false;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return false;
                }
                return false;
            }
            else if (BM_TransferVar.SincronizarWebPropio())
            {
                string LvrData;

                BK_Empresa.OPERACION = "BORRAR";
                BK_Empresa.PENTALPHA = pPENTALPHA;
                BK_Empresa.LOGO = "";

                BK_EmpresaLista = new List<JsonBikeMessengerEmpresa>
                {
                    BK_Empresa
                };

                try
                {
                    LvrData = JsonConvert.SerializeObject(BK_EmpresaLista);
                    LvrData = ProRegistroEmpresa("https://finanven.ddns.net", "443", "/Api/BikeMessengerEmpresa", LvrData);
                    if (LvrData != null)
                    {
                        BK_EmpresaLista = JsonConvert.DeserializeObject<List<JsonBikeMessengerEmpresa>>(LvrData);
                        if (BK_EmpresaLista != null && BK_EmpresaLista.Count > 0)
                        {
                            BK_Empresa = new JsonBikeMessengerEmpresa();
                            BK_Empresa = BK_EmpresaLista[0];
                            if (BK_Empresa.RESOPERACION == "OK")
                            {
                                return true;
                            }
                        }
                    }
                }
                catch (System.NullReferenceException Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return false;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return false;
                }
            }
            return false;
        }

        public List<string> GetPais()
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

            List<string> BK_PaisLista = new List<string>();

            if (BM_TransferVar.SincronizarBaseLocal())
            {
                return null;
            }
            else if (BM_TransferVar.SincronizarBaseSQLServer())
            {
                DataSet BM_DataSetPais;
                SqlDataAdapter BM_AdaptadorPais;
                SqlCommand BM_ComandoPais;
                try
                {
                    BM_ComandoPais = BM_Conexion.CreateCommand();
                    BM_ComandoPais.CommandText = string.Format("SELECT * FROM PAIS ORDER BY NOMBRE");

                    BM_AdaptadorPais = new SqlDataAdapter(BM_ComandoPais);

                    BM_DataSetPais = new DataSet();
                    _ = BM_AdaptadorPais.Fill(BM_DataSetPais, "PAIS");

                    foreach (DataRow LvrPais in BM_DataSetPais.Tables["PAIS"].Rows)
                    {
                        string BK_Pais = LvrPais["NOMBRE"].ToString();
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
            else if (BM_TransferVar.SincronizarWebPentalpha())
            {
                return null;
            }
            else if (BM_TransferVar.SincronizarWebPropio())
            {
                return null;
            }
            return null;
        }

        public List<string> GetRegion()
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

            List<string> BK_RegionLista = new List<string>();

            if (BM_TransferVar.SincronizarBaseLocal())
            {
                return null;
            }
            else if (BM_TransferVar.SincronizarBaseSQLServer())
            {
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
                    _ = BM_AdaptadorRegion.Fill(BM_DataSetRegion, "ESTADOREGION");

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
            else if (BM_TransferVar.SincronizarWebPentalpha())
            {
                return null;
            }
            else if (BM_TransferVar.SincronizarWebPropio())
            {
                return null;
            }
            return null;
        }

        public List<string> GetComuna()
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

            List<string> BK_ComunaLista = new List<string>();

            if (BM_TransferVar.SincronizarBaseLocal())
            {
                return null;
            }
            else if (BM_TransferVar.SincronizarBaseSQLServer())
            {
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
                    _ = BM_AdaptadorComuna.Fill(BM_DataSetComuna, "COMUNA");

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
            else if (BM_TransferVar.SincronizarWebPentalpha())
            {
                return null;
            }
            else if (BM_TransferVar.SincronizarWebPropio())
            {
                return null;
            }
            return null;
        }

        public List<string> GetCiudad()
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

            List<string> BK_CiudadLista = new List<string>();

            if (BM_TransferVar.SincronizarBaseLocal())
            {
                return null;
            }
            else if (BM_TransferVar.SincronizarBaseSQLServer())
            {
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
                    _ = BM_AdaptadorCiudad.Fill(BM_DataSetCiudad, "CIUDAD");

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
            else if (BM_TransferVar.SincronizarWebPentalpha())
            {
                return null;
            }
            else if (BM_TransferVar.SincronizarWebPropio())
            {
                return null;
            }
            return null;
        }

        //**************************************************
        // Ejecuta operacion de registro de empresa
        //**************************************************
        private string ProRegistroEmpresa(string pHttp, string pPort, string pController, string pPData)
        {
            string LvrPRecibirServer = "";
            string LvrPData = pPData;
            string LvrStringHttp = pHttp; // "https://finanven.ddns.net";
            string LvrStringPort = pPort; // "443";
            string LvrStringController = pController; // "/Api/BikeMessengerEmpresa";
            string LvrParametros;
            // LvrInternet LvrBKInternet;
            LvrInternet LvrBKInternet;


            // Llenar estructura Json
            // CopiarMemoriaEnJson(pTipoOperacion);

            // Proceso Serializar

            //EnviarJsonEmpresaArray = new List<JsonBikeMessengerEmpresa>();
            //EnviarJsonEmpresaArray.Add(EnviarJsonEmpresa);
            //LvrPData = JsonConvert.SerializeObject(EnviarJsonEmpresaArray);

            // Preparar Parametros
            LvrParametros = LvrPData;

            for (int i = 1; i < 5; i++)
            {
                LvrBKInternet = new LvrInternet();
                LvrBKInternet.LvrInetPOST(LvrStringHttp, LvrStringPort, LvrStringController, LvrParametros);
                LvrPRecibirServer = LvrBKInternet.LvrResultadoWeb;
                if (LvrPRecibirServer != "ERROR" && LvrPRecibirServer != "" && LvrPRecibirServer != null)
                {
                    break;
                }
            }

            if (LvrPRecibirServer != "ERROR" && LvrPRecibirServer != "" && LvrPRecibirServer != null)
            {
                // Procesar primer servidor
                //RecibirJsonEmpresaArray = JsonConvert.DeserializeObject<List<JsonBikeMessengerEmpresa>>(LvrPRecibirServer); // resp será el string JSON a deserializa
                //RecibirJsonEmpresa = RecibirJsonEmpresaArray[0];

                return LvrPRecibirServer;

            }
            return null;
        }
    }
}