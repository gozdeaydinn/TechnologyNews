using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyNews.Core.Entity;

namespace TechnologyNews.Model.Option
{
    public class Category:CoreEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<SubCategory> SubCategories { get; set; }
    }
}
