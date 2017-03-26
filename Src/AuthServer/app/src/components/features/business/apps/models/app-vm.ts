import {ExternalProvider} from "../../../auth/external-log-in/models/external-provider";
import {LocalAccountSettings} from "./local-account-settings";


export class AppVm {

    public name: string;
    public key: string;
    public isLocalAccountEnabled: boolean;
    public externalProviders: ExternalProvider[] = [];
    public localAccountSettings: LocalAccountSettings;

}