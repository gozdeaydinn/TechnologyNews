﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnologyNews.Model.Option;

namespace TechnologyNews.UI.Areas.Admin.Models.DTO
{
    public class AppUserDTO
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public Role Role { get; set; }
        public Gender Gender { get; set; }
        public string ImagePath { get; set; }
    }
}