import {ExternalProvidersSettings} from "../models/external-providers-settings";
import {UserLogInInfo} from "../models/user-log-in-info";
import {ExternalProvider} from "../models/external-provider";
import {SearchableExternalProvider} from "../models/searchable-external-provider";


export interface IExternalProvidersService {

    getExternalProvidersSettings(): Promise<ExternalProvidersSettings>;
    linkExternalLogIn(provider: string): void;
    deleteExternalLogIn(userLogInInfo: UserLogInInfo): Promise<void>;
    getAll(): Promise<ExternalProvider[]>;
    getSearchableProviders(): Promise<SearchableExternalProvider[]>;

}