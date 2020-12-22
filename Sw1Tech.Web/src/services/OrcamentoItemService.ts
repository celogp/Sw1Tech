import { CFG_URLAPI } from '../appconfig';
import { ServiceBase } from "./ServiceBase";

export class OrcamentoItemService extends ServiceBase {
    private OrcamentoItemUrl: string = CFG_URLAPI.OrcamentoItemUrl;

    DoSalvar(item) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(item),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.OrcamentoItemUrl + '/DoSalvar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoApagar(item) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(item),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.OrcamentoItemUrl + '/DoApagar', this.MySetup)
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
            .fetch(this.OrcamentoItemUrl + '/DoPesquisar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoObterSomaAmbientes(where : any){
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(where),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.OrcamentoItemUrl + '/DoObterSomaAmbientes', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoSalvarAmbientes(itens:any) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(itens),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.OrcamentoItemUrl + '/DoSalvarAmbientes', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoDuplicarAmbientes(itens:any) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(itens),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.OrcamentoItemUrl + '/DoDuplicarAmbientes', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoObterSomaBases(where : any){
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(where),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.OrcamentoItemUrl + '/DoObterSomaBases', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoSalvarBase(itens:any) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(itens),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.OrcamentoItemUrl + '/DoSalvarBase', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoObterSomaDetalhes(where : any){
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(where),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.OrcamentoItemUrl + '/DoObterSomaDetalhes', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }
    
}