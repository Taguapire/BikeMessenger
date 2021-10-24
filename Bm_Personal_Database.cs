using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SQLite;
using Newtonsoft.Json;

namespace BikeMessenger
{
    internal class Bm_Personal_Database
    {

        // public SqliteFactory BM_DB;
        // Paso de Parametros Sqlite
        // ***************************************
        private static SqlConnection BM_Conexion;
        private static SQLiteConnection BM_ConexionLite;

        private TransferVar BM_TransferVar = new TransferVar();

        private static JsonBikeMessengerPersonal BK_Personal;
        private static List<JsonBikeMessengerPersonal> BK_PersonalLista;

        public Bm_Personal_Database()
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

        // Busqueda por Muchos
        public List<JsonBikeMessengerPersonal> BuscarPersonal()
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

            BK_Personal = new JsonBikeMessengerPersonal();
            BK_PersonalLista = new List<JsonBikeMessengerPersonal>();

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
                    BM_Comando.CommandText = string.Format("SELECT * FROM PERSONAL");

                    BM_Adaptador = new SqlDataAdapter(BM_Comando)
                    {
                        AcceptChangesDuringFill = false
                    };

                    BM_DataSet = new DataSet();
                    BM_Adaptador.Fill(BM_DataSet, "PERSONAL");

