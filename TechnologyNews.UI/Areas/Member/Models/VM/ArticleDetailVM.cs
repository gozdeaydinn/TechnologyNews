using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnologyNews.Model.Option;

namespace TechnologyNews.UI.Areas.Member.Models.VM
{
    public class ArticleDetailVM
    {
        public ArticleDetailVM()
        {
            AppUsers = new List<AppUser>();
            Comments = new List<Comment>();
            Likes = new List<Like>();
            Article = new Article();
            AppUser = new AppUser();
        }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }

        public List<Comment> Comments { get; set; }
        public List<AppUser> AppUsers { get; set; }
        public List<Like> Likes { get; set; }

        public Article Article { get; set; }
        public AppUser AppUser { get; set; }
    }
}