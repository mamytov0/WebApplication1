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
    public class Type_CardDataAccessLayer
    {
        string connectionString;

        public Type_CardDataAccessLayer(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }
        public IEnumerable<Type_Card> GetAllData()
        {
            //string connectionString = ConnectionString.CName;
            List<Type_Card> lstr = new List<Type_Card>();


            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("GET_PS_D_C_PART ", con);
                cmd.Parameters.Add("PARAM1", OracleDbType.RefCursor, ParameterDirection.Output);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (OracleDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        PS_Bins pS_ = new PS_Bins();
                        Type_Card tp = new Type_Card();
                        tp.ID = Convert.ToInt32(rdr["ID"]);
                        tp.Name = (rdr["NAME"]).ToString();


                        lstr.Add(tp);
                    }
                }
                con.Close();
            }
            return lstr;
        }
    }
}
