import {Component} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthenticationService} from "../services/authentication.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";
import {AuthBaseComponent} from "../auth-base.component";
import {ChangePassword} from "../models/change-password";


@Component({
    selector: 'au-change-password',
    templateUrl: './change-password-page.component.html',
    styleUrls: ['../auth.scss', './change-password-page.component.scss']
})
export class ChangePasswordPageComponent extends AuthBaseComponent {

    constructor(
        private authenticationService: AuthenticationService,
        route: ActivatedRoute,
        router: Router,
        notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(route, router, notificationsService, spinnerService);
    }

    public changePassword: ChangePassword = new ChangePassword('', '', '');

    public onSubmit(): void {
        this.spinnerService.show();

        this.authenticationService
            .changePassword(this.changePassword)
            .then(() => this.redirectAfterLogin())
            .catch((error: any) => this.handleError(error));
    }

}