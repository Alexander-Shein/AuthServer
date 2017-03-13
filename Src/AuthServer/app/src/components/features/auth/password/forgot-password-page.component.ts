import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {AuthenticationService} from "../services/authentication.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";
import {ForgotPassword} from "../models/forgot-password";


@Component({
    selector: 'au-forgot-password',
    templateUrl: './forgot-password-page.component.html',
    styleUrls: ['../auth.scss', './forgot-password-page.component.scss']
})
export class ForgotPasswordPageComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private authenticationService: AuthenticationService,
        private notificationsService: NotificationsService,
        private spinnerService: SpinnerService
    ) {}

    private redirectUrl: string;
    private isEmailSent: boolean = false;

    public ngOnInit(): void {
        this.redirectUrl = this.route.params['redirectUrl'] || '';
    }

    public forgotPassword: ForgotPassword = new ForgotPassword('');

    public onSubmit(): void {
        this.spinnerService.show();

        this.authenticationService
            .forgotPassword(this.forgotPassword)
            .then(() => this.handle())
            .catch((error) => this.handleError(error));
    }

    private handle(): void {
        this.isEmailSent = true;
        this.spinnerService.hide();
    }

    private handleError(error: any): void {
        let message = error || error.message || '';

        this.notificationsService
            .error('Failed.', message);

        this.spinnerService.hide();
    }

}