using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.ViewModels
{
    public class AdminEditUserViewModel
    {
        [Display(Name = "İsim")]
        [Required]
        [MaxLength(25, ErrorMessage = "İsminiz 25 karakterden fazla olamaz.")]
        [MinLength(2, ErrorMessage = "Isminiz 2 karakterden az olamaz.")]
        public string Name { get; set; }
        [Display(Name = "Soyisim")]
        [Required]
        [MaxLength(40, ErrorMessage = "Soyisminiz 40 karakterden fazla olamaz")]
        [MinLength(2, ErrorMessage = "Soyisminiz 2 karakterden az olamaz")]
        public string Surname { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        [Required]
        [MaxLength(50, ErrorMessage = "Kullanıcı adınız 50 karakterden fazla olamaz")]
        [MinLength(5, ErrorMessage = "Kullanıcı adınız 5 karakterden az olamaz")]
        public string Username { get; set; }
        [Display(Name = "Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Telefon")]
        [Required]
        [StringLength(11, ErrorMessage = "Telefon numaranız 11 haneli olmalıdır.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Display(Name = "Cinsiyet")]
        [Required]
        public string Gender { get; set; }
        [Display(Name = "BirthDate")]
        [Required]
        [DataType(DataType.Date)]
        public string BirthDate { get; set; }
        [Display(Name = "Ülke")]
        [Required]
        public string Country { get; set; }
        [Display(Name = "Şehir")]
        [Required]
        public string City { get; set; }
        [Display(Name = "Adres")]
        [Required]
        public string Adress { get; set; }
        public string Role { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
