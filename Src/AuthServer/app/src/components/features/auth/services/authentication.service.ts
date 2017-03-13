import {Injectable, EventEmitter} from '@angular/core';
import {IAuthenticationService} from './i-authentication.service';
import {LogIn} from '../models/log-in';
import {LogInResult} from '../models/log-in-result';
import {ExternalLogIn} from "../models/external-log-in";
import {SignUp} from "../models/sign-up";
import {ForgotPassword} from "../models/forgot-password";
import {ResetPassword} from "../models/reset-password";
import {EmailConfirmation} from "../models/email-confirmation";


@Injectable()
export class AuthenticationService implements IAuthenticationService {

    public isLoggedIn(): boolean {
        return undefined;
    }

    public observeIsLoggedIn(): EventEmitter<boolean> {
        return undefined;
    }

    public logIn(logIn:LogIn): Promise<LogInResult> {
        return Promise.resolve<LogInResult>(new LogInResult(false, true, false));
    }

    public confirmEmail(emailConfirmation: EmailConfirmation): Promise<void> {
        return Promise.resolve();
    }

    public logOut(): void {
    }

    public signUp(signUp: SignUp): Promise<void> {
        return Promise.resolve();
    }

    public forgotPassword(forgotPassword: ForgotPassword): Promise<void> {
        return Promise.resolve();
    }

    public externalLogin(externalLogIn: ExternalLogIn): void {
      window.location.href = 'http://localhost:8080/dashboard';
        //redirect to /Account/ExternalLogin
    }

    public resetPassword(resetPassword: ResetPassword): Promise<void> {
        return Promise.resolve();
    }

}