using System;
using System.Threading.Tasks;
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

        [HttpPost]
        public string Run(string code, string input)
        {
            Console.WriteLine(code);
            Console.WriteLine(input);
            
            return code + input;
        }
        
        
    }
}