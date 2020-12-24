using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CourseManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public List<Course> OwnCourses { get; set; }
        public List<UserCourse> UserCourses { get; set; }
    }
}
