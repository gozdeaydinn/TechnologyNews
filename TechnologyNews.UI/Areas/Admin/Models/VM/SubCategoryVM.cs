using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnologyNews.Model.Option;
using TechnologyNews.UI.Areas.Admin.Models.DTO;

namespace TechnologyNews.UI.Areas.Admin.Models.VM
{
    public class SubCategoryVM
    {
        public SubCategoryVM()
        {
            Categories = new List<Category>();
            SubCategory = new SubCategoryDTO();
        }
        public List<Category> Categories { get; set; }
        public SubCategoryDTO SubCategory { get; set; }
    }
}