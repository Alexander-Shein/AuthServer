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
    templateUrl: './log-in.component.html',
    styleUrls: ['../auth.scss', './log-in.component.scss']
})
export class LogInComponent extends AuthBaseComponent {

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

    public next(): void{
        this.isValidUserName = true;
        this.isEmail = this.logIn.userName.indexOf('@') != -1;
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