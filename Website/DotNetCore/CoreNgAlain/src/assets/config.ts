import {environment} from '@env/environment';

class Config {
    public isDebug: boolean;
    public apiHost: string;

    constructor() {
        this.isDebug = !environment.production;
        this.apiHost = this.isDebug ? 'http://localhost:61541' : 'your product environment api';
    }
}

const config = new Config();
(<any>window).config = config;
export default config;