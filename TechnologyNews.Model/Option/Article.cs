using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyNews.Core.Entity;

namespace TechnologyNews.Model.Option
{
    public class Article:CoreEntity
    {
        public string Header { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public DateTime? PublishDate { get; set; }

        public Guid SubCategoryID { get; set; }
        public virtual SubCategory SubCategory { get; set; }

        public Guid AppUserID { get; set; }
        public virtual AppUser AppUser { get; set; }

        public virtual List<Comment> Comments { get; set; }
        public virtual List<Like> Likes { get; set; }
    }
}
