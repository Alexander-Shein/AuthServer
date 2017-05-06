import {App} from "../models/app";
import {AppVm} from "../models/app-vm";
import {Observable} from "rxjs";


export interface IAppsService {

    getAll(): Observable<App[]>;
    put(app: App): Observable<App>;
    post(app: App): Observable<App>;
    get(name: string): Promise<App>;
    remove(name: string): Promise<void>;
    getByUrl(url: string): Observable<AppVm>;

}