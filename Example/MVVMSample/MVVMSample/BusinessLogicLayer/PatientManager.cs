using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMSample.ResourceAccessLayer;
using MVVMSample.PresentationLayer.Models;

namespace MVVMSample.BusinessLogicLayer
{
    /// <summary>
    /// Implements Business Logic related to Patient.
    /// </summary>
    public class PatientManager
    {
        readonly PatientRepository patientRepository;
        
        /// <summary>
        /// Initialises all the private variables
        /// </summary>
        public PatientManager()
        {
            patientRepository = new PatientRepository();
        }
        
        /// <summary>
        /// Add Patient
        /// </summary>
        /// <param name="patient">Patient to add</param>
        /// <returns></returns>
        public bool Add(Patient patient)
        {
            //Search if the patient exists and if not add the patient.
            if (patientRepository.Search(patient.Id) == null)
            {
                patientRepository.Add(patient);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove Patient
        /// </summary>
        /// <param name="id">Patient ID</param>
        /// <returns></returns>
        public bool Remove(int id)
        {
            //Search if the patient exists and if exists remove the patient.
            Patient patient = patientRepository.Search(id);
            if (patient != null)
            {
                patientRepository.Remove(patient);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Search for a patient
        /// </summary>
        /// <param name="id">Patient ID</param>
        /// <returns></returns>
        public Patient Search(int id)
        {
            return patientRepository.Search(id);
        }
    }
}
