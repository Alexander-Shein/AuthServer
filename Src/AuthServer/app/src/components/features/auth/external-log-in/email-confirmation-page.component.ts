import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router, Params} from "@angular/router";
import {AuthenticationService} from "../services/authentication.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";
import {SignUp} from "../models/sign-up";
import {AuthBaseComponent} from "../auth-base.component";


@Component({
    selector: 'au-external-confirmation',
    templateUrl: './email-confirmation-page.component.html',
    styleUrls: ['../auth.scss', './email-confirmation-page.component.scss']
})
export class EmailConfirmationPageComponent extends AuthBaseComponent implements OnInit{

    constructor(
        private route: ActivatedRoute,
        private authenticationService: AuthenticationService,
        router: Router,
        notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(router, notificationsService, spinnerService);
    }

    public loginProvider: string = '';

    public ngOnInit(): void {
        this.route
            .params
            .subscribe((params: Params) => {
                this.signUp.email = params['email'] || '';
                this.loginProvider = params['loginProvider'];
                this.redirectUrl = params['redirectUrl'] || '';
            });
    }

    public signUp: SignUp = new SignUp('', null, null);

    public onSubmit(): void {
        this.spinnerService.show();

        this.authenticationService
            .externalSignUp(this.signUp)
            .then(() => this.redirectAfterLogin())
            .catch((error) => this.handleError(error));
    }
}