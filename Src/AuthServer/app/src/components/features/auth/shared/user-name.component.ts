import {Component, EventEmitter, Input, Output} from "@angular/core";
import {AppVm} from "../../business/apps/models/app-vm";
import {UserName} from "../models/user-name";
import {ExternalProvider} from "../external-log-in/models/external-provider";
import {AuthenticationService} from "../services/authentication.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {SearchableExternalProvider} from "../external-log-in/models/searchable-external-provider";
import {AuthBaseComponent} from "../auth-base.component";
import {ActivatedRoute, Router} from "@angular/router";


@Component({
    selector: 'au-user-name',
    template: `

        <form class="row" #userNameForm="ngForm" (submit)="onSubmit()" *ngIf="app.isLocalAccountEnabled">
            <div class="col">

                <div class="row">
                    <div class="col">
                        <md-input-container class="w-100" *ngIf="app.emailSettings.isEnabled && app.phoneSettings.isEnabled">
                            <input
                                    mdInput
                                    placeholder="Email or Phone"
                                    type="text"
                                    name="userName"
                                    maxlength="254"
                                    required
                                    emailOrPhone
                                    [(ngModel)]="im.userName"
                                    (input)="searchProvider()"
                                    #userName="ngModel">
                            <md-hint align="end">{{userName.value?.length || 0}} / 254</md-hint>
                            <md-hint *ngIf="userName.errors && (userName.dirty || userName.touched)" style="color: red;">
                                    <span [hidden]="!userName.errors.required">
                                        Email or Phone is required.
                                    </span>
                                <span [hidden]="!userName.errors.emailOrPhone || userName.errors.required">
                                        Email or Phone is not valid.
                                    </span>
                            </md-hint>
                        </md-input-container>

                        <md-input-container class="w-100" *ngIf="app.emailSettings.isEnabled && !app.phoneSettings.isEnabled">
                            <input
                                    mdInput
                                    placeholder="Email"
                                    type="text"
                                    name="userName"
                                    maxlength="254"
                                    required
                                    email
                                    [(ngModel)]="im.userName"
                                    (input)="searchProvider()"
                                    #userName="ngModel">
                            <md-hint align="end">{{userName.value?.length || 0}} / 254</md-hint>
                            <md-hint *ngIf="userName.errors && (userName.dirty || userName.touched)" style="color: red;">
                                    <span [hidden]="!userName.errors.required">
                                        Email is required.
                                    </span>
                                <span [hidden]="!userName.errors.email || userName.errors.required">
                                        Email is not valid.
                                    </span>
                            </md-hint>
                        </md-input-container>

                        <md-input-container class="w-100" *ngIf="!app.emailSettings.isEnabled && app.phoneSettings.isEnabled">
                            <input
                                    mdInput
                                    placeholder="Phone"
                                    type="text"
                                    name="userName"
                                    maxlength="50"
                                    required
                                    phone
                                    [(ngModel)]="im.userName"
                                    #userName="ngModel">
                            <md-hint align="end">{{userName.value?.length || 0}} / 50</md-hint>
                            <md-hint *ngIf="userName.errors && (userName.dirty || userName.touched)" style="color: red;">
                                    <span [hidden]="!userName.errors.required">
                                        Phone is required.
                                    </span>
                                <span [hidden]="!userName.errors.phone || userName.errors.required">
                                        Phone is not valid.
                                    </span>
                            </md-hint>
                        </md-input-container>
                    </div>
                </div>

                <div class="row mt-1">
                    <div class="col">
                        <button
                                class="w-100"
                                [disabled]="!userNameForm.form.valid"
                                md-raised-button color="primary">next</button>
                    </div>
                </div>

                <div class="row mt-1" *ngIf="searchResult">
                    <div class="col">
                        <au-social-network-button
                                [namePrefix]="'using'"
                                [namePostfix]="'?'"
                                (click)="externalLogIn(searchResult)"
                                [provider]="searchResult"
                        ></au-social-network-button>
                    </div>
                </div>
            </div>
        </form>
    `
})
export class UserNameComponent extends AuthBaseComponent {

    constructor(
        private authenticationService: AuthenticationService,
        route: ActivatedRoute,
        router: Router,
        spinnerService: SpinnerService
    ) {
        super(route, router, spinnerService);
    }

    @Input()
    public im: UserName;

    @Input()
    public app: AppVm;

    @Input()
    public searchableProviders: SearchableExternalProvider[];

    @Output()
    public next: EventEmitter<any> = new EventEmitter();

    public searchResult: ExternalProvider;

    public externalLogIn(externalProvider: ExternalProvider): void {
        this.authenticationService
            .externalLogIn(
                {
                    returnUrl: this.redirectUrl,
                    authenticationScheme: externalProvider.authenticationScheme
                });
    }

    public searchProvider(): void {
        this.searchResult = null;

        if (!this.im.userName) return;
        if (!this.searchableProviders) return;

        let userName = this.im.userName.toLowerCase();

        for(let item of this.searchableProviders) {
            for(let match of item.matches) {
                if(userName.indexOf(match) != -1) {
                    this.searchResult = item;
                    return;
                }
            }
        }
    }

    public onSubmit(): boolean {
        this.next.emit();
        return false;
    }
}