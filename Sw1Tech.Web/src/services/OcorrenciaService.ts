import {  CFG_URLAPI } from '../appconfig';
import { ServiceBase } from "./ServiceBase";

export class OcorrenciaService extends ServiceBase {
    private OrcamentoOcorrenciaUrl: string = CFG_URLAPI.OrcamentoOcorrenciaUrl;
    private PesquisaTabelaUrl: string = CFG_URLAPI.PesquisaTabelaUrl;
    private UsuarioUrl: string = CFG_URLAPI.UsuarioUrl;
    
    DoSalvar(orcamentoOcorrencia) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(orcamentoOcorrencia),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.OrcamentoOcorrenciaUrl + '/DoSalvar', this.MySetup)
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
            .fetch(this.OrcamentoOcorrenciaUrl + '/DoPesquisar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoApagar(orcamentoOcorrencia) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(orcamentoOcorrencia),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.OrcamentoOcorrenciaUrl + '/DoApagar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoPesquisarOcorrencia(where) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(where),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.PesquisaTabelaUrl + '/DoPesquisarOcorrencia', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoPesquisarUsuario(where) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(where),
            headers: {
                'Authorization': this.DoLerTokenLocal()
            }
        }
        return this.MyHttp
            .fetch(this.UsuarioUrl + '/DoPesquisar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }
}