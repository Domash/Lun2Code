using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		public async Task<List<GeneralChatMessage>> GetMessages()
		{
			return _context.Messages.ToList();
		}

		public async Task AddMessage(GeneralChatMessage message)
		{
			await _context.Messages.AddAsync(message);
		}

		public async Task SaveChanges()
		{
			await _context.SaveChangesAsync();
		}
	}
}