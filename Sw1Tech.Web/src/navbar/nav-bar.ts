import { inject } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';
import { AuthService } from '../services/AuthService';

@inject(AuthService, EventAggregator)
export class NavBar {
    private auth: any;
    private nomeUsuario:string;
    private senhaUsuario:string;
    // private message:string;
    
    constructor(private authService, private ae : EventAggregator) {
        this.auth = authService;
        this.nomeUsuario = this.auth.DoObterNomeUsu();
    }

    DoAutentica(){
        // this.message = "";
        this.auth.DoAutentica(this.nomeUsuario, this.senhaUsuario).then(response => {
            if (response.status == -1){
                // this.message = response.message;
                this.ae.publish('toast', {
                    type : 'error', 
                    message : response.message
                });
            }else{
                this.ae.publish('toast', {
                    type : 'success', 
                    message : "Usuário autenticado !."
                });
            }
        });
    }
}