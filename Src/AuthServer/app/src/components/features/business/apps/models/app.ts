import {AppCredentials} from "./app-credentials";
import {AppVm} from "./app-vm";


export class App extends AppVm {

    public appCredentials: AppCredentials = new AppCredentials();
    public redirectUrls: string[] = [];
    public isActive: boolean;
    public usersCount: number;

}