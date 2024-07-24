using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class RegisterValidator : AbstractValidator<UserRegister2ViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Ad Soyad boş bırakılamaz");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş bırakılamaz");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Lütfen şifre tekrarnı giriniz");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Lütfen şifrenizi giriniz!");
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("Şifreler birbirleriyle uyuşmuyor.");
                
        }
    }
}
