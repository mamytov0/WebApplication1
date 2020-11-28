using Oracle.ManagedDataAccess.Client;
using Paid_System_PS_.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
namespace Paid_System_PS_.Models.CallingStoredProcedures
{
    public class PS_Bins_DataAccessLayer
    {

        string connectionString;

        public PS_Bins_DataAccessLayer(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }

     
        public IEnumerable<PS_Bins> GetAllData()
        {
            //string connectionString = ConnectionString.CName;

            List<PS_Bins> lstr = new List<PS_Bins>();
           

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("GET_PS_BINS", con);
                cmd.Parameters.Add("PARAM1", OracleDbType.RefCursor, ParameterDirection.Output);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (OracleDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        PS_Bins pS_ = new PS_Bins();
                        pS_.ID = Convert.ToInt32(rdr["ID"]);
                        pS_.Code = (rdr["CODE"]).ToString();
                        pS_.Name = (rdr["NAME"]).ToString();
                        pS_.Card_Name = (rdr["Card_Type"]).ToString();
                        
                        lstr.Add(pS_);
                    }
                }
                con.Close();
            }
            return lstr;
        }
        public void Add(PS_Bins _Bins)
        {
            //string connectionString = ConnectionString.CName;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("INSERT_PS_BINS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CODE", _Bins.Code);
                cmd.Parameters.Add("NAME", _Bins.Name);
                cmd.Parameters.Add("CARDTYPE", _Bins.Card_type);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public PS_Bins Details(int ID)
        {
            //string connectionString = ConnectionString.CName;
            PS_Bins _Bins = new  PS_Bins();

            using (OracleConnection con = new OracleConnection(connectionString))
            {        
                OracleCommand cmd = new OracleCommand("DETAILS_PS_BINS ", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PARAM1", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
                cmd.Parameters.Add("ID1", OracleDbType.Varchar2, ID, System.Data.ParameterDirection.Input);        
                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _Bins.ID = Convert.ToInt32(rdr["ID"]);
                    _Bins.Code = (rdr["CODE"]).ToString();
                    _Bins.Name = (rdr["NAME"]).ToString();
                    _Bins.Card_Name = (rdr["Card"]).ToString();
                
                }
            }
            return _Bins;
        }
        public void Update(PS_Bins pS)
        {
            //string connectionString = ConnectionString.CName;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("UPDATE_PS_BINS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", pS.ID);
                cmd.Parameters.Add("CODE", pS.Code);
                cmd.Parameters.Add("Name", pS.Name);
                cmd.Parameters.Add("CARDTYPE", pS.Card_type);
               

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void Delete(int ID)
        {
            //string connectionString = ConnectionString.CName;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("DELETE_PS_BINS ", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", ID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
