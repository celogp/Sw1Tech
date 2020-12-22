namespace Sw1Tech.Domain.Entities.Filter
{
    public class ModeloKitFilter
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int ModeloId { get; set; }

        public ModeloKitFilter()
        {
            Id = 0;
            ProdutoId = 0;
            ModeloId = 0;
        }
    }
}
