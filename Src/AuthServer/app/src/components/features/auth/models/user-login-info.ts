export class UserLoginInfo {
    constructor(
        public loginProvider:string,
        public providerKey:string,
        public providerDisplayName:string
    ) {}
}