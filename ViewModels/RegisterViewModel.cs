using System.ComponentModel.DataAnnotations;

namespace Lun2Code.ViewModels
{
	public class RegisterViewModel
	{
		private const string RegErrorMessage = "Required Field";
		
		[Display(Name = "Email")]
		[Required(ErrorMessage = RegErrorMessage)]
		public string Email { get; set; }
		
		[Display(Name = "Password")]
		[DataType(DataType.Password)]
		[Required(ErrorMessage = RegErrorMessage)]
		public string Password { get; set; }
		
		[Display(Name = "Password Verification")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Passwords Don't Match")]
		[Required(ErrorMessage = RegErrorMessage)]
		public string PasswordVerification { get; set; }
		
		[Display(Name = "Name")]
		[Required(ErrorMessage = RegErrorMessage)]
		public string Name { get; set; }
		
		[Display(Name = "Surname")]
		[Required(ErrorMessage = RegErrorMessage)]
		public string Surname { get; set; }
		
	}
}