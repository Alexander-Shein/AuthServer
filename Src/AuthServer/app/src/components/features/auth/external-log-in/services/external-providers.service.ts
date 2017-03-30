import {Injectable} from "@angular/core";
import {ExternalProvidersSettings} from "../models/external-providers-settings";
import {UserLogInInfo} from "../models/user-log-in-info";
import {IExternalProvidersService} from "./i-external-providers.service";
import {ExternalProvider} from "../models/external-provider";
import {SearchableExternalProvider} from "../models/searchable-external-provider";


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

    public getExternalProvidersSettings(): Promise<ExternalProvidersSettings> {
        return Promise.resolve({
            hasPassword: true,
            otherLogIns: [
                {
                    displayName: 'vk',
                    authenticationScheme: 'Vk'
                },
                {
                    displayName: 'google',
                    authenticationScheme: 'Google'
                }
            ],
            currentLogIns: [
                {
                    loginProvider: 'login',
                    providerKey: 'key',
                    providerDisplayName: 'twitter'
                },
                {
                    loginProvider: 'login',
                    providerKey: 'key',
                    providerDisplayName: 'facebook'
                }
            ]
        });
    }

    public linkExternalLogIn(provider: string): void {
    }

    public deleteExternalLogIn(userLogInInfo: UserLogInInfo): Promise<void> {
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