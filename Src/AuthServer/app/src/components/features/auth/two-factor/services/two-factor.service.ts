import {Injectable} from "@angular/core";
import {Provider} from "../models/provider";
import {TwoFactorVerification} from "../models/two-factor-verification";
import {ITwoFactorService} from "./i-two-factor.service";


@Injectable()
export class TwoFactorService implements ITwoFactorService {

    constructor(
    ) {}

    public getTwoFactorProviders(): Promise<Provider[]> {
        let phoneProvider = new Provider();

        phoneProvider.name = 'Phone';
        phoneProvider.value = 'Phone';

        let emailProvider = new Provider();

        emailProvider.name = 'Email';
        emailProvider.value = 'Email';

        return Promise.resolve([
            phoneProvider,
            emailProvider
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

}