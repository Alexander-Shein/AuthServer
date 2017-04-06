import {Provider} from "./provider";


export class TwoFactorVerification {

    public provider: Provider;
    public code: string = '';
    public rememberBrowser: boolean = true;
    public rememberLogIn: boolean;

}