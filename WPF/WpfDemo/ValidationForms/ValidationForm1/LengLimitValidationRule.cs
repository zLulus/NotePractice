using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfDemo.ValidationForms.ValidationRules
{
    public class LengLimitValidationRule: ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string str = value as string;
            if (str != null)
            {
                if (!CanBeNull && str.Length == 0)
                {
                    return new ValidationResult(false, CanNotBeNullMessage);
                }
                if (str.Length >= MinLength && str.Length <= MaxLength)
                {
                    return ValidationResult.ValidResult;
                }
                if (str.Length > MaxLength)
                {
                    return new ValidationResult(false, string.Format(MaxLengthMessage, MaxLength));
                }
                if (str.Length < MinLength)
                {
                    return new ValidationResult(false, string.Format(MinLengthMessage, MinLength));
                }
            }
            else
            {
                if (CanBeNull)
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, CanNotBeNullMessage);
                }
            }
            return new ValidationResult(false, CanNotBeNullMessage);
        }
        public bool CanBeNull { get; set; }
        public long MinLength { get; set; }
        public long MaxLength { get; set; }
        public String MinLengthMessage { get; set; }
        public String MaxLengthMessage { get; set; }
        public String CanNotBeNullMessage { get; set; }
    }
}
