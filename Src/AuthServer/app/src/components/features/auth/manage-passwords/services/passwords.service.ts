import {Injectable} from "@angular/core";
import {Email} from "../models/email";
import {ResetPassword} from "../models/reset-password";
import {OldNewPassword} from "../models/old-new-password";
import {NewPassword} from "../models/new-password";
import {IPasswordsService} from "./i-passwords.service";


@Injectable()
export class PasswordsService implements IPasswordsService {

    public forgotPassword(email: Email): Promise<void> {
        return Promise.resolve();
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