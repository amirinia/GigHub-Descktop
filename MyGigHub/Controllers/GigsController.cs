using Microsoft.AspNet.Identity;
using MyGigHub.Models;
using MyGigHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyGigHub.Controllers
{
    public class GigsController : Controller
    {
        private ApplicationDbContext _Context;
        public GigsController()
        {
            _Context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _Context.Gigs
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now)
                .Include(g=>g.Genre)
                .ToList();

            return View(gigs);

        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _Context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a=>a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
            ViewBag.Heading = "Attending";
            ViewBag.isAuthenticated = User.Identity.IsAuthenticated;

            return View("Gigs",gigs);
        }

        // GET: Gig
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel();
            viewModel.Genres = _Context.Genres.ToList();
            return View("GigForm", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var gig = _Context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);
            var viewModel = new GigFormViewModel
            {
                Genres = _Context.Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre =gig.GenreId,
                Venue=gig.Venue
        }; 
            return View("GigForm",viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _Context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _Context.Gigs.Add(gig);
            _Context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }
    }
}