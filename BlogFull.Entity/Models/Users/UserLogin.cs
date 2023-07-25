﻿using System.ComponentModel.DataAnnotations;

namespace BlogFull.Entity.Models.Users
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Username is required")]
        [MinLength(5, ErrorMessage = "Must be 5-20 characters")]
        [MaxLength(20, ErrorMessage = "Must be 5-20 characters")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(10, ErrorMessage = "Must be 10-50 characters")]
        [MaxLength(50, ErrorMessage = "Must be 10-50 characters")]
        public string Password { get; set; }
    }
}
