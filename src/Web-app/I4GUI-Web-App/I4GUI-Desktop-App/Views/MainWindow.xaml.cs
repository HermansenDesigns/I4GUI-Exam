using System;
using System.Windows;
using I4GUI_Desktop_App.ViewModels;
using LiveCharts;
using LiveCharts.Wpf;

namespace I4GUI_Desktop_App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = MainViewModel.Instance;
        }

    }
}
