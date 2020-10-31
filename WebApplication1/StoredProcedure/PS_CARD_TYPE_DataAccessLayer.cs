using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.StoredProcedure
{
    public class PS_CARD_TYPE_DataAccessLayer
    {
        string connectionString;

        public PS_CARD_TYPE_DataAccessLayer(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<PS_CARD_TYPE> GetAllData()
        {
            //string connectionString = ConnectionString.CName;
            List<PS_CARD_TYPE> lstr = new List<PS_CARD_TYPE>();

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("GET_PS_CARD_TYPE", con);
                cmd.Parameters.Add("PARAM1", OracleDbType.RefCursor, ParameterDirection.Output);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (OracleDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        PS_CARD_TYPE card_type = new PS_CARD_TYPE();
                        card_type.ID = Convert.ToInt32(rdr["ID"]);
                        card_type.NAME = (rdr["NAME"]).ToString();
                        card_type.FIRSTSYMBOLS = (rdr["FIRSTSYMBOLS"]).ToString();
                        card_type.SERVICEADRESS = (rdr["SERVICEADRESS"]).ToString();
                        card_type.LOGIN = (rdr["LOGIN"]).ToString();
                        card_type.PASS = (rdr["PASS"]).ToString();
                        lstr.Add(card_type);
                    }
                }
                con.Close();
            }
            return lstr;
        }
        public void Add(PS_CARD_TYPE ps)
        {
            //string connectionString = ConnectionString.CName;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("INSERT_PS_CARD_TYPE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("Name", ps.NAME);
                cmd.Parameters.Add("FIRSTSYMBOLS)", ps.FIRSTSYMBOLS);
                cmd.Parameters.Add("SERVICEADRESS", ps.SERVICEADRESS);
                cmd.Parameters.Add("LOGIN", ps.LOGIN);
                cmd.Parameters.Add("PASS", ps.PASS);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public PS_CARD_TYPE Details(int ID)
        {
            //string connectionString = ConnectionString.CName;
            PS_CARD_TYPE pS = new PS_CARD_TYPE();

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                string oracleQuery = "SELECT * FROM PS_CARD_TYPE WHERE ID= " + ID;
                OracleCommand cmd = new OracleCommand(oracleQuery, con);
                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    pS.ID = Convert.ToInt32(rdr["ID"]);
                    pS.NAME = (rdr["NAME"]).ToString();
                    pS.FIRSTSYMBOLS = (rdr["FIRSTSYMBOLS"]).ToString();
                    pS.SERVICEADRESS = (rdr["SERVICEADRESS"]).ToString();
                    pS.LOGIN = (rdr["LOGIN"]).ToString();
                    pS.PASS = (rdr["PASS"]).ToString();
                }
            }
            return pS;
        }
        public void Update(PS_CARD_TYPE ps)
        {
            //string connectionString = ConnectionString.CName;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("Update_PS_CARD_TYPE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", ps.ID);
                cmd.Parameters.Add("Name", ps.NAME);
                cmd.Parameters.Add("FIRSTSYMBOLS)", ps.FIRSTSYMBOLS);
                cmd.Parameters.Add("SERVICEADRESS", ps.SERVICEADRESS);
                cmd.Parameters.Add("LOGIN", ps.LOGIN);
                cmd.Parameters.Add("PASS", ps.PASS);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void Delete(int id)
        {
            //string connectionString = ConnectionString.CName;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("DELETE_PS_CARD_TYPE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
