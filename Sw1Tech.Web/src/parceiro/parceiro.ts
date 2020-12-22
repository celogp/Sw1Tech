import { inject } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';
import { ValidationControllerFactory, ValidationController, ValidationRules } from 'aurelia-validation';
import { ParceiroModel } from './parceiromodel';
import { ICrud } from "../ICrud";
import { ParceiroService } from '../services/ParceiroService';
import { LocalizacaoService } from '../services/LocalizacaoService';
import * as $ from 'jquery';

@inject(ParceiroService, LocalizacaoService, ValidationControllerFactory, EventAggregator)
export class Parceiro extends ParceiroModel implements ICrud {
    heading: string = "Parceiros";
    message: string = "";
    lstSrvErro = [];
    txtPesquisa: string = "";
    isVisibleForm: boolean = true;
    isVisibleGrid: boolean = false;
    isAjaxServer: boolean = false;

    LstCpoPesquisa =  [{"nome": "Id"}, {"nome": "Nome"}, {"nome": "Razao"}, {"nome": "Cnpj"}, {"nome": "Cpf"}, {"nome": "Email"}, {"nome": "Fone"}, {"nome": "Celular"}, {"nome": "Contato"}, {"nome": "Logradouro"} ];
    CpoPesquisa = "Nome";

    maxId: number = 6;
    maxRazao: number = 100;
    maxNome: number = 100;
    maxLocalizacaoId: number = 4;
    maxCnpj: number = 15;
    maxInscricao: number = 14;
    maxCpf: number = 11;
    maxIdentidade: number = 16;
    maxEmail: number = 100;
    maxFone: number = 15;
    maxCelular: number = 15;
    maxContato: number = 100;
    maxFoneContato: number = 15;
    maxCelularContato: number = 15;
    
    isPesqParceiro: boolean = true;
    isPesqLocalizacao: boolean = false;
    isPesqCepCorreio: boolean = false;

