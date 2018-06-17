using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MVVMSample.PresentationLayer.Models;
using MVVMSample.BusinessLogicLayer;
using System.Windows;

namespace MVVMSample.PresentationLayer.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeletePatientCommand : ICommand
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
            
        }

        #endregion
    }
}
