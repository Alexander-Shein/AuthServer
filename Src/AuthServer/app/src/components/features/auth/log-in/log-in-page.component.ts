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
        private authenticationService: AuthenticationService,
        spinnerService: SpinnerService
    ) {
        super(route, router, spinnerService);
    }

    public ngOnInit(): void {
        this.route
            .data
            .subscribe((data: {app: AppVm}) => {
                this.app = data.app;
                this.setupUserNameLabel();
            });

        super.ngOnInit();
    }

    public app: AppVm;
    public logIn: LogIn = new LogIn();
    public searchResult: ExternalProvider;

    public userNameLabel: string;

    public onSubmit(): void {
        this.spinnerService.show();

        this.authenticationService
            .logIn(this.logIn)
            .subscribe(
                (result: LogInResult) => this.handle(result),
                () => this.spinnerService.hide());
    }

    public externalLogIn(externalProvider: ExternalProvider): void {
        this.authenticationService
            .externalLogIn(
                {
                    redirectUrl: this.redirectUrl,
                    authenticationScheme: externalProvider.authenticationScheme
                });
    }

    public searchExternalLogIn(): void
    {
        this.searchResult = null;

        if (this.logIn.userName.endsWith('@gmail.com')) {
            this.searchResult  = {
                displayName: 'GMail',
                authenticationScheme: 'GMail'
            };
        }

        if (this.logIn.userName.endsWith('@live.com')) {
            this.searchResult  = {
                displayName: 'Outlook',
                authenticationScheme: 'Outlook'
            };
        }
    }

    private setupUserNameLabel(): void {
        if (!this.app.isLocalAccountEnabled) return;

        if (this.app.emailSettings.isEnabled && this.app.phoneSettings.isEnabled) {
            this.userNameLabel = 'Email or Phone';
            return;
        }

        this.userNameLabel = this.app.emailSettings.isEnabled ? 'Email' : 'Phone';
    }

    private handle(result: LogInResult): void {
        if (result.requiresTwoFactor) {
            this.router
                .navigate(['/two-factor',
                        {
                            rememberLogIn: this.logIn.rememberLogIn
                        }],
                    {
                        queryParams: {
                            redirectUrl: this.redirectUrl
                        }
                    })
                .then(() => this.spinnerService.hide());
        } else {
            this.redirectAfterLogin();
        }
    }
}