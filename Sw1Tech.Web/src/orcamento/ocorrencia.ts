import { inject } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';
import { ValidationControllerFactory, ValidationController, ValidationRules } from 'aurelia-validation';
import { OcorrenciaModel } from './ocorrenciamodel';
import { OrcamentoService } from '../services/OrcamentoService';
import { OcorrenciaService } from '../services/OcorrenciaService';
import { FilesService } from '../services/FilesService';
import { CFG_API } from '../appconfig';
import { ICrud } from '../ICrud';
import * as moment from "moment";
import * as $ from 'jquery';

@inject(OcorrenciaService, OrcamentoService, FilesService, ValidationControllerFactory, EventAggregator)
export class Ocorrencia extends OcorrenciaModel implements ICrud {
    heading: string = 'Orçamento Ocorrência';
    message: string = "";
    txtPesquisa: string = "";
    lstSrvErro = [];
    isVisibleForm: boolean = true;
    isVisibleGrid: boolean = false;
    isAjaxServer: boolean = false;
    LstCpoPesquisa =  [{"nome": "Id"}, {"nome": "Numero"}, {"nome": "NomeParceiro"}, {"nome" : "NomeUsuario"}];
    CpoPesquisa = "NomeParceiro";
    isPesqOrcamento: boolean = true;
    isPesqOcorrencia: boolean = false;
    isPesqUsuario: boolean = false;
    nlkServerImage: string = CFG_API.Servidor + "/";

    lstOrca = [];
    lstOcoO = [];
    lstOcor = [];
    lstUsua = [];

    Numero : number = 0;
    NomeParceiro : string = "";
    NomeOcorrencia: string = "";
    NomeUsuario: string = "";
    controller: any;
	//Imagem para Anexar
	selectedFiles;

