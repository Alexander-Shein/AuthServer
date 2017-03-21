import {UserSettings} from "../models/user-settings";


export interface IUsersService {

    getUserSettings(): Promise<UserSettings>;

}