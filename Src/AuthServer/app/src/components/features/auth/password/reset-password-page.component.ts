import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {AuthenticationService} from "../services/authentication.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";
import {ResetPassword} from "../models/reset-password";

@Component({
    selector: 'au-reset-password',
    templateUrl: './reset-password-page.component.html',
    styleUrls: ['../auth.scss', './forgot-password-page.component.scss']
})
export class ResetPasswordPageComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private notificationsService: NotificationsService,
        private spinnerService: SpinnerService
    ) {}

    private code: string;

    public ngOnInit(): void {
        this.resetPassword.code = this.route.params['code'];
    }

    public resetPassword: ResetPassword = new ResetPassword('', '', '', '');

    public onSubmit(): void {
        this.spinnerService.show();

        this.authenticationService
            .resetPassword(this.resetPassword)
            .then(() => this.handle())
            .catch((error) => this.handleError(error));
    }

    private handle(): void {
        this.router.navigate(['/log-in']);
        this.spinnerService.hide();
    }

    private handleError(error: any): void {
        let message = error || error.message || '';

        this.notificationsService
            .error('Failed.', message);

        this.spinnerService.hide();
    }

}