using System;
using Lun2Code.Models;
using Microsoft.AspNetCore.Http;

namespace Lun2Code.ViewModels
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        
        public Gender Gender { get; set; }
        
        public string Email { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        
        public IFormFile MainPhoto { get; set; }

        public UserViewModel()
        {
            
        }
        
        public UserViewModel(User user)
        {
            Name = user.Name;
            Surname = user.Surname;
            Gender = user.Gender;
            Email = user.Email;
            Country = user.Country;
            City = user.City;
        }

    }
}