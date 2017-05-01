import {Injectable} from "@angular/core";
import {Provider} from "../models/provider";
import {ITwoFactorService} from "./i-two-factor.service";
import {Observable} from "rxjs";
import {ServiceBase} from "../../../../common/base.service";
import {NotificationsService} from "angular2-notifications";
import {Http} from "@angular/http";
import {TwoFactorVerification} from "../models/two-factor-verification";
import {AuthenticationService} from "../../services/authentication.service";


@Injectable()
export class TwoFactorService extends ServiceBase implements ITwoFactorService {

    private readonly apiUrl: string = 'http://localhost:5000/api/two-factor/';

    constructor(
        private http: Http,
        private authenticationService: AuthenticationService,
        notificationsService: NotificationsService)
    {
        super(notificationsService);
    }

    public getTwoFactorProviders(): Observable<Provider[]> {
        return this.http
            .get(this.apiUrl + 'providers')
            .map((res) => this.extractData(res))
            .catch((error) => this.handleError(error));
    }

    public sendCode(provider: Provider): Observable<void> {
        return this.http
            .post(this.apiUrl + 'codes', provider)
            .catch((error) => this.handleError(error));
    }

    public verifyCode(twoFactorVerification: TwoFactorVerification): Observable<void> {
        return this.http
            .post(this.apiUrl + 'verified', twoFactorVerification)
            .map(() => this.authenticationService.updateLoggedIn(true))
            .catch((error) => this.handleError(error));
    }

}