using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMSample.PresentationLayer.Models;
using System.ComponentModel;
using System.Windows.Input;
using MVVMSample.Common;
using MVVMSample.BusinessLogicLayer;
using System.Windows;
using System.Collections.ObjectModel;

namespace MVVMSample.PresentationLayer.ViewModels
{
    /* 
     * Observe that nothing is written in Code Behind. Every thing is moved to View Model. 
     * Writing every thing in view model which has nothing but normal methods and properties with all data centralised 
     * will help writing unit tests very simple
     */
    /// <summary>
    /// PatientDetailViewModel provides all the Data and Operations that will be used by the View.
    /// </summary>
    public class PatientDetailViewModel: INotifyPropertyChanged
    {
        #region Private Variables
        /*The Variables are meant to be readonly as we mustnot change the address of any of them by creating new instances.
         *Problem with new istances is that since address changes the binding becomes invalid.
         *Instantiate all the variables in the constructor.
         */
        private readonly Patient domObject;
        private readonly PatientManager patientManager;
        private readonly ObservableCollection<Patient> _patients;
        private readonly ICommand _addPatientCmd;
        private readonly ICommand _deletePatientCmd;
        private readonly ICommand _searchPatientCmd;
        #endregion

        #region Constructors

        /// <summary>
        /// Instatiates all the readonly variables
        /// </summary>
        public PatientDetailViewModel()
        {
            domObject = new Patient();
            patientManager = new PatientManager();
            
            _patients = new ObservableCollection<Patient>();
            _addPatientCmd = new RelayCommand(Add, CanAdd);
            _deletePatientCmd = new RelayCommand(Delete, CanDelete);
            _searchPatientCmd = new RelayCommand(Search, CanSearch);
        }
        #endregion

        #region Properties
        
        /// <summary>
        /// Gets or Sets Patient Id. Ready to be binded to UI.
        /// Impelments INotifyPropertyChanged which enables the binded element to refresh itself whenever the value changes.
        /// </summary>
        public int Id
        {
            get { return domObject.Id; }
            set
            {
                domObject.Id = value;
                OnPropertyChanged("Id");
            }
        }

