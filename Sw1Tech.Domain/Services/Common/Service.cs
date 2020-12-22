using System.Collections.Generic;
using Sw1Tech.Domain.Interfaces.Repository.Common;
using Sw1Tech.Domain.Interfaces.Service.Common;
using Sw1Tech.Domain.Validation;
using Sw1Tech.Domain.Interfaces.Validation;
using System;
using System.Linq.Expressions;

namespace Sw1Tech.Domain.Services.Common
{
    public class Service<TEntity> : IService<TEntity>
        where TEntity : class
    {
        private readonly IRepository<TEntity> _repo;
        private readonly ValidationResult _validationResult;

        public Service(IRepository<TEntity> repo)
        {
            _repo = repo;
            _validationResult = new ValidationResult();
        }

        protected ValidationResult ValidationResult
        {
            get { return _validationResult; }
        }

        public ValidationResult DoAdicionar(TEntity entity)
        {
            if (!_validationResult.IsValid)
                return ValidationResult;

            if (entity is ISelfValidation selfValidationEntity && !selfValidationEntity.IsValid)
                return selfValidationEntity.ValidationResult;

            var adicionou = _repo.DoAdicionar(entity);
            return _validationResult;
        }

        public ValidationResult DoAtualizar(TEntity entity)
        {
            if (!ValidationResult.IsValid)
                return ValidationResult;

            if (entity is ISelfValidation selfValidationEntity && !selfValidationEntity.IsValid)
                return selfValidationEntity.ValidationResult;

            var atualizar = _repo.DoAtualizar(entity);
            if (!atualizar)
                _validationResult.Add("A Entidade que você está tentando atualizar está nula, por favor tente novamente! Nome: " + entity + "Atualizar");
            return _validationResult;
        }

        public ValidationResult DoDeletar(TEntity entity)
        {
            if (!ValidationResult.IsValid)
                return ValidationResult;

            var deletou = _repo.DoDeletar(entity);
            if (!deletou)
                _validationResult.Add("A Entidade que você está tentando deletar está nula, por favor tente novamente! Nome: " + entity + "Deletar");
            return _validationResult;
        }

        public TEntity DoObterPorId(int id)
        {
            return _repo.DoObterPorId(id);
        }

        public IEnumerable<TEntity> DoObterTodos()
        {
            return _repo.DoObterTodos();
        }

        public IEnumerable<TEntity> DoObterPor(Expression<Func<TEntity, bool>> where = null)
        {
            return _repo.DoObterPor(where);
        }

        public ValidationResult DoDeletarRange(IEnumerable<TEntity> entities)
        {
            if (!ValidationResult.IsValid)
                return ValidationResult;

            var deletou = _repo.DoDeletarRange(entities);
            if (!deletou)
                _validationResult.Add("A Entidade que você está tentando deletar está nula, por favor tente novamente! Nome: " + entities + "Deletar");
            return _validationResult;
        }

        public ValidationResult DoAtualizarRange(IEnumerable<TEntity> entities)
        {
            if (!ValidationResult.IsValid)
                return ValidationResult;

            if (entities is ISelfValidation selfValidationEntity && !selfValidationEntity.IsValid)
                return selfValidationEntity.ValidationResult;

            var atualizar = _repo.DoAtualizarRange(entities);
            if (!atualizar)
                _validationResult.Add("A Entidade que você está tentando atualizar está nula, por favor tente novamente! Nome: " + entities + "Atualizar");
            return _validationResult;
        }
        
        public ValidationResult DoAdicionarRange(IEnumerable<TEntity> entities)
        {
            if (!_validationResult.IsValid)
                return ValidationResult;

            if (entities is ISelfValidation selfValidationEntity && !selfValidationEntity.IsValid)
                return selfValidationEntity.ValidationResult;

            var adicionou = _repo.DoAdicionarRange(entities);
            return _validationResult;
        }

        public ValidationResult DoIsValid(TEntity entity)
        {
            return _validationResult;
        }
    }
}