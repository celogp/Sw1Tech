using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sw1Tech.Domain.Entities;

namespace Sw1Tech.Infra.Context.Mapping.EF
{
    public class OrcamentoMapper
    {
        public OrcamentoMapper(EntityTypeBuilder<Orcamento> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.ParceiroId);
            entityBuilder.Property(t => t.Projeto);
            entityBuilder.Property(t => t.DtMovimento);
            entityBuilder.Property(t => t.DtEntrega);
            entityBuilder.Property(t => t.UsuarioId);
            entityBuilder.Property(t => t.DiaValidade);
            entityBuilder.Property(t => t.Numero);
            entityBuilder.Property(t => t.Status);

            entityBuilder.Property(t => t.TotBases);
            entityBuilder.Property(t => t.TotProdutos);
            entityBuilder.Property(t => t.TotAcabamentos);
            entityBuilder.Property(t => t.TotAcessorios);
            entityBuilder.Property(t => t.TotServicos);
            entityBuilder.Property(t => t.VlrDesconto);
            entityBuilder.Property(t => t.PerDesconto);
            entityBuilder.Property(t => t.TotOrcamento);
            //entityBuilder.Ignore(t => t.ValidationResult);
            //entityBuilder.Ignore(t => t.IsValid);
        }
    }
}
