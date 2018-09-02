using Microsoft.AspNet.Identity;
using MyGigHub.Models;
using MyGigHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MyGigHub.Controllers
{
    public class TimesheetController : Controller
    {
        private ApplicationDbContext _context;
        public TimesheetController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Timesheet
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var myTimesheet = _context.Timesheets
                .Include(g => g.Artist)
                .Where(g => g.ArtistId == userId);

            return View(myTimesheet);
        }

        // GET: Gig
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new TimesheetFormViewModel();
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(TimesheetFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Create", viewModel);
            var timesheet= new Timesheet
            {
                ArtistId = User.Identity.GetUserId(),
                StartDay = viewModel.StartTime,
                //EndDay = viewModel.EndTime
            };
            _context.Timesheets.Add(timesheet);
            _context.SaveChanges();

            return RedirectToAction("ListView", "Timesheet");
        }

        [Authorize]
        public ActionResult Edit(int Id)
        {
            var std = _context.Timesheets.Where(s => s.Id == Id).FirstOrDefault();
            return View(std);
        }

        [Authorize]
        public ActionResult ListView()
        {
            var userId = User.Identity.GetUserId();
            var myTimesheet = _context.Timesheets
                .Include(g => g.Artist)
                .Where(g => g.ArtistId == userId);

            return View(myTimesheet);
        }
    }
}