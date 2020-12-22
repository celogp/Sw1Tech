using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sw1Tech.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sw1Tech.Infra.Context.Mapping.EF
{
    public class ParceiroMapper
    {
        public ParceiroMapper(EntityTypeBuilder<Parceiro> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Nome);
            entityBuilder.Property(t => t.Razao);
            entityBuilder.Property(t => t.LocalizacaoId);
            entityBuilder.Property(t => t.Cnpj);
            entityBuilder.Property(t => t.Inscricao);
            entityBuilder.Property(t => t.Cpf);
            entityBuilder.Property(t => t.Identidade);
            entityBuilder.Property(t => t.Email);
            entityBuilder.Property(t => t.Sexo);
            entityBuilder.Property(t => t.Fone);
            entityBuilder.Property(t => t.Celular);
            entityBuilder.Property(t => t.Contato);
            entityBuilder.Property(t => t.FoneContato);
            entityBuilder.Property(t => t.CelularContato);
            entityBuilder.Property(t => t.CelularIsWhatsApp);
            entityBuilder.Property(t => t.CelularContatoIsWhatsApp);
            entityBuilder.Property(t => t.DhAtualizacao);

            //entityBuilder.Ignore(t => t.ValidationResult);
            //entityBuilder.Ignore(t => t.IsValid);

        }
    }
}
