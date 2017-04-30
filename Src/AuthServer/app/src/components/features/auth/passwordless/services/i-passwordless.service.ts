import {Observable} from "rxjs/Observable";
import {CallbackUrlAndUserName} from "../../shared/models/callback-url-and-user-name";
import {Code} from "../../shared/models/code";


export interface IPasswordlessService {

    sendLogInLink(callbackUrlAndUserName: CallbackUrlAndUserName): Observable<void>;
    sendSignUpLink(callbackUrlAndUserName: CallbackUrlAndUserName): Observable<void>;

    signUp(code: Code): Observable<void>;
    logIn(code: Code): Observable<void>;

}