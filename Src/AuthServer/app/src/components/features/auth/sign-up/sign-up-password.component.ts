import {Component, Input} from "@angular/core";
import {AppVm} from "../../business/apps/models/app-vm";
import {AuthBaseComponent} from "../auth-base.component";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthenticationService} from "../services/authentication.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {SignUp} from "../models/sign-up";


@Component({
    selector: 'au-sign-up-password',
    styleUrls: ['../auth.scss'],
    template: `

        <form class="row mt-1" #signUpForm="ngForm" (submit)="onSubmit()">
            <div class="col-12">
                <div class="row">
                    <div class="col">
                        <md-input-container class="w-100">
                            <input
                                    autofocus
                                    type="{{show ? 'text' : 'password'}}"
                                    mdInput
                                    placeholder="Password"
                                    type="password"
                                    name="password"
                                    maxlength="100"
                                    minlength="6"
                                    required
                                    [(ngModel)]="signUp.password"
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
                            <md-icon
                                    (click)="show = !show"
                                    class="au-show-password"
                                    mdTooltip="Show password"
                                    color="primary"
                                    [mdTooltipPosition]="'above'">remove_red_eye</md-icon>
                        </md-input-container>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <md-input-container class="w-100">
                            <input
                                    type="{{showConfirmation ? 'text' : 'password'}}"
                                    mdInput
                                    placeholder="Confirm password"
                                    type="password"
                                    name="confirmPassword"
                                    maxlength="100"
                                    minlength="6"
                                    required
                                    [(ngModel)]="signUp.confirmPassword"
                                    #confirmPassword="ngModel">
                            <md-hint align="end">{{confirmPassword.value?.length || 0}} / 100</md-hint>
                            <md-hint *ngIf="confirmPassword.errors && (confirmPassword.dirty || confirmPassword.touched)" style="color: red;">
                                <span [hidden]="!confirmPassword.errors.required" >
                                    Confirm password is required.
                                </span>
                                <span [hidden]="!confirmPassword.errors.minlength || confirmPassword.errors.required">
                                    Min length is 6 characters for password.
                                </span>
                            </md-hint>
                            <md-icon
                                    (click)="showConfirmation = !showConfirmation"
                                    class="au-show-password"
                                    mdTooltip="Show password"
                                    color="primary"
                                    [mdTooltipPosition]="'above'">remove_red_eye</md-icon>
                        </md-input-container>
                    </div>
                </div>
                <div class="row mt-1">
                    <div class="col">
                        <button
                                class="w-100"
                                [disabled]="!signUpForm.form.valid"
                                md-raised-button color="primary">sign up</button>
                    </div>
                </div>
            </div>
        </form>
    `
})
export class SignUpPasswordComponent extends AuthBaseComponent {

    constructor(
        route: ActivatedRoute,
        router: Router,
        private authenticationService: AuthenticationService,
        spinnerService: SpinnerService
    ) {
        super(route, router, spinnerService);
    }

    @Input()
    public signUp: SignUp;

    @Input()
    public app: AppVm;

    public show: boolean = false;
    public showConfirmation: boolean = false;

    public onSubmit() {
        this.spinnerService.show();

        this.authenticationService
            .signUp(this.signUp)
            .subscribe(
                () => this.redirectAfterLogin(),
                () => this.spinnerService.hide());
    }

}