import {Injectable} from "@angular/core";
import {IAppsService} from "./i-apps.service";
import {App} from "../models/app";
import {AppVm} from "../models/app-vm";


@Injectable()
export class AppsService implements IAppsService {

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

    public getByUrl(url: string): Promise<AppVm> {
        return Promise.resolve(
            {
                name: 'my-account',
                externalProviders: [
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
                    }],
                allowRememberLogIn: true,
                isLocalAccountEnabled: true
            });
    }

}