using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyNews.Model.Option;
using TechnologyNews.Service.Option;
using TechnologyNews.UI.Areas.Admin.Models.DTO;
using TechnologyNews.UI.Areas.Admin.Models.VM;
using TechnologyNews.Utility;

namespace TechnologyNews.UI.Areas.Admin.Controllers
{
    public class ArticleController : Controller
    {
        ArticleService _articleService;
        CategoryService _categoryService;
        SubCategoryService _subCategoryService;
        AppUserService _appUserService;

        public ArticleController()
        {
            _articleService = new ArticleService();
            _categoryService = new CategoryService();
            _subCategoryService = new SubCategoryService();
            _appUserService = new AppUserService();
        }
        public ActionResult Add()
        {
            AddArticleVM model = new AddArticleVM()
            {
                Categories = _categoryService.GetActive(),
                AppUsers = _appUserService.GetActive(),
                SubCategories = _subCategoryService.GetActive()
            };

            return View(model);

        }

        [HttpPost]
        public ActionResult Add(Article data)
        {
         
            data.Status = Core.Enum.Status.Active;
            data.PublishDate = DateTime.Now;
            _articleService.Add(data);
            return Redirect("/Admin/Article/List");
        }
        public ActionResult List()
        {
            List<Article> model = _articleService.GetActive();
            return View(model);
        }
        public ActionResult Update(Guid id)
        {
            Article article = _articleService.GetById(id);
            UpdateArticleVM model = new UpdateArticleVM();
            model.Article.ID = article.ID;
            model.Article.Header = article.Header;
            model.Article.Content = article.Content;
            model.Article.PublishDate = DateTime.Now;
            List<Category> categorymodel = _categoryService.GetActive();
            model.Categories = categorymodel;
            List<SubCategory> subcategorymodel = _subCategoryService.GetActive();
            model.SubCategories = subcategorymodel;
            List<AppUser> appusermodel = _appUserService.GetActive();
            model.AppUsers = appusermodel;
            return View(model);
        }
        [HttpPost]
        public ActionResult Update(ArticleDTO data)
        {
            Article article = _articleService.GetById(data.ID);
            article.Header = data.Header;
            article.Content = data.Content;
            article.PublishDate = data.PublishDate;
            article.SubCategory.CategoryID = data.CategoryID;
            article.SubCategoryID = data.SubCategoryID;
            article.AppUserID = data.AppUserID;
            article.Status = Core.Enum.Status.Updated;
            _articleService.Update(article);
            return Redirect("/Admin/Article/List");
        }
        public ActionResult Delete(Guid id)
        {
            _articleService.Remove(id);
            return Redirect("/Admin/Article/List");
        }

    }
}