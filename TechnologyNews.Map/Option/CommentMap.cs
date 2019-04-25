using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyNews.Core.Map;
using TechnologyNews.Model.Option;

namespace TechnologyNews.Map.Option
{
    public class CommentMap:CoreMap<Comment>
    {
        public CommentMap()
        {
            ToTable("dbo.Comments");
            Property(x => x.Content).IsOptional();

            HasRequired(x => x.Article)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.ArticleID)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.AppUser)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.AppUserID)
                .WillCascadeOnDelete(false);
        }
    }
}
