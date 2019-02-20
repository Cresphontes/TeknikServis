using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.ViewModels
{
    public class ProfileViewModel
    {
        [Display(Name = "Kullanıcı Adı")]
        [Required]
        [MaxLength(50, ErrorMessage = "Kullanıcı adınız 50 karakterden fazla olamaz")]
        [MinLength(5, ErrorMessage = "Kullanıcı adınız 5 karakterden az olamaz")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Telefon")]
        [Required]
        [StringLength(11, ErrorMessage = "Telefon numaranız 11 haneli olmalıdır.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

       
    }
}
