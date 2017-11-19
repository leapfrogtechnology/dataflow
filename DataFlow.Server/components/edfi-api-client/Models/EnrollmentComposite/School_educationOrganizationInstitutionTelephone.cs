using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace EdFi.OdsApi.Models.EnrollmentComposite 
{
    public class School_educationOrganizationInstitutionTelephone 
    {
        /// <summary>
        /// Key for TelephoneNumber
        /// </summary>
        public string institutionTelephoneNumberType { get; set; }

        /// <summary>
        /// The telephone number including the area code, and extension, if applicable.
        /// </summary>
        public string telephoneNumber { get; set; }

        }
}
