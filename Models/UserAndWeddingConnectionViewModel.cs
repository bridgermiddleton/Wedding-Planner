using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace WeddingPlanner.Models
{
    public class UserAndWeddingConnectionViewModel
    {
        public User TheUser{get;set;}
        public WeddingConnection NewWeddingConnection{get;set;}
        public List<Wedding> AllWeddings{get;set;}
    }
}