using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Entities.Validation;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Services.Common;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Service
{
    public class OrcamentoService : Service<Orcamento>, IOrcamentoService
    {
        private readonly IOrcamentoRepository _repo;
        public OrcamentoService(IOrcamentoRepository repo) : base(repo)
        {
            _repo = repo;
        }
        public void DoCalculaVlrLiquido(Orcamento orcamento)
        {
            if (orcamento.PerDesconto>0) {
                orcamento.TotOrcamento = (orcamento.TotOrcamento - (orcamento.TotOrcamento * orcamento.PerDesconto )/100);
            }
            if (orcamento.VlrDesconto>0){
                orcamento.TotOrcamento = orcamento.TotOrcamento - orcamento.VlrDesconto;
            }
        }

        new public ValidationResult DoIsValid(Orcamento orcamento)
        {
            var fiscal = new OrcamentoIsValid(_repo);
            ValidationResult.Add(fiscal.Valid(orcamento));
            return ValidationResult;
        }
    }
}