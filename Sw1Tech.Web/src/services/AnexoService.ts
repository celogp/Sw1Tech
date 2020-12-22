import { inject } from 'aurelia-framework';
import { CFG_URLAPI } from '../appconfig';
import { ServiceBase } from "./ServiceBase";

export class AnexoService extends ServiceBase {
    private OrcamentoAnexosUrl: string = CFG_URLAPI.OrcamentoAnexoUrl;

    DoSalvar(orcaAnexo) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(orcaAnexo),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.OrcamentoAnexosUrl + '/DoSalvar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoApagar(orcaAnexo) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(orcaAnexo),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.OrcamentoAnexosUrl + '/DoApagar', this.MySetup)
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
            .fetch(this.OrcamentoAnexosUrl + '/DoPesquisar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }
}