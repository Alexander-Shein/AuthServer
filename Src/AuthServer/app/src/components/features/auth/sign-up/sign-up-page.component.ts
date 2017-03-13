import {Component, OnInit} from '@angular/core';
import {SignUp} from '../models/sign-up';
import {ActivatedRoute, Router} from "@angular/router";
import {AuthenticationService} from "../services/authentication.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";

@Component({
    selector: 'au-sign-up',
    templateUrl: './sign-up-page.component.html',
    styleUrls: ['../auth.scss', './sign-up-page.component.scss']
})
export class SignUpPageComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private notificationsService: NotificationsService,
        private spinnerService: SpinnerService
    ) {}

    private redirectUrl: string;

    public ngOnInit(): void {
        this.redirectUrl = this.route.params['redirectUrl'] || '';
    }

    public signUp: SignUp = new SignUp('', '', '');

    public onSubmit(): void {
        this.spinnerService.show();

        this.authenticationService
            .signUp(this.signUp)
            .then(() => this.handleSignUp())
            .catch((error) => this.handleError(error));
    }

    private handleSignUp(): void {
        if (this.redirectUrl) {
            window.location.href = this.redirectUrl;
        } else {
            this.router.navigate(['dashboard']);
        }

        this.spinnerService.hide();
    }

    private handleError(error: any): void {
        let message = error || error.message || '';

        this.notificationsService
            .error('Failed.', message);

        this.spinnerService.hide();
    }

}