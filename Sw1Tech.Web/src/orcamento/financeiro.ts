import { inject } from 'aurelia-framework';
import { FinanceiroModel } from './financeiromodel';
import { FinanceiroService } from '../services/FinanceiroService';
import * as moment from "moment";

@inject(FinanceiroService)
export class Financeiro extends FinanceiroModel{
    heading = "Financeiro";
    Parcelas : number = 1;
    Dias : number = 30;
    Taxa : number = 0;
    PrimeiroVcto : string;
    VlrOrcamento : number;
    VlrAreceber : number;
    VlrPendente : number;
    NomeFormaPagamento:string;
    lstFpgt = [];
    lstFina = [];

    DoObterNomeFormaPagamento(Id : number){
        let Fpgt = this.lstFpgt.filter(i => i.id == Id)[0].nome;
        return Fpgt;
    }

    DoLoadFina() {
        let fina = {id: this.Id, orcamentoId : this.OrcamentoId, 
            parceiroId : this.ParceiroId, formaPagamentoId : this.FormaPagamentoId, nomeFormaPagamento : this.DoObterNomeFormaPagamento(this.FormaPagamentoId),
            dtMovimento : moment(this.DtMovimento).format('YYYY-MM-DD'), dtVencimento: moment(this.DtVencimento).format('YYYY-MM-DD'), 
            parcela : this.Parcela, vlrTaxa : this.VlrTaxa,
            vlrParcela : this.VlrParcela, vlrSaldo: this.VlrSaldo,
            historico : this.Historico, recDesp : this.RecDesp};
        return fina;
    }

    DoCalcular() {
        if (this.VlrPendente > 0){
            let fina;
            let nroParcela = this.lstFina.length+1;
            let difParcela = 0;
            let difTaxa = 0;
            let totTaxa = (this.VlrAreceber - this.VlrOrcamento );
            this.VlrTaxa = parseFloat( (totTaxa / this.Parcelas).toFixed(2));
            this.VlrParcela = parseFloat( (this.VlrOrcamento / this.Parcelas).toFixed(2));
            this.VlrSaldo = this.VlrParcela + this.VlrTaxa;
            difParcela = parseFloat(  (this.VlrOrcamento - (this.VlrParcela * this.Parcelas)).toFixed(2));
            difTaxa = parseFloat(  (totTaxa - (this.VlrTaxa * this.Parcelas)).toFixed(2));
            for (let index = 0; index < this.Parcelas; index++) {
                this.DtVencimento = moment(this.PrimeiroVcto).add((this.Dias * (index+1)), 'days').format('YYYY-MM-DD');
                this.Parcela = nroParcela;
                if ((difParcela != 0) && (nroParcela >= this.Parcelas)) {
                    this.VlrParcela = this.VlrParcela + difParcela;
                    this.VlrTaxa = this.VlrTaxa + difTaxa;
                    this.VlrSaldo = this.VlrParcela + this.VlrTaxa;
                }
                fina = this.DoLoadFina();
                this.lstFina.push(fina);
                nroParcela++;
            };
            this.VlrPendente = 0;
            this.VlrAreceber = 0;
        }
    }

    DoApagar() {
        return this.finaService.DoApagarLstFinanceiro(this.lstFina);
    }

    DoSalvar(){
        return this.finaService.DoSalvarLstFinanceiro(this.lstFina);
    }

    DoObter(){
        let Where = { Id: 0, OrcamentoId : this.OrcamentoId };
        this.lstFina = [];
        return this.finaService.DoPesquisar(Where).then(response => {
            this.lstFina = response;
            this.lstFina.forEach(item => {
                item.nomeFormaPagamento = this.DoObterNomeFormaPagamento(item.formaPagamentoId);
            } );
            if (this.lstFina.length != 0){
                this.VlrAreceber = 0;
                this.VlrPendente = 0;
            }
        });
    }

    async DoSomarLstFina() : Promise<number> {
        let VlrTotLista = 0;
        let Where = { Id: 0, OrcamentoId : this.OrcamentoId };
        await this.finaService.DoPesquisar(Where).then(response => {
            this.lstFina = response;
            this.lstFina.forEach(item => {
                VlrTotLista = VlrTotLista + item.vlrParcela;
            } );
        });
        return VlrTotLista;
    }

    DoObterFormaPagamento() {
        return this.finaService.DoPesquisarFormaPagamento()
        .then(response => {
            this.lstFpgt = response;
            this.DoObterRegraFormaPagamento(this.lstFpgt[0].id);
        });
    }

    DoObterRegraFormaPagamento(id){
        this.lstFpgt.forEach(item => {
            if (item.id == id){
                this.Dias = item.dias;
                this.Parcelas = item.parcelas;
                this.Taxa = item.taxa;
                //não calcular desconto se já tiver feito algum cálculo
                // if ((this.VlrOrcamento != this.VlrPendente) && (this.Taxa <=0)) {
                //     this.Taxa = 0;
                // }
               this.VlrAreceber = this.DoCalcularParcela(this.Taxa, this.VlrPendente);
               return this.VlrAreceber;
            }
        });
    }

    DoCalcularParcela(taxa, vlrPendente){
        let vlrParcela = vlrPendente;
        if (taxa != 0){
            vlrParcela = vlrParcela + (vlrParcela * taxa / 100);
        }
        return vlrParcela;
    }

    constructor(private finaService : FinanceiroService) {
        super();
        this.DoObterFormaPagamento();
        this.RecDesp = 1;
    }
}