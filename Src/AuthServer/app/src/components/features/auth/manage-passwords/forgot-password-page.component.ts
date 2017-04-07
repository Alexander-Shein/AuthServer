import {Component} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {AuthBaseComponent} from "../auth-base.component";
import {PasswordsService} from "./services/passwords.service";
import {UserName} from "../models/user-name";
import {SearchableExternalProvider} from "../external-log-in/models/searchable-external-provider";
import {AppVm} from "../../business/apps/models/app-vm";


@Component({
    selector: 'au-forgot-password',
    templateUrl: './forgot-password-page.component.html',
    styleUrls: ['../auth.scss', './forgot-password-page.component.scss']
})
export class ForgotPasswordPageComponent extends AuthBaseComponent {

    constructor(
        private passwordsService: PasswordsService,
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

    private isEmailSent: boolean = false;

    public onSubmit(): void {
        this.spinnerService.show();

        this.passwordsService
            .forgotPassword(this.im)
            .then(() => this.handle())
            .catch(() => this.spinnerService.hide());
    }

    private handle(): void {
        this.isEmailSent = true;
        this.spinnerService.hide();
    }

}