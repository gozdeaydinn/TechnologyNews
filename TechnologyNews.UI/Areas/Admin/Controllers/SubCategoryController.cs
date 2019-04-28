using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyNews.Model.Option;
using TechnologyNews.Service.Option;
using TechnologyNews.UI.Areas.Admin.Models.DTO;
using TechnologyNews.UI.Areas.Admin.Models.VM;

namespace TechnologyNews.UI.Areas.Admin.Controllers
{
    public class SubCategoryController : Controller
    {
        SubCategoryService _subCategoryService;
        CategoryService _categoryService;
        public SubCategoryController()
        {
            _categoryService = new CategoryService();
            _subCategoryService = new SubCategoryService();
        }
        public ActionResult Add()
        {
           List<Category> model=_categoryService.GetActive();
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(SubCategory data)
        {
            _subCategoryService.Add(data);
            return Redirect("/Admin/SubCategory/List");
        }
        public ActionResult List()
        {
            List<SubCategory> model = _subCategoryService.GetActive();
            return View(model);
        }
        public ActionResult Update(Guid id)
        {
            SubCategory sub = _subCategoryService.GetById(id);
            SubCategoryVM model = new SubCategoryVM();
            model.SubCategory.ID = sub.ID;
            model.SubCategory.Name = sub.Name;
            model.SubCategory.Description = sub.Description;
            List<Category> categorymodel = _categoryService.GetActive();
            model.Categories = categorymodel;

            return View(model);
        }
        [HttpPost]
        public ActionResult Update(SubCategoryDTO data)
        {
            SubCategory sub = _subCategoryService.GetById(data.ID);
            sub.Name = data.Name;
            sub.Description = data.Description;
            sub.CategoryID = data.CategoryID;
            _subCategoryService.Update(sub);

            return Redirect("/Admin/SubCategory/List");
        }

        public ActionResult Delete(Guid id)
        {
            _subCategoryService.Remove(id);
            return Redirect("/Admin/SubCategory/List");
        }
    }
}