import { CFG_URLAPI } from '../appconfig';
import { ServiceBase } from "./ServiceBase";

export class ProdutoService extends ServiceBase {
    private ProdutoUrl: string = CFG_URLAPI.ProdutoUrl;

    DoSalvar(produto) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(produto),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.ProdutoUrl + '/DoSalvar', this.MySetup)
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
            .fetch(this.ProdutoUrl + '/DoPesquisar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoApagar(produto) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(produto),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.ProdutoUrl + '/DoApagar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }
}