using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.Enums
{
    public enum Types
    {
        [Display(Name = "Buzdolabı")]
        buzdolabi,
        [Display(Name = "Çamaşır Makinesi")]
        camasir,
        [Display(Name = "Fırın")]
        firin,
        [Display(Name = "Ocak")]
        ocak,
        [Display(Name = "Bulaşık Makinesi")]
        bulasik,
        [Display(Name = "Davlumbaz")]
        davlumbaz,
        [Display(Name = "Mikrodalga")]
        mikrodalga
    }
}
