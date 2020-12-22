import { Aurelia, PLATFORM, inject } from 'aurelia-framework';
import { Redirect, Router, RouterConfiguration } from 'aurelia-router';
import { ValidationRules } from 'aurelia-validation';
import { EventAggregator } from 'aurelia-event-aggregator';
import * as $ from 'jquery';
import * as toastr from 'toastr';

@inject(EventAggregator)
export class App {
    router: Router
    IsAcessoMenu: boolean = true;
    toastSubscription: any;
    configureRouter(config: RouterConfiguration, router: Router) {
        config.title = 'Sw1';
        config.map([{
            route: ['', 'home'],
            name: 'home',
            settings: { icon: 'fa fa-home' },
            moduleId: PLATFORM.moduleName('./home/home'),
            nav: this.IsAcessoMenu,
            title: 'Home',
        }, {
            route: 'localizacao',
            name: 'localizacao',
            settings: { icon: 'fa fa-envelope' },
            moduleId: PLATFORM.moduleName('./localizacao/localizacao'),
            nav: this.IsAcessoMenu,
            title: 'Localização'
        }, {
            route: 'parceiro',
            name: 'parceiro',
            settings: { icon: 'fa fa-book' },
            moduleId: PLATFORM.moduleName('./parceiro/parceiro'),
            nav: this.IsAcessoMenu,
            title: 'Parceiro'
        }, {
            route: 'orcamento',
            name: 'orcamento',
            settings: { icon: 'fa fa-shopping-cart' },
            moduleId: PLATFORM.moduleName('./orcamento/orcamento'),
            nav: this.IsAcessoMenu,
            title: 'Orçamento'
        }, {
            route: 'ocorrencia',
            name: 'ocorrencia',
            settings: { icon: 'fa fa-calendar' },
            moduleId: PLATFORM.moduleName('./orcamento/ocorrencia'),
            nav: this.IsAcessoMenu,
            title: 'Ocorrência'
        }, {
            route: 'anexo',
            name: 'anexo',
            settings: { icon: 'fas fa-file' },
            moduleId: PLATFORM.moduleName('./orcamento/anexo'),
            nav: this.IsAcessoMenu,
            title: 'Anexo'
        }, {
            route: 'atualizarsenha',
            name: 'atualizarsenha',
            settings: { icon: 'fa fa-user' },
            moduleId: PLATFORM.moduleName('./usuario/atualizarsenha'),
            nav: this.IsAcessoMenu,
            title: 'Atualizar Senha'
        }, {
            route: 'login',
            name: 'login',
            settings: { icon: 'fa fa-user' },
            moduleId: PLATFORM.moduleName('./usuario/login'),
            nav: false,
            title: 'Login'
        }]);

        this.router = router;
    }

    attached() {
        $("#menu-toggle").click(function (e) {
            e.preventDefault();
            $("#wrapper").toggleClass("toggled");
        });
    }

    constructor(private ea : EventAggregator) {
        /*
        Pode ser utilizado em qualquer parte do sistema.
        */
        ValidationRules.customRule(
            'integerRange',
            (value, obj, min, max) => {
                var num = Number.parseInt(value);
                return num === null || num === undefined || (Number.isInteger(num) && num >= min && num <= max);
            },
            "${$displayName} não está entre ${$config.min} e ${$config.max}.",
            (min, max) => ({ min, max })
        );

        ValidationRules.customRule(
            'integerPositive',
            (value, obj) => {
                var num = Number.parseInt(value);
                return num === null || num === undefined || (Number.isInteger(num) && num > 0);
            },
            "${$displayName} precisa conter um valor positivo."
        );

        toastr.options = {
            "closeButton": true,
            "timeOut": "2000",
            "positionClass": "toast-top-center"
        }

        this.toastSubscription = this.ea.subscribe('toast', toast => {
            toastr[toast.type](toast.message);
        });

    }
}
