import {Component, Input} from "@angular/core";
import {LogIn} from "../models/log-in";
import {AppVm} from "../../business/apps/models/app-vm";


@Component({
    selector: 'au-email-log-in',
    template: `
    <div class="row mt-1">
        <md-icon color="primary" class="col-auto mt-3 ml-3">perm_identity</md-icon>
        <div class="col">
            <md-input-container class="w-100">
                <input
                        mdInput
                        #email
                        placeholder="Email"
                        type="email"
                        name="email"
                        maxlength="100"
                        required
                        [(ngModel)]="logIn.email">
                <md-hint align="end">{{email.value.length}} / 100</md-hint>
            </md-input-container>
        </div>
    </div>
    
    <div class="row" *ngIf="app.emailSettings.isPasswordEnabled">
        <md-icon color="primary" class="col-auto mt-3 ml-3">lock_open</md-icon>
        <div class="col">
            <md-input-container class="w-100">
                <input
                        mdInput
                        #password
                        name="password"
                        placeholder="Password"
                        type="password"
                        maxlength="100"
                        required
                        [(ngModel)]="logIn.password">
                <md-hint align="end">{{password.value.length}} / 100</md-hint>
            </md-input-container>
        </div>
    </div>
    
    <div class="row mt-1">
        <div class="col text-right">
            <button [disabled]="!logIn.email" md-raised-button color="primary">log in</button>
        </div>
    </div>
`
})
export class EmailLogInComponent {
    @Input()
    public logIn: LogIn;

    @Input()
    public app: AppVm;
}