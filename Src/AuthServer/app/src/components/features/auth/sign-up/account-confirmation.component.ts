import {Component} from "@angular/core";
import {AuthBaseComponent} from "../auth-base.component";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {Consts} from "../../../consts";
import {VerificationCode} from "../manage-passwords/models/verification-code";


@Component({
    selector: 'au-account-confirmation',
    template: `
        <div class="container-fluid mt-2">

            <div class="row justify-content-center">
                <div class="col-12 col-md-6 col-lg-5 col-xl-4" style="max-width: 320px;">

                    <div class="row">
                        <div class="col-12">
                            <h3>
                                <span style="vertical-align: text-top;">Account confirmation</span>
                            </h3>
                        </div>
                    </div>
                    
                    <div color="row">
                        <div class="col-12">
                            <au-icon-user-name [userName]="userName"></au-icon-user-name>
                        </div>
                    </div>

                    <form #confirmAccountForm="ngForm" class="row" (ngSubmit)="confirmAccount()">
                        <div class="col-12 text-center">
                            <md-icon color="primary">done</md-icon>
                            <div>
                                <span *ngIf="isEmail">Please check your email. We've sent you an email with code.</span>
                                <span *ngIf="!isEmail">Please check your phone. We've sent you a sms with code.</span>
                                <span>Enter a code or use link.</span>
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
                                        maxlength="100"
                                        required
                                        [(ngModel)]="im.code">
                                <md-hint align="end">{{code.value?.length || 0}} / 100</md-hint>
                            </md-input-container>
                        </div>

                        <div class="col-12 mt-1">
                            <button
                                    [disabled]="!confirmAccountForm.form.valid"
                                    class="w-100"
                                    md-raised-button
                                    color="primary">confirm</button>
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
export class AccountConfirmationComponent extends AuthBaseComponent {

    constructor(
        route: ActivatedRoute,
        router: Router,
        spinnerService: SpinnerService
    ) {
        super(route, router, spinnerService);
    }

    public ngOnInit(): void {
        this.route
            .params
            .subscribe((params: Params) => {
                this.userName = params[Consts.UserName];
                this.isEmail = this.userName.indexOf('@') > -1;
            });

        super.ngOnInit();
    }

    public userName: string;
    public isEmail: boolean;
    public im: VerificationCode = new VerificationCode();

    public confirmAccount(): void {

    }

}