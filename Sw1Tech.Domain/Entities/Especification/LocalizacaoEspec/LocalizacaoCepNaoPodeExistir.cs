using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Interfaces.Specification;
using System;
using System.Linq;

namespace Sw1Tech.Domain.Entities.Especification.LocalizacaoEspec
{
    public class LocalizacaoCepNaoPodeExistir : ISpecification<Localizacao>
    {
        private readonly ILocalizacaoRepository _repo;
        public LocalizacaoCepNaoPodeExistir(ILocalizacaoRepository repo)
        {
            _repo = repo;
        }

        public bool IsSatisfiedBy(Localizacao localizacao)
        {
            var valido = true;
            if (localizacao.Id == 0){
                valido = (_repo.DoObterPor(l => l.Cep.Equals(localizacao.Cep)).Count() == 0);
            }
            return valido;
        }
    }
}