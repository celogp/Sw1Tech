import { Aurelia, inject } from 'aurelia-framework';
import { HttpClient, json } from 'aurelia-fetch-client';
import { CFG_API, CFG_URLAPI, CFG_USUARIO } from '../appconfig';

@inject(Aurelia, HttpClient)
export class AuthService {
    private MyToken = null;
    private MySetup = {};
    constructor(private app: Aurelia, private myHttp: HttpClient) {
        this.MyToken = JSON.parse(JSON.stringify(localStorage[CFG_API.Token] || null));
        this.myHttp.configure(config => {
            config
                .useStandardConfiguration()
                .withDefaults({
                    headers: {
                        'Accept': 'application/json',
                        'content-type': 'application/json; charset=utf-8'
                    }
                })
        });
    }

    DoLogin(nome, senha) {
        let User = { Nome: nome, Senha: senha };
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(User)
        }
        return this.myHttp
            .fetch(CFG_URLAPI.UsuarioUrl + '/DoLogin', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                if (json.status == 1) {
                    localStorage[CFG_API.Token] = json.token;
                    localStorage[CFG_USUARIO.Id] = json.result.id;
                    localStorage[CFG_USUARIO.Nome] = json.result.nome;
                    this.MyToken = json.token;
                    this.app.setRoot('app');
                }
                return json;
            });
    }

    DoLogout() {
        localStorage.removeItem(CFG_API.Token);
        localStorage.removeItem(CFG_USUARIO.Id);
        localStorage.removeItem(CFG_USUARIO.Nome);
        this.MyToken = null;
        location.hash = '#/';
        this.app.setRoot('usuario/login');
    }
    
    DoObterNomeUsu(){
        return localStorage[CFG_USUARIO.Nome];
    }

    DoAuthenticado() {
        return this.MyToken !== null;
    }

    DoPermissao(permission) {
        return true; // why not?
    }

    DoAtualizarSenha(senha, senhaConfirmada) {
        let id = localStorage[CFG_USUARIO.Id];
        let nome = localStorage[CFG_USUARIO.Nome];
        let tkusr = localStorage[CFG_API.Token];
        let user = { Id: id, Nome: nome, Senha: senha, SenhaConfirmada: senhaConfirmada };

        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(user),
            headers: {
                'Authorization': 'Bearer ' + tkusr
            }
        }
        return this.myHttp
            .fetch(CFG_URLAPI.UsuarioUrl + '/DoSalvar', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                return json;
            });
    }

    DoAutentica(nome, senha) {
        let User = { Nome: nome, Senha: senha };
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(User)
        }
        return this.myHttp
            .fetch(CFG_URLAPI.UsuarioUrl + '/DoLogin', this.MySetup)
            .then(function (response) {
                return response.json();
            })
            .then(json => {
                if (json.status == 1) {
                    localStorage[CFG_API.Token] = json.token;
                    localStorage[CFG_USUARIO.Id] = json.result.id;
                    localStorage[CFG_USUARIO.Nome] = json.result.nome;
                    this.MyToken = json.token;
                }
                return json;
            });
    }
    
}