using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sw1Tech.Domain.Entities;

namespace Sw1Tech.Infra.Context.Mapping.EF
{
    public class FormaPagamentoMapper
    {
        public FormaPagamentoMapper(EntityTypeBuilder<FormaPagamento> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Nome).IsRequired();
            entityBuilder.Property(t => t.Parcelas);
            entityBuilder.Property(t => t.Dias);
            entityBuilder.Property(t => t.Taxa);
        }
    }
}
