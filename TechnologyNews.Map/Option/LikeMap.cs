using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyNews.Core.Map;
using TechnologyNews.Model.Option;

namespace TechnologyNews.Map.Option
{
    public class LikeMap:CoreMap<Like>
    {
        public LikeMap()
        {
            ToTable("dbo.Likes");

            HasRequired(x => x.Article)
                .WithMany(x => x.Likes)
                .HasForeignKey(x => x.ArticleID)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.AppUser)
               .WithMany(x => x.Likes)
               .HasForeignKey(x => x.AppUserID)
               .WillCascadeOnDelete(false);
        }
    }
}
