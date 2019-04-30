using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyNews.Model.Option;
using TechnologyNews.Service.Option;

namespace TechnologyNews.UI.Areas.Member.Controllers
{
    public class CommentController : Controller
    {
        CommentService _commentService;
        AppUserService _appUserService;
        public CommentController()
        {
            _commentService = new CommentService();
            _appUserService = new AppUserService();
        }
        public ActionResult Add()
        {
            return View();
        }
        
    }
}