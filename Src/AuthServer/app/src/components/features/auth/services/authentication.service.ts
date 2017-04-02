import {Injectable, EventEmitter} from "@angular/core";
import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import {IAuthenticationService} from "./i-authentication.service";
import {LogIn} from "../models/log-in";
import {LogInResult} from "../models/log-in-result";
import {ExternalLogIn} from "../models/external-log-in";
import {SignUp} from "../models/sign-up";
import {LocalStorageService} from "ng2-webstorage";
import {Consts} from "../../../consts";
import {ServiceBase} from "../../../common/base.service";
import {NotificationsService} from "angular2-notifications";


@Injectable()
export class AuthenticationService extends ServiceBase implements IAuthenticationService {

    constructor(
        private http: Http,
        private localStorageService: LocalStorageService,
        notificationsService: NotificationsService)
    {
        super(notificationsService);

        localStorageService
            .observe(Consts.IsLoggedIn)
            .subscribe((value: boolean) => this.setIsLoggedIn(value));

        this.loggedIn = this.localStorageService.retrieve(Consts.IsLoggedIn);
    }

    private readonly apiUrl: string = 'http://localhost:5000/api/auth/';
    private loggedIn: boolean;
    private isLoggedInEventEmitter: EventEmitter<boolean> = new EventEmitter();

    public isLoggedIn(): boolean {
        return this.loggedIn;
    }

    public observeIsLoggedIn(): EventEmitter<boolean> {
        return this.isLoggedInEventEmitter;
    }

    public logIn(logIn:LogIn): Observable<LogInResult> {

        let result = this.http
            .post(this.apiUrl + 'log-in', logIn)
            .map((res) => this.extractData(res));

        result.subscribe((res: LogInResult) => {
            if (!res.requiresTwoFactor) {
                this.setIsLoggedIn(true);
                this.localStorageService.store(Consts.IsLoggedIn, true);
            }
        });

        return result.catch((error) => this.handleError(error));
    }

    public externalLogIn(externalLogIn: ExternalLogIn): void {
        window.location.href = 'http://localhost:8080/external-log-in-callback?accessToken=54321';
    }

    public signUp(signUp: SignUp): Promise<void> {
        return Promise.resolve();
    }

    public externalSignUp(signUp: SignUp): Promise<void> {
        return Promise.resolve();
    }

    public logOut(): void {
        this.localStorageService.clear(Consts.IsLoggedIn);
    }

    private setIsLoggedIn(value: boolean): void {
        if (value === this.loggedIn) return;

        this.loggedIn = !this.loggedIn;
        this.isLoggedInEventEmitter.emit(this.loggedIn);
    }

    public isUserNameExists(userName: string): Observable<void> {
        return new Observable<void>(observer => {
            setTimeout(() => {
                observer.next();
            }, 1000);
        });
    }

}