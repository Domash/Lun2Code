using System.Threading.Tasks;
using Lun2Code.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lun2Code.Controllers
{
    public class UserController : Controller
    {
        private IUsersRepository _repository;
        private IHostingEnvironment _environment;
        
        User _user;
        UserManager<User> _userManager;

        public UserController(IUsersRepository repository, 
                              IHostingEnvironment environment, 
                              IHttpContextAccessor httpContextAccessor,
                              UserManager<User> userManager)
        {
            _repository = repository;
            _environment = environment;
            _userManager = userManager;
            
            var id = userManager.GetUserId(httpContextAccessor.HttpContext.User);
            _user = _repository.GetUserById(id);
        }


        public async Task<IActionResult> Index()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View(_user);
        }
    }
}