using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sw1Tech.Domain.Entities;

namespace Sw1Tech.Infra.Context.Mapping.EF
{
    public class OrcamentoOcorrenciaMapper
    {
        public OrcamentoOcorrenciaMapper(EntityTypeBuilder<OrcamentoOcorrencia> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.OrcamentoId).IsRequired();
            entityBuilder.Property(t => t.OcorrenciaId).IsRequired();
            entityBuilder.Property(t => t.UsuarioId).IsRequired();
            entityBuilder.Property(t => t.DtOcorrencia).IsRequired();
            entityBuilder.Property(t => t.Historico);
            entityBuilder.Property(t => t.LinkAnexo);
        }
    }
}
