import {ExternalProvider} from "../../../auth/external-log-in/models/external-provider";


export class BusinessAppVm {

    public name: string;
    public isLocalAccountEnabled: boolean;
    public allowRememberLogIn: boolean;
    public externalProviders: ExternalProvider[] = [];

}