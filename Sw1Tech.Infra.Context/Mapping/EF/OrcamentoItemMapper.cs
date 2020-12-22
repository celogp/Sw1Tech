using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sw1Tech.Domain.Entities;

namespace Sw1Tech.Infra.Context.Mapping.EF
{
    public class OrcamentoItemMapper
    {
        public OrcamentoItemMapper(EntityTypeBuilder<OrcamentoItem> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.RootId);
            entityBuilder.Property(t => t.OrcamentoId);
            entityBuilder.Property(t => t.Classificacao);
            entityBuilder.Property(t => t.ModeloId);
            entityBuilder.Property(t => t.ProdutoId);
            entityBuilder.Property(t => t.Ambiente);
            entityBuilder.Property(t => t.Largura);
            entityBuilder.Property(t => t.Area);
            entityBuilder.Property(t => t.Comprimento);
            entityBuilder.Property(t => t.QuantidadeKit);
            entityBuilder.Property(t => t.Quantidade);
            entityBuilder.Property(t => t.VlrCusto);
            entityBuilder.Property(t => t.VlrUnitario);
            entityBuilder.Property(t => t.VlrDesconto);
            entityBuilder.Property(t => t.IndDescontoProdutoFinal);
            entityBuilder.Property(t => t.PerDesconto);
            entityBuilder.Property(t => t.VlrBruto);
            entityBuilder.Property(t => t.VlrTotal);
        }
    }
}
