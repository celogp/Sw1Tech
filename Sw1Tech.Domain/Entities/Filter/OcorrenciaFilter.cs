namespace Sw1Tech.Domain.Entities.Filter
{
    public class OcorrenciaFilter
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public OcorrenciaFilter()
        {
            Id = 0;
            Nome = "";
        }

    }
}
