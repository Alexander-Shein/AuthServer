import { UserLoginInfo } from './user-login-info';
import { ExternalProvider } from './external-provider';

export class ExternalProvidersSettings {
    constructor(
        public currentLogIns: UserLoginInfo[],
        public otherLogIns: ExternalProvider[],
        public hasPassword: boolean
    ) {}
}