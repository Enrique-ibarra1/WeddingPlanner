using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class Association
    {
        [Key] 
        public int AssociationId {get;set;}
        public int WeddingId {get;set;}
        public int UserId {get;set;}
        public Wedding Wedding {get;set;}
        public User Guest {get;set;}
    }
}