using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace State_Management_Lab.Models
{
    public class Items
    {
        public string ItemName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public Items(string ItemName, string Description, double Price)
        {
            this.ItemName = ItemName;
            this.Description = Description;
            this.Price = Price;
        }

        public Items() { }
    }
}