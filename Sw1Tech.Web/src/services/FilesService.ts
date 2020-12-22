import { CFG_URLAPI, CFG_API } from '../appconfig';
import { HttpClient } from 'aurelia-fetch-client';
import { inject } from 'aurelia-framework';

export class FilesService {
    private FilesUrl: string = CFG_URLAPI.FilesUrl;
    private Myhttp: HttpClient;
    private MyToken = null;
    private MySetup = {};
    
    constructor() {
        this.Myhttp = new HttpClient();
        this.Myhttp.configure(config => {
            config
                .useStandardConfiguration()
                .withDefaults({
                    headers: {
                        'Accept': 'application/json'
                    }
                })
        });
    }
    
    DoLerTokenLocal():string{
        this.MyToken = JSON.parse(JSON.stringify(localStorage[CFG_API.Token] || null));
        return 'Bearer ' + this.MyToken;
    }

    DoUpLoadFilesOrcamento(files) {
        this.MySetup = {
            method: 'POST',
            body: files,
            headers: {
                'Accept': 'application/json',
                'Authorization': this.DoLerTokenLocal()
            }            
        };
        return this.Myhttp
            .fetch(this.FilesUrl + '/DoUploadFilesOrcamento', this.MySetup)
            .then(function (response) {
                return response;
            })
            .then(response => {
                return response;
            });
    }

    DoApagarFileOrcamento(file:string) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(file),
            headers: {
                'Accept': 'application/json',
                'content-type': 'application/json; charset=utf-8',
                'Authorization': this.DoLerTokenLocal()
            }            
        };
        return this.Myhttp
            .fetch(this.FilesUrl + '/DoApagarFilesOrcamento', this.MySetup)
            .then(function (response) {
                return response;
            })
            .then(response => {
                return response;
            });
    }

    DoUpLoadFilesOrcamentoOcorrencia(files) {
        this.MySetup = {
            method: 'POST',
            body: files,
            headers: {
                'Accept': 'application/json',
                'Authorization': this.DoLerTokenLocal()
            }            
        };
        return this.Myhttp
            .fetch(this.FilesUrl + '/DoUploadFilesOrcamentoOcorrencia', this.MySetup)
            .then(function (response) {
                return response;
            })
            .then(response => {
                return response;
            });
    }

    DoApagarFileOrcamentoOcorrencia(file:string) {
        this.MySetup = {
            method: 'POST',
            body: JSON.stringify(file),
            headers: {
                'Accept': 'application/json',
                'content-type': 'application/json; charset=utf-8',
                'Authorization': this.DoLerTokenLocal()
            }            
        };
        return this.Myhttp
            .fetch(this.FilesUrl + '/DoApagarFilesOrcamentoOcorrencia', this.MySetup)
            .then(function (response) {
                return response;
            })
            .then(response => {
                return response;
            });
    }
    
    
}    