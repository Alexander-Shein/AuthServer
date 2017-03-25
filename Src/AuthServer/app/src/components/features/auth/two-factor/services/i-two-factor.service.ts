import {Provider} from "../models/provider";
import {TwoFactorVerification} from "../models/two-factor-verification";


export interface ITwoFactorService {

    getTwoFactorProviders(): Promise<Provider[]>;
    sendCode(provider: Provider): Promise<void>;
    verifyCode(twoFactorVerification: TwoFactorVerification): Promise<void>;
    enableTwoFactor(): Promise<void>;
    disableTwoFactor(): Promise<void>;

}