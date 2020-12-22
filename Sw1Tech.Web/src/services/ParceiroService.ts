import { CFG_URLAPI } from '../appconfig';
import { ServiceBase } from "./ServiceBase";

export class ParceiroService extends ServiceBase{
    private ParceiroUrl: string = CFG_URLAPI.ParceiroUrl;
    private PesquisaTabelaUrl: string = CFG_URLAPI.PesquisaTabelaUrl;

    DoSalvar(parceiro) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(parceiro),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.ParceiroUrl + '/DoSalvar', this.MySetup)
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
            .fetch(this.ParceiroUrl + '/DoPesquisar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoApagar(parceiro) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(parceiro),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.ParceiroUrl + '/DoApagar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoPesquisarSexo() {
        this.MySetup = {
            method: 'GET',
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.PesquisaTabelaUrl + '/DoPesquisarSexo', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }
}