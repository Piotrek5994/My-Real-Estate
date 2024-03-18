using System.ComponentModel.DataAnnotations;

namespace Core.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DecimalPrecisionAttribute : ValidationAttribute
    {
        private readonly int _precision;

        public DecimalPrecisionAttribute(int precision = 2)
        {
            _precision = precision;
            ErrorMessage = $"Value cannot have more than {_precision} decimal places.";
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            if (decimal.TryParse(value.ToString(), out decimal dValue))
            {
                decimal scaleFactor = (decimal)Math.Pow(10, _precision);
                decimal roundedValue = Math.Round(dValue * scaleFactor) / scaleFactor;
                return dValue == roundedValue;
            }

            return false;
        }
    }
}
