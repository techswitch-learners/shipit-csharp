using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipIt.Models.ApiModels
{
    public class OrderLine
    {
        public String gtin { get; set; }
        public int quantity { get; set; }
    }
}