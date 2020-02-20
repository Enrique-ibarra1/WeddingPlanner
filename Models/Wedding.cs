using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId {get;set;}

        [Required]
        public string WedderOne {get;set;}

        [Required]
        public string WedderTwo {get;set;}
        [Required]
        public string Address {get;set;}
        [Required]
        public DateTime Date {get;set;} 
        public int UserId {get;set;}
        public User WeddingPlanner {get;set;}
        public List<Association> Guests {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    }
}