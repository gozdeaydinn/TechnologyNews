using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnologyNews.Model.Option;
using TechnologyNews.UI.Areas.Admin.Models.DTO;

namespace TechnologyNews.UI.Areas.Admin.Models.VM
{
    public class UpdateArticleVM
    {
        public UpdateArticleVM()
        {
            Categories = new List<Category>();
            SubCategories = new List<SubCategory>();
            AppUsers = new List<AppUser>();
            Article = new ArticleDTO();
        }
        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public List<AppUser> AppUsers { get; set; }
        public ArticleDTO Article { get; set; }
    }
}