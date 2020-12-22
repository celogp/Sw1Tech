using Dapper.Contrib.Extensions;

namespace Sw1Tech.WinF.Integracao.Models
{
    [Table("Cad_ParceiroJuridico")]
    public class CadParceiroJuridico
    {
        [ExplicitKey]
        public int    Prcr_codigo { get; set; }
        public string Prcr_nomefantasia { get; set; }
        public string Prcr_inscestadual { get; set; }
    }
}
