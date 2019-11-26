using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lun2Code.Models;

namespace Lun2Code.Controllers
{
    public class HomeController : Controller
    {
		public IActionResult Index()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "User");
			}
			return View();
		}
		public IActionResult Error()
		{
			return View();
		}
	}
}