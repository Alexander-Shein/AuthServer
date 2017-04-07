import {ResetPassword} from "../models/reset-password";
import {OldNewPassword} from "../models/old-new-password";
import {NewPassword} from "../models/new-password";
import {UserName} from "../../models/user-name";


export interface IPasswordsService {

    forgotPassword(userName: UserName): Promise<void>;
    resetPassword(resetPassword: ResetPassword): Promise<void>;
    changePassword(changePassword: OldNewPassword): Promise<void>;
    addPassword(newPassword: NewPassword): Promise<void>;

}