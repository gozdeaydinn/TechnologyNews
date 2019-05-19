using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyNews.Service.Option;
using TechnologyNews.UI.Areas.Member.Models.VM;

namespace TechnologyNews.UI.Areas.Member.Controllers
{
    public class HomeController : Controller
    {
        ArticleService _articleService;
        CommentService _commentService;
        LikeService _likeService;
        public HomeController()
        {
            _articleService = new ArticleService();
            _commentService = new CommentService();
            _likeService = new LikeService();
        }
        public ActionResult Index()
        {
            ArticleDetailVM model = new ArticleDetailVM();

            model.Articles = _articleService.GetActive();

            foreach (var item in model.Articles)
            {
                model.Comments = _commentService.GetDefault(x => x.ArticleID == item.ID).OrderByDescending(x => x.CreatedDate).Take(10).ToList();

                model.LikeCount = _likeService.GetDefault(x => x.ArticleID == item.ID).Count();
                model.CommentCount = _commentService.GetDefault(x => x.ArticleID == item.ID).Count();
            }

            return View(model);

        }
    }
}