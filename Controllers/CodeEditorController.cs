using Microsoft.AspNetCore.Mvc;

namespace Lun2Code.Controllers
{
    public class CodeEditorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}