import {AppVm} from "../models/app-vm";
import {Observable} from "rxjs";
import {ExtendedAppVm} from "../models/extended-app-vm";
import {ExtendedAppIm} from "../models/extended-app-im";


export interface IAppsService {

    getAll(): Observable<ExtendedAppVm[]>;
    put(id: string, app: ExtendedAppIm): Observable<ExtendedAppVm>;
    post(app: ExtendedAppIm): Observable<ExtendedAppVm>;
    get(id: string): Observable<ExtendedAppVm>;
    remove(id: string): Observable<void>;
    getByUrl(url: string): Observable<AppVm>;
    isAppExist(key: string): Observable<void>;

}