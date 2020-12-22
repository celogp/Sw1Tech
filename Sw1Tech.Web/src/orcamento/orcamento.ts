import { inject, computedFrom } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';
import { ValidationControllerFactory, ValidationController, ValidationRules } from 'aurelia-validation';
import { OrcamentoModel } from './orcamentomodel';
import { ICrud } from '../ICrud';
import { ParceiroService } from '../services/ParceiroService';
import { OrcamentoService } from '../services/OrcamentoService';
import { OrcamentoItem } from './orcamentoitem';
import { Financeiro } from './financeiro';
import { EclassificacaoProduto } from '../enum/eclassificacaoproduto';
import * as moment from "moment";
import { EstatusOrcamento } from '../enum/estatusorcamento';
import { CFG_USUARIO } from '../appconfig';
import * as $ from 'jquery';

@inject(OrcamentoService, ParceiroService, OrcamentoItem, Financeiro, ValidationControllerFactory, EventAggregator)
export class Orcamento extends OrcamentoModel implements ICrud {
    heading: string = "Orçamento";
    message: string = "";
    txtPesquisa: string = "";
    lstSrvErro = [];
    isVisibleForm: boolean = true;
    isVisibleGrid: boolean = false;
    isAjaxServer: boolean = false;
    LstCpoPesquisa =  [{"nome": "Id"}, {"nome": "Numero"}, {"nome": "NomeParceiro"}, {"nome" : "NomeUsuario"}];
    CpoPesquisa = "NomeParceiro";

    isPesqOrcamento: boolean;
    isPesqParceiro: boolean;
    isPesqProduto: boolean;
    isPesqModelo: boolean;
    isLstItemPrincipal : boolean = true;
    isFrmItemPrincipal : boolean = false;
    isFrmAmbientes: boolean = false;
    isFrmFinanceiro: boolean = false;
    isFrmBases: boolean = false;
    isLstResumo: boolean = false;
    
    datehoje = new Date;

    controller: ValidationController;
    
    lstParc = [];
    lstVddo = [];
    lstOrca = [];

    MaxId: number = 6;
    MaxNumero: number = 6;
    MaxParceiroId: number = 6;
    MaxProjeto : number = 60;
    MaxDiaValidade : number = 3;
    MaxPercDesconto: number = 8;
    MaxVlrDesconto: number = 18;
    
    DoAtivaGrade() {
        this.txtPesquisa = "";
        if (this.isVisibleForm == true) {
            this.isVisibleGrid = true;
            this.isVisibleForm = false;
        } else {
            this.isVisibleGrid = false;
            this.isVisibleForm = true;
        }
    }

    DoMontaFiltro(){
        let filtro : any;
        let txtPesquisa = this.txtPesquisa;
        if ((this.CpoPesquisa.indexOf("Id") != -1) && (this.txtPesquisa == "")){
            txtPesquisa = "0";
        }
        if (this.isPesqProduto == true){
            if (this.isLstItemPrincipal){
                filtro = { [this.CpoPesquisa]: txtPesquisa, Classificacao: EclassificacaoProduto.FINAL};
            }else{
                filtro = { [this.CpoPesquisa]: txtPesquisa, Classificacao: EclassificacaoProduto.TODOS};
            }
        }else if (this.isPesqModelo == true){
            filtro = {[this.CpoPesquisa] : txtPesquisa, ProdutoId : this.orcamentoItem.ProdutoId };
        }else{
            filtro = {[this.CpoPesquisa] : txtPesquisa};
        }
    
        return filtro;
    }
    
    DoPesquisar() {
        let Where = this.DoMontaFiltro();
        this.isAjaxServer = true;
        if (this.isPesqOrcamento == true) {
            return this.orcaService.DoPesquisar(Where)
                .then(response => {
                    this.lstOrca = response;
                    this.isAjaxServer = false;
                })
                .catch(err => {
                    this.ae.publish('toast', {
                        type : 'error', 
                        message : err.status + ' - ' + err.statusText
                    });
                    this.isAjaxServer = false;
                });
        } else if (this.isPesqParceiro == true) {
            return this.parcService.DoPesquisar(Where)
                .then(response => {
                    this.lstParc = response;
                    this.isAjaxServer = false;
                })
                .catch(err => {
                    this.ae.publish('toast', {
                        type : 'error', 
                        message : err.status + ' - ' + err.statusText
                    });
                    this.isAjaxServer = false;
                });
        } else if (this.isPesqProduto == true) {
            this.orcamentoItem.DoPesquisarProduto(Where);
            this.isAjaxServer = false;
        } else if (this.isPesqModelo == true) {
            // Where = { Nome: this.txtPesquisa, ProdutoId : this.orcamentoItem.ProdutoId };
            this.orcamentoItem.DoPesquisarModelo(Where);
            this.isAjaxServer = false;
        }
    }

