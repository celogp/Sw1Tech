using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sw1Tech.Domain.Entities;

namespace Sw1Tech.Infra.Context.Mapping.EF
{
    public class OrcamentoAnexoMapper
    {
        public OrcamentoAnexoMapper(EntityTypeBuilder<OrcamentoAnexo> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.OrcamentoId).IsRequired();
            entityBuilder.Property(t => t.Nome).IsRequired();
            entityBuilder.Property(t => t.LinkAnexo).IsRequired();
        }
    }
}
