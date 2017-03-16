import {Component} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthenticationService} from "../services/authentication.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";
import {AuthBaseComponent} from "../auth-base.component";
import {AddPassword} from "../models/add-password";


@Component({
    selector: 'au-add-password',
    templateUrl: './add-password-page.component.html',
    styleUrls: ['../auth.scss', './add-password-page.component.scss']
})
export class AddPasswordPageComponent extends AuthBaseComponent {

    constructor(
        private authenticationService: AuthenticationService,
        route: ActivatedRoute,
        router: Router,
        notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(route, router, notificationsService, spinnerService);
    }

    public addPassword: AddPassword = new AddPassword('', '');

    public onSubmit(): void {
        this.spinnerService.show();

        this.authenticationService
            .addPassword(this.addPassword)
            .then(() => this.redirectAfterLogin())
            .catch((error: any) => this.handleError(error));
    }

}