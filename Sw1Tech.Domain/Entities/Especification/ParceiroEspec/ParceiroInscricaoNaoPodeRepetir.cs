using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.ParceiroEspec
{
    public class ParceiroInscricaoNaoPodeRepetir : ISpecification<Parceiro>
    {
        private readonly IParceiroRepository _repo;
        public ParceiroInscricaoNaoPodeRepetir(IParceiroRepository repo)
        {
            _repo = repo;
        }

        public bool IsSatisfiedBy(Parceiro parceiro)
        {
            var valido = true;
            if (parceiro.Inscricao.ToString() != "")
            {
                valido = _repo.DoExisteNoBanco(k => k.Id != parceiro.Id && k.Inscricao == parceiro.Inscricao);
            }
            return valido;
        }
    }
}
