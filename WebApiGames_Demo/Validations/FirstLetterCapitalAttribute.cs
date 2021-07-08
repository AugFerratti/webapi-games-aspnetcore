using System.ComponentModel.DataAnnotations;

namespace WebApiGames_Demo.Validations
{
    public class FirstLetterCapitalAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var firstLetter = value.ToString()[0].ToString();
            if (firstLetter != firstLetter.ToUpper())
            {
                return new ValidationResult("The first letter of the game must be capital");
            }

            return ValidationResult.Success;
        }
    }
}
