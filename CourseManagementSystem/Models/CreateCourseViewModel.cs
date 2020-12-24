using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Models
{
    public class CreateCourseViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description{ get; set; }
    }
}
