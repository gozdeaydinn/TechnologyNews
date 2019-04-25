using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyNews.Model.Option;
using TechnologyNews.Service.Option;
using TechnologyNews.UI.Areas.Admin.Models.DTO;

namespace TechnologyNews.UI.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CategoryService _categoryService;
        public CategoryController()
        {
            _categoryService = new CategoryService();
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Category data)
        {
            _categoryService.Add(data);
            return Redirect("/Admin/Home/Index");
        }
        public ActionResult List()
        {
            List<Category> model = _categoryService.GetActive();
            return View(model);
        }
        public ActionResult Update(Guid id)
        {
            Category category = _categoryService.GetById(id);
            CategoryDTO model = new CategoryDTO();
            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            

            return View(model);     
        }
        [HttpPost]
        public ActionResult Update(CategoryDTO data)
        {
            Category category = _categoryService.GetById(data.ID);
            category.Name = data.Name;
            category.Description = data.Description;
            category.Status = Core.Enum.Status.Updated;
            _categoryService.Update(category);

            return Redirect("/Admin/Category/List");
        }
        public ActionResult Delete(Guid id)
        {
            _categoryService.Remove(id);
            return Redirect("/Admin/Category/List");
        }
    }
}