import {Component, Input} from "@angular/core";
import {LogIn} from "../models/log-in";
import {AppVm} from "../../business/apps/models/app-vm";


@Component({
    selector: 'au-log-in-password',
    template: `

        <form class="row" #logInForm="ngForm">
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
                <div class="row mt-1" *ngIf="app.isRememberLogInEnabled">
                    <div class="col text-left">
                        <div>
                            <md-checkbox
                                    color="primary"
                                    name="remember"
                                    [(ngModel)]="logIn.rememberLogIn">
                                <span>remember me </span>
                            </md-checkbox>
                            <md-icon style="position: absolute; margin-left: 3px;"
                                     mdTooltip="Protect your account. Uncheck if using public/shared device."
                                     [mdTooltipPosition]="'above'">info_outline</md-icon>
                        </div>
                    </div>
                </div>
                <div class="row text-left">
                    <a [routerLink]="['/forgot-password']" [queryParams]="{redirectUrl: redirectUrl}" class="col-12">Forgot your password?</a>
                </div>
            </div>
        </form>
    `
})
export class LogInPasswordComponent {

    @Input()
    public logIn: LogIn;

    @Input()
    public app: AppVm;
}