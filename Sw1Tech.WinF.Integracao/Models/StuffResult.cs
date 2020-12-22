using Sw1Tech.Domain.Validation;
using System;

namespace Sw1Tech.WinF.Integracao.Models
{
    public class StuffResult
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
        public string Token { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataExpiracao { get; set; }
        public ValidationResult ValidationResult { get; set; }

    }
}
