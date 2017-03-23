import {Injectable, EventEmitter} from "@angular/core";
import {IAuthenticationService} from "./i-authentication.service";
import {LogIn} from "../models/log-in";
import {LogInResult} from "../models/log-in-result";
import {ExternalLogIn} from "../models/external-log-in";
import {SignUp} from "../models/sign-up";
import {Http} from "@angular/http";
import {LocalStorageService} from "ng2-webstorage";
import {Consts} from "../../../consts";
import {Token} from "../models/token";


@Injectable()
export class AuthenticationService implements IAuthenticationService {

    constructor(
        private http: Http,
        private localStorageService: LocalStorageService)
    {
        localStorageService
            .observe(Consts.AccessToken)
            .subscribe((value: string) => this.setIsLoggedIn(!!value));

        this.loggedIn = !!this.localStorageService.retrieve(Consts.AccessToken);
    }

    private loggedIn: boolean;
    private isLoggedInEventEmitter: EventEmitter<boolean> = new EventEmitter();

    public isLoggedIn(): boolean {
        return this.loggedIn;
    }

    public observeIsLoggedIn(): EventEmitter<boolean> {
        return this.isLoggedInEventEmitter;
    }

    public getToken(): Token {
        return {
            accessToken: this.localStorageService.retrieve(Consts.AccessToken)
        };
    }

    public setToken(token: Token): void {
        this.localStorageService.store(Consts.AccessToken, token.accessToken);
    }

    public logIn(logIn:LogIn): Promise<LogInResult> {
        let result: LogInResult = {
            succeeded: false,
            requiresTwoFactor: true,
            token: {
                accessToken: '12345'
            }
        };

        if (result.succeeded) {
            this.setToken(result.token);
        }

        return Promise.resolve<LogInResult>(result);
    }

    public externalLogIn(externalLogIn: ExternalLogIn): void {
        window.location.href = 'http://localhost:8080/external-log-in-callback?accessToken=54321';
    }

    public signUp(signUp: SignUp): Promise<void> {
        this.setToken({
            accessToken: '312312'
        });

        return Promise.resolve();
    }

    public externalSignUp(signUp: SignUp): Promise<void> {
        this.setToken({
                accessToken: '312312'
            });

        return Promise.resolve();
    }

    public logOut(): void {
        this.localStorageService.clear(Consts.AccessToken);
    }

    private setIsLoggedIn(value: boolean): void {
        if (value === this.loggedIn) return;

        this.loggedIn = !this.loggedIn;
        this.isLoggedInEventEmitter.emit(this.loggedIn);
    }

}