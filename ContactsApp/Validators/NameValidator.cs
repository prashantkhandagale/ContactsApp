using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ContactsApp.Validators
{
    public class NameValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "Name cannot be empty.");
            else
            {
                if (value.ToString().Any(char.IsDigit))
                    return new ValidationResult(false, "Name should not contain numbers!");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class EmailValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "Name cannot be empty.");
            else
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(value.ToString());
                if (!match.Success)
                   return new ValidationResult(false, "Invalid Email Address");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class PhoneValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "Phone No cannot be empty.");
            else
            {
                Regex regex = new Regex(@"^((\+){0,1}91(\s){0,1}(\-){0,1}(\s){0,1}){0,1}9[0-9](\s){0,1}(\-){0,1}(\s){0,1}[1-9]{1}[0-9]{7}$");
                Match match = regex.Match(value.ToString());
                if (!match.Success)
                   return new ValidationResult(false, "Invalid Phone Number");
            }
            return ValidationResult.ValidResult;
        }
    }

}
