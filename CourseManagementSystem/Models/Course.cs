using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string InstructorId { get; set; }
        public ApplicationUser  Instructor { get; set; }
        public List<UserCourse> UserCourses { get; set; }
        public List<Assigment> Assigments { get; set; }
        
    }
}
