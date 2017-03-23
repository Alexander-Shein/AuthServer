import {Token} from "./token";


export class LogInResult {

    public succeeded: boolean;
    public requiresTwoFactor: boolean;
    public token: Token;

}