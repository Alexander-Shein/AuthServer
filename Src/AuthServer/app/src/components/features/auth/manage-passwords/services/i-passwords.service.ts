import {ResetPassword} from "../models/reset-password";
import {OldNewPassword} from "../models/old-new-password";
import {NewPassword} from "../models/new-password";
import {UserName} from "../../models/user-name";
import {Observable} from "rxjs";


export interface IPasswordsService {

    sendResetPasswordCode(userName: UserName): Observable<void>;
    resetPassword(resetPassword: ResetPassword): Promise<void>;
    changePassword(changePassword: OldNewPassword): Observable<void>;
    addPassword(newPassword: NewPassword): Observable<void>;

}