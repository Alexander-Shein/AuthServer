import {Observable} from "rxjs/Observable";
import {CallbackUrlAndUserName} from "../../shared/models/callback-url-and-user-name";
import {CodeAndUserName} from "../../shared/models/code-and-user-name";


export interface IPasswordlessService {

    sendLogInLink(callbackUrlAndUserName: CallbackUrlAndUserName): Observable<void>;
    sendSignUpLink(callbackUrlAndUserName: CallbackUrlAndUserName): Observable<void>;

    signUp(codeAndUserName: CodeAndUserName): Observable<void>;

}