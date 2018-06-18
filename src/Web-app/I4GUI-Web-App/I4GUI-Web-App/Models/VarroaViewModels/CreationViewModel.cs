using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4GUIWebApp.Models;

namespace I4GUIWebApp.Models.VarroaViewModels
{
    public class CreationViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Beehive")]
        [StringLength(18, ErrorMessage = "The {0} must be atleast {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Beehive { get; set; }

        [Required]
        [Display(Name = "Date of count")]
        public DateTime DOC { get; set; }

        [Required]
        [Display(Name = "Number of Varroa")]
        public int Varroa { get; set; }
        [Required]
        [Display(Name = "Observation length")]
        public int ObservationLength { get; set; }
        [Display(Name = "Comments")]
        public string Comments { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
