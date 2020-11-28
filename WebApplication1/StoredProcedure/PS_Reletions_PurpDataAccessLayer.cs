using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Paid_System_PS_.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Paid_System_PS_.Models.CallingStoredProcedures
{
    public class PS_Reletions_PurpDataAccessLayer
    {

        string connectionString;

        public PS_Reletions_PurpDataAccessLayer(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }
        public IEnumerable<PS_Reletions_Purp> GetAllData()
        {
            //string connectionString = ConnectionString.CName;
            List<PS_Reletions_Purp> lstr = new List<PS_Reletions_Purp>();


            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("GET_RELETIONS_PURP", con);
                cmd.Parameters.Add("PARAM1", OracleDbType.RefCursor, ParameterDirection.Output);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (OracleDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        PS_Reletions_Purp pS_Reletions_ = new PS_Reletions_Purp();
                        pS_Reletions_.ID = Convert.ToInt32(rdr["ID"]);
                        pS_Reletions_.service_p = (rdr["Service_Purp"]).ToString();
                        pS_Reletions_.common_s = (rdr["Common_Purp"]).ToString();
                       


                        lstr.Add(pS_Reletions_);
                    }
                }
                con.Close();
            }
            return lstr;
        }

        public void Add(PS_Reletions_Purp pS_Reletions)
        {
            //string connectionString = ConnectionString.CName;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("INSERT_RELETIONS_PURP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("SERVICE_PURP", pS_Reletions.Service_Purp);
                cmd.Parameters.Add("COMMON_SERV", pS_Reletions.Common_Serv);
             

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public PS_Reletions_Purp Details(int ID)
        {
            //string connectionString = ConnectionString.CName;
            PS_Reletions_Purp reletions_Purp = new PS_Reletions_Purp();
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("DETAILS_PS_RELETIONS_PURP", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PARAM1", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
                cmd.Parameters.Add("ID_1", OracleDbType.Varchar2, ID, System.Data.ParameterDirection.Input);

                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    reletions_Purp.ID = Convert.ToInt32(rdr["ID"]);
                    reletions_Purp.service_p= (rdr["service_p"]).ToString();
                    reletions_Purp.common_s = (rdr["common_s"]).ToString();
                  

                }
            }
            return reletions_Purp;
        }

        public void Delete(int ID)
        {
            //string connectionString = ConnectionString.CName;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("DELETE_RELETIONS_PURP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", ID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Update(PS_Reletions_Purp reletions_Purp)
        {
            //string connectionString = ConnectionString.CName;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("UPDATE_RELATIONS_PURP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", reletions_Purp.ID);
                cmd.Parameters.Add("SERVICE_PURP", reletions_Purp.Service_Purp);
                cmd.Parameters.Add("COMMON_SERV", reletions_Purp.Common_Serv);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
