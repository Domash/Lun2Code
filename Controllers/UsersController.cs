using Lun2Code.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lun2Code.Controllers
{
	public class UsersController : Controller
	{
		private IUsersRepository _repository;
		
		public UsersController(IUsersRepository repository)
		{
			_repository = repository;
		}
	}
}