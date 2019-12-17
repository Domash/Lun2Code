using System;
using Lun2Code.Contest;
using Microsoft.AspNetCore.Mvc;

namespace Lun2Code.Controllers
{
    public class ContestsController : Controller
    {
        public IActionResult Index()
        {
            var cf = new Codeforces(DateTime.Now);

            var kek = cf.GetContestsList();
            
            return View();
        }
        
    }
}