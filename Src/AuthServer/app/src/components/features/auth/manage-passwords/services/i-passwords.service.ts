import {ResetPassword} from "../models/reset-password";
import {OldNewPassword} from "../models/old-new-password";
import {NewPassword} from "../models/new-password";
import {Observable} from "rxjs";
import {UserName} from "../../models/user-name";


export interface IPasswordsService {

    sendResetPasswordCode(userName: UserName): Observable<void>;
    resetPassword(resetPassword: ResetPassword): Observable<void>;
    changePassword(changePassword: OldNewPassword): Observable<void>;
    addPassword(newPassword: NewPassword): Observable<void>;

}