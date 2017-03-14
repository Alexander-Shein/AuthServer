import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router, Params} from "@angular/router";
import {AuthenticationService} from "../services/authentication.service";
import {NotificationsService} from "angular2-notifications";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {Provider} from "../models/provider";
import {TwoFactorVerification} from "../models/two-factor-verification";
import {AuthBaseComponent} from "../auth-base.component";
import {Consts} from "../../../consts";


@Component({
    selector: 'au-two-factor',
    templateUrl: './two-factor-page.component.html',
    styleUrls: ['../auth.scss', './two-factor-page.component.scss']
})
export class TwoFactorPageComponent extends AuthBaseComponent {

    constructor(
        private authenticationService: AuthenticationService,
        route: ActivatedRoute,
        router: Router,
        notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(route, router, notificationsService, spinnerService);
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
    public twoFactorVerification: TwoFactorVerification = new TwoFactorVerification(null, '', false, false);

    public sendCode(): void {
        this.authenticationService
            .sendCode(this.twoFactorVerification.provider)
            .then(() => {
                this.isCodeSent = true;
                this.spinnerService.hide();
            })
            .catch((error: any) => this.handleError(error));
    }

    public verifyCode(): void {
        this.spinnerService.show();

        this.authenticationService
            .verifyCode(this.twoFactorVerification)
            .then(() => this.redirectAfterLogin())
            .catch((error: any) => this.handleError(error));
    }
}