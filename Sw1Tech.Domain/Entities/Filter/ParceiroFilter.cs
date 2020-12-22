using System;

namespace Sw1Tech.Domain.Entities.Filter
{
    public class ParceiroFilter
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Razao { get; set; }
        public string Cnpj { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Fone { get; set; }
        public string Celular { get; set; }
        public string Contato { get; set; }
        public string Logradouro {get; set;}
        public DateTimeOffset ? DhAtualizacao { get; set; }

        public ParceiroFilter()
        {
            Id = 0;
            Nome = "";
            Razao = "";
            Cnpj = "";
            Cpf = "";
            Email = "";
            Fone = "";
            Celular = "";
            Contato = "";
            Logradouro = "";
        }
    }
}
