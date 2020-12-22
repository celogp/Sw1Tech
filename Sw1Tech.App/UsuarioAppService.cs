using System;
using System.Collections.Generic;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Validation;
using Sw1Tech.Domain.Entities;
//using Sw1Tech.Infra.Context.Interfaces.Dapper;
using Sw1Tech.Infra.Context.Interfaces.EF;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace Sw1Tech.App
{
    public class UsuarioAppService : AppService, IUsuarioAppService
    {
        private readonly IUsuarioService _service;
        private readonly IUnitOfWork _uow;

        public UsuarioAppService(IUsuarioService service, IUnitOfWork uow)
        {
            _service = service;
            _uow = uow;
        }

        public ValidationResult DoAdicionar(Usuario usuario)
        {
            //Chamar metodo para gerar md5 da senha
            usuario.Senha = DoSenhaMD5(usuario);
            ValidationResult.Add(_service.DoIsValid(usuario));
            if (!ValidationResult.IsValid){
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAdicionar(usuario));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoAtualizar(Usuario usuario)
        {
            //Chamar metodo para gerar md5 da senha
            usuario.Senha = DoSenhaMD5(usuario);
            ValidationResult.Add(_service.DoIsValid(usuario));
            if (!ValidationResult.IsValid){
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAtualizar(usuario));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoDeletar(Usuario usuario)
        {
            if (usuario.Id != 0) {
                ValidationResult.Add(_service.DoExisteDependencia(usuario));
                if (ValidationResult.IsValid){
                    _uow.DoBeginTransaction();
                    ValidationResult.Add(_service.DoDeletar(usuario));
                    if (ValidationResult.IsValid) _uow.DoCommit();
                }
            }
            return ValidationResult;
        }

        public Usuario DoObterPorId(int id)
        {
            return _service.DoObterPorId(id);
        }

        public IEnumerable<Usuario> DoObterTodos()
        {
            return _service.DoObterTodos();
        }

        public IEnumerable<Usuario> DoObterPor(Expression<Func<Usuario, bool>> where = null)
        {
            return _service.DoObterPor(where);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Usuario DoLogin(Usuario usuario)
        {
            //Chamar metodo para gerar md5 da senha
            usuario.Senha = DoSenhaMD5(usuario);
            return _service.DoLogin(s => s.Nome == usuario.Nome && s.Senha == usuario.Senha);
        }

        private string DoSenhaMD5(Usuario usuario)
        {
            var strSenha = usuario.Senha;
            if (usuario.SenhaConfirmada != null)
            {
                strSenha = usuario.SenhaConfirmada;
            }
            strSenha = (usuario.Nome.Length * 2).ToString() + strSenha + usuario.Nome;
            using (MD5 md5Hash = MD5.Create())
            {
                return DoRetonarHash(md5Hash, strSenha);
            }
        }

        private string DoRetonarHash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

    }
}