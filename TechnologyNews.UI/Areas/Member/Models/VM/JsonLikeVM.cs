using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnologyNews.UI.Areas.Member.Models.VM
{
    public class JsonLikeVM
    {
        public string userMessage { get; set; }
        public int Likes { get; set; }
        public bool isSuccess { get; set; }
        public int Comments { get; set; }
    }
}