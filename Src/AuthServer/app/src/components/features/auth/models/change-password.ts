export class ChangePassword {
    constructor(
        public oldPassword: string,
        public newPassword: string,
        public confirmPassword: string
    ) {}
}