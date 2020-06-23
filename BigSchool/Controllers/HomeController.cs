using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigSchool.Models;
using System.Data.Entity;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;

namespace BigSchool.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;

        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var upcomingCourses = _dbContext.Courses
                .Include(c => c.Lecturer)
                .Include(c => c.Category)
                .Where(c => c.DateTime > DateTime.Now && c.IsCanceled == false);
            var viewModel = new CoursesViewModel
            {
                UpcomingCourses = upcomingCourses,
                ShowAction = User.Identity.IsAuthenticated
            };
            ViewBag.Attendings = _dbContext.Attendances.ToList();
            ViewBag.Followings = _dbContext.Followings.ToList();
            return View(viewModel);
        }

        [Authorize]
        public ActionResult BSFeed()
        {
            var upcomingCourses = _dbContext.Courses
                .Include(c => c.Lecturer)
                .Include(c => c.Category)
                .Where(c => c.DateTime > DateTime.Now && c.IsCanceled == false);

            var viewModel = new CoursesViewModel
            {
                UpcomingCourses = upcomingCourses,
                ShowAction = User.Identity.IsAuthenticated
            };

            var userId = User.Identity.GetUserId();

            ViewBag.Followees = _dbContext.Followings
                .Where(a => a.FollowerId == userId)
                .ToList();
            ViewBag.Attendings = _dbContext.Attendances.ToList();
            ViewBag.Followings = _dbContext.Followings.ToList();
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Search(string searchStr)
        {
            var upcomingCourses = _dbContext.Courses
               .Include(c => c.Lecturer)
               .Include(c => c.Category)
               .Where(c => c.DateTime > DateTime.Now && c.IsCanceled == false && c.Lecturer.Name.Contains(searchStr)
               || c.Place.Contains(searchStr) && c.DateTime > DateTime.Now && c.IsCanceled == false
               || c.Category.Name.Contains(searchStr) && c.DateTime > DateTime.Now && c.IsCanceled == false);
            var viewModel = new CoursesViewModel
            {
                UpcomingCourses = upcomingCourses,
                ShowAction = User.Identity.IsAuthenticated
            };
            ViewBag.Attendings = _dbContext.Attendances.ToList();
            ViewBag.Followings = _dbContext.Followings.ToList();
            return View("Index", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        } 
    }
}