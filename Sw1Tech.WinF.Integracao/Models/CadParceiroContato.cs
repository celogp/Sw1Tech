using Dapper.Contrib.Extensions;

namespace Sw1Tech.WinF.Integracao.Models
{
    [Table("Cad_ParceiroContato")]
    public class CadParceiroContato
    {
        [ExplicitKey]
        public int Prcrcntt_codigo { get; set; }
        public int Prcr_codigo { get; set; }
        public string Classificacao { get; set; }
        public string Titulo { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
