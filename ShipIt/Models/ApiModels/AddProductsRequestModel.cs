using ShipIt.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipIt.Models.ApiModels
{
    public class InboundManifestRequestModel
    {
        public int WarehouseId { get; set; }
        public string Gcp { get; set; }
        public IEnumerable<OrderLine> OrderLines { get; set; }
    }
}