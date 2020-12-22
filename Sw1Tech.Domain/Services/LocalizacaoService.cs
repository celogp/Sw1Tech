using System;
using System.Linq.Expressions;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Services.Common;
using System.Collections;
using Sw1Tech.Domain.Entities.Validation;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Service
{
    public class LocalizacaoService : Service<Localizacao>, ILocalizacaoService
    {
        private readonly ILocalizacaoRepository _repo;
        public LocalizacaoService(ILocalizacaoRepository repo) : base(repo)
        {
            _repo = repo;
        }

        public IEnumerable DoObterLocalidade(Expression<Func<Localizacao, bool>> where = null)
        {
            return _repo.DoObterLocalidade(where);
        }

        public IEnumerable DoObterBairro(Expression<Func<Localizacao, bool>> where = null)
        {
            return _repo.DoObterBairro(where);
        }

        new public ValidationResult DoIsValid(Localizacao localizacao)
        {
            var fiscal = new LocalizacaoIsValid(_repo);
            ValidationResult.Add(fiscal.Valid(localizacao));
            return ValidationResult;
        }

        public ValidationResult DoExisteDependencia(Localizacao localizacao)
        {
            if (_repo.DoExisteDependencia(localizacao))
            {
                ValidationResult.Add("A Localização não pode ser apagada porque existe ligação com cadastros.");
            }
            return ValidationResult;
        }
    }
}