    DoEditar(obj: any) {
        this.DoAtivaGrade();
        if (this.isPesqOrcamento) {
            if (this.Id == obj.id && this.TotOrcamento !=0 && this.TotOrcamento != obj.totOrcamento){
                this.ae.publish('toast', {
                    type : 'info', 
                    message : 'Atenção ! o orçamento da tela está diferente do escolhido na consulta !, não pode ser atualizado !.'
                });
            }else{
                this.Id = obj.id;
                this.Projeto = obj.projeto;
                this.Numero = obj.numero;
                this.DtMovimento = moment(obj.dtMovimento).format('YYYY-MM-DD');
                this.ParceiroId = obj.parceiroId;
                this.NomeParceiro = obj.parceiro.nome;
                this.DiaValidade = obj.diaValidade;
                this.DtEntrega = moment(obj.dtEntrega).format('YYYY-MM-DD');
                this.Status = obj.status;
                this.UsuarioId = obj.usuarioId;
                //Id do orçamento para a classe de item
                this.orcamentoItem.OrcamentoId = obj.id;
                //Totalizadores do orçamento
                this.TotProdutos = obj.totProdutos;
                this.TotBases = obj.totBases;
                this.TotAcabamentos = obj.totAcabamentos;
                this.TotAcessorios = obj.totAcessorios;
                this.TotServicos = obj.totServicos;
                this.PerDesconto = obj.perDesconto;
                this.VlrDesconto = obj.vlrDesconto;
                this.TotOrcamento = obj.totOrcamento;
                //buscar todos os produtos finais.
                this.orcamentoItem.DoPesquisarItensOrcados({orcamentoId: obj.id, RootId: 0, Classificacao: EclassificacaoProduto.FINAL});
                //Limpar o formulario de produtos e listas de detalhes
                this.DoLimparFormularioItem();
                this.orcamentoItem.DoLimparListaDetalhesOrcados();
                if (this.isLstItemPrincipal == false){
                    this.DoAtivarLstPrincipal();
                }
                this.orcamentoItem.heading = "Itens";
                this.isFrmItemPrincipal = false;
            }
        } else if (this.isPesqParceiro) {
            this.ParceiroId = obj.id;
            this.NomeParceiro = obj.nome;
        } else if (this.isPesqProduto) {
            this.orcamentoItem.ProdutoId = obj.id;
            this.orcamentoItem.NomeProduto = obj.nome;
            this.orcamentoItem.VlrCusto = obj.custo;
            this.orcamentoItem.VlrUnitario = obj.preco;
            this.orcamentoItem.Classificacao = obj.classificacao;
            this.orcamentoItem.Volume = obj.volume;
            if (this.orcamentoItem.Classificacao == EclassificacaoProduto.SERVICOS){
                this.orcamentoItem.Largura = 1;
                this.orcamentoItem.Comprimento = 1;
            }
            if (this.orcamentoItem.Classificacao == EclassificacaoProduto.FINAL){
                this.orcamentoItem.Quantidade = 1;
            }
            //buscar no modelokit o produto para pegar a quantidade ou iniciar com 1;
            if (obj.modeloId != 0){
                if (obj.classificacao != EclassificacaoProduto.FINAL){
                    this.orcamentoItem.DoPesquisarModeloKit({produtoId : this.orcamentoItem.ProdutoId, modeloId : this.orcamentoItem.ModeloId});
                }
            }
            if ((obj.classificacao != EclassificacaoProduto.FINAL) && (obj.classificacao != EclassificacaoProduto.BASE)){
                if ((obj.usarPrecoProdutoBase == "S") && (this.orcamentoItem.lstPbas.length != 0)) {
                    this.orcamentoItem.VlrUnitario = this.orcamentoItem.lstPbas[ this.orcamentoItem.lstPbas.length-1].vlrUnitario;
                }
            }
        } else if (this.isPesqModelo) {
            this.orcamentoItem.ModeloId = obj.modeloId;
            this.orcamentoItem.NomeModelo = obj.modelo.nome;
            if ((obj.modelo.largura !=0) && (obj.modelo.comprimento != 0)){
                this.orcamentoItem.Largura = obj.modelo.largura;
                this.orcamentoItem.Comprimento = obj.modelo.comprimento;
            }
        }
        this.isPesqOrcamento = true;
        this.isPesqParceiro = false;
        this.isPesqProduto = false;
        this.isPesqModelo = false;
        this.isFrmAmbientes = false;
        this.isFrmFinanceiro = false;
        this.isFrmBases = false;
        this.isLstResumo = false;
        
    }

    DoApagar() {
        if (! this.DoOrcamentoSelecionado()){
            return 
        }
        if (! this.DoOrcamentoBloqueado()){
            return;
        }
        let orca = this.DoLoadOrca();
        this.isAjaxServer = true;
        this.lstSrvErro = [];
        return this.orcaService.DoApagar(orca)
            .then(response => {
                if (response.validationResult.isValid == true) {
                    this.DoLimparFormulario();
                    this.orcamentoItem.lstOprd = [];
                    this.orcamentoItem.DoLimparListaDetalhesOrcados();
                    this.DoLimparFormularioItem();
                    this.ae.publish('toast', {
                        type : 'success', 
                        message : 'Registro apagado com sucesso!.'
                    });
                }else {
                    this.ae.publish('toast', {
                        type : 'error', 
                        message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente.'
                    });
                    this.lstSrvErro = response.validationResult.errors;
                }
                this.isAjaxServer = false;
            })
            .catch(err => {
                this.ae.publish('toast', {
                    type : 'error', 
                    message : err.status + ' - ' + err.statusText
                });
                this.isAjaxServer = false;
            });
    }

    DoGravar(){
        let orca = this.DoLoadOrca();
        this.isAjaxServer = true;
        this.lstSrvErro = [];
        this.controller.validate()
            .then(result => {
                if (result.valid) {
                    return this.orcaService.DoSalvar(orca)
                        .then(response => {
                            if (response.validationResult.isValid == true) {
                                this.Id = response.id;
                                this.orcamentoItem.OrcamentoId = this.Id;
                                this.ae.publish('toast', {
                                    type : 'success', 
                                    message : 'Registro atualizado com sucesso!.'
                                });
                            }else{
                                this.ae.publish('toast', {
                                    type : 'error', 
                                    message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente.'
                                });
                                this.lstSrvErro = response.validationResult.errors;
                            }
                            this.isAjaxServer = false;
                        })
                        .catch(err => {
                            this.ae.publish('toast', {
                                type : 'error', 
                                message : err.status + ' - ' + err.statusText
                            });
                            this.isAjaxServer = false;
                        });
                } else {
                    this.ae.publish('toast', {
                        type : 'error', 
                        message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente.'
                    });
                    this.isAjaxServer = false;
                }
            });
    }

    DoSalvar() {
        if (! this.DoOrcamentoBloqueado()){
            return;
        }
        this.DoGravar();
    }

