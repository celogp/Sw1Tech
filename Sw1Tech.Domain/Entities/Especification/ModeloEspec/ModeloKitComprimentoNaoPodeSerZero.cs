using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.ModeloEspec
{
    public class ModeloComprimentoNaoPodeSerZero : ISpecification<Modelo>
    {
        public bool IsSatisfiedBy(Modelo modelo)
        {
            var valido = (modelo.Comprimento>0);
            return valido;
        }
    }
}
