import { Component } from '@angular/core';
import { Router } from '@angular/router'
import { UsersService } from '../services/users.service';
import { ChangePassword } from '../models/change-password';

@Component({
    selector: 'au-change-password',
    templateUrl: './change-password-page.component.html',
    styleUrls: ['./change-password-page.component.scss']
})
export class ChangePasswordPageComponent {
    constructor(
        private usersService: UsersService,
        private router:Router) {}

    public changePassword: ChangePassword = new ChangePassword('', '', '');

    public onSubmit():void {
        this.usersService
            .changePassword(this.changePassword)
            .then(() => {
                this.router.navigate(['dashboard']);
            })
            .catch(() => {
            });
    }
}