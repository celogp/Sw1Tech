using Dapper.Contrib.Extensions;

namespace Sw1Tech.WinF.Integracao.Models
{
    [Table("Cad_ParceiroFisico")]
    public class CadParceiroFisico
    {
        [ExplicitKey]
        public int    Prcr_codigo { get; set; }
        public string Prcr_identidade { get; set; }
        public string Prcr_sexo { get; set; }
    }
}
