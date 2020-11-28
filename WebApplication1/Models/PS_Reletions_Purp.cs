using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Paid_System_PS_.Models
{
    public partial class PS_Reletions_Purp
    {
        public int ID { get; set; }
        [Display(Name = "Сервис назначения")]
        public int Service_Purp { get; set; }
        [Display(Name = "Общее назначение")]
        public int Common_Serv { get; set; }
        
        public string service_p;
        
        public string common_s;


    }
}
