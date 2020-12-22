using System;

namespace Sw1Tech.Domain.Entities
{
    public class Orcamento : BaseEntity
    {
        //public int Id { get; set; }
        public int ParceiroId { get; set; }
        public string  Projeto { get; set; }
        public DateTime DtMovimento { get; set; }
        public DateTime DtEntrega { get; set; }
        public int UsuarioId { get; set; }
        public int DiaValidade { get; set; }
        public int Numero { get; set; }
        public int Status { get; set; }
        public decimal TotBases { get; set; }
        public decimal TotProdutos { get; set; }
        public decimal TotAcabamentos { get; set; }
        public decimal TotAcessorios { get; set; }
        public decimal TotServicos { get; set; }
        public decimal VlrDesconto { get; set; }
        public decimal PerDesconto { get; set; }
        public decimal TotOrcamento { get; set; }
        public Parceiro Parceiro { get; set; }
        public Usuario Usuario { get; set; }

        public Orcamento()
        {
            TotBases = 0;
            TotProdutos = 0;
            TotAcabamentos = 0;
            TotAcessorios = 0;
            TotServicos = 0;
            VlrDesconto = 0;
            PerDesconto = 0;
            TotOrcamento = 0;
        }
    }
}