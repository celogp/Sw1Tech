import { inject } from 'aurelia-framework';
import { ValidationControllerFactory, ValidationController, ValidationRules } from 'aurelia-validation';
import { EventAggregator } from 'aurelia-event-aggregator';
import { LocalizacaoModel } from './localizacaomodel';
import { ICrud } from "../ICrud";
import { LocalizacaoService } from '../services/LocalizacaoService';
import * as $ from 'jquery';

@inject(LocalizacaoService, ValidationControllerFactory, EventAggregator)
export class Localizacao extends LocalizacaoModel implements ICrud {
    heading: string = "Localização";
    txtPesquisa: string = "";
    message: string = "";
    lstSrvErro = [];
    isVisibleForm: boolean = true;
    isVisibleGrid: boolean = false;
    isAjaxServer: boolean = false; 
    LstCpoPesquisa =  [{ "nome" : "Id"}, {"nome": "Cep"}, {"nome" :"Logradouro"}];
    CpoPesquisa = "Logradouro";

    isPesqLocalidade: boolean = false;
    isPesqBairro: boolean = false;
    isPesqCepCorreio: boolean = false;
    isPesqLocalizacao: boolean = true;
    controller: any;

    maxId: number = 5;
    maxCep: number = 8;
    maxLogradouro: number = 100;
    maxLocalidade: number = 100;

    maxUf: number = 2;
    maxBairro: number = 100;
    maxComplemento : number = 100;

    lstLocalizacao = [];
    lstUf = [];
    lstLocalidade = [];
    lstBair = [];
    lstCepCorreio = [];

    DoAtivaGrade() {
        this.txtPesquisa = "";
        if (this.isVisibleForm == true) {
            this.isVisibleGrid = true;
            this.isVisibleForm = false;
        } else {
            this.isVisibleGrid = false;
            this.isVisibleForm = true;
        }
    }

    DoMontaFiltro(){
        let filtro : any;
        let txtPesquisa = this.txtPesquisa;
        if (this.isPesqCepCorreio){
            filtro = txtPesquisa;
        }else{
            if ((this.CpoPesquisa.indexOf("Id") != -1) && (this.txtPesquisa == "")){
                txtPesquisa = "0";
            }
            filtro = {[this.CpoPesquisa] : txtPesquisa};
        }
        return filtro;
    }

    DoPesquisar() {
        let Where = this.DoMontaFiltro();
        this.isAjaxServer = true;
        if (this.isPesqLocalizacao == true) {
            return this.LocaService.DoPesquisar(Where)
                .then(response => {
                    this.lstLocalizacao = response;
                    this.isAjaxServer = false;
                })
                .catch(err => {
                    this.ae.publish('toast', {
                        type : 'error', 
                        message : err.status + ' - ' + err.statusText
                    });
                    this.isAjaxServer = false;
                });
        } else if (this.isPesqLocalidade == true) {
            return this.LocaService.DoPesquisarLocalidade(Where)
                .then(response => {
                    this.lstLocalidade = response;
                    this.isAjaxServer = false;
                })
                .catch(err => {
                    this.ae.publish('toast', {
                        type : 'error', 
                        message : err.status + ' - ' + err.statusText
                    });
                    this.isAjaxServer = false;
                });
        } else if (this.isPesqBairro == true) {
            return this.LocaService.DoPesquisarBairro(Where)
                .then(response => {
                    this.lstBair = response;
                    this.isAjaxServer = false;
                })
                .catch(err => {
                    this.ae.publish('toast', {
                        type : 'error', 
                        message : err.status + ' - ' + err.statusText
                    });
                    this.isAjaxServer = false;
                });
        }else if (this.isPesqCepCorreio == true){
            return this.LocaService.DoPesquisarCepCorreio(Where)
                .then(response => {
                    if (!("erro" in response)){
                        this.lstCepCorreio=[];
                        if (response.cep != undefined){
                            this.lstCepCorreio.push(response);
                        }if (response.length != undefined){
                            this.lstCepCorreio = response;
                        }
                    }else{
                        this.ae.publish('toast', {
                            type : 'info', 
                            message : 'Cep não localizado no correio.'
                        });
                    }
                    this.isAjaxServer = false;
                })
                .catch(err => {
                    this.ae.publish('toast', {
                        type : 'info', 
                        message : 'Cep não localizado no correio.'
                    });
                    this.isAjaxServer = false;
                });
        }
    }

