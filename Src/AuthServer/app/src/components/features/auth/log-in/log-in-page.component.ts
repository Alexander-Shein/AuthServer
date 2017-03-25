import {Component} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthBaseComponent} from '../auth-base.component';
import {AuthenticationService} from "../services/authentication.service";
import {LogIn} from "../models/log-in";
import {LogInResult} from "../models/log-in-result";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {ExternalProvider} from "../external-log-in/models/external-provider";
import {AppVm} from "../../business/apps/models/app-vm";


@Component({
    selector: 'au-log-in',
    templateUrl: './log-in-page.component.html',
    styleUrls: ['../auth.scss', './log-in-page.component.scss']
})
export class LogInPageComponent extends AuthBaseComponent {

    constructor(
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
            .subscribe((data: {app: AppVm}) => {
                this.app = data.app;
            });

        super.ngOnInit();
    }

    public app: AppVm;
    public logIn: LogIn = new LogIn();

    public onSubmit(): void {
        this.spinnerService.show();

        this.authenticationService
            .logIn(this.logIn)
            .then((result: LogInResult) => this.handle(result))
            .catch(() => this.spinnerService.hide());
    }

    public externalLogIn(externalProvider: ExternalProvider): void {
        this.authenticationService
            .externalLogIn(
                {
                    redirectUrl: this.redirectUrl,
                    authenticationScheme: externalProvider.authenticationScheme
                });
    }

    private handle(result: LogInResult): void {
        if (result.succeeded) {
            this.redirectAfterLogin();
        }

        if (result.requiresTwoFactor) {
            this.router
                .navigate(['/two-factor',
                    {
                        rememberLogin: this.logIn.rememberLogin
                    }],
                    {
                        queryParams: {
                            redirectUrl: this.redirectUrl
                        }
                    });
        }

        this.spinnerService.hide();
    }
}