using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Interfaces.Validation
{
    public interface ISelfValidation
    {
        ValidationResult ValidationResult { get; }
        bool IsValid { get; }
    }
}