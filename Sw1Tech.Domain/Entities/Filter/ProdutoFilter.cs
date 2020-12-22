namespace Sw1Tech.Domain.Entities.Filter
{
    public class ProdutoFilter
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Classificacao { get; set; }

        public ProdutoFilter()
        {
            Id = 0;
            Nome = "";
            Classificacao = 0;
        }
    }
}
