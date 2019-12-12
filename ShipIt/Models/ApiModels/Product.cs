using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShipIt.Models.DataModels;

namespace ShipIt.Models.ApiModels
{
    public class Product
    {
        public int Id { get; set; }
        public string Gtin { get; set; }
        public string Gcp { get; set; }
        public string Name { get; set; }
        public float Weight { get; set; }
        public int LowerThreshold { get; set; }
        public bool Discontinued { get; set; }
        public int MinimumOrderQuantity { get; set; }

        public Product(ProductDataModel dataModel)
        {
            Id = dataModel.Id;
            Gtin = dataModel.Gtin;
            Gcp = dataModel.Gcp;
            Name = dataModel.Name;
            Weight = (float)dataModel.Weight;
            LowerThreshold = dataModel.LowerThreshold;
            Discontinued = dataModel.Discontinued == 1;
            MinimumOrderQuantity = dataModel.MinimumOrderQuantity;
        }

        //Empty constructor needed for Xml serialization
        public Product()
        {
        }
    }
}