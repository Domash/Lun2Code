using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.EntityFrameworkCore;

namespace Lun2Code.Models
{
	public class UsersContext : IdentityDbContext<User>
	{
		public UsersContext(DbContextOptions<UsersContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<GeneralChatMessage>()
				.HasOne<User>(a => a.User)
				.WithMany(d => d.Messages)
				.HasForeignKey(d => d.UserId);
		}

		public DbSet<Photo> Photos { get; set; }
		public DbSet<GeneralChatMessage> Messages { get; set; }
		
	}
}