namespace Sw1Tech.Domain.Entities.Filter
{
    public class ModeloFilter
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ModeloFilter()
        {
            Id = 0;
            Nome = "";
        }
    }
}
