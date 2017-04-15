import {Component} from "@angular/core";
import {ActivatedRoute, Router, Params} from "@angular/router";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {ResetPassword} from "./models/reset-password";
import {AuthBaseComponent} from "../auth-base.component";
import {Consts} from "../../../consts";
import {PasswordsService} from "./services/passwords.service";


@Component({
    selector: 'au-reset-password',
    templateUrl: './reset-password-page.component.html',
    styleUrls: ['../auth.scss', './forgot-password-page.component.scss']
})
export class ResetPasswordPageComponent extends AuthBaseComponent {

    constructor(
        private passwordsService: PasswordsService,
        route: ActivatedRoute,
        router: Router,
        spinnerService: SpinnerService
    ) {
        super(route, router, spinnerService);
    }

    public ngOnInit(): void {
        this.route
            .params
            .subscribe((params: Params) => {
                this.im.code = params[Consts.Code];
                this.im.userName = params[Consts.UserName];
            });

        super.ngOnInit();
    }

    public im: ResetPassword = new ResetPassword();
    public show: boolean = false;
    public showConfirmation: boolean = false;

    public onSubmit(): void {
        this.spinnerService.show();

        this.passwordsService
            .resetPassword(this.im)
            .subscribe(
                () => this.redirectAfterLogin(),
                () => this.spinnerService.hide()
            );
    }

}