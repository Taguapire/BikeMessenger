using System;
using System.Collections.Generic;
using SQLite;

namespace BikeMessenger
{
    class BM_CCRP
    {
        private TransferVar BM_TransferVar = new TransferVar();

        // Valode de Memoria a Bases de Datos

        private string CompletoNombreBD = "";

        public BM_CCRP()
        {
            CompletoNombreBD = BM_TransferVar.DIRECTORIO_BASE_LOCAL + "\\BikeMessenger.db";
        }

        // ************************************
        // Procesar Comuna/Municipio
        // ************************************
        public List<string> BuscarComuna()
        {
            List<string> VCOMUNA = new List<string>();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerComuna> results = BM_ConexionLite.Table<TbBikeMessengerComuna>().ToList();

            for (int i = 0; i < results.Count; i++)
            {
                VCOMUNA.Add(results[i].VALOR);
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return VCOMUNA;
        }

        public bool AgregarComuna(string pComuna)
        {
            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                TbBikeMessengerComuna record = new TbBikeMessengerComuna
                {
                    VALOR = pComuna
                };
                _ = BM_ConexionLite.InsertOrReplace(record);
            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return true;
        }

        // ************************************
        // Procesar Ciudad
        // ************************************
        public List<string> BuscarCiudad()
        {
            List<string> VCIUDAD = new List<string>();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerCiudad> results = BM_ConexionLite.Table<TbBikeMessengerCiudad>().ToList();

            for (int i = 0; i < results.Count; i++)
            {
                VCIUDAD.Add(results[i].VALOR);
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return VCIUDAD;
        }

        public bool AgregarCiudad(string pCiudad)
        {
            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                TbBikeMessengerCiudad record = new TbBikeMessengerCiudad
                {
                    VALOR = pCiudad
                };
                _ = BM_ConexionLite.InsertOrReplace(record);
            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return true;
        }

        // ************************************
        // Procesar Region/Estado
        // ************************************
        public List<string> BuscarRegion()
        {
            List<string> VREGION = new List<string>();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerRegion> results = BM_ConexionLite.Table<TbBikeMessengerRegion>().ToList();

            for (int i = 0; i < results.Count; i++)
            {
                VREGION.Add(results[i].VALOR);
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return VREGION;
        }

        public bool AgregarRegion(string pRegion)
        {
            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            BM_ConexionLite.RunInTransaction(() =>
            {
                TbBikeMessengerRegion record = new TbBikeMessengerRegion
                {
                    VALOR = pRegion
                };
                _ = BM_ConexionLite.InsertOrReplace(record);
            });

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return true;
        }

        // ************************************
        // Procesar Pais
        // ************************************
        public List<string> BuscarPais()
        {
            List<string> VPAIS = new List<string>();

            SQLiteConnection BM_ConexionLite = new SQLiteConnection(CompletoNombreBD);

            List<TbBikeMessengerPais> results = BM_ConexionLite.Table<TbBikeMessengerPais>().ToList();

            for (int i = 0; i < results.Count; i++)
            {
                VPAIS.Add(results[i].VALOR);
            }

            BM_ConexionLite.Close();
            BM_ConexionLite.Dispose();

            return VPAIS;
        }
    }

    internal class TbBikeMessengerComuna
    {
        [PrimaryKey]
        [Column("valor")]
        public string VALOR { get; set; }
    }

    internal class TbBikeMessengerCiudad
    {
        [PrimaryKey]
        [Column("valor")]
        public string VALOR { get; set; }
    }

    internal class TbBikeMessengerRegion
    {
        [PrimaryKey]
        [Column("valor")]
        public string VALOR { get; set; }
    }

    internal class TbBikeMessengerPais
    {
        [PrimaryKey]
        [Column("valor")]
        public string VALOR { get; set; }
    }
}