    DoAdicionar(){
        this.Id = 0;
        //Totalizadores
        this.TotProdutos = 0;
        this.TotBases = 0;
        this.TotAcabamentos = 0;
        this.TotAcessorios = 0;
        this.TotServicos = 0;
        this.VlrDesconto = 0;
        this.PerDesconto = 0;
        this.TotOrcamento = 0;
        this.Status = EstatusOrcamento.PENDENTE;
        this.UsuarioId = localStorage[CFG_USUARIO.Id];
        this.DoSalvar();
        this.orcamentoItem.DoLimparListaProdutosOrcados();
        this.orcamentoItem.DoLimparListaDetalhesOrcados();
        this.DoLimparFormularioItem();
    }

    DoOrcamentoBloqueado(): boolean{
        if ((this.Id != 0) && (this.Status == EstatusOrcamento.BLOQUEADO)){
            this.ae.publish('toast', {
                type : 'info', 
                message : 'Atenção !, Orçamento está bloqueado!.'
            });
            return false;
        }
        return true;
    }

    DoOrcamentoSelecionado(): boolean{
        if ((this.Id == 0) || (this.Id == undefined)){
            this.ae.publish('toast', {
                type : 'info', 
                message : 'Atenção !, nenhum orçamento selecionado !.'
            });
            return false;
        }
        return true;
    }

    async DoBloquear() {
        if (this.Status == EstatusOrcamento.BLOQUEADO){
            this.ae.publish('toast', {
                type : 'info', 
                message : 'Atenção !, orçamento já foi bloqueado!.'
            });
        }else{
            if (await this.DoValidarFinanceiro()){
                this.Status = EstatusOrcamento.BLOQUEADO;
                let orca = this.DoLoadOrca();
                this.isAjaxServer = true;
                this.lstSrvErro = [];
                this.controller.validate()
                    .then(result => {
                        if (result.valid) {
                            return this.orcaService.DoBloquear(orca)
                                .then(response => {
                                    if (response.validationResult.isValid == true) {
                                        this.Id = response.id;
                                        this.orcamentoItem.OrcamentoId = this.Id;
                                        this.ae.publish('toast', {
                                            type : 'success', 
                                            message : 'Registro bloqueado com sucesso!.'
                                        });
                                    }else{
                                        this.ae.publish('toast', {
                                            type : 'error', 
                                            message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente.'
                                        });
                                        this.lstSrvErro = response.validationResult.errors;
                                        this.Status = EstatusOrcamento.BLOQUEADO;
                                    }
                                    this.isAjaxServer = false;
                                })
                                .catch(err => {
                                    this.ae.publish('toast', {
                                        type : 'error', 
                                        message : err.status + ' - ' + err.statusText
                                    });
                                    this.isAjaxServer = false;
                                    this.Status = EstatusOrcamento.BLOQUEADO;
                                });
                        } else {
                            this.ae.publish('toast', {
                                type : 'error', 
                                message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente.'
                            });
                            this.isAjaxServer = false;
                        }
                    });
            }
        }
    }

    async DoValidarFinanceiro() : Promise< boolean> {
        let VlrSomaFinanceiro = 0;
        this.financeiro.OrcamentoId = this.Id;
        await this.financeiro.DoSomarLstFina().then(response => {
            VlrSomaFinanceiro = response;
        });
        if (Math.round(VlrSomaFinanceiro) != Math.round(this.TotOrcamento)){
            this.ae.publish('toast', {
                type : 'info', 
                message : 'Atenção ! financeiro NÃO confere com o total do orçamento !.'
            });
            return false;
        }
        return true;
    }

    DoDuplicar() {
        if ((this.Id == 0) || (this.Id == undefined)){
            this.ae.publish('toast', {
                type : 'info', 
                message : 'Atenção !, nenhum orçamento selecionado para fazer a duplicação !.'
            });
            return;
        }
        this.Status = EstatusOrcamento.PENDENTE;
        this.UsuarioId = localStorage[CFG_USUARIO.Id];
        let orca = this.DoLoadOrca();
        this.isAjaxServer = true;
        this.lstSrvErro = [];
        this.controller.validate()
            .then(result => {
                if (result.valid) {
                    return this.orcaService.DoDuplicar(orca)
                        .then(response => {
                            if (response.validationResult.isValid == true) {
                                this.Id = response.id;
                                this.orcamentoItem.OrcamentoId = this.Id;
                                this.orcamentoItem.DoPesquisarItensOrcados({orcamentoId: this.orcamentoItem.OrcamentoId, RootId: 0, Classificacao: EclassificacaoProduto.FINAL});
                                this.ae.publish('toast', {
                                    type : 'success', 
                                    message : 'Registro DUPLICADO com sucesso !.'
                                });
                            }else{
                                this.ae.publish('toast', {
                                    type : 'error', 
                                    message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente.'
                                });
                                this.lstSrvErro = response.validationResult.errors;
                            }
                            this.isAjaxServer = false;
                        })
                        .catch(err => {
                            this.ae.publish('toast', {
                                type : 'error', 
                                message : err.status + ' - ' + err.statusText
                            });
                            this.isAjaxServer = false;
                        });
                } else {
                    this.ae.publish('toast', {
                        type : 'error', 
                        message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente.'
                    });
                    this.isAjaxServer = false;
                }
            });
    }

