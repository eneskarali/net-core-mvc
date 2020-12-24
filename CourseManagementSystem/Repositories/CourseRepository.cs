using CourseManagementSystem.Data;
using CourseManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext contex)
        {
            _context = contex;
        }

        public async Task<int> CreateCourseAsync(Course course)
        {
            _context.Add(course);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        public List<Course> GetUserCourses(string userId)
        {
            var courseIds = _context.UserCourses.Where(p => p.UserId == userId).Select(r => r.CourseId).ToArray();

            var courseList = (from p in _context.Courses
                         where courseIds.Contains(p.CourseId)
                         select p).ToList();

            return courseList;
        }

       public List<Course> GetUserOwnCourses(string userId)
        {
            var courseList = _context.Courses.Where(c => c.InstructorId == userId).ToList();

            return courseList;
        }

        public async Task<int> JoinCourseWithId(Course course, ApplicationUser user)
        {
            UserCourse userCourse = new UserCourse
            {
                Course = course,
                CourseId = course.CourseId,
                User = user,
                UserId = user.Id
            };

            _context.Add(userCourse);
            var result = await _context.SaveChangesAsync();
            
            return result;
        }

        public Course GetCourseById(int courseId)
        {
            var course =  _context.Courses.Find(courseId);

            return course;
        }

        public async Task<int> DeleteCourse(int courseId)
        {
           Course course = _context.Courses.Find(courseId);
            _context.Remove(course);
            var result = await _context.SaveChangesAsync();

            return result;

        }

        public async Task<int> EditCourse(CreateCourseViewModel courseModel, int id)
        {
            var data = _context.Courses.Find(id);

            data.Name = courseModel.Name;
            data.Description = courseModel.Description;

            var result = await _context.SaveChangesAsync();

            return result;

        }

        public List<Course> SearcCourse(string id, string name)
        {
            var courseList =  _context.Courses.Where(c => c.InstructorId == id && c.Name == name).ToList();
           
            return courseList;
        }
    }
}
