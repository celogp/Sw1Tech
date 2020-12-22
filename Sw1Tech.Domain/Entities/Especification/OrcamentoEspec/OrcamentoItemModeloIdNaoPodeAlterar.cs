using Sw1Tech.Domain.Enums;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Interfaces.Specification;
using System.Linq;

namespace Sw1Tech.Domain.Entities.Especification.OrcamentoEspec
{
    public class OrcamentoItemModeloIdNaoPodeAlterar : ISpecification<OrcamentoItem>
    {
        private readonly IOrcamentoItemRepository _repo;
        public OrcamentoItemModeloIdNaoPodeAlterar(IOrcamentoItemRepository repo)
        {
            _repo = repo;
        }

        public bool IsSatisfiedBy(OrcamentoItem orcamentoItem)
        {
            var valido = true;
            if ((orcamentoItem.Classificacao == (int)EClassificacaoProduto.FINAL) && (orcamentoItem.ProdutoId != 0) && (orcamentoItem.Id != 0))
            {
                OrcamentoItem oldOrcamentoItem = (OrcamentoItem) _repo.DoObterPor(k => k.Id == orcamentoItem.Id).SingleOrDefault();
                if (oldOrcamentoItem.ModeloId != orcamentoItem.ModeloId)
                {
                    valido = false;
                }
            }
            return valido;
        }
    }
}