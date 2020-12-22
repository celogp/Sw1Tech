using System;

namespace Sw1Tech.Domain.Entities.Filter
{
    public class OrcamentoItemFilter
    {
        public int Id { get; set; }
        public int OrcamentoId { get; set; }
        public int RootId { get; set; }
        public int ProdutoId { get; set; }
        public int Classificacao { get; set; }

        public OrcamentoItemFilter()
        {
            Id = 0;
            OrcamentoId = 0;
            RootId = 0;
            ProdutoId = 0;
            Classificacao = 0;
        }
    }
}
