import { inject } from 'aurelia-framework';
import { ValidationControllerFactory, ValidationController, ValidationRules } from 'aurelia-validation';
import { UsuarioModel } from './usuariomodel';
import { AuthService } from '../services/AuthService';
import * as toastr from 'toastr';

@inject(AuthService, ValidationControllerFactory)
export class Login extends UsuarioModel {
    maxId: number = 4;
    maxNome: number = 60;
    maxSenha: number = 20;
    maxSenhaNova: number = 20;
    maxSenhaConfirmada: number = 20;
    
    private Auth: any;
    private controller :any;

    constructor(private authService, controllerFactory) {
        super();
        this.Auth = authService;
        this.controller = controllerFactory.createForCurrentScope();
    }

    activate() {
        toastr.options = {
            "closeButton": true,
            "timeOut": "2000",
            "positionClass": "toast-top-center"
        }
    }

    DoLogin() {
        this.controller.validate()
            .then(result => {
                if (result.valid) {
                    return this.Auth.DoLogin(this.Nome, this.Senha)
                        .then(response => {
                            if (response.status == -1){
                                toastr.error(response.message + '!.');
                            }else{
                                toastr.success(response.message + '!.');
                            }
                        })
                        .catch(err => {
                            toastr.error(err.status + ' - ' + err.statusText);
                        });
                }
        });
    }

    attached() {
        ValidationRules
            .ensure('Senha').required().withMessage('Senha é obrigatoria !.')
            .ensure('Nome').required().withMessage('Nome é obrigatorio !.')
            .on(this);
    }
}