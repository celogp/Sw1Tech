namespace Sw1Tech.Domain.Entities.Filter
{
    public class VendedorFilter
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }

        public VendedorFilter()
        {
            Id = 0;
            Nome = "";
            Apelido = "";
        }

    }
}
