using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.ModeloEspec
{
    public class ModeloLarguraNaoPodeSerZero : ISpecification<Modelo>
    {
        public bool IsSatisfiedBy(Modelo modelo)
        {
            var valido = (modelo.Largura>0);
            return valido;
        }
    }
}
