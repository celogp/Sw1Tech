namespace Sw1Tech.Domain.Entities
{
    public class ModeloKit : BaseEntity
    {
        //public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int ModeloId { get; set; }
        public decimal Quantidade { get; set; }
        public Produto Produto { get; set;}

        //public ValidationResult ValidationResult { get; private set; }
        //public bool IsValid
        //{
        //    get
        //    {
        //        var fiscal = new ModeloKitIsValid();
        //        ValidationResult = fiscal.Valid(this);
        //        return ValidationResult.IsValid;
        //    }
        //}

        public ModeloKit()
        {
            Quantidade = 0;
        }
    }
}
