import {Component, Input} from "@angular/core";
import {AppVm} from "../../business/apps/models/app-vm";
import {SignUp} from "../models/sign-up";
import {AuthBaseComponent} from "../auth-base.component";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {ActivatedRoute, Router} from "@angular/router";
import {PasswordlessService} from "../passwordless/services/passwordless.service";
import {Consts} from "../../../consts";


@Component({
    selector: 'au-sign-up-passwordless',
    template: `
        <div class="row">
            <div class="col-12 mt-3 mb-3 text-center">
                <span>Or sign up without password</span>
            </div>
            <div class="col-12">
                <button
                        class="w-100"
                        md-raised-button
                        color="accent"
                        (click)="sendCode()">receive {{isEmail ? 'an email' : 'a sms'}} with sign up link</button>
            </div>
        </div>
    `
})
export class SignUpPasswordlessComponent extends AuthBaseComponent {

    constructor(
        route: ActivatedRoute,
        router: Router,
        spinnerService: SpinnerService,
        private passwordlessService: PasswordlessService
    ) {
        super(route, router, spinnerService);
    }

    public ngOnInit(): void {
        this.isEmail = this.signUp.userName.indexOf('@') > -1;

        let stgs = this.isEmail ? this.app.emailSettings : this.app.phoneSettings;

        if (!stgs.isPasswordEnabled && stgs.isPasswordlessEnabled) {
            this.sendCode();
        }
    }

    @Input()
    public signUp: SignUp;

    @Input()
    public app: AppVm;

    public isEmail: boolean;

    public sendCode() {
        this.spinnerService.show();

        this.passwordlessService
            .sendSignUpLink({
                userName: this.signUp.userName,
                callbackUrl: this.buildSignUpCallbackUrl()
            })
            .subscribe(() => {
                this.router
                    .navigate(['/sign-up/passwordless-confirmation', {
                        userName: this.signUp.userName
                    }])
                    .then(() => this.spinnerService.hide());
            });
    }

    private buildSignUpCallbackUrl(): string {
        let arr = window.location.href.split('/');
        let result = arr[0] + '//' + arr[2];

        let url =
            result + '/sign-up/passwordless-confirmation;' +
            Consts.Code + '={code};' + Consts.UserName + '=' + encodeURIComponent(this.signUp.userName);

        return url;
    }
}