import {ClientSettings} from "../models/client-settings";


export interface IClientsService {

    getClientSettings(url:string): Promise<ClientSettings>;

}