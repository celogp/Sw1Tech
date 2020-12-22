using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sw1Tech.Domain.Entities;

namespace Sw1Tech.Infra.Context.Mapping.EF
{
    public class ModeloMapper
    {
        public ModeloMapper(EntityTypeBuilder<Modelo> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Nome).IsRequired();
            entityBuilder.Property(t => t.LinkImagem).IsRequired();
            entityBuilder.Property(t => t.Largura).IsRequired();
            entityBuilder.Property(t => t.Comprimento).IsRequired();
            //entityBuilder.Ignore(t => t.ValidationResult);
            //entityBuilder.Ignore(t => t.IsValid);
        }
    }
}
