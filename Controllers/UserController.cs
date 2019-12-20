using System;
using System.IO;
using System.Threading.Tasks;
using Lun2Code.Models;
using Lun2Code.ViewModels;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> Update(UserViewModel info)
        {
            _user.UpdateUserInformation(info);
            _repository.Update(_user);
            _repository.Save();
            return RedirectToAction("Index");
        }

        public async Task UpdateMainPhoto(IFormFile mainPhoto)
        {
            var path = "/usersMainPhotos" + _user.Id + ".jpg";
            using (var stream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
            {
                await mainPhoto.CopyToAsync(stream);
            }

        }
        
        
        [HttpGet]
        public ViewResult Update()
        {
            return View(new UserViewModel(_user));
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