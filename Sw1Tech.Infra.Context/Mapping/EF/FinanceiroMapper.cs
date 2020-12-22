using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sw1Tech.Domain.Entities;

namespace Sw1Tech.Infra.Context.Mapping.EF
{
    public class FinanceiroMapper
    {
        public FinanceiroMapper(EntityTypeBuilder<Financeiro> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.OrcamentoId);
            entityBuilder.Property(t => t.FormaPagamentoId).IsRequired();
            entityBuilder.Property(t => t.ParceiroId).IsRequired();
            entityBuilder.Property(t => t.DtMovimento).IsRequired();
            entityBuilder.Property(t => t.DtVencimento).IsRequired();
            entityBuilder.Property(t => t.Parcela).IsRequired();
            entityBuilder.Property(t => t.VlrParcela).IsRequired();
            entityBuilder.Property(t => t.VlrTaxa);
            entityBuilder.Property(t => t.VlrSaldo);
            entityBuilder.Property(t => t.Historico);
            entityBuilder.Property(t => t.RecDesp).IsRequired();
        }
    }
}
