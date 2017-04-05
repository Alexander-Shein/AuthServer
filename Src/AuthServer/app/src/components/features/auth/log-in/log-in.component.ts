import {Component} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthBaseComponent} from '../auth-base.component';
import {AuthenticationService} from "../services/authentication.service";
import {LogIn} from "../models/log-in";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {ExternalProvider} from "../external-log-in/models/external-provider";
import {AppVm} from "../../business/apps/models/app-vm";
import {SearchableExternalProvider} from "../external-log-in/models/searchable-external-provider";
import {UsersService} from "../services/users.service";
import {NotificationsService} from "angular2-notifications";


@Component({
    selector: 'au-log-in',
    templateUrl: './log-in.component.html',
    styleUrls: ['../auth.scss', './log-in.component.scss']
})
export class LogInComponent extends AuthBaseComponent {

    constructor(
        route: ActivatedRoute,
        router: Router,
        private authenticationService: AuthenticationService,
        private usersService: UsersService,
        private notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(route, router, spinnerService);
    }

    public ngOnInit(): void {
        this.route
            .data
            .subscribe((data: {app: AppVm, searchableProviders: SearchableExternalProvider[]}) => {
                this.app = data.app;
                this.searchableProviders = data.searchableProviders;
            });

        super.ngOnInit();
    }

    public app: AppVm;
    public logIn: LogIn = new LogIn();

    public searchableProviders: SearchableExternalProvider[];
    public isValidUserName: boolean = false;
    public isEmail: boolean = false;

    public validateUserName(): void {
        this.spinnerService.show();

        this.usersService
            .isUserNameExists(this.logIn.userName)
            .subscribe(
                () => {
                    this.isValidUserName = true;
                    this.isEmail = this.logIn.userName.indexOf('@') != -1;
                    this.spinnerService.hide();
                },
                () => {
                    this.notificationsService
                        .error('Failed.', 'AuthGuardian can\'t find user. Please try again.');
                    this.isValidUserName = false;
                    this.spinnerService.hide()
                });
    }

    public externalLogIn(externalProvider: ExternalProvider): void {
        this.authenticationService
            .externalLogIn(
                {
                    returnUrl: this.redirectUrl,
                    authenticationScheme: externalProvider.authenticationScheme
                });
    }
}