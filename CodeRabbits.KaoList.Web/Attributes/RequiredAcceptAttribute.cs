using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace CodeRabbits.KaoList.Web
{
    public class RequiredAcceptAttribute : ValidationAttribute
    {
        public RequiredAcceptAttribute() : base(() => "The {0} must accept.")
        {
        }

        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            if (!(bool)value!)
            {
                var localizer = validationContext.GetService<IStringLocalizer<SharedResource>>();
                return new ValidationResult(
                    string.Format(localizer?.GetString(ErrorMessageString) ?? ErrorMessageString,
                    validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }
    }
}
