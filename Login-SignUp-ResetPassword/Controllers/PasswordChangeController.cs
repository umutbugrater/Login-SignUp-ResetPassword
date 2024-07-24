using EntityLayer.Concrete;
using Login_SignUp_ResetPassword.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace Login_SignUp_ResetPassword.Controllers
{
    [AllowAnonymous]
    public class PasswordChangeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public PasswordChangeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Mail);
            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var passwordResetTokenLink = Url.Action("ResetPassword", "PasswordChange", new { userId = user.Id, token = passwordResetToken }, HttpContext.Request.Scheme);

            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Proje.com", "umutbugra21@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", forgetPasswordViewModel.Mail);
            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = "Şifre Değişiklik Talebi";
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = passwordResetTokenLink;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("umutbugra21@gmail.com", "lilh meyh rqbz cxko");
            client.Send(mimeMessage);
            client.Disconnect(true);

            //yüklenme ekranı ve başarılı şekilde sıfırlama bağlantısı yollandı yazısı
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            TempData["userid"] = userId;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        //Validation eklenecek 2 şifrenin aynı olması içn
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var userId = TempData["userid"];
            var token = TempData["token"];
            if (userId == null || token == null)
            {
                //hata mesajı
            }
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.ResetPasswordAsync(user, token.ToString(), resetPasswordViewModel.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Login");
            }
            return View();
        }
    }
}
