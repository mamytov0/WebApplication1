using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Utility;

namespace WebApplication1.StoredProcedure
{
    public class PS_Service_PurposeDataAccessLayer
    {
        string connectionString;

        public PS_Service_PurposeDataAccessLayer(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<PS_Service_Purpose> GetAllData()
        {
            //string connectionString = ConnectionString.CName;
            List<PS_Service_Purpose> lstr = new List<PS_Service_Purpose>();


            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("GET_PS_SERVICE_PURPOSE", con);
                cmd.Parameters.Add("PARAM1", OracleDbType.RefCursor, ParameterDirection.Output);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (OracleDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        PS_Service_Purpose pS_Service_ = new PS_Service_Purpose();
                        pS_Service_.ID = Convert.ToInt32(rdr["ID"]);
                        pS_Service_.Description = (rdr["DESCRIPTION"]).ToString();
                        pS_Service_.Code_for_Service = (rdr["CODE_FOR_SERVICE"]).ToString();
                        pS_Service_.Card = (rdr["Card"]).ToString();


                        lstr.Add(pS_Service_);
                    }
                }
                con.Close();
            }
            return lstr;
        }
        public void Add(PS_Service_Purpose pS_Service_)
        {
            //string connectionString = ConnectionString.CName;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("INSERT_PS_SERVICE_PURPOSE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("DESCRIPTION1", pS_Service_.Description);
                cmd.Parameters.Add("CODE_FOR_SERVICE1", pS_Service_.Code_for_Service);
                cmd.Parameters.Add("CARD_TYPE1", pS_Service_.Card_type);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public PS_Service_Purpose Details(int ID)
        {
            //string connectionString = ConnectionString.CName;
            PS_Service_Purpose pS_Service_ = new PS_Service_Purpose();
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("DETAILS_PS_SERVICE_PURPOSE ", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PARAM1", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
                cmd.Parameters.Add("ID_1", OracleDbType.Varchar2, ID, System.Data.ParameterDirection.Input);

                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    pS_Service_.ID = Convert.ToInt32(rdr["ID"]);
                    pS_Service_.Description = (rdr["DESCRIPTION"]).ToString();
                    pS_Service_.Code_for_Service = (rdr["CODE_FOR_SERVICE"]).ToString();
                    pS_Service_.Card = (rdr["Card"]).ToString();
                }
            }
            return pS_Service_;
        }
        public void Delete(int ID)
        {
            //string connectionString = ConnectionString.CName;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("DELETE_PS_SERVICE_PURPOSE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", ID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        public void Update(PS_Service_Purpose s_Service_Purpose)
        {
            string connectionString = ConnectionString.CName;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("UPDATE_SERVICE_PURPOSE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", s_Service_Purpose.ID);
                cmd.Parameters.Add("DESCRIPTION", s_Service_Purpose.Description);
                cmd.Parameters.Add("CODE_FOR_SERVICE", s_Service_Purpose.Code_for_Service);
                cmd.Parameters.Add("CARD_TYPE", s_Service_Purpose.Card_type);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
