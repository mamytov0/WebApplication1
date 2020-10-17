using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Utility
{
    public class ConnectionString
    {

        private static string cName = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST =localhost)(PORT = 1521 ))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME =orcl.local.com )));Password= 123 ;User ID=tilek;";
        public static string CName
        {
            get => cName;
        }
    }
}
