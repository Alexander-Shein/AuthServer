import {ExternalProvider} from "../../auth/external-log-in/models/external-provider";


export class ClientSettings {

    public allowRememberLogIn: boolean;
    public enableLocalLogIn: boolean;
    public externalProviders: ExternalProvider[];

}