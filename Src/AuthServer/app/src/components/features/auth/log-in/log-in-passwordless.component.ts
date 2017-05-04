import {Component, Input} from "@angular/core";
import {AppVm} from "../../business/apps/models/app-vm";
import {AuthBaseComponent} from "../auth-base.component";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {ActivatedRoute, Router} from "@angular/router";
import {PasswordlessService} from "../passwordless/services/passwordless.service";
import {Consts} from "../../../consts";
import {LogIn} from "../models/log-in";


@Component({
    selector: 'au-log-in-passwordless',
    template: `
        <div class="row">
            <div class="col-12 mt-3 mb-3 text-center">
                <span>Or log in without password</span>
            </div>
            <div class="col-12">
                <button
                        class="w-100"
                        md-raised-button
                        color="accent"
                        (click)="sendCode()">receive {{isEmail ? 'an email' : 'a sms'}} with log in link</button>
            </div>
        </div>
    `
})
export class LogInPasswordlessComponent extends AuthBaseComponent {

    constructor(
        route: ActivatedRoute,
        router: Router,
        private spinnerService: SpinnerService,
        private passwordlessService: PasswordlessService
    ) {
        super(route, router);
    }

    public ngOnInit(): void {
        this.isEmail = this.logIn.userName.indexOf('@') > -1;

        let stgs = this.isEmail ? this.app.emailSettings : this.app.phoneSettings;

        if (!stgs.isPasswordEnabled && stgs.isPasswordlessEnabled) {
            this.sendCode();
        }
    }

    @Input()
    public logIn: LogIn;

    @Input()
    public app: AppVm;

    public isEmail: boolean;

    public sendCode() {
        this.spinnerService.show();

        this.passwordlessService
            .sendLogInLink({
                userName: this.logIn.userName,
                callbackUrl: this.buildLogInCallbackUrl()
            })
            .subscribe(() => {
                this.router
                    .navigate(['/log-in/passwordless-confirmation', {
                        userName: this.logIn.userName
                    }])
            }, () => {
                this.spinnerService.hide();
            });
    }

    private buildLogInCallbackUrl(): string {
        let arr = window.location.href.split('/');
        let result = arr[0] + '//' + arr[2];

        let url =
            result + '/log-in/passwordless-confirmation;' +
            Consts.Code + '={code};' + Consts.UserName + '=' + encodeURIComponent(this.logIn.userName);

        return url;
    }
}