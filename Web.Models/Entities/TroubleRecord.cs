using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models.EntityIdentity;
using Web.Models.Enums;
using Web.Models.IdentityEntities;

namespace Web.Models.Entities
{
    [Table("ArızaKayıt")]
    public class TroubleRecord:BaseEntity<int>
    {
       
        [Column("Beyaz Esya")]
        [DisplayName("Beyaz Eşya Tipi")]
        [Required]
        public Types Types  { get; set; }
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
        [MaxLength(100, ErrorMessage = "Mesaj kısmına en fazla 100 karakter girebilirsiniz.")]
        [MinLength(10, ErrorMessage = "Mesaj kısmına en az 10 karakter girmelisiniz.")]
        [Required]
        public string Message { get; set; }

        
       public virtual ICollection<UserTroubleRecord> UserTroubleRecords { get; set; } = new HashSet<UserTroubleRecord>();

    }
}
