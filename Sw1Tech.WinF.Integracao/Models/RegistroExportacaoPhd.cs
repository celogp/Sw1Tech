using Dapper.Contrib.Extensions;
using System;

namespace Sw1Tech.WinF.Integracao.Models
{
    [Table("TREGISTROEXPORTACAOPHD")]
    public class RegistroExportacaoPhd
    {
        [Key]
        public int Id { get; set; }
        public string Tabela { get; set; }
        public DateTime DhUltExportacao { get; set; }
    }
}
