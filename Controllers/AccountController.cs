using System;
using System.Threading.Tasks;
using Lun2Code.Models;
using Lun2Code.Services;
using Lun2Code.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Lun2Code.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		private readonly IEmailService _emailService;
		private readonly IStringLocalizer<AccountController> _localizer;

		public AccountController(
				UserManager<User> userManager, 
				SignInManager<User> signInManager,
				IStringLocalizer<AccountController> localizer,
				IEmailService emailService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_localizer = localizer;
			_emailService = emailService;
		}
		
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new User
				{
					Email    = model.Email,
					UserName = model.Email,
					Name     = model.Name,
					Surname  = model.Surname
				};

				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					await _signInManager.SignInAsync(user, false);
					return RedirectToAction("Index", "Home"); // Need to create UserController
				}
				
				ModelState.AddModelError("", _localizer["sign"]); // Add errorMessage field to class 

			}

			return View(model);
		}

		[HttpGet]
		public IActionResult Login(string returnUrl = null)
		{
			return View(new LoginViewModel { ReturnUrl = returnUrl });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
				if (result.Succeeded)
				{
					if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
					{
						return Redirect(model.ReturnUrl);
					}
					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", _localizer["sign"]);
			}

			return View(model);
		}
		
		public async Task<IActionResult> LogOff()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
		
	}
}