    DoEditarItem(obj: any) {
        this.orcamentoItem.Id = obj.id;
        this.orcamentoItem.OrcamentoId = obj.orcamentoId;
        this.orcamentoItem.RootId = obj.rootId;
        this.orcamentoItem.ProdutoId = obj.produtoId;
        this.orcamentoItem.NomeProduto = obj.produto.nome;
        this.orcamentoItem.Volume = obj.produto.volume;
        this.orcamentoItem.Classificacao = obj.classificacao;
        this.orcamentoItem.ModeloId = obj.modeloId;
        this.orcamentoItem.NomeModelo = "";
        this.orcamentoItem.Ambiente = obj.ambiente;
        this.orcamentoItem.Quantidade = obj.quantidade;
        this.orcamentoItem.QuantidadeKit = obj.quantidadeKit;
        this.orcamentoItem.VlrCusto = obj.vlrCusto;
        this.orcamentoItem.VlrUnitario = obj.vlrUnitario;
        this.orcamentoItem.Largura = obj.largura;
        this.orcamentoItem.Comprimento = obj.comprimento;
        this.orcamentoItem.Area = obj.area;
        this.orcamentoItem.PerDesconto = obj.perDesconto;
        this.orcamentoItem.VlrDesconto = obj.vlrDesconto;
        this.orcamentoItem.VlrBruto = obj.vlrBruto;
        this.orcamentoItem.VlrTotal = obj.vlrTotal;
        this.orcamentoItem.IndDescontoProdutoFinal = obj.indDescontoProdutoFinal;
        //buscar Acabamentos, acessorios e serviços do produto escolhido se for produto final
        if (this.orcamentoItem.Classificacao == EclassificacaoProduto.FINAL){
            // this.orcamentoItem.IndDescontoProdutoFinal = obj.indDescontoProdutoFinal;
            this.orcamentoItem.DoPesquisarDetalhesOrcados({OrcamentoId: this.orcamentoItem.OrcamentoId, RootId: this.orcamentoItem.Id, Classificacao: EclassificacaoProduto.TODOS});
        }else{
            this.DoAtivarFrmItem();
        }
    }
    
    DoApagarItem() {
        if (! this.DoOrcamentoSelecionado()){
            return 
        }
        if (! this.DoOrcamentoBloqueado()){
            return;
        }
        let item = this.DoLoadItem();
        this.isAjaxServer = true;
        this.lstSrvErro = [];
        return this.orcamentoItem.DoApagar(item)
            .then(response => {
                if (response.validationResult.isValid == true) {
                    this.DoSetTotalOrcamento(response);
                    //buscar todos os produtos da lista excluida
                    if (this.orcamentoItem.Classificacao == EclassificacaoProduto.FINAL){
                        this.orcamentoItem.DoPesquisarItensOrcados({OrcamentoId: item.orcamentoId, RootId: 0, Classificacao: item.classificacao});
                        this.orcamentoItem.DoLimparListaDetalhesOrcados();
                    }else {
                        this.orcamentoItem.DoPesquisarDetalhesOrcados({OrcamentoId: item.orcamentoId, RootId: item.rootId, Classificacao: item.classificacao});
                    }
                    this.DoLimparFormularioItem();
                    this.ae.publish('toast', {
                        type : 'success', 
                        message : 'Registro apagado com sucesso !.'
                    });
                }else{
                    this.ae.publish('toast', {
                        type : 'error', 
                        message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente.'
                    });
                    this.lstSrvErro = response.validationResult.errors;
                }
                this.isAjaxServer = false;
            })
            .catch(err => {
                this.ae.publish('toast', {
                    type : 'error', 
                    message : err.status + ' - ' + err.statusText
                });
                this.isAjaxServer = false;
            });
    }

    DoGravarItem(item){
        if (! this.DoOrcamentoSelecionado()){
            return 
        }
        if (! this.DoOrcamentoBloqueado()){
            return;
        }
        let ehInclusao = (item.id==0);
        if ( (item.produtoId==0) || (item.produtoId == undefined)) {
            this.ae.publish('toast', {
                type : 'info', 
                message : 'Atenção ! O item precisa ser informado !.'
            });
           return;
        }
        if ((item.classificacao != EclassificacaoProduto.FINAL) && (item.rootId == 0) && (item.id != 0)) {
            item.rootId = item.id;
        }
        this.orcamentoItem.controller.validate()
            .then(result => {
                if (result.valid) {
                    this.isAjaxServer = true;
                    this.lstSrvErro = [];
                    return this.orcamentoItem.DoSalvar(item)
                        .then(response => {
                            if (response.validationResult.isValid == true) {
                                this.DoSetTotalOrcamento(response);
                                this.orcamentoItem.Id = response.validationResult.result.id;
                                if (this.orcamentoItem.Classificacao == EclassificacaoProduto.FINAL){
                                    //Pesquisa com push para já adicionar na lista e não precisar ir no banco de dados.
                                    if (ehInclusao){
                                        this.orcamentoItem.DoPesquisarItensOrcados({Id: this.orcamentoItem.Id});
                                    }
                                    this.orcamentoItem.DoPesquisarDetalhesOrcados({OrcamentoId: this.orcamentoItem.OrcamentoId, RootId: this.orcamentoItem.Id, Classificacao: EclassificacaoProduto.TODOS});
                                }else if (this.orcamentoItem.Classificacao == EclassificacaoProduto.BASE) {
                                    this.orcamentoItem.DoPesquisarDetalhesOrcados({OrcamentoId: this.orcamentoItem.OrcamentoId, RootId: this.orcamentoItem.RootId, Classificacao: EclassificacaoProduto.TODOS});
                                }else{
                                    this.orcamentoItem.DoPesquisarDetalhesOrcados({OrcamentoId: this.orcamentoItem.OrcamentoId, RootId: this.orcamentoItem.RootId, Classificacao: this.orcamentoItem.Classificacao});
                                }
                                this.DoLimparFormularioItem();
                                this.ae.publish('toast', {
                                    type : 'success', 
                                    message : 'Registro atualizado com sucesso !.'
                                });
                            }else{
                                this.ae.publish('toast', {
                                    type : 'error', 
                                    message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente.'
                                });
                                this.lstSrvErro = response.validationResult.errors;
                            }
                            this.isAjaxServer = false;
                        })
                        .catch(err => {
                            this.ae.publish('toast', {
                                type : 'error', 
                                message : err.status + ' - ' + err.statusText
                            });
                            this.isAjaxServer = false;
                        });
                } else {
                    this.ae.publish('toast', {
                        type : 'error', 
                        message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente.'
                    });
                    this.isAjaxServer = false;
                }
            });
    }

