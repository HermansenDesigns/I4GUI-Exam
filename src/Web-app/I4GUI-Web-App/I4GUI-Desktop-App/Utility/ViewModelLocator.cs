using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4GUI_Desktop_App.ViewModels;

namespace I4GUI_Desktop_App.Utility
{
    public class ViewModelLocator
    {
        public VarroaViewModel VarroaViewModel { get; set; } = new VarroaViewModel();
    }
}
