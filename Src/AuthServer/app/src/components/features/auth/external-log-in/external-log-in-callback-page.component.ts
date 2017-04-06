import {Component} from "@angular/core";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {Consts} from "../../../consts";
import {NotificationsService} from "angular2-notifications";
import {AuthBaseComponent} from "../auth-base.component";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {AuthenticationService} from "../services/authentication.service";


@Component({
    selector: 'au-external-log-in-callback',
    template: ''
})
export class ExternalLogInCallbackPageComponent extends AuthBaseComponent {

    constructor(
        route: ActivatedRoute,
        router: Router,
        private notificationsService: NotificationsService,
        private authenticationService: AuthenticationService,
        spinnerService: SpinnerService
    ) {
        super(route, router, spinnerService);
    }

    public ngOnInit(): void {
        super.ngOnInit();

        this.route
            .queryParams
            .subscribe((params: Params) => {
                let twoFactor: boolean = params['requiresTwoFactor'] || false;
                if (twoFactor) {
                    this.router
                        .navigate(['/two-factor']);

                    return;
                }

                let isNewAccount: boolean = params['isNewAccount'] || false;
                if (isNewAccount) {
                    let email: string = params[Consts.Email];
                    let phone: string = params[Consts.Phone];

                    this.router
                        .navigate(['/email-confirmation', {
                            email: email,
                            phone: phone,
                            redirectUrl: this.redirectUrl
                        }]);

                    return;
                }

                let error: string = params[Consts.ErrorMessage];
                if (error) {
                    this.notificationsService
                        .error('Failed.', error);

                    this.router
                        .navigate(['/log-in', {
                            redirectUrl: this.redirectUrl
                        }]);
                    return;
                }

                this.authenticationService.updateLoggedIn(true);
                this.redirectAfterLogin();
            });
    }

}