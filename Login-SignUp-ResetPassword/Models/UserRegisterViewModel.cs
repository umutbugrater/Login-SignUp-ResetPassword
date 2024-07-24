using System.ComponentModel.DataAnnotations;

namespace Login_SignUp_ResetPassword.Models
{
    public class UserRegisterViewModel
    {
        [Display(Name = "Kullanıcı Adınız")]
        [Required(ErrorMessage = "Lütfen Kullanıcı Adınızı Giriniz")]
        public string UserName { get; set; }
        public string ImageUrl { get; set; }

        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage = "Lütfen Ad Soyad Giriniz")]
        public string NameSurname { get; set; }

        [Display(Name = "Mail Adresi")]
        [Required(ErrorMessage = "Lütfen mail giriniz")]
        public string Email { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Lütfen şifre giriniz")]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor!")]
        [Required(ErrorMessage = "Lütfen şifre tekrarını giriniz")]
        public string ConfirmPassword { get; set; }
    }
}
