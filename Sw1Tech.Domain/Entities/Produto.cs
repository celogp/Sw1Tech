using System;

namespace Sw1Tech.Domain.Entities
{
    public class Produto : BaseEntity
    {
        public string Nome { get; set; }
        public string Volume { get; set; }
        public int Classificacao { get; set; }
        public decimal Preco { get; set; }
        public decimal Custo { get; set; }
        public string UsarPrecoProdutoBase { get; set; }

        //public ValidationResult ValidationResult { get; private set; }
        //public bool IsValid
        //{
        //    get
        //    {
        //        var fiscal = new ProdutoIsValid();
        //        ValidationResult = fiscal.Valid(this);
        //        return ValidationResult.IsValid;
        //    }
        //}
    }
}
