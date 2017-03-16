import {Component} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthenticationService} from "../services/authentication.service";
import {NotificationsService} from "angular2-notifications";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {AuthBaseComponent} from "../auth-base.component";
import {AddPhoneNumber} from "../models/add-phone-number";


@Component({
    selector: 'au-add-phone',
    templateUrl: './add-phone-page.component.html',
    styleUrls: ['../auth.scss', './add-phone-page.component.scss']
})
export class AddPhonePageComponent extends AuthBaseComponent {

    constructor(
        private authenticationService: AuthenticationService,
        route: ActivatedRoute,
        router: Router,
        notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(route, router, notificationsService, spinnerService);
    }

    public addPhoneNumber: AddPhoneNumber = new AddPhoneNumber('', null);
    public isCodeSent: boolean = false;

    public sendCode(): void {
        this.authenticationService
            .sendVerificationCode(this.addPhoneNumber)
            .then(() => {
                this.isCodeSent = true;
                this.spinnerService.hide();
            })
            .catch((error: any) => this.handleError(error));
    }

    public verifyCode(): void {
        this.spinnerService.show();

        this.authenticationService
            .addPhoneNumber(this.addPhoneNumber)
            .then(() => this.redirectAfterLogin())
            .catch((error: any) => this.handleError(error));
    }
}