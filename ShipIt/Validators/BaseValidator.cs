using System;
using System.Collections.Generic;

namespace ShipIt.Validators
{
    public abstract class BaseValidator<T>
    {
        List<string> errors;

        protected BaseValidator()
        {
            errors = new List<string>();
        }

        public void Validate(T target)
        {
            DoValidation(target);
        }

        protected abstract void DoValidation(T target);

        void addError(String error)
        {
            errors.Add(error);
        }

        void addErrors(List<String> errors)
        {
            this.errors.AddRange(errors);
        }

/**
 * Object validators
 */

        void assertNotNull(String fieldName, Object value)
        {
            if (value == null)
            {
                addError($"Field {fieldName} cannot be null");
            }
        }

/**
 * String validators
 */

        protected void assertNotBlank(string fieldName, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                addError($"Field {fieldName} cannot be blank");
            }
        }

        protected void AssertNumeric(string fieldName, string value)
        {
            if (!double.TryParse(value, out double d))
            {
                addError($"Field {fieldName} must be numeric");
            }
        }

        protected void AssertMaxLength(String fieldName, string value, int maxLength)
        {
            if (value.Length > maxLength)
            {
                addError($"Field {fieldName} must be shorter than {maxLength} characters");
            }
        }

        protected void AssertExactLength(string fieldName, string value, int exactLength)
        {
            if (value.Length != exactLength)
            {
                addError($"Field {fieldName} must be exactly {exactLength} characters");
            }
        }

/**
 * Numeric validators
 */

        protected void AssertNonNegative(string fieldName, int value)
        {
            if (value < 0)
            {
                addError($"Field {fieldName} must be non-negative");
            }
        }

        protected void AssertNonNegative(string fieldName, float value)
        {
            if (value < 0)
            {
                addError($"Field {fieldName} must be non-negative");
            }
        }

/**
 * Specific validators
 */

        protected void ValidateGtin(string value)
        {
            assertNotBlank("gtin", value);
            AssertNumeric("gtin", value);
            AssertMaxLength("gtin", value, 13);
        }

        protected void ValidateGcp(String value)
        {
            assertNotBlank("gcp", value);
            AssertNumeric("gcp", value);
            AssertMaxLength("gcp", value, 13);
        }

        protected void validateWarehouseId(int warehouseId)
        {
            AssertNonNegative("warehouseId", warehouseId);
        }
        /*
    protected void validateOrderLines(List<OrderLine> orderLines)
    {
        Set<String> gtins = new HashSet<String>(orderLines.size());
        for (OrderLine orderLine : orderLines)
        {
            OrderLineValidator orderLineValidator = new OrderLineValidator();
            orderLineValidator.doValidation(orderLine);
            addErrors(orderLineValidator.errors);

            if (gtins.contains(orderLine.getGtin()))
            {
                addError(String.format("Order contains duplicate GTINs: {0}", orderLine.getGtin()));
            }
            else
            {
                gtins.add(orderLine.getGtin());
            }
        }
    }*/
    }
}