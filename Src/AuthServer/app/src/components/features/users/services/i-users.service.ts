import { UserSettings } from '../models/user-settings';
import { ChangePassword } from '../models/change-password';
import { Password } from '../models/password';
import { ExternalProvidersSettings } from '../models/external-providers-settings';

export interface IUsersService {
    getUserSettings(): Promise<UserSettings>;
    changePassword(changePassword:ChangePassword): Promise<void>;
    addPassword(password:Password): Promise<void>;
    getExternalProvidersSettings(): Promise<ExternalProvidersSettings>;
}