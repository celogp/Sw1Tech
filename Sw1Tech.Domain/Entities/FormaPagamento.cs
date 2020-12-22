namespace Sw1Tech.Domain.Entities
{
    public class FormaPagamento : BaseEntity
    {
        public string Nome { get; set; }
        public int Parcelas { get; set; }
        public int Dias { get; set; }
        public decimal Taxa { get; set; }
    }
}
