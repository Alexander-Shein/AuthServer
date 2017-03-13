import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Params, Router} from "@angular/router";
import {AuthenticationService} from "../services/authentication.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";
import {ForgotPassword} from "../models/forgot-password";
import {AuthBaseComponent} from "../auth-base.component";


@Component({
    selector: 'au-forgot-password',
    templateUrl: './forgot-password-page.component.html',
    styleUrls: ['../auth.scss', './forgot-password-page.component.scss']
})
export class ForgotPasswordPageComponent extends AuthBaseComponent implements OnInit{

    constructor(
        private route: ActivatedRoute,
        private authenticationService: AuthenticationService,
        router: Router,
        notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(router, notificationsService, spinnerService);
    }

    private isEmailSent: boolean = false;

    public ngOnInit(): void {
        this.route
            .params
            .subscribe((params: Params) => {
                this.redirectUrl = params['redirectUrl'] || '';
            });
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

}