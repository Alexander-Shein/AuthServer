export class LogInResult {
    constructor(
        public succeeded: boolean,
        public isLockedOut: boolean,
        public requiresTwoFactor: boolean
    ) { }
}