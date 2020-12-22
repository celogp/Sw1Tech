namespace Sw1Tech.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; set; }
        public string Senha  { get; set; }
        public string SenhaConfirmada { get; set; }
    }
}
