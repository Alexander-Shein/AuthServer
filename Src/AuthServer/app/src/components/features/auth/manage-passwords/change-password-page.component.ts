import {Component} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {AuthBaseComponent} from "../auth-base.component";
import {OldNewPassword} from "./models/old-new-password";
import {PasswordsService} from "./services/passwords.service";


@Component({
    selector: 'au-change-password',
    templateUrl: './change-password-page.component.html',
    styleUrls: ['../auth.scss', './change-password-page.component.scss']
})
export class ChangePasswordPageComponent extends AuthBaseComponent {

    constructor(
        private passwordsService: PasswordsService,
        route: ActivatedRoute,
        router: Router,
        spinnerService: SpinnerService
    ) {
        super(route, router, spinnerService);
    }

    public im: OldNewPassword = new OldNewPassword();

    public onSubmit(): void {
        this.spinnerService.show();

        this.passwordsService
            .changePassword(this.im)
            .then(() => this.redirectAfterLogin())
            .catch(() => this.spinnerService.hide());
    }

}