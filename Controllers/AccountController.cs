using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Lun2Code.Logging;
using Lun2Code.Models;
using Lun2Code.Services;
using Lun2Code.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Lun2Code.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		private readonly IEmailService _emailService;
		private readonly IStringLocalizer<AccountController> _localizer;
		// private readonly ILogger _log = Logger.CreateLogger<AccountController>();

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
					Surname  = model.Surname,
					Country  = model.Country,
					City     = model.City,
					Gender   = model.Gender
				};

				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					await _userManager.AddToRoleAsync(user, "User");
					await _signInManager.SignInAsync(user, false);
					return RedirectToAction("Index", "Home");
				}

				ModelState.AddModelError("", _localizer["sign"]); 

			}

			return View(model);
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> ConfirmEmail(string id, string code)
		{
			if (id == null || code == null)
			{
				return View("Error");
			}

			var user = await _userManager.FindByIdAsync(id).ConfigureAwait(true);

			if (user == null)
			{
				return View("Error");
			}

			var result = await _userManager.ConfirmEmailAsync(user, code).ConfigureAwait(true);

			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Home");
			} 
			
			return View("Error");
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Login(string returnUrl = null)
		{
			
			var model = new LoginViewModel
			{
				ReturnUrl = returnUrl,
				ExternalLogins = 
					(await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
			};
			
			return View(model);
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
		
		[AllowAnonymous]
		[HttpPost]
		public IActionResult ExternalLogin(string provider, string returnUrl)
		{
			var redirectUrl = Url.Action("GoogleResponse", "Account",
				new { ReturnUrl = returnUrl });
			var properties = _signInManager
				.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
			return new ChallengeResult(provider, properties);
		}
		
		[AllowAnonymous]
		public async Task<IActionResult> GoogleResponse()
		{
			var info = await _signInManager.GetExternalLoginInfoAsync();

			if (info == null)
			{
				return RedirectToAction(nameof(Login));
			}

			var user = new User
			{
				Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
				UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
				Name = "",
				Surname = "",
				Country = "Belarus",
				City = "Minsk",
				Gender = Gender.Female
			};
  
			var result = await _userManager.CreateAsync(user, "28vehera");
			
			Console.WriteLine(result.ToString());
			
			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(user, false);
				return RedirectToAction("Index", "Home");
			}
			
			return RedirectToAction(nameof(Login));
		}

		public async Task<IActionResult> LogOff()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
		
	}
}

//					var code = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(true);
//					var callbackUrl = Url.Action(
//						"ConfirmEmail",
//						"Account",
//						new { userId = user.Id, code = code },
//						protocol: HttpContext.Request.Scheme
//					);
//					
//					await _emailService.SendEmailAsync(user.Email, "Confirm your account",
//						$"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>").ConfigureAwait(true);
//
//					return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
//