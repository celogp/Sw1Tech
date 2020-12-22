using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sw1Tech.Domain.Entities;

namespace Sw1Tech.Infra.Context.Mapping.EF
{
    public class OcorrenciaMapper
    {
        public OcorrenciaMapper(EntityTypeBuilder<Ocorrencia> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Nome).IsRequired();
            entityBuilder.Property(t => t.Sistema);
            //entityBuilder.HasQueryFilter(t => t.Sistema.Equals('N'));
        }
    }
}
