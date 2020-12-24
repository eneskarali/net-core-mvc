using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Models
{
    public class Assigment
    {
        [Key]
        public int AssigmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
