using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4GUI_Web_App.Models;

namespace I4GUIWebApp.Models
{
    public class VarroaCount
    {
        public int Id { get; set; }
        public string Beehive { get; set; }
        public DateTime DOC { get; set; }
        public int Varroa { get; set; }
        public int ObservationLength { get; set; }
        public string Comments { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
