import {Injectable} from "@angular/core";
import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import {IPasswordlessService} from "./i-passwordless.service";
import {CallbackUrlAndUserName} from "../../shared/models/callback-url-and-user-name";
import {ServiceBase} from "../../../../common/base.service";
import {NotificationsService} from "angular2-notifications";
import {AuthenticationService} from "../../services/authentication.service";
import {Code} from "../../shared/models/code";


@Injectable()
export class PasswordlessService extends ServiceBase implements IPasswordlessService {

    constructor(
        private http: Http,
        private authenticationService: AuthenticationService,
        notificationsService: NotificationsService) {

        super(notificationsService);
    }

    private readonly apiUrl: string = 'http://localhost:5000/api/passwordless/';

    public sendLogInLink(callbackUrlAndUserName: CallbackUrlAndUserName): Observable<void> {
        return this.http
            .post(this.apiUrl + 'log-in/link', callbackUrlAndUserName)
            .catch((error) => this.handleError(error));
    }

    public logIn(code: Code): Observable<void> {
        return this.http
            .post(this.apiUrl + 'log-in', code)
            .map(() => this.authenticationService.updateLoggedIn(true))
            .catch((error) => this.handleError(error));
    }

    public sendSignUpLink(callbackUrlAndUserName: CallbackUrlAndUserName): Observable<void> {
        return this.http
            .post(this.apiUrl + 'sign-up/link', callbackUrlAndUserName)
            .catch((error) => this.handleError(error));
    }

    public signUp(code: Code): Observable<void> {
        return this.http
            .post(this.apiUrl + 'sign-up', code)
            .map(() => this.authenticationService.updateLoggedIn(true))
            .catch((error) => this.handleError(error));
    }

}
