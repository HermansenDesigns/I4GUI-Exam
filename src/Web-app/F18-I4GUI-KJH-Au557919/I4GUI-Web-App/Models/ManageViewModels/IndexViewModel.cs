using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace I4GUI_Web_App.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [Display(Name = "First name")]
        [StringLength(100, ErrorMessage = "The {0} must be atleast {2} and at max {1} characters long.", MinimumLength = 6)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [StringLength(100, ErrorMessage = "The {0} must be atleast {2} and at max {1} characters long.", MinimumLength = 6)]
        public string LastName { get; set; }

        [Required]
        [Range(0, 99999, ErrorMessage = "The DBF controlnumber must be between 00000 and 99999")]
        [Display(Name = "Dansk Biavler Forening (DBF) kontrolnummer")]
        public int DBF { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(100, ErrorMessage = "The {0} must be atleast {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Address { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be atleast {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "AlternativeAddress")]
        public string AlternativeAddress { get; set; }
        [Required]
        [Display(Name = "ZipCode")]
        public string ZipCode { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string StatusMessage { get; set; }
    }
}
