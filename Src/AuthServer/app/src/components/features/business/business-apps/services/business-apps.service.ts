import {Injectable} from "@angular/core";
import {IBusinessAppsService} from "./i-business-apps.service";
import {BusinessApp} from "../models/business-app";
import {BusinessAppVm} from "../models/business-app-vm";


@Injectable()
export class BusinessAppsService implements IBusinessAppsService {

    public getAll(): Promise<BusinessApp[]> {
        return new Promise<BusinessApp[]>((resolve) =>
            setTimeout(() => resolve([
                {
                    isActive: true,
                    name: 'test-app',
                    isLocalAccountEnabled: true,
                    appCredentials: {
                        secret: 'secret',
                        id: 'client-id'
                    },
                    externalProviders: [],
                    redirectUrls: ['http://localhost:8000'],
                    usersCount: 435,
                    allowRememberLogIn: true
                },
                {
                    isActive: true,
                    name: 'staging-app',
                    isLocalAccountEnabled: true,
                    appCredentials: {
                        secret: 'secret',
                        id: 'client-id'
                    },
                    externalProviders: [],
                    redirectUrls: ['http://localhost:8000'],
                    usersCount: 4476,
                    allowRememberLogIn: true
                },
                {
                    isActive: true,
                    name: 'production-app',
                    isLocalAccountEnabled: true,
                    appCredentials: {
                        secret: 'secret',
                        id: 'client-id'
                    },
                    externalProviders: [],
                    redirectUrls: ['http://localhost:8000'],
                    usersCount: 123435,
                    allowRememberLogIn: true
                }]), 500)
        );
    }

    public put(businessApp: BusinessApp): Promise<BusinessApp> {
        return new Promise<BusinessApp>((resolve) =>
            setTimeout(() => resolve({
                isActive: true,
                name: name,
                isLocalAccountEnabled: true,
                appCredentials: {
                    secret: 'secret',
                    id: 'client-id'
                },
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

    public get(name: string): Promise<BusinessApp> {
        return new Promise<BusinessApp>((resolve) =>
            setTimeout(() => resolve({
                isActive: true,
                name: name,
                isLocalAccountEnabled: true,
                appCredentials: {
                    secret: 'secret',
                    id: 'client-id'
                },
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

    public getByUrl(url: string): Promise<BusinessAppVm> {
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