import {User} from "../models/user";
import {Observable} from "rxjs";
import {UserIm} from "../models/user-im";
import {VerificationCode} from "../manage-passwords/models/verification-code";


export interface IUsersService {

    getUser(): Observable<User>;
    isUserNameExists(userName: string): Observable<void>;
    update(im: UserIm): Observable<User>;
    confirmAccount(verificationCode: VerificationCode, provider: string): Observable<void>;

}