using Dapper.Contrib.Extensions;
using System;

namespace Sw1Tech.WinF.Integracao.Models
{
    [Table("Cad_Parceiro")]
    public class CadParceiro
    {
        [ExplicitKey]
        public int    Prcr_codigo { get; set; }
        public string Prcr_nome { get; set; }
        public string Prcr_apelidoabrevia { get; set; }
        public string Prcr_cpfcnpj { get; set; }
        public string Prcr_email { get; set; }
        public string Prcr_telprincipal { get; set; }
        public string Prcr_telcelular { get; set; }
        public string Prcr_tipo { get; set; }
        

        //ligacao com Sw1Tech
        public int Ad_Id { get; set; }
        public DateTime AD_DHEXPORTACAO { get; set; }
    }
}
