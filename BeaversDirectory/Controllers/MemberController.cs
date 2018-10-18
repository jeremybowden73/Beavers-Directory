using BeaversDirectory.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BeaversDirectory.Controllers
{
    public class MemberController : Controller
    {
        // create local instances of Interfaces
        private readonly IBeaversRepository _beaversRepository;
        private readonly IFeedbackRepository _feedbackRepository;

        // constructor injection
        public MemberController(IBeaversRepository beaversRepository, IFeedbackRepository feedbackRepository)
        {
            _beaversRepository = beaversRepository;
            _feedbackRepository = feedbackRepository;
        }

        //
        // define routes

        public IActionResult Feedback()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Feedback(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _feedbackRepository.AddFeedback(feedback);
                return RedirectToAction("FeedbackComplete");
            }

            return View(feedback);
        }

        public IActionResult FeedbackComplete()
        {
            return View();
        }

        public IActionResult Edit(int Id)
        {
            var beaver = _beaversRepository.GetBeaverById(Id);
            if (beaver == null)
            {
                return NotFound();
            }

            return View(beaver);
        }

        [HttpPost]
        public IActionResult Edit(Beaver beaver)
        {
            if (ModelState.IsValid)
            {
                _beaversRepository.UpdateBeaver(beaver);
                return RedirectToAction("EditComplete");
            }

            return View(beaver);

        }

        public IActionResult EditComplete()
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
