using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MVVMSample.PresentationLayer.Models
{
    /// <summary>
    /// DOM to maintain Patient Information
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Gets or Sets Unique integer ID for the Patient
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Name of the Patient
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets MobileNumber of the Patient
        /// </summary>
        public Int64 MobileNumber { get; set; }
    }
}
