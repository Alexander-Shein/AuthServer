import {Email} from "../models/email";
import {ResetPassword} from "../models/reset-password";
import {OldNewPassword} from "../models/old-new-password";
import {NewPassword} from "../models/new-password";


export interface IPasswordsService {

    forgotPassword(email: Email): Promise<void>;
    resetPassword(resetPassword: ResetPassword): Promise<void>;
    changePassword(changePassword: OldNewPassword): Promise<void>;
    addPassword(newPassword: NewPassword): Promise<void>;

}