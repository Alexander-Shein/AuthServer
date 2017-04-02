import {Component, Input, OnInit} from "@angular/core";
import {LogIn} from "../models/log-in";
import {AppVm} from "../../business/apps/models/app-vm";


@Component({
    selector: 'au-phone-log-in',
    template: `

        <au-log-in-password [logIn]="logIn" [app]="app" *ngIf="!isCodeSent"></au-log-in-password>

        <div class="row" *ngIf="app.phoneSettings.isPasswordlessEnabled && !isCodeSent">
            <div class="col-12 mt-3 mb-3 text-center">
                <span>Or log in without password</span>
            </div>
            <div class="col-12">
                <button
                        class="w-100"
                        md-raised-button color="primary" (click)="sendCodeToPhone()">send sms with code</button>
            </div>
        </div>

        <form class="row" #codeConfirmation="ngForm" *ngIf="isCodeSent">
            <div class="col-12 mb-3 text-center">
                <md-icon color="primary">done</md-icon>
                <div><span>Please check your phone. We've sent you a sms with code.</span></div>
            </div>
            <div class="col-12">
                <md-input-container class="w-100">
                    <input
                            mdInput
                            placeholder="Code"
                            type="code"
                            name="text"
                            maxlength="100"
                            required
                            [(ngModel)]="logIn.password"
                            #code="ngModel">
                    <md-hint align="end">{{code.value?.length || 0}} / 100</md-hint>
                    <md-hint *ngIf="code.errors && (code.dirty || code.touched)" style="color: red;">
                                    <span [hidden]="!code.errors.required" >
                                        code is required.
                                    </span>
                    </md-hint>
                </md-input-container>
            </div>
            <div class="col-12">
                <button
                        class="w-100"
                        [disabled]="!codeConfirmation.form.valid"
                        md-raised-button color="primary">log in</button>
            </div>
        </form>
        <div class="row mt-1" *ngIf="isCodeSent">
            <div class="col-12">
                <button
                        class="w-100"
                        md-raised-button>resend</button>
            </div>
        </div>
    `
})
export class PhoneLogInComponent implements OnInit{

    public ngOnInit(): void {
        let stgs = this.app.phoneSettings;

        if (!stgs.isPasswordEnabled && stgs.isPasswordlessEnabled) {
            this.sendCodeToPhone();
        }
    }

    @Input()
    public logIn: LogIn;

    @Input()
    public app: AppVm;

    public code: string;

    public isCodeSent: boolean = false;

    public sendCodeToPhone() {
        this.isCodeSent = true;
    }
}