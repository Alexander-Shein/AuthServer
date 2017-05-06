import {Injectable} from "@angular/core";
import {IExternalProvidersService} from "./i-external-providers.service";
import {ExternalProvider} from "../models/external-provider";
import {SearchableExternalProvider} from "../models/searchable-external-provider";
import {UserExternalProvider} from "../models/user-external-provider";
import {ServiceBase} from "../../../../common/base.service";
import {Http} from "@angular/http";
import {NotificationsService} from "angular2-notifications";
import {Observable} from "rxjs/Observable";


@Injectable()
export class ExternalProvidersService extends ServiceBase implements IExternalProvidersService {

    private readonly apiUrl: string = 'http://localhost:5000/api/external-providers/';

    constructor(
        private http: Http,
        notificationsService: NotificationsService)
    {
        super(notificationsService);
    }

    public getSearchableProviders(): Promise<SearchableExternalProvider[]> {
        return Promise.resolve([
                {
                    matches: ['@live', '@outlook'],
                    displayName: 'Microsoft',
                    authenticationScheme: 'Microsoft'
                },
                {
                    matches: ['@gmail', '@outlook'],
                    displayName: 'Google',
                    authenticationScheme: 'Google'
                }
            ]);
    }

    public linkExternalLogIn(provider: string): void {
    }

    public deleteExternalLogIn(userExternalProvider: UserExternalProvider): Promise<void> {
        return Promise.resolve();
    }

    public getAll(): Observable<ExternalProvider[]> {
        return this.http
            .get(this.apiUrl)
            .map((res) => this.extractData(res))
            .catch((error) => this.handleError(error));
    }

}