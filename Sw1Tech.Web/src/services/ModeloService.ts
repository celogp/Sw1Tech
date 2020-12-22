import { CFG_URLAPI } from '../appconfig';
import { ServiceBase } from "./ServiceBase";

export class ModeloService extends ServiceBase {
    private modeloUrl: string = CFG_URLAPI.ModeloUrl;
    
    DoSalvar(modelo) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(modelo),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.modeloUrl + '/DoSalvar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoSalvarKit(modeloKit) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(modeloKit),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.modeloUrl + '/DoSalvarKit', this.MySetup)
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
            .fetch(this.modeloUrl + '/DoPesquisar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoPesquisarKit(where) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(where),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.modeloUrl + '/DoPesquisarKit', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoApagar(modelo) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(modelo),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.modeloUrl + '/DoApagar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoApagarKit(modeloKit) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(modeloKit),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.modeloUrl + '/DoApagarKit', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }
    
}