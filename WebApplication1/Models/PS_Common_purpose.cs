using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PS_Common_purpose
    {
        public int ID { get; set; }
        [Display(Name = "Код Сервиса")]
        public string Code_for_service { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Decription { get; set; }
        [Display(Name = "Тип Карты")]
        public int Typeof_Card { get; set; }

        public string Card;
        public Type_Card Type_ { get; set; }
    }
}
