import {Component, Input} from "@angular/core";
import {LogIn} from "../models/log-in";


@Component({
    selector: 'au-phone-log-in',
    template: `
    <div class="row">
        <md-icon color="primary" class="col-auto mt-3 ml-3">perm_identity</md-icon>
        <div class="col">
            <md-input-container class="w-100">
                <input
                        mdInput
                        #phone
                        placeholder="Phone number"
                        type="text"
                        name="phone"
                        maxlength="100"
                        required
                        [(ngModel)]="logIn.phone">
                <md-hint align="end">{{phone.value.length}} / 100</md-hint>
            </md-input-container>
        </div>
    </div>
    
    <div class="row">
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
`
})
export class PhoneLogInComponent {
    @Input()
    public logIn: LogIn;
}