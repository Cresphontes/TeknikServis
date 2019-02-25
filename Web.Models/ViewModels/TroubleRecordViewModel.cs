using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public int Id { get; set; }

        [Column("Beyaz Esya")]
        [DisplayName("Beyaz Eşya Tipi")]
        [Required]
        public Types Types { get; set; }
        [Column("Marka")]
        [DisplayName("Marka")]
        [Required]
        public BrandTypes BrandTypes { get; set; }
        [Column("Fotoğraf")]
        [DisplayName("Fotoğraf")]
        [Required]
        public string PhotoPath { get; set; }
        [Column("Mesaj")]
        [DisplayName("Arıza Bilgisi")]
        [MaxLength(100,ErrorMessage = "Mesaj kısmına en fazla 100 karakter girebilirsiniz.")]
        [MinLength(10,ErrorMessage = "Mesaj kısmına en az 10 karakter girmelisiniz.")]
        [Required]
        public string Message { get; set; }
        [DisplayName("Fotoğraf")]
        [Required]
        public HttpPostedFileBase PostedFile { get; set; }

        [DisplayName("Teknisyen")]
        public string UserName { get; set; }   
    }
}
