import {Injectable} from "@angular/core";
import {Http, URLSearchParams} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import {IAppsService} from "./i-apps.service";
import {App} from "../models/app";
import {AppVm} from "../models/app-vm";
import {Consts} from "../../../../consts";
import {ServiceBase} from "../../../../common/base.service";
import {NotificationsService} from "angular2-notifications";


@Injectable()
export class AppsService extends ServiceBase implements IAppsService {

    constructor (
        private http: Http,
        notificationsService: NotificationsService
    ) {
        super(notificationsService);
    }

    private readonly apiUrl: string = 'http://localhost:5000/api/apps/';

    public getAll(): Promise<App[]> {
        return new Promise<App[]>((resolve) =>
            setTimeout(() => resolve([
                {
                    isActive: true,
                    name: 'test-app',
                    isLocalAccountEnabled: true,
                    secret: 'secret',
                    key: 'client-id',
                    externalProviders: [],
                    redirectUrls: ['http://localhost:8000'],
                    usersCount: 435,
                    allowRememberLogIn: true
                },
                {
                    isActive: true,
                    name: 'staging-app',
                    isLocalAccountEnabled: true,
                    secret: 'secret',
                    key: 'client-id',
                    externalProviders: [],
                    redirectUrls: ['http://localhost:8000'],
                    usersCount: 4476,
                    allowRememberLogIn: true
                },
                {
                    isActive: true,
                    name: 'production-app',
                    isLocalAccountEnabled: true,
                    secret: 'secret',
                    key: 'client-id',
                    externalProviders: [],
                    redirectUrls: ['http://localhost:8000'],
                    usersCount: 123435,
                    allowRememberLogIn: true
                }]), 500)
        );
    }

    public put(app: App): Promise<App> {
        return new Promise<App>((resolve) =>
            setTimeout(() => resolve({
                isActive: true,
                name: name,
                isLocalAccountEnabled: true,
                secret: 'secret',
                key: 'client-id',
                externalProviders: [
                    {
                        displayName: 'twitter',
                        authenticationScheme: 'Twitter'
                    },
                    {
                        displayName: 'facebook',
                        authenticationScheme: 'Facebook'
                    }
                ],
                redirectUrls: ['http://localhost:8000'],
                usersCount: 435,
                allowRememberLogIn: true
            }), 500)
        );
    }

    public get(name: string): Promise<App> {
        return new Promise<App>((resolve) =>
            setTimeout(() => resolve({
                isActive: true,
                name: name,
                isLocalAccountEnabled: true,
                secret: 'secret',
                key: 'client-id',
                externalProviders: [
                    {
                        displayName: 'twitter',
                        authenticationScheme: 'Twitter'
                    },
                    {
                        displayName: 'facebook',
                        authenticationScheme: 'Facebook'
                    }
                ],
                redirectUrls: ['http://localhost:8000', 'http://localhost:8001'],
                usersCount: 435,
                allowRememberLogIn: true
            }), 500)
        );
    }

    public remove(name: string): Promise<void> {
        return new Promise<void>((resolve) =>
            setTimeout(() => resolve(), 500)
        );
    }

    public getByUrl(redirectUrl: string): Observable<AppVm> {
        let params = new URLSearchParams();
        params.set(Consts.RedirectUrl, redirectUrl);

        return this.http
                    .get(this.apiUrl + 'search', { search: params })
                    .map((res) => this.extractData(res))
                    .catch((error) => this.handleError(error));
    }

}