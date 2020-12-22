using System;
using System.Collections.Generic;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Validation;
using Sw1Tech.Domain.Entities;
//using Sw1Tech.Infra.Context.Interfaces.Dapper;
using Sw1Tech.Infra.Context.Interfaces.EF;
using System.Linq.Expressions;
using System.Collections;
using System.Net;
using System.Xml.Linq;
using System.Linq;

namespace Sw1Tech.App
{
    public class LocalizacaoAppService : AppService, ILocalizacaoAppService
    {
        private readonly ILocalizacaoService _service;
        private readonly IUnitOfWork _uow;

        private const string VIACEP_URL = "https://viacep.com.br/ws";


        public LocalizacaoAppService(ILocalizacaoService service, IUnitOfWork uow)
        {
            _service = service;
            _uow = uow;
        }

        public ValidationResult DoAdicionar(Localizacao localizacao)
        {
            ValidationResult.Add(_service.DoIsValid(localizacao));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAdicionar(localizacao));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoAtualizar(Localizacao localizacao)
        {
            ValidationResult.Add(_service.DoIsValid(localizacao));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAtualizar(localizacao));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoDeletar(Localizacao localizacao)
        {
            if (localizacao.Id != 0){
                ValidationResult.Add(_service.DoExisteDependencia(localizacao));
                if (ValidationResult.IsValid){
                    _uow.DoBeginTransaction();
                    ValidationResult.Add(_service.DoDeletar(localizacao));
                    if (ValidationResult.IsValid) _uow.DoCommit();
                }
            }
            return ValidationResult;
        }

        public Localizacao DoObterPorId(int id)
        {
            return _service.DoObterPorId(id);
        }

        public IEnumerable<Localizacao> DoObterTodos()
        {
            return _service.DoObterTodos();
        }

        public IEnumerable<Localizacao> DoObterPor(Expression<Func<Localizacao, bool>> where = null)
        {
            return _service.DoObterPor(where);
        }

        public IEnumerable DoObterLocalidade(Expression<Func<Localizacao, bool>> where = null)
        {
            return _service.DoObterLocalidade(where);
        }

        public IEnumerable DoObterBairro(Expression<Func<Localizacao, bool>> where = null)
        {
            return _service.DoObterBairro(where);
        }

        public IEnumerable<Localizacao> DoBuscarPorCEP(string strViaCep)
        {
            var url = $"{VIACEP_URL}/{strViaCep}/xml";
            var ret = DoConsultaCEP(url);
            return ret;
        }

        private IEnumerable<Localizacao> DoConsultaCEP(string url)
        {
            var ret = new List<Localizacao>();
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ProtocolVersion = HttpVersion.Version10;
            webRequest.UserAgent = "Mozilla/4.0 (compatible; Synapse)";

            webRequest.KeepAlive = true;
            webRequest.Headers.Add(HttpRequestHeader.KeepAlive, "150");

            try
            {
                var response = webRequest.GetResponse();
                var xmlStream = response.GetResponseStream();
                var doc = XDocument.Load(xmlStream);

                var rootElement = doc.Element("xmlcep");

                if (rootElement == null) return ret;

                if (rootElement.Element("enderecos") != null)
                {
                    var element = rootElement.Element("enderecos");
                    if (element == null) return ret;

                    var elements = element.Elements("endereco");
                    ret.AddRange(elements.Select(DoProcessElement));
                }
                else
                {
                    var endereco = DoProcessElement(rootElement);
                    ret.Add(endereco);
                }
            }
            catch (Exception)
            {
                return ret;
            }
            return ret;
        }

        private Localizacao DoProcessElement(XElement element)
        {
            var endereco = new Localizacao
            {
                Cep = element.Element("cep").Value.Replace("-", ""),
                Logradouro = element.Element("logradouro").Value,
                Complemento = element.Element("complemento").Value,
                Bairro = element.Element("bairro").Value,
                Localidade = element.Element("localidade").Value,
                Uf = element.Element("uf").Value
            };

            return endereco;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}