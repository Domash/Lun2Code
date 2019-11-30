using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lun2Code.Models
{
	public class UsersContext : IdentityDbContext<User>
	{
		public UsersContext(DbContextOptions<UsersContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<Photo> Photos { get; set; }

	}
}