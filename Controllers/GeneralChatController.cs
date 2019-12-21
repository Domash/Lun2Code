using System.Threading.Tasks;
using Lun2Code.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lun2Code.Controllers
{
    public class GeneralChatController : Controller
    {
        private readonly IUsersRepository _repository;
        private readonly UserManager<User> _userManager;

        public GeneralChatController(IUsersRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            
            var messages = await _repository.GetMessages();

            return View(messages);

        }
    
        public async Task<IActionResult> Create(GeneralChatMessage message)
        {
            if (ModelState.IsValid)
            {
                message.UserName = User.Identity.Name;
                var sender = await _userManager.GetUserAsync(User);
                message.UserId = sender.Id;
                message.UserEmail = sender.Email;

                await _repository.AddMessage(message);
                await _repository.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }
        

    }
}