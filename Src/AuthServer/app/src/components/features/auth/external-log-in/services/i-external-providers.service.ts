import {ExternalProvider} from "../models/external-provider";
import {SearchableExternalProvider} from "../models/searchable-external-provider";
import {UserExternalProvider} from "../models/user-external-provider";
import {Observable} from "rxjs/Observable";


export interface IExternalProvidersService {

    linkExternalLogIn(provider: string): void;
    deleteExternalLogIn(userExternalProvider: UserExternalProvider): Promise<void>;
    getAll(): Observable<ExternalProvider[]>;
    getSearchableProviders(): Promise<SearchableExternalProvider[]>;

}