namespace Sw1Tech.Domain.Entities.Filter
{
    public class ProdutoModeloFilter
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int ModeloId { get; set; }
        public string Nome { get; set; }

        public ProdutoModeloFilter()
        {
            Id = 0;
            ProdutoId = 0;
            ModeloId = 0;
            Nome = "";
        }
    }
}
