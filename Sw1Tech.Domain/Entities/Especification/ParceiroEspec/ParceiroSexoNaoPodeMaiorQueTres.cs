using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.ParceiroEspec
{
    public class ParceiroSexoNaoPodeMaiorQueTres : ISpecification<Parceiro>
    {
        public bool IsSatisfiedBy(Parceiro parceiro)
        {
            var valido = (parceiro.Sexo>=1) && (parceiro.Sexo<=3);
            return valido;
        }
    }
}
