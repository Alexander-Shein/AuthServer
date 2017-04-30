import {Component} from "@angular/core";
import {AuthBaseComponent} from "../auth-base.component";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {Consts} from "../../../consts";
import {PasswordlessService} from "../passwordless/services/passwordless.service";
import {Code} from "../shared/models/code";


@Component({
    selector: 'au-sign-up-passwordless-confirmation-page',
    template: `
        <div class="container-fluid mt-2">

            <div class="row justify-content-center">
                <div class="col-12 col-md-6 col-lg-5 col-xl-4" style="max-width: 320px;">

                    <div class="row">
                        <div class="col-12 text-center">
                            <h3>
                                <span style="vertical-align: text-top;">Sign up</span>
                            </h3>
                        </div>
                    </div>

                    <div color="row">
                        <div class="col-12">
                            <au-icon-user-name [userName]="userName"></au-icon-user-name>
                        </div>
                    </div>

                    <form #signUpForm="ngForm" class="row" (ngSubmit)="signUp()">
                        <div class="col-12 text-center">
                            <md-icon color="primary">done</md-icon>
                            <div>
                                <span *ngIf="isEmail">Please check your email. We've sent you an email with code.</span>
                                <span *ngIf="!isEmail">Please check your phone. We've sent you a sms with code.</span>
                                <div><span>Use link in the message or enter a code.</span></div>
                            </div>
                        </div>
                        <div class="col-12">
                            <md-input-container class="w-100">
                                <input
                                        mdInput
                                        #code="ngModel"
                                        placeholder="Code"
                                        type="text"
                                        name="code"
                                        maxlength="4"
                                        minlength="4"
                                        required
                                        [(ngModel)]="im.code">
                                <md-hint align="end">{{code.value?.length || 0}} / 4</md-hint>
                                <md-hint *ngIf="code.errors && (code.dirty || code.touched)" style="color: red;">
                                <span [hidden]="!code.errors.required">
                                    Code is required.
                                </span>
                                    <span [hidden]="!code.errors.minlength || code.errors.required">
                                    Code must be 4 digits.
                                </span>
                                </md-hint>
                            </md-input-container>
                        </div>

                        <div class="col-12 mt-1">
                            <button
                                    [disabled]="!signUpForm.form.valid"
                                    class="w-100"
                                    md-raised-button
                                    color="primary">sign up</button>
                        </div>
                    </form>

                    <div class="row">
                        <div class="col-12 text-right">
                            <span style="font-size: 10px;">Protected by <strong>AuthGuardian</strong></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `
})
export class SignUpPasswordlessConfirmationPageComponent extends AuthBaseComponent {

    constructor(
        route: ActivatedRoute,
        router: Router,
        spinnerService: SpinnerService,
        private passwordlessService: PasswordlessService) {
        super(route, router, spinnerService);
    }

    public ngOnInit(): void {
        super.ngOnInit();

        this.route
            .params
            .subscribe((params: Params) => {

                let code = params[Consts.Code];

                if (code) {
                    this.im.code = code;
                    this.signUp();

                } else {
                    this.userName = params[Consts.UserName];
                    this.isEmail = this.userName.indexOf('@') > -1;
                }
            });
    }

    public userName: string;
    public isEmail: boolean;
    public im: Code = new Code();

    public signUp(): void {
        this.spinnerService.show();

        this.passwordlessService
            .signUp(this.im)
            .subscribe(
                () => this.redirectAfterLogin(),
                () => this.spinnerService.hide()
            );
    }

}