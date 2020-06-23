using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool.Controllers.Api
{
    public class AttendingController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;
        public AttendingController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult UnAttend(int id)
        {
            var userId = User.Identity.GetUserId();
            var attendance = _dbContext.Attendances.Single(a => a.CourseId == id && a.AttendeeId == userId);

            _dbContext.Attendances.Remove(attendance);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
