import {User} from "../models/user";
import {Observable} from "rxjs";


export interface IUsersService {

    getUser(): Observable<User>;
    isUserNameExists(userName: string): Observable<void>;

}