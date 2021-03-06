import {Component} from "@angular/core";
import {SignUp} from "../models/sign-up";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthenticationService} from "../services/authentication.service";
import {AuthBaseComponent} from "../auth-base.component";
import {AppVm} from "../../business/apps/models/app-vm";
import {ExternalProvider} from "../external-log-in/models/external-provider";


@Component({
    selector: 'au-sign-up',
    templateUrl: './sign-up.component.html',
    styleUrls: ['../auth.scss', './sign-up.component.scss']
})
export class SignUpComponent extends AuthBaseComponent {

    constructor(
        private authenticationService: AuthenticationService,
        route: ActivatedRoute,
        router: Router
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
    public signUp: SignUp = new SignUp();
    public isValidUserName: boolean = false;
    public isEmail: boolean = false;

    public next(): void {
        this.isValidUserName = true;
        this.isEmail = this.signUp.userName.indexOf('@') != -1;
    }

    public back(): void {
        this.isValidUserName = false;
        this.isEmail = false;
        this.signUp.password = '';
        this.signUp.confirmPassword = '';
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