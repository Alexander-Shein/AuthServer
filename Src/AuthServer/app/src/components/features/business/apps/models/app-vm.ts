import {ExternalProvider} from "../../../auth/external-log-in/models/external-provider";


export class AppVm {

    public name: string;
    public key: string;
    public isLocalAccountEnabled: boolean;
    public allowRememberLogIn: boolean;
    public externalProviders: ExternalProvider[] = [];

}