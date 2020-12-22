using System;

namespace Sw1Tech.Domain.Entities.Filter
{
    public class OrcamentoOcorrenciaFilter
    {
        public int UsuarioId { get; set; }
        public int OrcamentoId { get; set; }
        public DateTime DtIniOcorrencia { get; set; }
        public DateTime DtFimOcorrencia { get; set; }

        public OrcamentoOcorrenciaFilter()
        {
            UsuarioId = 0;
            OrcamentoId = 0;
        }
    }
}
