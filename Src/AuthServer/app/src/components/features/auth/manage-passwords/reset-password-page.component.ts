import {Component} from "@angular/core";
import {ActivatedRoute, Router, Params} from "@angular/router";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";
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
        notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(route, router, notificationsService, spinnerService);
    }

    public ngOnInit(): void {
        this.route
            .queryParams
            .subscribe((params: Params) => {
                this.im.code = params[Consts.Code];
            });

        super.ngOnInit();
    }

    public im: ResetPassword = new ResetPassword();

    public onSubmit(): void {
        this.spinnerService.show();

        this.passwordsService
            .resetPassword(this.im)
            .then(() => this.handle())
            .catch((error) => this.handleError(error));
    }

    private handle(): void {
        this.router
            .navigate(['/log-in'],
            {
                queryParams: {
                    redirectUrl: this.redirectUrl
                }
            });

        this.spinnerService.hide();
    }

}