import {Component, Input, OnInit} from "@angular/core";


@Component({
    selector: 'au-icon-user-name',
    template: `
        <div>
            <md-icon style="vertical-align: bottom;">{{isEmail ? 'email' : 'phone'}}</md-icon>
            <span>{{userName}}</span>
        </div>
    `
})
export class IconUserNameComponent implements OnInit{

    @Input()
    public userName: string;
    public isEmail: boolean;

    public ngOnInit(): void {
        this.isEmail = this.userName.indexOf('@') > -1;
    }

}