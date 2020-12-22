using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.ParceiroEspec
{
    public class ParceiroIdentidadeNaoPodeRepetir : ISpecification<Parceiro>
    {
        private readonly IParceiroRepository _repo;
        public ParceiroIdentidadeNaoPodeRepetir(IParceiroRepository repo)
        {
            _repo = repo;
        }

        public bool IsSatisfiedBy(Parceiro parceiro)
        {
            var valido = true;
            if (parceiro.Identidade.ToString() != "")
            {
                valido = _repo.DoExisteNoBanco(k => k.Id != parceiro.Id && k.Identidade == parceiro.Identidade);
            }
            return valido;
        }
    }
}
