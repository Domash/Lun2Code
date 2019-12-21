using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lun2Code.Models
{
	public interface IUsersRepository
	{
		void Save();

		User GetUserById(string id);

		void Update(User user);

		Task<List<GeneralChatMessage>> GetMessages();
		
		Task AddMessage(GeneralChatMessage message);

		Task SaveChanges();
	}
}