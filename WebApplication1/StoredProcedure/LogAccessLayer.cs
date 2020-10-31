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
    public class LogAccessLayer
    {
        string connectionString;

        public LogAccessLayer(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }
        public List<PS_Log> Get_Log(DateTime start, DateTime finish)
        {
            //string connectionString = ConnectionString.CName;
            List<PS_Log> log_list = new List<PS_Log>();


            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("GET_LOG", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PARAM1", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
                cmd.Parameters.Add("Date_start", OracleDbType.Date, start, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("Date_finish ", OracleDbType.Date, finish, System.Data.ParameterDirection.Input);
                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    PS_Log log = new PS_Log();
                    log.System_Req = (rdr["SYSTEM_REQ"]).ToString();
                    log.Request = (rdr["REQUEST"]).ToString();
                    log.Response = (rdr["RESPONSE"]).ToString();
                    log.Target_Path = (rdr["TARGET_PATH"]).ToString();
                    log.INS_Date = Convert.ToDateTime(rdr["INS_DATE"]);
                    if (rdr["AMNT"] == null)
                    {
                        log.AMNT = 0;
                    }
                    else
                    {
                        log.AMNT = Convert.ToInt32(rdr["AMNT"]);
                    }

                    log.ACC = (rdr["ACC"]).ToString();
                    log.Cur = (rdr["CUR"]).ToString();
                    log_list.Add(log);
                }
            }
            return log_list;
        }
    }
}
