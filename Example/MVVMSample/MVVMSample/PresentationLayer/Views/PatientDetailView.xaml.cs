using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVMSample.PresentationLayer.Views
{
    /// <summary>
    /// Interaction logic for PatientDataView.xaml
    /// </summary>
    public partial class PatientDetailView : UserControl
    {
        public PatientDetailView()
        {
            InitializeComponent();
        }
        /* 
         * Observe that nothing is written in Code Behind. Every thing is moved to View Model. 
         * Writing every thing in view model which has nothing but normal methods and properties with all data centralised 
         * will help writing unit tests very simple
         */
    }
}
