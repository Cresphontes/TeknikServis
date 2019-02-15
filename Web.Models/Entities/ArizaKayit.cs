using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models.Enums;

namespace Web.Models.Entities
{
    [Table("ArızaKayıt")]
    public class ArizaKayit:BaseEntity<int>
    {

        public Types Types  { get; set; }
        public BrandTypes BrandTypes { get; set; }
        public string PhotoPath { get; set; }
        public string Message { get; set; }

    }
}
