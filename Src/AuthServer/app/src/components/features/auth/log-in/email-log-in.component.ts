import {Component, Input} from "@angular/core";
import {LogIn} from "../models/log-in";
import {AppVm} from "../../business/apps/models/app-vm";


@Component({
    selector: 'au-email-log-in',
    template: `

        <form class="row" #logInForm="ngForm" *ngIf="!isLinkSent">
            <div class="col-12">
                <div class="row">
                    <div class="col">
                        <md-input-container class="w-100">
                            <input
                                    mdInput
                                    placeholder="Password"
                                    type="password"
                                    name="password"
                                    maxlength="100"
                                    minlength="6"
                                    required
                                    [(ngModel)]="logIn.password"
                                    #password="ngModel">
                            <md-hint align="end">{{password.value?.length || 0}} / 100</md-hint>
                            <md-hint *ngIf="password.errors && (password.dirty || password.touched)" style="color: red;">
                                    <span [hidden]="!password.errors.required" >
                                        Password is required.
                                    </span>
                                <span [hidden]="!password.errors.minlength || password.errors.required">
                                        Min length is 6 characters for password.
                                    </span>
                            </md-hint>
                        </md-input-container>
                    </div>
                </div>
                <div class="row mt-1">
                    <div class="col">
                        <button
                                class="w-100"
                                [disabled]="!logInForm.form.valid"
                                md-raised-button color="primary">log in</button>
                    </div>
                </div>
            </div>
        </form>

        <div class="row" *ngIf="app.emailSettings.isPasswordlessEnabled && !isLinkSent">
            <div class="col-12 mt-3 mb-3 text-center">
                <span>Or log in without password</span>
            </div>
            <div class="col-12">
                <button
                        class="w-100"
                        md-raised-button color="primary" (click)="isLinkSent = true">send link to email</button>
            </div>
        </div>

        <div class="row" *ngIf="isLinkSent">
            <div class="col-12 mb-3 text-center">
                <md-icon color="primary">done</md-icon>
                <div><span>Please check your email. We've sent you a log in link.</span></div>
            </div>
            <div class="col-12">
                <button
                        class="w-100"
                        md-raised-button color="primary">resend</button>
            </div>
        </div>
`
})
export class EmailLogInComponent {
    @Input()
    public logIn: LogIn;

    @Input()
    public app: AppVm;

    @Input()
    public isLinkSent: boolean = false;
}