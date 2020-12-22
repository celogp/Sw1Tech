namespace Sw1Tech.Domain.Entities
{
    public class ProdutoModelo : BaseEntity
    {
        //public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int ModeloId { get; set; }
        public Modelo Modelo { get; set; }

        //public ValidationResult ValidationResult { get; private set; }
        //public bool IsValid
        //{
        //    get
        //    {
        //        var fiscal = new ProdutoModeloIsValid();
        //        ValidationResult = fiscal.Valid(this);
        //        return ValidationResult.IsValid;
        //    }
        //}
    }
}
