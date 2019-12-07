using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lun2Code.Controllers
{
    public class CodeEditorController : Controller
    {
        public static string[] Langs =  {
            "C", "CPP11", "CSHARP", "PYTHON"
        };

        private static string _runUrl = "https://api.hackerearth.com/v3/code/run/";
        private static string _clienSecret = "fd0bf73c7c3d34564bfc94339f8fc247eb906a95";
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Run(string code, string input)
        {

            string result;
            
            using (var webClient = new WebClient())
            {
                var data = new NameValueCollection
                {
                    {"client_secret", _clienSecret},
                    {"async", "0"},
                    {"source", code},
                    {"lang", "PYTHON"},
                    {"input", input},
                    {"time_limit", "5"},
                    {"memory_limit", "262144"}
                };
                
                var response = webClient.UploadValues(_runUrl, "POST", data);
                
                result = Encoding.UTF8.GetString(response);
                
                
            }
            
            return result;
        }
        
        
    }
}