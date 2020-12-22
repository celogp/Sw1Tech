using Dapper.Contrib.Extensions;

namespace Sw1Tech.WinF.Integracao.Models
{
    [Table("Cad_Endereco")]
    public class CadEndereco
    {
        [ExplicitKey]
        public int    Endr_codigo { get; set; }
        public string Endr_tipo { get; set; }
        public string Endr_logradouro { get; set; }
        public string Endr_bairro { get; set; }
        public string Endr_cep { get; set; }
        public int    Cdduf_codigo { get; set; }

        //ligacao com Sw1Tech
        public int Ad_LocalizacaoId { get; set; }

    }
}
