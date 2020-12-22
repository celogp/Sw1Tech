using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sw1Tech.Domain.Entities;

namespace Sw1Tech.Infra.Context.Mapping.EF
{
    public class RegistroExportacaoMapper
    {
        public RegistroExportacaoMapper(EntityTypeBuilder<RegistroExportacao> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Tabela).IsRequired();
            entityBuilder.Property(t => t.UltDhAtualizacao);

        }
    }
}
