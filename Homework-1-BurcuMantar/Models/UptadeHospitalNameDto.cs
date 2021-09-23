using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_1_BurcuMantar.Models
{ //Created DTO class with one property for Using HttpPatch request
    public class UptadeHospitalNameDto
    {
        [Required]
        public string HospitalName { get; set; }
    }
}
