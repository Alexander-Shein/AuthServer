import {UserExternalProvider} from "../external-log-in/models/user-external-provider";


export class User {

    public email: string;
    public isEmailConfirmed: boolean;
    public phoneNumber: string;
    public isPhoneNumberConfirmed: boolean;
    public isTwoFactorEnabled: boolean;
    public hasPassword: boolean;

    public externalProviders: UserExternalProvider[];

}