import {Component, OnInit} from "@angular/core";
import {ActivatedRoute} from "@angular/router";
import {SpinnerService} from "../../common/spinner/services/spinner.service";
import {App} from "../business/apps/models/app";
import {ConfirmationDialogComponent} from "../../common/pop-ups/confirmation-dialog.component";
import {MdDialog} from "@angular/material";
import {User} from "../auth/models/user";
import {UsersService} from "../auth/services/users.service";
import {UserIm} from "../auth/models/user-im";


@Component({
    selector: 'au-dashboard',
    templateUrl: './dashboard-page.component.html',
    styleUrls: ['./dashboard-page.component.scss']
})
export class DashboardPageComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private usersService: UsersService,
        private spinnerService: SpinnerService,
        private dialog: MdDialog) {}

    public user: User;
    public apps: App[];

    public ngOnInit(): void {
        this.route.data
            .subscribe((data: {user: User, apps: App[]}) => {
                this.user = data.user;
                this.apps = data.apps;
            });
    }

    public deleteEmail(): boolean {
        let dialogRef = this.dialog.open(ConfirmationDialogComponent);
        dialogRef.afterClosed().subscribe((result: boolean) => {
            if (result) {
                this.spinnerService.show();

                let im: UserIm = {
                    phoneNumber: null,
                    phoneNumberCode: null,
                    email: '',
                    emailCode: null,
                    isTwoFactorEnabled: null
                };

                this.usersService
                    .update(im)
                    .subscribe((user: User) => {
                        this.user = user;
                        this.spinnerService.hide();
                    }, () => this.spinnerService.hide());
            }
        });

        return false;
    }

    public deletePhoneNumber(): boolean {
        let dialogRef = this.dialog.open(ConfirmationDialogComponent);
        dialogRef.afterClosed().subscribe((result: boolean) => {
            if (result) {
                this.spinnerService.show();

                let im: UserIm = {
                    phoneNumber: '',
                    phoneNumberCode: null,
                    email: null,
                    emailCode: null,
                    isTwoFactorEnabled: null
                };

                this.usersService
                    .update(im)
                    .subscribe((user: User) => {
                        this.user = user;
                        this.spinnerService.hide();
                    }, () => this.spinnerService.hide());
            }
        });

        return false;
    }

    public toggleTwoFactor(): void {
        this.spinnerService.show();

        let im: UserIm = {
            phoneNumber: null,
            phoneNumberCode: null,
            email: null,
            emailCode: null,
            isTwoFactorEnabled: this.user.isTwoFactorEnabled
        };

        this.usersService
            .update(im)
            .subscribe((user: User) => {
                this.user = user;
                this.spinnerService.hide();
            }, () => this.spinnerService.hide());
    }

}