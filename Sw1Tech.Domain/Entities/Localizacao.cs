using System;

namespace Sw1Tech.Domain.Entities
{
    public class Localizacao : BaseEntity
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Bairro { get; set; }
        public int Longitude { get; set; }
        public int Latitude { get; set; }
        public DateTimeOffset ? DhAtualizacao { get; set; }

        //public ValidationResult ValidationResult { get; private set; }

        //public bool IsValid
        //{
        //    get
        //    {
        //        var fiscal = new LocalizacaoIsValid();
        //        ValidationResult = fiscal.Valid(this);
        //        return ValidationResult.IsValid;
        //    }
        //}

    }
}
