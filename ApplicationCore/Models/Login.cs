﻿using System.ComponentModel.DataAnnotations;

namespace WebKursach.ApplicationCore.Models
{
    public class Login
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}
