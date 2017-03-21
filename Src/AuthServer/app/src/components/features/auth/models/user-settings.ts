import {UserLogInInfo} from "../external-log-in/models/user-log-in-info";


export class UserSettings {

    public hasPassword: boolean;
    public phoneNumber: string;
    public twoFactor: boolean;
    public logIns: UserLogInInfo[];
    public browserRemembered: boolean;

}