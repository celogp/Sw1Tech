using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.ParceiroEspec
{
    public class ParceiroCpfNaoPodeRepetir : ISpecification<Parceiro>
    {
        private readonly IParceiroRepository _repo;
        public ParceiroCpfNaoPodeRepetir(IParceiroRepository repo)
        {
            _repo = repo;
        }

        public bool IsSatisfiedBy(Parceiro parceiro)
        {
            var valido = true;
            if (parceiro.Cpf.ToString() != "")
            {
                valido = _repo.DoExisteNoBanco(k => k.Id != parceiro.Id && k.Cpf == parceiro.Cpf);
            }
            return valido;
        }
    }
}
