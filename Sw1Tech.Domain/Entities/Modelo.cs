namespace Sw1Tech.Domain.Entities
{
    public class Modelo : BaseEntity
    {
        public string Nome { get; set; }
        public string LinkImagem { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }

        //public ValidationResult ValidationResult { get; private set; }

        //public bool IsValid
        //{
        //    get
        //    {
        //        var fiscal = new ModeloIsValid();
        //        ValidationResult = fiscal.Valid(this);
        //        return ValidationResult.IsValid;
        //    }
        //}

        public Modelo()
        {
            Largura = 0;
            Comprimento = 0;
        }

    }
}
