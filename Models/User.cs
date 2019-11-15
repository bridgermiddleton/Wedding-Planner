using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace WeddingPlanner.Models
{
    public class User
    {
        [Key]
        public int UserId{get;set;}
        [Required]
        public string FirstName{get;set;}
        [Required]
        public string LastName{get;set;}
        [Required]
        public string Email{get;set;}
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password{get;set;}
        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Password")]

        public string Confirm{get;set;}
        public List<WeddingConnection> CreatedWeddings {get;set;}

    }
}