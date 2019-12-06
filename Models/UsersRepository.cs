using Microsoft.EntityFrameworkCore;

namespace Lun2Code.Models
{
	public class UsersRepository : IUsersRepository
	{

		private readonly UsersContext _context;

		public UsersRepository(UsersContext context)
		{
			_context = context;
		}
		
		public void Save()
		{
			_context.SaveChanges();
		}

		public User GetUserById(string id)
		{
			return _context.Users.Find(id);
		}

		public void Update(User user)
		{
			_context.Entry(user).State = EntityState.Modified;
		}
	}
}