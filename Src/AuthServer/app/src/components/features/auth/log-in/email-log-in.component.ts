import {Component, Input, OnInit} from "@angular/core";
import {LogIn} from "../models/log-in";
import {AppVm} from "../../business/apps/models/app-vm";


@Component({
    selector: 'au-email-log-in',
    template: `

        <au-log-in-password [logIn]="logIn" [app]="app" *ngIf="!isLinkSent"></au-log-in-password>

        <div class="row" *ngIf="app.emailSettings.isPasswordlessEnabled && !isLinkSent">
            <div class="col-12 mt-2 mb-3 text-center">
                <span>Or</span>
            </div>
            <div class="col-12">
                <button
                        class="w-100"
                        md-raised-button
                        color="accent"
                        (click)="sendLinkToEmail()">receive an email with log in link</button>
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
export class EmailLogInComponent implements OnInit{

    public ngOnInit(): void {
        let stgs = this.app.emailSettings;

        if (!stgs.isPasswordEnabled && stgs.isPasswordlessEnabled) {
            this.sendLinkToEmail();
        }
    }

    @Input()
    public logIn: LogIn;

    @Input()
    public app: AppVm;

    public isLinkSent: boolean = false;

    public sendLinkToEmail() {
        this.isLinkSent = true;
    }
}