                    foreach (DataRow LvrPersonal in BM_DataSet.Tables["PERSONAL"].Rows)
                    {
                        BK_Personal = new JsonBikeMessengerPersonal();
                        BK_Personal.PENTALPHA = LvrPersonal["PENTALPHA"].ToString();
                        BK_Personal.RUTID = LvrPersonal["RUTID"].ToString();
                        BK_Personal.DIGVER = LvrPersonal["DIGVER"].ToString();
                        BK_Personal.APELLIDOS = LvrPersonal["APELLIDOS"].ToString();
                        BK_Personal.NOMBRES = LvrPersonal["NOMBRES"].ToString();
                        BK_Personal.TELEFONO1 = LvrPersonal["TELEFONO1"].ToString();
                        BK_Personal.TELEFONO2 = LvrPersonal["TELEFONO2"].ToString();
                        BK_Personal.EMAIL = LvrPersonal["EMAIL"].ToString();
                        BK_Personal.AUTORIZACION = LvrPersonal["AUTORIZACION"].ToString();
                        BK_Personal.CARGO = LvrPersonal["CARGO"].ToString();
                        BK_Personal.DOMICILIO = LvrPersonal["DOMICILIO"].ToString();
                        BK_Personal.NUMERO = LvrPersonal["NUMERO"].ToString();
                        BK_Personal.PISO = LvrPersonal["PISO"].ToString();
                        BK_Personal.DPTO = LvrPersonal["DPTO"].ToString();
                        BK_Personal.CODIGOPOSTAL = LvrPersonal["CODIGOPOSTAL"].ToString();
                        BK_Personal.CIUDAD = LvrPersonal["CIUDAD"].ToString();
                        BK_Personal.COMUNA = LvrPersonal["COMUNA"].ToString();
                        BK_Personal.REGION = LvrPersonal["REGION"].ToString();
                        BK_Personal.PAIS = LvrPersonal["PAIS"].ToString();
                        BK_Personal.OBSERVACIONES = LvrPersonal["OBSERVACIONES"].ToString();
                        BK_Personal.FOTO = LvrPersonal["FOTO"].ToString();
                        BK_PersonalLista.Add(BK_Personal);
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return null;
                }
                return BK_PersonalLista;
            }
            else if (BM_TransferVar.SincronizarWebPentalpha())
            {
                string LvrData;
                BK_Personal = new JsonBikeMessengerPersonal

                {
                    OPERACION = "CONSULTAR",
                    PENTALPHA = BM_TransferVar.PENTALPHA_ID,
                    FOTO = ""
                };

                BK_PersonalLista = new List<JsonBikeMessengerPersonal>
                {
                    BK_Personal
                };

                LvrData = JsonConvert.SerializeObject(BK_PersonalLista);
                LvrData = ProRegistroPersonal("https://finanven.ddns.net", "443", "/Api/BikeMessengerPersonal", LvrData);
                if (LvrData != null)
                {
                    BK_PersonalLista = JsonConvert.DeserializeObject<List<JsonBikeMessengerPersonal>>(LvrData);
                    return BK_PersonalLista;
                }
                return null;
            }
            else if (BM_TransferVar.SincronizarWebPropio())
            {
                return null;
            }
            return null;
        }

        public List<JsonBikeMessengerPersonal> BuscarPersonal(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

            BK_Personal = new JsonBikeMessengerPersonal();
            BK_PersonalLista = new List<JsonBikeMessengerPersonal>();

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
                    BM_Comando.CommandText = string.Format("SELECT * FROM PERSONAL WHERE PENTALPHA = '" + pPENTALPHA + "'  AND RUTID = '" + pRUTID + "' AND DIGVER = '" + pDIGVER + "'");

                    BM_Adaptador = new SqlDataAdapter(BM_Comando)
                    {
                        AcceptChangesDuringFill = false
                    };
                    BM_DataSet = new DataSet();
                    BM_Adaptador.Fill(BM_DataSet, "PERSONAL");

                    foreach (DataRow LvrPersonal in BM_DataSet.Tables["PERSONAL"].Rows)
                    {
                        BK_Personal = new JsonBikeMessengerPersonal();
                        BK_Personal.PENTALPHA = LvrPersonal["PENTALPHA"].ToString();
                        BK_Personal.RUTID = LvrPersonal["RUTID"].ToString();
                        BK_Personal.DIGVER = LvrPersonal["DIGVER"].ToString();
                        BK_Personal.APELLIDOS = LvrPersonal["APELLIDOS"].ToString();
                        BK_Personal.NOMBRES = LvrPersonal["NOMBRES"].ToString();
                        BK_Personal.TELEFONO1 = LvrPersonal["TELEFONO1"].ToString();
                        BK_Personal.TELEFONO2 = LvrPersonal["TELEFONO2"].ToString();
                        BK_Personal.EMAIL = LvrPersonal["EMAIL"].ToString();
                        BK_Personal.AUTORIZACION = LvrPersonal["AUTORIZACION"].ToString();
                        BK_Personal.CARGO = LvrPersonal["CARGO"].ToString();
                        BK_Personal.DOMICILIO = LvrPersonal["DOMICILIO"].ToString();
                        BK_Personal.NUMERO = LvrPersonal["NUMERO"].ToString();
                        BK_Personal.PISO = LvrPersonal["PISO"].ToString();
                        BK_Personal.DPTO = LvrPersonal["DPTO"].ToString();
                        BK_Personal.CODIGOPOSTAL = LvrPersonal["CODIGOPOSTAL"].ToString();
                        BK_Personal.CIUDAD = LvrPersonal["CIUDAD"].ToString();
                        BK_Personal.COMUNA = LvrPersonal["COMUNA"].ToString();
                        BK_Personal.REGION = LvrPersonal["REGION"].ToString();
                        BK_Personal.PAIS = LvrPersonal["PAIS"].ToString();
                        BK_Personal.OBSERVACIONES = LvrPersonal["OBSERVACIONES"].ToString();
                        BK_Personal.FOTO = LvrPersonal["FOTO"].ToString();
                        BK_PersonalLista.Add(BK_Personal);
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return null;
                }
                return BK_PersonalLista;
            }
            else if (BM_TransferVar.SincronizarWebPentalpha())
            {
                string LvrData;
                BK_Personal = new JsonBikeMessengerPersonal

                {
                    OPERACION = "CONSULTAR",
                    PENTALPHA = pPENTALPHA,
                    RUTID = pRUTID,
                    DIGVER = pDIGVER,
                    FOTO = ""
                };

                BK_PersonalLista = new List<JsonBikeMessengerPersonal>
                {
                    BK_Personal
                };

                LvrData = JsonConvert.SerializeObject(BK_PersonalLista);
                LvrData = ProRegistroPersonal("https://finanven.ddns.net", "443", "/Api/BikeMessengerPersonal", LvrData);
                if (LvrData != null)
                {
                    BK_PersonalLista = JsonConvert.DeserializeObject<List<JsonBikeMessengerPersonal>>(LvrData);
                    return BK_PersonalLista;
                }
                return null;
            }
            else if (BM_TransferVar.SincronizarWebPropio())
            {
                string LvrData;
                return null;
            }
            return null;
        }


        public bool AgregarPersonal(JsonBikeMessengerPersonal aBK_Personal)
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return false;
            }

            BK_Personal = new JsonBikeMessengerPersonal();
            BK_PersonalLista = new List<JsonBikeMessengerPersonal>();

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
                    BM_Comando.CommandText = string.Format("SELECT * FROM PERSONAL");

                    BM_Adaptador = new SqlDataAdapter(BM_Comando)
                    {
                        AcceptChangesDuringFill = true
                    };

                    BM_DataSet = new DataSet();
                    _ = BM_Adaptador.Fill(BM_DataSet, "PERSONAL");

                    DataRow LvrPersonal = BM_DataSet.Tables["PERSONAL"].NewRow();
                    BM_Builder = new SqlCommandBuilder(BM_Adaptador);

                    LvrPersonal["PENTALPHA"] = aBK_Personal.PENTALPHA;
                    LvrPersonal["RUTID"] = aBK_Personal.RUTID;
                    LvrPersonal["DIGVER"] = aBK_Personal.DIGVER;
                    LvrPersonal["APELLIDOS"] = aBK_Personal.APELLIDOS;
                    LvrPersonal["NOMBRES"] = aBK_Personal.NOMBRES;
                    LvrPersonal["TELEFONO1"] = aBK_Personal.TELEFONO1;
                    LvrPersonal["TELEFONO2"] = aBK_Personal.TELEFONO2;
                    LvrPersonal["EMAIL"] = aBK_Personal.EMAIL;
                    LvrPersonal["AUTORIZACION"] = aBK_Personal.AUTORIZACION;
                    LvrPersonal["CARGO"] = aBK_Personal.CARGO;
                    LvrPersonal["DOMICILIO"] = aBK_Personal.DOMICILIO;
                    LvrPersonal["NUMERO"] = aBK_Personal.NUMERO;
                    LvrPersonal["PISO"] = aBK_Personal.PISO;
                    LvrPersonal["DPTO"] = aBK_Personal.DPTO;
                    LvrPersonal["CODIGOPOSTAL"] = aBK_Personal.CODIGOPOSTAL;
                    LvrPersonal["CIUDAD"] = aBK_Personal.CIUDAD;
                    LvrPersonal["COMUNA"] = aBK_Personal.COMUNA;
                    LvrPersonal["REGION"] = aBK_Personal.REGION;
                    LvrPersonal["PAIS"] = aBK_Personal.PAIS;
                    LvrPersonal["OBSERVACIONES"] = aBK_Personal.OBSERVACIONES;
                    LvrPersonal["FOTO"] = aBK_Personal.FOTO;
                    BM_DataSet.Tables["PERSONAL"].Rows.Add(LvrPersonal);
                    _ = BM_Builder.GetInsertCommand(true);
                    _ = BM_Adaptador.Update(BM_DataSet, "PERSONAL");
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

                aBK_Personal.OPERACION = "AGREGAR";

                BK_PersonalLista = new List<JsonBikeMessengerPersonal>
                {
                    aBK_Personal
                };

                try
                {
                    LvrData = JsonConvert.SerializeObject(BK_PersonalLista);
                    LvrData = ProRegistroPersonal("https://finanven.ddns.net", "443", "/Api/BikeMessengerPersonal", LvrData);
                    if (LvrData != null)
                    {
                        BK_PersonalLista = JsonConvert.DeserializeObject<List<JsonBikeMessengerPersonal>>(LvrData);
                        if (BK_PersonalLista != null && BK_PersonalLista.Count > 0)
                        {
                            BK_Personal = new JsonBikeMessengerPersonal();
                            BK_Personal = BK_PersonalLista[0];
                            if (BK_Personal.RESOPERACION == "OK")
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
            else if (BM_TransferVar.SincronizarWebPropio())
            {
                return false;
            }
            return false;
        }


        public bool ModificarPersonal(JsonBikeMessengerPersonal mBK_Personal)
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
                    BM_Comando.CommandText = string.Format("SELECT * FROM PERSONAL WHERE PENTALPHA = '" + mBK_Personal.PENTALPHA + "' AND RUTID = '" + mBK_Personal.RUTID + "' AND DIGVER = '" + mBK_Personal.DIGVER + "'");

                    BM_Adaptador = new SqlDataAdapter(BM_Comando)
                    {
                        AcceptChangesDuringFill = true
                    };

                    BM_DataSet = new DataSet();
                    _ = BM_Adaptador.Fill(BM_DataSet, "PERSONAL");

                    DataRow LvrPersonal = BM_DataSet.Tables["PERSONAL"].Rows[0];

                    BM_Builder = new SqlCommandBuilder(BM_Adaptador);

                    LvrPersonal["PENTALPHA"] = mBK_Personal.PENTALPHA;
                    LvrPersonal["RUTID"] = mBK_Personal.RUTID;
                    LvrPersonal["DIGVER"] = mBK_Personal.DIGVER;
                    LvrPersonal["APELLIDOS"] = mBK_Personal.APELLIDOS;
                    LvrPersonal["NOMBRES"] = mBK_Personal.NOMBRES;
                    LvrPersonal["TELEFONO1"] = mBK_Personal.TELEFONO1;
                    LvrPersonal["TELEFONO2"] = mBK_Personal.TELEFONO2;
                    LvrPersonal["EMAIL"] = mBK_Personal.EMAIL;
                    LvrPersonal["AUTORIZACION"] = mBK_Personal.AUTORIZACION;
                    LvrPersonal["CARGO"] = mBK_Personal.CARGO;
                    LvrPersonal["DOMICILIO"] = mBK_Personal.DOMICILIO;
                    LvrPersonal["NUMERO"] = mBK_Personal.NUMERO;
                    LvrPersonal["PISO"] = mBK_Personal.PISO;
                    LvrPersonal["DPTO"] = mBK_Personal.DPTO;
                    LvrPersonal["CODIGOPOSTAL"] = mBK_Personal.CODIGOPOSTAL;
                    LvrPersonal["CIUDAD"] = mBK_Personal.CIUDAD;
                    LvrPersonal["COMUNA"] = mBK_Personal.COMUNA;
                    LvrPersonal["REGION"] = mBK_Personal.REGION;
                    LvrPersonal["PAIS"] = mBK_Personal.PAIS;
                    LvrPersonal["OBSERVACIONES"] = mBK_Personal.OBSERVACIONES;
                    LvrPersonal["FOTO"] = mBK_Personal.FOTO;
                    _ = BM_Builder.GetUpdateCommand(true);
                    _ = BM_Adaptador.Update(BM_DataSet, "PERSONAL");
                    _ = BM_Adaptador.UpdateCommand;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                    Console.WriteLine(Ex.InnerException.Message);
                    return false;
                }
                return true;
            }
            else if (BM_TransferVar.SincronizarWebPentalpha())
            {
                string LvrData;

                mBK_Personal.OPERACION = "MODIFICAR";

                BK_PersonalLista = new List<JsonBikeMessengerPersonal>
                {
                    mBK_Personal
                };

                LvrData = JsonConvert.SerializeObject(BK_PersonalLista);
                LvrData = ProRegistroPersonal("https://finanven.ddns.net", "443", "/Api/BikeMessengerPersonal", LvrData);
                BK_PersonalLista = JsonConvert.DeserializeObject<List<JsonBikeMessengerPersonal>>(LvrData);
                if (BK_PersonalLista != null && BK_PersonalLista.Count > 0)
                {
                    BK_Personal = new JsonBikeMessengerPersonal();
                    BK_Personal = BK_PersonalLista[0];
                    if (BK_Personal.RESOPERACION == "OK")
                    {
                        return true;
                    }
                }
                return false;
            }
            else if (BM_TransferVar.SincronizarWebPropio())
            {
                return false;
            }
            return false;
        }

        public bool BorrarPersonal(string pPENTALPHA, string pRUTID, string pDIGVER)
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return false;
            }

            BK_Personal = new JsonBikeMessengerPersonal();
            BK_PersonalLista = new List<JsonBikeMessengerPersonal>();

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
                    BM_Comando.CommandText = string.Format("DELETE FROM PERSONAL WHERE PENTALPHA = '" + pPENTALPHA + "' AND RUTID = '" + pRUTID + "' AND DIGVER = '" + pDIGVER + "'");
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

                BK_Personal.OPERACION = "BORRAR";
                BK_Personal.PENTALPHA = pPENTALPHA;
                BK_Personal.FOTO = "";

                BK_PersonalLista = new List<JsonBikeMessengerPersonal>
                {
                    BK_Personal
                };

                LvrData = JsonConvert.SerializeObject(BK_PersonalLista);
                LvrData = ProRegistroPersonal("https://finanven.ddns.net", "443", "/Api/BikeMessengerPersonal", LvrData);
                if (LvrData != null)
                {
                    BK_PersonalLista = JsonConvert.DeserializeObject<List<JsonBikeMessengerPersonal>>(LvrData);
                    if (BK_PersonalLista != null && BK_PersonalLista.Count > 0)
                    {
                        BK_Personal = new JsonBikeMessengerPersonal();
                        BK_Personal = BK_PersonalLista[0];
                        if (BK_Personal.RESOPERACION == "OK")
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            else if (BM_TransferVar.SincronizarWebPropio())
            {
                return false;
            }
            return false;
        }


        public List<ClasePersonalGrid> BuscarGridPersonal()
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

            List<ClasePersonalGrid> GridLocalPersonalLista = new List<ClasePersonalGrid>();

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
                string BK_Pais;

                try
                {
                    BM_ComandoPais = BM_Conexion.CreateCommand();
                    BM_ComandoPais.CommandText = string.Format("SELECT * FROM PAIS ORDER BY NOMBRE");

                    BM_AdaptadorPais = new SqlDataAdapter(BM_ComandoPais);

                    BM_DataSetPais = new DataSet();
                    _ = BM_AdaptadorPais.Fill(BM_DataSetPais, "PAIS");

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

        public string Bm_Personal_Listado()
        {
            if (BM_TransferVar.ESTADOPARAMETROS == "NADA")
            {
                return null;
            }

            BK_Personal = new JsonBikeMessengerPersonal();
            BK_PersonalLista = new List<JsonBikeMessengerPersonal>();

            LvrTablaHtml DocumentoHtml = new LvrTablaHtml();

            DocumentoHtml.CrearTexto("PERSONAL");
            DocumentoHtml.InicioDocumento();
            DocumentoHtml.AgregarTituloTabla("Listado de Personal");
            DocumentoHtml.AbrirEncabezado();
            DocumentoHtml.AgregarEncabezado("RUT-DIGVER");
            DocumentoHtml.AgregarEncabezado("APELLIDOS");
            DocumentoHtml.AgregarEncabezado("NOMBRES");
            DocumentoHtml.AgregarEncabezado("TELEFONO 1");
            DocumentoHtml.AgregarEncabezado("TELEFONO 2");
            DocumentoHtml.AgregarEncabezado("EMAIL");
            DocumentoHtml.AgregarEncabezado("AUTORIZACION");
            DocumentoHtml.AgregarEncabezado("CARGO");
            DocumentoHtml.AgregarEncabezado("CIUDAD");
            DocumentoHtml.CerrarEncabezado();

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
                    BM_Comando.CommandText = string.Format("SELECT * FROM PERSONAL_VISTA_LISTADO");

                    BM_Adaptador = new SqlDataAdapter(BM_Comando)
                    {
                        AcceptChangesDuringFill = false
                    };
                    BM_DataSet = new DataSet();
                    _ = BM_Adaptador.Fill(BM_DataSet, "PERSONAL_VISTA_LISTADO");

                    foreach (DataRowView LvrPersonal in BM_DataSet.Tables["PERSONAL_VISTA_LISTADO"].DefaultView)
                    {
                        DocumentoHtml.AbrirFila();
                        DocumentoHtml.AgregarCampo(LvrPersonal["RUTID"].ToString() + "-" + LvrPersonal["DIGVER"].ToString(), false);
                        DocumentoHtml.AgregarCampo(LvrPersonal["APELLIDOS"].ToString(), false);
                        DocumentoHtml.AgregarCampo(LvrPersonal["NOMBRES"].ToString(), false);
                        DocumentoHtml.AgregarCampo(LvrPersonal["TELEFONO1"].ToString(), false);
                        DocumentoHtml.AgregarCampo(LvrPersonal["TELEFONO2"].ToString(), false);
                        DocumentoHtml.AgregarCampo(LvrPersonal["EMAIL"].ToString(), false);
                        DocumentoHtml.AgregarCampo(LvrPersonal["AUTORIZACION"].ToString(), false);
                        DocumentoHtml.AgregarCampo(LvrPersonal["CARGO"].ToString(), false);
                        DocumentoHtml.AgregarCampo(LvrPersonal["CIUDAD"].ToString(), false);
                        DocumentoHtml.CerrarFila();
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                    return null;
                }
            }
            else if (BM_TransferVar.SincronizarWebPentalpha())
            {
                return null;
            }
            else if (BM_TransferVar.SincronizarWebPropio())
            {
                return null;
            }

            DocumentoHtml.FinDocumento();
            return DocumentoHtml.BufferHtml;
        }

        //**************************************************
        // Ejecuta operacion de registro de empresa
        //**************************************************
        private string ProRegistroPersonal(string pHttp, string pPort, string pController, string pPData)
        {
            string LvrPRecibirServer = "";
            string LvrPData = pPData;
            string LvrStringHttp = pHttp; // "https://finanven.ddns.net";
            string LvrStringPort = pPort; // "443";
            string LvrStringController = pController; // "/Api/BikeMessengerPersonal";
            string LvrParametros;
            LvrInternet LvrBKInternet;


            // Llenar estructura Json
            // CopiarMemoriaEnJson(pTipoOperacion);

            // Proceso Serializar

            //EnviarJsonPersonalArray = new List<JsonBikeMessengerPersonal>();
            //EnviarJsonPersonalArray.Add(EnviarJsonPersonal);
            //LvrPData = JsonConvert.SerializeObject(EnviarJsonPersonalArray);

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
                //RecibirJsonPersonalArray = JsonConvert.DeserializeObject<List<JsonBikeMessengerPersonal>>(LvrPRecibirServer); // resp será el string JSON a deserializa
                //RecibirJsonPersonal = RecibirJsonPersonalArray[0];

                return LvrPRecibirServer;

            }
            return null;
        }
    }

    public class ClasePersonalGrid
    {
        public string RUTID { get; set; }
        public string DIGVER { get; set; }
        public string APELLIDOS { get; set; }
        public string NOMBRES { get; set; }
    }
}

