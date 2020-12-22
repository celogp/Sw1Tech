namespace Sw1Tech.Domain.Entities.Filter
{
    public class LocalidadeFilter
    {
        public string Localidade { get; set; }
        public string Uf { get; set; }

        public LocalidadeFilter()
        {
            Localidade = "";
            Uf = "";
        }
    }
}
