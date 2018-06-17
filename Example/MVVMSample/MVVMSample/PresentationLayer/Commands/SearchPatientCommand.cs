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
    public class SearchPatientCommand : ICommand
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
            PatientDetailModel patientDetail = parameter as PatientDetailModel;

            PatientDetailModel patient = new PatientManager().Search(patientDetail.Id);

            if (patient == null)
                MessageBox.Show("Patient with this ID does not exist !");
            else
            {
                patientDetail.Id = patient.Id;
                patientDetail.Name = patient.Name;
            }
        }

        #endregion
    }
}