    lstSexo = [];
    lstLocalizacao = [];
    lstCepCorreio = [];
    lstParc = [];
    controller: any;

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
        if (this.isPesqParceiro == true) {
            return this.parcService.DoPesquisar(Where)
                .then(response => {
                    this.lstParc = response;
                    this.isAjaxServer = false;
                })
                .catch(err => {
                    this.ae.publish('toast', {
                        type : 'error', 
                        message : err.status + ' - ' + err.statusText
                    });
                    this.isAjaxServer = false;
                });
        } else if (this.isPesqLocalizacao == true) {
            return this.locaService.DoPesquisar(Where)
                .then(response => {
                    // if (Id != 0){
                    //     this.DoSetLocalizacao(response[0]);
                    // }
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
        }else if (this.isPesqCepCorreio == true) {
            return this.locaService.DoPesquisarCepCorreio(Where)
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
                        message : 'Cep não localizado no correio !.'
                    });
                }
                this.isAjaxServer = false;
            })
            .catch(err => {
                this.ae.publish('toast', {
                    type : 'info', 
                    message : 'Cep não localizado no correio !.'
                });
                this.isAjaxServer = false;
            });            
        }
    }

    DoEditar(obj) {
        this.DoAtivaGrade();
        if (this.isPesqParceiro) {
            this.DoSetParc(obj);
        } else if (this.isPesqLocalizacao) {
            this.DoSetLocalizacao(obj);
        } else if (this.isPesqCepCorreio) {
            this.DoSetCepCorreio(obj);
        }
    }

    DoApagar() {
        let Parc = this.DoLoadParc();
        this.isAjaxServer = true;
        this.lstSrvErro = [];
        return this.parcService.DoApagar(Parc)
            .then(response => {
                if (response.validationResult.isValid == true) {
                    this.ae.publish('toast', {
                        type : 'success', 
                        message : 'Registro apagado com sucesso !.'
                    });
                    this.DoLimparFormulario();
                }else{
                    this.ae.publish('toast', {
                        type : 'error', 
                        message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente !.'
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
        let Parc = this.DoLoadParc();
        this.isAjaxServer = true;
        this.lstSrvErro = [];
        this.controller.validate()
            .then(result => {
                if (result.valid) {
                    return this.parcService.DoSalvar(Parc)
                        .then(response => {
                            if (response.validationResult.isValid == true) {
                                this.ae.publish('toast', {
                                    type : 'success', 
                                    message : 'Registro atualizado com sucesso !.'
                                });
                                this.Id = response.id;
                            }else{
                                this.ae.publish('toast', {
                                    type : 'error', 
                                    message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente !.'
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
                } else {
                    this.ae.publish('toast', {
                        type : 'error', 
                        message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente !.'
                    });
                    this.isAjaxServer = false;
                }
            });
    }

    DoAdicionar() {
		this.Id=0;
		this.DoSalvar();
	}    
    
    DoLimparFormulario() {
        this.Id = 0;
        this.Razao = "";
        this.Nome = "";
        this.LocalizacaoId = 0;
        this.Logradouro = "";
        this.Cnpj = "";
        this.Inscricao = "";
        this.Cpf = "";
        this.Identidade = "";
        this.Email = "";
        this.Sexo = 1;
        this.Fone = "";
        this.Celular = "";
        this.Contato = "";
        this.FoneContato = "";
        this.CelularContato = "";
        this.CelularContatoIsWhatsApp = false;
        this.CelularIsWhatsApp = false;
    }

    DoAtivaLstParceiro() {
        this.LstCpoPesquisa =  [{"nome": "Id"}, {"nome": "Nome"}, {"nome": "Razao"}, {"nome": "Cnpj"}, {"nome": "Cpf"}, {"nome": "Email"}, {"nome": "Fone"}, {"nome": "Celular"}, {"nome": "Contato"}, {"nome": "Logradouro"} ];
        this.CpoPesquisa = "Nome";
        this.isPesqParceiro = true;
        this.isPesqLocalizacao = false;
        this.isPesqCepCorreio = false;
        this.DoAtivaGrade();
        if (this.Id != 0 && this.Id != undefined) {
            this.CpoPesquisa = "Id";
            this.txtPesquisa = this.Id.toString();
            this.DoPesquisar();
        }
    }

    DoAtivaLstLocalizacao() {
        this.LstCpoPesquisa =  [{ "nome" : "Id"}, {"nome": "Cep"}, {"nome" :"Logradouro"}];
        this.CpoPesquisa = "Logradouro";
        this.isPesqParceiro = false;
        this.isPesqLocalizacao = true;
        this.isPesqCepCorreio = false;
        this.DoAtivaGrade();
        if (this.LocalizacaoId != 0 && this.LocalizacaoId != undefined) {
            this.CpoPesquisa = "Id";
            this.txtPesquisa = this.LocalizacaoId.toString();
            this.DoPesquisar();
        }
    }

    DoAtivaLstCepCorreio() {
        this.LstCpoPesquisa =  [{"nome": "Correio"}];
        this.CpoPesquisa = "Correio";
        this.isPesqParceiro = false;
        this.isPesqLocalizacao = false;
        this.isPesqCepCorreio = true;
        this.DoAtivaGrade();
    }

    DoPesquisarSexo() {
    this.parcService.DoPesquisarSexo()
        .then(response => {
            this.lstSexo = response;
        })
        .catch(err => {
            this.ae.publish('toast', {
                type : 'error', 
                message : err.status + ' - ' + err.statusText
            });
        });
    }

    DoLoadParc() {
        let Parc = {
            Id: this.Id, Nome: this.Nome, Razao: this.Razao, 
            LocalizacaoId: this.LocalizacaoId, Cnpj: this.Cnpj,
            Inscricao: this.Inscricao, Cpf: this.Cpf,
            Identidade: this.Identidade, Email: this.Email,
            Sexo: this.Sexo, Fone: this.Fone, Celular: this.Celular,
            Contato: this.Contato, FoneContato: this.FoneContato,
            CelularContato: this.CelularContato, 
            CelularContatoIsWhatsApp : (this.CelularContatoIsWhatsApp==true ? "S" : "N" ),
            CelularIsWhatsApp: (this.CelularIsWhatsApp==true ? "S" : "N" )
        };
        return Parc;
    }

    DoSetParc(obj){
        this.Id = obj.id;
        this.Razao = obj.razao;
        this.Nome = obj.nome;
        this.LocalizacaoId = obj.localizacaoId;
        this.Logradouro = obj.localizacao.logradouro + ', ' + obj.localizacao.localidade + ', ' + obj.localizacao.bairro + ', CEP : ' + obj.localizacao.cep;
        this.Cnpj = obj.cnpj;
        this.Inscricao = obj.inscricao;
        this.Cpf = obj.cpf;
        this.Identidade = obj.identidade;
        this.Email = obj.email;
        this.Sexo = obj.sexo;
        this.Fone = obj.fone;
        this.Celular = obj.celular;
        this.Contato = obj.contato;
        this.FoneContato = obj.foneContato;
        this.CelularContato = obj.celularContato;
        this.CelularContatoIsWhatsApp = (obj.celularContatoIsWhatsApp=="S");
        this.CelularIsWhatsApp = (obj.celularIsWhatsApp=="S");
        this.isPesqParceiro = true;
        this.isPesqLocalizacao = false;
    }

    DoSetLocalizacao(obj){
        this.LocalizacaoId = obj.id;
        this.Logradouro = obj.logradouro + ', ' + obj.localidade + ', ' + obj.bairro + ', CEP : ' + obj.cep;
        this.isPesqParceiro = false;
        this.isPesqLocalizacao = true;
    }

    DoSetCepCorreio(obj){
        let Loca = {
            Id: 0, Cep: this.locaService.DoRemoveNonDigits(obj.cep) , Logradouro: obj.logradouro, Localidade: obj.localidade, Complemento : obj.complemento, Uf: obj.uf,
            Bairro: obj.bairro, Longitude: 0, Latitude: 0
        };
        this.lstSrvErro = [];
        this.isAjaxServer = true;
        this.locaService.DoSalvar(Loca)
        .then(response => {
            if (response.validationResult.isValid == true) {
                this.ae.publish('toast', {
                    type : 'success', 
                    message : 'Cep incluído com sucesso !.'
                });
                obj.id = response.id;
                this.DoSetLocalizacao(obj);
            }else{
                this.ae.publish('toast', {
                    type : 'error', 
                    message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente !.'
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
        this.isPesqCepCorreio = true;
    }
    
    attached() {
        ValidationRules
            .ensure('Razao').required().withMessage('Razão social é obrigatoria !.')
            .ensure('Nome').required().withMessage('Nome é obrigatorio !.')
            .ensure('Email').email().withMessage('Email inválido!.')
            .ensure((p: this) => p.LocalizacaoId).required().withMessage('Localizacao Obrigatória.').satisfiesRule('integerPositive')
            .ensure((p: this) => p.Id).required().withMessage('Id Obrigatória.').satisfiesRule('integerPositive')
        .on(this);
    }

    constructor(private parcService: ParceiroService, 
                private locaService: LocalizacaoService, 
                controllerFactory, 
                private ae : EventAggregator) {
        super();
        this.controller = controllerFactory.createForCurrentScope();
        this.Sexo = 1;
        this.DoPesquisarSexo();
    }

    activate() {
        $("#wrapper").toggleClass();
    }
}