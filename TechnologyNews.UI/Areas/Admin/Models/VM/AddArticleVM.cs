using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnologyNews.Model.Option;

namespace TechnologyNews.UI.Areas.Admin.Models.VM
{
    public class AddArticleVM
    {
        public AddArticleVM()
        {
            Categories = new List<Category>();
            AppUsers = new List<AppUser>();
            SubCategories = new List<SubCategory>();
        }

        public List<Category> Categories { get; set; }
        public List<AppUser> AppUsers { get; set; }
        public List<SubCategory> SubCategories { get; set; }
    }
}