import {Component} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";
import {AuthBaseComponent} from "../auth-base.component";
import {NewPassword} from "./models/new-password";
import {PasswordsService} from "./services/passwords.service";


@Component({
    selector: 'au-add-password',
    templateUrl: './add-password-page.component.html',
    styleUrls: ['../auth.scss', './add-password-page.component.scss']
})
export class AddPasswordPageComponent extends AuthBaseComponent {

    constructor(
        private passwordsService: PasswordsService,
        route: ActivatedRoute,
        router: Router,
        notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(route, router, notificationsService, spinnerService);
    }

    public im: NewPassword = new NewPassword();

    public onSubmit(): void {
        this.spinnerService.show();

        this.passwordsService
            .addPassword(this.im)
            .then(() => this.redirectAfterLogin())
            .catch((error: any) => this.handleError(error));
    }

}