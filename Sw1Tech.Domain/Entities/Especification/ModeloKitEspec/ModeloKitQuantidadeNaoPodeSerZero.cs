using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.ModeloKitEspec
{
    public class ModeloKitQuantidadeNaoPodeSerZero : ISpecification<ModeloKit>
    {
        public bool IsSatisfiedBy(ModeloKit modelokit)
        {
            var valido = (modelokit.Quantidade>0);
            return valido;
        }
    }
}
