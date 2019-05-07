using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyNews.Service.Option;
using TechnologyNews.UI.Areas.Member.Models.VM;

namespace TechnologyNews.UI.Areas.Member.Controllers
{
    public class ArticleController : Controller
    {
        ArticleService _articleService;
        CategoryService _categoryService;
        AppUserService _appUserService;
        CommentService _commentService;
        LikeService _likeService;

        public ArticleController()
        {
            _articleService = new ArticleService();
            _categoryService = new CategoryService();
            _appUserService = new AppUserService();
            _commentService = new CommentService();
            _likeService = new LikeService();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Show(Guid id)
        {
            ArticleDetailVM model = new ArticleDetailVM();
            model.Article = _articleService.GetById(id);
            model.AppUser = _appUserService.GetById(model.Article.AppUser.ID);
            model.Comments = _commentService.GetDefault(x => x.ArticleID == id &&(x.Status==Core.Enum.Status.Active || x.Status==Core.Enum.Status.Updated));
            model.LikeCount = _likeService.GetDefault(x => x.ArticleID == id && (x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated)).Count;
            model.CommentCount = _commentService.GetDefault(x => x.ArticleID == id && (x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated)).Count;
            model.Likes = _likeService.GetDefault(x => x.ArticleID == id && (x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated));

            return View(model);
        }
    }
}