    DoAdicionarItem(){
        let item = this.DoLoadItem();
        item.id = 0;
        this.DoGravarItem(item)
    }
        
    DoSalvarItem() {
        let item = this.DoLoadItem();
        this.DoGravarItem(item);
    }

    DoCarregarItensOrcados(){
        this.isAjaxServer = true;
        if (this.isLstItemPrincipal){
            this.orcamentoItem.DoPesquisarItensOrcados({orcamentoId: this.orcamentoItem.OrcamentoId, RootId: 0, Classificacao: EclassificacaoProduto.FINAL});
        }else{
            this.orcamentoItem.DoPesquisarDetalhesOrcados({OrcamentoId: this.orcamentoItem.OrcamentoId, RootId: this.orcamentoItem.RootId, Classificacao: EclassificacaoProduto.TODOS});
        }
        this.isAjaxServer = false;
    }

    @computedFrom('VlrDesconto', 'PerDesconto', 'TotProdutos', 'TotAcabamentos', 'TotAcessorios', 'TotServicos')
    get GetTotOrcamento() {
        this.TotOrcamento = this.TotProdutos;
        if (this.PerDesconto > 0) {
            this.TotOrcamento = this.TotOrcamento - ((this.TotOrcamento * this.PerDesconto) / 100);
        } 
        if (this.VlrDesconto > 0) {
            this.TotOrcamento = this.TotOrcamento - this.VlrDesconto;
        }
        return (this.TotOrcamento);
    }

    DoLimparFormulario() {
        this.Id = 0;
        this.ParceiroId = 0;
        this.NomeParceiro = "";
        this.Projeto = "";
        this.DtMovimento = "";
        this.DtEntrega = "";
        this.DiaValidade = 3;
        this.Numero = 0;
        this.Status = 0;

        //Totalizadores
        this.TotProdutos = 0;
        this.TotBases = 0;
        this.TotAcabamentos = 0;
        this.TotAcessorios = 0;
        this.TotServicos = 0;
        this.VlrDesconto = 0;
        this.PerDesconto = 0;
        this.TotOrcamento = 0;
    }

    DoLimparFormularioItem() {
        this.orcamentoItem.Id = 0;
        this.orcamentoItem.ProdutoId = 0;
        this.orcamentoItem.NomeProduto = "";
        this.orcamentoItem.Volume = "";
        this.orcamentoItem.QuantidadeKit = 1;
        this.orcamentoItem.VlrCusto = 0;
        this.orcamentoItem.VlrUnitario = 0;
        this.orcamentoItem.PerDesconto = 0;
        this.orcamentoItem.VlrDesconto = 0;
        this.orcamentoItem.VlrBruto = 0;
        this.orcamentoItem.VlrTotal = 0;
        if (this.orcamentoItem.Classificacao == EclassificacaoProduto.FINAL){
            this.orcamentoItem.ModeloId = 0;
            this.orcamentoItem.NomeModelo = "";
            this.orcamentoItem.Ambiente = "";
            this.orcamentoItem.IndDescontoProdutoFinal = 1;
            this.orcamentoItem.Quantidade = 1;
            this.orcamentoItem.Largura = 1;
            this.orcamentoItem.Comprimento = 1;
        }
        this.orcamentoItem.Classificacao = 0;
    }   

    DoLoadOrca() {
        let orca = {
            Id: this.Id, ParceiroId: this.ParceiroId, NomeParceiro: this.NomeParceiro, Projeto: this.Projeto,
            DtMovimento: this.DtMovimento, DtEntrega: this.DtEntrega,
            DiaValidade: this.DiaValidade, Numero: this.Numero,
            Status: this.Status, UsuarioId: this.UsuarioId,
            TotProdutos: this.TotProdutos, TotAcabamentos: this.TotAcabamentos, TotAcessorios: this.TotAcessorios,
            TotServicos: this.TotServicos, VlrDesconto: this.VlrDesconto, PerDesconto: this.PerDesconto,
            TotOrcamento: this.TotOrcamento, TotBases : this.TotBases
        }
        return orca;
    }

    DoLoadItem() {
        let item = {
            id: this.orcamentoItem.Id, orcamentoId: this.orcamentoItem.OrcamentoId, rootId: this.orcamentoItem.RootId, 
            produtoId: this.orcamentoItem.ProdutoId, volume: this.orcamentoItem.Volume, ambiente: this.orcamentoItem.Ambiente, classificacao: this.orcamentoItem.Classificacao,
            quantidade: this.orcamentoItem.Quantidade, quantidadeKit: this.orcamentoItem.QuantidadeKit, largura: this.orcamentoItem.Largura, comprimento: this.orcamentoItem.Comprimento,
            area: this.orcamentoItem.Area, vlrCusto: this.orcamentoItem.VlrCusto, vlrUnitario: this.orcamentoItem.VlrUnitario, vlrDesconto: this.orcamentoItem.VlrDesconto,perDesconto: this.orcamentoItem.PerDesconto,
            vlrBruto: this.orcamentoItem.VlrBruto, vlrTotal: this.orcamentoItem.VlrTotal, modeloId : this.orcamentoItem.ModeloId, indDescontoProdutoFinal : this.orcamentoItem.IndDescontoProdutoFinal
        }
        return item;
    }

