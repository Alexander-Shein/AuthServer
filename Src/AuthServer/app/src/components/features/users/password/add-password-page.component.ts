import { Component } from '@angular/core';
import { Router } from '@angular/router'
import { UsersService } from '../services/users.service';
import { Password } from '../models/password';

@Component({
    selector: 'au-add-password',
    templateUrl: './add-password-page.component.html',
    styleUrls: ['./add-password-page.component.scss']
})
export class AddPasswordPageComponent {
    constructor(
        private usersService: UsersService,
        private router:Router) {}

    public password: Password = new Password('', '');

    public onSubmit():void {
        this.usersService
            .addPassword(this.password)
            .then(() => {
                this.router.navigate(['dashboard']);
            })
            .catch(() => {
            });
    }
}