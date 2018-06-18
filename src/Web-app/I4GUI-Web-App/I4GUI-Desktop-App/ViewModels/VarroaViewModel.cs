using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using I4GUI_Desktop_App.Annotations;
using I4GUI_Desktop_App.Models;
using I4GUI_Desktop_App.Utility;

namespace I4GUI_Desktop_App.ViewModels
{
    public class VarroaViewModel : ObservableCollection<Hive>, INotifyPropertyChanged
    {

        public VarroaViewModel()
        {
            Add(new Hive()
            {
                BeeHive = "Hey",
                Comments = "Hey",
                DateOfCount = DateTime.Now,
                Amount = 100
            });
            Add(new Hive()
            {
                BeeHive = "Der",
                Comments = "Der",
                DateOfCount = DateTime.Now,
                Amount = 102
            });
        }

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

        #region Properties

        private int _currentIndex;
        private Hive _currentHive;

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
