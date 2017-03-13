import { Injectable } from '@angular/core';
import { IUsersService } from './i-users.service';
import { UserSettings } from '../models/user-settings';
import { ChangePassword } from '../models/change-password';
import { Password } from '../models/password';
import { ExternalProvidersSettings } from '../models/external-providers-settings';
import { UserLoginInfo } from '../models/user-login-info';
import { ExternalProvider } from '../models/external-provider';

@Injectable()
export class UsersService implements IUsersService {

    public getUserSettings():Promise<UserSettings> {
        return Promise.resolve(new UserSettings(true, '', true, [], true));
    }

    changePassword(changePassword:ChangePassword): Promise<void> {
        return Promise.resolve();
    }

    addPassword(password:Password): Promise<void> {
        return Promise.resolve();
    }

    getExternalProvidersSettings():Promise<ExternalProvidersSettings> {
        return Promise.resolve(new ExternalProvidersSettings([
            new UserLoginInfo('login', 'key', 'twitter'),
            new UserLoginInfo('login', 'key', 'facebook')
        ], [
            new ExternalProvider('vk', 'Vk'),
            new ExternalProvider('google', 'google')
        ], true));
    }

}