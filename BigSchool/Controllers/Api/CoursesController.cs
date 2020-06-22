using bigschool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bigschool.Controllers.API
{
	public class CouresesController : ApiController
	{
		public ApplicationDbContext _dbContext { get; set; }
		public CouresesController()
		{
			_dbContext = new ApplicationDbContext();
		}

		[HttpDelete]
		public IHttpActionResult Cancel(int id)
		{
			var userId = User.Identity.GetUserId();
			var course = _dbContext.Courses.Single(c => c.Id == id && c.LecturerId == userId);
			if (course.IsCanceled)
				return NotFound();
			course.IsCanceled = true;
			_dbContext.SaveChanges();
			return Ok();
		}
	}
}
