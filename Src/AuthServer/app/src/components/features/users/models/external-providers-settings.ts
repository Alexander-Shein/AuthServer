import { UserLoginInfo } from './user-login-info';
import { ExternalProvider } from './external-provider';

export class ExternalProvidersSettings {
    constructor(
        public currentLogins: UserLoginInfo[],
        public otherLogins: ExternalProvider[],
        public hasPassword: boolean
    ) {}
}
