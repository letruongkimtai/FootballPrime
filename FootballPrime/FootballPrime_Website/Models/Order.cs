using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FootballPrime_Website.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        public DateTime Odate { get; set; }

        public string PaymentMethod { get; set; }

        public bool PaymentCheck { get; set; }

        public bool DeliveryStatus { get; set; }


        public int CtmID { get; set; }
        public virtual Customer Customers { get; set; }
    }
}