namespace Sw1Tech.Domain.Entities
{
    public class Uf : BaseEntity
    {
        public string Nome { get; set; }
        public string Sigla { get; private set; }

        //public ValidationResult ValidationResult { get; private set; }
        //public bool IsValid
        //{
        //    get
        //    {
        //        var fiscal = new UfIsValid();
        //        ValidationResult = fiscal.Valid(this);
        //        return ValidationResult.IsValid;
        //    }
        //}

    }
}
