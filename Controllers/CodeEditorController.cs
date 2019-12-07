using Microsoft.AspNetCore.Mvc;

namespace Lun2Code.Controllers
{
    public class CodeEditorController : Controller
    {
        public static string[] Langs =  {
            "C", "CPP11", "CSHARP", "PYTHON"
        };
        
        public IActionResult Index()
        {
            return View();
        }
    }
}