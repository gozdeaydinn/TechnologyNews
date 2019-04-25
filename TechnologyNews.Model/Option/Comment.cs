using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyNews.Core.Entity;

namespace TechnologyNews.Model.Option
{
    public class Comment:CoreEntity
    {
        public string Content { get; set; }

        public Guid ArticleID { get; set; }
        public virtual Article Article { get; set; }

        public Guid AppUserID { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
