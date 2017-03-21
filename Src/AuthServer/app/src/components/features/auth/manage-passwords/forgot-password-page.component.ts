import {Component} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";
import {Email} from "./models/email";
import {AuthBaseComponent} from "../auth-base.component";
import {PasswordsService} from "./services/passwords.service";


@Component({
    selector: 'au-forgot-password',
    templateUrl: './forgot-password-page.component.html',
    styleUrls: ['../auth.scss', './forgot-password-page.component.scss']
})
export class ForgotPasswordPageComponent extends AuthBaseComponent {

    constructor(
        private passwordsService: PasswordsService,
        route: ActivatedRoute,
        router: Router,
        notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(route, router, notificationsService, spinnerService);
    }

    private isEmailSent: boolean = false;

    public im: Email = new Email();

    public onSubmit(): void {
        this.spinnerService.show();

        this.passwordsService
            .forgotPassword(this.im)
            .then(() => this.handle())
            .catch((error) => this.handleError(error));
    }

    private handle(): void {
        this.isEmailSent = true;
        this.spinnerService.hide();
    }

}