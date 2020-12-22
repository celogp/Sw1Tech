using Sw1Tech.Domain.Entities.Especification.OrcamentoEspec;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Entities.Validation
{
    public class OrcamentoIsValid : Validation<Orcamento>
    {
        public OrcamentoIsValid(IOrcamentoRepository repo)
        {
            base.AddRule(new ValidationRule<Orcamento>(new OrcamentoParceiroIdNaoPodeSerZero(), "Parceiro não foi informado."));
            base.AddRule(new ValidationRule<Orcamento>(new OrcamentoNumeroNaoPodeSerZero(), "Numero de controle do orçamento não foi informado."));
            base.AddRule(new ValidationRule<Orcamento>(new OrcamentoStatusBloqueadoNaoPodeAlterar(repo), "Orçamento está bloqueado."));
            
        }
    }
}