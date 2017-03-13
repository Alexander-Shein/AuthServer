import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router, Params} from "@angular/router";
import {AuthenticationService} from "../services/authentication.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";
import {EmailConfirmation} from "../models/email-confirmation";


@Component({
    selector: 'au-external-confirmation',
    templateUrl: './email-confirmation-page.component.html',
    styleUrls: ['../auth.scss', './email-confirmation-page.component.scss']
})
export class EmailConfirmationPageComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private notificationsService: NotificationsService,
        private spinnerService: SpinnerService
    ) {}

    public loginProvider: string = '';

    public ngOnInit(): void {
        this.route
            .params
            .subscribe((params: Params) => {
                this.emailConfirmation.email = params['email'] || '';
                this.loginProvider = params['loginProvider'];
            });
    }

    public emailConfirmation: EmailConfirmation = new EmailConfirmation('');

    public onSubmit(): void {
        this.spinnerService.show();

        this.authenticationService
            .confirmEmailForExternalLogIn(this.emailConfirmation)
            .then(() => this.handle())
            .catch((error) => this.handleError(error));
    }

    private handle(): void {
        this.router.navigate(['/dashboard']);
        this.spinnerService.hide();
    }

    private handleError(error: any): void {
        let message = error || error.message || '';

        this.notificationsService
            .error('Failed.', message);

        this.spinnerService.hide();
    }

}