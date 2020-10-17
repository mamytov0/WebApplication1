using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PS_Bins
    {
        public int ID { get; set; }
        [Display(Name = "Код")]
        public string Code { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Тип Карты")]
        public int Card_type { get; set; }
        //public virtual PS_CARD_TYPE _CARD_TYPE { get; set; }
        public string Card_Name;
    }
}
