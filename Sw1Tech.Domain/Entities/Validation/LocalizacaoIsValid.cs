using Sw1Tech.Domain.Entities.Especification.LocalizacaoEspec;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Entities.Validation
{
    public class LocalizacaoIsValid : Validation<Localizacao>
    {
        public LocalizacaoIsValid(ILocalizacaoRepository repo)
        {
            base.AddRule(new ValidationRule<Localizacao>(new LocalizacaoCepNaoPodeSerBrancoOuNulo(), "CEP da localização NÃO pode ser branco ou null."));
            base.AddRule(new ValidationRule<Localizacao>(new LocalizacaoLogradouroNaoPodeSerBrancoOuNulo(), "Endereço da localização NÃO pode ser branco ou null."));
            base.AddRule(new ValidationRule<Localizacao>(new LocalizacaoLocalidadeNaoPodeSerBrancoOuNulo(), "Cidade da localização NÃO pode ser branca ou null."));
            base.AddRule(new ValidationRule<Localizacao>(new LocalizacaoUfNaoPodeSerBrancoOuNulo(), "UF da Cidade da localização NÃO pode ser branco ou null."));
            base.AddRule(new ValidationRule<Localizacao>(new LocalizacaoBairroNaoPodeSerBrancoOuNulo(), "Bairro da localização NÃO pode ser branco ou null."));
            base.AddRule(new ValidationRule<Localizacao>(new LocalizacaoCepNaoPodeExistir(repo), "Cep da localização já está cadastrada."));
        }
    }
}