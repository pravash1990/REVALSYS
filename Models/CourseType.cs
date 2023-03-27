using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class CourseType
    {
        public int CourseTypeId { get; set; }
        [Required]
        public string? CourseName { get; set; }
        public string? Description { get; set; }
    }
}
