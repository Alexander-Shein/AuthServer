import {ExternalProvidersSettings} from "../models/external-providers-settings";
import {UserSettings} from "../models/user-settings";


export interface IUsersService {
    getUserSettings(): Promise<UserSettings>;
    getExternalProvidersSettings(): Promise<ExternalProvidersSettings>;
}