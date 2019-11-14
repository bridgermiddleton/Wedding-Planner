using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace WeddingPlanner.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId{get;set;}
        [Required]
        public string WedderOne{get;set;}
        [Required]
        public string WeddingTwo{get;set;}
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date{get;set;}
        [Required]
        public string Address{get;set;}
        public int UserId{get;set;}
        public List<WeddingConnection> WeddingGuests{get;set;}
    }
}