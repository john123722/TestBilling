using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace FormPractise.Models
{
    public class PatientModel
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]

        public string Gender { get; set; }
        [Required]

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]

        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]

        public DateTime CreatedDate { get; set; }
        [Required]
        public string Referal { get; set; }

       
    }
}