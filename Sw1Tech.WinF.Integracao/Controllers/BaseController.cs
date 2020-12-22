using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Sw1Tech.Domain.Entities;
using Sw1Tech.WinF.Integracao.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Sw1Tech.WinF.Integracao.Controllers
{
    public class BaseController
    {
        private const string _strVazia = "";
        private CadUsuario cadUsuario;
        protected string _conn = "";
        protected string _url = "";
        protected HttpClient clientHttp;
        protected HttpResponseMessage response;
        protected string _jwtIntegra = "";

        public BaseController()
        {
            clientHttp = new HttpClient();
            DoLerConfiguracao();
        }

        protected void DoLogin()
        {
            var _urlUsuario = _url + "Usuario/DoAuthenticado";
            clientHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtIntegra);
            response = clientHttp.GetAsync(_urlUsuario).Result;
            if (response.ReasonPhrase.Equals("Unauthorized"))
            {
                _urlUsuario = _url + "Usuario/DoLogin";
                var content = new StringContent(JsonConvert.SerializeObject(cadUsuario), Encoding.UTF8, "application/json");
                response = clientHttp.PostAsync(_urlUsuario, content).Result;
                // processa a resposta
                var responseString = response.Content.ReadAsStringAsync().Result.ToString();
                var responseJson = JsonConvert.DeserializeObject<StuffResult>(responseString);
                _jwtIntegra = responseJson.Token;
            }
        }

        //Alguns campos na base PHD aceitam nullo, e no envio para a API isso invalida o objeto.
        protected string DoNullToString(string value)
        {
            if (value == null)
            {
                return _strVazia;
            }
            return value;
        }

        protected RegistroExportacaoPhd DoObterRegistroExportacaoPhd(string tabela, MySqlConnection conexao)
        {
            var registroExportacaoPhd = conexao.Query<RegistroExportacaoPhd>(
                "SELECT * FROM TREGISTROEXPORTACAOPHD " +
                "WHERE TABELA = @TABELA",
                new
                {
                    TABELA = tabela
                }).SingleOrDefault();
            return registroExportacaoPhd;
        }

        protected RegistroExportacao DoObterRegistroExportacaoSw1(string tabela)
        {
            var _urlRegistroExportacao = _url + "RegistroExportacao/DoPesquisar";
            var _where = new { Tabela = tabela };
            var content = new StringContent(JsonConvert.SerializeObject(_where), Encoding.UTF8, "application/json");
            clientHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtIntegra);
            response = clientHttp.PostAsync(_urlRegistroExportacao, content).Result;
            // Decode da resposta
            var responseString = response.Content.ReadAsStringAsync().Result.ToString();
            var responseJson = JsonConvert.DeserializeObject<IEnumerable<RegistroExportacao>>(responseString).SingleOrDefault();
            if (responseJson != null)
            {
                return responseJson;
            }
            Logger.LogThisLine("DoObterRegistroExportacaoSw1 - Não conseguiu obter o registro de exportação.");
            return null;
        }

        protected CadParceiroEndereco DoObterCadParceiroEndereco(int Prcr_codigo, MySqlConnection conexao)
        {
            var cadParceiroEndereco = conexao.Query<CadParceiroEndereco>(
                                                "SELECT " +
                                                "PE.* " +
                                                "FROM CAD_PARCEIROENDERECO PE " +
                                                "WHERE PE.PRCR_CODIGO = @PRCR_CODIGO " +
                                                "AND PE.ENDPRCR_TIPO = 1 ",
                                                new { PRCR_CODIGO = Prcr_codigo }).FirstOrDefault();
            if (cadParceiroEndereco == null)
            {
                cadParceiroEndereco = new CadParceiroEndereco();
            }
            return cadParceiroEndereco;
        }

        protected CadParceiroContato DoObterCadParceiroContato(int Prcr_codigo, MySqlConnection conexao)
        {
            var cadParceiroContato = conexao.Query<CadParceiroContato>(
                                        "SELECT  " +
                                        "C.* " +
                                        "FROM CAD_PARCEIROCONTATO C " +
                                        "WHERE C.PRCR_CODIGO = @PRCR_CODIGO " +
                                        "AND C.CLASSIFICACAO = '1' ",
                                        new { PRCR_CODIGO = Prcr_codigo }).FirstOrDefault();
            if (cadParceiroContato == null)
            {
                cadParceiroContato = new CadParceiroContato();
            }
            return cadParceiroContato;
        }

        protected CadParceiroFisico DoObterCadParceiroFisico(int Prcr_codigo, MySqlConnection conexao)
        {
            var cadParceiroFisico = conexao.Query<CadParceiroFisico>(
                "SELECT  " +
                "P.PRCR_CODIGO, P.PRCR_IDENTIDADE, P.PRCR_SEXO " +
                "FROM CAD_PARCEIROFISICO P " +
                "WHERE P.PRCR_CODIGO = @PRCR_CODIGO ",
                new { PRCR_CODIGO = Prcr_codigo }).FirstOrDefault();
            if (cadParceiroFisico == null)
            {
                cadParceiroFisico = new CadParceiroFisico();
            }
            return cadParceiroFisico;
        }

        protected CadParceiroJuridico DoObterCadParceiroJuridico(int Prcr_codigo, MySqlConnection conexao)
        {
            var cadParceiroJuridico = conexao.Query<CadParceiroJuridico>(
                "SELECT  " +
                "P.PRCR_CODIGO, P.PRCR_NOMEFANTASIA, P.PRCR_INSCESTADUAL " +
                "FROM CAD_PARCEIROJURIDICO P " +
                "WHERE P.PRCR_CODIGO = @PRCR_CODIGO ",
                new { PRCR_CODIGO = Prcr_codigo }).FirstOrDefault();
            if (cadParceiroJuridico == null)
            {
                cadParceiroJuridico = new CadParceiroJuridico();
            }
            return cadParceiroJuridico;
        }

        private void DoLerConfiguracao()
        {
            using (StreamReader r = new StreamReader("AppConfig.json"))
            {
                string json = r.ReadToEnd();
                var result = JsonConvert.DeserializeObject<AppConfig>(json);
                _conn = result.Connection;
                _url = result.Url;
                cadUsuario = (new CadUsuario
                {
                    Nome = result.UsrSw1,
                    Senha = result.PwdSw1
                });
                DoLogin();
            }
        }
    }
}
