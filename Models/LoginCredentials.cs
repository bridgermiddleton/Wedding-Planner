using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace WeddingPlanner.Models
{
    public class LoginCredentials
    {
        [Required]
        public string Email{get;set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password{get;set;}
    }
}