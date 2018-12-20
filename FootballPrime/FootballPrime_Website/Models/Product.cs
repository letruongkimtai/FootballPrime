using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FootballPrime_Website.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PrID { get; set; }
        public string PrName { get; set; }
        public string Pic { get; set; }
        public string Describe { get; set; }
        public decimal Price { get; set; }

        public int PrTypeID { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}