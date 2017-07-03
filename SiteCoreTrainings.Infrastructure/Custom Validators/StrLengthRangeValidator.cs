using System.Runtime.Serialization;
using Sitecore.Data.Validators;

namespace SiteCoreTrainings.Infrastructure.Custom_Validators
{
    public class StrLengthRangeValidator : StandardValidator
    {
        public StrLengthRangeValidator(SerializationInfo info, StreamingContext context) : base(info, context)
        {
                
        }
        public StrLengthRangeValidator()
        {
                
        }

        public override string Name => "Must be in given range";

        protected override ValidatorResult GetMaxValidatorResult()
        {
            return GetFailedResult(ValidatorResult.Error);
        }

        protected override ValidatorResult Evaluate()
        {
            Parameters.TryGetValue("minLength", out string min);
            Parameters.TryGetValue("maxLength", out string max);

            bool isMinLengthParamValid = int.TryParse(min, out int minLength);
            bool isMaxLengthParamValid = int.TryParse(max, out int maxLength);

            if (!isMinLengthParamValid || !isMaxLengthParamValid)
            {
                Text =
                    "minLength or maxLength param probably are not an integers. Please check Parameters tab in aprpriate validation rule";
                return ValidatorResult.CriticalError;
            }

            if (ControlValidationValue.Length < minLength || ControlValidationValue.Length > maxLength)
            {
                Text = $"{this.GetFieldDisplayName()} length must be in range between {minLength} and {maxLength}";
                return  ValidatorResult.CriticalError;
            }

            return ValidatorResult.Valid;
        }
    }
}
