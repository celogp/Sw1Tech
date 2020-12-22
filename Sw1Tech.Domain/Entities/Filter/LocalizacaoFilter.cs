namespace Sw1Tech.Domain.Entities.Filter
{
    public class LocalizacaoFilter
    {
        public int Id { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string StrViaCep { get; set; }

        public LocalizacaoFilter()
        {
            Cep = "";
            Logradouro = "";
            StrViaCep = "";
        }
    }
}
