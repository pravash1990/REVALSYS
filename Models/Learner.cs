using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Learner
    {
        public int LearnerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Customer Type")]
        public int CourseTypeId { get; set; }              
        public string Phone { get; set; }
        public string Email { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
    }
}
