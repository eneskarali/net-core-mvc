using CourseManagementSystem.Models;
using CourseManagementSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IAccountRepository _accountRepository;

        public CourseController(ICourseRepository courseRepository, IAccountRepository accountRepository)
        {
            _courseRepository = courseRepository;
            _accountRepository = accountRepository;
        }

        [Authorize]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [Authorize]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseViewModel model)
        {
            var currentUser = User;
            var currentUserEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var user = await _accountRepository.FindByEmailAsync(currentUserEmail);

            var newCourse = new Course
            {
                Instructor = user,
                Name = model.Name,
                Description = model.Description
            };

            var result = await _courseRepository.CreateCourseAsync(newCourse);

            if (result < 1)
            {
                ModelState.AddModelError("", "Could not create!");
                return View();
            }
            ModelState.Clear();
            return RedirectToAction("Courses", "Course");
        }

        [Authorize]
        [Route("Join")]
        public IActionResult Join()
        {
            return View();
        }

        [Authorize]
        [Route("Join")]
        [HttpPost]
        public async Task<IActionResult> Join(JoinCourseViewModel courseModel)
        {
            var currentUser = User;
            var currentUserEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var user = await _accountRepository.FindByEmailAsync(currentUserEmail);

            var course = _courseRepository.GetCourseById(courseModel.Id);

            if(course.InstructorId == user.Id)
            {
                ModelState.AddModelError("", "You cant join your own course as a student!");
                return View();
            }

            var result = await _courseRepository.JoinCourseWithId(course, user);

            if (result < 1)
            {
                ModelState.AddModelError("", "Could not create!");
                return View();
            }

            ModelState.Clear();
            return RedirectToAction("JoinedCourses", "Course");


        }

        [Authorize]
        public async Task<IActionResult> Courses()
        {
            var currentUser = User;
            var currentUserEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var user = await _accountRepository.FindByEmailAsync(currentUserEmail);

            var courseList = _courseRepository.GetUserOwnCourses(user.Id);

            user.OwnCourses = courseList;
            return View(user);

        }

        [Authorize]
        public async Task<IActionResult> JoinedCourses()
        {
            var currentUser = User;
            var currentUserEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var user = await _accountRepository.FindByEmailAsync(currentUserEmail);

            var courseList = _courseRepository.GetUserCourses(user.Id);

            user.OwnCourses = courseList;
            return View(user);

        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _courseRepository.DeleteCourse(id);
            if (result < 1)
            {
                ModelState.AddModelError("", "Could not delete!");
                return View();
            }

            ModelState.Clear();
            return RedirectToAction("Courses", "Course");
        }

        [Authorize]
        [Route("EditCourse")]
        public IActionResult EditCourse(int id)
        {
            ViewBag.id = id;

            var course = _courseRepository.GetCourseById(id);

            CreateCourseViewModel courseModel = new CreateCourseViewModel
            {
                Name = course.Name,
                Description = course.Description
            };
            return View(courseModel);
        }

        [Authorize]
        [Route("EditCourse")]
        [HttpPost]
        public async Task<IActionResult> EditCourse(CreateCourseViewModel courseModel, int id)
        {
            var result = await _courseRepository.EditCourse(courseModel, id);

            if (result < 1)
            {
                ModelState.AddModelError("", "Could not edit!");
                return View();
            }

            ModelState.Clear();
            return RedirectToAction("Courses", "Course");
        }

        [Authorize]
        [HttpPost]
        [Route("Search")]
        public async Task<IActionResult> Search(string id, string data)
        {

            if(data == null )
            {
                return RedirectToAction("Courses", "Course");
            }
            var currentUser = User;
            var currentUserEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var user = await _accountRepository.FindByEmailAsync(currentUserEmail);

            var courseList = _courseRepository.SearcCourse(id, data);

            user.OwnCourses = courseList;
            return View(user);
        }
    }
}
