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
        private spinnerService: SpinnerService
    ) {
        super(route, router);
    }

    public im: OldNewPassword = new OldNewPassword();

    public onSubmit(): void {
        this.spinnerService.show();

        this.passwordsService
            .changePassword(this.im)
            .subscribe(
                () => this.redirectAfterLogin(),
                () => this.spinnerService.hide()
            );
    }

}