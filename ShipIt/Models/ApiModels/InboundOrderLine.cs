using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipIt.Models.ApiModels
{
    public class OrderLine
    {
        public string gtin { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }

        public OrderLine()
        {
        }
    }
}