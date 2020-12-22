using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Sw1Tech.Domain.Entities;
using Sw1Tech.WinF.Integracao.Models;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Sw1Tech.WinF.Integracao.Controllers
{
    public class ExportacaoPHDController : BaseController
    {
        private RegistroExportacao registroExportacao;
        private RegistroExportacaoPhd registroExportacaoPhd;

        public ExportacaoPHDController()
        {
            registroExportacao = new RegistroExportacao();
            registroExportacaoPhd = new RegistroExportacaoPhd();
        }

        private void DoSalvarRegistroExportacaoPhd(RegistroExportacaoPhd registroExportacaoPhd, MySqlConnection conexao)
        {
            conexao.Query(
                "UPDATE TREGISTROEXPORTACAOPHD SET " +
                "DHULTEXPORTACAO = @DHULTEXPORTACAO " +
                "WHERE TABELA = @TABELA",
                new
                {
                    TABELA = registroExportacaoPhd.Tabela,
                    DHULTEXPORTACAO = registroExportacaoPhd.DhUltExportacao
                }).SingleOrDefault();
        }

        private CadEndereco DoObterCadEndereco(int Endr_codigo, MySqlConnection conexao)
        {
            var cadEndereco = conexao.Query<CadEndereco>(
                                "SELECT " +
                                "CE.ENDR_CODIGO, CE.ENDR_TIPO, CE.ENDR_LOGRADOURO, " +
                                "CE.ENDR_BAIRRO, CE.ENDR_CEP, CE.CDDUF_CODIGO, CE.AD_LOCALIZACAOID " +
                                "FROM CAD_ENDERECO CE " +
                                "WHERE CE.ENDR_CODIGO = @ENDR_CODIGO ",
                                new { ENDR_CODIGO = Endr_codigo }).FirstOrDefault();
            if (cadEndereco == null)
            {
                cadEndereco = new CadEndereco();
            }
            return cadEndereco;
        }

        private CadCidadeUf DoObterCidadePorCodigo(int Cdduf_codigo, MySqlConnection conexao)
        {
            var cadCidadeUf = conexao.Query<CadCidadeUf>(
                "SELECT " +
                "CC.CDDUF_CIDADE, CC.CDDUF_ESTADO, CC.CDUF_PAIS " +
                "FROM CAD_CIDADEUF CC " +
                "WHERE " +
                "CC.CDDUF_CODIGO = @CDDUF_CODIGO ",
                new { CDDUF_CODIGO = Cdduf_codigo }).FirstOrDefault();

            if (cadCidadeUf == null)
            {
                cadCidadeUf = new CadCidadeUf();
            }
            return cadCidadeUf;
        }

        //Le e exporta o endereço da base Phd
        private Localizacao DoExportarLocalizacao(CadParceiro cadParceiro, MySqlConnection conexao)
        {
            var cadParceiroEndereco = DoObterCadParceiroEndereco(cadParceiro.Prcr_codigo, conexao);
            var cadEndereco = DoObterCadEndereco(cadParceiroEndereco.Endr_codigo, conexao);
            var cadCidadeUf = DoObterCidadePorCodigo(cadEndereco.Cdduf_codigo, conexao);

            var localizacao = (new Localizacao
            {
                Id = cadEndereco.Ad_LocalizacaoId,
                Bairro = "",
                Cep = "",
                Complemento = "",
                Logradouro = "",
                Localidade = "",
                Uf = ""
            });

            if (cadEndereco.Endr_cep != null)
            {
                localizacao.Bairro = cadEndereco.Endr_bairro;
                localizacao.Cep = cadEndereco.Endr_cep;
                localizacao.Logradouro = cadEndereco.Endr_logradouro;
                if (cadCidadeUf.Cdduf_cidade != "")
                {
                    localizacao.Localidade = cadCidadeUf.Cdduf_cidade;
                    localizacao.Uf = cadCidadeUf.Cdduf_estado.Substring(0, 2);
                }
                //
                var _urlLocalizacao = _url + "Localizacao/DoSalvar";
                var content = new StringContent(JsonConvert.SerializeObject(localizacao), Encoding.UTF8, "application/json");
                clientHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtIntegra);
                response = clientHttp.PostAsync(_urlLocalizacao, content).Result;
                // Decode da resposta
                var responseString = response.Content.ReadAsStringAsync().Result.ToString();
                var responseJson = JsonConvert.DeserializeObject<StuffResult>(responseString);
                //
                if (responseJson == null)
                {
                    Logger.LogThisLine("DoExportarLocalizacao - Objeto Localizacao contem campos nullos, não fo possível salvar na API");
                }
                else if (!responseJson.ValidationResult.IsValid)
                {
                    Logger.LogThisLine("DoExportarLocalizacao - Erro no objeto localizacao : " + localizacao.Localidade);
                    foreach (var item in responseJson.ValidationResult.Errors)
                    {
                        Logger.LogThisLine("DoExportarLocalizacao - Erro no objeto localizacao :" + item.Message.ToString());
                    }
                }
                else if ((cadEndereco.Ad_LocalizacaoId == 0) && (responseJson.Id != 0))
                {
                    //Gravo o ID do Sw1Tech na base da Phd
                    conexao.Execute("UPDATE CAD_ENDERECO SET " +
                        "AD_LOCALIZACAOID = @AD_LOCALIZACAOID," +
                        "AD_DHEXPORTACAO = AD_DHEXPORTACAO " +
                        "WHERE ENDR_CODIGO = @ENDR_CODIGO",
                        new
                        {
                            ENDR_CODIGO = cadEndereco.Endr_codigo,
                            AD_LOCALIZACAOID = responseJson.Id
                        });
                    localizacao.Id = responseJson.Id;
                }
            }
            return localizacao;
        }

        //Le o parceiro na base Phd
        public void DoExportarParceiro()
        {
            using (MySqlConnection conexao = new MySqlConnection(_conn))
            {
                registroExportacaoPhd = DoObterRegistroExportacaoPhd("CAD_PARCEIRO", conexao);
                var cadParceiroLst = conexao.Query<CadParceiro>(
                    "SELECT " +
                    "P.* " +
                    "FROM CAD_PARCEIRO P " +
                    "WHERE P.AD_DHEXPORTACAO > @AD_DHEXPORTACAO",
                    new { AD_DHEXPORTACAO = registroExportacaoPhd.DhUltExportacao });

                var qtdClientes = cadParceiroLst.Count();
                if (qtdClientes != 0)
                {
                    //Faz o login ou recupera o Token
                    DoLogin();
                    if (_jwtIntegra == "") { return; }
                    registroExportacao = DoObterRegistroExportacaoSw1("TPARCEIRO");
                    foreach (var cadParceiro in cadParceiroLst)
                    {
                        //Parceiro Juridico
                        var cadParceiroJuridico = DoObterCadParceiroJuridico(cadParceiro.Prcr_codigo, conexao);
                        //Parceiro Fisico
                        var cadParceiroFisico = DoObterCadParceiroFisico(cadParceiro.Prcr_codigo, conexao);
                        //Parceiro contato
                        var cadParceiroContato = DoObterCadParceiroContato(cadParceiro.Prcr_codigo, conexao);
                        //Parceiro Endereco para a localizacao
                        var localizacao = DoExportarLocalizacao(cadParceiro, conexao);
                        if (localizacao.Id == 0)
                        {
                            localizacao.Id = 1;
                        }
                        var parceiro = (new Parceiro
                        {
                            Id = cadParceiro.Ad_Id,
                            Nome = DoNullToString(cadParceiro.Prcr_apelidoabrevia),
                            Razao = DoNullToString(cadParceiro.Prcr_nome),
                            LocalizacaoId = localizacao.Id,
                            Email = DoNullToString(cadParceiro.Prcr_email),
                            Fone = DoNullToString(cadParceiro.Prcr_telprincipal),
                            Celular = DoNullToString(cadParceiro.Prcr_telcelular),
                            Contato = "",
                            FoneContato = "",
                            CelularContato = "",
                            CelularContatoIsWhatsApp = "N",
                            CelularIsWhatsApp = "N",
                            Cpf = "",
                            Cnpj = "",
                            Inscricao = "",
                            Identidade = "",
                            DhAtualizacao = registroExportacao.UltDhAtualizacao
                        });
                        if (cadParceiroContato != null)
                        {
                            parceiro.Contato = cadParceiroContato.Titulo;
                            parceiro.FoneContato = cadParceiroContato.Telefone;
                        }
                        if (cadParceiro.Prcr_tipo == "0")
                        {
                            parceiro.Cpf = cadParceiro.Prcr_cpfcnpj;
                            parceiro.Cnpj = "";
                            parceiro.Inscricao = "";
                            parceiro.Sexo = 1;
                            if (cadParceiroFisico != null)
                            {
                                parceiro.Identidade = DoNullToString(cadParceiroFisico.Prcr_identidade);
                            }
                        }
                        else
                        {
                            parceiro.Cnpj = cadParceiro.Prcr_cpfcnpj;
                            parceiro.Cpf = "";
                            parceiro.Identidade = "";
                            parceiro.Sexo = 3;
                            if (cadParceiroJuridico != null)
                            {
                                parceiro.Inscricao = DoNullToString(cadParceiroJuridico.Prcr_inscestadual);
                            }
                        }
                        //Chama a API para registrar o parceiro e pegar o código gerado.
                        var _urlParceiro = _url + "Parceiro/DoSalvar";
                        var content = new StringContent(JsonConvert.SerializeObject(parceiro), Encoding.UTF8, "application/json");
                        clientHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtIntegra);
                        response = clientHttp.PostAsync(_urlParceiro, content).Result;
                        // Decode da resposta
                        var responseString = response.Content.ReadAsStringAsync().Result.ToString();
                        var responseJson = JsonConvert.DeserializeObject<StuffResult>(responseString);
                        if (responseJson == null)
                        {
                            Logger.LogThisLine("DoExportarParceiro - Objeto Parceiro contem campos nullos, não fo possível salvar na API");
                        }
                        else if (!responseJson.ValidationResult.IsValid)
                        {
                            Logger.LogThisLine("DoExportarParceiro - Erro no objeto parceiro : " + parceiro.Nome + " - " + parceiro.Razao);
                            foreach (var item in responseJson.ValidationResult.Errors)
                            {
                                Logger.LogThisLine("DoExportarParceiro - Erro no objeto parceiro :" + item.Message.ToString());
                            }
                        }
                        else
                        {
                            //Gravo o ID do Sw1Tech na base da Phd
                            if ((cadParceiro.Ad_Id == 0) && (responseJson.Id != 0))
                            {
                                conexao.Execute("UPDATE CAD_PARCEIRO SET " +
                                    "AD_ID = @AD_ID, " +
                                    "AD_DHEXPORTACAO = AD_DHEXPORTACAO " +
                                    "WHERE PRCR_CODIGO = @PRCR_CODIGO ",
                                    new { PRCR_CODIGO = cadParceiro.Prcr_codigo, AD_ID = responseJson.Id });

                            }
                            //Pega a ultima data exportada do parceiro;
                            registroExportacaoPhd.DhUltExportacao = cadParceiroLst.Max(o => o.AD_DHEXPORTACAO);
                            //Atualiza a ultima data de exportacao;
                            DoSalvarRegistroExportacaoPhd(registroExportacaoPhd, conexao);
                        }
                    }
                }
            }
        }

        public void DoExportarProduto()
        {
            using (MySqlConnection conexao = new MySqlConnection(_conn))
            {
                registroExportacaoPhd = DoObterRegistroExportacaoPhd("CAD_PRODUTO", conexao);
                var cadProdutoLst = conexao.Query<CadProduto>(
                    "SELECT " +
                    "P.* " +
                    "FROM CAD_PRODUTOVIEWSW1 P " +
                    "WHERE P.AD_DHEXPORTACAO > @AD_DHEXPORTACAO " +
                    "AND P.AD_INTEGRASW1 = 'S' ",
                    new { AD_DHEXPORTACAO = registroExportacaoPhd.DhUltExportacao });

                var qtdProdutos = cadProdutoLst.Count();
                if (qtdProdutos != 0)
                {
                    //Faz o login ou recupera o Token
                    DoLogin();
                    if (_jwtIntegra == "") { return; }
                    foreach (var cadProduto in cadProdutoLst)
                    {
                        var produto = (new Produto
                        {
                            Id = cadProduto.Ad_Id,
                            Nome = cadProduto.Prdt_descricao, 
                            Volume = cadProduto.Prdtundd_sigla,
                            Classificacao = cadProduto.Ad_classificacao,
                            UsarPrecoProdutoBase = cadProduto.Ad_usarprecoprodutobase, 
                            Preco = cadProduto.Prdtestqu_vlrvenda,
                            Custo = cadProduto.Prdtestqu_vlrultimocusto
                        });

                        //Chama a API para registrar o parceiro e pegar o código gerado.
                        var _urlProduto = _url + "Produto/DoSalvar";
                        var content = new StringContent(JsonConvert.SerializeObject(produto), Encoding.UTF8, "application/json");
                        clientHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtIntegra);
                        response = clientHttp.PostAsync(_urlProduto, content).Result;
                        // Decode da resposta
                        var responseString = response.Content.ReadAsStringAsync().Result.ToString();
                        var responseJson = JsonConvert.DeserializeObject<StuffResult>(responseString);
                        if (responseJson == null)
                        {
                            Logger.LogThisLine("DoExportarProduto - Objeto Produto contem campos nullos, não fo possível salvar na API");
                        }
                        else if (!responseJson.ValidationResult.IsValid)
                        {
                            Logger.LogThisLine("DoExportarProduto - Erro no objeto produto : " + produto.Nome);
                            foreach (var item in responseJson.ValidationResult.Errors)
                            {
                                Logger.LogThisLine("DoExportarProduto - Erro no objeto produto :" + item.Message.ToString());
                            }
                        }
                        else
                        {
                            //Gravo o ID do Sw1Tech na base da Phd
                            if ((cadProduto.Ad_Id == 0) && (responseJson.Id != 0))
                            {
                                conexao.Execute("UPDATE CAD_PRODUTO SET " +
                                    "AD_ID = @AD_ID, " +
                                    "AD_DHEXPORTACAO = AD_DHEXPORTACAO " +
                                    "WHERE PRDT_CODIGO = @PRDT_CODIGO ",
                                    new { PRDT_CODIGO = cadProduto.Prdt_codigo, AD_ID = responseJson.Id });

                            }
                            //Pega a ultima data exportada do parceiro;
                            registroExportacaoPhd.DhUltExportacao = cadProdutoLst.Max(o => o.Ad_DhExportacao);
                            //Atualiza a ultima data de exportacao;
                            DoSalvarRegistroExportacaoPhd(registroExportacaoPhd, conexao);
                        }
                    }
                }
            }
        }
    }
}