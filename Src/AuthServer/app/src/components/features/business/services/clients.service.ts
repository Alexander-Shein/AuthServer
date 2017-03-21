import {Injectable} from "@angular/core";
import {IClientsService} from "./i-clients.service";
import {ClientSettings} from "../models/client-settings";


@Injectable()
export class ClientsService implements IClientsService {

    getClientSettings(url:string):Promise<ClientSettings> {
        return Promise.resolve(
            {
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
                enableLocalLogIn: true
            });
    }

}