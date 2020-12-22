namespace Sw1Tech.Domain.Entities
{
    public class Sexo : BaseEntity
    {
        public string Nome { get; set; }
        //public ValidationResult ValidationResult { get; private set; }

        //public bool IsValid
        //{
        //    get
        //    {
        //        var fiscal = new SexoIsValid();
        //        ValidationResult = fiscal.Valid(this);
        //        return ValidationResult.IsValid;
        //    }
        //}
    }
}
