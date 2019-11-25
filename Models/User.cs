using Microsoft.AspNetCore.Identity;

namespace Lun2Code.Models
{
	public class User : IdentityUser
	{
		public string Name { get; set; }
		public string Surname { get; set; }

		public override string ToString()
		{
			return Name + " " + Surname;
		}

		public User()
		{
			
		}
	}
}