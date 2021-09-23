using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_1_BurcuMantar.Models
{ 
        //Model and Validation for Entities
    public class DoctorDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="This area is required")]
        [MinLength(3),MaxLength(30)] 
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This area is required"), MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("(M|F|N)", ErrorMessage = "You can enter only one character that is  ' M ' (Male) or ' F '(Female) or ' N ' (Not defined).")]
        public string Gender { get; set; }
        public string HospitalName { get; set; }

        [Required(ErrorMessage = "This area is required"), MaxLength(50)]
        public string Clinic { get; set; }
       
    }
}
