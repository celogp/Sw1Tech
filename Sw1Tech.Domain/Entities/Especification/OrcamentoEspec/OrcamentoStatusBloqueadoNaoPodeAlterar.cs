using Sw1Tech.Domain.Enums;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Interfaces.Specification;
using System.Linq;

namespace Sw1Tech.Domain.Entities.Especification.OrcamentoEspec
{
    public class OrcamentoStatusBloqueadoNaoPodeAlterar : ISpecification<Orcamento>
    {
        private readonly IOrcamentoRepository _repo;
        public OrcamentoStatusBloqueadoNaoPodeAlterar(IOrcamentoRepository repo)
        {
            _repo = repo;
        }

        public bool IsSatisfiedBy(Orcamento orcamento)
        {
            var valido = true;
            if (orcamento.Id != 0)
            {
                Orcamento oldOrcamento = (Orcamento) _repo.DoObterPor(k => k.Id == orcamento.Id).SingleOrDefault();
                if (oldOrcamento.Status == (int)EStatusOrcamento.BLOQUEADO)
                {
                    valido = false;
                }
            }
            return valido;
        }
    }
}