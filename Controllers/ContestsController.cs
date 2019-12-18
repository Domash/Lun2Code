using System;
using System.Collections.Generic;
using Lun2Code.Contest;
using Microsoft.AspNetCore.Mvc;

namespace Lun2Code.Controllers
{
    public class ContestsController : Controller
    {

        public static List<Contest.Contest> Contests { get; set; }
        
        public IActionResult Index()
        {
            var cf = new Codeforces(DateTime.Now);

            if (Contests == null)
            {
                Contests = cf.GetContestsList();
                Contests.Reverse();
            }

            ViewData.Model = Contests;
            
            return View();
        }
        
    }
}