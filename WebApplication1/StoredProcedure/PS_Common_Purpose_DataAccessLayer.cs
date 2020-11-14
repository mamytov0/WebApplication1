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
    public class PS_Common_Purpose_DataAccessLayer
    {
        string connectionString;

        public PS_Common_Purpose_DataAccessLayer(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<PS_Common_purpose> GetAllData()
        {
            string connectionString = ConnectionString.CName;
            List<PS_Common_purpose> lstr = new List<PS_Common_purpose>();


            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("GET_PS_COMMON_PURPOSE ", con);
                cmd.Parameters.Add("PARAM1", OracleDbType.RefCursor, ParameterDirection.Output);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (OracleDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        PS_Common_purpose common_Purpose = new PS_Common_purpose();
                        common_Purpose.ID = Convert.ToInt32(rdr["ID"]);
                        common_Purpose.Code_for_service = (rdr["CODE_FOR_SERVICE"]).ToString();
                        common_Purpose.Name = (rdr["Name"]).ToString();
                        common_Purpose.Decription = (rdr["Description"]).ToString();
                        common_Purpose.Card = (rdr["Card"]).ToString();

                        //common_Purpose.Typeof_Card = Convert.ToInt32(rdr["D_C_PART"]);
                        lstr.Add(common_Purpose);
                    }
                    
                }
                con.Close();
            }
            return lstr;
        }
        public void Add(PS_Common_purpose cs)
        {
            //string connectionString = ConnectionString.CName;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("INSERT_PS_Common_purpose", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CODE_FOR_SERVICE", cs.Code_for_service);
                cmd.Parameters.Add("NAME)", cs.Name);
                cmd.Parameters.Add("DECRIPTION", cs.Decription);
                cmd.Parameters.Add("D_C_PART", cs.Typeof_Card);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public PS_Common_purpose Details(int ID)
        {
            //string connectionString = ConnectionString.CName;
            PS_Common_purpose s_Common_Purpose = new PS_Common_purpose();
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("DETAILS_PS_COMMON_PURPOSE", con);
              
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PARAM1", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
                cmd.Parameters.Add("ID_1", OracleDbType.Varchar2, ID, System.Data.ParameterDirection.Input);

                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    s_Common_Purpose.ID = Convert.ToInt32(rdr["ID"]);
                    s_Common_Purpose.Code_for_service = (rdr["CODE_FOR_SERVICE"]).ToString();
                    s_Common_Purpose.Name= (rdr["NAME"]).ToString();
                    s_Common_Purpose.Decription = (rdr["DESCRIPTION"]).ToString();
                    s_Common_Purpose.Card = (rdr["Card"]).ToString();
               
                }
            }
            return s_Common_Purpose;
        }

        public void Delete(int ID)
        {
            //string connectionString = ConnectionString.CName;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("DELETE_COMMON_PURPOSE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", ID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void Update(PS_Common_purpose pS_Common)
        {
            //string connectionString = ConnectionString.CName;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("UPDATE_COMMON_PURPOSE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", pS_Common.ID);
                cmd.Parameters.Add("CODE_FOR_SERVICE",pS_Common.Code_for_service);
                cmd.Parameters.Add("NAME",pS_Common.Name);
                cmd.Parameters.Add("DESCRIPTION", pS_Common.Decription);
                cmd.Parameters.Add("D_C_PART",pS_Common.Typeof_Card);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}
