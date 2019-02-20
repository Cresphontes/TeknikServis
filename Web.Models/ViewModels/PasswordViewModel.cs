using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.ViewModels
{
    public class PasswordViewModel
    {
        [Display(Name = "Eski Şifre")]
        [Required]
        [MinLength(5, ErrorMessage = "Şifreniz 5 karakterden az olamaz")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Display(Name = " Yeni Şifre")]
        [Required]
        [MinLength(5, ErrorMessage = "Şifreniz 5 karakterden az olamaz")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Display(Name = "Yeni Şifre Tekrar")]
        [Required]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Yeni Belirlemek İstediğiniz Şifreler Uyuşmuyor")]
        public string ConfirmNewPassword { get; set; }
    }
}
