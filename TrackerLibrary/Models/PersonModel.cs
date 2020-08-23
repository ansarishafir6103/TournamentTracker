using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// represents on person.
    /// </summary>
    public class PersonModel
    {
        /// <summary>
        /// the unique identity for Person.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// the first name of the person.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// the last name of the person.
        /// </summary>
        public string  LastName { get; set; }
        /// <summary>
        /// the primary email address of the person.
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// the primary cell phone number of the person.
        /// </summary>
        public string CellPhoneNumber { get; set; }
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }    
        }

    }
}
