namespace Sw1Tech.Domain.Entities.Filter
{
    public class RegistroExportacaoFilter
    {
        public int Id { get; set; }
        public string Tabela { get; set; }

        public RegistroExportacaoFilter()
        {
            Id = 0;
            Tabela = "";
        }
    }
}
