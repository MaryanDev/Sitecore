using System.Linq;
using Sitecore.Data.Validators;
using SiteCoreTrainings.Infrastructure.Models;

namespace SiteCoreTrainings.Infrastructure.Custom_Validators
{
    public class AddressValidator : StandardValidator
    {
        protected override ValidatorResult Evaluate()
        {
            var addressValidationResponse = Address.ValidateAddress(ControlValidationValue);

            if (addressValidationResponse != null)
            {
                if (addressValidationResponse.ValidationResult.IsValid)
                {
                    return ValidatorResult.Valid;
                }
                Text = addressValidationResponse.ValidationResult.Messages != null && addressValidationResponse.ValidationResult.Messages.Any() ?
                    $"{addressValidationResponse.ValidationResult.Messages.FirstOrDefault().Code}, {addressValidationResponse.ValidationResult.Messages.FirstOrDefault().Text}" 
                    : "Address does not exist";
                return ValidatorResult.CriticalError;
            }
            Text = "Address does not exist";
            return ValidatorResult.CriticalError;
        }

        protected override ValidatorResult GetMaxValidatorResult()
        {
            return GetFailedResult(ValidatorResult.Error);
        }

        public override string Name => "Address must exist";
    }
}
