using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sw1Tech.Domain.Entities;

namespace Sw1Tech.Infra.Context.Mapping.EF
{
    public class UsuarioMapper
    {
        public UsuarioMapper(EntityTypeBuilder<Usuario> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Nome).IsRequired();
            entityBuilder.Property(t => t.Senha).IsRequired();
            entityBuilder.Ignore(t => t.SenhaConfirmada);
            //entityBuilder.Ignore(t => t.ValidationResult);
            //entityBuilder.Ignore(t => t.IsValid);
        }
    }
}
