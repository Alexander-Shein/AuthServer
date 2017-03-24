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
        let token = 'eyJhbGciOiJSUzI1NiIsImtpZCI6ImE3YTI4NzJkZDk3ZWNiOTIxNjk0NzdhNWUzMGNmODIzIiwidHlwIjoiSldUIn0.eyJuYmYiOjE0OTAzNjI4NzgsImV4cCI6MTQ5MDM2NjQ3OCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC9yZXNvdXJjZXMiLCJhcGkxIl0sImNsaWVudF9pZCI6Im12YyIsInN1YiI6Ijg3MDliZWVlLTQxYzEtNDE0Ni1hZTQyLTdiYTQ0Zjc5ODU4ZiIsImF1dGhfdGltZSI6MTQ5MDM2Mjg3NSwiaWRwIjoibG9jYWwiLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiYXBpMSIsIm9mZmxpbmVfYWNjZXNzIl0sImFtciI6WyJwd2QiXX0.T4P5Q5duc53U1VXvTCev0aKu-jJy8NWXEkoo35piEWBTtZd98R6DHu0cqJdmpYY5dzt3muT1vYguznUhnoqaaL7D9YFu_pVOgy9dFHItPqJN5jTv44483rE2gNgJzuD4rJPyNM9XDud6G3WhSeV0dNuQmi4-Nt3OvKYJv-FLfKKSIkWWNP8ko81OU5CRED-f2xufO-AT5hcpOyYMfNyiyH21u4DNLDfpN0CRu2mz_DuyjRnuFer-vXVBwXwqLRl9lUYcqo3OYBOo24vLrN3IX97P9yWMPh9YsP1dJnC2327jRAI8PKPtQEiE1qK63ItUcztKRtFybd8axey0aFZF2g';

        let result: LogInResult = {
            succeeded: false,
            requiresTwoFactor: true,
            token: {
                accessToken: token
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