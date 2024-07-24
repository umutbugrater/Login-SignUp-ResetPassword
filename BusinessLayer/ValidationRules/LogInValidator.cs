using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class LogInValidator : AbstractValidator<AppUser>
    {
        public LogInValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta boş bırakılamaz")
                .Matches("^((?!admin).)*$").WithMessage("Email Address should not contain admin");// admin kelimesini içermemeli

            RuleFor(x => x.PasswordHash).NotEmpty().WithMessage("Lütfen şifrenizi giriniz!")
                .MinimumLength(6).WithMessage("Şifrenizi 6 karakterden fazla giriniz!")
                .Matches(@"[a-z]+").WithMessage("Şifrede en az bir küçük harf olmalıdır.")
                .Matches(@"[A-Z]+").WithMessage("Şifrede en az bir büyük harf olmalıdır.")
                .Matches(@"[\!\?\*\.]+").WithMessage("Şifrede en az bir özel harf olmalıdır (!? *.)")
                .Matches(@"[0-9]+").WithMessage("Şifrede en az bir rakam olmalıdır");
        }
    }
}
