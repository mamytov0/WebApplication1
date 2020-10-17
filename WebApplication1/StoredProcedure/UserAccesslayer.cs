using Microsoft.Extensions.Configuration;
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
    public class UserAccesslayer
    {
        string connectionString;

        public UserAccesslayer(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }
        public IEnumerable<User> GetAllData()
        {
            string connectionString = ConnectionString.CName;
            List<User> lstr = new List<User>();


            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("GET_USER", con);
                cmd.Parameters.Add("PARAM1", OracleDbType.RefCursor, ParameterDirection.Output);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (OracleDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        PS_Bins pS_ = new PS_Bins();
                        User u = new User();
                        u.ID = Convert.ToInt32(rdr["ID"]);
                        u.Email = (rdr["EMAIL"]).ToString();
                        u.Password = (rdr["PASSWORD"]).ToString();

                        lstr.Add(u);
                    }
                }
                con.Close();
            }
            return lstr;
        }


        public IEnumerable<User> GetAll()
        {
            string connectionString = ConnectionString.CName;
            List<User> lstr = new List<User>();


            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("GET_USER", con);
                cmd.Parameters.Add("PARAM1", OracleDbType.RefCursor, ParameterDirection.Output);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (OracleDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        PS_Bins pS_ = new PS_Bins();
                        User u = new User();
                        u.ID = Convert.ToInt32(rdr["ID"]);
                        u.Email = (rdr["EMAIL"]).ToString();

                        lstr.Add(u);
                    }
                }
                con.Close();
            }
            return lstr;
        }


        //cmd.Parameters.Add("EMAIL", OracleDbType.Varchar2).Value=u.Email;

        public void Add(User user)
        {
            Crypt sets = new Crypt();
            string connectionString = ConnectionString.CName;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("INSERT_PS_USER", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("EMAIL", user.Email);
                cmd.Parameters.Add("PASSWORD", OracleDbType.Varchar2).Value = sets.GetMD5(user.Password);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Update(User user)
        {
            Crypt sets = new Crypt();
            string connectionString = ConnectionString.CName;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("UPDATE_PS_USER", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", user.ID);
                cmd.Parameters.Add("EMAIL", user.Email);
                if (user.Password == null)
                {
                    cmd.Parameters.Add("PASSWORD", user.Password);
                }
                else
                {
                    cmd.Parameters.Add("PASSWORD", OracleDbType.Varchar2).Value = sets.GetMD5(user.Password);

                }

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void Delete(int ID)
        {
            string connectionString = ConnectionString.CName;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("DELETE_PS_USER", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", ID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public User Details(int ID)
        {
            string connectionString = ConnectionString.CName;
            User user = new User();
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("DETAILS_PS_USER", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID_1", OracleDbType.Varchar2, ID, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("PARAM1", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);


                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    user.ID = Convert.ToInt32(rdr["ID"]);
                    user.Email = (rdr["EMAIL"]).ToString();
                    user.Password = (rdr["PASSWORD"]).ToString();

                }
            }
            return user;
        }

    }
}
