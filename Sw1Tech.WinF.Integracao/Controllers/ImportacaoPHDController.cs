using Dapper;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Sw1Tech.Domain.Entities;
using Sw1Tech.WinF.Integracao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Sw1Tech.WinF.Integracao.Controllers
{
    public class ImportacaoPHDController: BaseController
    {
        //private string _token;
        private RegistroExportacao registroExportacao;
        private RegistroExportacaoPhd registroExportacaoPhd;

        public ImportacaoPHDController()
        {
            registroExportacao = new RegistroExportacao();
            registroExportacaoPhd = new RegistroExportacaoPhd();
        }

        private CadParceiro DoObterParceiro(int Ad_Id, string Prcr_cpfcnpj, MySqlConnection conexao)
        {
            var cadParceiro = conexao.Query<CadParceiro>("" +
            "SELECT * FROM CAD_PARCEIRO " +
            "WHERE ((PRCR_CPFCNPJ = @PRCR_CPFCNPJ) OR (AD_ID = @AD_ID)) ",
            new { PRCR_CPFCNPJ = Prcr_cpfcnpj, AD_ID = Ad_Id }).FirstOrDefault();
            if (cadParceiro == null)
            {
                cadParceiro = new CadParceiro();
            }
            return cadParceiro;
        }

        private int DoAtualizaCadEndereco(CadEndereco cadEndereco, Localizacao localizacao, MySqlConnection conexao)
        {
            if (cadEndereco.Endr_codigo == 0)
            {
                //var cadCidadeUf = DoObterCadCidadePorLocalizacao(localizacao, conexao);
                var cadCidadeUf = conexao.Query<CadCidadeUf>("" +
                    "SELECT " +
                    "* " +
                    "FROM CAD_CIDADEUF " +
                    "WHERE (CDDUF_CIDADE = @CDDUF_CIDADE) ",
                    new { CDDUF_CIDADE = localizacao.Localidade }).FirstOrDefault();
                if (cadCidadeUf == null)
                {
                    cadCidadeUf = new CadCidadeUf();
                }
                cadEndereco.Cdduf_codigo = cadCidadeUf.Cdduf_codigo;
                if (cadCidadeUf.Cdduf_codigo == 0)
                {
                    cadCidadeUf.Cdduf_cidade = localizacao.Localidade;
                    cadCidadeUf.Cdduf_estado = localizacao.Uf;
                    cadCidadeUf.Cduf_pais = "Brasil";
                    cadEndereco.Cdduf_codigo = (int)conexao.Insert(cadCidadeUf);
                }
                cadEndereco.Cdduf_codigo = cadEndereco.Cdduf_codigo;
                cadEndereco.Endr_bairro = localizacao.Bairro;
                cadEndereco.Endr_logradouro = localizacao.Logradouro;
                cadEndereco.Endr_tipo = localizacao.Logradouro.Substring(0, 3);
                cadEndereco.Endr_cep = localizacao.Cep;
                cadEndereco.Ad_LocalizacaoId = localizacao.Id;
                cadEndereco.Endr_codigo = (int)conexao.Insert(cadEndereco);
            }
            else if (cadEndereco.Ad_LocalizacaoId == 0)
            {
                cadEndereco.Ad_LocalizacaoId = localizacao.Id;
                conexao.Update(cadEndereco);
            }
            return cadEndereco.Endr_codigo;
        }

        private CadEndereco DoObterEnderecoPorLocalizacao(Localizacao localizacao, MySqlConnection conexao)
        {
            var cadEndereco = conexao.Query<CadEndereco>("" +
                "SELECT " +
                "* " +
                "FROM CAD_ENDERECO " +
                "WHERE ((AD_LOCALIZACAOID = @AD_LOCALIZACAOID) " +
                "OR (ENDR_CEP = @ENDR_CEP)) ",
                new {AD_LOCALIZACAOID = localizacao.Id,
                     ENDR_CEP = localizacao.Cep
                }).FirstOrDefault();
            if (cadEndereco == null) {
                cadEndereco = new CadEndereco();
            }
            return cadEndereco;
        }
       
        //Salva o ultimo registro exportado da tabela
        private bool DoSalvarRegistroExportacao(RegistroExportacao registroExportacao)
        {
            var _urlRegistroExportacao = _url + "RegistroExportacao/DoSalvar";
            var content = new StringContent(JsonConvert.SerializeObject(registroExportacao), Encoding.UTF8, "application/json");
            clientHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtIntegra);
            response = clientHttp.PostAsync(_urlRegistroExportacao, content).Result;
            return true;
        }
        
        public void DoImportarParceiro()
        {
            //Faz o login ou recupera o Token
            DoLogin();
            registroExportacao = DoObterRegistroExportacaoSw1("TPARCEIRO");
            var _urlParceiro = _url + "Parceiro/DoPesquisar";
            //Filtro para pegar as ultimas mudanças.
            var _where = new { DhAtualizacao = registroExportacao.UltDhAtualizacao };
            var content = new StringContent(JsonConvert.SerializeObject(_where), Encoding.UTF8, "application/json");
            clientHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtIntegra);
            response = clientHttp.PostAsync(_urlParceiro, content).Result;
            // Decode da resposta
            var responseString = response.Content.ReadAsStringAsync().Result.ToString();
            var responseJson = JsonConvert.DeserializeObject<IEnumerable<Parceiro>>(responseString);
            if (responseJson == null)
            {
                Logger.LogThisLine("DoImportarParceiro - Objeto Parceiro contem campos nullos, não fo possível salvar na API");
            }
            else if (responseJson.Count() != 0)
            {
                using (MySqlConnection conexao = new MySqlConnection(_conn))
                {
                    registroExportacaoPhd = DoObterRegistroExportacaoPhd("CAD_PARCEIRO", conexao);
                    //
                    foreach (var item in responseJson)
                    {
                        var Tipo = "0";
                        var Prcr_cpfcnpj = item.Cpf;
                        if (item.Cnpj != "")
                        {
                            Prcr_cpfcnpj = item.Cnpj;
                            Tipo = "1";
                        }
                        var Sexo = "F";
                        if (item.Sexo == 1)
                        {
                            Sexo = "M";
                        }
                        //Parceiro
                        var cadParceiro = DoObterParceiro(item.Id, Prcr_cpfcnpj, conexao);
                        cadParceiro.Prcr_apelidoabrevia = item.Nome;
                        cadParceiro.Prcr_nome = item.Razao;
                        cadParceiro.Prcr_cpfcnpj = Prcr_cpfcnpj;
                        cadParceiro.Prcr_email = item.Email;
                        cadParceiro.Prcr_telprincipal = item.Fone;
                        cadParceiro.Prcr_telcelular = item.Celular;
                        cadParceiro.Prcr_tipo = Tipo;
                        cadParceiro.Ad_Id = item.Id;
                        //Parceiro Juridico
                        var cadParceiroJuridico = DoObterCadParceiroJuridico(cadParceiro.Prcr_codigo, conexao);
                        cadParceiroJuridico.Prcr_nomefantasia = item.Nome;
                        cadParceiroJuridico.Prcr_inscestadual = item.Inscricao;
                        //Parceiro Fisico
                        var cadParceiroFisico = DoObterCadParceiroFisico(cadParceiro.Prcr_codigo, conexao);
                        cadParceiroFisico.Prcr_identidade = item.Identidade;
                        cadParceiroFisico.Prcr_sexo = Sexo;
                        //Contato
                        var cadParceiroContato = DoObterCadParceiroContato(cadParceiro.Prcr_codigo, conexao);
                        cadParceiroContato.Titulo = item.Contato;
                        cadParceiroContato.Telefone = item.FoneContato;
                        cadParceiroContato.Classificacao = "1";
                        cadParceiroContato.Email = "";
                        //Endereço
                        var cadEndereco = DoObterEnderecoPorLocalizacao(item.Localizacao, conexao);
                        //Endereço do parceiro
                        var cadParceiroEndereco = DoObterCadParceiroEndereco(cadParceiro.Prcr_codigo, conexao);
                        cadParceiroEndereco.Prcr_codigo = cadParceiro.Prcr_codigo;
                        cadParceiroEndereco.Endr_codigo = cadEndereco.Endr_codigo;
                        cadParceiroEndereco.Endprcr_endcomple = item.Localizacao.Complemento;
                        cadParceiroEndereco.Endprcr_tipo = "1";
                        cadParceiroEndereco.Endprcr_endnum = "S/N";
                        cadParceiroEndereco.Endprcr_classifica = "1";
                        cadParceiroEndereco.Endprcr_telefone = "";
                        //Inclusao na base da PHD
                        if (cadParceiro.Prcr_codigo == 0)
                        {
                            cadParceiro.AD_DHEXPORTACAO = registroExportacaoPhd.DhUltExportacao;
                            var Prcr_Codigo = (int) conexao.Insert(cadParceiro);
                            cadParceiroContato.Prcr_codigo = Prcr_Codigo;
                            if (Tipo == "0")
                            {
                                cadParceiroFisico.Prcr_codigo = Prcr_Codigo;
                                conexao.Insert(cadParceiroFisico);
                            }
                            else
                            {
                                cadParceiroJuridico.Prcr_codigo = Prcr_Codigo;
                                conexao.Insert(cadParceiroJuridico);
                            }
                            conexao.Insert(cadParceiroContato);
                            //
                            cadEndereco.Endr_codigo = DoAtualizaCadEndereco(cadEndereco, item.Localizacao, conexao);
                            //Parceiro endereco
                            cadParceiroEndereco.Prcr_codigo = Prcr_Codigo;
                            cadParceiroEndereco.Endr_codigo = cadEndereco.Endr_codigo;
                            conexao.Insert(cadParceiroEndereco);
                        }
                        else
                        {
                            conexao.Update(cadParceiro);
                            //
                            if (cadParceiroContato.Prcrcntt_codigo == 0)
                            {
                                cadParceiroContato.Prcr_codigo = cadParceiro.Prcr_codigo;
                                conexao.Insert(cadParceiroContato);
                            }
                            else
                            {
                                if (cadParceiroContato.Titulo != "")
                                {
                                    conexao.Update(cadParceiroContato);
                                }
                            }
                            if (Tipo == "0")
                            {
                                if (cadParceiroFisico.Prcr_codigo == 0)
                                {
                                    cadParceiroFisico.Prcr_codigo = cadParceiro.Prcr_codigo;
                                    conexao.Insert(cadParceiroFisico);
                                }
                                else
                                {
                                    conexao.Update(cadParceiroFisico);
                                }
                            }
                            else
                            {
                                if (cadParceiroJuridico.Prcr_codigo == 0)
                                {
                                    cadParceiroJuridico.Prcr_codigo = cadParceiro.Prcr_codigo;
                                    conexao.Insert(cadParceiroJuridico);
                                }
                                else
                                {
                                    conexao.Update(cadParceiroJuridico);
                                }
                            }
                            cadEndereco.Endr_codigo = DoAtualizaCadEndereco(cadEndereco, item.Localizacao, conexao);
                            //Parceiro endereco
                            cadParceiroEndereco.Prcr_codigo = cadParceiro.Prcr_codigo;
                            cadParceiroEndereco.Endr_codigo = cadEndereco.Endr_codigo;
                            cadParceiroEndereco.Endprcr_endcomple = item.Localizacao.Complemento;
                            if (cadParceiroEndereco.Endr_codigo == 0)
                            {
                                conexao.Insert(cadParceiroEndereco);
                            }
                            else
                            {
                                conexao.Update(cadParceiroEndereco);
                            }
                        }
                    }
                }
                //salvar ultimo registro exportado dessa tabela
                registroExportacao.UltDhAtualizacao = responseJson.Max(o => o.DhAtualizacao);
                DoSalvarRegistroExportacao(registroExportacao);
            }
        }
    }
}