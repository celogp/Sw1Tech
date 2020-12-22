using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sw1Tech.Domain.Entities;

namespace Sw1Tech.Infra.Context.Mapping.EF
{
    public class LocalizacaoMapper
    {
        public LocalizacaoMapper(EntityTypeBuilder<Localizacao> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Cep).IsRequired();
            entityBuilder.Property(t => t.Logradouro);
            entityBuilder.Property(t => t.Complemento);
            entityBuilder.Property(t => t.Localidade);
            entityBuilder.Property(t => t.Uf);
            entityBuilder.Property(t => t.Bairro);
            entityBuilder.Property(t => t.Longitude);
            entityBuilder.Property(t => t.Latitude);
            entityBuilder.Property(t => t.DhAtualizacao);

            //entityBuilder.Ignore(t => t.ValidationResult);
            //entityBuilder.Ignore(t => t.IsValid);
        }
    }
}
