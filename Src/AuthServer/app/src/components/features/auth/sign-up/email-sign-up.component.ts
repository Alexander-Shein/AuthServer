import {Component, Input, OnInit} from "@angular/core";
import {AppVm} from "../../business/apps/models/app-vm";
import {SignUp} from "../models/sign-up";


@Component({
    selector: 'au-email-sign-up',
    template: `

        <au-sign-up-password [signUp]="signUp" [app]="app" *ngIf="!isLinkSent"></au-sign-up-password>

        <div class="row" *ngIf="app.emailSettings.isPasswordlessEnabled && !isLinkSent">
            <div class="col-12 mt-3 mb-3 text-center">
                <span>Or sign up without password</span>
            </div>
            <div class="col-12">
                <button
                        class="w-100"
                        md-raised-button color="primary" (click)="sendLinkToEmail()">send link to email</button>
            </div>
        </div>

        <div class="row" *ngIf="isLinkSent">
            <div class="col-12 mb-3 text-center">
                <md-icon color="primary">done</md-icon>
                <div><span>Please check your email. We've sent you a sign up link.</span></div>
            </div>
            <div class="col-12">
                <button
                        class="w-100"
                        md-raised-button color="primary">resend</button>
            </div>
        </div>
`
})
export class EmailSignUpComponent implements OnInit{

    public ngOnInit(): void {
        let stgs = this.app.emailSettings;

        if (!stgs.isPasswordEnabled && stgs.isPasswordlessEnabled) {
            this.sendLinkToEmail();
        }
    }

    @Input()
    public signUp: SignUp;

    @Input()
    public app: AppVm;

    public isLinkSent: boolean = false;

    public sendLinkToEmail() {
        this.isLinkSent = true;
    }
}