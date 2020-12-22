using Sw1Tech.Domain.Validation;

namespace Sw1Tech.App
{
    public class AppService
    {
        public AppService()
        {
            ValidationResult = new ValidationResult();
        }
        protected ValidationResult ValidationResult { get; private set; }
    }
}
