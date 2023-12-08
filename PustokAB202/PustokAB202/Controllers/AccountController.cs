using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PustokAB202.DAL;
using PustokAB202.Models;
using PustokAB202.ViewModels;

namespace PustokAB202.Controllers
{
    
    public class AccountController : Controller
    {
		private readonly AppDbContext _context;
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AccountController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_context = context;
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterUserVM userVM, string? returnUrl)
        {
           
            if (!ModelState.IsValid) return View(userVM);
			AppUser newUser = new AppUser
			{
				UserName = userVM.UserName,
				Email = userVM.Email,
				Name = userVM.Name,
				Surname = userVM.Surname,
				Gender = userVM.Gender,

			};

			var res = await _userManager.CreateAsync(newUser,userVM.Password);

			if (!res.Succeeded)
			{
				foreach (IdentityError err in res.Errors)
				{
					ModelState.AddModelError(string.Empty, err.Description);
				}

				return View(userVM);
			}


			await _signInManager.SignInAsync(newUser, false);



			if (returnUrl is not null) Redirect(returnUrl);
			return RedirectToAction("Index", "Home");




        }

		public IActionResult LogIn()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> LogIn(LoginUserVM loginUserVM)
		{
			if (!ModelState.IsValid) return View();
			AppUser user = await _userManager.FindByNameAsync(loginUserVM.UsernameOrEmail);
			if (user == null)
			{
				user = await _userManager.FindByEmailAsync(loginUserVM.UsernameOrEmail);
				if (user == null)
				{
					ModelState.AddModelError(String.Empty, "Username, Email or Password is incorrect");
					return View();
				}
			}

			var result = await _signInManager.PasswordSignInAsync(user, loginUserVM.Password, loginUserVM.IsRemembered, true);
			if (result.IsLockedOut)
			{
                ModelState.AddModelError(String.Empty, "You are blocked, please try again later");
                return View();
            }
			if (!result.Succeeded)
			{
                ModelState.AddModelError(String.Empty, "Username, Email or Password is incorrect");
                return View();
            }

			return RedirectToAction("Index","Home");
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