    DoEditar(obj: any) {
        this.DoAtivaGrade();
        if (this.isPesqLocalizacao) {
            this.DoSetLocalizacao(obj);
        } else if (this.isPesqLocalidade) {
            this.Localidade = obj.localidade;
            this.isPesqLocalizacao = false;
            this.isPesqLocalidade = true;
            this.isPesqBairro = false;
            this.isPesqCepCorreio = false;
        } else if (this.isPesqBairro) {
            this.Bairro = obj.bairro;
            this.isPesqLocalizacao = false;
            this.isPesqLocalidade = false;
            this.isPesqCepCorreio = false;
            this.isPesqBairro = true;
        }else if (this.isPesqCepCorreio){
            this.Cep = this.LocaService.DoRemoveNonDigits(obj.cep);
            this.Logradouro = obj.logradouro;
            this.Localidade = obj.localidade;
            this.Bairro = obj.bairro;
            this.Complemento = obj.complemento;
            this.Uf = obj.uf;
            
            this.isPesqLocalizacao = false;
            this.isPesqLocalidade = false;
            this.isPesqBairro = false;
            this.isPesqCepCorreio = true;
        }
    }

    DoApagar() {
        let Loca = this.DoLoadLoca();
        this.isAjaxServer = true;
        this.lstSrvErro = [];
        return this.LocaService.DoApagar(Loca)
            .then(response => {
                if (response.validationResult.isValid == true) {
                    this.ae.publish('toast', {
                        type : 'success', 
                        message : 'Registro apagado com sucesso!'
                    });
                    this.DoLimparFormulario();
                } else {
                    this.ae.publish('toast', {
                        type : 'error', 
                        message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente.'
                    });
                    this.lstSrvErro = response.validationResult.errors;
                }
                this.isAjaxServer = false;
            })
            .catch(err => {
                this.ae.publish('toast', {
                    type : 'error', 
                    message : err.status + ' - ' + err.statusText
                });
                this.isAjaxServer = false;
            });
    }

    DoSalvar() {
        let Loca = this.DoLoadLoca();
        this.isAjaxServer = true;
        this.lstSrvErro = [];
        this.controller.validate()
        .then(result => {
            if (result.valid) {
                return this.LocaService.DoSalvar(Loca)
                    .then(response => {
                        if (response.validationResult.isValid == true) {
                            this.ae.publish('toast', {
                                type : 'success', 
                                message : 'Registro atualizado com sucesso!'
                            });
                            this.Id = response.id;
                        } else {
                            this.lstSrvErro = response.validationResult.errors;
                            this.ae.publish('toast', {
                                type : 'error', 
                                message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente.'
                            });
                        }
                        this.isAjaxServer = false;
                    })
                    .catch(err => {
                        this.ae.publish('toast', {
                            type : 'error', 
                            message : err.status + ' - ' + err.statusText
                        });
                        this.isAjaxServer = false;
                    });
            } else {
                this.isAjaxServer = false;
                this.ae.publish('toast', {
                    type : 'error', 
                    message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente.'
                });
            }
        });
    }

    DoAdicionar() {
		this.Id=0;
		this.DoSalvar();
	}    

    DoLimparFormulario() {
        this.Id = 0;
        this.Cep = "";
        this.Logradouro = "";
        this.Localidade = ""
        this.Complemento = ""
        this.Uf = "";
        this.Bairro = "";
        this.Longitude = 0;
        this.Latitude = 0;
    }

