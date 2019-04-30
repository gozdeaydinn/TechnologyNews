using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyNews.Model.Option;
using TechnologyNews.Service.Option;
using TechnologyNews.UI.Areas.Member.Models.VM;

namespace TechnologyNews.UI.Areas.Member.Controllers
{
    public class CommentController : Controller
    {
        CommentService _commentService;
        AppUserService _appUserService;
        ArticleService _articleService;
        LikeService _likeService;
        public CommentController()
        {
            _commentService = new CommentService();
            _appUserService = new AppUserService();
            _articleService = new ArticleService();
            _likeService = new LikeService();
        }
        public ActionResult Show(Guid id)
        {
            ArticleDetailVM model = new ArticleDetailVM();
            model.Article = _articleService.GetById(id);
            model.AppUser = _appUserService.GetById(model.Article.AppUser.ID);
            model.Comments = _commentService.GetDefault(x => x.ArticleID == id);
            model.LikeCount = _likeService.GetDefault(x => x.ArticleID == id).Count;
            model.CommentCount = _commentService.GetDefault(x => x.ArticleID == id).Count;
            model.Likes = _likeService.GetDefault(x => x.ArticleID == id);

            return View(model);
        }
        public JsonResult AddComment(string usercomment, Guid id)
        {
            Comment comment = new Comment();

            comment.AppUserID = _appUserService.FindByUserName(User.Identity.Name).ID;
            comment.ArticleID = id;
            comment.Content = usercomment;
            comment.CreatedDate = DateTime.Now;
            _commentService.Add(comment);

            bool isAdded = false;
            try
            {
                _commentService.Add(comment);
                isAdded = true;
            }
            catch (Exception ex)
            {
                isAdded = false;
            }

            return Json(isAdded, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetAtricleComment(string id)
        {

            Guid articleID = new Guid(id);

            Comment comment = _commentService.GetDefault(x => x.ArticleID == articleID && x.Status == Core.Enum.Status.Active).LastOrDefault();


            return Json(new
            {
                AppUserImagePath = comment.AppUser.UserImage,
                FirstName = comment.AppUser.FirstName,
                LastName = comment.AppUser.LastName,
                CreatedDate = comment.CreatedDate.ToString(),
                Content = comment.Content
            }, JsonRequestBehavior.AllowGet);
        }

    }
}