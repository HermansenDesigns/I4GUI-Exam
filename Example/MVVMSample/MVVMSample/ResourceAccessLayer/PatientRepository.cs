using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMSample.PresentationLayer.Models;

namespace MVVMSample.ResourceAccessLayer
{
    /// <summary>
    /// PatientRepository provides mechanism to interact with storage.
    /// Uses a temp collection for storage. Can be extended to store in DB.
    /// </summary>
    public class PatientRepository
    {
        //Maintains the patient collection locally
        private static List<Patient> patients = new List<Patient>();

        /// <summary>
        /// Add a patient 
        /// </summary>
        internal void Add(Patient patient)
        {
            patients.Add(patient);
        }

        /// <summary>
        /// Remove a patient based on 
        /// </summary>
        /// <param name="patient">Patient to remove</param>
        internal void Remove(Patient patient)
        {
            patients.Remove(patient);
        }

        /// <summary>
        /// Search for the patient with Patient ID
        /// </summary>
        /// <param name="id">Patient ID</param>
        /// <returns></returns>
        internal Patient Search(int id)
        {
            //Get the patients index in the collection
            int index = GetIndex(id);
            //If present then return the element
            if (index > -1)
                return patients[index];
            return null;
        }

        /// <summary>
        /// Search for the patient ID in the collection and return the Index
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private int GetIndex(int id)
        {
            int index = -1;
            //If Collection has Items
            if (patients.Count > 0)
            {
                //Loop through the collection
                for (int i = 0; i < patients.Count; i++)
                {
                    //If match
                    if (patients[i].Id == id)
                    {
                        index = i;
                        break;
                    }
                }
            }
            return index;
        }
    }
}
