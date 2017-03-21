import {AppCredentials} from "./app-credentials";
import {ExternalProvider} from "../../../auth/external-log-in/models/external-provider";


export class BusinessApp {

    public name: string;
    public isLocalAccountEnabled: boolean;
    public externalProviders: ExternalProvider[] = [];
    public appCredentials: AppCredentials = new AppCredentials();
    public redirectUrls: string[] = [];
    public isActive: boolean;
    public usersCount: number;

}