import { inject, computedFrom } from 'aurelia-framework';
import { OrcamentoItemModel } from './orcamentoitemmodel';
import { ProdutoService } from '../services/ProdutoService';
import { ProdutoModeloService } from '../services/ProdutoModeloService';
import { OrcamentoItemService } from '../services/OrcamentoItemService';
import { ModeloService } from '../services/ModeloService';
import { ValidationControllerFactory, ValidationController, ValidationRules } from 'aurelia-validation';
import { EclassificacaoProduto } from '../enum/eclassificacaoproduto';

@inject(ProdutoService, ProdutoModeloService, OrcamentoItemService, ModeloService, ValidationControllerFactory)
export class OrcamentoItem extends OrcamentoItemModel{
    heading: string = "Itens";
    MaxId: number = 6;
    MaxAmbiente : number = 60;
    MaxProdutoId: number = 6;
    MaxModeloId: number = 6;
    MaxQuantidade: number = 18;
    MaxVlrUnitario: number = 18;
    MaxPerDesconto: number = 8;
    MaxVlrDesconto: number = 18;
    MaxLargura: number = 18;
    MaxComprimento: number = 18;
    MaxVlrTotal: number = 18;
    
    //Para calculo dos descontos em ambientes
    VlrBrutoAmbiente : number = 0;
    VlrTotalAmbiente : number = 0;
    VlrDescontoAmbiente : number = 0;
    PerDescontoAmbiente : number = 0;
    IndDescontoAmbiente : number = 0;

    //Para calculo dos descontos em bases
    CountRegBase : number = 1;
    TotBrutoBase : number = 0;
    TotAreaBase : number = 0;
    VlrTotalBase : number = 0;
    VlrUnitarioBase : number = 0;
    VlrDescontoBase : number = 0;
    PerDescontoBase : number = 0;
    IndDescontoBase : number = 0;

    //Para calculo de lucro
    PerLucroBase : number = 0;
    PerLucroAcab : number = 0;
    PerLucroAces : number = 0;
    PerLucroServ : number = 0;
    PerLucro : number = 0;

    //listas de pesquisas de cadastros
    lstProd = [];
    lstMode = [];
    //
    lstOprd = []; //produtos finais
    lstPbas = []; //produto base
    lstPaca = []; //Acabamentos
    lstPace = []; //Acessorios
    lstPser = []; //Serviços
    lstSAmb = []; //Lista somatoria dos Ambientes
    lstSBas = []; //Lista somatoria das Bases
    
    controller: ValidationController;

    DoPesquisarProduto(pesquisa: any) {
        return this.prodService.DoPesquisar(pesquisa)
            .then(response => {
                if(pesquisa.Id != undefined){
                    this.NomeProduto = response[0].nome;
                }
                this.lstProd = response;
            });
    }

    DoPesquisarModeloKit(pesquisa: any) {
        return this.modeloService.DoPesquisarKit(pesquisa)
            .then(response => {
                if (response.length == 0 ){
                    this.QuantidadeKit = 1;
                }else{
                    this.QuantidadeKit = response[0].quantidade;
                }
            });
    }

    DoPesquisarModelo(pesquisa: any) {
        return this.prodModeloService.DoPesquisar(pesquisa)
            .then(response => {
                if(pesquisa.Id != undefined){
                    this.NomeModelo = response[0].modelo.nome;
                }
                this.lstMode = response;
            });
    }

    DoLimparListaProdutosOrcados(){
        this.lstOprd = [];
    }
    
    DoLimparListaDetalhesOrcados(){
        this.lstPbas = []; //produto base
        this.lstPaca = []; //Acabamentos
        this.lstPace = []; //Acessorios
        this.lstPser = []; //Serviços
    }

    DoPesquisarItensOrcados(pesquisa: any) {
        return this.orcamentoItemService.DoPesquisar(pesquisa)
            .then(response => {
                if(pesquisa.Id != undefined){
                    this.lstOprd.push(response[0]);
                }else{
                    this.lstOprd = response;
                }
            });
    }

    DoPesquisarDetalhesOrcados(pesquisa: any) {
        return this.orcamentoItemService.DoPesquisar(pesquisa)
            .then(response => {
                if ( pesquisa.Classificacao == EclassificacaoProduto.TODOS ){
                    this.lstPbas = response.filter((itens) => itens.classificacao == EclassificacaoProduto.BASE);
                    this.lstPaca = response.filter((itens) => itens.classificacao == EclassificacaoProduto.ACABAMENTOS);
                    this.lstPace = response.filter((itens) => itens.classificacao == EclassificacaoProduto.ACESSORIOS);
                    this.lstPser = response.filter((itens) => itens.classificacao == EclassificacaoProduto.SERVICOS);
                }else if (pesquisa.Classificacao == EclassificacaoProduto.BASE){
                    this.lstPbas = response.filter((itens) => itens.classificacao == EclassificacaoProduto.BASE);
                }else if (pesquisa.Classificacao == EclassificacaoProduto.ACABAMENTOS){
                    this.lstPaca = response.filter((itens) => itens.classificacao == EclassificacaoProduto.ACABAMENTOS);
                }else if (pesquisa.Classificacao == EclassificacaoProduto.ACESSORIOS){
                    this.lstPace = response.filter((itens) => itens.classificacao == EclassificacaoProduto.ACESSORIOS );
                }else if (pesquisa.Classificacao == EclassificacaoProduto.SERVICOS){
                    this.lstPser = response.filter((itens) => itens.classificacao == EclassificacaoProduto.SERVICOS );
                }
            });
    }

