using ShipIt.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipIt.Parsers
{
    public static class InboundOrderLineParser
    {
        public static OrderLine Parse(this OrderLine requestModel)
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


            if (!string.TryParse(requestModel.Discontinued, out bool discontinued))
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

            return new OrderLine()
            {
                gtin = requestModel.gtin,
                name = requestModel.name,
                quantity = requestModel.quantity
            };
        }
    }
}