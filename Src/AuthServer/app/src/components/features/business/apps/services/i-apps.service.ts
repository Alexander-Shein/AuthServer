import {App} from "../models/app";
import {AppVm} from "../models/app-vm";


export interface IAppsService {

    getAll(): Promise<App[]>;
    put(app: App): Promise<App>;
    get(name: string): Promise<App>;
    remove(name: string): Promise<void>;
    getByUrl(url: string): Promise<AppVm>;

}