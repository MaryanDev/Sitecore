using Sitecore.Data.Validators;

namespace SiteCoreTrainings.Infrastructure.Custom_Validators
{
    public class VideoSourceValidator : StandardValidator
    {
        private readonly string youtubeUrl = "https://www.youtube.com/embed/";
        protected override ValidatorResult Evaluate()
        {
            if (!ControlValidationValue.StartsWith(youtubeUrl))
            {
                Text = $"Invalid link inserted, link should start with {youtubeUrl}";
                return ValidatorResult.CriticalError;
            }
            return ValidatorResult.Valid;
        }

        protected override ValidatorResult GetMaxValidatorResult()
        {
            return GetFailedResult(ValidatorResult.Error);
        }

        public override string Name { get; }
    }
}
