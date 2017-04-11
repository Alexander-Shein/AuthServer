import {ExternalProvidersSettings} from "../models/external-providers-settings";
import {ExternalProvider} from "../models/external-provider";
import {SearchableExternalProvider} from "../models/searchable-external-provider";
import {UserExternalProvider} from "../models/user-external-provider";


export interface IExternalProvidersService {

    getExternalProvidersSettings(): Promise<ExternalProvidersSettings>;
    linkExternalLogIn(provider: string): void;
    deleteExternalLogIn(userExternalProvider: UserExternalProvider): Promise<void>;
    getAll(): Promise<ExternalProvider[]>;
    getSearchableProviders(): Promise<SearchableExternalProvider[]>;

}