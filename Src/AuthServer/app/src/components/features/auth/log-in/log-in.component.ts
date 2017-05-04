import {Component} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthBaseComponent} from '../auth-base.component';
import {AuthenticationService} from "../services/authentication.service";
import {LogIn} from "../models/log-in";
import {ExternalProvider} from "../external-log-in/models/external-provider";
import {AppVm} from "../../business/apps/models/app-vm";


@Component({
    selector: 'au-log-in',
    templateUrl: './log-in.component.html',
    styleUrls: ['../auth.scss', './log-in.component.scss']
})
export class LogInComponent extends AuthBaseComponent {

    constructor(
        route: ActivatedRoute,
        router: Router,
        private authenticationService: AuthenticationService
    ) {
        super(route, router);
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
    public logIn: LogIn = new LogIn();
    public isValidUserName: boolean = false;
    public isEmail: boolean = false;

    public next(): void{
        this.isValidUserName = true;
        this.isEmail = this.logIn.userName.indexOf('@') != -1;
    }

    public back(): void {
        this.isValidUserName = false;
        this.isEmail = false;
        this.logIn.password = '';
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