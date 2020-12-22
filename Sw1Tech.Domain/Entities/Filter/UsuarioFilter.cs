namespace Sw1Tech.Domain.Entities.Filter
{
    public class UsuarioFilter
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }

        public UsuarioFilter()
        {
            Id = 0;
            Nome = "";
            Senha = "";
        }
    }
}
