import { Injectable } from '@angular/core';
import { IClientsService } from './i-clients.service';
import { ClientSettings } from '../models/client-settings';
import { ExternalProvider } from '../models/external-provider';

@Injectable()
export class ClientsService implements IClientsService {

    getClientSettings(url:string):Promise<ClientSettings> {
        return Promise.resolve(new ClientSettings(true, true, [
            new ExternalProvider('twitter', 'Twitter'),
            new ExternalProvider('facebook', 'Facebook'),
            new ExternalProvider('vk', 'Vk')
        ]));
    }

}