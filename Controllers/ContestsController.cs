using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lun2Code.Contest;
using Microsoft.AspNetCore.Mvc;

namespace Lun2Code.Controllers
{
    public class ContestsController : Controller
    {
        public static List<Contest.Contest> Contests { get; set; }
        
        public IActionResult Index()
        {
            ViewData.Model = Contests;
            return View();
        }

        public static void UpdateContests()
        {
            var cf = new Codeforces(DateTime.Now);
            var dmoj = new DMOJ(DateTime.Now);
            
            Contests.Clear();
            
            Contests = cf.GetContestsList().Result;
            Contests.AddRange(dmoj.GetContestsList().Result);
            
            Contests?.Sort();
            
        }
        
    }
}