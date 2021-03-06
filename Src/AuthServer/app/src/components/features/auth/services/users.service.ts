import {Injectable} from "@angular/core";
import {User} from "../models/user";
import {IUsersService} from "./i-users.service";
import {Observable} from "rxjs";
import {ServiceBase} from "../../../common/base.service";
import {NotificationsService} from "angular2-notifications";
import {Http} from "@angular/http";
import {UserIm} from "../models/user-im";
import {VerificationCode} from "../manage-passwords/models/verification-code";
import {AuthenticationService} from "./authentication.service";
import {UserName} from "../models/user-name";


@Injectable()
export class UsersService extends ServiceBase implements IUsersService {

    private readonly apiUrl: string = 'http://localhost:5000/api/users/';

    constructor(
        private http: Http,
        private authenticationService: AuthenticationService,
        notificationsService: NotificationsService)
    {
        super(notificationsService);
    }

    public getUser(): Observable<User> {
        return this.http
            .get(this.apiUrl + 'me')
            .map((res) => this.extractData(res))
            .catch((error) => this.handleError(error));
    }

    public isUserNameExists(userName: string): Observable<void> {
        let params = new URLSearchParams();
        params.set('userName', userName);

        return this.http
            .head(this.apiUrl + '?userName=' + encodeURIComponent(userName))
            .catch((error) => this.handleError(error));
    }

    public update(im: UserIm): Observable<User> {
        return this.http
            .patch(this.apiUrl + 'me', im)
            .map((res) => this.extractData(res))
            .catch((error) => this.handleError(error));
    }

    public confirmAccount(im: VerificationCode, provider: string): Observable<void> {
        return this.http
            .put(this.apiUrl + 'me/providers/' + provider + '/confirmed', im)
            .map(() => this.authenticationService.updateLoggedIn(true))
            .catch((error) => this.handleError(error));
    }

    public sendCodeToAddLocalProvider(userName: UserName): Observable<void> {
        return this.http
            .post(this.apiUrl + 'me/notifications/new-local-provider', userName)
            .catch((error) => this.handleError(error));
    }

}