import {Component} from "@angular/core";
import {ActivatedRoute, Router, Params} from "@angular/router";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {AuthBaseComponent} from "../auth-base.component";
import {Consts} from "../../../consts";
import {TwoFactorService} from "./services/two-factor.service";
import {Provider} from "./models/provider";
import {TwoFactorVerification} from "./models/two-factor-verification";
import {AuthenticationService} from "../services/authentication.service";


@Component({
    selector: 'au-two-factor',
    templateUrl: './two-factor-page.component.html',
    styleUrls: ['../auth.scss', './two-factor-page.component.scss']
})
export class TwoFactorPageComponent extends AuthBaseComponent {

    constructor(
        private twoFactorService: TwoFactorService,
        route: ActivatedRoute,
        router: Router,
        authenticationService: AuthenticationService,
        spinnerService: SpinnerService
    ) {
        super(route, router, authenticationService, spinnerService);
    }

    public ngOnInit(): void {
        this.route
            .data
            .subscribe((data: {providers: Provider[]}) => {
                this.providers = data.providers;
                this.twoFactorVerification.provider = this.providers[0];
            });

        this.route
            .params
            .subscribe((params: Params) => {
                this.twoFactorVerification.rememberLogin = params[Consts.RememberLogin] || false;
            });

        super.ngOnInit();
    }

    public providers: Provider[];
    public isCodeSent: boolean = false;
    public twoFactorVerification: TwoFactorVerification = new TwoFactorVerification();

    public sendCode(): void {
        this.twoFactorService
            .sendCode(this.twoFactorVerification.provider)
            .then(() => {
                this.isCodeSent = true;
                this.spinnerService.hide();
            })
            .catch(() => this.spinnerService.hide());
    }

    public verifyCode(): void {
        this.spinnerService.show();

        this.twoFactorService
            .verifyCode(this.twoFactorVerification)
            .then(() => this.redirectAfterLogin())
            .catch(() => this.spinnerService.hide());
    }
}