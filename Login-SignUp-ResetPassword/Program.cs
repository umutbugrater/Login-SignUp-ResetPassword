using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Login_SignUp_ResetPassword.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Login_SignUp_ResetPassword
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<Context>();
            //Normalde böyle olacak ama þifremi unuttum kýsmýný ekleyince ek kod eklemek lazým
            //builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>()
            //    .AddErrorDescriber<CustomIdentityValidator>().AddEntityFrameworkStores<Context>();
            builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>()
               .AddErrorDescriber<CustomIdentityValidator>().AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider).AddEntityFrameworkStores<Context>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/SignInSignUp/SignIn";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=SignInSignUp}/{action=SignIn}/{id?}");

            app.Run();
        }
    }
}