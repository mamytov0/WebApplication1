using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public partial class PS_CARD_TYPE
    {
        public int ID { get; set; }
        [Display(Name = "Наименование")]
        public string NAME { get; set; }
        public string FIRSTSYMBOLS { get; set; }
        [Display(Name = "Адрес Сервиса")]
        public string SERVICEADRESS { get; set; }
        [Display(Name = "Логин")]
        public string LOGIN { get; set; }
        [Display(Name = "Пароль")]
        public string PASS { get; set; }
    }
}
