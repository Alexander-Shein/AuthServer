import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router'
import {ClientSettings} from '../../clients/models/client-settings';
import {AuthenticationService} from '../services/authentication.service';
import {LogIn} from '../models/log-in';
import {LogInResult} from '../models/log-in-result';
import {NotificationsService} from "angular2-notifications";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {ExternalProvider} from "../../clients/models/external-provider";
import {ExternalLogIn} from "../models/external-log-in";

@Component({
    selector: 'au-log-in',
    templateUrl: './log-in-page.component.html',
    styleUrls: ['../auth.scss', './log-in-page.component.scss']
})
export class LogInPageComponent implements OnInit{

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private notificationsService: NotificationsService,
        private spinnerService: SpinnerService
    ) {}

    public ngOnInit(): void {
        this.route
            .data
            .subscribe((data: { clientSettings: ClientSettings }) => {
                this.clientSettings = data.clientSettings;
            });

        this.redirectUrl = this.route.params['redirectUrl'] || '';
    }

    public redirectUrl: string;
    public clientSettings: ClientSettings;
    public logIn: LogIn = new LogIn('', '', false);

    public onSubmit(): void {
        this.spinnerService.show();

        this.authenticationService
            .logIn(this.logIn)
            .then((result) => this.handleLogIn(result))
            .catch((error) => this.handleError(error));
    }

    public externalLogIn(externalProvider:ExternalProvider): void {
        this.authenticationService
            .externalLogin(new ExternalLogIn(this.redirectUrl, externalProvider.authenticationScheme));
    }

    private handleLogIn(result: LogInResult): void {
        if (result.succeeded) {
            if (this.redirectUrl) {
                window.location.href = this.redirectUrl;
            } else {
                this.router.navigate(['dashboard']);
            }
        }

        if (result.isLockedOut) {
            this.handleError('Maximum number of attempts exited. Please contact administrator.');
            return;
        }

        if (result.requiresTwoFactor) {
            this.router.navigate(['two-factor']);
        }

        this.spinnerService.hide();
    }

    private handleError(error: any): void {
        let message = error || error.message || '';

        this.notificationsService
            .error('Failed.', message);

        this.spinnerService.hide();
    }
}