using System.ComponentModel.DataAnnotations;

namespace UsersNotebook.Attributes
{
    public class FirstLetterCapitalAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && value is string name)
            {
                if (name.Length > 0 && char.IsLower(name[0]))
                {
                    return new ValidationResult("Pierwsza litera powinna być napisana z dużej litery.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
