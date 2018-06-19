using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using I4GUI_Desktop_App.Annotations;
using I4GUI_Desktop_App.Models;
using I4GUI_Desktop_App.Utility;
using LiveCharts;
using LiveCharts.Wpf;

namespace I4GUI_Desktop_App.ViewModels
{
    public class ChartViewModel : INotifyPropertyChanged
    {
        public ChartViewModel()
        {
        }


        private SeriesCollection _seriesCollection;
        private List<string> _labes;
        private Func<double, string> _formatter;

        public SeriesCollection SeriesCollection
        {
            get => _seriesCollection;
            set
            {
                if (_seriesCollection == value) return;
                _seriesCollection = value;
                OnPropertyChanged();
            }
        }

        public List<string> Labels
        {
            get => _labes;
            set
            {
                if (_labes == value) return;
                _labes = value;
                OnPropertyChanged();
            }

        }
        public Func<double, string> Formatter
        {
            get => _formatter;
            set
            {
                if (_formatter == value) return;
                _formatter = value;
                OnPropertyChanged();
            }
        }

        private ICommand _calculateGraphCommand;
        public ICommand CalculateGraphCommand => _calculateGraphCommand ?? (_calculateGraphCommand = new RelayCommand(CalculateGraph));

        private void CalculateGraph()
        {
            Labels = new List<string>();
            var mainViewModel = MainViewModel.Instance;

            var hives = mainViewModel.VarroaViewModel.ToList();

            var names = new List<string>();
            var dates = new List<DateTime>();


            foreach (var hive in hives)
            {
                names.Add(hive.BeeHive);
                dates.Add(hive.DateOfCount);
            }

            names = names.Distinct().ToList();
            names.Sort();

            dates = dates.Distinct().ToList();
            dates.Sort();

            SeriesCollection = new SeriesCollection();

            dates.ForEach((i) => {Labels.Add(i.ToShortDateString());});

            var listOfHives = new Dictionary <string, List<Hive>>();
            foreach (var name in names)
            {
                listOfHives.Add(name, hives.Where(i => i.BeeHive == name).ToList());
            }

            foreach (var value in listOfHives.Values)
            {
                var listOfValues = new List<double>();
                foreach (var hive in value)
                {
                    listOfValues.Add(hive.Amount);
                }

                SeriesCollection.Add(new ColumnSeries()
                {
                    Title = value[0].BeeHive,
                    Values = new ChartValues<double>(listOfValues)
                });
            }

            Formatter = value => value.ToString("N");
        }





        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
