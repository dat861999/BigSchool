using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class LecturersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public LecturersController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET: Lecturers
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var lecturers = _dbContext.Followings
                .Where(a => a.FollowerId == userId)
                .Select(a => a.Followee)
                .ToList();
            var viewModel = new FollowingViewModel
            {
                UpcomingLecturers = lecturers,
                ShowAction = User.Identity.IsAuthenticated
            };
            ViewBag.Followings = _dbContext.Followings.ToList();
            return View(viewModel);
        }
    }
}