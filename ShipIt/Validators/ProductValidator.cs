using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Web;
using ShipIt.Exceptions;
using ShipIt.Models.ApiModels;

namespace ShipIt.Validators
{
    public class ProductValidator: BaseValidator<ProductApiModel>
    {
        protected override void DoValidation(ProductApiModel target)
        {
        assertNotBlank("name", target.name);
        AssertMaxLength("name", target.name, 255);

        ValidateGtin(target.gtin);

        ValidateGcp(target.gcp);

        AssertNonNegative("m_g", target.weight);

        AssertNonNegative("lowerThreshold", target.lowerThreshold);

        AssertNonNegative("minimumOrderQuantity", target.minimumOrderQuantity);
        }
    }
}