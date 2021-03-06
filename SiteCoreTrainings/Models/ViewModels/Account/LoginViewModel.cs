﻿using System.ComponentModel.DataAnnotations;

namespace SiteCoreTrainings.Models.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}