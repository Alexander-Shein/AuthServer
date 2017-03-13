import { UserLoginInfo } from './user-login-info';

export class UserSettings {
    constructor(
        public hasPassword: boolean,
        public phoneNumber: string,
        public twoFactor: boolean,
        public logins: UserLoginInfo[],
        public browserRemembered: boolean
    ) {}
}