    DoAtivarLstPrincipal(){
        this.LstCpoPesquisa =  [{"nome": "Nome"}];
        this.CpoPesquisa = "Nome";
        this.orcamentoItem.RootId = 0;
        if (this.isLstItemPrincipal == true){
            if ((this.orcamentoItem.Id==0) || (this.orcamentoItem.Id == undefined)){
                this.ae.publish('toast', {
                    type : 'info', 
                    message : 'Atenção !, é preciso selecionar um produto na lista de produto !.'
                });
                return
            }
            this.isLstItemPrincipal = false;
            this.orcamentoItem.RootId = this.orcamentoItem.Id;
            this.orcamentoItem.heading = 'Produto: '  + this.orcamentoItem.NomeProduto + ' Qtd :' + this.orcamentoItem.Quantidade + " Vol: " + this.orcamentoItem.Volume ;
        }else{
            this.isLstItemPrincipal = true;
            this.orcamentoItem.heading = "Itens";
        }
        this.orcamentoItem.Id = 0;
        this.orcamentoItem.ProdutoId = 0;
        this.orcamentoItem.NomeProduto = "";
        this.orcamentoItem.Volume = "";
        this.orcamentoItem.VlrCusto = 0;
        this.orcamentoItem.VlrUnitario = 0;
        this.orcamentoItem.VlrBruto = 0;
        this.orcamentoItem.VlrTotal = 0;
        this.orcamentoItem.PerDesconto = 0;
        this.orcamentoItem.VlrDesconto = 0;
        this.orcamentoItem.lstProd = [];
    }

    DoAtivarFrmItem(){
        if (this.isFrmItemPrincipal == true){
            this.isFrmItemPrincipal = false;
            // console.log('Desativando form item DoAtivarFrmItem() ', this.isLstItemPrincipal);
            this.orcamentoItem.DoPesquisarItensOrcados({orcamentoId: this.orcamentoItem.OrcamentoId, RootId: 0, Classificacao: EclassificacaoProduto.FINAL});
            this.orcamentoItem.DoPesquisarDetalhesOrcados({OrcamentoId: this.orcamentoItem.OrcamentoId, RootId: this.orcamentoItem.RootId, Classificacao: EclassificacaoProduto.TODOS});
        }else{
            this.isFrmItemPrincipal = true;
            // console.log('ativando form item DoAtivarFrmItem() ', this.isLstItemPrincipal);
        }
    }

    DoAtivaLstOrcamento() {
        this.LstCpoPesquisa =  [{"nome": "Id"}, {"nome": "Numero"}, {"nome": "NomeParceiro"}, {"nome":"NomeUsuario"}];
        this.CpoPesquisa = "NomeParceiro";
        this.DoAtivaGrade();
        this.isPesqOrcamento = true;
        this.isPesqParceiro = false;
        this.isPesqProduto = false;
        this.isPesqModelo = false;
        if (this.Id != 0 && this.Id != undefined) {
            this.CpoPesquisa = "Id";
            this.txtPesquisa = this.Id.toString();
            this.DoPesquisar();
        }
    }

    DoAtivaLstParceiro() {
        this.LstCpoPesquisa =  [{"nome": "Id"}, {"nome": "Nome"}, {"nome": "Razao"}, {"nome": "Cnpj"}, {"nome": "Cpf"}, {"nome": "Email"}, {"nome": "Fone"}, {"nome": "Celular"}, {"nome": "Contato"}, {"nome": "Logradouro"} ];
        this.CpoPesquisa = "Nome";
        this.isPesqOrcamento = false;
        this.isPesqParceiro = true;
        this.isPesqProduto = false;
        this.isPesqModelo = false;
        this.DoAtivaGrade();
        if (this.ParceiroId != 0 && this.ParceiroId != undefined) {
            this.CpoPesquisa = "Id";
            this.txtPesquisa = this.ParceiroId.toString();
            this.DoPesquisar();
        }
        
        // if (this.ParceiroId != 0 && this.ParceiroId != undefined) {
        //     this.DoPesquisar(this.ParceiroId);
        // }else{
        //     this.DoAtivaGrade();
        // }
    }

    DoAtivarLstItem(){
        this.LstCpoPesquisa =  [{"nome": "Id"}, {"nome": "Nome"}];
        this.CpoPesquisa = "Nome";
        this.isPesqOrcamento = false;
        this.isPesqParceiro = false;
        this.isPesqProduto = true;
        this.isPesqModelo = false;
        this.DoAtivaGrade();
        if (this.orcamentoItem.ProdutoId != 0 && this.orcamentoItem.ProdutoId != undefined) {
            this.CpoPesquisa = "Id";
            this.txtPesquisa = this.orcamentoItem.ProdutoId.toString();
            this.DoPesquisar();
        }
    }

    DoAtivarLstModelo(){
        this.LstCpoPesquisa =  [{"nome": "ModeloId"}, {"nome": "Nome"}];
        this.CpoPesquisa = "ModeloId";
        this.isPesqOrcamento = false;
        this.isPesqParceiro = false;
        this.isPesqProduto = false;
        this.isPesqModelo = true;
        this.DoAtivaGrade();
        if (this.orcamentoItem.ModeloId != 0 && this.orcamentoItem.ModeloId != undefined) {
            this.txtPesquisa = this.orcamentoItem.ModeloId.toString();
            this.DoPesquisar();
        }
        // if (this.orcamentoItem.ModeloId != 0 && this.orcamentoItem.ModeloId != undefined) {
        //     this.DoPesquisar(this.orcamentoItem.ModeloId);
        // }else{
        //     this.DoAtivaGrade();
        // }
    }
        
    DoAtivarFrmAmbiente(){
        this.orcamentoItem.Ambiente = '';
        if (this.isFrmAmbientes == true){
            this.isFrmAmbientes = false;
        }else{
            this.isFrmAmbientes = true;
            this.DoObterSomaAmbientes();
        }
        this.isFrmFinanceiro = false;
        this.isFrmBases = false;
        this.isLstResumo = false;
    }

