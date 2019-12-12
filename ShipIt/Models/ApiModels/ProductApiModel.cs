using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShipIt.Models.DataModels;

namespace ShipIt.Models.ApiModels
{
    public class Product
    {
        public int id { get; set; }
        public string gtin { get; set; }
        public string gcp { get; set; }
        public string name { get; set; }
        public float weight { get; set; }
        public int lowerThreshold { get; set; }
        public bool discontinued { get; set; }
        public int minimumOrderQuantity { get; set; }

        public Product(ProductDataModel dataModel)
        {
            id = dataModel.Id;
            gtin = dataModel.Gtin;
            gcp = dataModel.Gcp;
            name = dataModel.Name;
            weight = (float)dataModel.Weight;
            lowerThreshold = dataModel.LowerThreshold;
            discontinued = dataModel.Discontinued == 1;
            minimumOrderQuantity = dataModel.MinimumOrderQuantity;
        }

        //Empty constructor needed for Xml serialization
        public Product()
        {
        }
    }
}