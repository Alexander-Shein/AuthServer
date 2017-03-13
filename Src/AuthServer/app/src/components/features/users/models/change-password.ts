import { Password } from './password';

export class ChangePassword extends Password{
    constructor (
        public oldPassword: string,
        newPassword: string,
        confirmPassword: string
    ) {
        super(newPassword, confirmPassword)
    }
}