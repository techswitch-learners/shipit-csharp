using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipIt.Models.ApiModels
{
    public class OutboundOrderRequestModel
    {
        public int WarehouseId { get; set; }
        public IEnumerable<OrderLine> OrderLines { get; set; }
    }
}
