using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Controllers
{
    [Route("dashboard")]
    public class WeddingController : Controller
    {
        private User LoggedIn()
        {
            return dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
        }
        private HomeContext dbContext;
        public WeddingController(HomeContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            User dbUser = LoggedIn();
            if(dbUser == null)
            {
                return RedirectToAction("Logout", "Home");
            }
            List<Wedding> Weddings = dbContext.Weddings.Include(w => w.Guests).ThenInclude(
                a => a.Guest).Include( w => w.WeddingPlanner).ToList();
            ViewBag.User = dbUser;
            return View(Weddings);
        }

        [HttpGet("new_wedding")]
        public IActionResult NewWedding()
        {
            User dbUser = LoggedIn();
            if(dbUser == null)
            {
                return RedirectToAction("Logout", "Home");
            }
            return View();
        }
        [HttpPost("createwedding")]
        public IActionResult CreateWedding(Wedding webForm)
        {
            User dbUser = LoggedIn();
                if(dbUser == null)
                {
                    return RedirectToAction("Logout", "Home");
                }
            if(ModelState.IsValid)
            {
                webForm.UserId = dbUser.UserId;
                dbContext.Weddings.Add(webForm);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("NewWedding");
            }
        }

        [HttpGet("rsvp{wedId}")]
        public IActionResult Rsvp(Association newAssoc, int wedId)
        {
            User dbUser = LoggedIn();
            if(dbUser == null)
            {
                return RedirectToAction("Logout", "Home");
            }
            newAssoc.WeddingId = wedId;
            newAssoc.Guest = dbUser;
            dbContext.Associations.Add(newAssoc);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet("delete{wedId}")]
        public IActionResult Delete(int wedId)
        {
            Wedding dbWedding = dbContext.Weddings.FirstOrDefault(w => w.WeddingId == wedId);
            dbContext.Weddings.Remove(dbWedding);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet("un_rsvp{wedId}")]
        public IActionResult UnRsvp(int wedId)
        {
            Association delAssoc = dbContext.Associations.FirstOrDefault(a => a.WeddingId == wedId);
            dbContext.Associations.Remove(delAssoc);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet("wedding/{wedId}")]
        public IActionResult WeddingPage(int wedId)
        {
            User dbUser = LoggedIn();
            if(dbUser == null)
            {
                return RedirectToAction("Logout", "Home");
            }
            Wedding dbWedding = dbContext.Weddings.Include(w => w.Guests).ThenInclude(
                a => a.Guest).FirstOrDefault( w => w.WeddingId == wedId);
            
            return View("Wedding", dbWedding);
        }
    }
}