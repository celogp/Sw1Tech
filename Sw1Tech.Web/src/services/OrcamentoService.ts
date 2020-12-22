import { CFG_URLAPI } from '../appconfig';
import { ServiceBase } from "./ServiceBase";

export class OrcamentoService extends ServiceBase {
    private OrcamentoUrl: string = CFG_URLAPI.OrcamentoUrl;

    DoSalvar(orca) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(orca),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.OrcamentoUrl + '/DoSalvar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoApagar(orca) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(orca),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.OrcamentoUrl + '/DoApagar', this.MySetup)
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
            .fetch(this.OrcamentoUrl + '/DoPesquisar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoDuplicar(orca) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(orca),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.OrcamentoUrl + '/DoDuplicar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }
    
    DoBloquear(orca) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(orca),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.OrcamentoUrl + '/DoBloquear', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }
}