    DoObterSomaAmbientes(){
        if (this.DoOrcamentoSelecionado()){
            this.orcamentoItem.DoObterSomaAmbientes({ orcamentoId: this.orcamentoItem.OrcamentoId });
        }
    }

    DoEditarAmbiente(obj: any) {
        this.orcamentoItem.Ambiente = obj.ambiente;
        this.orcamentoItem.VlrBrutoAmbiente = obj.vlrBruto;
        this.orcamentoItem.VlrDescontoAmbiente = obj.vlrDesconto;
        this.orcamentoItem.VlrTotalAmbiente = obj.vlrTotal;
    }

    DoSalvarAmbientes(){
        if (! this.DoOrcamentoBloqueado()){
            return;
        }
        if (this.orcamentoItem.VlrTotalAmbiente != 0){
            return this.orcamentoItem.DoSalvarAmbientes()
            .then(response => {
                if (response.validationResult.isValid == true) {
                    this.DoSetTotalOrcamento(response);
                    this.orcamentoItem.DoPesquisarItensOrcados({orcamentoId: this.orcamentoItem.OrcamentoId, RootId: 0, Classificacao: EclassificacaoProduto.FINAL});
                    this.ae.publish('toast', {
                        type : 'success', 
                        message : 'Registro atualizado com sucesso !.'
                    });
                }
            })
        }
    }

    DoDuplicarAmbientes(){
        if (! this.DoOrcamentoSelecionado()){
            return;
        }
        if (! this.DoOrcamentoBloqueado()){
            return;
        }
        if (this.orcamentoItem.VlrTotalAmbiente != 0){
            return this.orcamentoItem.DoDuplicarAmbientes()
            .then(response => {
                if (response.validationResult.isValid == true) {
                    this.DoSetTotalOrcamento(response);
                    this.orcamentoItem.DoPesquisarItensOrcados({orcamentoId: this.orcamentoItem.OrcamentoId, RootId: 0, Classificacao: EclassificacaoProduto.FINAL});
                    this.ae.publish('toast', {
                        type : 'success', 
                        message : 'Registro duplicado com sucesso !.'
                    });
                }
            })
        }
    }    

    DoSetTotalOrcamento(obj : any){
        this.TotOrcamento = obj.validationResult.result.totOrcamento;
        this.TotBases = obj.validationResult.result.totBases;
        this.TotProdutos = obj.validationResult.result.totProdutos;
        this.TotAcabamentos = obj.validationResult.result.totAcabamentos;
        this.TotAcessorios = obj.validationResult.result.totAcessorios;
        this.TotServicos = obj.validationResult.result.totServicos;
        this.orcamentoItem.VlrTotal = obj.validationResult.result.totProdutos;
    }

    DoAtivarFrmFinanceiro(){
        if (this.isFrmFinanceiro == true){
            this.isFrmFinanceiro = false;
        }else{
            this.isFrmFinanceiro = true;
            this.DoObterFinanceiro();
        }
        this.isFrmAmbientes = false;
        this.isFrmBases = false;
        this.isLstResumo = false;
    }

    DoCalcularFinanceiro(){
        if (! this.DoOrcamentoSelecionado()){
            return ;
        }
        this.financeiro.DoCalcular();
    }

    DoIniciarDadosFinanceiro(){
        this.financeiro.OrcamentoId = this.Id;
        this.financeiro.PrimeiroVcto = this.DtMovimento;
        this.financeiro.ParceiroId = this.ParceiroId;
        this.financeiro.VlrOrcamento = this.TotOrcamento;
        this.financeiro.VlrPendente = this.TotOrcamento;
        this.financeiro.VlrAreceber =  this.financeiro.DoCalcularParcela(this.financeiro.Taxa, this.financeiro.VlrOrcamento);
    }

    DoObterFinanceiro(){
        if (! this.DoOrcamentoSelecionado()){
            return ;
        }
        this.isAjaxServer = true;
        this.DoIniciarDadosFinanceiro();
        this.financeiro.DoObter();
        this.isAjaxServer = false;
    }

    DoSalvarFinanceiro() {
        if (! this.DoOrcamentoSelecionado()){
            return ;
        }
        if (! this.DoOrcamentoBloqueado()){
            return;
        }
        if (this.financeiro.lstFina.length>0){
            this.ae.publish('toast', {
                type : 'info', 
                message : 'Atenção ! financeiro já foi calculado !.'
            });
            return;
        }
        this.isAjaxServer = true;
        this.lstSrvErro = [];
        this.financeiro.DoCalcular();
        return this.financeiro.DoSalvar().then(response => {
            if (response.validationResult.isValid == true) {
                this.ae.publish('toast', {
                    type : 'success', 
                    message : 'Registro atualizado com sucesso !.'
                });
            } else {
                this.ae.publish('toast', {
                    type : 'error', 
                    message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente.'
                });
                this.lstSrvErro = response.validationResult.errors;
            }
            this.isAjaxServer = false;
        }).catch(err => {
            this.ae.publish('toast', {
                type : 'error', 
                message : err.status + ' - ' + err.statusText
            });
            this.isAjaxServer = false;
        });
    }

    DoApagarFinanceiro() {
        if (! this.DoOrcamentoSelecionado()){
            return ;
        }
        if (! this.DoOrcamentoBloqueado()){
            return;
        }
        this.isAjaxServer = true;
        this.lstSrvErro = [];
        return this.financeiro.DoApagar().then(response => {
            if (response.validationResult.isValid == true) {
                this.financeiro.lstFina = [];
                // this.financeiro.VlrAreceber = this.TotOrcamento;
                // this.financeiro.VlrPendente = this.TotOrcamento;
                this.DoIniciarDadosFinanceiro();
                this.ae.publish('toast', {
                    type : 'success', 
                    message : 'Registro apagado com sucesso !.'
                });
            } else {
                this.ae.publish('toast', {
                    type : 'error', 
                    message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente.'
                });
                this.lstSrvErro = response.validationResult.errors;
            }
            this.isAjaxServer = false;
        }).catch(err => {
            this.ae.publish('toast', {
                type : 'error', 
                message : err.status + ' - ' + err.statusText
            });
            this.isAjaxServer = false;
        });
    }