        /// <summary>
        /// Gets or Sets Patient Name. Ready to be binded to UI.
        /// Impelments INotifyPropertyChanged which enables the binded element to refresh itself whenever the value changes.
        /// </summary>
        public string Name
        {
            get { return domObject.Name; }
            set
            {
                domObject.Name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets or Sets Patient MobileNumber. Ready to be binded to UI.
        /// Impelments INotifyPropertyChanged which enables the binded element to refresh itself whenever the value changes.
        /// </summary>
        public Int64 MobileNumber
        {
            get { return domObject.MobileNumber; }
            set
            {
                domObject.MobileNumber = value;
                OnPropertyChanged("MobileNumber");
            }
        }

        /// <summary>
        /// Gets the Patients. Used to maintain the Patient List.
        /// Since this observable collection it makes sure all changes will automatically reflect in UI 
        /// as it implements both CollectionChanged, PropertyChanged;
        /// </summary>
        public ObservableCollection<Patient> Patients { get { return _patients; } }

        /// <summary>
        /// Sets the Selected Patient. Used to identify the selected patient from the list. 
        /// </summary>
        public Patient SelectedPatient 
        { 
            set { Id = value.Id; 
                  MobileNumber = value.MobileNumber; 
                  Name = value.Name; } 
        }

        #region Commands

        /// <summary>
        /// Gets the AddPatientCommand. Used for Add patient Button Operations
        /// </summary>
        public ICommand AddPatientCmd { get { return _addPatientCmd; } }

        /// <summary>
        /// Gets the DeletePatientCmd. Used for Delete patient Button Operations
        /// </summary>
        public ICommand DeletePatientCmd { get { return _deletePatientCmd; } }

        /// <summary>
        /// Gets the SearchPatientCmd. Used for Search patient Button Operations
        /// </summary>
        public ICommand SearchPatientCmd { get { return _searchPatientCmd; } }
        #endregion

        #endregion

        #region Commands

        #region AddCommand

        /// <summary>
        /// CanAdd operation of the AddPatientCmd.
        /// Tells us if the control is to be enabled or disabled.
        /// This method will be fired repeatedly as long as the view is open.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CanAdd(object obj)
        {
            //Enable the Button only if the mandatory fields are filled
            if (Name != string.Empty && MobileNumber!=0)
                return true;
            return false;
        }

        /// <summary>
        /// Add operation of the AddPatientCmd.
        /// Operation that will be performormed on the control click.
        /// </summary>
        /// <param name="obj"></param>
        public void Add(object obj)
        {
            //Always create a new instance of patient before adding. 
            //Otherwise we will endup sending the same instance that is binded, to the BL which will cause complications
            var patient = new Patient { Id = Id, Name = Name, MobileNumber=MobileNumber };
            //Add patient will be successfull only if the patient with same ID does not exist.
            if (patientManager.Add(patient))
            {
                Patients.Add(patient);
                ResetPatient();
                MessageBox.Show("Patient Add Successful !");
            }
            else
                MessageBox.Show("Patient with this ID already exists !");
        }
        #endregion

        #region DeleteCommand

        /// <summary>
        /// CanDelete operation of the DeletePatientCmd.
        /// Tells us if the control is to be enabled or disabled.
        /// This method will be fired repeatedly as long as the view is open.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanDelete(object obj)
        {
            //Enable the Button only if the patients exist
            if(Patients.Count>0)
                return true;
            return false;
        }

        /// <summary>
        /// Delete operation of the DeletePatientCmd.
        /// Operation that will be performormed on the control click.
        /// </summary>
        /// <param name="obj"></param>
        private void Delete(object obj)
        {
            //Delete patient will be successfull only if the patient with this ID exists.
            if (!patientManager.Remove(Id))
                MessageBox.Show("Patient with this ID does not exist !");
            else
            {
                //Remove the patient from our collection as well.
                Patients.RemoveAt(GetIndex(Id));
                ResetPatient();
                MessageBox.Show("Patient Remove Successful !");
            }
        }

        #endregion

        #region SearchCommand

        /// <summary>
        /// CanSearch operation of the SearchPatientCmd.
        /// Tells us if the control is to be enabled or disabled.
        /// This method will be fired repeatedly as long as the view is open.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanSearch(object obj)
        {
            //Enable the Button only if the patients exist
            if(Patients.Count>0)
                return true;
            return false;
        }

        /// <summary>
        /// Search operation of the SearchPatientCmd.
        /// Operation that will be performormed on the control click.
        /// </summary>
        /// <param name="obj"></param>
        private void Search(object obj)
        {
            Patient patient = patientManager.Search(Id);

            if (patient == null)
                MessageBox.Show("Patient with this ID does not exist !");
            else
            {
                //Change the binded elements so that the searched patient will reflect in UI
                Id = patient.Id;
                Name = patient.Name;
                MobileNumber = patient.MobileNumber;
            }
        }
        #endregion
        #endregion

        #region Private Methods

        /// <summary>
        /// Resets the Patient properties which will in turn auto reset the UI aswell
        /// </summary>
        private void ResetPatient()
        {
            Id = 0;
            Name = string.Empty;
            MobileNumber = 0;
        }

        /// <summary>
        /// Finds the patient in the collection and return the index
        /// </summary>
        /// <param name="Id">Patient ID</param>
        /// <returns></returns>
        private int GetIndex(int Id)
        {
            for (int i = 0; i < Patients.Count; i++)
                if (Patients[i].Id == Id)
                    return i;
            return -1;
        }
        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Event to which the view's controls will subscribe.
        /// This will enable them to refresh themselves when the binded property changes provided you fire this event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// When property is changed call this method to fire the PropertyChanged Event
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            //Fire the PropertyChanged event in case somebody subscribed to it
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
