using System;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Services.Common;
using System.Linq.Expressions;
using Sw1Tech.Domain.Validation;
using Sw1Tech.Domain.Entities.Validation;
using System.Collections.Generic;
using System.Linq;

namespace Sw1Tech.Domain.Service
{
    public class UsuarioService : Service<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        public UsuarioService(IUsuarioRepository repo) : base(repo)
        {
            _repo = repo;
        }

        public Usuario DoLogin(Expression<Func<Usuario, bool>> where = null)
        {
            return _repo.DoLogin(where);
        }

        new public ValidationResult DoIsValid(Usuario usuario)
        {
            var fiscal = new UsuarioIsValid();
            ValidationResult.Add(fiscal.Valid(usuario));
            return ValidationResult;
        }

        new public IEnumerable<Usuario> DoObterTodos()
        {
            var usuario = _repo.DoObterTodos().Select( u =>  new Usuario() {Id = u.Id, Nome = u.Nome} ) ;
            return usuario;
            // return _repo.DoObterTodos();
        }

       new public IEnumerable<Usuario> DoObterPor(Expression<Func<Usuario, bool>> where = null)
        {
            var usuario = _repo.DoObterPor(where).Select( u =>  new Usuario() {Id = u.Id, Nome = u.Nome} ) ;
            return usuario;
            // return _service.DoObterPor(where);
        }

        public ValidationResult DoExisteDependencia(Usuario usuario)
        {
            if (_repo.DoExisteDependencia(usuario))
            {
                ValidationResult.Add("O Usuário não pode ser apagado porque existe ligação com os movimentos.");
            }
            return ValidationResult;
        }
    }
}