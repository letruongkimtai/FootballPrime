using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPrime_Website.Models
{
    public class Cart
    {
        FootballPrimeDbContext db = new FootballPrimeDbContext();
        public int TempID { get; set; }
        public string TempName { get; set; }
        public string TempPic { get; set; }
        public Double TempPrice { get; set; }
        public int TempAmount { get; set; }
        public Double TempTotal
        {
            get { return TempPrice * TempAmount; }
        }


        public Cart(int id)
        {
            TempID = id;
            Product product = db.Products.Single(n => n.PrID == TempID);
            TempName = product.PrName;
            TempPic = product.Pic;
            TempPrice = double.Parse(product.Price.ToString());
            TempAmount = 1;
        }
    }
}