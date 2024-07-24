using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Login_SignUp_ResetPassword.Controllers
{
    [AllowAnonymous]
    public class ModalOrnekController : Controller
    {
        ModalOrnekManager modalOrnekManager = new ModalOrnekManager(new EfModalOrnekRepository());

        public IActionResult Index()
        {
            //var values = modalOrnekManager.TGetList();
            //return View(values);
            return View();
        }


        [HttpPost]
        public IActionResult ModalAdd(ModalOrnek modal)
        {
            modalOrnekManager.TAdd(modal);
            return View("Index");
        }
    }
}
