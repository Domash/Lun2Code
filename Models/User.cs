using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lun2Code.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Lun2Code.Models
{
	public class User : IdentityUser
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		
		public Gender Gender { get; set; }
		
		public string Country { get; set; }
		public string City { get; set; }

		public ICollection<GeneralChatMessage> Messages { get; set; }

		[NotMapped]
		public string PhotoPath
		{
			get { return "~/images/gabriela_lotarynska_cpp.jpg"; }
		}

		public void UpdateUserInformation(UserViewModel information)
		{
			Name = information.Name;
			Surname = information.Surname;
			Gender = information.Gender;
			Country = information.Country;
			City = information.City;
		}
		
		
		public override string ToString()
		{
			return Name + " " + Surname;
		}
		
		public User()
		{
			Messages = new List<GeneralChatMessage>();
		}

	}
	
	public enum Gender
	{
		[Display(Name = "Мужской")] Male,
		[Display(Name = "Женский")] Female
	}

}