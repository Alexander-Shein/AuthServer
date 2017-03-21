import {Injectable, EventEmitter} from "@angular/core";
import {IAuthenticationService} from "./i-authentication.service";
import {LogIn} from "../models/log-in";
import {LogInResult} from "../models/log-in-result";
import {ExternalLogIn} from "../models/external-log-in";
import {SignUp} from "../models/sign-up";


@Injectable()
export class AuthenticationService implements IAuthenticationService {

    public isLoggedIn(): boolean {
        return undefined;
    }

    public observeIsLoggedIn(): EventEmitter<boolean> {
        return undefined;
    }

    public logIn(logIn:LogIn): Promise<LogInResult> {
        return Promise.resolve<LogInResult>({
            succeeded: true,
            requiresTwoFactor: true
        });
    }

    public externalLogIn(externalLogIn: ExternalLogIn): void {
        window.location.href = 'http://localhost:8080/dashboard';
    }

    public signUp(signUp: SignUp): Promise<void> {
        return Promise.resolve();
    }

    public externalSignUp(signUp: SignUp): Promise<void> {
        return Promise.resolve();
    }

    public logOut(): void {
    }

}