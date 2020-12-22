import { inject } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';
import { AnexoModel } from './anexomodel';
import { AnexoService } from '../services/AnexoService';
import { OrcamentoService } from '../services/OrcamentoService';
import { FilesService } from '../services/FilesService';
import { ICrud } from '../ICrud';
import { CFG_API } from '../appconfig';
import * as $ from 'jquery';

@inject(AnexoService, OrcamentoService, FilesService, EventAggregator)
export class Anexo extends AnexoModel implements ICrud {
	heading: string = "Orçamento Anexo";
	txtPesquisa: string = "";
	message: string = "";
	lstSrvErro = [];
	isVisibleForm: boolean = true;
	isVisibleGrid: boolean = false;
	isAjaxServer: boolean = false;
    LstCpoPesquisa =  [{"nome": "Id"}, {"nome": "Numero"}, {"nome": "NomeParceiro"}, {"nome" : "NomeUsuario"}];
    CpoPesquisa = "NomeParceiro";
    nlkServerImage: string = CFG_API.Servidor + "/";
	
	//Orcamento
	isPesqOrcamento: boolean = false;
	lstOrca = [];
	lstAnex = [];
	Numero: number = 0;
	NomeParceiro: string = '';
	LinkAnexo: string = '';
	//Imagem para Anexar
	selectedFiles;
	
    MaxId: number = 6;
    MaxOrcamentoId: number = 6;
    MaxNome: number = 100;
	

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
			
	DoPesquisarAnexo(){
		let Where: any;
		if (this.OrcamentoId == 0){
			return;
		}
		Where = { OrcamentoId: this.OrcamentoId };
		this.isAjaxServer = true;
		return this.anexoService.DoPesquisar(Where)
			.then(response => {
				this.lstAnex = response;
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

	DoPesquisar() {
		let Where = this.DoMontaFiltro();
		this.isAjaxServer = true;
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
	}

	DoEditarAnexo(obj:any){
		this.Id = obj.id;
		this.Nome = obj.nome;
		this.LinkAnexo = obj.linkAnexo;
	}

	DoEditar(obj: any) {
		this.DoAtivaGrade();
		this.OrcamentoId = obj.id;
		this.Numero = obj.numero;
		this.NomeParceiro = obj.parceiro.nome;
		this.selectedFiles = "";
		this.DoPesquisarAnexo();
	}

    DoApagar() {
		let OrcaAnexo = this.DoLoadOrcaAnexo();
        this.isAjaxServer = true;
		this.lstSrvErro = [];
        return this.anexoService.DoApagar(OrcaAnexo)
            .then(response => {
                if (response.validationResult.isValid == true) {
                    this.filesService.DoApagarFileOrcamento(OrcaAnexo.LinkAnexo.toString() );
					this.ae.publish('toast', {
						type : 'success', 
						message : 'Registro apagado com sucesso!.'
					});
					this.DoCarregarAnexos();
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
		let formData = new FormData();
		if ((this.selectedFiles != null) && (this.selectedFiles.length!=0)){
			if (this.selectedFiles.length != 0) {
				for (let i = 0; i < this.selectedFiles.length; i++) {
					formData.append(`files[${i}]`, this.selectedFiles[i]);
				}
				this.LinkAnexo = this.nlkServerImage + CFG_API.imagesOrcamento + this.selectedFiles[0].name;
				this.filesService.DoUpLoadFilesOrcamento(formData).then(response => {
					if (response) {
						this.DoGravar();
					};
				});
			}
		} else {
			this.DoGravar();
		}
	}

	DoAdicionar() {
		this.Id=0;
		this.DoSalvar();
	}

	DoGravar(){
		let OrcaAnexo = this.DoLoadOrcaAnexo();
		this.isAjaxServer = true;
		this.anexoService.DoSalvar(OrcaAnexo)
			.then(response => {
				if (response.validationResult.isValid == true) {
					this.Id = response.id;
					this.DoCarregarAnexos();
					this.ae.publish('toast', {
						type : 'success', 
						message : 'Registro atualizado com sucesso!.'
					});
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

	DoLimparFormulario() {
		this.Id = 0;
		this.OrcamentoId = 0;
		this.Numero = 0;
		this.NomeParceiro = "";
		this.Nome = "";
		this.LinkAnexo = "";
		this.lstAnex = [];
	}

	DoCarregarAnexos() {
		this.Id = 0;
		this.Nome = "";
		this.selectedFiles = "";
		this.DoPesquisarAnexo();
	}
	
	DoAtivaLstOrcamento() {
        this.LstCpoPesquisa =  [{"nome": "Id"}, {"nome": "Numero"}, {"nome": "NomeParceiro"}, {"nome" : "NomeUsuario"}];
        this.CpoPesquisa = "NomeParceiro";
        this.DoAtivaGrade();
		this.isPesqOrcamento = true;
        if (this.OrcamentoId != 0 && this.OrcamentoId != undefined) {
            this.CpoPesquisa = "Id";
            this.txtPesquisa = this.OrcamentoId.toString();
            this.DoPesquisar();
        }
	}

	DoLoadOrcaAnexo() {
		let orcaAnexo = { Id: this.Id, OrcamentoId: this.OrcamentoId, Nome: this.Nome, LinkAnexo: this.LinkAnexo }
		return orcaAnexo;
	}

	constructor(private anexoService: AnexoService,
				private orcaService: OrcamentoService,
				private filesService: FilesService, 
				private ae : EventAggregator) {
		super();
        if (CFG_API.Servidor == "//localhost"){
            this.nlkServerImage = "";
        }
	}

    activate() {
        $("#wrapper").toggleClass();
    }
	
}