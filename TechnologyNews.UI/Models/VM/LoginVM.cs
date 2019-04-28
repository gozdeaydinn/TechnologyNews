using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechnologyNews.UI.Models.VM
{
    public class LoginVM
    {
        [Required(ErrorMessage = "User Name Error!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password Error!")]
        public string Password { get; set; }
    }
}