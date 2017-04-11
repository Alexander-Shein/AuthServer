import {User} from "../models/user";
import {Observable} from "rxjs";
import {UserIm} from "../models/user-im";


export interface IUsersService {

    getUser(): Observable<User>;
    isUserNameExists(userName: string): Observable<void>;
    update(im: UserIm): Observable<User>;

}