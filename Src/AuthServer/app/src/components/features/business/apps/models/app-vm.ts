import {ExternalProvider} from "../../../auth/external-log-in/models/external-provider";
import {LocalAccountSettings} from "./local-account-settings";


export class AppVm {

    public id: string;
    public name: string;
    public key: string;
    public isLocalAccountEnabled: boolean;
    public isRememberLogInEnabled: boolean;
    public isSecurityQuestionsEnabled: boolean;
    public emailSettings: LocalAccountSettings;
    public phoneSettings: LocalAccountSettings;
    public externalProviders: ExternalProvider[] = [];

}