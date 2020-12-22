import {  CFG_URLAPI } from '../appconfig';
import { ServiceBase } from "./ServiceBase";

export class FinanceiroService extends ServiceBase {
    private FinanceiroUrl: string = CFG_URLAPI.FinanceiroUrl;
    private PesquisaTabelaUrl: string = CFG_URLAPI.PesquisaTabelaUrl;
    
    DoSalvar(financeiro) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(financeiro),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.FinanceiroUrl + '/DoSalvar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoPesquisar(where) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(where),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.FinanceiroUrl + '/DoPesquisar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoApagar(financeiro) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(financeiro),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.FinanceiroUrl + '/DoApagar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoSalvarLstFinanceiro(lstFinanceiro) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(lstFinanceiro),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.FinanceiroUrl + '/DoSalvarLstFinanceiro', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoApagarLstFinanceiro(lstFinanceiro) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(lstFinanceiro),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.FinanceiroUrl + '/DoApagarLstFinanceiro', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoPesquisarFormaPagamento() {
        this.MySetup = {
            method: 'GET',
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.PesquisaTabelaUrl + '/DoPesquisarFormaPagamento', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }
    
}