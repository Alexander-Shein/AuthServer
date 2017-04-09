import {Injectable} from "@angular/core";
import {ResetPassword} from "../models/reset-password";
import {OldNewPassword} from "../models/old-new-password";
import {NewPassword} from "../models/new-password";
import {IPasswordsService} from "./i-passwords.service";
import {UserName} from "../../models/user-name";
import {Observable} from "rxjs";


@Injectable()
export class PasswordsService implements IPasswordsService {

    public sendResetPasswordCode(userName: UserName): Observable<void> {
        return null;
    }

    public resetPassword(resetPassword: ResetPassword): Promise<void> {
        return Promise.resolve();
    }

    public changePassword(changePassword: OldNewPassword): Promise<void> {
        return Promise.resolve();
    }

    public addPassword(newPassword: NewPassword): Promise<void> {
        return Promise.resolve();
    }

}