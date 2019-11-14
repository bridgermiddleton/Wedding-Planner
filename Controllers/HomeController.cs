using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;


namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult LoginAndRegPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(LoginAndRegViewModel modelData)
        {
            Console.WriteLine("$$$$$$$$$$$$$$$$$WORKING$$$$$$$$$$$$$$$$$$$$");
            if(ModelState.IsValid)
            {
                User newUser = modelData.NewUser;
                if(dbContext.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("LoginAndRegPage");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                dbContext.Add(newUser);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("user_id", newUser.UserId);
                return RedirectToAction("Dashboard");
            }
            return View("LoginAndRegPage");

        }
        [HttpPost]
        public IActionResult Login(LoginAndRegViewModel modelData)
        {
            if (ModelState.IsValid)
            {
                LoginCredentials loggeduser = modelData.LoggedUser;
                User userInDb = dbContext.Users.FirstOrDefault(u => u.Email == loggeduser.Email);
                if (userInDb == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("LoginAndRegPage");
                }
                var hasher = new PasswordHasher<LoginCredentials>();
                var result = hasher.VerifyHashedPassword(loggeduser, userInDb.Password, loggeduser.Password);
                if (result == 0)
                {
                    ModelState.AddModelError("Password", "Invalid Email/Password");
                    return View("LoginAndRegPage");
                }
                HttpContext.Session.SetInt32("user_id", userInDb.UserId);
                return RedirectToAction("Dashboard");
            }
            return View("LoginAndRegPage");
        }
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            UserAndWeddingConnectionViewModel ViewModel = new UserAndWeddingConnectionViewModel()
            {
                AllWeddings = dbContext.Weddings.Include(w => w.WeddingGuests).ThenInclude(u => u.User).ToList(),
                TheUser = dbContext.Users.Where(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id")).Include(w => w.CreatedWeddings).ThenInclude(b => b.Wedding).FirstOrDefault(),


            };
            foreach (Wedding wedding in ViewModel.AllWeddings)
            {
                Console.WriteLine(wedding.WedderOne);
            }
            
            return View(ViewModel);
        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginAndRegPage");
        }
        [HttpGet("wedding/{weddingid}")]
        public IActionResult WeddingPage(int weddingid)
        {
            
            Wedding TheWedding = dbContext.Weddings.Where(w => w.WeddingId == weddingid).Include(u => u.WeddingGuests).ThenInclude(r => r.User).FirstOrDefault();
            return View(TheWedding);

            
        }
        [HttpGet("newwedding")]
        public IActionResult NewWeddingPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddWedding(Wedding newWedding)
        {
            if (ModelState.IsValid)
            {
                newWedding.UserId = (int)HttpContext.Session.GetInt32("user_id");
                dbContext.Add(newWedding);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard"); 
            }
            return View("NewWeddingPage");
            
        }
        [HttpPost]
        public IActionResult RSVP(UserAndWeddingConnectionViewModel modelData)
        {
            if (ModelState.IsValid)
            {
                WeddingConnection newConnection = modelData.NewWeddingConnection;
                dbContext.Add(newConnection);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");

            }
            return View("Dashboard");
        }
        [HttpPost("delete/{weddingid}")]
        public IActionResult Delete(int weddingid)
        {
            Wedding theWedding = dbContext.Weddings.Where(w => w.WeddingId == weddingid).Include(u => u.WeddingGuests).ThenInclude(b => b.User).FirstOrDefault();
            dbContext.Weddings.Remove(theWedding);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpPost("unrsvp/{weddingconnectionid}")]
        public IActionResult UnRSVP(int weddingconnectionid)
        {
            WeddingConnection theweddingconnection = dbContext.WeddingConnections.Where(w => w.WeddingConnectionId == weddingconnectionid).FirstOrDefault();
            dbContext.WeddingConnections.Remove(theweddingconnection);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
