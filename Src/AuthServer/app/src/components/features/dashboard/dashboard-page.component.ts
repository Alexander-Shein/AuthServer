import {Component, OnInit} from "@angular/core";
import {ActivatedRoute} from "@angular/router";
import {SpinnerService} from "../../common/spinner/services/spinner.service";
import {PhonesService} from "../auth/manage-phones/services/phones.service";
import {TwoFactorService} from "../auth/two-factor/services/two-factor.service";
import {App} from "../business/apps/models/app";
import {ConfirmationDialogComponent} from "../../common/pop-ups/confirmation-dialog.component";
import {MdDialog} from "@angular/material";
import {TwoFactorSettings} from "../auth/two-factor/models/two-factor-settings";
import {User} from "../auth/models/user";


@Component({
    selector: 'au-dashboard',
    templateUrl: './dashboard-page.component.html',
    styleUrls: ['./dashboard-page.component.scss']
})
export class DashboardPageComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private spinnerService: SpinnerService,
        private phonesService: PhonesService,
        private dialog: MdDialog,
        private twoFactorService: TwoFactorService) {}

    public user: User;
    public apps: App[];

    public ngOnInit(): void {
        this.route.data
            .subscribe((data: {user: User, apps: App[]}) => {
                this.user = data.user;
                this.apps = data.apps;
            });
    }

    public deletePhoneNumber(): boolean {
        let dialogRef = this.dialog.open(ConfirmationDialogComponent);
        dialogRef.afterClosed().subscribe((result: boolean) => {
            if (result) {
                this.spinnerService.show();

                this.phonesService
                    .remove()
                    .then(() => {
                        this.user.phoneNumber = '';
                        this.spinnerService.hide();
                    })
                    .catch(() => this.spinnerService.hide());
            }
        });

        return false;
    }

    public toggleTwoFactor(): void {
        this.spinnerService.show();

        let twoFactorSettings: TwoFactorSettings = {
            enabled: this.user.isTwoFactorEnabled
        };

        this.twoFactorService
            .updateTwoFactorSettings(twoFactorSettings)
            .subscribe(
                () => this.spinnerService.hide(),
                () => this.spinnerService.hide());
    }

}