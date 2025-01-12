﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace I4GUI_Web_App.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DBF { get; set; }
        public string Address { get; set; }
        public string AlternativeAddress { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}
