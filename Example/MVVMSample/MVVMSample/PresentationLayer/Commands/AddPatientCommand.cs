using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MVVMSample.BusinessLogicLayer;
using MVVMSample.PresentationLayer.Models;
using System.Windows;

namespace MVVMSample.PresentationLayer.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class AddPatientCommand : ICommand
    {
        #region ICommand Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            PatientManager patientManager = new PatientManager();
            PatientDetailModel patientDetail = parameter as PatientDetailModel;

            if (patientManager.Add(new PatientDetailModel { Id=patientDetail.Id, Name=patientDetail.Name }))
                MessageBox.Show("Patient Add Successful !");
            else
                MessageBox.Show("Patient with this ID already exists !");
        }

        #endregion
    }
}
