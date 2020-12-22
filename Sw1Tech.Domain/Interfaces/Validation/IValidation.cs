using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Interfaces.Validation
{
    public interface IValidation<in TEntity>
    {
        ValidationResult Valid(TEntity entity);
    }
}