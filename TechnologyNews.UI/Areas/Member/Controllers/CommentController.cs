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

        public CommentController()
        {
            _commentService = new CommentService();
            _appUserService = new AppUserService();

        }

        public JsonResult AddComment(string userComment, Guid id)
        {

            Comment comment = new Comment();

            comment.AppUserID = _appUserService.FindByUserName(HttpContext.User.Identity.Name).ID;
            comment.ArticleID = id;
            comment.Content = userComment;

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
                AppUserImagePath = comment.AppUser.XSmallUserImage,
                FirstName = comment.AppUser.FirstName,
                LastName = comment.AppUser.LastName,
                CreatedDate = comment.CreatedDate.ToString(),
                Content = comment.Content
            }, JsonRequestBehavior.AllowGet);
        }

    }
}