import { inject } from 'aurelia-framework';
import { HttpClient, json } from 'aurelia-fetch-client';
import { CFG_API } from '../appconfig';

@inject( HttpClient )
export class ServiceBase {
    protected MyToken = null;
    protected MySetup = {};
    constructor(protected MyHttp: HttpClient) {
        this.MyHttp.configure(config => {
            config
                .useStandardConfiguration()
                .withDefaults({
                    headers: {
                        'Accept': 'application/json',
                        'content-type': 'application/json; charset=utf-8',
                    }
                });
        });
    }

    DoLerTokenLocal():string{
        this.MyToken = JSON.parse(JSON.stringify(localStorage[CFG_API.Token] || null));
        return 'Bearer ' + this.MyToken;
    }

    DoRemoveNonDigits(input) {
        let digits = '';
        for (let i = 0; i < input.length; i++) {
            let char = input.charAt(i);
            if ('0' <= char && char <= '9')
                digits += char;
        }
        return digits;
    }    
    
}    