using CourseManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Repositories
{
    public interface ICourseRepository
    {
        Task<int> CreateCourseAsync(Course course);
        List<Course> GetUserCourses(string userId);
        List<Course> GetUserOwnCourses(string userId);
        Task<int> JoinCourseWithId(Course course, ApplicationUser user);
        Course GetCourseById(int courseId);
        Task<int> DeleteCourse(int courseId);
        Task<int> EditCourse(CreateCourseViewModel course, int id);
        List<Course> SearcCourse(string id, string name);
    }
}
