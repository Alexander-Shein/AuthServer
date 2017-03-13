import {ExternalProvider} from './external-provider';

export class ClientSettings{
    constructor(
        public allowRememberLogin:boolean,
        public enableLocalLogin:boolean,
        public externalProviders: ExternalProvider[])
    {}
}