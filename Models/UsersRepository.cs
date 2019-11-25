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
	}
}