using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfDemo.ValidationForms.ValidationForm2
{
    public class OverThirteenValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value != null)
            {
                int age = 0;
                try
                {
                    age = Convert.ToInt32(value);
                }
                catch
                {
                    return new ValidationResult(false, "You must be older than 13!");
                }

                if (age > 13)
                    return ValidationResult.ValidResult;

            }
            return new ValidationResult(false, "You must be older than 13!");
        }
    }
}
