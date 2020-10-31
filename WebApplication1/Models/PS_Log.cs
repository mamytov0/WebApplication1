using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PS_Log
    {
        public string Target_Path { get; set; }
        public string System_Req { get; set; }
        public string Response { get; set; }
        public string Request { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime INS_Date { get; set; }
        public string Cur { get; set; }
        public int AMNT { get; set; }
        public string ACC { get; set; }
    }
}
