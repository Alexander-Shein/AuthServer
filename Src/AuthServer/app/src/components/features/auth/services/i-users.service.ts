import {UserSettings} from "../models/user-settings";
import {Observable} from "rxjs";


export interface IUsersService {

    getUserSettings(): Promise<UserSettings>;
    isUserNameExists(userName: string): Observable<void>;

}