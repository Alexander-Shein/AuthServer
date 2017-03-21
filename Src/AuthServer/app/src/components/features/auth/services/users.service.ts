import {Injectable} from "@angular/core";
import {UserSettings} from "../models/user-settings";
import {IUsersService} from "./i-users.service";


@Injectable()
export class UsersService implements IUsersService {

    public getUserSettings(): Promise<UserSettings> {
        return Promise.resolve({
            hasPassword: true,
            phoneNumber: '+375259065587',
            twoFactor: false,
            logIns: [],
            browserRemembered: false
        });
    }

}