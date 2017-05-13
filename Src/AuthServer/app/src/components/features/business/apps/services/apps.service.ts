import {Injectable} from "@angular/core";
import {Http, URLSearchParams} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import {IAppsService} from "./i-apps.service";
import {AppVm} from "../models/app-vm";
import {Consts} from "../../../../consts";
import {ServiceBase} from "../../../../common/base.service";
import {NotificationsService} from "angular2-notifications";
import {ExtendedAppVm} from "../models/extended-app-vm";
import {ExtendedAppIm} from "../models/extended-app-im";


@Injectable()
export class AppsService extends ServiceBase implements IAppsService {

    constructor (
        private http: Http,
        notificationsService: NotificationsService
    ) {
        super(notificationsService);
    }

    private readonly apiUrl: string = 'http://localhost:5000/api/apps/';

    public getAll(): Observable<ExtendedAppVm[]> {

        return this.http
            .get(this.apiUrl)
            .map((res) => this.extractData(res))
            .catch((errors) => this.handleError(errors));
    }

    public put(id: string, app: ExtendedAppIm): Observable<ExtendedAppVm> {
        return this.http
            .put(this.apiUrl + id, app)
            .map((res) => this.extractData(res))
            .catch((errors) => this.handleError(errors));
    }

    public post(app: ExtendedAppIm): Observable<ExtendedAppVm> {
        return this.http
            .post(this.apiUrl, app)
            .map((res) => this.extractData(res))
            .catch((errors) => this.handleError(errors));
    }

    public get(id: string): Observable<ExtendedAppVm> {

        return this.http
            .get(this.apiUrl + id)
            .map((res) => this.extractData(res))
            .catch((errors) => this.handleError(errors));
    }

    public remove(id: string): Observable<void> {
        return this.http
            .delete(this.apiUrl + id)
            .catch((errors) => this.handleError(errors));
    }

    public getByUrl(redirectUrl: string): Observable<AppVm> {
        return this.http
                    .get(this.apiUrl + 'search?returnUrl=' + encodeURIComponent(redirectUrl || ''))
                    .map((res) => this.extractData(res))
                    .catch((error) => this.handleError(error));
    }

    public isAppExist(key: string): Observable<void> {
        return this.http
            .head(this.apiUrl + '?key=' + encodeURIComponent(key))
            .catch((error) => this.handleError(error));
    }

}