    MaxId: number = 6;
    MaxOrcamentoId: number = 6;
    MaxOcorrenciaId: number = 6;
    MaxUsuarioId: number = 6;
    MaxHistorico: number = 100;
    
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
        if ((this.CpoPesquisa.indexOf("Id") != -1) && (this.txtPesquisa == "")){
            txtPesquisa = "0";
        }
        filtro = {[this.CpoPesquisa] : txtPesquisa};
        return filtro;
    }

    DoPesquisar() {
        let Where = this.DoMontaFiltro();
        this.isAjaxServer = true;
        if (this.isPesqOrcamento == true) {
            return this.orcaService.DoPesquisar(Where)
                .then(response => {
                    this.lstOrca = response;
                    this.isAjaxServer = false;
                })
                .catch(err => {
                    this.ae.publish('toast', {
                        type : 'error', 
                        message : err.status + ' - ' + err.statusText
                    });
                    this.isAjaxServer = false;
                });
        } else if (this.isPesqOcorrencia == true) {
            return this.ocorrenciaService.DoPesquisarOcorrencia(Where)
                .then(response => {
                    this.lstOcor = response;
                    this.isAjaxServer = false;
                })
                .catch(err => {
                    this.ae.publish('toast', {
                        type : 'error', 
                        message : err.status + ' - ' + err.statusText
                    });
                    this.isAjaxServer = false;
                });
        } else if (this.isPesqUsuario) {
            return this.ocorrenciaService.DoPesquisarUsuario(Where)
                .then(response => {
                    this.lstUsua = response;
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
    }

    DoEditar(obj: any) {
        this.DoAtivaGrade();
        if (this.isPesqOrcamento) {
            this.OrcamentoId = obj.id;
            this.Numero = obj.numero;
            this.NomeParceiro = obj.parceiro.nome;
            this.DoCarregaLstOcoO();
            this.DoLimparOcorrencia();
        }else if (this.isPesqOcorrencia) {
            this.OcorrenciaId = obj.id;
            this.NomeOcorrencia = obj.nome;
        } else if (this.isPesqUsuario) {
            this.UsuarioId = obj.id;
            this.NomeUsuario = obj.nome;
        }
    }

    DoEditarOcorrencia(obj: any) {
        this.Id = obj.id;
        this.UsuarioId = obj.usuarioId;
        this.NomeUsuario = obj.usuario.nome;
        this.OcorrenciaId = obj.ocorrenciaId;
        this.NomeOcorrencia = obj.ocorrencia.nome;
        this.DtOcorrencia = moment(obj.dtOcorrencia).format('YYYY-MM-DD');
        this.Historico = obj.historico;
        this.LinkAnexo = obj.linkAnexo;
    }

    DoApagar() {
        let Ocor = this.DoLoadOcor();
        this.isAjaxServer = true;
        this.lstSrvErro = [];
        return this.ocorrenciaService.DoApagar(Ocor)
            .then(response => {
                if (response.validationResult.isValid == true) {
                    this.filesService.DoApagarFileOrcamentoOcorrencia(Ocor.LinkAnexo.toString() );
					this.ae.publish('toast', {
						type : 'success', 
						message : 'Registro apagado com sucesso!.'
					});
                    this.DoCarregaLstOcoO();
                    this.DoLimparOcorrencia();                                              
                }else{
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
        this.isAjaxServer = true;
        this.lstSrvErro = [];
        this.controller.validate()
            .then(result => {
                if (result.valid) {
                    let formData = new FormData();
                    if ((this.selectedFiles != null) && (this.selectedFiles.length!=0)){
                        if (this.selectedFiles.length != 0) {
                            for (let i = 0; i < this.selectedFiles.length; i++) {
                                formData.append(`files[${i}]`, this.selectedFiles[i]);
                            }
                            this.LinkAnexo = this.nlkServerImage + CFG_API.imagesOcorrencia + this.selectedFiles[0].name;
                        }
                    }                                
                    let Ocor = this.DoLoadOcor();
                    if (this.selectedFiles.length != 0 ){
                        this.filesService.DoUpLoadFilesOrcamentoOcorrencia(formData);
                    }
                    return this.ocorrenciaService.DoSalvar(Ocor)
                        .then(response => {
                            if (response.validationResult.isValid == true) {
                                this.ae.publish('toast', {
                                    type : 'success', 
                                    message : 'Registro atualizado com sucesso!.'
                                });
                                this.Id = response.id;
                                this.DoCarregaLstOcoO();
                                this.DoLimparOcorrencia();
                            }else{
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
                } else {
					this.ae.publish('toast', {
						type : 'error', 
						message : 'Exitem campos inconsistentes retornado pelo servidor !, verifique e tente novamente.'
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
        this.OrcamentoId = 0;
        this.Numero = 0;
        this.NomeParceiro = "";
        this.Id = 0;
        this.UsuarioId = 0;
        this.NomeUsuario = "";
        this.OcorrenciaId = 0;
        this.NomeOcorrencia = "";
        this.DtOcorrencia = "";
        this.Historico = "";
        this.LinkAnexo = "";
        this.lstOcoO = [];
    }

    DoLimparOcorrencia(){
        this.Id = 0;
        this.UsuarioId = 0;
        this.NomeUsuario = "";
        this.OcorrenciaId = 0;
        this.NomeOcorrencia = "";
        this.DtOcorrencia = "";
        this.Historico = "";
        this.LinkAnexo = "";
        this.selectedFiles = "";
    }

    DoAtivaLstOrcamento() {
        this.LstCpoPesquisa =  [{"nome": "Id"}, {"nome": "Numero"}, {"nome": "NomeParceiro"}, {"nome" : "NomeUsuario"}];
        this.CpoPesquisa = "NomeParceiro";
        this.DoAtivaGrade();
        this.isPesqOrcamento = true;
        this.isPesqOcorrencia = false;
        this.isPesqUsuario = false;
        if (this.Id != 0 && this.Id != undefined) {
            this.CpoPesquisa = "Id";
            this.txtPesquisa = this.Id.toString();
            this.DoPesquisar();
        }
    }
    
    DoAtivaLstOcorrencia() {
        this.LstCpoPesquisa =  [{"nome": "Id"}, {"nome": "Nome"}];
        this.CpoPesquisa = "Nome";
        this.isPesqOrcamento = false;
        this.isPesqOcorrencia = true;
        this.isPesqUsuario = false;
        this.DoAtivaGrade();
        if (this.OcorrenciaId != 0 && this.OcorrenciaId != undefined) {
            this.CpoPesquisa = "Id";
            this.txtPesquisa = this.OcorrenciaId.toString();
            this.DoPesquisar();
        }
    }
        
    DoAtivaLstUsuario() {
        this.LstCpoPesquisa =  [{"nome": "Id"}, {"nome": "Nome"}];
        this.CpoPesquisa = "Nome";
        this.isPesqOrcamento = false;
        this.isPesqOcorrencia = false;
        this.isPesqUsuario = true;
        this.DoAtivaGrade();
        if (this.UsuarioId != 0 && this.UsuarioId != undefined) {
            this.CpoPesquisa = "Id";
            this.txtPesquisa = this.UsuarioId.toString();
            this.DoPesquisar();
        }
    }

    DoLoadOcor(){
        let Ocor = {
            Id: this.Id, OrcamentoId: this.OrcamentoId, OcorrenciaId: this.OcorrenciaId, 
            UsuarioId: this.UsuarioId, DtOcorrencia: this.DtOcorrencia, Historico : this.Historico, LinkAnexo: this.LinkAnexo
        };
        return Ocor;
    }

    DoCarregaLstOcoO(){
        this.isAjaxServer = true;
        this.ocorrenciaService.DoPesquisar({OrcamentoId: this.OrcamentoId}).then( response => {
            this.isAjaxServer = false;
            this.lstOcoO = response;
        }).catch(err => {
            this.isAjaxServer = false;
            this.ae.publish('toast', {
                type : 'error', 
                message : err.status + ' - ' + err.statusText
            });
        });
    }

	DoCarregarAnexos() {
		this.Id = 0;
        this.selectedFiles = "";
        this.DoCarregaLstOcoO();
	}

    attached() {
        ValidationRules
            .ensure((o: this) => o.OcorrenciaId).required().withMessage('Id da ocorrencia é obrigatoria !.').satisfiesRule('integerPositive')
            .ensure((o: this) => o.UsuarioId).required().withMessage('Id do usuário é obrigatorio !.').satisfiesRule('integerPositive')
            .ensure('DtOcorrencia').required().withMessage('Data da ocorrencia é obrigatoria !.')
            .on(this);
    }
    
    constructor(private ocorrenciaService: OcorrenciaService,
                private orcaService: OrcamentoService,
                private filesService: FilesService,
                controllerFactory, 
                private ae : EventAggregator) {
        super();
        this.controller = controllerFactory.createForCurrentScope();
        if (CFG_API.Servidor == "//localhost"){
            this.nlkServerImage = "";
        }
    }

    activate() {
        $("#wrapper").toggleClass();
    }
    
}