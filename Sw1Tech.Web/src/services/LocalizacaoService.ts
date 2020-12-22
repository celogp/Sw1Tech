import { CFG_URLAPI } from '../appconfig';
import { ServiceBase } from "./ServiceBase";

export class LocalizacaoService extends ServiceBase {
    private LocalizacaoUrl: string = CFG_URLAPI.LocalizacaoUrl;
    private PesquisaTabelaUrl: string = CFG_URLAPI.PesquisaTabelaUrl;
    private PesquisaViaCep: string = CFG_URLAPI.ViaCepUrl;
    
    DoSalvar(localizacao) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(localizacao),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.LocalizacaoUrl + '/DoSalvar', this.MySetup)
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
            .fetch(this.LocalizacaoUrl + '/DoPesquisar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoPesquisarLocalidade(where) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(where),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.LocalizacaoUrl + '/DoPesquisarLocalidade', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoPesquisarBairro(where) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(where),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.LocalizacaoUrl + '/DoPesquisarBairro', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoApagar(localizacao) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(localizacao),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.LocalizacaoUrl + '/DoApagar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoPesquisarUf() {
        this.MySetup = {
            method: 'GET',
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.PesquisaTabelaUrl + '/DoPesquisarUf', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoPesquisarCepCorreio(cep){
        let url = this.PesquisaViaCep+cep+'/json/'
        this.MySetup = {
            method: 'POST'
        }
        return this.MyHttp
            .fetch(url, this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
        
    }
}