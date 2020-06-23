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
    public class FollowingController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;
        public FollowingController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var userId = User.Identity.GetUserId();
            var following = _dbContext.Followings.Single(f => f.FolloweeId == id && f.FollowerId == userId);

            _dbContext.Followings.Remove(following);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
