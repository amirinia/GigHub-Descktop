using MyGigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace MyGigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult WhoFollowing()
        {
            var userId = User.Identity.GetUserId();
            var artists = _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();


            ViewBag.Heading = "Following";

            ViewBag.isAuthenticated = User.Identity.IsAuthenticated;
            return View(artists);
        }

        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var upcomingGigs = _context.Gigs
            .Include(g => g.Genre)
                .Include(g => g.Artist)
                .Where(g => g.DateTime > DateTime.Now);

            ViewBag.Heading = "UpComing Gigs";

            ViewBag.isAuthenticated = User.Identity.IsAuthenticated;
            return View("Gigs",upcomingGigs);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult TimesheetList()
        {
            ViewBag.Message = "Your List:";

            var userId = User.Identity.GetUserId();
            var myTimesheet = _context.Timesheets
                .Include(g => g.Artist)
                .Where(g => g.ArtistId == userId);

            return View(myTimesheet);
        }

    }
}