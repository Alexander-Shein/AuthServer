export class LogInResult {
    constructor(
        public succeeded: boolean,
        public requiresTwoFactor: boolean
    ) { }
}