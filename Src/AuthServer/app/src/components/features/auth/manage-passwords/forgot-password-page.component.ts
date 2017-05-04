import {Component} from '@angular/core';
import {ActivatedRoute, Params, Router} from "@angular/router";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {AuthBaseComponent} from "../auth-base.component";
import {PasswordsService} from "./services/passwords.service";
import {UserName} from "../models/user-name";
import {AppVm} from "../../business/apps/models/app-vm";
import {VerificationCode} from "./models/verification-code";
import {Consts} from "../../../consts";


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
        private spinnerService: SpinnerService
    ) {
        super(route, router);
    }

    public ngOnInit(): void {
        this.route
            .data
            .subscribe((data: {app: AppVm}) => {
                this.app = data.app;
            });

        this.route
            .params
            .subscribe((params: Params) => {
                let userName = params[Consts.UserName];

                if (userName) {
                    this.im.userName = userName;
                }
            });

        super.ngOnInit();
    }

    public app: AppVm;
    public im: UserName = new UserName();
    public verificationCode: VerificationCode = new VerificationCode();

    public isCodeSent: boolean = false;
    public isEmail: boolean = false;

    public next(): void {
        this.spinnerService.show();

        this.passwordsService
            .sendResetPasswordCode(this.im)
            .subscribe(() => {
                    this.isEmail = this.im.userName.indexOf('@') != -1;
                    this.isCodeSent = true;
                    this.spinnerService.hide();
                },
                () => this.spinnerService.hide());
    }

}