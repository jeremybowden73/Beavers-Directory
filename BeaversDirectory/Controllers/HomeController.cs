using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeaversDirectory.Models;
using BeaversDirectory.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BeaversDirectory.Controllers
{
    public class HomeController : Controller
    {
        // create local instances of Interfaces
        private readonly IBeaversRepository _beaversRepository;
        private readonly IEnquireRepository _enquireRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        // constructor injection
        public HomeController(IBeaversRepository beaversRepository, IEnquireRepository enquireRepository, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _beaversRepository = beaversRepository;
            _enquireRepository = enquireRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //
        // define routes

        public IActionResult Index(/*LoginViewModel loginViewModel*/)
        {
            var beavers = _beaversRepository.AllBeavers().OrderBy(b => b.Id);

            string loggedInUser = "";

            if (_signInManager.IsSignedIn(User))
            {
                loggedInUser = _userManager.GetUserName(User);
            }

            // create a new instance of the HomeViewModel class and populate the properties
            var homeViewModel = new HomeViewModel()
            {
                Beavers = beavers.ToList(),
                Title = "89th Leicestershire Beaver Scouts Directory",
                User = loggedInUser
            };

            return View(homeViewModel);
        }

        public IActionResult Details(int id)
        {
            var beaver = _beaversRepository.GetBeaverById(id);
            if (beaver == null)
                return NotFound();

            return View(beaver);
        }

        public IActionResult Enquire()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Enquire(Enquire enquire)
        {
            // validate the received enquire object; add it to the db if it's good
            if (ModelState.IsValid)
            {
                _enquireRepository.AddEnquire(enquire);
                return RedirectToAction("EnquireComplete");
            }

            // if ModelState.IsValid returned false there was a problem with the form content, so
            // return the received enquire object to the GET Enquire view
            return View(enquire);
        }

        public IActionResult EnquireComplete()
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
