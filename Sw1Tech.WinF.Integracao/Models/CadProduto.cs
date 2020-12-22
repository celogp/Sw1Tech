using Dapper.Contrib.Extensions;
using System;

namespace Sw1Tech.WinF.Integracao.Models
{
    [Table("Cad_Produto")]
    public class CadProduto
    {
        [ExplicitKey]
        public int    Prdt_codigo { get; set; }
        public string Prdt_descricao { get; set; }
        public string Prdtundd_sigla { get; set; }
        public decimal Prdtestqu_vlrvenda { get; set; }
        public decimal Prdtestqu_vlrultimocusto { get; set; }
        //
        public int      Ad_Id { get; set; }
        public int      Ad_classificacao { get; set; }
        public string   Ad_usarprecoprodutobase { get; set; }
        public DateTime Ad_DhExportacao { get; set; }
    }
}
