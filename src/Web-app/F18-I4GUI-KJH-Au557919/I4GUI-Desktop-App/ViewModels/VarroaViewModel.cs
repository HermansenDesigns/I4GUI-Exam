using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using I4GUI_Desktop_App.Annotations;
using I4GUI_Desktop_App.Models;
using I4GUI_Desktop_App.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace I4GUI_Desktop_App.ViewModels
{
    public class VarroaViewModel : ObservableCollection<Hive>, INotifyPropertyChanged
    {

        public VarroaViewModel()
        {
            SearchableHives = this;
            Add(new Hive()
            {
                BeeHive = "Hey",
                Comments = "Hey",
                DateOfCount = new DateTime(2000, 1, 1),
                Amount = 100
            });

            Add(new Hive()
            {
                BeeHive = "Hey",
                Comments = "Hey",
                DateOfCount = new DateTime(2000, 1, 1),
                Amount = 103
            });
            Add(new Hive()
            {
                BeeHive = "Der",
                Comments = "Der",
                DateOfCount = new DateTime(2000, 1, 1),
                Amount = 102
            });

            Add(new Hive()
            {
                BeeHive = "Der",
                Comments = "Der",
                DateOfCount = new DateTime(2000, 1, 2),
                Amount = 104
            });
        }

        #region Commands


        private ICommand _addCommand;
        public ICommand AddCommand => _addCommand ?? (_addCommand = new RelayCommand(AddHive));

        private void AddHive()
        {
            var hive = new Hive();
            Add(hive);
            CurrentHive = hive;
            NotifyPropertyChanged();
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(DeleteHive, DeleteHive_CanExecute));

        private bool DeleteHive_CanExecute()
        {
            return Count > 0 && CurrentIndex >= 0;
        }

        private void DeleteHive()
        {
            Remove(CurrentHive);
            NotifyPropertyChanged();
        }

        private ICommand _duplicateCommand;
        public ICommand DuplicateCommand => _duplicateCommand ?? (_duplicateCommand = new RelayCommand(DuplicateHive, DuplicateHive_CanExecute));

        private bool DuplicateHive_CanExecute()
        {
            return CurrentHive != null;
        }

        private void DuplicateHive()
        {
            Add(CurrentHive);
            NotifyPropertyChanged();
        }

        private ICommand _searchCommand;
        public ICommand SearchCommand => _searchCommand ?? (_searchCommand = new RelayCommand(Search, Search_CanExecute));

        private bool Search_CanExecute()
        {
            return !string.IsNullOrEmpty(Filter);
        }

        private void Search()
        {
            var hives = new ObservableCollection<Hive>(SearchableHives.Where(i => i.BeeHive.Contains(_filter)));
            SearchableHives = hives;
            NotifyPropertyChanged();
        }

        private ICommand _resetCommand;
        public ICommand ResetCommand => _resetCommand ?? (_resetCommand = new RelayCommand(Reset));

        private void Reset()
        {
            SearchableHives = this;
            NotifyPropertyChanged();
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(Save));

        private void Save()
        {
            string output = JsonConvert.SerializeObject(this);

            using (StreamWriter sw = new StreamWriter(@"../save.json"))
            {
                sw.Write(output);
            }
        }

        private ICommand _loadCommand;
        public ICommand LoadCommand => _loadCommand ?? (_loadCommand = new RelayCommand(Load));

        private void Load()
        {
            string input;

            using (StreamReader sr = new StreamReader(@"../save.json"))
            {
                input = sr.ReadToEnd();
            }
            this.ClearItems();
            var list = JsonConvert.DeserializeObject<List<Hive>>(input);

            foreach (var hive in list)
            {
                Add(hive);
            }
        }

        #endregion

        #region Properties

        private int _currentIndex;
        private Hive _currentHive;
        private ObservableCollection<Hive> _searchableHives;
        private string _filter;


        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                if (_currentIndex != value)
                {
                    _currentIndex = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Hive CurrentHive
        {
            get => _currentHive;
            set
            {
                if (_currentHive != value)
                {
                    _currentHive = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<Hive> SearchableHives
        {
            get => _searchableHives;
            set
            {
                if (_searchableHives != value)
                {
                    _searchableHives = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Filter
        {
            get => _filter;
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties Changes

        public new event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
