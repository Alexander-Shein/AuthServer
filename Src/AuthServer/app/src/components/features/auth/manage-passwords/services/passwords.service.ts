import {Injectable} from "@angular/core";
import {ResetPassword} from "../models/reset-password";
import {OldNewPassword} from "../models/old-new-password";
import {NewPassword} from "../models/new-password";
import {IPasswordsService} from "./i-passwords.service";
import {UserName} from "../../models/user-name";
import {Observable} from "rxjs";
import {ServiceBase} from "../../../../common/base.service";
import {Http} from "@angular/http";
import {NotificationsService} from "angular2-notifications";
import {ForgotPassword} from "../models/forgot-password";
import {Consts} from "../../../../consts";


@Injectable()
export class PasswordsService extends ServiceBase implements IPasswordsService {

    private readonly apiUrl: string = 'http://localhost:5000/api/passwords/';

    constructor(
        private http: Http,
        notificationsService: NotificationsService)
    {
        super(notificationsService);
    }

    public sendResetPasswordCode(userName: UserName): Observable<void> {
        let forgotPassword: ForgotPassword = {
            userName: userName.userName,
            resetPasswordUrl: this.buildResetPasswordUrl(userName.userName)
        };

        return this.http
            .post(this.apiUrl + 'forgot', forgotPassword)
            .catch((error) => this.handleError(error));
    }

    public resetPassword(resetPassword: ResetPassword): Observable<void> {
        return this.http
            .post(this.apiUrl + 'reset', resetPassword)
            .catch((error) => this.handleError(error));
    }

    public changePassword(changePassword: OldNewPassword): Observable<void> {
        return this.http
            .put(this.apiUrl + 'change', changePassword)
            .catch((error) => this.handleError(error));
    }

    public addPassword(newPassword: NewPassword): Observable<void> {
        return this.http
            .patch(this.apiUrl, newPassword)
            .catch((error) => this.handleError(error));
    }

    private buildResetPasswordUrl(userName: string): string {
        let arr = window.location.href.split('/');
        let result = arr[0] + '//' + arr[2];

        let url =
            result + '/reset-password;' +
            Consts.Code + '={code};' + Consts.UserName + '=' + encodeURIComponent(userName);

        return url;
    }

}