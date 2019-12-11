using System.Collections.Generic;
using System.Linq;
using ShipIt.Exceptions;
using ShipIt.Models.ApiModels;

namespace ShipIt.Parsers
{
    public class ProductRequestModel
    {
        public string Gtin { get; set; }
        public string Gcp { get; set; }
        public string Name { get; set; }
        public string Weight { get; set; }
        public string LowerThreshold { get; set; }
        public string Discontinued { get; set; }
        public string MinimumOrderQuantity { get; set; }
    }


    public static class ProductParser
    {
        public static ProductApiModel Parse(this ProductRequestModel requestModel)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(requestModel.Discontinued))
            {
                errors.Add("Discontinued must be set");
            }
            if (string.IsNullOrEmpty(requestModel.LowerThreshold))
            {
                errors.Add("LowerThreshold must be set");
            }
            if (string.IsNullOrEmpty(requestModel.MinimumOrderQuantity))
            {
                errors.Add("Discontinued must be set");
            }
            if (string.IsNullOrEmpty(requestModel.Weight))
            {
                errors.Add("Discontinued must be set");
            }

            if (errors.Any())
            {
                throw new MalformedRequestException(string.Join("\n", errors));
            }


            if (!bool.TryParse(requestModel.Discontinued, out bool discontinued))
            {
                errors.Add("Discontinued must be set to true or false");
            }
            if (!int.TryParse(requestModel.Gtin, out int lowerThreshold))
            {
                errors.Add("LowerThreshold must be set to an integer");
            }
            if (!int.TryParse(requestModel.Gtin, out int minimumOrderQuantity))
            {
                errors.Add("MinimumOrderQuantity must be set to an integer");
            }
            if (!int.TryParse(requestModel.Weight, out int weight))
            {
                errors.Add("Weight must be set to an integer");
            }

            if (errors.Any())
            {
                throw new MalformedRequestException(string.Join("\n", errors));
            }

            return new ProductApiModel()
            {
                discontinued = discontinued,
                gcp = requestModel.Gcp,
                gtin = requestModel.Gtin,
                lowerThreshold = lowerThreshold,
                minimumOrderQuantity = minimumOrderQuantity,
                name = requestModel.Name,
                weight = weight
            };
        }
    }
}