using Dapper.Contrib.Extensions;

namespace Sw1Tech.WinF.Integracao.Models
{
    [Table("Cad_CidadeUf")]
    public class CadCidadeUf
    {
        [ExplicitKey]
        public int    Cdduf_codigo { get; set; }
        public string Cdduf_cidade { get; set; }
        public string Cdduf_estado { get; set; }
        public string Cduf_pais { get; set; }
    }
}
