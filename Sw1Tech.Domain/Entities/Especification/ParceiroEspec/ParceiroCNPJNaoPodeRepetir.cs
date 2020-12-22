using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.ParceiroEspec
{
    public class ParceiroCnpjNaoPodeRepetir : ISpecification<Parceiro>
    {
        private readonly IParceiroRepository _repo;
        public ParceiroCnpjNaoPodeRepetir(IParceiroRepository repo)
        {
            _repo = repo;
        }

        public bool IsSatisfiedBy(Parceiro parceiro)
        {
            var valido = true;
            if (parceiro.Cnpj.ToString() != "")
            {
                valido = _repo.DoExisteNoBanco(k => k.Id != parceiro.Id && k.Cnpj == parceiro.Cnpj);
            }
            return valido;
        }
    }
}
