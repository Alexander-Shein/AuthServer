import {Provider} from "../models/provider";
import {TwoFactorVerification} from "../models/two-factor-verification";
import {Observable} from "rxjs";


export interface ITwoFactorService {

    getTwoFactorProviders(): Observable<Provider[]>;
    sendCode(provider: Provider): Observable<void>;
    verifyCode(twoFactorVerification: TwoFactorVerification): Observable<void>;

}