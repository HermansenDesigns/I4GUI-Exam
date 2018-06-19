using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4GUI_Desktop_App.ViewModels
{
    public class MainViewModel
    {
        public VarroaViewModel VarroaViewModel { get; set; }
        public ChartViewModel ChartViewModel { get; set; }

        private static MainViewModel _instance = null;
        public static MainViewModel Instance => _instance ?? (_instance = new MainViewModel());

        private MainViewModel()
        {
            VarroaViewModel = new VarroaViewModel();
            ChartViewModel = new ChartViewModel();
        }
    }
}
