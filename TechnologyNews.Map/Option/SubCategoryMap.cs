using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyNews.Core.Map;
using TechnologyNews.Model.Option;

namespace TechnologyNews.Map.Option
{
    public class SubCategoryMap:CoreMap<SubCategory>
    {
        public SubCategoryMap()
        {
            ToTable("dbo.SubCategories");

            Property(x => x.Name).IsOptional();
            Property(x => x.Description).IsOptional();

            HasMany(x => x.Articles)
                .WithRequired(x => x.SubCategory)
                .HasForeignKey(x => x.SubCategoryID)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Category)
                .WithMany(x => x.SubCategories)
                .HasForeignKey(x => x.CategoryID)
                .WillCascadeOnDelete(false);
        }
    }
}
