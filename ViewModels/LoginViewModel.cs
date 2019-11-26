using System;
using System.ComponentModel.DataAnnotations;

namespace Lun2Code.ViewModels
{
	public class LoginViewModel
	{
		[Display(Name = "Email")]
		public string Email { get; set; }
		
		[Display(Name = "Password")]
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Required Field")]
		public string Password { get; set; }
		
		public string ReturnUrl { get; set; }

	}

}