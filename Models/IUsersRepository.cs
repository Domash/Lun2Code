namespace Lun2Code.Models
{
	public interface IUsersRepository
	{
		void Save();

		User GetUserById(string id);

		void Update(User user);
	}
}