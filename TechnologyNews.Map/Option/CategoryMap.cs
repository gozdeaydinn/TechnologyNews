using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyNews.Core.Map;
using TechnologyNews.Model.Option;

namespace TechnologyNews.Map.Option
{
    public class CategoryMap:CoreMap<Category>
    {
        public CategoryMap()
        {
            ToTable("dbo.Categories");

            Property(x => x.Name).IsOptional();
            Property(x => x.Description).IsOptional();

            HasMany(x => x.SubCategories)
               .WithRequired(x => x.Category)
               .HasForeignKey(x => x.CategoryID)
               .WillCascadeOnDelete(false);
        }
     
    }
}
