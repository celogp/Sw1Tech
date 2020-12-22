using System;

namespace Sw1Tech.Domain.Entities
{
    public class Parceiro : BaseEntity
    {
        public string Nome { get; set; }
        public string Razao { get; set; }
        public int LocalizacaoId { get; set; }
        public string Cnpj { get; set; }
        public string Inscricao { get; set; }
        public string Cpf { get; set; }
        public string Identidade { get; set; }
        public string Email { get; set; }
        public int Sexo { get; set; }
        public string Fone { get; set; }
        public string Celular { get; set; }
        public string Contato { get; set; }
        public string FoneContato { get; set; }
        public string CelularContato { get; set; }
        public string CelularIsWhatsApp { get; set; }
        public string CelularContatoIsWhatsApp { get; set; }
        public DateTimeOffset ? DhAtualizacao { get; set; }

        public Localizacao Localizacao { get; set; }
        //public ValidationResult ValidationResult { get; private set; }
        //public bool IsValid
        //{
        //    get
        //    {
        //        var fiscal = new ParceiroIsValid();
        //        ValidationResult = fiscal.Valid(this);
        //        return ValidationResult.IsValid;
        //    }
        //}
    }
}
