using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace WeddingPlanner.Models
{
    public class LoginAndRegViewModel
    {
        public User NewUser{get;set;}
        public LoginCredentials LoggedUser{get;set;}
    }
}