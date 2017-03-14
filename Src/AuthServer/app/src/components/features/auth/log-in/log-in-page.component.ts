import {Component} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthBaseComponent} from '../auth-base.component';
import {ClientSettings} from "../../clients/models/client-settings";
import {AuthenticationService} from "../services/authentication.service";
import {LogIn} from "../models/log-in";
import {LogInResult} from "../models/log-in-result";
import {NotificationsService} from "angular2-notifications";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {ExternalProvider} from "../../clients/models/external-provider";
import {ExternalLogIn} from "../models/external-log-in";


@Component({
    selector: 'au-log-in',
    templateUrl: './log-in-page.component.html',
    styleUrls: ['../auth.scss', './log-in-page.component.scss']
})
export class LogInPageComponent extends AuthBaseComponent {

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
            .subscribe((data: {clientSettings: ClientSettings}) => {
                this.clientSettings = data.clientSettings;
            });

        super.ngOnInit();
    }

    public clientSettings: ClientSettings;
    public logIn: LogIn = new LogIn('', '', false);

    public onSubmit(): void {
        this.spinnerService.show();

        this.authenticationService
            .logIn(this.logIn)
            .then((result: LogInResult) => this.handle(result))
            .catch((error: any) => this.handleError(error));
    }

    public externalLogIn(externalProvider:ExternalProvider): void {
        this.authenticationService
            .externalLogIn(new ExternalLogIn(this.redirectUrl, externalProvider.authenticationScheme));
    }

    private handle(result: LogInResult): void {
        if (result.succeeded) {
            this.redirectAfterLogin();
        }

        if (result.requiresTwoFactor) {
            this.router
                .navigate(['/two-factor', {
                    redirectUrl: this.redirectUrl,
                    rememberLogin: this.logIn.rememberLogin
                }]);
        }

        this.spinnerService.hide();
    }
}