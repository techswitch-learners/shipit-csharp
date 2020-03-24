using System.Collections.Generic;
using System.Linq;

namespace ShipIt.Models.ApiModels
{
    public class TruckManifestModel
    {
        public IEnumerable<ManifestItem> Items { get; set; }
        public double TotalWeight => Items.Sum(i => i.TotalWeight);
    }

    public class ManifestItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double WeightPerItem { get; set; }
        public double TotalWeight => Quantity * WeightPerItem;
    }
}