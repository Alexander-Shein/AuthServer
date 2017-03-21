import {Injectable} from "@angular/core";
import {IBusinessAppsService} from "./i-business-apps.service";
import {BusinessApp} from "../models/business-app";


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
                    usersCount: 435
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
                    usersCount: 4476
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
                    usersCount: 123435
                }]), 1500)
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
                usersCount: 435
            }), 1500)
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
                usersCount: 435
            }), 1500)
        );
    }

    public remove(name: string): Promise<void> {
        return new Promise<void>((resolve) =>
            setTimeout(() => resolve(), 1500)
        );
    }

}