using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMessenger
{
    class Bm_Servicios_Database
    {
        // public SQLiteFactory BM_DB;
        public SQLiteConnection BM_Connection;
        SQLiteCommand BK_Cmd_Servicios;
        SQLiteDataReader BK_Reader_Servicios;
        SQLiteCommand BK_Cmd_Servicios_Pais;
        SQLiteDataReader BK_Reader_Servicios_Pais;
        string StrAgregar_Servicios;
        string StrModificar_Servicios;
        string StrBorrar_Servicios;
        readonly string StrBuscar_Servicios = "SELECT * FROM SERVICIOS";
        readonly String StrBuscar_Servicios_Pais = "SELECT * FROM PAIS ORDER BY PAIS ASC";

		// Campos Servicios
		public string BK_NROENVIO { get; set; }
		public string BK_GUIADESPACHO { get; set; }
		public string BK_FECHA { get; set; }
		public string BK_HORA { get; set; }
		public string BK_CLIENTERUT { get; set; }
		public string BK_CLIENTEDIGVER { get; set; }
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
		public string BK_DESCRIPCION { get; set; }
		public string BK_OBSERVACIONES { get; set; }
		public string BK_ENTREGA { get; set; }
		public string BK_RECEPCION { get; set; }
		public string BK_TESPERA { get; set; }
		public string BK_FECHAENTREGA { get; set; }
		public string BK_HORAENTREGA { get; set; }
		public string BK_DISTANCIA { get; set; }
		public string BK_PROGRAMADO { get; set; }

		// Campos de PAIS
		public Int16 BK_E_CODPAIS { get; set; }
		public string BK_E_PAIS { get; set; }

		public bool BM_CreateDatabase(SQLiteConnection BM_Connection)
		{
			this.BM_Connection = BM_Connection;
			return true;
		}

		public bool Bm_Servicios_Agregar()
		{
			StrAgregar_Servicios = "INSERT INTO SERVICIOS (";
			StrAgregar_Servicios += "NROENVIO,";
			StrAgregar_Servicios += "GUIADESPACHO,";
			StrAgregar_Servicios += "FECHA,";
			StrAgregar_Servicios += "HORA,";
			StrAgregar_Servicios += "CLIENTERUT,";
			StrAgregar_Servicios += "CLIENTEDIGVER,";
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
			StrAgregar_Servicios += "DESCRIPCION,";
			StrAgregar_Servicios += "OBSERVACIONES,";
			StrAgregar_Servicios += "ENTREGA,";
			StrAgregar_Servicios += "RECEPCION,";
			StrAgregar_Servicios += "TESPERA,";
			StrAgregar_Servicios += "FECHAENTREGA,";
			StrAgregar_Servicios += "HORAENTREGA,";
			StrAgregar_Servicios += "DISTANCIA,";
			StrAgregar_Servicios += "PROGRAMADO) VALUES (";
			StrAgregar_Servicios += "'" + BK_NROENVIO + "',";
			StrAgregar_Servicios += "'" + BK_GUIADESPACHO + "',";
			StrAgregar_Servicios += "'" + BK_FECHA + "',";
			StrAgregar_Servicios += "'" + BK_HORA + "',";
			StrAgregar_Servicios += "'" + BK_CLIENTERUT + "',";
			StrAgregar_Servicios += "'" + BK_CLIENTEDIGVER + "',";
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
			StrAgregar_Servicios += "'" + BK_DESCRIPCION + "',";
			StrAgregar_Servicios += "'" + BK_OBSERVACIONES + "',";
			StrAgregar_Servicios += "'" + BK_ENTREGA + "',";
			StrAgregar_Servicios += "'" + BK_RECEPCION + "',";
			StrAgregar_Servicios += "'" + BK_TESPERA + "',";
			StrAgregar_Servicios += "'" + BK_FECHAENTREGA + "',";
			StrAgregar_Servicios += "'" + BK_HORAENTREGA + "',";
			StrAgregar_Servicios += "'" + BK_DISTANCIA + "',";
			StrAgregar_Servicios += "'" + BK_PROGRAMADO + "')";
			try {
				BK_Cmd_Servicios = new SQLiteCommand(StrAgregar_Servicios, BM_Connection);
				BK_Cmd_Servicios.ExecuteNonQuery();
				return true;
			}
			catch (System.Data.SQLite.SQLiteException)
			{
				return false;
			}
		}

		public bool Bm_Servicios_Buscar()
		{
			try {
				BK_Cmd_Servicios = new SQLiteCommand(StrBuscar_Servicios, BM_Connection);
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
					BK_DESCRIPCION = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DESCRIPCION"));
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
			catch (System.Data.SQLite.SQLiteException)
			{
				return false;
			}
		}

		public bool Bm_Servicios_Buscar(string pNROENVIO)
		{
			try
			{
				BK_Cmd_Servicios = new SQLiteCommand(StrBuscar_Servicios + " WHERE NROENVIO = '" + pNROENVIO + "'", BM_Connection);
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
					BK_DESCRIPCION = BK_Reader_Servicios.GetString(BK_Reader_Servicios.GetOrdinal("DESCRIPCION"));
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
			catch (System.Data.SQLite.SQLiteException)
			{
				return false;
			}
		}

		// Procedimiento Buscar Pais
		public bool Bm_E_Pais_EjecutarSelect()
		{
			try {
				BK_Cmd_Servicios_Pais = new SQLiteCommand(StrBuscar_Servicios_Pais, BM_Connection);
				BK_Reader_Servicios_Pais = BK_Cmd_Servicios_Pais.ExecuteReader();
				return true;
			}
			catch (System.Data.SQLite.SQLiteException)
			{
				return false;
			}
		}

		public bool Bm_E_Pais_Buscar()
		{
			try {
				if (BK_Reader_Servicios_Pais.Read())
				{
					// Llenar Valores de Recursos
					BK_E_CODPAIS = BK_Reader_Servicios_Pais.GetInt16(BK_Reader_Servicios_Pais.GetOrdinal("CODPAIS"));
					BK_E_PAIS = BK_Reader_Servicios_Pais.GetString(BK_Reader_Servicios_Pais.GetOrdinal("PAIS"));
					return true;
				}
				else
				{
					// No existe Recursos
					return false;
				}
			}
			catch (System.Data.SQLite.SQLiteException)
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
			BK_DESCRIPCION = "";
			BK_OBSERVACIONES = "";
			BK_ENTREGA = "";
			BK_RECEPCION = "";
			BK_TESPERA = "";
			BK_FECHAENTREGA = "";
			BK_HORAENTREGA = "";
			BK_DISTANCIA = "";
			BK_PROGRAMADO = "";
		}
	}
}
