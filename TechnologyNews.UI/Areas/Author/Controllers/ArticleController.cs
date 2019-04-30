using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyNews.Model.Option;
using TechnologyNews.Service.Option;
using TechnologyNews.UI.Areas.Author.Models.DTO;
using TechnologyNews.UI.Areas.Author.Models.VM;
using TechnologyNews.Utility;

namespace TechnologyNews.UI.Areas.Author.Controllers
{
    public class ArticleController : Controller
    {
        CategoryService _categoryService;
        SubCategoryService _subCategoryService;
        AppUserService _appUserService;
        ArticleService _articleService;

        public ArticleController()
        {
            _categoryService = new CategoryService();
            _subCategoryService = new SubCategoryService();
            _appUserService = new AppUserService();
            _articleService = new ArticleService();
        }
        public ActionResult Add()
        {
            AddArticleVM model = new AddArticleVM()
            {
                Categories = _categoryService.GetActive(),
                SubCategories = _subCategoryService.GetActive()
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(Article data,HttpPostedFileBase Image)
        {
            List<string> UploadedImagePaths = new List<string>();

            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);

            data.ImagePath = UploadedImagePaths[0];

            if (data.ImagePath == "0" || data.ImagePath == "1" || data.ImagePath == "2")
            {
                data.ImagePath = ImageUploader.DefaultProfileImagePath;
                data.ImagePath = ImageUploader.DefaultXSmallProfileImage;
                data.ImagePath = ImageUploader.DefaulCruptedProfileImage;
            }
            else
            {
                data.ImagePath = UploadedImagePaths[1];
                data.ImagePath = UploadedImagePaths[2];
            }

            AppUser user = _appUserService.GetByDefault(x=>x.UserName==User.Identity.Name);
            data.AppUserID = user.ID;
            data.PublishDate = DateTime.Now;

            _articleService.Add(data);
            return Redirect("/Author/Article/List");
        }
        public ActionResult List()
        {
            Guid userID = _appUserService.FindByUserName(User.Identity.Name).ID;
            List<Article> model = _articleService.GetDefault(x => x.AppUserID == userID && (x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated));
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
            model.Article.ImagePath = article.ImagePath;
            List<Category> categorymodel = _categoryService.GetActive();
            model.Categories = categorymodel;
            List<SubCategory> subcategorymodel = _subCategoryService.GetActive();
            model.SubCategories = subcategorymodel;

            return View(model);
                
        }
        [HttpPost]
        public ActionResult Update(ArticleDTO data,HttpPostedFileBase Image)
        {
            List<string> UploadedImagePaths = new List<string>();

            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);

            data.ImagePath = UploadedImagePaths[0];

            Article update = _articleService.GetById(data.ID);

            if (data.ImagePath == "0" || data.ImagePath == "1" || data.ImagePath == "2")
            {

                if (update.ImagePath == null || update.ImagePath == ImageUploader.DefaultProfileImagePath)
                {
                    update.ImagePath = ImageUploader.DefaultProfileImagePath;
                    update.ImagePath = ImageUploader.DefaultXSmallProfileImage;
                    update.ImagePath = ImageUploader.DefaulCruptedProfileImage;
                }
                else
                {
                    update.ImagePath = update.ImagePath;
                }

            }
            else
            {
                update.ImagePath = UploadedImagePaths[0];
                update.ImagePath = UploadedImagePaths[1];
                update.ImagePath = UploadedImagePaths[2];
            }

            Article article = _articleService.GetById(data.ID);
            article.Header = data.Header;
            article.Content = data.Content;
            article.PublishDate = data.PublishDate;
            article.SubCategory.CategoryID = data.CategoryID;
            article.SubCategoryID = data.SubCategoryID;
            article.Status = Core.Enum.Status.Updated;
            _articleService.Update(article);
            return Redirect("/Author/Article/List");
        }
        public ActionResult Delete(Guid id)
        {
            _articleService.Remove(id);
            return Redirect("/Author/Article/List");
        }
    }
   
}