    DoLoadLoca() {
        let Loca = {
            Id: this.Id, Cep: this.Cep, Logradouro: this.Logradouro, Localidade: this.Localidade, Complemento : this.Complemento, Uf: this.Uf,
            Bairro: this.Bairro, Longitude: this.Longitude, Latitude: this.Latitude
        };
        return Loca;
    }

    DoPesquisarUf() {
        this.LocaService.DoPesquisarUf()
            .then(response => {
                this.lstUf = response;
            })
            .catch(err => {
                this.ae.publish('toast', {
                    type : 'error', 
                    message : err.status + ' - ' + err.statusText
                });
            });
    }

    DoAtivaLstLocalidade() {
        this.LstCpoPesquisa =  [{"nome": "Localidade"}];
        this.CpoPesquisa = "Localidade";
        this.isPesqLocalizacao = false;
        this.isPesqLocalidade = true;
        this.isPesqBairro = false;
        this.isPesqCepCorreio = false;
        this.DoAtivaGrade();
        if (this.Localidade != "" && this.Localidade != undefined) {
            this.txtPesquisa = this.Localidade;
            this.DoPesquisar();
        }
    }

    DoAtivaLstBairro() {
        this.LstCpoPesquisa =  [{"nome": "Bairro"}];
        this.CpoPesquisa = "Bairro";
        this.isPesqLocalizacao = false;
        this.isPesqLocalidade = false;
        this.isPesqBairro = true;
        this.isPesqCepCorreio = false;
        this.DoAtivaGrade();
        if (this.Bairro != "" && this.Bairro != undefined) {
            this.txtPesquisa = this.Bairro;
            this.DoPesquisar();
        }
    }

    DoAtivaLstLocalizacao() {
        this.LstCpoPesquisa =  [{ "nome" : "Id"}, {"nome": "Cep"}, {"nome" :"Logradouro"}];
        this.CpoPesquisa = "Logradouro";
        this.isPesqLocalizacao = true;
        this.isPesqLocalidade = false;
        this.isPesqBairro = false;
        this.isPesqCepCorreio = false;
        this.DoAtivaGrade();
        if (this.Id != 0 && this.Id != undefined) {
            this.CpoPesquisa = "Id";
            this.txtPesquisa = this.Id.toString();
            this.DoPesquisar();
        }
    }

    DoAtivaLstCepCorreio() {
        this.LstCpoPesquisa =  [{"nome": "Correio"}];
        this.CpoPesquisa = "CepCorreio";
        this.isPesqLocalizacao = false;
        this.isPesqLocalidade = false;
        this.isPesqBairro = false;
        this.isPesqCepCorreio = true;
        this.txtPesquisa = this.Cep;
        this.DoAtivaGrade();
    }

    DoSetLocalizacao(obj){
        this.Id = obj.id;
        this.Cep = obj.cep;
        this.Logradouro = obj.logradouro;
        this.Localidade = obj.localidade;
        this.Uf = obj.uf;
        this.Bairro = obj.bairro;
        this.Complemento = obj.complemento;
        this.Longitude = obj.longitude;
        this.Latitude = obj.latitude;
        this.isPesqLocalizacao = true;
        this.isPesqLocalidade = false;
        this.isPesqBairro = false;
        this.isPesqCepCorreio = false;
    }
    
    attached() {
        ValidationRules
            .ensure('Cep').required().withMessage('CEP NÃO pode ficar em branco.')
            .ensure('Logradouro').required().withMessage('Logradouro/Endereço NÃO pode ficar em branco')
            .ensure('Uf').required().withMessage('UF da Localidade/Cidade NÃO pode ficar em branco.')
            .ensure('Bairro').required().withMessage('Bairro NÃO pode ficar em branco.')
            .ensure('Localidade').required().withMessage('Localidade/Cidade NÃO pode ficar em branco.')
            .on(this);
    }

    constructor(private LocaService: LocalizacaoService, controllerFactory, private ae : EventAggregator) {
        super();
        this.controller = controllerFactory.createForCurrentScope();
        this.DoPesquisarUf();
        this.Uf = "MG";
    }

    activate() {
        $("#wrapper").toggleClass();
    }
}