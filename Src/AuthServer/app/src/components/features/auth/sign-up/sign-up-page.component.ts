import {Component, OnInit} from "@angular/core";
import {SignUp} from "../models/sign-up";
import {ActivatedRoute, Router, Params} from "@angular/router";
import {AuthenticationService} from "../services/authentication.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";
import {AuthBaseComponent} from "../auth-base.component";


@Component({
    selector: 'au-sign-up',
    templateUrl: './sign-up-page.component.html',
    styleUrls: ['../auth.scss', './sign-up-page.component.scss']
})
export class SignUpPageComponent extends AuthBaseComponent implements OnInit{

    constructor(
        private route: ActivatedRoute,
        private authenticationService: AuthenticationService,
        router: Router,
        notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(router, notificationsService, spinnerService);
    }

    public ngOnInit(): void {
        this.route
            .params
            .subscribe((params: Params) => {
                this.redirectUrl = params['redirectUrl'] || '';
            });
    }

    public signUp: SignUp = new SignUp('', '', '');

    public onSubmit(): void {
        this.spinnerService.show();

        this.authenticationService
            .signUp(this.signUp)
            .then(() => this.redirectAfterLogin())
            .catch((error: any) => this.handleError(error));
    }

}