using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyNews.Model.Option;
using TechnologyNews.Service.Option;

namespace TechnologyNews.UI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        AppUserService _appUserService;
        public HomeController()
        {
            _appUserService = new AppUserService();
        }
        public ActionResult Index()
        {
            TempData["class"] = "custom-hide";


            if (!HttpContext.User.Identity.IsAuthenticated)//kullanıcı kimliği doğrulandığında doğrulanır ... kimliği doğrulanmış formlarda kimliği doğrulanmış kullanıcıyı tanımlamak için setauthcookie kullanırız ....user otantike ise modele yönlendir
            {
                return View();
            }

            AppUser appuser = new AppUser();
            appuser = _appUserService.FindByUserName(HttpContext.User.Identity.Name);
            if (appuser.Role == Role.Admin)
            {
                TempData["class"] = "custom-show";
            }
            return View();
        }
    }
}