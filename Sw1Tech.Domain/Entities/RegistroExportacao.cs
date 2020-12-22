using System;

namespace Sw1Tech.Domain.Entities
{
    public class RegistroExportacao: BaseEntity
    {
        public string   Tabela { get; set; }
        public DateTimeOffset ? UltDhAtualizacao { get; set; }
    }
}