    DoApagar(Item) {
        return this.orcamentoItemService.DoApagar(Item);
    }

    DoSalvar(Item) {
        return this.orcamentoItemService.DoSalvar(Item);
    }

    @computedFrom('Comprimento','Largura')
    get GetArea() {
        if (this.Classificacao == EclassificacaoProduto.SERVICOS){
            this.Largura = 1;
            this.Comprimento = 1;
            this.Area = 1;
        }else{
            if (((this.Comprimento != undefined) && (this.Comprimento !=0)) &&
                ((this.Largura != undefined) && (this.Largura !=0))) {
                this.Area = this.Comprimento * this.Largura;
            }
        }            
        return (this.Area);
    }

    @computedFrom('QuantidadeKit', 'Quantidade', 'VlrUnitario', 'Area', 'VlrDesconto', 'PerDesconto')
    get GetVlrTotal() {
        if (this.VlrUnitario != undefined){
            this.VlrBruto = this.Quantidade * this.QuantidadeKit * this.Area * this.VlrUnitario;
            this.VlrTotal = this.VlrBruto;
            if (this.PerDesconto > 0) {
                this.VlrTotal = this.VlrTotal - ((this.VlrTotal * this.PerDesconto) / 100);
            }
            if (this.VlrDesconto > 0) {
                this.VlrTotal = this.VlrTotal - this.VlrDesconto;
            }
            if ((this.IndDescontoProdutoFinal != 0) && (this.IndDescontoProdutoFinal != undefined)){
                if ((this.Classificacao == EclassificacaoProduto.FINAL)){
                    this.IndDescontoProdutoFinal = 1;
                }
                this.VlrTotal = this.VlrTotal * this.IndDescontoProdutoFinal;
            }
            // if (this.Classificacao == EclassificacaoProduto.FINAL) {
            //   //Para voltar nem sempre é possível fazendo a mesma conta de ida
            //   if ((this.PerDesconto == 0) && (this.VlrDesconto == 0)) {
            //     this.IndDescontoProdutoFinal = 1;
            //   } else {
            //     this.IndDescontoProdutoFinal = this.VlrTotal / this.VlrBruto;
            //   }
            // } else if ((this.IndDescontoProdutoFinal != 0) && (this.IndDescontoProdutoFinal != undefined)) {
            //     this.VlrTotal = this.VlrTotal * this.IndDescontoProdutoFinal;
            // }
        }
        return (this.VlrTotal);
    }

    DoObterSomaAmbientes(where : any){
        this.VlrDescontoAmbiente = 0;
        this.PerDescontoAmbiente = 0;
        this.VlrBrutoAmbiente = 0;
        this.VlrTotalAmbiente = 0;
        return this.orcamentoItemService.DoObterSomaAmbientes(where)
        .then(response => {
            this.lstSAmb = response;
        });
    };

    DoSalvarAmbientes(){
        let lstSamb = [];
        this.lstOprd.filter((itens) => itens.classificacao == EclassificacaoProduto.FINAL && itens.ambiente == this.Ambiente).forEach(item => {
            item.perDesconto = 0;
            item.vlrDesconto = item.vlrBruto * (1-this.IndDescontoAmbiente);
            item.vlrTotal = item.vlrBruto - item.vlrDesconto;
            item.indDescontoProdutoFinal = item.vlrTotal / item.vlrBruto;
            lstSamb.push(item);
        });
        return this.orcamentoItemService.DoSalvarAmbientes(lstSamb);
    }

    DoDuplicarAmbientes(){
        let lstSamb = this.lstOprd.filter((itens) => itens.classificacao == EclassificacaoProduto.FINAL && itens.ambiente == this.Ambiente);
        return this.orcamentoItemService.DoDuplicarAmbientes(lstSamb);
    }
    
    @computedFrom('VlrBrutoAmbiente','VlrDescontoAmbiente', 'PerDescontoAmbiente')
    get GetVlrTotalAmbiente() {
        this.VlrTotalAmbiente = this.VlrBrutoAmbiente;
        if (this.PerDescontoAmbiente > 0) {
            this.VlrTotalAmbiente = this.VlrTotalAmbiente - ((this.VlrTotalAmbiente * this.PerDescontoAmbiente) / 100);
        }
        if (this.VlrDescontoAmbiente > 0) {
            this.VlrTotalAmbiente = this.VlrTotalAmbiente - this.VlrDescontoAmbiente;
        }
        if ((this.PerDescontoAmbiente == 0) && (this.VlrDescontoAmbiente == 0)) {
            this.IndDescontoAmbiente = 1;
        }else{
            this.IndDescontoAmbiente = this.VlrTotalAmbiente / this.VlrBrutoAmbiente;
        }
        return (this.VlrTotalAmbiente);
    }

