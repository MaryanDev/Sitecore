using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sitecore.Data.Validators;
using SiteCoreTrainings.Infrastructure.Models;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;

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
                //var jAddressData = JObject.Parse(ControlValidationValue);
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
