import {ExternalProvider} from "../models/external-provider";
import {SearchableExternalProvider} from "../models/searchable-external-provider";
import {UserExternalProvider} from "../models/user-external-provider";


export interface IExternalProvidersService {

    linkExternalLogIn(provider: string): void;
    deleteExternalLogIn(userExternalProvider: UserExternalProvider): Promise<void>;
    getAll(): Promise<ExternalProvider[]>;
    getSearchableProviders(): Promise<SearchableExternalProvider[]>;

}