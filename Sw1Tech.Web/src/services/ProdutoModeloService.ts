import { CFG_URLAPI } from '../appconfig';
import { ServiceBase } from "./ServiceBase";

export class ProdutoModeloService extends ServiceBase {
    private ProdutoModeloUrl: string = CFG_URLAPI.ProdutoModeloUrl;

    DoSalvar(produtoModelo) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(produtoModelo),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.ProdutoModeloUrl + '/DoSalvar', this.MySetup)
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
            .fetch(this.ProdutoModeloUrl + '/DoPesquisar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoApagar(produtoModelo) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(produtoModelo),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.ProdutoModeloUrl + '/DoApagar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }
}