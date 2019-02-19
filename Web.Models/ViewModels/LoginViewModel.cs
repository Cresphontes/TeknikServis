using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Kullanıcı Adı")]
        [Required]
        [MaxLength(50, ErrorMessage = "Kullanıcı adınız 50 karakterden fazla olamaz")]
        [MinLength(5, ErrorMessage = "Kullanıcı adınız 5 karakterden az olamaz")]
        public string Username { get; set; }
        [Display(Name = "Şifre")]
        [Required]
        [MinLength(5, ErrorMessage = "Şifreniz 5 karakterden az olamaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }

    }
}
