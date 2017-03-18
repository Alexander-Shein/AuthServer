import {Injectable, EventEmitter} from "@angular/core";
import {IAuthenticationService} from "./i-authentication.service";
import {LogIn} from "../models/log-in";
import {LogInResult} from "../models/log-in-result";
import {ExternalLogIn} from "../models/external-log-in";
import {SignUp} from "../models/sign-up";
import {ForgotPassword} from "../models/forgot-password";
import {ResetPassword} from "../models/reset-password";
import {Provider} from "../models/provider";
import {TwoFactorVerification} from "../models/two-factor-verification";
import {ChangePassword} from "../models/change-password";
import {AddPassword} from "../models/add-password";
import {AddPhoneNumber} from "../models/add-phone-number";
import {ExternalProvidersSettings} from "../models/external-providers-settings";
import {UserLoginInfo} from "../models/user-login-info";
import {ExternalProvider} from "../models/external-provider";
import {UserSettings} from "../models/user-settings";


@Injectable()
export class AuthenticationService implements IAuthenticationService {

    public isLoggedIn(): boolean {
        return undefined;
    }

    public observeIsLoggedIn(): EventEmitter<boolean> {
        return undefined;
    }

    public logIn(logIn:LogIn): Promise<LogInResult> {
        return Promise.resolve<LogInResult>(new LogInResult(false, true));
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

    public forgotPassword(forgotPassword: ForgotPassword): Promise<void> {
        return Promise.resolve();
    }

    public resetPassword(resetPassword: ResetPassword): Promise<void> {
        return Promise.resolve();
    }

    public changePassword(changePassword: ChangePassword): Promise<void> {
        return Promise.resolve();
    }

    public addPassword(addPassword: AddPassword): Promise<void> {
        return undefined;
    }

    public sendVerificationCode(addPhoneNumber: AddPhoneNumber): Promise<void> {
        return Promise.resolve();
    }

    public deletePhoneNumber(): Promise<void> {
        return Promise.resolve();
    }

    public addPhoneNumber(addPhoneNumber: AddPhoneNumber): Promise<void> {
        return Promise.resolve();
    }

    public getTwoFactorProviders(): Promise<Provider[]> {
        return Promise.resolve([
            new Provider('Phone', 'Phone'),
            new Provider('Email', 'Email')
        ]);
    }

    public sendCode(provider: Provider): Promise<void> {
        return Promise.resolve();
    }

    public verifyCode(twoFactorVerification: TwoFactorVerification): Promise<void> {
        return Promise.resolve();
    }

    public enableTwoFactor(): Promise<void> {
        return Promise.resolve();
    }

    public disableTwoFactor(): Promise<void> {
        return Promise.resolve();
    }

    public getExternalProvidersSettings():Promise<ExternalProvidersSettings> {
        return Promise.resolve(new ExternalProvidersSettings([
            new UserLoginInfo('login', 'key', 'twitter'),
            new UserLoginInfo('login', 'key', 'facebook')
        ], [
            new ExternalProvider('vk', 'Vk'),
            new ExternalProvider('google', 'google')
        ], true));
    }

    public linkExternalLogin(provider: string): void {
    }

    public deleteExternalLogin(userLoginInfo: UserLoginInfo): Promise<void> {
        return Promise.resolve();
    }

    public getUserSettings():Promise<UserSettings> {
        return Promise.resolve(new UserSettings(true, '', true, [], true));
    }

}