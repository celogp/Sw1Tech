import { inject } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';
import { ValidationControllerFactory, ValidationController, ValidationRules } from 'aurelia-validation';
import { UsuarioModel } from './usuariomodel';
import { AuthService } from '../services/AuthService';
import * as $ from 'jquery';

@inject(AuthService, ValidationControllerFactory, EventAggregator)
export class AtualizarSenha extends UsuarioModel {
    maxId: number = 4;
    maxNome: number = 60;
    maxSenha: number = 20;
    maxSenhaNova: number = 20;
    maxSenhaConfirmada: number = 20;
    
    // private message: string = "";
    private auth: any;
    private controller : any;
    private ae : any;
    constructor(authService, controllerFactory, EventAggregator) {
        super();
        this.auth = authService;
        this.controller = controllerFactory.createForCurrentScope();
        this.ae = EventAggregator;
    }

    activate() {
        // this.message = '';
        $("#wrapper").toggleClass();
    }

    DoAtualizarSenha() {
        // this.message = '';
        if (this.SenhaConfirmada != this.SenhaNova) {
            // this.message = "Atenção ! a NOVA senha precisa ser igual a senha CONFIRMADA";
            this.ae.publish('toast', {
                type : 'error', 
                message : 'Atenção ! a NOVA senha precisa ser igual a senha CONFIRMADA !.'
            });
            return
        }
        if (this.Senha == this.SenhaNova) {
            // this.message = "Atenção ! a NOVA senha precisa ser diferente da anterior";
            this.ae.publish('toast', {
                type : 'error', 
                message : 'Atenção ! a NOVA senha precisa ser diferente da anterior !.'
            });
            return
        }

        this.controller.validate()
            .then(result => {
                if (result.valid) {
                    return this.auth.DoAtualizarSenha(this.Senha, this.SenhaConfirmada)
                        .then(response => {
                            if (response.validationResult.isValid == true) {
                                // this.message = 'Registro atualizado com sucesso!';
                                this.ae.publish('toast', {
                                    type : 'success', 
                                    message : 'Registro atualizado com sucesso !.'
                                });
                            } else {
                                // this.message = response.validationResult.errors;
                                this.ae.publish('toast', {
                                    type : 'error', 
                                    message : response.validationResult.errors
                                });
                            }
                        })
                        .catch(err => {
                            // this.message = err.status +' - '+ err.statusText;
                            this.ae.publish('toast', {
                                type : 'error', 
                                message : err.status +' - '+ err.statusText
                            });
                        });
                }
            });
    }

    attached() {
        ValidationRules
            .ensure('Senha').required().withMessage('Senha atual é obrigatoria !.')
            .ensure('SenhaNova').required().withMessage('Nova senha é obrigatória!.')
            .ensure('SenhaConfirmada').required().withMessage('Confirmar senha é obrigatório!.')
            .on(this);
    }
}