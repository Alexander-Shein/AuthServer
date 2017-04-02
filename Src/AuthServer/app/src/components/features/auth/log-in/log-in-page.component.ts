import {Component} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthBaseComponent} from '../auth-base.component';
import {AuthenticationService} from "../services/authentication.service";
import {LogIn} from "../models/log-in";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {ExternalProvider} from "../external-log-in/models/external-provider";
import {AppVm} from "../../business/apps/models/app-vm";
import {SearchableExternalProvider} from "../external-log-in/models/searchable-external-provider";


@Component({
    selector: 'au-log-in',
    templateUrl: './log-in-page.component.html',
    styleUrls: ['../auth.scss', './log-in-page.component.scss']
})
export class LogInPageComponent extends AuthBaseComponent {

    constructor(
        route: ActivatedRoute,
        router: Router,
        private authenticationService: AuthenticationService,
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

        this.authenticationService
            .isUserNameExists(this.logIn.userName)
            .subscribe(
                () => {
                    this.isValidUserName = true;
                    this.isEmail = this.logIn.userName.indexOf('@') != -1;
                    this.spinnerService.hide();
                },
                () => this.spinnerService.hide());
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