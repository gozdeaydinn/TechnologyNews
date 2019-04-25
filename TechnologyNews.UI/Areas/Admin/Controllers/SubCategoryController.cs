using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyNews.Model.Option;
using TechnologyNews.Service.Option;

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

        }

    }
}