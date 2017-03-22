import {AppCredentials} from "./app-credentials";
import {BusinessAppVm} from "./business-app-vm";


export class BusinessApp extends BusinessAppVm {

    public appCredentials: AppCredentials = new AppCredentials();
    public redirectUrls: string[] = [];
    public isActive: boolean;
    public usersCount: number;

}