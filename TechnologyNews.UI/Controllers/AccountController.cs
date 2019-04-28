using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TechnologyNews.Core.Enum;
using TechnologyNews.Model.Option;
using TechnologyNews.Service.Option;
using TechnologyNews.UI.Models.VM;

namespace TechnologyNews.UI.Controllers
{
    public class AccountController : Controller
    {
        AppUserService _appUserService;
        public AccountController()
        {
            _appUserService = new AppUserService();
        }
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser user = _appUserService.FindByUserName(User.Identity.Name);
                if (user.Status == Status.Active || user.Status == Status.Updated)
                {
                    if (user.Role == Role.Admin)
                    {
                        string cookie = user.UserName;
                        FormsAuthentication.SetAuthCookie(cookie, true);
                        Session["FullName"] = user.FirstName + ' ' + user.LastName;
                        return Redirect("/Admin/Home/Index");
                    }
                    else if (user.Role == Role.Author)
                    {
                        string cookie = user.UserName;
                        FormsAuthentication.SetAuthCookie(cookie, true);
                        Session["FullName"] = user.FirstName + ' ' + user.LastName;
                        return Redirect("/Author/Home/Index");
                    }
                    else
                    {
                        string cookie = user.UserName;
                        FormsAuthentication.SetAuthCookie(cookie, true);
                        Session["FullName"] = user.FirstName + ' ' + user.LastName;
                        return Redirect("/Member/Home/Index");
                    }
                }
                else
                {
                    ViewData["error"] = "Username or Password is wrong!";
                    return View();
                }
            }
            else
            {
                TempData["class"] = "custom-hide";
                return View();
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM credential)
        {
            if (ModelState.IsValid)
            {
                if (_appUserService.CheckCredentials(credential.UserName, credential.Password))
                {
                    AppUser user = _appUserService.FindByUserName(credential.UserName);
                    if (user.Status == Status.Active || user.Status == Status.Updated)
                    {
                        if (user.Role == Role.Admin)
                        {
                            string cookie = user.UserName;
                            FormsAuthentication.SetAuthCookie(cookie, true);
                            Session["FullName"] = user.FirstName + ' ' + user.LastName;
                            return Redirect("/Admin/Home/Index");
                        }
                        else if (user.Role == Role.Author)
                        {
                            string cookie = user.UserName;
                            FormsAuthentication.SetAuthCookie(cookie, true);
                            Session["FullName"] = user.FirstName + ' ' + user.LastName;
                            return Redirect("/Author/Home/Index");
                        }
                        else
                        {
                            string cookie = user.UserName;
                            FormsAuthentication.SetAuthCookie(cookie, true);
                            Session["FullName"] = user.FirstName + ' ' + user.LastName;
                            return Redirect("/Member/Home/Index");
                        }
                    }
                    else
                    {
                        ViewData["error"] = "Username or Password is wrong!";
                        return View();
                    }

                }
                else
                {
                    ViewData["error"] = "Username or Password is wrong!";
                    return View();
                }
            }
            else
            {
                TempData["class"] = "custom-hide";
                return View();
            }
        }
        [Authorize]//Login olmuş kullanıcılar için
        public ActionResult LogOut()
        {

            FormsAuthentication.SignOut();
            return Redirect("/Account/Login");
        }
    }
}