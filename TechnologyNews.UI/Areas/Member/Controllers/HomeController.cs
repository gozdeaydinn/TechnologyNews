using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyNews.Service.Option;

namespace TechnologyNews.UI.Areas.Member.Controllers
{
    public class HomeController : Controller
    {
        ArticleService _articleService;
        public HomeController()
        {
            _articleService = new ArticleService();
        }
        public ActionResult Index()
        {
            var model =_articleService.GetActive().OrderByDescending(x => x.CreatedDate).Take(5);
            return View(model);
        }
    }
}