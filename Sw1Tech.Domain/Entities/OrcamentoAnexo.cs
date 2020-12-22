namespace Sw1Tech.Domain.Entities
{
    public class OrcamentoAnexo : BaseEntity
    {
        //public int Id { get; set; }
        public int OrcamentoId { get; set; }
        public string Nome { get; set; }
        public string LinkAnexo { get; set; }
    }
}