using Microsoft.AspNetCore.Mvc;

namespace Lun2Code.Controllers
{
    public class ContestsController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        
    }
}