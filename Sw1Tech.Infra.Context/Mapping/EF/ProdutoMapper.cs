using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sw1Tech.Domain.Entities;

namespace Sw1Tech.Infra.Context.Mapping.EF
{
    public class ProdutoMapper
    {
        public ProdutoMapper(EntityTypeBuilder<Produto> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Nome).IsRequired();
            entityBuilder.Property(t => t.Volume).IsRequired();
            entityBuilder.Property(t => t.Classificacao).IsRequired();
            entityBuilder.Property(t => t.Preco).IsRequired();
            entityBuilder.Property(t => t.Custo);
            entityBuilder.Property(t => t.UsarPrecoProdutoBase).IsRequired();

            //entityBuilder.Ignore(t => t.ValidationResult);
            //entityBuilder.Ignore(t => t.IsValid);
        }
    }
}