    DoAtivarFrmBase(){
        if (this.isFrmBases == true){
            this.isFrmBases = false;
        }else{
            this.isFrmBases = true;
            this.DoObterSomaBases();
        }
        this.isFrmAmbientes = false;
        this.isFrmFinanceiro = false;
        this.isLstResumo = false;
    }

    DoObterSomaBases(){
        if (this.DoOrcamentoSelecionado()){
            this.orcamentoItem.DoObterSomaBases({ orcamentoId: this.orcamentoItem.OrcamentoId });
        }
    }

    DoEditarBase(obj: any) {
        this.orcamentoItem.ProdutoId = obj.produtoId;
        this.orcamentoItem.CountRegBase = obj.countReg;
        this.orcamentoItem.TotAreaBase = obj.area;
        this.orcamentoItem.TotBrutoBase = obj.vlrBruto;
        this.orcamentoItem.VlrTotalBase = obj.vlrTotal;
        this.orcamentoItem.VlrUnitarioBase = obj.vlrUnitario;
        this.orcamentoItem.PerDescontoBase = obj.perDesconto;
        this.orcamentoItem.VlrDescontoBase = obj.vlrDesconto;
        this.orcamentoItem.IndDescontoBase = obj.indDescontoProdutoFinal;
    }

    DoSalvarBase(){
        if (! this.DoOrcamentoBloqueado()){
            return;
        }
        if (this.orcamentoItem.VlrTotalBase != 0){
            let item = {Id : this.orcamentoItem.Id, OrcamentoId : this.orcamentoItem.OrcamentoId, 
                ProdutoId : this.orcamentoItem.ProdutoId, 
                VlrUnitario : this.orcamentoItem.VlrUnitarioBase, 
                VlrDesconto : this.orcamentoItem.VlrDescontoBase, 
                PerDesconto: this.orcamentoItem.PerDescontoBase, 
                IndDescontoProdutoFinal: this.orcamentoItem.IndDescontoBase};
            return this.orcamentoItem.DoSalvarBase(item)
            .then(response => {
                if (response.validationResult.isValid == true) {
                    this.DoSetTotalOrcamento(response);
                    this.orcamentoItem.DoPesquisarItensOrcados({orcamentoId: this.orcamentoItem.OrcamentoId, RootId: 0, Classificacao: EclassificacaoProduto.FINAL});
                    this.ae.publish('toast', {
                        type : 'success', 
                        message : 'Registro atualizado com sucesso !.'
                    });
                }
            })
        }
    }

    DoAtivarLstResumo(){
        if (this.isLstResumo == true){
            this.isLstResumo = false;
        }else{
            this.isLstResumo = true;
            this.DoObterSomaDetalhes();
        }
        this.isFrmAmbientes = false;
        this.isFrmFinanceiro = false;
        this.isFrmBases = false;
    }

    DoObterSomaDetalhes(){
        if (this.DoOrcamentoSelecionado()){
            this.financeiro.VlrOrcamento = this.TotOrcamento;
            this.financeiro.VlrAreceber = this.TotOrcamento;
            this.financeiro.VlrPendente = this.TotOrcamento;
            this.orcamentoItem.DoObterSomaDetalhes({ orcamentoId: this.orcamentoItem.OrcamentoId });
        }
    }

    attached() {
        ValidationRules
            .ensure('Numero').required().withMessage('Numero do orçamento é obrigatorio !.')
            .ensure('ParceiroId').required().withMessage('Parceiro é obrigatorio !.')
            .ensure('DtMovimento').required().withMessage('Dt.Movimento é obrigatorio !.')
            .ensure('DtEntrega').required().withMessage('Dt.Entrega é obrigatoria !.')
            .ensure('DiaValidade').required().withMessage('Validade é obrigatorio !.')
            .on(this);
    }

    constructor(private orcaService: OrcamentoService,
                private parcService: ParceiroService,
                private orcamentoItem : OrcamentoItem,
                private financeiro: Financeiro,
                controllerFactory, 
                private ae : EventAggregator
                ) {
        super();
        this.controller = controllerFactory.createForCurrentScope();
        this.isPesqOrcamento = true;
        this.isPesqParceiro = false;
        this.isPesqProduto = false;
        this.isPesqModelo = false;
        this.isLstItemPrincipal = true;
        this.isFrmItemPrincipal = false;
        this.isFrmFinanceiro = false;
        this.DtMovimento = moment(this.datehoje).format('YYYY-MM-DD');
        this.DtEntrega = moment(this.datehoje).format('YYYY-MM-DD');
        //
        // tem que olhar depois para ver porque não está limpando as variavais "this.orcamentoItem.OrcamentoId"ao ir para outra tela.
        //
        //listas de pesquisas de cadastros
        this.orcamentoItem.lstProd = [];
        this.orcamentoItem.lstMode = [];
        this.orcamentoItem.lstOprd = []; //produtos finais
        this.orcamentoItem.lstPbas = []; //produto base
        this.orcamentoItem.lstPaca = []; //Acabamentos
        this.orcamentoItem.lstPace = []; //Acessorios
        this.orcamentoItem.lstPser = []; //Serviços
        this.orcamentoItem.lstSAmb = []; //Lista somatoria dos Ambientes
        this.orcamentoItem.lstSBas = []; //Lista somatoria das Bases
        this.orcamentoItem.OrcamentoId = 0;
        this.financeiro.lstFina = []; //lista de financeiros
        this.financeiro.OrcamentoId = 0;
    }

    activate() {
        $("#wrapper").toggleClass();
        this.UsuarioId = localStorage[CFG_USUARIO.Id];
    }
    
}