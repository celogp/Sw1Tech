namespace Sw1Tech.Domain.Entities
{
    public class Ocorrencia : BaseEntity
    {
        public string Nome { get; set; }
        public string Sistema { get; set; }

        public Ocorrencia()
        {
            Sistema = "N";
        }
    }
}
