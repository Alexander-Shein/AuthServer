import {Component} from "@angular/core";
import {SignUp} from "../models/sign-up";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthenticationService} from "../services/authentication.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";
import {AuthBaseComponent} from "../auth-base.component";
import {AppVm} from "../../business/apps/models/app-vm";
import {ExternalProvider} from "../external-log-in/models/external-provider";


@Component({
    selector: 'au-sign-up',
    templateUrl: './sign-up-page.component.html',
    styleUrls: ['../auth.scss', './sign-up-page.component.scss']
})
export class SignUpPageComponent extends AuthBaseComponent {

    constructor(
        authenticationService: AuthenticationService,
        route: ActivatedRoute,
        router: Router,
        notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(route, router, authenticationService, notificationsService, spinnerService);
    }

    public ngOnInit(): void {
        this.route
            .data
            .subscribe((data: {app: AppVm}) => {
                this.app = data.app;
            });

        super.ngOnInit();
    }

    public app: AppVm;
    public signUp: SignUp = new SignUp();

    public onSubmit(): void {
        this.spinnerService.show();

        this.authenticationService
            .signUp(this.signUp)
            .then(() => this.redirectAfterLogin())
            .catch((error: any) => this.handleError(error));
    }

    public externalLogIn(externalProvider: ExternalProvider): void {
        this.authenticationService
            .externalLogIn(
                {
                    redirectUrl: this.redirectUrl,
                    authenticationScheme: externalProvider.authenticationScheme
                });
    }

}