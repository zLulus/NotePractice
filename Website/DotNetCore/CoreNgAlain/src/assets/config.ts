import {environment} from '@env/environment';

export class Config {
    static apiHost= !environment.production ? 'http://localhost:49849' : 'your product environment api';
}

const config = new Config();
(<any>window).config = config;
export default config;