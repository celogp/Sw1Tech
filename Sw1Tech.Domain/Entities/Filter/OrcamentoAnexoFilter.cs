namespace Sw1Tech.Domain.Entities.Filter
{
    public class OrcamentoAnexoFilter
    {
        public int Id { get; set; }
        public int OrcamentoId { get; set; }

        public OrcamentoAnexoFilter()
        {
            Id = 0;
            OrcamentoId = 0;
        }
    }
}
