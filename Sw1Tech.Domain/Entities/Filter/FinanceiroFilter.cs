using System;

namespace Sw1Tech.Domain.Entities.Filter
{
    public class FinanceiroFilter
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int RecDesp { get; set; }
        public int OrcamentoId { get; set; }
        public int ParceiroId { get; set; }
        public DateTime DtMovimento { get; set; }
        public DateTime DtVencimento { get; set; }

        public FinanceiroFilter()
        {
            Id = 0;
            Nome = "";
            RecDesp = 1;
            OrcamentoId = 0;
            ParceiroId = 0;
        }
    }
}
