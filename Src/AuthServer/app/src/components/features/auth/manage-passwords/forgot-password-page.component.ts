import {Component} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {AuthBaseComponent} from "../auth-base.component";
import {PasswordsService} from "./services/passwords.service";
import {UserName} from "../models/user-name";
import {SearchableExternalProvider} from "../external-log-in/models/searchable-external-provider";
import {AppVm} from "../../business/apps/models/app-vm";
import {UsersService} from "../services/users.service";
import {NotificationsService} from "angular2-notifications";


@Component({
    selector: 'au-forgot-password',
    templateUrl: './forgot-password-page.component.html',
    styleUrls: ['../auth.scss', './forgot-password-page.component.scss']
})
export class ForgotPasswordPageComponent extends AuthBaseComponent {

    constructor(
        private passwordsService: PasswordsService,
        private usersService: UsersService,
        private notificationsService: NotificationsService,
        route: ActivatedRoute,
        router: Router,
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
    public im: UserName = new UserName();
    public searchableProviders: SearchableExternalProvider[];

    public isCodeSent: boolean = false;
    public isEmail: boolean = false;

    public sendResetPasswordCode(): void {
        this.spinnerService.show();

        this.usersService
            .isUserNameExists(this.im.userName)
            .subscribe(
                () => {
                    this.passwordsService
                        .sendResetPasswordCode(this.im)
                        .subscribe(() => {
                            this.isEmail = this.im.userName.indexOf('@') != -1;
                            this.isCodeSent = true;
                            this.spinnerService.hide();
                        },
                            () => {
                                this.notificationsService
                                    .error('Failed.', 'Cannot send code. Please try again.');
                                this.spinnerService.hide();
                            });
                },
                () => {
                    this.notificationsService
                        .error('Failed.', 'AuthGuardian can\'t find user. Please try again.');
                    this.spinnerService.hide();
                });
    }

}