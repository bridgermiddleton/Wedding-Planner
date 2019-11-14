using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace WeddingPlanner.Models
{
    public class WeddingConnection
    {
        [Key]
        public int WeddingConnectionId{get;set;}
        public int UserId{get;set;}
        public int WeddingId{get;set;}
        public User User{get;set;}
        public Wedding Wedding{get;set;}
    }
}