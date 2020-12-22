using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sw1Tech.Domain.Entities;

namespace Sw1Tech.Infra.Context.Mapping.EF
{
    public class ProdutoModeloMapper
    {
        public ProdutoModeloMapper(EntityTypeBuilder<ProdutoModelo> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.ProdutoId);
            entityBuilder.Property(t => t.ModeloId);
            //entityBuilder.Ignore(t => t.ValidationResult);
            //entityBuilder.Ignore(t => t.IsValid);
        }
    }
}
