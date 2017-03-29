import {Component, Input} from "@angular/core";
import {LogIn} from "../models/log-in";
import {AppVm} from "../../business/apps/models/app-vm";


@Component({
    selector: 'au-email-log-in',
    template: `
        <div class="row">
            <div class="col-12">
                <button
                        class="w-100"
                        md-raised-button color="primary">send link to email</button>
            </div>
        </div>

        <form class="row" #logInForm="ngForm">
            <div class="col-12 mt-3 mb-3" *ngIf="app.emailSettings.isPasswordEnabled">
                <span>Or use password</span>
            </div>
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
`
})
export class EmailLogInComponent {
    @Input()
    public logIn: LogIn;

    @Input()
    public app: AppVm;
}