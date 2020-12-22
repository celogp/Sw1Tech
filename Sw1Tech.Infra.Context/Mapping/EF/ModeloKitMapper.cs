using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sw1Tech.Domain.Entities;

namespace Sw1Tech.Infra.Context.Mapping.EF
{
    public class ModeloKitMapper
    {
        public ModeloKitMapper(EntityTypeBuilder<ModeloKit> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.ModeloId).IsRequired();
            entityBuilder.Property(t => t.ProdutoId).IsRequired();
            entityBuilder.Property(t => t.Quantidade).IsRequired();
            //entityBuilder.Ignore(t => t.ValidationResult);
            //entityBuilder.Ignore(t => t.IsValid);
        }
    }
}
