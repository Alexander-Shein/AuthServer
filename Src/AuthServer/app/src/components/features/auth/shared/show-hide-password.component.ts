import {Component, Input} from "@angular/core";


@Component({
    selector: 'au-show-hide-password',
    styleUrls: ['../auth.scss'],
    template: `
        <md-icon
                (click)="password.show = !password.show"
                class="au-show-password"
                mdTooltip="{{password.show ? 'Hide password' : 'Show password'}}"
                [mdTooltipPosition]="'above'">{{password.show ? 'visibility' : 'visibility_off'}}</md-icon>
    `
})
export class ShowHidePasswordComponent {

    @Input()
    public password: any;

}