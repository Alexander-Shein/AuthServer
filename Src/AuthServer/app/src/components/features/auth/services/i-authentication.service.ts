import {EventEmitter} from "@angular/core";
import {LogIn} from "../models/log-in";
import {LogInResult} from "../models/log-in-result";
import {ExternalLogIn} from "../models/external-log-in";
import {SignUp} from "../models/sign-up";


export interface IAuthenticationService {

    isLoggedIn(): boolean;
    observeIsLoggedIn(): EventEmitter<boolean>;

    logIn(logIn: LogIn): Promise<LogInResult>;
    signUp(signUp: SignUp): Promise<void>;

    externalLogIn(externalLogIn: ExternalLogIn): void;
    externalSignUp(signUp: SignUp): Promise<void>;

    logOut(): void;

}