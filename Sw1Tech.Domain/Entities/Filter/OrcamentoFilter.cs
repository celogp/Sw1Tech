using System;

namespace Sw1Tech.Domain.Entities.Filter
{
    public class OrcamentoFilter
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public DateTime DtMovimento { get; set; }
        public string NomeParceiro { get; set; }
        public string NomeUsuario { get; set; }

        public OrcamentoFilter()
        {
            Id = 0;
            Numero = 0;
            NomeParceiro = "";
            NomeUsuario = "";
        }
    }
}
