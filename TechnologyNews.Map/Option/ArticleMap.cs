using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyNews.Core.Map;
using TechnologyNews.Model.Option;

namespace TechnologyNews.Map.Option
{
    public class ArticleMap:CoreMap<Article>
    {
        public ArticleMap()
        {
            ToTable("dbo.Articles");
            Property(x => x.Content).IsOptional();
            Property(x => x.Header).IsOptional();
            Property(x => x.PublishDate).IsOptional();
            //Property(x => x.ImagePath).IsOptional();

            HasRequired(x => x.SubCategory)
                .WithMany(x => x.Articles)
                .HasForeignKey(x => x.SubCategoryID)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.AppUser)
                .WithMany(x => x.Articles)
                .HasForeignKey(x => x.AppUserID)
                .WillCascadeOnDelete(false);

            HasMany(x => x.Comments)
                .WithRequired(x => x.Article)
                .HasForeignKey(x => x.ArticleID)
                .WillCascadeOnDelete(false);

            HasMany(x => x.Likes)
               .WithRequired(x => x.Article)
               .HasForeignKey(x => x.ArticleID)
               .WillCascadeOnDelete(false);
        }
    }
}
