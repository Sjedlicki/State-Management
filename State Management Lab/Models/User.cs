using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace State_Management_Lab.Models
{
    public class User
    {
        [Required]
        //[RegularExpression(@"^[A-Z{1}]+[a-zA-z{1,30}]+$", ErrorMessage = "Please enter a valid name.")]
        public string Name { get; set; }

        [Required]
        //[RegularExpression(@"^[a-zA-Z0-9{5,30}]+@[a-zA-A0-9{5,10}]+\.[a-zA-Z0-9{2,3}]+$", ErrorMessage = "Incorrect E-mail Format!")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        //[RegularExpression(@"^\d{1,3}$", ErrorMessage = "Please enter a valid number!")]
        public int Age { get; set; }

        public User(string Name, string Email, string Password, int Age)
        {
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
            this.Age = Age;
        }

        public User() { }
    }
}