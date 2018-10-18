using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BeaversDirectory.ViewModels;


namespace BeaversDirectory.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            // if UserManager returns a user, use SignInManager to
            // try to sign-in the user by using 'user' and the password that was passed in
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                // if it's good, redirect to the Index action method in the Home Controller
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            // if UserManager didn't return a user, or the sign-in attempt failed, return to the
            // Login view with GET. We use a custom Error on the ModelSate, and pass to it a generic failure message
            ModelState.AddModelError("", "User name/password not found");
            return View(loginViewModel);
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginViewModel loginViewModel)
        {
            // if data received is valid format, try to create a new user, using the UserName and Password that were passed in
            if (ModelState.IsValid)
            {
                var user = new IdentityUser() { UserName = loginViewModel.UserName };
                var result = await _userManager.CreateAsync(user, loginViewModel.Password);

                // if it's successful, redirect to the Index action method in the Home Controller
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
            }

            // if it is invalid format, return the user to the Regsiter view with GET and passing the loginViewModel object
            return View(loginViewModel);
        }

        // Logout POST method, to log the user out then redirect to the Home page
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
