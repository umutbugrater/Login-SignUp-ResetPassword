using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Login_SignUp_ResetPassword.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace Login_SignUp_ResetPassword.Controllers
{
    [AllowAnonymous]
    public class SignInSignUpController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SignInSignUpController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegister2ViewModel user, IFormFile img)
        {
            RegisterValidator rv = new RegisterValidator();
            ValidationResult results = rv.Validate(user);

            if (results.IsValid)
            {
                if (img != null)
                {
                    string uzantı = Path.GetExtension(img.FileName);
                    string resimAdi = Guid.NewGuid() + uzantı;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/userimages/{resimAdi}");
                    using var stream = new FileStream(path, FileMode.Create);
                    img.CopyTo(stream);
                    user.ImageUrl = resimAdi;
                }
                else
                {
                    user.ImageUrl = "noPhoto.jpg";
                }
                AppUser appuser = new AppUser()
                {
                    Email = user.Email,
                    ImageUrl = user.ImageUrl,
                    NameSurname = user.NameSurname,
                    UserName = user.Email
                };
                //identity kütüphanesinde, şifre metot çağrılırken giriliyor
                var result = await _userManager.CreateAsync(appuser, user.Password);
                //await _userManager.AddToRoleAsync(user, "Member");

                if (result.Succeeded)
                {
                    MimeMessage mimeMessage = new MimeMessage();
                    MailboxAddress mailboxAddressFrom = new MailboxAddress("Proje.com", "umutbugra21@gmail.com");
                    mimeMessage.From.Add(mailboxAddressFrom);
                    MailboxAddress mailboxAddressTo = new MailboxAddress("User", user.Email);
                    mimeMessage.Subject = "Başarılı bir şekilde hesabınz oluşturuldu";
                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = "Text Body içerik alanı";
                    mimeMessage.Body = bodyBuilder.ToMessageBody();
                    mimeMessage.To.Add(mailboxAddressTo);

                    SmtpClient client = new SmtpClient();
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("umutbugra21@gmail.com", "lilh meyh rqbz cxko");
                    client.Send(mimeMessage);
                    client.Disconnect(true);
                    return RedirectToAction("SignIn");

                }
                //Password için küçük,büyük,uzunluk,özel karakter kullanılıp kullanılmadığına bakıyor
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
                return View(user);
            }

            //Validatorde hata veren yer
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View(user);
        }



        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(AppUser p)
        {
            LogInValidator lv = new LogInValidator();
            ValidationResult results = lv.Validate(p);
            if (results.IsValid)
            {
                //                        ---,---, çerezlerde hatırlansın mı?, 5 kere yanlış girildiğinde sisteme giriş belli süre(5dk) engellecenek 
                var result = await _signInManager.PasswordSignInAsync(p.Email, p.PasswordHash, false, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }

            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View(p);
        }

    }
}