    DoObterSomaBases(where : any){
        this.CountRegBase = 1;
        this.TotAreaBase = 0;
        this.TotBrutoBase = 0;
        this.VlrUnitarioBase = 0;
        this.PerDescontoBase = 0;
        this.VlrDescontoBase = 0;
        this.VlrTotalBase = 0;
        return this.orcamentoItemService.DoObterSomaBases(where)
        .then(response => {
            this.lstSBas = response;
        });
    };

    DoSalvarBase(item){
        return this.orcamentoItemService.DoSalvarBase(item);
    }
    
    @computedFrom('TotAreaBase','VlrUnitarioBase','VlrDescontoBase', 'PerDescontoBase')
    get GetVlrTotalBase() {
        this.VlrTotalBase = (this.TotAreaBase * this.VlrUnitarioBase);
        if (this.PerDescontoBase > 0) {
            this.VlrTotalBase = this.VlrTotalBase - (((this.VlrTotalBase * this.PerDescontoBase) / 100) * this.CountRegBase);
        }
        if (this.VlrDescontoBase > 0) {
            this.VlrTotalBase = this.VlrTotalBase - (this.VlrDescontoBase * this.CountRegBase);
        }
        if  (this.IndDescontoBase != undefined) {
            this.VlrTotalBase = this.VlrTotalBase * this.IndDescontoBase;
        }        
        return (this.VlrTotalBase);
    }

    DoObterSomaDetalhes(where : any){
        return this.orcamentoItemService.DoObterSomaDetalhes(where)
        .then(response => {
            let totCus = 0;
            let totLiq = 0;
            let somCus = 0;
            let somLiq = 0;
            this.lstPbas = response.filter((itens) => itens.classificacao == EclassificacaoProduto.BASE);
            this.lstPaca = response.filter((itens) => itens.classificacao == EclassificacaoProduto.ACABAMENTOS);
            this.lstPace = response.filter((itens) => itens.classificacao == EclassificacaoProduto.ACESSORIOS);
            this.lstPser = response.filter((itens) => itens.classificacao == EclassificacaoProduto.SERVICOS);
            //Lucro da base
            this.lstPbas.forEach(item => {
                totCus = totCus + ((item.quantidade * item.quantidadeKit * item.area) * item.vlrCusto) * item.indDescontoProdutoFinal;
                totLiq = totLiq + item.vlrTotal;
            });
            this.PerLucroBase = totCus / totLiq * 100;
            somCus = somCus + totCus;
            somLiq = somLiq + totLiq;
            //Lucro do acabamento
            totCus = 0;
            totLiq = 0;
            this.lstPaca.forEach(item => {
                totCus = totCus + ((item.quantidade * item.quantidadeKit * item.area) * item.vlrCusto) * item.indDescontoProdutoFinal;
                totLiq = totLiq + item.vlrTotal;
            });
            this.PerLucroAcab = totCus / totLiq * 100;
            somCus = somCus + totCus;
            somLiq = somLiq + totLiq;
            //Lucro do acessorio
            totCus = 0;
            totLiq = 0;
            this.lstPace.forEach(item => {
                totCus = totCus + ((item.quantidade * item.quantidadeKit * item.area) * item.vlrCusto) * item.indDescontoProdutoFinal;
                totLiq = totLiq + item.vlrTotal;
            });
            this.PerLucroAces = totCus / totLiq * 100;
            somCus = somCus + totCus;
            somLiq = somLiq + totLiq;
            //Lucro do serviço
            totCus = 0;
            totLiq = 0;
            this.lstPser.forEach(item => {
                totCus = totCus + ((item.quantidade * item.quantidadeKit * item.area) * item.vlrCusto) * item.indDescontoProdutoFinal;
                totLiq = totLiq + item.vlrTotal;
            });
            this.PerLucroServ = totCus / totLiq * 100;
            somCus = somCus + totCus;
            somLiq = somLiq + totLiq;
            this.PerLucro = somCus / somLiq * 100;

        });
    };
    
    DoObterBaseDetalhe(idBase:any){
        let nomeBase = '';
        let qtd = this.lstPbas.length;
        if (qtd > 1) {
            let itemBase = this.lstPbas.forEach(item => {
                if (item.rootId == idBase){
                    nomeBase = item.produto.nome + ' - ';
                    return nomeBase;
                }
            });
            }
        return nomeBase;
    }

    attached() {
        ValidationRules
            .ensure('Quantidade').required().withMessage('Quantidade é obrigatoria !.')
            .ensure('OrcamentoId').required().withMessage('Chave primária do orçamento é obrigatoria !.')
            .ensure('orcamentoItem.Ambiente').required().withMessage('Ambiente é obrigatorio !.')
            .on(this);
    }

    constructor(private prodService: ProdutoService,
                private prodModeloService: ProdutoModeloService,
                private orcamentoItemService: OrcamentoItemService,
                private modeloService : ModeloService,
                controllerFactory
                ) {
        super();
        this.controller = controllerFactory.createForCurrentScope();
    }
}