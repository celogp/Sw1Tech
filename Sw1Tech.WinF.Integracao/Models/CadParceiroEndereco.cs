using Dapper.Contrib.Extensions;

namespace Sw1Tech.WinF.Integracao.Models
{
    [Table("Cad_ParceiroEndereco")]
    public class CadParceiroEndereco
    {
        [ExplicitKey]
        public int Endprcr_codigo { get; set; }
        public int Prcr_codigo { get; set; }
        public string Endprcr_tipo { get; set; }
        public int Endr_codigo { get; set; }
        public string Endprcr_endnum { get; set; }
        public string Endprcr_endcomple { get; set; }
        public string Endprcr_classifica { get; set; }
        public string Endprcr_telefone { get; set; }
    }
}
