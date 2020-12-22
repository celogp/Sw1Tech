namespace Sw1Tech.Domain.Entities
{
    public class OrcamentoItem : BaseEntity
    {
        //public int Id { get; set; }
        public int OrcamentoId { get; set; }
        public int RootId { get; set; }
        public string Ambiente { get; set; }
        public int  ProdutoId { get; set; }
        public int  Classificacao { get; set; }
        public decimal Quantidade { get; set; }
        public decimal QuantidadeKit { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Area { get; set; }
        public decimal VlrCusto { get; set; }
        public decimal VlrUnitario { get; set; }
        public decimal VlrDesconto { get; set; }
        public decimal PerDesconto { get; set; }
        public decimal IndDescontoProdutoFinal { get; set; }
        public decimal VlrBruto { get; set; }
        public decimal VlrTotal { get; set; }
        public int ? ModeloId { get; set; }
        public Produto Produto { get; set; }

        public OrcamentoItem ()
        {
            RootId = 0;
            QuantidadeKit = 1;
            Quantidade = 0;
            VlrUnitario = 0;
            VlrDesconto = 0;
            PerDesconto = 0;
            IndDescontoProdutoFinal = 1;
            VlrBruto = 0;
            Area = 0;
            Largura = 0;
            Comprimento = 0;
            ModeloId = 0;
        }
    }
}