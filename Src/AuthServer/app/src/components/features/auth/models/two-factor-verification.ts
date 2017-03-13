import {Provider} from "./provider";


export class TwoFactorVerification {
    constructor(
        public provider: Provider,
        public code: string,
        public rememberBrowser: boolean,
        public rememberLogin: boolean
    ) {}
}