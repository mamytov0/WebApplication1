using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PS_Service_Purpose
    {
        public int ID { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Код Сервиса")]
        public string Code_for_Service { get; set; }
        public int Card_type { get; set; }

        public string Card;

    }
}
