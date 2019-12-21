using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;

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
		
		public IList<AuthenticationScheme> ExternalLogins { get; set; }
	}

}