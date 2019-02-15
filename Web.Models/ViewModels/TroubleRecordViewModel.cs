using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Web.Models.Enums;

namespace Web.Models.ViewModels
{
    public class TroubleRecordViewModel
    {
        [Column("Beyaz Esya")]
        [DisplayName("Beyaz Eşya Tipi")]
        public Types Types { get; set; }
        [Column("Marka")]
        [DisplayName("Marka")]
        public BrandTypes BrandTypes { get; set; }
        [Column("Fotoğraf")]
        [DisplayName("Fotoğraf")]
        public string PhotoPath { get; set; }
        [Column("Mesaj")]
        [DisplayName("Arıza Bilgisi")]
        public string Message { get; set; }

        public HttpPostedFileBase PostedFile { get; set; }
    }
}
