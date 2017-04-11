import {Injectable} from "@angular/core";
import {IExternalProvidersService} from "./i-external-providers.service";
import {ExternalProvider} from "../models/external-provider";
import {SearchableExternalProvider} from "../models/searchable-external-provider";
import {UserExternalProvider} from "../models/user-external-provider";


@Injectable()
export class ExternalProvidersService implements IExternalProvidersService {

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

    public getAll(): Promise<ExternalProvider[]> {
        return new Promise<ExternalProvider[]>((resolve) =>
            setTimeout(() => resolve([
                {
                    displayName: 'twitter',
                    authenticationScheme: 'Twitter'
                },
                {
                    displayName: 'facebook',
                    authenticationScheme: 'Facebook'
                },
                {
                    displayName: 'vk',
                    authenticationScheme: 'Vk'
                },
                {
                    displayName: 'linkedin',
                    authenticationScheme: 'LinkedIn'
                },
                {
                    displayName: 'google+',
                    authenticationScheme: 'GooglePlus'
                }]), 700)
        );
    }

}