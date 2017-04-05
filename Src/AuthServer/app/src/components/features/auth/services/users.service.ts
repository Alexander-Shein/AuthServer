import {Injectable} from "@angular/core";
import {UserSettings} from "../models/user-settings";
import {IUsersService} from "./i-users.service";
import {Observable} from "rxjs";
import {ServiceBase} from "../../../common/base.service";
import {NotificationsService} from "angular2-notifications";
import {Http} from "@angular/http";


@Injectable()
export class UsersService  extends ServiceBase implements IUsersService {

    private readonly apiUrl: string = 'http://localhost:5000/api/users';

    constructor(
        private http: Http,
        notificationsService: NotificationsService)
    {
        super(notificationsService);
    }

    public getUserSettings(): Promise<UserSettings> {
        return Promise.resolve({
            hasPassword: true,
            phoneNumber: '+375259065587',
            twoFactor: false,
            logIns: [],
            browserRemembered: false
        });
    }

    public isUserNameExists(userName: string): Observable<void> {
        let params = new URLSearchParams();
        params.set('userName', userName);

        return this.http
            .head(this.apiUrl + '?userName=' + encodeURIComponent(userName))
            .catch((error) => this.handleError(error));
    }

}